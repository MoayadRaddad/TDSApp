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
        private int BankId;
        private static Models.Screen CurrentScreen;
        public AddEditScreen()
        {
            InitializeComponent();
        }
        public AddEditScreen(int id)
        {
            try
            {
                InitializeComponent();
                BankId = id;
                ddlActive.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }
        public AddEditScreen(int bankId, int screenId)
        {
            try
            {
                InitializeComponent();
                BankId = bankId;
                FillScreenData(screenId);
                Fillbuttons();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void AddEditScreen_Load(object sender, EventArgs e)
        {
            try
            {
                LoadToolTips();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

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
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text != "")
                {
                    if (ddlActive.SelectedIndex != 0)
                    {
                        if (CurrentScreen == null)
                        {
                            insertNewScreen(txtName.Text, Convert.ToBoolean(ddlActive.SelectedItem));
                            MessageBox.Show("Screen has been saved successfully", "Done");
                        }
                        else
                        {
                            if (gvButtons.RowCount > 0)
                            {
                                UpdateScreen(txtName.Text, Convert.ToBoolean(ddlActive.SelectedItem));
                                CurrentScreen = null;
                                MessageBox.Show("Screen has been updated successfully", "Done");
                            }
                            else
                            {
                                MessageBox.Show("Screen cannot be saved because there is no buttons added to this screen", "Done");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select active mode", "Warning");
                    }
                }
                else
                {
                    MessageBox.Show("Please fill screen Name", "Warning");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void DeleteScreen(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(TSD.GlobalConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "delete from tblScreens where id = @id";
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void insertActiveScreen(int LastID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(TSD.GlobalConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "update tblScreens set isActive = 0 where id != @id";
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = LastID.ToString();
                    cmd.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void insertNewScreen(string ScreenName, bool isActive)
        {
            try
            {
                int LastID;
                using (SqlConnection con = new SqlConnection(TSD.GlobalConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "insert into tblScreens OUTPUT INSERTED.IDENTITYCOL  values (@Name,@isActive,@BankId)";
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = ScreenName;
                    cmd.Parameters.Add("@isActive", SqlDbType.Bit).Value = isActive;
                    cmd.Parameters.Add("@BankId", SqlDbType.NVarChar).Value = BankId;
                    LastID = (int)cmd.ExecuteScalar();
                    con.Close();
                    CurrentScreen = new Models.Screen(LastID, ScreenName, isActive == true ? "Activated" : "Deactivated", BankId);
                }
                if (ddlActive.SelectedIndex == 1)
                {
                    insertActiveScreen(LastID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void UpdateScreen(string ScreenName, bool isActive)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(TSD.GlobalConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "update tblScreens set name = @Name,isActive = @isActive where id = @id";
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = ScreenName;
                    cmd.Parameters.Add("@isActive", SqlDbType.Bit).Value = isActive;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = CurrentScreen.id;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (ddlActive.SelectedIndex == 1)
                {
                    insertActiveScreen(CurrentScreen.id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                TSD tsd = null;
                if (gvButtons.RowCount > 0)
                {
                    CurrentScreen = null;
                    this.Dispose();
                    tsd = new TSD(true);
                    tsd.Show();
                }
                else
                {
                    if (CurrentScreen != null)
                    {
                        DialogResult dialogResult = MessageBox.Show(@"The Screen will be discard because there is no button\s added to this screen, are you sure you want to continue ?", "Warning", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            DeleteScreen(CurrentScreen.id);
                            CurrentScreen = null;
                            this.Dispose();
                            tsd = new TSD(true);
                            tsd.Show();
                        }
                    }
                    else
                    {
                        this.Dispose();
                        tsd = new TSD(true);
                        tsd.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void FillScreenData(int screenId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(TSD.GlobalConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT id,name,isActive,BankId FROM tblScreens where id = @id";
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = screenId;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        CurrentScreen = new Models.Screen(Convert.ToInt32(reader["id"]), reader["name"].ToString(), (bool)reader["isActive"] == true ? "Activated" : "Deactivated", (int)reader["BankId"]);
                        txtName.Text = CurrentScreen.Name;
                        if (CurrentScreen.isActive.ToString() == "Activated")
                        {
                            ddlActive.SelectedIndex = 1;
                        }
                        else
                        {
                            ddlActive.SelectedIndex = 2;
                        }
                        ddlActive.SelectedText = CurrentScreen.isActive.ToString();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                if (gvButtons.RowCount == 0)
                {
                    if (CurrentScreen != null)
                    {
                        DialogResult dialogResult = MessageBox.Show(@"The Screen will be discard because there is no button\s added to this screen.", "Warning");
                        DeleteScreen(CurrentScreen.id);
                        CurrentScreen = null;
                    }
                }
                TSD tsd = null;
                this.Dispose();
                tsd = new TSD(true);
                tsd.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void Fillbuttons()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(TSD.GlobalConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = con;
                    cmd.CommandText = "SELECT id, ENName, ARName, Type, MessageAR, MessageEN, issueType, ScreenId FROM tblButtons where ScreenId = @ScreenId";
                    cmd.Parameters.Add("@ScreenId", SqlDbType.Int).Value = CurrentScreen.id;
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    List<Models.Button> lstButtons = new List<Models.Button>();
                    while (reader.Read())
                    {
                        lstButtons.Add(new Models.Button((int)reader["id"], (string)reader["ENName"], (string)reader["ARName"], (string)reader["Type"],
                            reader["MessageAR"] == null ? (string)reader["MessageAR"] : null, reader["MessageEN"] == null ? (string)reader["MessageEN"] : null,
                            reader["issueType"] == null ? (string)reader["issueType"] : null, (int)reader["ScreenId"]));
                    }
                    con.Close();
                    gvButtons.DataSource = lstButtons;
                }
                ChangeColumnWidth();
                this.gvButtons.Columns[0].Visible = false;
                this.gvButtons.Columns[4].Visible = false;
                this.gvButtons.Columns[5].Visible = false;
                this.gvButtons.Columns[6].Visible = false;
                this.gvButtons.Columns[7].Visible = false;
                this.gvButtons.AllowUserToAddRows = false;
                this.gvButtons.AllowUserToResizeColumns = false;
                this.gvButtons.AllowUserToResizeRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void ChangeColumnWidth()
        {
            DataGridViewColumn column = null;
            for (int columnIndex = 0; columnIndex < gvButtons.ColumnCount; columnIndex++)
            {
                column = gvButtons.Columns[columnIndex];
                column.Width = (gvButtons.Width / 3) - 14;
            }
        }

        private void btnDeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvButtons.SelectedRows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show(@"Are you sure you want to delete selected button\s ?", "Warning", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow buttonRow in gvButtons.SelectedRows)
                        {
                            int id = (int)buttonRow.Cells["id"].Value;
                            DeleteButton(id);
                        }
                        Fillbuttons();
                        MessageBox.Show(@"Button\s have been deleted successfully", "Done");
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        MessageBox.Show(@"No button\s have been deleted", "Warning");
                    }
                }
                else
                {
                    MessageBox.Show("Please select item to delete", "Warning");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void DeleteButton(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(TSD.GlobalConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "delete from tblButtons where id = @id";
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = id;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void btnAddButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentScreen != null)
                {
                    AddEditButton addEditButton = new AddEditButton(CurrentScreen.id);
                    this.Hide();
                    addEditButton.Show();
                }
                else
                {
                    MessageBox.Show("Please save current screen before adding buttons", "Warning");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void btnEditButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvButtons.SelectedRows.Count > 0)
                {
                    if (gvButtons.SelectedRows.Count == 1)
                    {
                        int buttonId = (int)gvButtons.SelectedRows[0].Cells[0].Value;
                        AddEditButton addEditButton = new AddEditButton(CurrentScreen.id, buttonId);
                        this.Hide();
                        addEditButton.Show();
                    }
                    else
                    {
                        MessageBox.Show("Please select one item", "Warning");
                    }
                }
                else
                {
                    MessageBox.Show("Please select item to edit", "Warning");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }
    }
}
