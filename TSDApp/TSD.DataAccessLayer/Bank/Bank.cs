using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TSD.DataAccessLayer.Bank
{
    public static class Bank
    {
        public static TSDApp.Models.Bank CheckBankExist(TSDApp.Models.Bank pBank)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DBHelper.DBHelper.GetConnectionString()))
                {
                    SqlCommand go = new SqlCommand();
                    con.Open();
                    go.Connection = con;
                    go.CommandText = "SELECT id,Name FROM tblBanks WHERE Name = @Name";
                    go.Parameters.Add(new SqlParameter("@Name", pBank.Name));

                    SqlDataReader reader = go.ExecuteReader();
                    if (reader.Read())
                    {
                        pBank.id = Convert.ToInt32(reader["id"].ToString());
                    }
                    con.Close();
                }
                return pBank;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static TSDApp.Models.Bank InsertBank(TSDApp.Models.Bank pBank)
        {
            try
            {
                string pquery = "insert into tblBanks OUTPUT INSERTED.IDENTITYCOL  values (@Name)";
                List<SqlParameter> BankParams = new List<SqlParameter>();
                BankParams.Add(new SqlParameter("@Name", pBank.Name));
                pBank.id = Convert.ToInt32(DBHelper.DBHelper.ExecuteScalar(pquery, BankParams));
                return pBank;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
    }
}
