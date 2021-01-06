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
        private static Models.Button CurrentButton;
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
        /// Parametarize constructor (if new button)
        /// </summary>
        /// <param name="pScreenId"></param>
        public AddEditButton(int pScreenId)
        {
            try
            {
                InitializeComponent();
                CurrentScreen = new Models.Screen();
                CurrentScreen.id = pScreenId;
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
        public AddEditButton(int pScreenId, int pButtonId)
        {
            try
            {
                InitializeComponent();
                CurrentScreen.id = pScreenId;
                FillButtonData(pButtonId);
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
                if (CurrentButton == null)
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
                CurrentButton = null;
                this.Dispose();
                if (CurrentScreen.id != 0)
                {
                    addEditScreen = new AddEditScreen(TSD.CurrentBank.id, CurrentScreen.id);
                }
                else
                {
                    addEditScreen = new AddEditScreen(TSD.CurrentBank.id);
                }
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
                CurrentButton = null;
                this.Dispose();
                if (CurrentScreen.id != 0)
                {
                    addEditScreen = new AddEditScreen(TSD.CurrentBank.id, CurrentScreen.id);
                }
                else
                {
                    addEditScreen = new AddEditScreen(TSD.CurrentBank.id);
                }
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
                            //Check if combobox selected
                            if (ddlIssueTicket.SelectedIndex != 0)
                            {
                                //Check if it is new button to insert or edit button to update
                                if (CurrentButton == null)
                                {
                                    if (CurrentScreen.id == 0)
                                    {
                                        string pInsertScreenquery = "insert into tblScreens OUTPUT INSERTED.IDENTITYCOL  values (@Name,@isActive,@BankId)";
                                        CurrentScreen = BusinessAccessLayer.Screen.Screen.InsertScreen(pInsertScreenquery, CurrentScreen);
                                    }
                                    //Call insertNewButton to insert new button and go to previous form
                                    CurrentButton = new Models.Button(0, txtENName.Text, txtARName.Text, rbIssueTicket.Text, null, null, ddlIssueTicket.SelectedItem.ToString(), CurrentScreen.id);
                                    string pInsertButton = "insert into tblButtons OUTPUT INSERTED.IDENTITYCOL  values (@ENName,@ARName,@Type,@MessageAR,@MessageEN,@issueType,@ScreenId)";
                                    BusinessAccessLayer.Button.Button.InsertButton(pInsertButton, CurrentButton);
                                    MessageBox.Show("Button had been added to screen successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    CurrentButton = null;
                                    this.Dispose();
                                    AddEditScreen addEditScreen = new AddEditScreen(TSD.CurrentBank.id, CurrentScreen.id);
                                    addEditScreen.Show();
                                }
                                else
                                {
                                    //Call UpdateButton to update current button and go to previous form
                                    CurrentButton = new Models.Button(CurrentButton.id, txtENName.Text, txtARName.Text, rbIssueTicket.Text, null, null, ddlIssueTicket.SelectedItem.ToString(), CurrentScreen.id);
                                    string pInsertButton = "update tblButtons set ENName = @ENName,ARName = @ARName,Type = @Type,MessageAR = @MessageAR,MessageEN = @MessageEN,issueType = @issueType,ScreenId = @ScreenId where id = @id";
                                    BusinessAccessLayer.Button.Button.UpdateButton(pInsertButton, CurrentButton);
                                    MessageBox.Show("Button had been updated", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    CurrentButton = null;
                                    this.Dispose();
                                    AddEditScreen addEditScreen = new AddEditScreen(TSD.CurrentBank.id, CurrentScreen.id);
                                    addEditScreen.Show();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please select issue ticket", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                    if (CurrentButton == null)
                                    {
                                        if (CurrentScreen.id == 0)
                                        {
                                            string pInsertScreenquery = "insert into tblScreens OUTPUT INSERTED.IDENTITYCOL  values (@Name,@isActive,@BankId)";
                                            CurrentScreen = BusinessAccessLayer.Screen.Screen.InsertScreen(pInsertScreenquery, CurrentScreen);
                                        }
                                        //Call insertNewButton to insert new button and go to previous form
                                        CurrentButton = new Models.Button(0, txtENName.Text, txtARName.Text, rbShowMessage.Text, txtMessageEN.Text, txtMessageAR.Text, null, CurrentScreen.id);
                                        string pInsertButton = "insert into tblButtons OUTPUT INSERTED.IDENTITYCOL  values (@ENName,@ARName,@Type,@MessageAR,@MessageEN,@issueType,@ScreenId)";
                                        BusinessAccessLayer.Button.Button.InsertButton(pInsertButton, CurrentButton);
                                        MessageBox.Show("Button had been added to screen successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        CurrentButton = null;
                                        this.Dispose();
                                        AddEditScreen addEditScreen = new AddEditScreen(TSD.CurrentBank.id, CurrentScreen.id);
                                        addEditScreen.Show();
                                    }
                                    else
                                    {
                                        //Call UpdateButton to update current button and go to previous form
                                        CurrentButton = new Models.Button(CurrentButton.id, txtENName.Text, txtARName.Text, rbShowMessage.Text, txtMessageEN.Text, txtMessageAR.Text, null, CurrentScreen.id);
                                        string pInsertButton = "update tblButtons set ENName = @ENName,ARName = @ARName,Type = @Type,MessageAR = @MessageAR,MessageEN = @MessageEN,issueType = @issueType,ScreenId = @ScreenId where id = @id";
                                        BusinessAccessLayer.Button.Button.UpdateButton(pInsertButton, CurrentButton);
                                        MessageBox.Show("Button had been updated", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        CurrentButton = null;
                                        this.Dispose();
                                        AddEditScreen addEditScreen = new AddEditScreen(TSD.CurrentBank.id, CurrentScreen.id);
                                        addEditScreen.Show();
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
        /// Function to fill data for the current button
        /// </summary>
        /// <param name="pButtonId"></param>
        private void FillButtonData(int pButtonId)
        {
            try
            {
                string pSelectButtonById = "SELECT * FROM tblButtons where id = @id";
                CurrentButton = BusinessAccessLayer.Button.Button.SelectButtonById(pSelectButtonById, pButtonId);
                txtENName.Text = CurrentButton.ENName;
                txtARName.Text = CurrentButton.ARName;
                txtMessageEN.Text = CurrentButton.MessageEN;
                txtMessageAR.Text = CurrentButton.MessageAR;
                if (CurrentButton.Type == rbIssueTicket.Text)
                {
                    rbIssueTicket.Checked = true;
                    lblMessageAR.Visible = false;
                    txtMessageAR.Visible = false;
                    lblMessageEN.Visible = false;
                    txtMessageEN.Visible = false;
                    if (CurrentButton.issueType == "withdraw")
                    {
                        ddlIssueTicket.SelectedIndex = 1;
                    }
                    else
                    {
                        ddlIssueTicket.SelectedIndex = 2;
                    }
                }
                else
                {
                    rbShowMessage.Checked = true;
                    lblIssueTicket.Visible = false;
                    ddlIssueTicket.Visible = false;
                    ddlIssueTicket.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        #endregion
    }
}