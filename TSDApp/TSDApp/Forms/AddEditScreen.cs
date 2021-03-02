using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TSDApp.Forms;
using TSDApp.Models;

namespace TSDApp.Fomrs
{
    public partial class AddEditScreen : Form
    {
        #region Variables
        public int BankId;
        private BusinessObjects.Models.Screen currentScreen;
        public List<BusinessObjects.Models.ShowMessageButton> lstShowMessageButtons;
        public List<BusinessObjects.Models.IssueTicketButton> lstIssueTicketButtons;
        public List<BusinessObjects.Models.Button> lstButtons;
        public event EventHandler<int> canelButtonEvent;
        Thread refreshThread;
        private bool userNotEdit = false;
        #endregion

        #region constructors
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
                BankId = pBankId;
                ddlActive.SelectedIndex = 0;
                currentScreen = new BusinessObjects.Models.Screen();
                lstShowMessageButtons = new List<BusinessObjects.Models.ShowMessageButton>();
                lstIssueTicketButtons = new List<BusinessObjects.Models.IssueTicketButton>();
                lstButtons = new List<BusinessObjects.Models.Button>();
            }
            catch (Exception ex)
            {
                
                SharingMethods.saveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Parametarize constructor (if edit screen)
        /// </summary>
        /// <param name="pBankId"></param>
        /// <param name="pScreenId"></param>
        public AddEditScreen(int pBankId, BusinessObjects.Models.Screen pScreen)
        {
            try
            {
                InitializeComponent();
                ddlActive.DropDownStyle = ComboBoxStyle.DropDownList;
                BankId = pBankId;
                currentScreen = new BusinessObjects.Models.Screen();
                currentScreen = pScreen;
                lstShowMessageButtons = new List<BusinessObjects.Models.ShowMessageButton>();
                lstIssueTicketButtons = new List<BusinessObjects.Models.IssueTicketButton>();
                lstButtons = new List<BusinessObjects.Models.Button>();
                fillScreens(currentScreen);
                fillButtons();
                userNotEdit = true;
                refreshThread = new Thread(delegate () { fillButtonsSave(); });
                refreshThread.Start();
            }
            catch (Exception ex)
            {
                
                SharingMethods.saveExceptionToLogFile(ex);
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
                loadToolTips();
            }
            catch (Exception ex)
            {
                
                SharingMethods.saveExceptionToLogFile(ex);
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
                        BusinessAccessLayer.BALScreen.BALScreen screen = new BusinessAccessLayer.BALScreen.BALScreen();
                        if (currentScreen.id != 0 && (screen.checkIfScreenIsDeleted(currentScreen.id)))
                        {
                            MessageBox.Show("Screen cant be save to database because someone already delete it", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            closeForm();
                        }
                        else
                        {
                            if (gvButtons.RowCount > 0)
                            {
                                if (lstIssueTicketButtons.Where(x => x.DeletedFromAnotherUsers).Count() > 0 || lstShowMessageButtons.Where(x => x.DeletedFromAnotherUsers).Count() > 0)
                                {
                                    MessageBox.Show("Screen cant be save to database because someone already modified it", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    fillButtons();
                                    userNotEdit = true;
                                }
                                else
                                {
                                    userNotEdit = false;
                                    if (currentScreen.id == 0)
                                    {
                                        currentScreen.name = txtName.Text;
                                        currentScreen.isActive = Convert.ToBoolean(ddlActive.SelectedItem);
                                        currentScreen.bankId = BankId;
                                        currentScreen = screen.insertScreenAndEditButtons(currentScreen, lstShowMessageButtons, lstIssueTicketButtons);
                                        if (currentScreen == null)
                                        {
                                            MessageBox.Show("Screen and button not save to database, please check your connection", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                        closeForm();
                                    }
                                    else
                                    {
                                        currentScreen.name = txtName.Text;
                                        currentScreen.isActive = Convert.ToBoolean(ddlActive.SelectedItem);
                                        currentScreen = screen.updateScreenAndEditButtons(currentScreen, lstShowMessageButtons, lstIssueTicketButtons);
                                        if (currentScreen == null)
                                        {
                                            MessageBox.Show("Screen and button not save to database, please check your connection", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                        lblGVTitle.Text = currentScreen.name;
                                        closeForm();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Screen cannot be saved because there is no buttons added to this screen", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
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
                
                SharingMethods.saveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Function to delete current screen and cancel button to check if screen have buttons or not if not then delete screen
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                closeForm();
            }
            catch (Exception ex)
            {
                
                SharingMethods.saveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Override OnFormClosing method to dispose current form, check if screen have buttons or not if not then delete screen and move to the previous form
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                closeForm();
            }
            catch (Exception ex)
            {
                
                SharingMethods.saveExceptionToLogFile(ex);
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
                    userNotEdit = false;
                    DialogResult dialogResult = MessageBox.Show(@"Are you sure you want to delete selected button\s ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow buttonRow in gvButtons.SelectedRows)
                        {
                            var btnId = (int)buttonRow.Cells["id"].Value;
                            var btnType = (string)buttonRow.Cells["type"].Value;
                            if (btnType == BusinessObjects.Models.btnType.ShowMessage.ToString())
                            {
                                if (btnId == 0)
                                {
                                    BusinessObjects.Models.Button btn = lstButtons[buttonRow.Index];
                                    lstShowMessageButtons.Remove(lstShowMessageButtons.Where(x => x.id == btn.id &&
                                    x.enName == btn.enName && x.arName == btn.arName).FirstOrDefault());
                                }
                                else
                                {
                                    lstShowMessageButtons.Where(x => x.id == btnId).FirstOrDefault().isDeleted = true;
                                }
                            }
                            else
                            {
                                if (btnId == 0)
                                {
                                    BusinessObjects.Models.Button btn = lstButtons[buttonRow.Index];
                                    lstIssueTicketButtons.Remove(lstIssueTicketButtons.Where(x => x.id == btn.id &&
                                    x.enName == btn.enName && x.arName == btn.arName).FirstOrDefault());
                                }
                                else
                                {
                                    lstIssueTicketButtons.Where(x => x.id == btnId).FirstOrDefault().isDeleted = true;
                                }
                            }
                        }
                        refreshGrid();
                    }
                }
                else
                {
                    MessageBox.Show("Please select item to delete", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                
                SharingMethods.saveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Add button to redirect to add/edit button form to add new button
        /// </summary>
        private void btnAddButton_Click(object sender, EventArgs e)
        {
            try
            {
                userNotEdit = false;
                AddEditButton addEditButton = new AddEditButton(BankId);
                addEditButton.saveShowMessageButton += addShowMessageButton;
                addEditButton.saveIssueTicketButton += addIssueTicketButton;
                addEditButton.canelButtonEvent += canelButtonEventFunc;
                addEditButton.Show();
                formDisabledAndEnabled(true);
            }
            catch (Exception ex)
            {
                
                SharingMethods.saveExceptionToLogFile(ex);
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
                        formDisabledAndEnabled(true);
                        userNotEdit = false;
                        DataGridViewRow buttonRow = gvButtons.SelectedRows[0];
                        BusinessObjects.Models.Button btn = new BusinessObjects.Models.Button((int)buttonRow.Cells["id"].Value, (string)buttonRow.Cells["enName"].Value,
                            (string)buttonRow.Cells["arName"].Value, (int)buttonRow.Cells["screenId"].Value, (string)buttonRow.Cells["type"].Value);
                        if (btn.type == BusinessObjects.Models.btnType.ShowMessage.ToString())
                        {
                            BusinessObjects.Models.ShowMessageButton currentButton = lstShowMessageButtons.Where(x => x.id == btn.id
                            && x.enName == btn.enName && x.arName == btn.arName && x.screenId == btn.screenId && x.type == btn.type).FirstOrDefault();
                            currentButton.indexUpdated = lstShowMessageButtons.IndexOf(currentButton);
                            AddEditButton addEditButton = new AddEditButton(currentButton, null);
                            //addEditButton.FormClosed += new FormClosedEventHandler(FunctionClosed);
                            addEditButton.saveShowMessageButton += addShowMessageButton;
                            addEditButton.saveIssueTicketButton += addIssueTicketButton;
                            addEditButton.canelButtonEvent += canelButtonEventFunc;
                            addEditButton.Show();
                        }
                        else
                        {
                            BusinessObjects.Models.IssueTicketButton currentButton = lstIssueTicketButtons.Where(x => x.id == btn.id
                            && x.enName == btn.enName && x.arName == btn.arName && x.screenId == btn.screenId && x.type == btn.type).FirstOrDefault();
                            currentButton.indexUpdated = lstIssueTicketButtons.IndexOf(currentButton);
                            AddEditButton addEditButton = new AddEditButton(null, currentButton);
                            //addEditButton.FormClosed += new FormClosedEventHandler(FunctionClosed);
                            addEditButton.saveShowMessageButton += addShowMessageButton;
                            addEditButton.saveIssueTicketButton += addIssueTicketButton;
                            addEditButton.canelButtonEvent += canelButtonEventFunc;
                            addEditButton.Show();
                        }
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
                
                SharingMethods.saveExceptionToLogFile(ex);
            }
        }
        private void canelButtonEventFunc(object sender, int issueTicketButton)
        {
            try
            {
                formDisabledAndEnabled(false);
            }
            catch (Exception ex)
            {
                
                SharingMethods.saveExceptionToLogFile(ex);
            }
        }
        #endregion

        #region Methods
        private void onCancelButton(int issueTicketButton)
        {
            try
            {
                userNotEdit = false;
                var handler = canelButtonEvent;
                if (canelButtonEvent != null)
                {
                    canelButtonEvent.Invoke(this, issueTicketButton);
                }
            }
            catch (Exception ex)
            {
                
                SharingMethods.saveExceptionToLogFile(ex);
            }
        }
        private void addShowMessageButton(object sender, BusinessObjects.Models.ShowMessageButton showMessageButton)
        {
            try
            {
                BusinessAccessLayer.BALScreen.BALScreen bALScreen = new BusinessAccessLayer.BALScreen.BALScreen();
                if (showMessageButton.indexUpdated == -1)
                {
                    lstShowMessageButtons.Add(showMessageButton);
                }
                else
                {
                    lstShowMessageButtons[lstShowMessageButtons.FindIndex(x => x.indexUpdated == showMessageButton.indexUpdated)] = showMessageButton;
                }
                refreshGrid();
                formDisabledAndEnabled(false);
            }
            catch (Exception ex)
            {
                
                SharingMethods.saveExceptionToLogFile(ex);
            }
        }
        private void addIssueTicketButton(object sender, BusinessObjects.Models.IssueTicketButton issueTicketButton)
        {
            try
            {
                if (issueTicketButton.indexUpdated == -1)
                {
                    lstIssueTicketButtons.Add(issueTicketButton);
                }
                else
                {
                    lstIssueTicketButtons[lstIssueTicketButtons.FindIndex(x => x.indexUpdated == issueTicketButton.indexUpdated)] = issueTicketButton;
                }
                refreshGrid();
                formDisabledAndEnabled(false);
            }
            catch (Exception ex)
            {
                
                SharingMethods.saveExceptionToLogFile(ex);
            }
        }
        private void closeForm()
        {
            try
            {
                userNotEdit = false;
                currentScreen = null;
                lstShowMessageButtons = null;
                lstIssueTicketButtons = null;
                lstButtons = null;
                this.Dispose();
                onCancelButton(1);
            }
            catch (Exception ex)
            {
                
                SharingMethods.saveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Function to fill textboxes with current screen data
        /// </summary>
        /// <param name="pScreenId"></param>
        private void fillScreens(BusinessObjects.Models.Screen pScreen)
        {
            try
            {
                currentScreen = pScreen;
                txtName.Text = currentScreen.name;
                lblGVTitle.Text = currentScreen.name + " - Buttons";
                lblTitle.Text = "Edit Screen";
                btnSave.Text = "Save";
                if (currentScreen.isActive.ToString() == "True")
                {
                    ddlActive.SelectedIndex = 1;
                }
                else
                {
                    ddlActive.SelectedIndex = 2;
                }
                ddlActive.SelectedText = currentScreen.isActive.ToString();
            }
            catch (Exception ex)
            {
                
                SharingMethods.saveExceptionToLogFile(ex);
            }
        }
        private void fillButtonsSave()
        {
            try
            {
                while (userNotEdit)
                {
                    if (InvokeRequired)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            fillButtons();
                        });
                    }
                    Thread.Sleep(10000);
                }
            }
            catch (Exception ex)
            {
                
                SharingMethods.saveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Function to fill grid view with buttons for current screen
        /// </summary>
        private void fillButtons()
        {
            try
            {
                lstButtons = new List<BusinessObjects.Models.Button>();
                if (currentScreen.id != 0)
                {
                    BusinessAccessLayer.BALButton.BALButton button = new BusinessAccessLayer.BALButton.BALButton();
                    lstShowMessageButtons = button.selectButtonsbyScreenId<BusinessObjects.Models.ShowMessageButton>(currentScreen.id, BusinessObjects.Models.btnType.ShowMessage);
                    lstIssueTicketButtons = button.selectButtonsbyScreenId<BusinessObjects.Models.IssueTicketButton>(currentScreen.id, BusinessObjects.Models.btnType.IssueTicket);
                    refreshGrid();
                }
            }
            catch (Exception ex)
            {
                
                SharingMethods.saveExceptionToLogFile(ex);
            }
        }
        private void refreshGrid()
        {
            try
            {
                lstButtons.Clear();
                foreach (var item in lstShowMessageButtons)
                {
                    if (!item.isDeleted)
                    {
                        lstButtons.Add(new BusinessObjects.Models.Button(item.id, item.enName, item.arName, item.screenId, item.type));
                    }
                }
                foreach (var item in lstIssueTicketButtons)
                {
                    if (!item.isDeleted)
                    {
                        lstButtons.Add(new BusinessObjects.Models.Button(item.id, item.enName, item.arName, item.screenId, item.type));
                    }
                }
                if (lstButtons != null)
                {
                    gvButtons.DataSource = new List<BusinessObjects.Models.Button>(lstButtons);
                    setdataGridViewDisplay();
                }
                else
                {
                    MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                
                SharingMethods.saveExceptionToLogFile(ex);
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
                
                SharingMethods.saveExceptionToLogFile(ex);
            }
        }
        private void setdataGridViewDisplay()
        {
            try
            {
                SharingMethods.ChangeColumnWidth(gvButtons, 3);
                this.gvButtons.Columns[0].Visible = false;
                this.gvButtons.Columns[3].Visible = false;
                this.gvButtons.Columns[5].Visible = false;
                this.gvButtons.Columns[6].Visible = false;
                this.gvButtons.Columns[7].Visible = false;
                this.gvButtons.Columns[8].Visible = false;
                this.gvButtons.AllowUserToAddRows = false;
                this.gvButtons.AllowUserToResizeColumns = false;
                this.gvButtons.AllowUserToResizeRows = false;
                if (lstButtons.Count() > 0)
                {
                    gvButtons.Rows[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                
                SharingMethods.saveExceptionToLogFile(ex);
            }
        }
        private void formDisabledAndEnabled(bool isEnabled)
        {
            try
            {
                if (isEnabled)
                {
                    btnAddButton.Enabled = false;
                    btnEditButton.Enabled = false;
                    btnDeleteButton.Enabled = false;
                }
                else
                {
                    btnAddButton.Enabled = true;
                    btnEditButton.Enabled = true;
                    btnDeleteButton.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                
                SharingMethods.saveExceptionToLogFile(ex);
            }
        }
        #endregion

    }
}