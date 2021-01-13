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
        public int BankId;
        private BusinessObjects.Models.Screen CurrentScreen;
        public List<BusinessObjects.Models.ShowMessageButton> LstShowMessageButtons;
        public List<BusinessObjects.Models.IssueTicketButton> LstIssueTicketButtons;
        public List<BusinessObjects.Models.Button> LstButtons;
        public IEnumerable<BusinessObjects.Models.Button> IEnumrableLstButtons;
        public event EventHandler<int> CanelButtonEvent;
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
                CurrentScreen = new BusinessObjects.Models.Screen();
                LstShowMessageButtons = new List<BusinessObjects.Models.ShowMessageButton>();
                LstIssueTicketButtons = new List<BusinessObjects.Models.IssueTicketButton>();
                LstButtons = new List<BusinessObjects.Models.Button>();
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
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
                CurrentScreen = new BusinessObjects.Models.Screen();
                CurrentScreen = pScreen;
                LstShowMessageButtons = new List<BusinessObjects.Models.ShowMessageButton>();
                LstIssueTicketButtons = new List<BusinessObjects.Models.IssueTicketButton>();
                LstButtons = new List<BusinessObjects.Models.Button>();
                FillScreens(CurrentScreen);
                FillButtons();
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
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
                IEnumrableLstButtons = new List<BusinessObjects.Models.Button>().ToList();
                LoadToolTips();
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
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
                            BusinessAccessLayer.BALScreen.BALScreen screen = new BusinessAccessLayer.BALScreen.BALScreen();
                            if (CurrentScreen.id == 0)
                            {
                                CurrentScreen.name = txtName.Text;
                                CurrentScreen.isActive = Convert.ToBoolean(ddlActive.SelectedItem);
                                CurrentScreen.bankId = BankId;
                                CurrentScreen = screen.InsertScreen(CurrentScreen);
                                if (CurrentScreen == null)
                                {
                                    MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                CurrentScreen.name = txtName.Text;
                                CurrentScreen.isActive = Convert.ToBoolean(ddlActive.SelectedItem);
                                CurrentScreen = screen.UpdateScreen(CurrentScreen);
                                if (CurrentScreen == null)
                                {
                                    MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                lblGVTitle.Text = CurrentScreen.name;
                            }
                            BusinessAccessLayer.BALButton.BALButton button = new BusinessAccessLayer.BALButton.BALButton();
                            foreach (BusinessObjects.Models.IssueTicketButton pbutton in LstIssueTicketButtons)
                            {
                                if (pbutton.id == 0)
                                {
                                    pbutton.screenId = CurrentScreen.id;
                                    BusinessObjects.Models.IssueTicketButton btnInsertCheck = button.InsertIssueTicketButton(pbutton);
                                    if (btnInsertCheck == null)
                                    {
                                        MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                else if (pbutton.updated == true)
                                {
                                    BusinessObjects.Models.IssueTicketButton btnUpdateCheck = button.UpdateIssueTicketButton(pbutton);
                                    if (btnUpdateCheck == null)
                                    {
                                        MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }
                            foreach (BusinessObjects.Models.ShowMessageButton pbutton in LstShowMessageButtons)
                            {
                                if (pbutton.id == 0)
                                {
                                    pbutton.screenId = CurrentScreen.id;
                                    BusinessObjects.Models.ShowMessageButton btnInsertCheck = button.InsertShowMessageButton(pbutton);
                                    if (btnInsertCheck == null)
                                    {
                                        MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                else if (pbutton.updated == true)
                                {
                                    BusinessObjects.Models.ShowMessageButton btnUpdateCheck = button.UpdateShowMessageButton(pbutton);
                                    if (btnUpdateCheck == null)
                                    {
                                        MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }
                            CloseForm();
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
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Function to delete current screen and cancel button to check if screen have buttons or not if not then delete screen
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                CloseForm();
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Override OnFormClosing method to dispose current form, check if screen have buttons or not if not then delete screen and move to the previous form
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                CloseForm();
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
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
                        IDictionary<int, string> pButtonsDetailsIds = new Dictionary<int, string>();
                        foreach (DataGridViewRow buttonRow in gvButtons.SelectedRows)
                        {
                            var btnId = (int)buttonRow.Cells["id"].Value;
                            var btnType = (string)buttonRow.Cells["Type"].Value;
                            if (btnId != 0)
                            {
                                pButtonsDetailsIds.Add(btnId, btnType);
                            }
                            if (btnType == BusinessObjects.Models.btnType.ShowMessage.ToString())
                            {
                                LstShowMessageButtons.Remove((LstShowMessageButtons.Where(x => x.id == btnId).FirstOrDefault()));
                            }
                            else
                            {
                                LstIssueTicketButtons.Remove((LstIssueTicketButtons.Where(x => x.id == btnId).FirstOrDefault()));
                            }
                        }
                        BusinessAccessLayer.BALButton.BALButton button = new BusinessAccessLayer.BALButton.BALButton();
                        int CheckDelete = button.DeleteButtonWhere(pButtonsDetailsIds, "id");
                        if (CheckDelete == 1)
                        {
                            RefreshGrid();
                            MessageBox.Show(@"Button\s have been deleted successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Add button to redirect to add/edit button form to add new button
        /// </summary>
        private void btnAddButton_Click(object sender, EventArgs e)
        {
            try
            {
                AddEditButton addEditButton = new AddEditButton();
                addEditButton.SaveShowMessageButton += AddShowMessageButton;
                addEditButton.SaveIssueTicketButton += AddIssueTicketButton;
                addEditButton.Show();
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
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
                        DataGridViewRow buttonRow = gvButtons.SelectedRows[0];
                        BusinessObjects.Models.Button btn = new BusinessObjects.Models.Button((int)buttonRow.Cells["id"].Value, (string)buttonRow.Cells["enName"].Value,
                            (string)buttonRow.Cells["arName"].Value, (int)buttonRow.Cells["screenId"].Value, (string)buttonRow.Cells["type"].Value);
                        if (btn.type == BusinessObjects.Models.btnType.ShowMessage.ToString())
                        {
                            BusinessObjects.Models.ShowMessageButton CurrentButton = LstShowMessageButtons.Where(x => x.id == btn.id
                            && x.enName == btn.enName && x.arName == btn.arName && x.screenId == btn.screenId && x.type == btn.type).FirstOrDefault();
                            CurrentButton.indexUpdated = LstShowMessageButtons.IndexOf(CurrentButton);
                            AddEditButton addEditButton = new AddEditButton(CurrentButton, null);
                            //addEditButton.FormClosed += new FormClosedEventHandler(FunctionClosed);
                            addEditButton.SaveShowMessageButton += AddShowMessageButton;
                            addEditButton.SaveIssueTicketButton += AddIssueTicketButton;
                            addEditButton.Show();
                        }
                        else
                        {
                            BusinessObjects.Models.IssueTicketButton CurrentButton = LstIssueTicketButtons.Where(x => x.id == btn.id
                            && x.enName == btn.enName && x.arName == btn.arName && x.screenId == btn.screenId && x.type == btn.type).FirstOrDefault();
                            CurrentButton.indexUpdated = LstIssueTicketButtons.IndexOf(CurrentButton);
                            AddEditButton addEditButton = new AddEditButton(null, CurrentButton);
                            //addEditButton.FormClosed += new FormClosedEventHandler(FunctionClosed);
                            addEditButton.SaveShowMessageButton += AddShowMessageButton;
                            addEditButton.SaveIssueTicketButton += AddIssueTicketButton;
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
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        #endregion

        #region Methods
        private void OnCancelButton(int issueTicketButton)
        {
            var handler = CanelButtonEvent;
            if (CanelButtonEvent != null)
            {
                CanelButtonEvent.Invoke(this, issueTicketButton);
            }
        }
        private void AddShowMessageButton(object sender, BusinessObjects.Models.ShowMessageButton showMessageButton)
        {
            try
            {
                if (showMessageButton.indexUpdated == -1)
                {
                    LstShowMessageButtons.Add(showMessageButton);
                }
                else
                {
                    LstShowMessageButtons[LstShowMessageButtons.FindIndex(x => x.indexUpdated == showMessageButton.indexUpdated)] = showMessageButton;
                }
                RefreshGrid();
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        private void AddIssueTicketButton(object sender, BusinessObjects.Models.IssueTicketButton issueTicketButton)
        {
            try
            {
                if (issueTicketButton.indexUpdated == -1)
                {
                    LstIssueTicketButtons.Add(issueTicketButton);
                }
                else
                {
                    LstIssueTicketButtons[LstIssueTicketButtons.FindIndex(x => x.indexUpdated == issueTicketButton.indexUpdated)] = issueTicketButton;
                }
                RefreshGrid();
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        private void CloseForm()
        {
            try
            {
                CurrentScreen = null;
                LstShowMessageButtons = null;
                LstIssueTicketButtons = null;
                LstButtons = null;
                IEnumrableLstButtons = null;
                this.Dispose();
                OnCancelButton(1);
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Function to fill textboxes with current screen data
        /// </summary>
        /// <param name="pScreenId"></param>
        private void FillScreens(BusinessObjects.Models.Screen pScreen)
        {
            try
            {
                CurrentScreen = pScreen;
                txtName.Text = CurrentScreen.name;
                lblGVTitle.Text = CurrentScreen.name + " - Buttons";
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
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Function to fill grid view with buttons for current screen
        /// </summary>
        private void FillButtons()
        {
            try
            {
                LstButtons = new List<BusinessObjects.Models.Button>();
                if ((LstShowMessageButtons == null || LstIssueTicketButtons == null) || (LstShowMessageButtons.Count == 0 && LstIssueTicketButtons.Count == 0))
                {
                    BusinessAccessLayer.BALButton.BALButton button = new BusinessAccessLayer.BALButton.BALButton();
                    LstShowMessageButtons = button.SelectButtonsbyScreenId<BusinessObjects.Models.ShowMessageButton>(CurrentScreen.id, BusinessObjects.Models.btnType.ShowMessage);
                    LstIssueTicketButtons = button.SelectButtonsbyScreenId<BusinessObjects.Models.IssueTicketButton>(CurrentScreen.id, BusinessObjects.Models.btnType.IssueTicket);
                }
                RefreshGrid();
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        private void RefreshGrid()
        {
            LstButtons.Clear();
            foreach (var item in LstShowMessageButtons)
            {
                LstButtons.Add(new BusinessObjects.Models.Button(item.id, item.enName, item.arName, item.screenId, item.type));
            }
            foreach (var item in LstIssueTicketButtons)
            {
                LstButtons.Add(new BusinessObjects.Models.Button(item.id, item.enName, item.arName, item.screenId, item.type));
            }
            if (LstButtons != null)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                IEnumrableLstButtons = sharingMethods.GetIEnumrable(LstButtons).ToList();
                gvButtons.DataSource = IEnumrableLstButtons.ToList();
                SetdataGridViewDisplay();
            }
            else
            {
                MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        private void SetdataGridViewDisplay()
        {
            Models.SharingMethods sharingMethods = new Models.SharingMethods();
            sharingMethods.ChangeColumnWidth(gvButtons, 3);
            this.gvButtons.Columns[0].Visible = false;
            this.gvButtons.Columns[3].Visible = false;
            this.gvButtons.Columns[5].Visible = false;
            this.gvButtons.Columns[6].Visible = false;
            this.gvButtons.AllowUserToAddRows = false;
            this.gvButtons.AllowUserToResizeColumns = false;
            this.gvButtons.AllowUserToResizeRows = false;
        }
        #endregion
    }
}