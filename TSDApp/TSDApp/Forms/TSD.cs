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
        private int checkDataBase = 0;
        private BusinessObjects.Models.Bank currentBank;
        Thread refreshThread;
        private bool deleteRow = false;
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
                sharingMethods.saveExceptionToLogFile(ex);
            }
        }
        #endregion

        #region Events
        private void TSD_Load(object sender, EventArgs e)
        {
            try
            {
                int connectionStringFileExist = BusinessCommon.ConnectionString.ConnectionString.checkConnectionStringStatus();
                if (connectionStringFileExist == 1)
                {
                    MainPanel.Visible = false;
                }
                else if (connectionStringFileExist == 2)
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
                sharingMethods.saveExceptionToLogFile(ex);
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
                    currentBank = new BusinessObjects.Models.Bank();
                    currentBank.name = txtBankName.Text;
                    BusinessAccessLayer.BALBank.BALBank bank = new BusinessAccessLayer.BALBank.BALBank();
                    currentBank = bank.checkBankExist(currentBank);
                    if (currentBank != null)
                    {
                        if (currentBank.id == 0)
                        {
                            currentBank = new BusinessObjects.Models.Bank();
                            currentBank.name = txtBankName.Text;
                            currentBank = bank.insertBank(currentBank);
                            if (currentBank == null)
                            {
                                MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.Dispose();
                                System.Environment.Exit(1);
                            }
                        }
                        BankNamePanel.Visible = false;
                        MainPanel.Visible = true;
                        refreshThread = new Thread(delegate () { fillScreensSave(); });
                        refreshThread.Start();
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
                sharingMethods.saveExceptionToLogFile(ex);
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
                    DialogResult deleteCheck = MessageBox.Show(@"Are you sure you want to delete selected screen\s ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (deleteCheck == DialogResult.Yes)
                    {
                        BusinessAccessLayer.BALButton.BALButton button = new BusinessAccessLayer.BALButton.BALButton();
                        BusinessAccessLayer.BALScreen.BALScreen screen = new BusinessAccessLayer.BALScreen.BALScreen();
                        foreach (DataGridViewRow screenRow in gvScreens.SelectedRows)
                        {
                            int pScreenId = (int)screenRow.Cells["id"].Value;
                            string pScreenName = screenRow.Cells["name"].Value.ToString();
                            DialogResult editCheck = DialogResult.Yes;
                            if (screen.checkIfScreenIsBusy(pScreenId))
                            {
                                editCheck = MessageBox.Show(@"Someone already is use (" + pScreenName + ") screen, Are you sure you want to delete it ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            }
                            if (deleteCheck == DialogResult.Yes && editCheck == DialogResult.Yes)
                            {
                                checkDataBase = button.deleteScreenAndButtonByScreenId(pScreenId);
                                if (checkDataBase == 0)
                                {
                                    MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                        deleteRow = true;
                        fillScreens();
                    }
                    else if (deleteCheck == DialogResult.No)
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
                sharingMethods.saveExceptionToLogFile(ex);
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
                AddEditScreen addEditScreen = new AddEditScreen(currentBank.id);
                addEditScreen.FormClosed += new FormClosedEventHandler(addEditScreen_Closed);
                addEditScreen.canelButtonEvent += canelButtonEventFunc;
                addEditScreen.Show();
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.saveExceptionToLogFile(ex);
            }
        }
        private void canelButtonEventFunc(object sender, int issueTicketButton)
        {
            try
            {
                fillScreens();
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.saveExceptionToLogFile(ex);
            }
        }
        void addEditScreen_Closed(object sender, FormClosedEventArgs e)
        {
            try
            {
                fillScreens();
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.saveExceptionToLogFile(ex);
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
                        BusinessObjects.Models.Screen pScreen = new BusinessObjects.Models.Screen();
                        pScreen.id = (int)gvScreens.SelectedRows[0].Cells[0].Value;
                        pScreen.name = (string)gvScreens.SelectedRows[0].Cells[1].Value;
                        pScreen.isActive = (bool)gvScreens.SelectedRows[0].Cells[2].Value;
                        pScreen.bankId = (int)gvScreens.SelectedRows[0].Cells[3].Value;
                        AddEditScreen addEditScreen = new AddEditScreen(currentBank.id, pScreen);
                        addEditScreen.FormClosed += new FormClosedEventHandler(addEditScreen_Closed);
                        addEditScreen.canelButtonEvent += canelButtonEventFunc;
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
                sharingMethods.saveExceptionToLogFile(ex);
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
                sharingMethods.saveExceptionToLogFile(ex);
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
                sharingMethods.saveExceptionToLogFile(ex);
            }
        }
        #endregion

        #region Methods
        private void fillScreensSave()
        {
            while (true)
            {
                if (InvokeRequired)
                {
                    Invoke((MethodInvoker)delegate
                {
                    fillScreens();
                });
                }
                Thread.Sleep(10000);
            }
        }
        /// <summary>
        /// Function to get screens for current bank, fill Title and rezise design
        /// </summary>
        private void fillScreens()
        {
            try
            {
                lblTitle.Text = "Main Form - " + currentBank.name;
                lblGVTitle.Text = "Bank " + currentBank.name + " screens";
                BusinessAccessLayer.BALScreen.BALScreen screen = new BusinessAccessLayer.BALScreen.BALScreen();
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                List<BusinessObjects.Models.Screen> screens = sharingMethods.GetIEnumrable(screen.selectScreensByBankId(currentBank)).ToList();
                if (screens != null)
                {
                    if (screens.Count() > 0 || deleteRow == true)
                    {
                        deleteRow = false;
                        gvScreens.DataSource = screens;
                        sharingMethods.ChangeColumnWidth(gvScreens, 2);
                        this.gvScreens.Columns[0].Visible = false;
                        this.gvScreens.Columns[3].Visible = false;
                        this.gvScreens.AllowUserToAddRows = false;
                        this.gvScreens.AllowUserToResizeColumns = false;
                        this.gvScreens.AllowUserToResizeRows = false;
                        this.gvScreens.ReadOnly = true;
                        loadToolTips();
                    }
                }
                else
                {
                    MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Dispose();
                    System.Environment.Exit(1);
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.saveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Function to fill tooltips
        /// </summary>
        private void loadToolTips()
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
                sharingMethods.saveExceptionToLogFile(ex);
            }
        }
        #endregion
    }
}