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
        private bool isVisited = false;
        public static Models.Bank CurrentBank;
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
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Parametarize constructor
        /// </summary>
        /// <param name="pIsVisited">Fill isVisited to know if the user already choose a bank</param>
        public TSD(bool pIsVisited)
        {
            try
            {
                InitializeComponent();
                isVisited = pIsVisited;
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Load function to fill data and show panels
        /// </summary>
        #endregion

        #region Events
        private void TSD_Load(object sender, EventArgs e)
        {
            try
            {
                if (isVisited)
                {
                    BankNamePanel.Visible = false;
                    MainPanel.Visible = true;
                    FillScreens();
                }
                else
                {
                    Models.SharingMethods.SetConnectionString();
                    MainPanel.Visible = false;
                }
                LoadToolTips();
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Cancel button to exit the Application
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
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
                    CurrentBank.Name = txtBankName.Text;
                    string pSelectBankquery = "SELECT id,Name FROM tblBanks WHERE Name = @Name";
                    if (BusinessAccessLayer.ConnectionString.ConnectionString.IsServerConnected())
                    {
                        CurrentBank = BusinessAccessLayer.Bank.Bank.CheckBankExist(pSelectBankquery, CurrentBank);
                        if (CurrentBank == null)
                        {
                            CurrentBank = new Models.Bank();
                            CurrentBank.Name = txtBankName.Text;
                            string pInsertBankquery = "insert into tblBanks OUTPUT INSERTED.IDENTITYCOL  values (@Name)";
                            CurrentBank = BusinessAccessLayer.Bank.Bank.InsertBank(pInsertBankquery, CurrentBank);
                        }
                        FillScreens();
                        BankNamePanel.Visible = false;
                        MainPanel.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("Please check your connection to database", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Bank Name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
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
                if (BusinessAccessLayer.ConnectionString.ConnectionString.IsServerConnected())
                {
                    if (gvScreens.SelectedRows.Count > 0)
                    {
                        DialogResult dialogResult = MessageBox.Show(@"Are you sure you want to delete selected screen\s ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            foreach (DataGridViewRow screenRow in gvScreens.SelectedRows)
                            {
                                int id = (int)screenRow.Cells["id"].Value;
                                //Delete buttons for the deleted screen
                                string pDeleteButtonQuery = "delete from tblButtons where ScreenId = @ScreenId";
                                BusinessAccessLayer.Button.Button.DeleteButtonsByScreenId(pDeleteButtonQuery, id);
                                //Delete screen whitch is selected
                                string pDeleteScreenQuery = "delete from tblScreens where id = @id";
                                BusinessAccessLayer.Screen.Screen.DeleteScreenById(pDeleteScreenQuery, id);
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
                else
                {
                    MessageBox.Show("Please check your connection to database", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
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
                this.Hide();
                addEditScreen.Show();
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
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
                        pScreen.Name = (string)gvScreens.SelectedRows[0].Cells[1].Value;
                        pScreen.isActive = (bool)gvScreens.SelectedRows[0].Cells[2].Value == true ? "True" : "False";
                        pScreen.BankId = (int)gvScreens.SelectedRows[0].Cells[3].Value;
                        AddEditScreen addEditScreen = new AddEditScreen(CurrentBank.id, pScreen);
                        this.Hide();
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
                Models.SharingMethods.SaveExceptionToLogFile(ex);
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
                Application.Exit();
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
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
                if (BusinessAccessLayer.ConnectionString.ConnectionString.IsServerConnected())
                {
                    lblTitle.Text = "Main Form - " + CurrentBank.Name;
                    lblGVTitle.Text = "Bank " + CurrentBank.Name + " screens";
                    string pSelectScreens = "SELECT id,name,isActive,BankId FROM tblScreens where BankId = @BankId";
                    gvScreens.DataSource = BusinessAccessLayer.Screen.Screen.SelectScreensByBankId(pSelectScreens, CurrentBank);
                    TSDApp.Models.SharingMethods.ChangeColumnWidth(gvScreens, 2);
                    this.gvScreens.Columns[0].Visible = false;
                    this.gvScreens.Columns[3].Visible = false;
                    this.gvScreens.AllowUserToAddRows = false;
                    this.gvScreens.AllowUserToResizeColumns = false;
                    this.gvScreens.AllowUserToResizeRows = false;
                    this.gvScreens.ReadOnly = true;
                }
                else
                {
                    MessageBox.Show("Please check your connection to database", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                toolTip1.SetToolTip(btnAddScreen, "Add new screen");
                toolTip1.SetToolTip(btnEditScreen, "Edit screen");
                toolTip1.SetToolTip(btnDeleteScreen, "Delete screen");
                toolTip1.SetToolTip(btnEnter, "Confirm");
                toolTip1.SetToolTip(btnCancel, "Close Application");
                toolTip1.SetToolTip(txtBankName, "Bank Name");
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        #endregion
    }
}