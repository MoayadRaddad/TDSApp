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
        public static List<BusinessObjects.Models.ShowMessage> LstShowMessageButtons = new List<BusinessObjects.Models.ShowMessage>();
        public static List<BusinessObjects.Models.IssueTicket> LstIssueTicketButtons = new List<BusinessObjects.Models.IssueTicket>();
        public static List<BusinessObjects.Models.ButtonMaster> LstButtons;
        public static IEnumerable<BusinessObjects.Models.ButtonMaster> IEnumrableLstButtons;
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
                LstShowMessageButtons = new List<BusinessObjects.Models.ShowMessage>();
                LstIssueTicketButtons = new List<BusinessObjects.Models.IssueTicket>();
                LstButtons = new List<BusinessObjects.Models.ButtonMaster>();
                IEnumrableLstButtons = new List<BusinessObjects.Models.ButtonMaster>().ToList();
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
        public AddEditScreen(int pBankId, Models.Screen pScreen, BusinessObjects.Models.ShowMessage pNewButton, BusinessObjects.Models.ShowMessage pOldButton)
        {
            try
            {
                InitializeComponent();
                ddlActive.DropDownStyle = ComboBoxStyle.DropDownList;
                BankId = pBankId;
                FillScreens(pScreen);
                if (pOldButton == null && pNewButton != null)
                {
                    LstShowMessageButtons.Add(pNewButton);
                }
                else if (pNewButton != null)
                {
                    LstShowMessageButtons[LstShowMessageButtons.FindIndex(x => x.indexUpdated == pOldButton.indexUpdated)] = pNewButton;
                }
                RefreshGrid();
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        public AddEditScreen(int pBankId, Models.Screen pScreen, BusinessObjects.Models.IssueTicket pNewButton, BusinessObjects.Models.IssueTicket pOldButton)
        {
            try
            {
                InitializeComponent();
                ddlActive.DropDownStyle = ComboBoxStyle.DropDownList;
                BankId = pBankId;
                FillScreens(pScreen);
                if (pOldButton == null && pNewButton != null)
                {
                    LstIssueTicketButtons.Add(pNewButton);
                }
                else if (pNewButton != null)
                {
                    LstIssueTicketButtons[LstIssueTicketButtons.FindIndex(x => x.indexUpdated == pOldButton.indexUpdated)] = pNewButton;
                }
                RefreshGrid();
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
                            foreach (BusinessObjects.Models.IssueTicket pbutton in LstIssueTicketButtons)
                            {
                                if (pbutton.id == 0)
                                {
                                    pbutton.ScreenId = CurrentScreen.id;
                                    BusinessObjects.Models.IssueTicket btnInsertCheck = BusinessAccessLayer.Button.Button.InsertIssueTicketButton(pbutton);
                                    if (btnInsertCheck == null)
                                    {
                                        MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                else if (pbutton.Updated == true)
                                {
                                    BusinessObjects.Models.IssueTicket btnUpdateCheck = BusinessAccessLayer.Button.Button.UpdateIssueTicketButton(pbutton);
                                    if (btnUpdateCheck == null)
                                    {
                                        MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }
                            foreach (BusinessObjects.Models.ShowMessage pbutton in LstShowMessageButtons)
                            {
                                if (pbutton.id == 0)
                                {
                                    pbutton.ScreenId = CurrentScreen.id;
                                    BusinessObjects.Models.ShowMessage btnInsertCheck = BusinessAccessLayer.Button.Button.InsertShowMessageButton(pbutton);
                                    if (btnInsertCheck == null)
                                    {
                                        MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                else if (pbutton.Updated == true)
                                {
                                    BusinessObjects.Models.ShowMessage btnUpdateCheck = BusinessAccessLayer.Button.Button.UpdateShowMessageButton(pbutton);
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
                LstShowMessageButtons = null;
                LstIssueTicketButtons = null;
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
                LstShowMessageButtons = null;
                LstIssueTicketButtons = null;
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
                        int CheckDelete = BusinessAccessLayer.Button.Button.DeleteButtonWhere(pButtonsDetailsIds, "id");
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
                        DataGridViewRow buttonRow = gvButtons.SelectedRows[0];
                        int buttonId = (int)buttonRow.Cells["id"].Value;
                        string buttonType = (string)buttonRow.Cells["Type"].Value;
                        if (buttonType == BusinessObjects.Models.btnType.ShowMessage.ToString())
                        {
                            BusinessObjects.Models.ShowMessage CurrentButton = LstShowMessageButtons.Where(x => x.id == buttonId).FirstOrDefault();
                            CurrentButton.indexUpdated = LstShowMessageButtons.IndexOf(CurrentButton);
                            AddEditButton addEditButton = new AddEditButton(CurrentScreen, CurrentButton);
                            this.Hide();
                            addEditButton.Show();
                        }
                        else
                        {
                            BusinessObjects.Models.IssueTicket CurrentButton = LstIssueTicketButtons.Where(x => x.id == buttonId).FirstOrDefault();
                            CurrentButton.indexUpdated = LstIssueTicketButtons.IndexOf(CurrentButton);
                            AddEditButton addEditButton = new AddEditButton(CurrentScreen, CurrentButton);
                            this.Hide();
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
                LstButtons = new List<BusinessObjects.Models.ButtonMaster>();
                if ((LstShowMessageButtons == null || LstIssueTicketButtons == null) || (LstShowMessageButtons.Count == 0 && LstIssueTicketButtons.Count == 0))
                {
                    LstShowMessageButtons = BusinessAccessLayer.Button.Button.SelectButtonsbyScreenId<BusinessObjects.Models.ShowMessage>(CurrentScreen.id, BusinessObjects.Models.btnType.ShowMessage);
                    LstIssueTicketButtons = BusinessAccessLayer.Button.Button.SelectButtonsbyScreenId<BusinessObjects.Models.IssueTicket>(CurrentScreen.id, BusinessObjects.Models.btnType.IssueTicket);
                }
                RefreshGrid();
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        private void RefreshGrid()
        {
            LstButtons.Clear();
            foreach (var item in LstShowMessageButtons)
            {
                LstButtons.Add(new BusinessObjects.Models.ButtonMaster(item.id, item.ENName, item.ARName, item.ScreenId, item.Type));
            }
            foreach (var item in LstIssueTicketButtons)
            {
                LstButtons.Add(new BusinessObjects.Models.ButtonMaster(item.id, item.ENName, item.ARName, item.ScreenId, item.Type));
            }
            if (LstButtons != null)
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