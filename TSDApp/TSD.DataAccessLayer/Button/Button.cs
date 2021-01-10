using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TSD.DataAccessLayer.Button
{
    public static class Button
    {
        public static List<TSDApp.Models.Button> SelectButtonsbyScreenId(int pScreenId)
        {
            try
            {
                List<TSDApp.Models.Button> lstButtons = new List<TSDApp.Models.Button>();
                using (SqlConnection con = new SqlConnection(DBHelper.DBHelper.GetConnectionString()))
                {
                    SqlCommand go = new SqlCommand();
                    con.Open();
                    go.Connection = con;
                    go.CommandText = "SELECT id, ENName, ARName, Type, MessageAR, MessageEN, issueTicketType, ScreenId FROM tblButtons where ScreenId = @ScreenId";
                    go.Parameters.Add(new SqlParameter("@ScreenId", pScreenId));

                    SqlDataReader reader = go.ExecuteReader();
                    while (reader.Read())
                    {
                        lstButtons.Add(new TSDApp.Models.Button(
                        reader["id"] != null ? Convert.ToInt32(reader["id"]) : 0,
                        reader["ENName"] != null ? Convert.ToString(reader["ENName"]) : string.Empty,
                        reader["ARName"] != null ? Convert.ToString(reader["ARName"]) : string.Empty,
                        reader["Type"] != null ? Convert.ToString(reader["Type"]) : string.Empty,
                        reader["MessageAR"] != null ? Convert.ToString(reader["MessageAR"]) : string.Empty,
                        reader["MessageEN"] != null ? Convert.ToString(reader["MessageEN"]) : string.Empty,
                        reader["issueTicketType"] != DBNull.Value ? Convert.ToInt32(reader["issueTicketType"]) : 0,
                        reader["ScreenId"] != null ? Convert.ToInt32(reader["ScreenId"]) : 0));
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
        public static TSDApp.Models.Button InsertButton(TSDApp.Models.Button pButton)
        {
            try
            {
                string pquery = "insert into tblButtons OUTPUT INSERTED.IDENTITYCOL  values (@ENName,@ARName,@Type,@MessageAR,@MessageEN,@issueTicketType,@ScreenId)";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@ENName", pButton.ENName));
                ScreenParams.Add(new SqlParameter("@ARName", pButton.ARName));
                ScreenParams.Add(new SqlParameter("@Type", pButton.Type));
                ScreenParams.Add(new SqlParameter("@MessageAR", pButton.MessageAR != null ? pButton.MessageAR : (object)DBNull.Value));
                ScreenParams.Add(new SqlParameter("@MessageEN", pButton.MessageEN != null ? pButton.MessageEN : (object)DBNull.Value));
                ScreenParams.Add(new SqlParameter("@issueTicketType", pButton.issueTicketType != null ? pButton.issueTicketType : (object)DBNull.Value));
                ScreenParams.Add(new SqlParameter("@ScreenId", pButton.ScreenId));
                pButton.id = Convert.ToInt32(DBHelper.DBHelper.ExecuteScalar(pquery, ScreenParams));
                return pButton;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static TSDApp.Models.Button UpdateButton(TSDApp.Models.Button pButton)
        {
            try
            {
                string pquery = "update tblButtons set ENName = @ENName,ARName = @ARName,Type = @Type,MessageAR = @MessageAR,MessageEN = @MessageEN,issueTicketType = @issueTicketType,ScreenId = @ScreenId where id = @id";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@id", pButton.id));
                ScreenParams.Add(new SqlParameter("@ENName", pButton.ENName));
                ScreenParams.Add(new SqlParameter("@ARName", pButton.ARName));
                ScreenParams.Add(new SqlParameter("@Type", pButton.Type));
                ScreenParams.Add(new SqlParameter("@MessageAR", pButton.MessageAR != null ? pButton.MessageAR : (object)DBNull.Value));
                ScreenParams.Add(new SqlParameter("@MessageEN", pButton.MessageEN != null ? pButton.MessageEN : (object)DBNull.Value));
                ScreenParams.Add(new SqlParameter("@issueTicketType", pButton.issueTicketType));
                ScreenParams.Add(new SqlParameter("@ScreenId", pButton.ScreenId));
                DBHelper.DBHelper.ExecuteNonQuery(pquery, ScreenParams);
                return pButton;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static int DeleteButtonWhere(List<int> pButtonsIds, string ConditionColumn)
        {
            try
            {
                string pquery = "delete from tblButtons where " + ConditionColumn + " = @id";
                foreach (int item in pButtonsIds)
                {
                    List<SqlParameter> ScreenParams = new List<SqlParameter>();
                    ScreenParams.Add(new SqlParameter("@id", item));
                    DBHelper.DBHelper.ExecuteNonQuery(pquery, ScreenParams);
                }
                return 1;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return 0;
            }
        }
    }
}
