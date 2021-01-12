using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TSDApp.Fomrs;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TSDApp
{
    public partial class TSD : Form
    {
        #region Variables
        private int CheckDataBase = 0;
        private Models.Bank CurrentBank;
        #endregion

        #region constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public TSD()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        #endregion

        #region Events
        private void TSD_Load(object sender, EventArgs e)
        {
            try
            {
                BusinessAccessLayer.ConnectionString.BALConnectionString bALConnectionString = new BusinessAccessLayer.ConnectionString.BALConnectionString();
                int ConnectionStringFileExist = bALConnectionString.SetConnectionString();
                if (ConnectionStringFileExist == 1)
                {
                    MainPanel.Visible = false;
                }
                else
                {
                    MessageBox.Show("ConnectionString file does not exist.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Dispose();
                    System.Environment.Exit(1);
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Enter button to check if bank is exsit or not and get screens if exist
        /// </summary>
        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBankName.Text != "")
                {
                    CurrentBank = new Models.Bank();
                    CurrentBank.name = txtBankName.Text;
                    BusinessAccessLayer.Bank.BALBank bank = new BusinessAccessLayer.Bank.BALBank();
                    CurrentBank = bank.CheckBankExist(CurrentBank);
                    if (CurrentBank != null)
                    {
                        if (CurrentBank.id == 0)
                        {
                            CurrentBank = new Models.Bank();
                            CurrentBank.name = txtBankName.Text;
                            CurrentBank = bank.InsertBank(CurrentBank);
                            if (CurrentBank == null)
                            {
                                MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        FillScreens();
                        BankNamePanel.Visible = false;
                        MainPanel.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Bank Name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Delete button :
        /// Delete selected row/s(screen/s)
        /// Delete buttons for the deleted screen
        /// </summary>
        private void btnDeleteScreen_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvScreens.SelectedRows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show(@"Are you sure you want to delete selected screen\s ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow screenRow in gvScreens.SelectedRows)
                        {
                            int pScreenId = (int)screenRow.Cells["id"].Value;
                            //Delete buttons for the deleted screen
                            BusinessAccessLayer.Button.BALButton button = new BusinessAccessLayer.Button.BALButton();
                            CheckDataBase = button.DeleteAllButtonByScreenId(pScreenId);
                            if (CheckDataBase == 1)
                            {
                                BusinessAccessLayer.Screen.BALScreen screen = new BusinessAccessLayer.Screen.BALScreen();
                                CheckDataBase = screen.DeleteScreenById(pScreenId);
                                if (CheckDataBase == 0)
                                {
                                    MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            //Delete screen whitch is selected
                        }
                        FillScreens();
                        MessageBox.Show(@"Screen\s have been deleted successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        MessageBox.Show(@"No screen\s have been deleted", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please select item to delete", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }

        /// <summary>
        /// Add screen button to redirect to add/edit screen form to add new screen
        /// </summary>
        private void btnAddScreen_Click(object sender, EventArgs e)
        {
            try
            {
                //Send current bank id to use it in add/edit screen form
                AddEditScreen addEditScreen = new AddEditScreen(CurrentBank.id);
                addEditScreen.FormClosed += new FormClosedEventHandler(AddEditScreen_Closed);
                addEditScreen.Show();
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        void AddEditScreen_Closed(object sender, FormClosedEventArgs e)
        {
            FillScreens();
        }

        /// <summary>
        /// Edit screen button to redirect to add/edit screen form to edit selected screen
        /// </summary>
        private void btnEditScreen_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvScreens.SelectedRows.Count > 0)
                {
                    if (gvScreens.SelectedRows.Count == 1)
                    {
                        Models.Screen pScreen = new Models.Screen();
                        pScreen.id = (int)gvScreens.SelectedRows[0].Cells[0].Value;
                        pScreen.name = (string)gvScreens.SelectedRows[0].Cells[1].Value;
                        pScreen.isActive = (bool)gvScreens.SelectedRows[0].Cells[2].Value;
                        pScreen.bankId = (int)gvScreens.SelectedRows[0].Cells[3].Value;
                        AddEditScreen addEditScreen = new AddEditScreen(CurrentBank.id, pScreen);
                        addEditScreen.FormClosed += new FormClosedEventHandler(AddEditScreen_Closed);
                        addEditScreen.Show();
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
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Cancel button to exit the Application
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Dispose();
                System.Environment.Exit(1);
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Override OnFormClosing method to dispose used form/s and exit the application
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                this.Dispose();
                System.Environment.Exit(1);
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Function to get screens for current bank, fill Title and rezise design
        /// </summary>
        private void FillScreens()
        {
            try
            {
                lblTitle.Text = "Main Form - " + CurrentBank.name;
                lblGVTitle.Text = "Bank " + CurrentBank.name + " screens";
                BusinessAccessLayer.Screen.BALScreen screen = new BusinessAccessLayer.Screen.BALScreen();
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                List<TSDApp.Models.Screen> screens = sharingMethods.GetIEnumrable(screen.SelectScreensByBankId(CurrentBank)).ToList();
                if (screens != null)
                {
                    gvScreens.DataSource = screens;
                    sharingMethods.ChangeColumnWidth(gvScreens, 2);
                    this.gvScreens.Columns[0].Visible = false;
                    this.gvScreens.Columns[3].Visible = false;
                    this.gvScreens.AllowUserToAddRows = false;
                    this.gvScreens.AllowUserToResizeColumns = false;
                    this.gvScreens.AllowUserToResizeRows = false;
                    this.gvScreens.ReadOnly = true;
                    LoadToolTips();
                }
                else
                {
                    MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
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
                toolTip1.SetToolTip(btnAddScreen, "Add new screen");
                toolTip1.SetToolTip(btnEditScreen, "Edit screen");
                toolTip1.SetToolTip(btnDeleteScreen, "Delete screen");
                toolTip1.SetToolTip(btnEnter, "Confirm");
                toolTip1.SetToolTip(btnCancel, "Close Application");
                toolTip1.SetToolTip(txtBankName, "Bank Name");
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        #endregion
    }
}