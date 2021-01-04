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
    //start
    public partial class AddEditButton : Form
    {
        private int ScreenId;
        private static Models.Button CurrentButton;
        public AddEditButton()
        {
            InitializeComponent();
        }
        public AddEditButton(int id)
        {
            try
            {
                InitializeComponent();
                ScreenId = id;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),"Error");
            }
        }
        public AddEditButton(int screenId, int buttonId)
        {
            try
            {
                InitializeComponent();
                ScreenId = screenId;
                FillButtonData(buttonId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),"Error");
            }
        }

        private void FillButtonData(int buttonId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(TSD.GlobalConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT * FROM tblButtons where id = @id";
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = buttonId;

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
                MessageBox.Show(ex.ToString(),"Error");
            }
        }

        private void AddEditButton_Load(object sender, EventArgs e)
        {
            if (CurrentButton == null)
            {
                rbIssueTicket.Checked = true;
                ddlIssueTicket.SelectedIndex = 0;
            }
        }

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
                MessageBox.Show(ex.ToString(),"Error");
            }
        }

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
                MessageBox.Show(ex.ToString(),"Error");
            }
        }

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
                MessageBox.Show(ex.ToString(),"Error");
            }
        }

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
                MessageBox.Show(ex.ToString(),"Error");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtENName.Text != "")
                {
                    if (txtARName.Text != "")
                    {
                        if (rbIssueTicket.Checked)
                        {
                            if (ddlIssueTicket.SelectedIndex != 0)
                            {
                                if (CurrentButton == null)
                                {
                                    insertNewButton(txtENName.Text, txtARName.Text, rbIssueTicket.Text, null, null, ddlIssueTicket.SelectedItem.ToString(), ScreenId);
                                    MessageBox.Show("Button had been added to screen successfully", "Done");
                                    CurrentButton = null;
                                    this.Dispose();
                                    AddEditScreen addEditScreen = new AddEditScreen(TSD.CurrentBank.id, ScreenId);
                                    addEditScreen.Show();
                                }
                                else
                                {
                                    UpdateButton(txtENName.Text, txtARName.Text, rbIssueTicket.Text, null, null, ddlIssueTicket.SelectedItem.ToString(), ScreenId);
                                    MessageBox.Show("Button had been updated", "Done");
                                    CurrentButton = null;
                                    this.Dispose();
                                    AddEditScreen addEditScreen = new AddEditScreen(TSD.CurrentBank.id, ScreenId);
                                    addEditScreen.Show();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please select issue ticket", "Warning");
                            }
                        }
                        else
                        {
                            if (txtMessageEN.Text != "")
                            {
                                if (txtMessageAR.Text != "")
                                {
                                    if (CurrentButton == null)
                                    {
                                        insertNewButton(txtENName.Text, txtARName.Text, rbShowMessage.Text, txtMessageEN.Text, txtMessageAR.Text, null, ScreenId);
                                        CurrentButton = null;
                                        this.Dispose();
                                        AddEditScreen addEditScreen = new AddEditScreen(TSD.CurrentBank.id, ScreenId);
                                        addEditScreen.Show();
                                    }
                                    else
                                    {
                                        UpdateButton(txtENName.Text, txtARName.Text, rbShowMessage.Text, txtMessageEN.Text, txtMessageAR.Text, null, ScreenId);
                                        CurrentButton = null;
                                        this.Dispose();
                                        AddEditScreen addEditScreen = new AddEditScreen(TSD.CurrentBank.id, ScreenId);
                                        addEditScreen.Show();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please fill AR Message", "Warning");
                                }

                            }
                            else
                            {
                                MessageBox.Show("Please fill EN Message", "Warning");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please fill AR Name", "Warning");
                    }
                }
                else
                {
                    MessageBox.Show("Please fill EN Name", "Warning");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),"Error");
            }
        }

        private void insertNewButton(string ENName, string ARName, string Type, string MessageEN, string MessageAR, string issueType, int ScreenId)
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
                    cmd.Parameters.Add("@ENName", SqlDbType.NVarChar).Value = ENName;
                    cmd.Parameters.Add("@ARName", SqlDbType.NVarChar).Value = ARName;
                    if(Type == rbIssueTicket.Text)
                    {
                        cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = Type;
                        cmd.Parameters.Add("@MessageAR", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd.Parameters.Add("@MessageEN", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd.Parameters.Add("@issueType", SqlDbType.NVarChar).Value = issueType;
                    }
                    else
                    {
                        cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = Type;
                        cmd.Parameters.Add("@MessageAR", SqlDbType.NVarChar).Value = MessageAR;
                        cmd.Parameters.Add("@MessageEN", SqlDbType.NVarChar).Value = MessageEN;
                        cmd.Parameters.Add("@issueType", SqlDbType.NVarChar).Value = DBNull.Value;
                    }
                    cmd.Parameters.Add("@ScreenId", SqlDbType.Int).Value = ScreenId;
                    LastID = (int)cmd.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),"Error");
            }
        }

        private void UpdateButton(string ENName, string ARName, string Type, string MessageEN, string MessageAR, string issueType, int ScreenId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(TSD.GlobalConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "update tblButtons set ENName = @ENName,ARName = @ARName,Type = @Type,MessageAR = @MessageAR,MessageEN = @MessageEN,issueType = @issueType,ScreenId = @ScreenId where id = @id";
                    cmd.Parameters.Add("@ENName", SqlDbType.NVarChar).Value = ENName;
                    cmd.Parameters.Add("@ARName", SqlDbType.NVarChar).Value = ARName;
                    if (Type == rbIssueTicket.Text)
                    {
                        cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = Type;
                        cmd.Parameters.Add("@MessageAR", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd.Parameters.Add("@MessageEN", SqlDbType.NVarChar).Value = DBNull.Value;
                        cmd.Parameters.Add("@issueType", SqlDbType.NVarChar).Value = issueType;
                    }
                    else
                    {
                        cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = Type;
                        cmd.Parameters.Add("@MessageAR", SqlDbType.NVarChar).Value = MessageAR;
                        cmd.Parameters.Add("@MessageEN", SqlDbType.NVarChar).Value = MessageEN;
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
                MessageBox.Show(ex.ToString(),"Error");
            }
        }
    }
}
