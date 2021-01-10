using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TSD.DataAccessLayer.Screen
{
    public static class Screen
    {
        public static List<TSDApp.Models.Screen> SelectScreensByBankId(TSDApp.Models.Bank pBank)
        {
            try
            {
                List<TSDApp.Models.Screen> lstButtons = new List<TSDApp.Models.Screen>();
                using (SqlConnection con = new SqlConnection(DBHelper.DBHelper.GetConnectionString()))
                {
                    SqlCommand go = new SqlCommand();
                    con.Open();
                    go.Connection = con;
                    go.CommandText = "SELECT id,name,isActive,BankId FROM tblScreens where BankId = @BankId";
                    go.Parameters.Add(new SqlParameter("@BankId", pBank.id));

                    SqlDataReader reader = go.ExecuteReader();
                    while (reader.Read())
                    {
                        lstButtons.Add(new TSDApp.Models.Screen(
                            reader["id"] != null ? Convert.ToInt32(reader["id"]) : 0,
                            reader["Name"] != null ? Convert.ToString(reader["Name"]) : string.Empty,
                            reader["isActive"] != null ? Convert.ToBoolean(reader["isActive"]) : false,
                            reader["BankId"] != null ? Convert.ToInt32(reader["BankId"]) : 0));
                    }
                    con.Close();
                }
                return lstButtons;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static int DeleteScreenById(int pScreenId)
        {
            try
            {
                string pquery = "delete from tblScreens where id = @id";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@id", pScreenId));
                DBHelper.DBHelper.ExecuteNonQuery(pquery, ScreenParams);
                return 1;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return 0;
            }
        }
        public static TSDApp.Models.Screen InsertScreen(TSDApp.Models.Screen pScreen)
        {
            try
            {
                string pquery = "insert into tblScreens OUTPUT INSERTED.IDENTITYCOL  values (@Name,@isActive,@BankId)";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@Name", pScreen.Name));
                ScreenParams.Add(new SqlParameter("@isActive", pScreen.isActive));
                ScreenParams.Add(new SqlParameter("@BankId", pScreen.BankId));
                pScreen.id = Convert.ToInt32(DBHelper.DBHelper.ExecuteScalar(pquery, ScreenParams));
                if (pScreen.isActive)
                {
                    UpdateActiveScreen(pScreen.id);
                }
                return pScreen;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static TSDApp.Models.Screen UpdateScreen(TSDApp.Models.Screen pScreen)
        {
            try
            {
                string pquery = "update tblScreens set name = @Name,isActive = @isActive where id = @id";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@Name", pScreen.Name));
                ScreenParams.Add(new SqlParameter("@isActive", pScreen.isActive));
                ScreenParams.Add(new SqlParameter("@id", pScreen.id));
                DBHelper.DBHelper.ExecuteNonQuery(pquery, ScreenParams);
                if (pScreen.isActive)
                {
                    UpdateActiveScreen(pScreen.id);
                }
                return pScreen;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static void UpdateActiveScreen(int pScreenId)
        {
            try
            {
                string pquery = "update tblScreens set isActive = 0 where id != @id";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@id", pScreenId));
                DBHelper.DBHelper.ExecuteScalar(pquery, ScreenParams);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
            }
        }
    }
}
