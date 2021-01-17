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
using System.Threading;
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
        private BusinessObjects.Models.Bank CurrentBank;
        Thread RefreshThread;
        private bool userNotEdit = true;
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
                int ConnectionStringFileExist = BusinessCommon.ConnectionString.ConnectionString.CheckConnectionStringStatus();
                if (ConnectionStringFileExist == 1)
                {
                    MainPanel.Visible = false;
                }
                else if (ConnectionStringFileExist == 2)
                {
                    MessageBox.Show("ConnectionString file isEmpty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Dispose();
                    System.Environment.Exit(1);
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
                    CurrentBank = new BusinessObjects.Models.Bank();
                    CurrentBank.name = txtBankName.Text;
                    BusinessAccessLayer.BALBank.BALBank bank = new BusinessAccessLayer.BALBank.BALBank();
                    CurrentBank = bank.CheckBankExist(CurrentBank);
                    if (CurrentBank != null)
                    {
                        if (CurrentBank.id == 0)
                        {
                            CurrentBank = new BusinessObjects.Models.Bank();
                            CurrentBank.name = txtBankName.Text;
                            CurrentBank = bank.InsertBank(CurrentBank);
                            if (CurrentBank == null)
                            {
                                MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.Dispose();
                                System.Environment.Exit(1);
                            }
                        }
                        BankNamePanel.Visible = false;
                        MainPanel.Visible = true;
                        RefreshThread = new Thread(delegate () { FillScreens(); });
                        RefreshThread.Start();
                    }
                    else
                    {
                        MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Dispose();
                        System.Environment.Exit(1);
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
                    DialogResult DeleteCheck = MessageBox.Show(@"Are you sure you want to delete selected screen\s ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (DeleteCheck == DialogResult.Yes)
                    {
                        BusinessAccessLayer.BALButton.BALButton button = new BusinessAccessLayer.BALButton.BALButton();
                        BusinessAccessLayer.BALScreen.BALScreen screen = new BusinessAccessLayer.BALScreen.BALScreen();
                        foreach (DataGridViewRow screenRow in gvScreens.SelectedRows)
                        {
                            int pScreenId = (int)screenRow.Cells["id"].Value;
                            string pScreenName = screenRow.Cells["name"].Value.ToString();
                            DialogResult EditCheck = DialogResult.Yes;
                            if (screen.CheckIfScreenIsBusy(pScreenId))
                            {
                                EditCheck = MessageBox.Show(@"Someone already is use (" + pScreenName + ") screen, Are you sure you want to delete it ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            }
                            if (DeleteCheck == DialogResult.Yes && EditCheck == DialogResult.Yes)
                            {
                                CheckDataBase = button.DeleteScreenAndButtonByScreenId(pScreenId);
                                if (CheckDataBase == 0)
                                {
                                    MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                }
                            }
                        }
                        FillScreens();
                    }
                    else if (DeleteCheck == DialogResult.No)
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
                userNotEdit = false;
                AddEditScreen addEditScreen = new AddEditScreen(CurrentBank.id);
                addEditScreen.FormClosed += new FormClosedEventHandler(AddEditScreen_Closed);
                addEditScreen.CanelButtonEvent += CanelButtonEventFunc;
                addEditScreen.Show();
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        private void CanelButtonEventFunc(object sender, int issueTicketButton)
        {
            try
            {
                userNotEdit = true;
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        void AddEditScreen_Closed(object sender, FormClosedEventArgs e)
        {
            try
            {
                userNotEdit = true;
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
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
                        userNotEdit = false;
                        BusinessObjects.Models.Screen pScreen = new BusinessObjects.Models.Screen();
                        pScreen.id = (int)gvScreens.SelectedRows[0].Cells[0].Value;
                        pScreen.name = (string)gvScreens.SelectedRows[0].Cells[1].Value;
                        pScreen.isActive = (bool)gvScreens.SelectedRows[0].Cells[2].Value;
                        pScreen.bankId = (int)gvScreens.SelectedRows[0].Cells[3].Value;
                        AddEditScreen addEditScreen = new AddEditScreen(CurrentBank.id, pScreen);
                        addEditScreen.FormClosed += new FormClosedEventHandler(AddEditScreen_Closed);
                        addEditScreen.CanelButtonEvent += CanelButtonEventFunc;
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
                while (userNotEdit)
                {
                    if (InvokeRequired)
                    {
                        Invoke((MethodInvoker)delegate
                    {
                        lblTitle.Text = "Main Form - " + CurrentBank.name;
                        lblGVTitle.Text = "Bank " + CurrentBank.name + " screens";
                        BusinessAccessLayer.BALScreen.BALScreen screen = new BusinessAccessLayer.BALScreen.BALScreen();
                        Models.SharingMethods sharingMethods = new Models.SharingMethods();
                        List<BusinessObjects.Models.Screen> screens = sharingMethods.GetIEnumrable(screen.SelectScreensByBankId(CurrentBank)).ToList();
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
                            this.Dispose();
                            System.Environment.Exit(1);
                        }
                    });
                        Thread.Sleep(10000);
                    }
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