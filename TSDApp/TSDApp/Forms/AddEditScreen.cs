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
        //Variable to define selected bank id
        private int BankId;
        //Object to know currently screen
        private static Models.Screen CurrentScreen;
        //Default constructor
        public AddEditScreen()
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
        /*Parametarize constructor (if new screen) :
          Fill bankId to know the currently bank id
          Set combobox to first index*/
        public AddEditScreen(int pId)
        {
            try
            {
                InitializeComponent();
                BankId = pId;
                ddlActive.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /*Parametarize constructor (if edit screen) :
          Fill bankId to know the currently bank id
          Fill data for current screen to this form
          Fill grid view with buttons for current screen id exist*/
        public AddEditScreen(int pBankId, int pScreenId)
        {
            try
            {
                InitializeComponent();
                BankId = pBankId;
                FillScreenData(pScreenId);
                Fillbuttons();
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        private void AddEditScreen_Load(object sender, EventArgs e)
        {
            try
            {
                //Call LoadToolTips to fill tooltips
                LoadToolTips();
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        //Function to fill tooltips
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
        //Save button to insert or update current screen
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Check if name is filled or not
                if (txtName.Text != "")
                {
                    //Check if combobox active is selected or not
                    if (ddlActive.SelectedIndex != 0)
                    {
                        //Check if it is new screen to insert or edit screen to update
                        if (CurrentScreen == null)
                        {
                            insertNewScreen(txtName.Text, Convert.ToBoolean(ddlActive.SelectedItem));
                            MessageBox.Show("Screen has been saved successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (gvButtons.RowCount > 0)
                            {
                                UpdateScreen(txtName.Text, Convert.ToBoolean(ddlActive.SelectedItem));
                                CurrentScreen = null;
                                MessageBox.Show("Screen has been updated successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        //Function to delete current screen
        private void DeleteScreen(int pId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(TSD.GlobalConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "delete from tblScreens where id = @id";
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = pId;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        //Function to update screens table to set all isActive value to false if current screen is active when new screen added
        private void insertActiveScreen(int pLastID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(TSD.GlobalConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "update tblScreens set isActive = 0 where id != @id";
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = pLastID.ToString();
                    cmd.ExecuteScalar();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        //Function to insert new screen
        private void insertNewScreen(string pScreenName, bool pIsActive)
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
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = pScreenName;
                    cmd.Parameters.Add("@isActive", SqlDbType.Bit).Value = pIsActive;
                    cmd.Parameters.Add("@BankId", SqlDbType.NVarChar).Value = BankId;
                    LastID = (int)cmd.ExecuteScalar();
                    con.Close();
                    CurrentScreen = new Models.Screen(LastID, pScreenName, pIsActive == true ? "Activated" : "Deactivated", BankId);
                    lblGVTitle.Text = CurrentScreen.Name + " - Buttons";
                    lblTitle.Text = "Edit Screen";
                    btnSave.Text = "Edit";
                }
                if (ddlActive.SelectedIndex == 1)
                {
                    insertActiveScreen(LastID);
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        //Function to update current screen
        private void UpdateScreen(string pScreenName, bool pIsActive)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(TSD.GlobalConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "update tblScreens set name = @Name,isActive = @isActive where id = @id";
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = pScreenName;
                    cmd.Parameters.Add("@isActive", SqlDbType.Bit).Value = pIsActive;
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
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        //Cancel button to check if screen have buttons or not if not then delete screen
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                TSD tsd = null;
                //check if current screen have buttons
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
                        DialogResult dialogResult = MessageBox.Show(@"The Screen will be discard because there is no button\s added to this screen, are you sure you want to continue ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        //Function to fill name and active for current screen 
        private void FillScreenData(int pScreenId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(TSD.GlobalConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "SELECT id,name,isActive,BankId FROM tblScreens where id = @id";
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = pScreenId;

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        CurrentScreen = new Models.Screen(Convert.ToInt32(reader["id"]), reader["name"].ToString(), (bool)reader["isActive"] == true ? "Activated" : "Deactivated", (int)reader["BankId"]);
                        txtName.Text = CurrentScreen.Name;
                        lblGVTitle.Text = CurrentScreen.Name + " - Buttons";
                        lblTitle.Text = "Edit Screen";
                        btnSave.Text = "Edit";
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
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /*Override OnFormClosing method to dispose current form, check if screen have buttons or not if not then delete screen
          and move to the previous form*/
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                if (gvButtons.RowCount == 0)
                {
                    if (CurrentScreen != null)
                    {
                        DialogResult dialogResult = MessageBox.Show(@"The Screen will be discard because there is no button\s added to this screen.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        //Function to fill grid view with buttons for current screen
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
                //Resize columns width
                ChangeColumnWidth();
                /*Hide not usefull columns from grid view
                  Deny user from resize the grid view(columns & rows)
                  Deny user to change values on the grid view cells*/
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
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        //Set column width
        private void ChangeColumnWidth()
        {
            DataGridViewColumn column = null;
            for (int columnIndex = 0; columnIndex < gvButtons.ColumnCount; columnIndex++)
            {
                column = gvButtons.Columns[columnIndex];
                column.Width = (gvButtons.Width / 3) - 14;
            }
        }
        /*Delete button :
          Delete selected row/s(button/s)
          Delete buttons for the deleted button/s*/
        private void btnDeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvButtons.SelectedRows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show(@"Are you sure you want to delete selected button\s ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow buttonRow in gvButtons.SelectedRows)
                        {
                            int id = (int)buttonRow.Cells["id"].Value;
                            //Call delete function to delete button
                            DeleteButton(id);
                        }
                        //Refresh grid view
                        Fillbuttons();
                        MessageBox.Show(@"Button\s have been deleted successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        //Function to delete button from database
        private void DeleteButton(int pId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(TSD.GlobalConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "delete from tblButtons where id = @id";
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = pId;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        //Add button to redirect to add/edit button form to add new button
        private void btnAddButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentScreen != null)
                {
                    //Send current screen id to use it in add/edit button form
                    AddEditButton addEditButton = new AddEditButton(CurrentScreen.id);
                    this.Hide();
                    addEditButton.Show();
                }
                else
                {
                    MessageBox.Show("Please save current screen before adding buttons", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        //Edit button to redirect to add/edit button form to edit selected button
        private void btnEditButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Check if user select row
                if (gvButtons.SelectedRows.Count > 0)
                {
                    //Check if user select only one row
                    if (gvButtons.SelectedRows.Count == 1)
                    {
                        int buttonId = (int)gvButtons.SelectedRows[0].Cells[0].Value;
                        //Send current screen id and selected button id to use it in add/edit button form
                        AddEditButton addEditButton = new AddEditButton(CurrentScreen.id, buttonId);
                        this.Hide();
                        addEditButton.Show();
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
    }
}
