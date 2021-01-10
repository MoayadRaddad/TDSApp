﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TSDApp.Forms;

namespace TSDApp.Fomrs
{
    public partial class AddEditScreen : Form
    {
        #region Variables
        private int BankId;
        private static Models.Screen CurrentScreen;
        public static List<Models.Button> LstButtons = new List<Models.Button>();
        public static IEnumerable<Models.Button> IEnumrableLstButtons;
        #endregion

        #region constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public AddEditScreen()
        {
            try
            {
                InitializeComponent();
                ddlActive.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Parametarize constructor (if new screen)
        /// </summary>
        /// <param name="pBankId"></param>
        public AddEditScreen(int pBankId)
        {
            try
            {
                InitializeComponent();
                ddlActive.DropDownStyle = ComboBoxStyle.DropDownList;
                LstButtons = new List<Models.Button>();
                IEnumrableLstButtons = new List<Models.Button>().ToList();
                BankId = pBankId;
                ddlActive.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Parametarize constructor (if edit screen)
        /// </summary>
        /// <param name="pBankId"></param>
        /// <param name="pScreenId"></param>
        public AddEditScreen(int pBankId, Models.Screen pScreen)
        {
            try
            {
                InitializeComponent();
                ddlActive.DropDownStyle = ComboBoxStyle.DropDownList;
                BankId = pBankId;
                FillScreens(pScreen);
                FillButtons();
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        public AddEditScreen(int pBankId, Models.Screen pScreen, Models.Button pNewButton, Models.Button pOldButton)
        {
            try
            {
                InitializeComponent();
                ddlActive.DropDownStyle = ComboBoxStyle.DropDownList;
                BankId = pBankId;
                FillScreens(pScreen);
                if (pOldButton == null && pNewButton != null)
                {
                    LstButtons.Add(pNewButton);
                    IEnumrableLstButtons = Models.SharingMethods.GetIEnumrable(LstButtons).ToList();
                }
                else if (pNewButton != null)
                {
                    LstButtons[LstButtons.FindIndex(x => x.LstIndex == pOldButton.LstIndex)] = pNewButton;
                    IEnumrableLstButtons = Models.SharingMethods.GetIEnumrable(LstButtons).ToList();
                }
                gvButtons.DataSource = LstButtons.ToList();
                SetdataGridViewDisplay();
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Load function to fill needed data
        /// </summary>
        private void AddEditScreen_Load(object sender, EventArgs e)
        {
            try
            {
                LoadToolTips();
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Save button to insert or update current screen
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text != "")
                {
                    if (ddlActive.SelectedIndex != 0)
                    {
                        if (gvButtons.RowCount > 0)
                        {
                            if (CurrentScreen.id == 0)
                            {
                                CurrentScreen = BusinessAccessLayer.Screen.Screen.InsertScreen(CurrentScreen);
                                if (CurrentScreen == null)
                                {
                                    MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                CurrentScreen.Name = txtName.Text;
                                CurrentScreen.isActive = Convert.ToBoolean(ddlActive.SelectedItem);
                                CurrentScreen = BusinessAccessLayer.Screen.Screen.UpdateScreen(CurrentScreen);
                                if (CurrentScreen == null)
                                {
                                    MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                lblGVTitle.Text = CurrentScreen.Name;
                            }
                            foreach (Models.Button pbutton in LstButtons)
                            {
                                if (pbutton.id == 0)
                                {
                                    pbutton.ScreenId = CurrentScreen.id;
                                    Models.Button btnInsertCheck = BusinessAccessLayer.Button.Button.InsertButton(pbutton);
                                    if (btnInsertCheck == null)
                                    {
                                        MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                else if (pbutton.Updated == true)
                                {
                                    Models.Button btnUpdateCheck = BusinessAccessLayer.Button.Button.UpdateButton(pbutton);
                                    if (btnUpdateCheck == null)
                                    {
                                        MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }
                            MessageBox.Show("Button and screen had been saved successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Screen cannot be saved because there is no buttons added to this screen", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select active mode", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please fill screen Name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Function to delete current screen and cancel button to check if screen have buttons or not if not then delete screen
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentScreen = null;
                TSD tsd = null;
                LstButtons = null;
                IEnumrableLstButtons = null;
                this.Dispose();
                tsd = new TSD(true);
                tsd.Show();
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Override OnFormClosing method to dispose current form, check if screen have buttons or not if not then delete screen and move to the previous form
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                CurrentScreen = null;
                TSD tsd = null;
                LstButtons = null;
                IEnumrableLstButtons = null;
                this.Dispose();
                tsd = new TSD(true);
                tsd.Show();
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Delete selected screen/s and delete buttons for the deleted screen/s
        /// </summary>
        private void btnDeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvButtons.SelectedRows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show(@"Are you sure you want to delete selected button\s ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        List<int> pButtonsIds = new List<int>();
                        foreach (DataGridViewRow buttonRow in gvButtons.SelectedRows)
                        {
                            pButtonsIds.Add((int)buttonRow.Cells["id"].Value);
                            LstButtons.Remove((LstButtons.Where(x => x.id == (int)buttonRow.Cells["id"].Value).FirstOrDefault()));
                        }
                        int CheckDelete = BusinessAccessLayer.Button.Button.DeleteButtonWhere(pButtonsIds, "id");
                        if (CheckDelete == 1)
                        {
                            IEnumrableLstButtons = Models.SharingMethods.GetIEnumrable(LstButtons).ToList();
                            if (IEnumrableLstButtons != null)
                            {
                                gvButtons.DataSource = IEnumrableLstButtons.ToList();
                                MessageBox.Show(@"Button\s have been deleted successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        MessageBox.Show(@"No button\s have been deleted", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please select item to delete", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Add button to redirect to add/edit button form to add new button
        /// </summary>
        private void btnAddButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text != "")
                {
                    if (ddlActive.SelectedIndex != 0)
                    {
                        if (CurrentScreen == null)
                        {
                            AddEditButton addEditButton = null;
                            CurrentScreen = new Models.Screen();
                            CurrentScreen.Name = txtName.Text;
                            CurrentScreen.isActive = Convert.ToBoolean(ddlActive.SelectedItem);
                            CurrentScreen.BankId = BankId;
                            addEditButton = new AddEditButton(CurrentScreen);
                            this.Hide();
                            addEditButton.Show();
                        }
                        else
                        {
                            AddEditButton addEditButton = null;
                            CurrentScreen.Name = txtName.Text;
                            CurrentScreen.isActive = Convert.ToBoolean(ddlActive.SelectedItem);
                            addEditButton = new AddEditButton(CurrentScreen);
                            this.Hide();
                            addEditButton.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select active mode", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please fill screen Name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Edit button to redirect to add/edit button form to edit selected button
        /// </summary>
        private void btnEditButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvButtons.SelectedRows.Count > 0)
                {
                    if (gvButtons.SelectedRows.Count == 1)
                    {
                        int buttonIndex = (int)gvButtons.SelectedRows[0].Index;
                        Models.Button CurrentButton = LstButtons[buttonIndex];
                        CurrentButton.LstIndex = buttonIndex;
                        AddEditButton addEditButton = new AddEditButton(CurrentScreen, CurrentButton);
                        this.Hide();
                        addEditButton.Show();
                    }
                    else
                    {
                        MessageBox.Show("Please select one item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please select item to edit", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Function to fill textboxes with current screen data
        /// </summary>
        /// <param name="pScreenId"></param>
        private void FillScreens(Models.Screen pScreen)
        {
            try
            {
                CurrentScreen = pScreen;
                txtName.Text = CurrentScreen.Name;
                lblGVTitle.Text = CurrentScreen.Name + " - Buttons";
                lblTitle.Text = "Edit Screen";
                btnSave.Text = "Save";
                if (CurrentScreen.isActive.ToString() == "True")
                {
                    ddlActive.SelectedIndex = 1;
                }
                else
                {
                    ddlActive.SelectedIndex = 2;
                }
                ddlActive.SelectedText = CurrentScreen.isActive.ToString();
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Function to fill grid view with buttons for current screen
        /// </summary>
        private void FillButtons()
        {
            try
            {
                LstButtons = new List<Models.Button>();
                LstButtons = BusinessAccessLayer.Button.Button.SelectButtonsbyScreenId(CurrentScreen.id);
                if(LstButtons != null)
                {
                    IEnumrableLstButtons = Models.SharingMethods.GetIEnumrable(LstButtons).ToList();
                    gvButtons.DataSource = IEnumrableLstButtons.ToList();
                    SetdataGridViewDisplay();
                }
                else
                {
                    MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Function to fill tooltips
        /// </summary>
        private void LoadToolTips()
        {
            try
            {
                System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip();
                toolTip1.SetToolTip(btnAddButton, "Add new button");
                toolTip1.SetToolTip(btnEditButton, "Edit button");
                toolTip1.SetToolTip(btnDeleteButton, "Delete button");
                toolTip1.SetToolTip(btnSave, "Add Screen");
                toolTip1.SetToolTip(btnCancel, "Back");
                toolTip1.SetToolTip(txtName, "Screen name");
                toolTip1.SetToolTip(ddlActive, "Screen mode");
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        private void SetdataGridViewDisplay()
        {
            TSDApp.Models.SharingMethods.ChangeColumnWidth(gvButtons, 3);
            this.gvButtons.Columns[0].Visible = false;
            this.gvButtons.Columns[4].Visible = false;
            this.gvButtons.Columns[5].Visible = false;
            this.gvButtons.Columns[6].Visible = false;
            this.gvButtons.Columns[7].Visible = false;
            this.gvButtons.Columns[8].Visible = false;
            this.gvButtons.Columns[9].Visible = false;
            this.gvButtons.Columns[10].Visible = false;
            this.gvButtons.AllowUserToAddRows = false;
            this.gvButtons.AllowUserToResizeColumns = false;
            this.gvButtons.AllowUserToResizeRows = false;
        }
        #endregion
    }
}