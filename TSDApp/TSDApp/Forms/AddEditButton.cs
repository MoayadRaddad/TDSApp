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
        private BusinessObjects.Models.ShowMessageButton showMessageButton;
        private BusinessObjects.Models.IssueTicketButton issueTicketButton;
        public event EventHandler<BusinessObjects.Models.ShowMessageButton> saveShowMessageButton;
        public event EventHandler<BusinessObjects.Models.IssueTicketButton> saveIssueTicketButton;
        public event EventHandler<int> canelButtonEvent;
        #endregion

        #region constructors
        /// <summary>
        /// Parametarize constructor get screen object
        /// </summary>
        public AddEditButton()
        {
            try
            {
                InitializeComponent();
                fillComboBox();
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.saveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Parametarize constructor (if edit button):
        /// Fill screenId to know the currently screen id
        /// Fill data for current button to this form
        /// </summary>
        /// <param name="pShowMessageButton"></param>
        /// <param name="pIssueTicketButton"></param>
        public AddEditButton(BusinessObjects.Models.ShowMessageButton pShowMessageButton, BusinessObjects.Models.IssueTicketButton pIssueTicketButton)
        {
            try
            {
                InitializeComponent();
                fillComboBox();
                if (!(pShowMessageButton == null))
                {
                    showMessageButton = pShowMessageButton;
                    fillShowMessageData();
                }
                else
                {
                    issueTicketButton = pIssueTicketButton;
                    fillIssueTicketData();
                }
                lblTitle.Text = "Edit Button";
                btnSave.Text = "Edit";
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.saveExceptionToLogFile(ex);
            }
        }
        #endregion

        #region Events
        private void AddEditButton_Load(object sender, EventArgs e)
        {
            try
            {
                if (issueTicketButton == null && showMessageButton == null)
                {
                    rbIssueTicket.Checked = true;
                    ddlIssueTicket.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.saveExceptionToLogFile(ex);
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
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.saveExceptionToLogFile(ex);
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
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.saveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Override OnFormClosing method to dispose current form and move to the previous form
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                showMessageButton = null;
                issueTicketButton = null;
                this.Dispose();
                onCancelButton(1);
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.saveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Cancel button to dispose current form and move to the previous form
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                showMessageButton = null;
                issueTicketButton = null;
                this.Dispose();
                onCancelButton(1);
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.saveExceptionToLogFile(ex);
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
                            if (issueTicketButton == null)
                            {
                                issueTicketButton = new BusinessObjects.Models.IssueTicketButton(0, txtENName.Text, txtARName.Text, Convert.ToInt32(ddlIssueTicket.SelectedValue), 0);
                                onSaveIssueTicketButton(issueTicketButton);
                            }
                            else
                            {
                                issueTicketButton = new BusinessObjects.Models.IssueTicketButton(issueTicketButton.id, txtENName.Text, txtARName.Text, Convert.ToInt32(ddlIssueTicket.SelectedValue), issueTicketButton.screenId, true, issueTicketButton.indexUpdated);
                                BusinessAccessLayer.BALButton.BALButton bALButton = new BusinessAccessLayer.BALButton.BALButton();
                                if (issueTicketButton.id != 0 && bALButton.checkIfButtonIsDeleted(issueTicketButton.id,BusinessObjects.Models.btnType.IssueTicket))
                                {
                                    issueTicketButton.DeletedFromAnotherUsers = true;
                                }
                                onSaveIssueTicketButton(issueTicketButton);
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
                                    if (showMessageButton == null)
                                    {
                                        showMessageButton = new BusinessObjects.Models.ShowMessageButton(0, txtENName.Text, txtARName.Text, txtMessageAR.Text, txtMessageEN.Text, 0);
                                        onSaveShowMessageButton(showMessageButton);
                                    }
                                    else
                                    {
                                        showMessageButton = new BusinessObjects.Models.ShowMessageButton(showMessageButton.id, txtENName.Text, txtARName.Text, txtMessageAR.Text, txtMessageEN.Text, showMessageButton.id, true, showMessageButton.indexUpdated);
                                        BusinessAccessLayer.BALButton.BALButton bALButton = new BusinessAccessLayer.BALButton.BALButton();
                                        if (showMessageButton.id != 0 && bALButton.checkIfButtonIsDeleted(showMessageButton.id, BusinessObjects.Models.btnType.ShowMessage))
                                        {
                                            showMessageButton.DeletedFromAnotherUsers = true;
                                        }
                                        onSaveShowMessageButton(showMessageButton);
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
                        showMessageButton = null;
                        issueTicketButton = null;
                        this.Dispose();
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
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.saveExceptionToLogFile(ex);
            }
        }
        #endregion

        #region Methods
        private void onCancelButton(int enable)
        {
            var handler = canelButtonEvent;
            if (canelButtonEvent != null)
            {
                canelButtonEvent.Invoke(this, enable);
            }
        }
        private void onSaveShowMessageButton(BusinessObjects.Models.ShowMessageButton showMessageButton)
        {
            var handler = saveShowMessageButton;
            if (saveShowMessageButton != null)
            {
                saveShowMessageButton.Invoke(this, showMessageButton);
            }
        }
        private void onSaveIssueTicketButton(BusinessObjects.Models.IssueTicketButton issueTicketButton)
        {
            var handler = saveIssueTicketButton;
            if (saveIssueTicketButton != null)
            {
                saveIssueTicketButton.Invoke(this, issueTicketButton);
            }
        }
        /// <summary>
        /// Function to fill Issue Ticket data for the current button
        /// </summary>
        /// <param name="pButtonId"></param>
        private void fillShowMessageData()
        {
            try
            {
                txtENName.Text = showMessageButton.enName;
                txtARName.Text = showMessageButton.arName;
                txtMessageEN.Text = showMessageButton.messageEN;
                txtMessageAR.Text = showMessageButton.messageAR;
                rbShowMessage.Checked = true;
                lblIssueTicket.Visible = false;
                ddlIssueTicket.Visible = false;
                rbIssueTicket.Enabled = false;
                ddlIssueTicket.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.saveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Function to fill Show Message data for the current button
        /// </summary>
        /// <param name="pButtonId"></param>
        private void fillIssueTicketData()
        {
            try
            {
                txtENName.Text = issueTicketButton.enName;
                txtARName.Text = issueTicketButton.arName;
                ddlIssueTicket.SelectedValue = issueTicketButton.serviceId;
                rbIssueTicket.Checked = true;
                lblMessageAR.Visible = false;
                txtMessageAR.Visible = false;
                lblMessageEN.Visible = false;
                txtMessageEN.Visible = false;
                rbShowMessage.Enabled = false;
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.saveExceptionToLogFile(ex);
            }
        }

        private void fillComboBox()
        {
            ddlIssueTicket.DropDownStyle = ComboBoxStyle.DropDownList;
            ddlIssueTicket.Items.Clear();
            BusinessAccessLayer.BALService.BALService service = new BusinessAccessLayer.BALService.BALService();
            List<BusinessObjects.Models.Service> listIssueTicket = service.selectIssueTicketType();
            if (listIssueTicket != null)
            {
                ddlIssueTicket.DataSource = listIssueTicket;
                ddlIssueTicket.ValueMember = "id";
                ddlIssueTicket.DisplayMember = "Name";
            }
            else
            {
                MessageBox.Show("Please check your connection to databse", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Dispose();
                System.Environment.Exit(1);
            }
        }
        #endregion
    }
}