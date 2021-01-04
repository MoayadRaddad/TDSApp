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
        public static string GlobalConnectionString;
        private bool isVisited = false;
        public static Models.Bank CurrentBank;
        public TSD()
        {
            InitializeComponent();
        }
        public TSD(bool isvisited)
        {
            try
            {
                InitializeComponent();
                isVisited = isvisited;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void TSD_Load(object sender, EventArgs e)
        {
            try
            {
                if (isVisited)
                {
                    BankNamePanel.Visible = false;
                    MainPanel.Visible = true;
                    FillScreens();
                }
                else
                {
                    SetConnectionString();
                    MainPanel.Visible = false;
                }
                LoadToolTips();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void LoadToolTips()
        {
            System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip();
            toolTip1.SetToolTip(btnAddScreen, "Add new screen");
            toolTip1.SetToolTip(btnEditScreen, "Edit screen");
            toolTip1.SetToolTip(btnDeleteScreen, "Delete screen");
            toolTip1.SetToolTip(btnEnter, "Confirm");
            toolTip1.SetToolTip(btnCancel, "Close Application");
            toolTip1.SetToolTip(txtBankName, "Bank Name");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBankName.Text != "")
                {
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
                            BankNamePanel.Visible = false;
                            MainPanel.Visible = true;
                        }
                        else
                        {
                            int InsertedId = insertNewBank(txtBankName.Text);
                            CurrentBank = new Models.Bank(InsertedId, txtBankName.Text);
                            BankNamePanel.Visible = false;
                            MainPanel.Visible = true;
                        }
                        con.Close();

                        FillScreens();
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Bank Name", "Warning");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }
        public void SetConnectionString()
        {
            try
            {
                string txtpath = System.AppDomain.CurrentDomain.BaseDirectory + "ConnectionString.txt";
                //string txtpath = @"D:\ConnectionString.txt";
                string ConnectionString = "";
                if (File.Exists(txtpath))
                {
                    using (StreamReader reader = new StreamReader(txtpath))
                    {
                        if (reader.Peek() >= 0)
                        {
                            ConnectionString = reader.ReadLine();
                        }
                    }
                    GlobalConnectionString = ConnectionString;
                }
                else
                {
                    MessageBox.Show("The File for connection sting is missing or not found", "Warning");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private int insertNewBank(string bankName)
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
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = bankName;
                    LastID = (int)cmd.ExecuteScalar();
                    con.Close();
                }
                return LastID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
                return 0;
            }
        }

        private void FillScreens()
        {
            try
            {
                lblTitle.Text = "Main Form - " + CurrentBank.Name;
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
                ChangeColumnWidth();
                this.gvScreens.Columns[0].Visible = false;
                this.gvScreens.Columns[3].Visible = false;
                this.gvScreens.AllowUserToAddRows = false;
                this.gvScreens.AllowUserToResizeColumns = false;
                this.gvScreens.AllowUserToResizeRows = false;
                this.gvScreens.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void ChangeColumnWidth()
        {
            DataGridViewColumn column = null;
            for (int columnIndex = 0; columnIndex < gvScreens.ColumnCount; columnIndex++)
            {
                column = gvScreens.Columns[columnIndex];
                column.Width = (gvScreens.Width / 2) - 22;
            }
        }

        private void btnDeleteScreen_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvScreens.SelectedRows.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show(@"Are you sure you want to delete selected screen\s ?", "Warning", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow screenRow in gvScreens.SelectedRows)
                        {
                            int id = (int)screenRow.Cells["id"].Value;
                            DeleteButtons(id);
                            DeleteScreen(id);
                        }
                            FillScreens();
                            MessageBox.Show(@"Screen\s have been deleted successfully", "Done");
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        MessageBox.Show(@"No screen\s have been deleted", "Warning");
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

        private void DeleteButtons(int pScreenid)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(TSD.GlobalConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "delete from tblButtons where ScreenId = @ScreenId";
                    cmd.Parameters.Add("@ScreenId", SqlDbType.Int).Value = pScreenid;
                    cmd.ExecuteNonQuery();
                    con.Close();
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
                using (SqlConnection con = new SqlConnection(GlobalConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "delete from tblScreens where id = @id";
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void btnAddScreen_Click(object sender, EventArgs e)
        {
            try
            {
                AddEditScreen addEditScreen = new AddEditScreen(CurrentBank.id);
                this.Hide();
                addEditScreen.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void btnEditScreen_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvScreens.SelectedRows.Count > 0)
                {
                    if (gvScreens.SelectedRows.Count == 1)
                    {
                        int screenId = (int)gvScreens.SelectedRows[0].Cells[0].Value;
                        AddEditScreen addEditScreen = new AddEditScreen(CurrentBank.id, screenId);
                        this.Hide();
                        addEditScreen.Show();
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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                this.Dispose();
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }
    }
}
