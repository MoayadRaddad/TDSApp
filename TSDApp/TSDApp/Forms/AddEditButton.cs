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
using TSDApp.Fomrs;

namespace TSDApp.Forms
{
    public partial class AddEditButton : Form
    {
        #region Variables
        private static BusinessObjects.Models.ShowMessage ShowMessageButton;
        private static BusinessObjects.Models.IssueTicket IssueTicketButton;
        private static Models.Screen CurrentScreen;
        #endregion

        #region constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public AddEditButton()
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
        /// Parametarize constructor get screen object
        /// </summary>
        /// <param name="pScreen"></param>
        public AddEditButton(Models.Screen pScreen)
        {
            try
            {
                InitializeComponent();
                FillComboBox();
                CurrentScreen = pScreen;
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Parametarize constructor (if edit button):
        /// Fill screenId to know the currently screen id
        /// Fill data for current button to this form
        /// </summary>
        /// <param name="pScreenId"></param>
        /// <param name="pButtonId"></param>
        public AddEditButton(Models.Screen pScreen, BusinessObjects.Models.ShowMessage pButton)
        {
            try
            {
                InitializeComponent();
                FillComboBox();
                CurrentScreen = new Models.Screen();
                CurrentScreen = pScreen;
                ShowMessageButton = pButton;
                FillShowMessageData();
                lblTitle.Text = "Edit Button";
                btnSave.Text = "Edit";
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        public AddEditButton(Models.Screen pScreen, BusinessObjects.Models.IssueTicket pButton)
        {
            try
            {
                InitializeComponent();
                FillComboBox();
                CurrentScreen = new Models.Screen();
                CurrentScreen = pScreen;
                IssueTicketButton = pButton;
                FillIssueTicketData();
                lblTitle.Text = "Edit Button";
                btnSave.Text = "Edit";
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        #endregion

        #region Events
        private void AddEditButton_Load(object sender, EventArgs e)
        {
            try
            {
                if (IssueTicketButton == null && ShowMessageButton == null)
                {
                    rbIssueTicket.Checked = true;
                    ddlIssueTicket.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Hide and show textboxes
        /// </summary>
        private void rbShowMessage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbShowMessage.Checked)
                {
                    rbIssueTicket.Checked = false;
                    lblMessageAR.Visible = true;
                    txtMessageAR.Visible = true;
                    lblMessageEN.Visible = true;
                    txtMessageEN.Visible = true;
                    lblIssueTicket.Visible = false;
                    ddlIssueTicket.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Hide and show textboxes
        /// </summary>
        private void rbIssueTicket_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbIssueTicket.Checked)
                {
                    rbShowMessage.Checked = false;
                    lblMessageAR.Visible = false;
                    txtMessageAR.Visible = false;
                    lblMessageEN.Visible = false;
                    txtMessageEN.Visible = false;
                    lblIssueTicket.Visible = true;
                    ddlIssueTicket.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Override OnFormClosing method to dispose current form and move to the previous form
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                AddEditScreen addEditScreen = null;
                ShowMessageButton = null;
                IssueTicketButton = null;
                this.Dispose();
                if (CurrentScreen.id != 0)
                {
                    addEditScreen = new AddEditScreen(TSD.CurrentBank.id, CurrentScreen, null, ShowMessageButton);
                }
                else
                {
                    addEditScreen = new AddEditScreen(TSD.CurrentBank.id);
                }
                CurrentScreen = null;
                addEditScreen.Show();
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Cancel button to dispose current form and move to the previous form
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                AddEditScreen addEditScreen = null;
                ShowMessageButton = null;
                IssueTicketButton = null;
                this.Dispose();
                addEditScreen = new AddEditScreen(TSD.CurrentBank.id, CurrentScreen, null, ShowMessageButton);
                CurrentScreen = null;
                addEditScreen.Show();
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Save button to insert or update current button
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Check if EN name is filled or not
                if (txtENName.Text != "")
                {
                    //Check if AR name is filled or not
                    if (txtARName.Text != "")
                    {
                        //Check which radio button checked
                        if (rbIssueTicket.Checked)
                        {
                            //Check if it is new button to insert or edit button to update
                            if (IssueTicketButton == null)
                            {
                                IssueTicketButton = new BusinessObjects.Models.IssueTicket(0, txtENName.Text, txtARName.Text, Convert.ToInt32(ddlIssueTicket.SelectedValue), CurrentScreen.id);
                                this.Dispose();
                                AddEditScreen addEditScreen = new AddEditScreen(TSD.CurrentBank.id, CurrentScreen, IssueTicketButton, null);
                                addEditScreen.Show();
                                IssueTicketButton = null;
                            }
                            else
                            {
                                BusinessObjects.Models.IssueTicket OldButton = IssueTicketButton;
                                IssueTicketButton = new BusinessObjects.Models.IssueTicket(IssueTicketButton.id, txtENName.Text, txtARName.Text, Convert.ToInt32(ddlIssueTicket.SelectedValue), CurrentScreen.id, true, OldButton.indexUpdated);
                                this.Dispose();
                                AddEditScreen addEditScreen = new AddEditScreen(TSD.CurrentBank.id, CurrentScreen, IssueTicketButton, OldButton);
                                addEditScreen.Show();
                                IssueTicketButton = null;
                            }
                        }
                        else
                        {
                            //Check if AR message is filled or not
                            if (txtMessageEN.Text != "")
                            {
                                //Check if AR message is filled or not
                                if (txtMessageAR.Text != "")
                                {
                                    //Check if it is new button to insert or edit button to update
                                    if (ShowMessageButton == null)
                                    {
                                        ShowMessageButton = new BusinessObjects.Models.ShowMessage(0, txtENName.Text, txtARName.Text, txtMessageAR.Text, txtMessageEN.Text, CurrentScreen.id);
                                        this.Dispose();
                                        AddEditScreen addEditScreen = new AddEditScreen(TSD.CurrentBank.id, CurrentScreen, ShowMessageButton, null);
                                        addEditScreen.Show();
                                        ShowMessageButton = null;
                                    }
                                    else
                                    {
                                        BusinessObjects.Models.ShowMessage OldButton = ShowMessageButton;
                                        ShowMessageButton = new BusinessObjects.Models.ShowMessage(ShowMessageButton.id, txtENName.Text, txtARName.Text, txtMessageAR.Text, txtMessageEN.Text, CurrentScreen.id, true, OldButton.indexUpdated);
                                        this.Dispose();
                                        AddEditScreen addEditScreen = new AddEditScreen(TSD.CurrentBank.id, CurrentScreen, ShowMessageButton, OldButton);
                                        addEditScreen.Show();
                                        ShowMessageButton = null;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please fill AR Message", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }

                            }
                            else
                            {
                                MessageBox.Show("Please fill EN Message", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please fill AR Name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please fill EN Name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// Function to fill Issue Ticket data for the current button
        /// </summary>
        /// <param name="pButtonId"></param>
        private void FillShowMessageData()
        {
            try
            {
                txtENName.Text = ShowMessageButton.ENName;
                txtARName.Text = ShowMessageButton.ARName;
                txtMessageEN.Text = ShowMessageButton.MessageEN;
                txtMessageAR.Text = ShowMessageButton.MessageAR;
                rbShowMessage.Checked = true;
                lblIssueTicket.Visible = false;
                ddlIssueTicket.Visible = false;
                rbIssueTicket.Enabled = false;
                ddlIssueTicket.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Function to fill Show Message data for the current button
        /// </summary>
        /// <param name="pButtonId"></param>
        private void FillIssueTicketData()
        {
            try
            {
                txtENName.Text = IssueTicketButton.ENName;
                txtARName.Text = IssueTicketButton.ARName;
                ddlIssueTicket.SelectedValue = IssueTicketButton.SreviceType;
                rbIssueTicket.Checked = true;
                lblMessageAR.Visible = false;
                txtMessageAR.Visible = false;
                lblMessageEN.Visible = false;
                txtMessageEN.Visible = false;
                rbShowMessage.Enabled = false;
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }

        private void FillComboBox()
        {
            ddlIssueTicket.DropDownStyle = ComboBoxStyle.DropDownList;
            ddlIssueTicket.Items.Clear();
            List<BusinessObjects.Models.IssueServiceType> ListIssueTicket = BusinessAccessLayer.IssueTicketType.IssueTicketType.SelectIssueTicketType();
            if (ListIssueTicket != null)
            {
                ddlIssueTicket.DataSource = ListIssueTicket;
                ddlIssueTicket.ValueMember = "id";
                ddlIssueTicket.DisplayMember = "Name";
            }
            else
            {
                MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}