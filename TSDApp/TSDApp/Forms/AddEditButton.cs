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
        private BusinessObjects.Models.ShowMessageButton ShowMessageButton;
        private BusinessObjects.Models.IssueTicketButton IssueTicketButton;
        public event EventHandler<BusinessObjects.Models.ShowMessageButton> SaveShowMessageButton;
        public event EventHandler<BusinessObjects.Models.IssueTicketButton> SaveIssueTicketButton;
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
                FillComboBox();
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
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
                FillComboBox();
                if (!(pShowMessageButton == null))
                {
                    ShowMessageButton = pShowMessageButton;
                    FillShowMessageData();
                }
                else
                {
                    IssueTicketButton = pIssueTicketButton;
                    FillIssueTicketData();
                }
                lblTitle.Text = "Edit Button";
                btnSave.Text = "Edit";
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
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
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
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
                sharingMethods.SaveExceptionToLogFile(ex);
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
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Override OnFormClosing method to dispose current form and move to the previous form
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                ShowMessageButton = null;
                IssueTicketButton = null;
                this.Dispose();
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Cancel button to dispose current form and move to the previous form
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ShowMessageButton = null;
                IssueTicketButton = null;
                this.Dispose();
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
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
                                IssueTicketButton = new BusinessObjects.Models.IssueTicketButton(0, txtENName.Text, txtARName.Text, Convert.ToInt32(ddlIssueTicket.SelectedValue), 0);
                                OnSaveIssueTicketButton(IssueTicketButton);
                            }
                            else
                            {
                                IssueTicketButton = new BusinessObjects.Models.IssueTicketButton(IssueTicketButton.id, txtENName.Text, txtARName.Text, Convert.ToInt32(ddlIssueTicket.SelectedValue), IssueTicketButton.screenId, true, IssueTicketButton.indexUpdated);
                                BusinessAccessLayer.BALButton.BALButton bALButton = new BusinessAccessLayer.BALButton.BALButton();
                                if (IssueTicketButton.id != 0 && bALButton.CheckIfButtonIsDeleted(IssueTicketButton.id,BusinessObjects.Models.btnType.IssueTicket))
                                {
                                    IssueTicketButton.indexUpdated = -2;
                                    MessageBox.Show("Button cant be save to database because someone already delete it", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                OnSaveIssueTicketButton(IssueTicketButton);
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
                                        ShowMessageButton = new BusinessObjects.Models.ShowMessageButton(0, txtENName.Text, txtARName.Text, txtMessageAR.Text, txtMessageEN.Text, 0);
                                        OnSaveShowMessageButton(ShowMessageButton);
                                    }
                                    else
                                    {
                                        ShowMessageButton = new BusinessObjects.Models.ShowMessageButton(ShowMessageButton.id, txtENName.Text, txtARName.Text, txtMessageAR.Text, txtMessageEN.Text, ShowMessageButton.id, true, ShowMessageButton.indexUpdated);
                                        BusinessAccessLayer.BALButton.BALButton bALButton = new BusinessAccessLayer.BALButton.BALButton();
                                        if (ShowMessageButton.id != 0 && bALButton.CheckIfButtonIsDeleted(ShowMessageButton.id, BusinessObjects.Models.btnType.ShowMessage))
                                        {
                                            ShowMessageButton.indexUpdated = -2;
                                            MessageBox.Show("Button cant be save to database because someone already delete it", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                        OnSaveShowMessageButton(ShowMessageButton);
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
                        ShowMessageButton = null;
                        IssueTicketButton = null;
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
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        #endregion

        #region Methods
        private void OnSaveShowMessageButton(BusinessObjects.Models.ShowMessageButton showMessageButton)
        {
            var handler = SaveShowMessageButton;
            if (SaveShowMessageButton != null)
            {
                SaveShowMessageButton.Invoke(this, showMessageButton);
            }
        }
        private void OnSaveIssueTicketButton(BusinessObjects.Models.IssueTicketButton issueTicketButton)
        {
            var handler = SaveIssueTicketButton;
            if (SaveIssueTicketButton != null)
            {
                SaveIssueTicketButton.Invoke(this, issueTicketButton);
            }
        }
        /// <summary>
        /// Function to fill Issue Ticket data for the current button
        /// </summary>
        /// <param name="pButtonId"></param>
        private void FillShowMessageData()
        {
            try
            {
                txtENName.Text = ShowMessageButton.enName;
                txtARName.Text = ShowMessageButton.arName;
                txtMessageEN.Text = ShowMessageButton.messageEN;
                txtMessageAR.Text = ShowMessageButton.messageAR;
                rbShowMessage.Checked = true;
                lblIssueTicket.Visible = false;
                ddlIssueTicket.Visible = false;
                rbIssueTicket.Enabled = false;
                ddlIssueTicket.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.SaveExceptionToLogFile(ex);
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
                txtENName.Text = IssueTicketButton.enName;
                txtARName.Text = IssueTicketButton.arName;
                ddlIssueTicket.SelectedValue = IssueTicketButton.serviceId;
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
                sharingMethods.SaveExceptionToLogFile(ex);
            }
        }

        private void FillComboBox()
        {
            ddlIssueTicket.DropDownStyle = ComboBoxStyle.DropDownList;
            ddlIssueTicket.Items.Clear();
            BusinessAccessLayer.BALService.BALService service = new BusinessAccessLayer.BALService.BALService();
            List<BusinessObjects.Models.Service> ListIssueTicket = service.SelectIssueTicketType();
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