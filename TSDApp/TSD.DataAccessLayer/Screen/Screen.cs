using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TSD.DataAccessLayer.Screen
{
    public static class Screen
    {
        public static DataTable SelectScreensByBankId(TSDApp.Models.Bank pBank)
        {
            try
            {
                string pquery = "SELECT id,name,isActive,BankId FROM tblScreens where BankId = @BankId";
                SqlParameter BankName = new SqlParameter("@BankId", pBank.id);
                object[] BankParams = new object[] { BankName.ParameterName, BankName.SqlValue };
                return DBHelper.DBHelper.ExecuteQuery(pquery, BankParams);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static void DeleteScreenById(int pScreenId)
        {
            try
            {
                string pquery = "delete from tblScreens where id = @id";
                SqlParameter BankName = new SqlParameter("@id", pScreenId);
                object[] BankParams = new object[] { BankName.ParameterName, BankName.SqlValue };
                DBHelper.DBHelper.ExecuteNonQuery(pquery, BankParams);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
            }
        }
        public static TSDApp.Models.Screen SelectScreenbyId(string pquery, int pScreenId)
        {
            try
            {
                SqlParameter ScreenId = new SqlParameter("@id", pScreenId);
                object[] ScreenParams = new object[] { ScreenId.ParameterName, ScreenId.SqlValue };
                DataTable ScreenTable = DBHelper.DBHelper.ExecuteQuery(pquery, ScreenParams);
                TSDApp.Models.Screen CurrentScreen = new TSDApp.Models.Screen();
                CurrentScreen.id = Convert.ToInt32(ScreenTable.Rows[0]["id"]);
                CurrentScreen.Name = Convert.ToString(ScreenTable.Rows[0]["Name"]);
                if (Convert.ToBoolean(ScreenTable.Rows[0]["isActive"]))
                {
                    CurrentScreen.isActive = "Activated";
                }
                else
                {
                    CurrentScreen.isActive = "Deactivated";
                }
                CurrentScreen.BankId = Convert.ToInt32(ScreenTable.Rows[0]["BankId"]);
                return CurrentScreen;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static TSDApp.Models.Screen InsertScreen(TSDApp.Models.Screen pScreen)
        {
            try
            {
                string pquery = "insert into tblScreens OUTPUT INSERTED.IDENTITYCOL  values (@Name,@isActive,@BankId)";
                SqlParameter ScreenName = new SqlParameter("@Name", pScreen.Name);
                SqlParameter ScreenIsActive = new SqlParameter("@isActive", pScreen.isActive);
                SqlParameter ScreenBankId = new SqlParameter("@BankId", pScreen.BankId);
                object[] ScreenParams = new object[] { ScreenName.ParameterName, ScreenName.SqlValue, ScreenIsActive.ParameterName, ScreenIsActive.SqlValue, ScreenBankId.ParameterName, ScreenBankId.SqlValue };
                pScreen.id = Convert.ToInt32(DBHelper.DBHelper.ExecuteScalar(pquery, ScreenParams));
                if (pScreen.isActive == "True")
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
                SqlParameter ScreenName = new SqlParameter("@Name", pScreen.Name);
                SqlParameter ScreenIsActive = new SqlParameter("@isActive", pScreen.isActive);
                SqlParameter ScreenId = new SqlParameter("@id", pScreen.id);
                object[] ScreenParams = new object[] { ScreenName.ParameterName, ScreenName.SqlValue, ScreenIsActive.ParameterName, ScreenIsActive.Value, ScreenId.ParameterName, ScreenId.Value};
                DBHelper.DBHelper.ExecuteNonQuery(pquery, ScreenParams);
                if (pScreen.isActive == "True")
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
                SqlParameter ScreenId = new SqlParameter("@id", pScreenId);
                object[] ScreenParams = new object[] { ScreenId.ParameterName, ScreenId.SqlValue };
                DBHelper.DBHelper.ExecuteScalar(pquery, ScreenParams);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
            }
        }
    }
}
