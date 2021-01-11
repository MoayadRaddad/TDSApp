using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TSD.DataAccessLayer.Button
{
    public static class Button
    {
        public static List<T> SelectButtonsbyScreenId<T>(int pScreenId, BusinessObjects.Models.btnType btnType)
        {
            try
            {
                List<T> lstButtons = new List<T>();
                using (SqlConnection con = new SqlConnection(DBHelper.DBHelper.GetConnectionString()))
                {
                    SqlCommand go = new SqlCommand();
                    con.Open();
                    go.Connection = con;
                    go.CommandText = "SELECT * FROM tbl" + btnType.ToString() + " where ScreenId = @ScreenId";
                    go.Parameters.Add(new SqlParameter("@ScreenId", pScreenId));

                    SqlDataReader reader = go.ExecuteReader();
                    while (reader.Read())
                    {
                        if (btnType == BusinessObjects.Models.btnType.ShowMessage)
                        {
                            var btn = new BusinessObjects.Models.ShowMessage(Convert.ToInt32(reader["id"]), reader["ENName"].ToString(), reader["ARName"].ToString(),
                                reader["MessageAR"].ToString(), reader["MessageEN"].ToString(), Convert.ToInt32(reader["ScreenId"]));
                            lstButtons.Add((T)(object)btn);
                        }
                        else
                        {
                            var btn = new BusinessObjects.Models.IssueTicket(Convert.ToInt32(reader["id"]), reader["ENName"].ToString(), reader["ARName"].ToString(),
                                Convert.ToInt32(reader["SreviceType"]), Convert.ToInt32(reader["ScreenId"]));
                            lstButtons.Add((T)(object)btn);
                        }
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
        public static BusinessObjects.Models.ShowMessage InsertShowMessageButton(BusinessObjects.Models.ShowMessage pButton)
        {
            try
            {
                string pquery = "insert into tblShowMessage OUTPUT INSERTED.IDENTITYCOL  values (@ENName,@ARName,@MessageEN,@MessageAR,@ScreenId)";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@ENName", pButton.ENName));
                ScreenParams.Add(new SqlParameter("@ARName", pButton.ARName));
                ScreenParams.Add(new SqlParameter("@Type", pButton.Type));
                ScreenParams.Add(new SqlParameter("@MessageEN", pButton.MessageEN));
                ScreenParams.Add(new SqlParameter("@MessageAR", pButton.MessageAR));
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
        public static BusinessObjects.Models.ShowMessage UpdateShowMessageButton(BusinessObjects.Models.ShowMessage pButton)
        {
            try
            {
                string pquery = "update tblShowMessage set ENName = @ENName,ARName = @ARName,Type = @Type,MessageAR = @MessageAR,MessageEN = @MessageEN,issueTicketType = @issueTicketType where id = @id";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@id", pButton.id));
                ScreenParams.Add(new SqlParameter("@ENName", pButton.ENName));
                ScreenParams.Add(new SqlParameter("@ARName", pButton.ARName));
                ScreenParams.Add(new SqlParameter("@Type", pButton.Type));
                ScreenParams.Add(new SqlParameter("@MessageEN", pButton.MessageEN));
                ScreenParams.Add(new SqlParameter("@MessageAR", pButton.MessageAR));
                DBHelper.DBHelper.ExecuteNonQuery(pquery, ScreenParams);
                return pButton;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static BusinessObjects.Models.IssueTicket InsertIssueTicketButton(BusinessObjects.Models.IssueTicket pButton)
        {
            try
            {
                string pquery = "insert into tblIssueTicket OUTPUT INSERTED.IDENTITYCOL  values (@ENName,@ARName,@ServiceType,@ScreenId)";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@ENName", pButton.ENName));
                ScreenParams.Add(new SqlParameter("@ARName", pButton.ARName));
                ScreenParams.Add(new SqlParameter("@ServiceType", pButton.Type));
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
        public static BusinessObjects.Models.IssueTicket UpdateIssueTicketButton(BusinessObjects.Models.IssueTicket pButton)
        {
            try
            {
                string pquery = "update tblIssueTicket set ENName = @ENName,ARName = @ARName,ServiceType = @ServiceType where id = @id";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@id", pButton.id));
                ScreenParams.Add(new SqlParameter("@ENName", pButton.ENName));
                ScreenParams.Add(new SqlParameter("@ARName", pButton.ARName));
                ScreenParams.Add(new SqlParameter("@ServiceType", pButton.Type));
                DBHelper.DBHelper.ExecuteNonQuery(pquery, ScreenParams);
                return pButton;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static int DeleteButtonWhere(IDictionary<int, string> pButtonsIds, string ConditionColumn)
        {
            try
            {
                foreach (KeyValuePair<int, string> item in pButtonsIds)
                {
                    string pquery = string.Empty;
                    pquery = "delete from tbl" + item.Value.ToString() + " where " + ConditionColumn + " = @id";
                    List<SqlParameter> ScreenParams = new List<SqlParameter>();
                    ScreenParams.Add(new SqlParameter("@id", item.Key));
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
        public static int DeleteAllButtonByScreenId(int pScreenId)
        {
            try
            {
                string pquery = "delete from tblShowMessage where ScreenId = @ScreenId";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@ScreenId", pScreenId));
                DBHelper.DBHelper.ExecuteNonQuery(pquery, ScreenParams);
                pquery = "delete from tblIssueTicket where ScreenId = @ScreenId";
                ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@ScreenId", pScreenId));
                DBHelper.DBHelper.ExecuteNonQuery(pquery, ScreenParams);
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
