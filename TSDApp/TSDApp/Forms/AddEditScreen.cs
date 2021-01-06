using System;
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
        public AddEditScreen(int pBankId, int pScreenId)
        {
            try
            {
                InitializeComponent();
                BankId = pBankId;
                FillScreens(pScreenId);
                FillButtons();
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
                            CurrentScreen.Name = txtName.Text;
                            CurrentScreen.isActive = ddlActive.SelectedItem.ToString();
                            string pUpdateScreenquery = "update tblScreens set name = @Name,isActive = @isActive where id = @id";
                            CurrentScreen = BusinessAccessLayer.Screen.Screen.UpdateScreen(pUpdateScreenquery, CurrentScreen);
                            lblGVTitle.Text = CurrentScreen.Name;
                            MessageBox.Show("Screen has been updated successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        }
                        string pDeleteButtonByIdQuery = "delete from tblButtons where id = @id";
                        BusinessAccessLayer.Button.Button.DeleteButtonsByIds(pDeleteButtonByIdQuery, pButtonsIds);
                        FillButtons();
                        MessageBox.Show(@"Button\s have been deleted successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        AddEditButton addEditButton = null;
                        if (CurrentScreen == null || CurrentScreen.id == 0)
                        {
                            CurrentScreen = new Models.Screen();
                            CurrentScreen.Name = txtName.Text;
                            CurrentScreen.isActive = ddlActive.SelectedItem.ToString();
                            CurrentScreen.BankId = BankId;
                            addEditButton = new AddEditButton(CurrentScreen);
                        }
                        else
                        {
                            addEditButton = new AddEditButton(CurrentScreen.id);
                        }
                        this.Hide();
                        addEditButton.Show();
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
                        int buttonId = (int)gvButtons.SelectedRows[0].Cells[0].Value;
                        AddEditButton addEditButton = new AddEditButton(CurrentScreen.id, buttonId);
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
        private void FillScreens(int pScreenId)
        {
            try
            {
                string pSelectScreenQuery = "SELECT id,name,isActive,BankId FROM tblScreens where id = @id";
                CurrentScreen = BusinessAccessLayer.Screen.Screen.SelectScreenbyId(pSelectScreenQuery, pScreenId);
                txtName.Text = CurrentScreen.Name;
                lblGVTitle.Text = CurrentScreen.Name + " - Buttons";
                lblTitle.Text = "Edit Screen";
                btnSave.Text = "Edit";
                if (CurrentScreen.isActive.ToString() == "Activated")
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
                string pSelectButtonsQuery = "SELECT id, ENName, ARName, Type, MessageAR, MessageEN, issueType, ScreenId FROM tblButtons where ScreenId = @ScreenId";
                gvButtons.DataSource = BusinessAccessLayer.Button.Button.SelectButtonsbyScreenId(pSelectButtonsQuery, CurrentScreen.id);
                TSDApp.Models.SharingMethods.ChangeColumnWidth(gvButtons, 3);
                this.gvButtons.Columns[0].Visible = false;
                this.gvButtons.Columns[4].Visible = false;
                this.gvButtons.Columns[5].Visible = false;
                this.gvButtons.Columns[6].Visible = false;
                this.gvButtons.Columns[7].Visible = false;
                this.gvButtons.AllowUserToAddRows = false;
                this.gvButtons.AllowUserToResizeColumns = false;
                this.gvButtons.AllowUserToResizeRows = false;
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
        #endregion
    }
}