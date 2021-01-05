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
        //Variable to define selected screen id
        private int ScreenId;
        //Object to know currently button
        private static Models.Button CurrentButton;
        //Default constructor
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
        /*Parametarize constructor (if new button) :
          Fill screenId to know the currently screen id*/
        public AddEditButton(int pId)
        {
            try
            {
                InitializeComponent();
                ScreenId = pId;
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /*Parametarize constructor (if edit button) :
          Fill screenId to know the currently screen id
          Fill data for current button to this form*/
        public AddEditButton(int pScreenId, int pButtonId)
        {
            try
            {
                InitializeComponent();
                ScreenId = pScreenId;
                FillButtonData(pButtonId);
                lblTitle.Text = "Edit Button";
                btnSave.Text = "Edit";
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        //Function to fill data for the current button
        private void FillButtonData(int pButtonId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(TSD.GlobalConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM tblButtons where id = @id";
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = pButtonId;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        CurrentButton = new Models.Button(Convert.ToInt32(reader["id"]), reader["ENName"].ToString(), reader["ARName"].ToString(), reader["type"].ToString(), reader["MessageAR"].ToString(), reader["MessageEN"].ToString(), reader["issueType"].ToString(), (int)reader["ScreenId"]);
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
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        private void AddEditButton_Load(object sender, EventArgs e)
        {
            try
            {
                /*Check if CurrentButton is null :
                  if CurrentButton is null then add button
                  if CurrentButton is not null then edit button*/
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
        private void rbShowMessage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //Show and hide component
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
        private void rbIssueTicket_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //Show and hide component
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
        //Override OnFormClosing method to dispose current form and move to the previous form
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                CurrentButton = null;
                this.Dispose();
                AddEditScreen addEditScreen = new AddEditScreen(TSD.CurrentBank.id, ScreenId);
                addEditScreen.Show();
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        //Cancel button to dispose current form and move to the previous form
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentButton = null;
                this.Dispose();
                AddEditScreen addEditScreen = new AddEditScreen(TSD.CurrentBank.id, ScreenId);
                addEditScreen.Show();
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        //Save button to insert or update current button
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
                                    //Call insertNewButton to insert new button and go to previous form
                                    insertNewButton(txtENName.Text, txtARName.Text, rbIssueTicket.Text, null, null, ddlIssueTicket.SelectedItem.ToString(), ScreenId);
                                    MessageBox.Show("Button had been added to screen successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    CurrentButton = null;
                                    this.Dispose();
                                    AddEditScreen addEditScreen = new AddEditScreen(TSD.CurrentBank.id, ScreenId);
                                    addEditScreen.Show();
                                }
                                else
                                {
                                    //Call UpdateButton to update current button and go to previous form
                                    UpdateButton(txtENName.Text, txtARName.Text, rbIssueTicket.Text, null, null, ddlIssueTicket.SelectedItem.ToString(), ScreenId);
                                    MessageBox.Show("Button had been updated", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    CurrentButton = null;
                                    this.Dispose();
                                    AddEditScreen addEditScreen = new AddEditScreen(TSD.CurrentBank.id, ScreenId);
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
                                    if (CurrentButton == null)
                                    {
                                        //Call insertNewButton to insert new button and go to previous form
                                        insertNewButton(txtENName.Text, txtARName.Text, rbShowMessage.Text, txtMessageEN.Text, txtMessageAR.Text, null, ScreenId);
                                        MessageBox.Show("Button had been added to screen successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        CurrentButton = null;
                                        this.Dispose();
                                        AddEditScreen addEditScreen = new AddEditScreen(TSD.CurrentBank.id, ScreenId);
                                        addEditScreen.Show();
                                    }
                                    else
                                    {
                                        //Call UpdateButton to update current button and go to previous form
                                        UpdateButton(txtENName.Text, txtARName.Text, rbShowMessage.Text, txtMessageEN.Text, txtMessageAR.Text, null, ScreenId);
                                        MessageBox.Show("Button had been updated", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        CurrentButton = null;
                                        this.Dispose();
                                        AddEditScreen addEditScreen = new AddEditScreen(TSD.CurrentBank.id, ScreenId);
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
        //Function to insert new button
        private void insertNewButton(string pENName, string pARName, string pType, string pMessageEN, string pMessageAR, string pIssueType, int pScreenId)
        {
            try
            {
                int LastID;
                using (SqlConnection con = new SqlConnection(TSD.GlobalConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "insert into tblButtons OUTPUT INSERTED.IDENTITYCOL  values (@ENName,@ARName,@Type,@MessageAR,@MessageEN,@issueType,@ScreenId)";
                    cmd.Parameters.Add("@ENName", SqlDbType.NVarChar).Value = pENName;
                    cmd.Parameters.Add("@ARName", SqlDbType.NVarChar).Value = pARName;
                    if(pType == rbIssueTicket.Text)
                    {
                        cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = pType;
                        cmd.Parameters.Add("@MessageAR", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd.Parameters.Add("@MessageEN", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd.Parameters.Add("@issueType", SqlDbType.NVarChar).Value = pIssueType;
                    }
                    else
                    {
                        cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = pType;
                        cmd.Parameters.Add("@MessageAR", SqlDbType.NVarChar).Value = pMessageAR;
                        cmd.Parameters.Add("@MessageEN", SqlDbType.NVarChar).Value = pMessageEN;
                        cmd.Parameters.Add("@issueType", SqlDbType.NVarChar).Value = DBNull.Value;
                    }
                    cmd.Parameters.Add("@ScreenId", SqlDbType.Int).Value = ScreenId;
                    LastID = (int)cmd.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        //Function to edit current button
        private void UpdateButton(string pENName, string pARName, string pType, string pMessageEN, string pMessageAR, string pIssueType, int pScreenId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(TSD.GlobalConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "update tblButtons set ENName = @ENName,ARName = @ARName,Type = @Type,MessageAR = @MessageAR,MessageEN = @MessageEN,issueType = @issueType,ScreenId = @ScreenId where id = @id";
                    cmd.Parameters.Add("@ENName", SqlDbType.NVarChar).Value = pENName;
                    cmd.Parameters.Add("@ARName", SqlDbType.NVarChar).Value = pARName;
                    if (pType == rbIssueTicket.Text)
                    {
                        cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = pType;
                        cmd.Parameters.Add("@MessageAR", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd.Parameters.Add("@MessageEN", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd.Parameters.Add("@issueType", SqlDbType.NVarChar).Value = pIssueType;
                    }
                    else
                    {
                        cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = pType;
                        cmd.Parameters.Add("@MessageAR", SqlDbType.NVarChar).Value = pMessageAR;
                        cmd.Parameters.Add("@MessageEN", SqlDbType.NVarChar).Value = pMessageEN;
                        cmd.Parameters.Add("@issueType", SqlDbType.NVarChar).Value = DBNull.Value;
                    }
                    cmd.Parameters.Add("@ScreenId", SqlDbType.Int).Value = ScreenId;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = CurrentButton.id;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
    }
}
