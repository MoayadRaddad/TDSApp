using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TSD.DataAccessLayer.Button
{
    public static class Button
    {
        public static void DeleteButtonsByScreenId(string pquery, int pScreenId)
        {
            try
            {
                SqlParameter BankName = new SqlParameter("@ScreenId", pScreenId);
                object[] BankParams = new object[] { BankName.ParameterName, BankName.SqlValue };
                DBHelper.DBHelper.ExecuteNonQuery(pquery, BankParams);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
            }
        }
        public static DataTable SelectButtonsbyScreenId(string pquery, int pScreenId)
        {
            try
            {
                SqlParameter ScreenId = new SqlParameter("@ScreenId", pScreenId);
                object[] ScreenParams = new object[] { ScreenId.ParameterName, ScreenId.SqlValue };
                return DBHelper.DBHelper.ExecuteQuery(pquery, ScreenParams);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static void DeleteButtonsByIds(string pquery, List<int> pButtonsIds)
        {
            try
            {
                foreach (int item in pButtonsIds)
                {
                    SqlParameter ButtonId = new SqlParameter("@id", item);
                    object[] ButtonParams = new object[] { ButtonId.ParameterName, ButtonId.SqlValue };
                    DBHelper.DBHelper.ExecuteNonQuery(pquery, ButtonParams);
                }
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
            }
        }
        public static TSDApp.Models.Button SelectButtonById(string pquery, int pButtonId)
        {
            try
            {
                SqlParameter ButtonId = new SqlParameter("@id", pButtonId);
                object[] ScreenParams = new object[] { ButtonId.ParameterName, ButtonId.SqlValue };
                DataTable ButtonTable = DBHelper.DBHelper.ExecuteQuery(pquery, ScreenParams);
                TSDApp.Models.Button CurrentButton = new TSDApp.Models.Button();
                CurrentButton.id = Convert.ToInt32(ButtonTable.Rows[0]["id"]);
                CurrentButton.ARName = Convert.ToString(ButtonTable.Rows[0]["ARName"]);
                CurrentButton.ENName = Convert.ToString(ButtonTable.Rows[0]["ENName"]);
                CurrentButton.Type = Convert.ToString(ButtonTable.Rows[0]["Type"]);
                CurrentButton.MessageAR = Convert.ToString(ButtonTable.Rows[0]["MessageAR"]);
                CurrentButton.MessageEN = Convert.ToString(ButtonTable.Rows[0]["MessageEN"]);
                CurrentButton.issueType = Convert.ToString(ButtonTable.Rows[0]["issueType"]);
                CurrentButton.ScreenId = Convert.ToInt32(ButtonTable.Rows[0]["ScreenId"]);
                return CurrentButton;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static TSDApp.Models.Button InsertButton(string pquery, TSDApp.Models.Button pButton)
        {
            try
            {
                SqlParameter ENName = new SqlParameter("@ENName", pButton.ENName);
                SqlParameter ARName = new SqlParameter("@ARName", pButton.ARName);
                SqlParameter Type = new SqlParameter("@Type", pButton.Type);
                SqlParameter MessageAR = new SqlParameter("@MessageAR", pButton.MessageAR != null ? pButton.MessageAR : (object)DBNull.Value);
                SqlParameter MessageEN = new SqlParameter("@MessageEN", pButton.MessageEN != null ? pButton.MessageEN : (object)DBNull.Value);
                SqlParameter issueType = new SqlParameter("@issueType", pButton.issueType != null ? pButton.issueType : (object)DBNull.Value);
                SqlParameter ScreenBankId = new SqlParameter("@ScreenId", pButton.ScreenId);
                object[] ScreenParams = new object[] { ENName.ParameterName, ENName.SqlValue, ARName.ParameterName, ARName.SqlValue,
                                                       Type.ParameterName, Type.SqlValue, MessageAR.ParameterName, MessageAR.SqlValue,
                                                       MessageEN.ParameterName, MessageEN.SqlValue, issueType.ParameterName, issueType.SqlValue,
                                                       ScreenBankId.ParameterName, ScreenBankId.SqlValue};
                pButton.id = Convert.ToInt32(DBHelper.DBHelper.ExecuteScalar(pquery, ScreenParams));
                return pButton;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static TSDApp.Models.Button UpdateButton(string pquery, TSDApp.Models.Button pButton)
        {
            try
            {
                SqlParameter id = new SqlParameter("@id", pButton.id);
                SqlParameter ENName = new SqlParameter("@ENName", pButton.ENName);
                SqlParameter ARName = new SqlParameter("@ARName", pButton.ARName);
                SqlParameter Type = new SqlParameter("@Type", pButton.Type);
                SqlParameter MessageAR = new SqlParameter("@MessageAR", pButton.MessageAR != null ? pButton.MessageAR : (object)DBNull.Value);
                SqlParameter MessageEN = new SqlParameter("@MessageEN", pButton.MessageEN != null ? pButton.MessageEN : (object)DBNull.Value);
                SqlParameter issueType = new SqlParameter("@issueType", pButton.issueType != null ? pButton.issueType : (object)DBNull.Value);
                SqlParameter ScreenBankId = new SqlParameter("@ScreenId", pButton.ScreenId);
                object[] ScreenParams = new object[] { ENName.ParameterName, ENName.SqlValue, ARName.ParameterName, ARName.SqlValue,
                                                       Type.ParameterName, Type.SqlValue, MessageAR.ParameterName, MessageAR.SqlValue,
                                                       MessageEN.ParameterName, MessageEN.SqlValue, issueType.ParameterName, issueType.SqlValue,
                                                       ScreenBankId.ParameterName, ScreenBankId.SqlValue, id.ParameterName, id.SqlValue };
                DBHelper.DBHelper.ExecuteNonQuery(pquery, ScreenParams);
                return pButton;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
    }
}
