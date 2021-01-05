using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TSDApp.Fomrs;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TSDApp
{
    public partial class TSD : Form
    {
        //public variable to access connection string from all forms
        public static string GlobalConnectionString;
        //Variable to check if is it the first time to show bank select panel
        private bool isVisited = false;
        //Object to know currently bank
        public static Models.Bank CurrentBank;
        //Default constructor
        public TSD()
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
        /*Parametarize constructor:
          Fill isVisited to know if the user already choose a bank*/
        public TSD(bool pIsVisited)
        {
            try
            {
                InitializeComponent();
                isVisited = pIsVisited;
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        private void TSD_Load(object sender, EventArgs e)
        {
            try
            {
                /*Check isViseted :
                 If true : show main panel and fill screens
                 If false : show bank panel and get connection string*/
                if (isVisited)
                {
                    /*Parametarize constructor:
                      Fill isVisited to know if the user already choose a bank*/
                    BankNamePanel.Visible = false;
                    MainPanel.Visible = true;
                    FillScreens();
                }
                else
                {
                    GlobalConnectionString = Models.SharingMethods.SetConnectionString();
                    MainPanel.Visible = false;
                }
                //Function to fill tooltips
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
                toolTip1.SetToolTip(btnAddScreen, "Add new screen");
                toolTip1.SetToolTip(btnEditScreen, "Edit screen");
                toolTip1.SetToolTip(btnDeleteScreen, "Delete screen");
                toolTip1.SetToolTip(btnEnter, "Confirm");
                toolTip1.SetToolTip(btnCancel, "Close Application");
                toolTip1.SetToolTip(txtBankName, "Bank Name");
                throw new IndexOutOfRangeException("Error");
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        //Cancel button to exit the Application
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        //Enter button to check if bank is exsit or not and get screens if exist
        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                //Validation on bank name to check if empty or not
                if (txtBankName.Text != "")
                {
                    /*Check bank name :
                      *If bank name is exist :
                       1 - fill CurrentBank object
                       2 - Show main panel
                       3 - fill grid view with screens if exist
                      *If bank name not exist :
                       1 - Insert new bank to database
                       2 - Show main panel*/

                    using (SqlConnection con = new SqlConnection(GlobalConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand();

                        con.Open();
                        cmd.Connection = con;
                        cmd.CommandText = "SELECT id,Name FROM tblBanks WHERE Name = @Name";
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = txtBankName.Text;

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            CurrentBank = new Models.Bank(Convert.ToInt32(reader["id"]), reader["Name"].ToString());
                            FillScreens();
                        }
                        else
                        {
                            int InsertedId = insertNewBank(txtBankName.Text);
                            CurrentBank = new Models.Bank(InsertedId, txtBankName.Text);
                        }
                        BankNamePanel.Visible = false;
                        MainPanel.Visible = true;
                        con.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Bank Name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        //Private function to insert new bank and return id inserted to fill Currentbank
        private int insertNewBank(string pBankName)
        {
            try
            {
                int LastID;
                using (SqlConnection con = new SqlConnection(GlobalConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "insert into tblBanks OUTPUT INSERTED.IDENTITYCOL  values (@Name)";
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = pBankName;
                    LastID = (int)cmd.ExecuteScalar();
                    con.Close();
                }
                return LastID;
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
                return 0;
            }
        }
        //Function to get screens for current bank and fill Title
        private void FillScreens()
        {
            try
            {
                lblTitle.Text = "Main Form - " + CurrentBank.Name;
                lblGVTitle.Text = "Bank " + CurrentBank.Name + " screens";
                using (SqlConnection con = new SqlConnection(GlobalConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = con;
                    cmd.CommandText = "SELECT id,name,isActive,BankId FROM tblScreens where BankId = @BankId";
                    cmd.Parameters.Add("@BankId", SqlDbType.Int).Value = CurrentBank.id;
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    List<Models.Screen> lstScreens = new List<Models.Screen>();
                    while (reader.Read())
                    {
                        lstScreens.Add(new Models.Screen((int)reader["id"], (string)reader["Name"], (bool)reader["isActive"] == true ? "Activated" : "Deactivated", (int)reader["BankId"]));
                    }
                    con.Close();
                    gvScreens.DataSource = lstScreens;
                }
                //Resize columns width
                ChangeColumnWidth();
                /*Hide not usefull columns from grid view
                  Deny user from resize the grid view(columns & rows)
                  Deny user to change values on the grid view cells*/
                this.gvScreens.Columns[0].Visible = false;
                this.gvScreens.Columns[3].Visible = false;
                this.gvScreens.AllowUserToAddRows = false;
                this.gvScreens.AllowUserToResizeColumns = false;
                this.gvScreens.AllowUserToResizeRows = false;
                this.gvScreens.ReadOnly = true;
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        //Set column width
        private void ChangeColumnWidth()
        {
            try
            {
                DataGridViewColumn column = null;
                for (int columnIndex = 0; columnIndex < gvScreens.ColumnCount; columnIndex++)
                {
                    column = gvScreens.Columns[columnIndex];
                    column.Width = (gvScreens.Width / 2) - 22;
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /*Delete button :
          Delete selected row/s(screen/s)
          Delete buttons for the deleted screen/s*/
        private void btnDeleteScreen_Click(object sender, EventArgs e)
        {
            try
            {
                //check if user select row/s to delete
                if (gvScreens.SelectedRows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show(@"Are you sure you want to delete selected screen\s ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow screenRow in gvScreens.SelectedRows)
                        {
                            int id = (int)screenRow.Cells["id"].Value;
                            //Delete buttons for the deleted screen
                            DeleteButtons(id);
                            //Delete screen whitch is selected
                            DeleteScreen(id);
                        }
                        //Refresh grid view
                        FillScreens();
                        MessageBox.Show(@"Screen\s have been deleted successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        MessageBox.Show(@"No screen\s have been deleted", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        //Function take a screen id and delete all button for it from database
        private void DeleteButtons(int pScreenId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(TSD.GlobalConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "delete from tblButtons where ScreenId = @ScreenId";
                    cmd.Parameters.Add("@ScreenId", SqlDbType.Int).Value = pScreenId;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        //Function take a screen id and delete it from database
        private void DeleteScreen(int pScreenId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "delete from tblScreens where id = @id";
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = pScreenId;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        //Add screen button to redirect to add/edit screen form to add new screen
        private void btnAddScreen_Click(object sender, EventArgs e)
        {
            try
            {
                //Send current bank id to use it in add/edit screen form
                AddEditScreen addEditScreen = new AddEditScreen(CurrentBank.id);
                this.Hide();
                addEditScreen.Show();
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        //Edit screen button to redirect to add/edit screen form to edit selected screen
        private void btnEditScreen_Click(object sender, EventArgs e)
        {
            try
            {
                //Check if user select row
                if (gvScreens.SelectedRows.Count > 0)
                {
                    //Check if user select only one row
                    if (gvScreens.SelectedRows.Count == 1)
                    {
                        int screenId = (int)gvScreens.SelectedRows[0].Cells[0].Value;
                        //Send current bank id and selected screen id to use it in add/edit screen form
                        AddEditScreen addEditScreen = new AddEditScreen(CurrentBank.id, screenId);
                        this.Hide();
                        addEditScreen.Show();
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
        //Override OnFormClosing method to dispose used form/s and exit the application
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                this.Dispose();
                Application.Exit();
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
    }
}
