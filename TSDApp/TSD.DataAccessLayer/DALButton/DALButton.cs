using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TSD.DataAccessLayer.Button
{
    public class DALButton
    {
        DBHelper.DALDBHelper dBHelper;
        public List<T> SelectButtonsbyScreenId<T>(int pScreenId, BusinessObjects.Models.btnType btnType)
        {
            try
            {
                List<T> lstButtons = new List<T>();
                string pquery = "SELECT * FROM tbl" + btnType.ToString() + " where ScreenId = @ScreenId";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@ScreenId", pScreenId));
                dBHelper = new DBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.ExecuteAdapter(pquery, ScreenParams);
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    if (btnType == BusinessObjects.Models.btnType.ShowMessage)
                    {
                        var btn = new BusinessObjects.Models.ShowMessageButton(Convert.ToInt32(dataRow["id"]), dataRow["ENName"].ToString(), dataRow["ARName"].ToString(),
                            dataRow["MessageAR"].ToString(), dataRow["MessageEN"].ToString(), Convert.ToInt32(dataRow["ScreenId"]));
                        lstButtons.Add((T)(object)btn);
                    }
                    else
                    {
                        var btn = new BusinessObjects.Models.IssueTicketButton(Convert.ToInt32(dataRow["id"]), dataRow["ENName"].ToString(), dataRow["ARName"].ToString(),
                            Convert.ToInt32(dataRow["ServiceId"]), Convert.ToInt32(dataRow["ScreenId"]));
                        lstButtons.Add((T)(object)btn);
                    }
                }
                return lstButtons;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessObjects.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.ShowMessageButton InsertShowMessageButton(BusinessObjects.Models.ShowMessageButton pButton)
        {
            try
            {
                string pquery = "insert into tblShowMessage OUTPUT INSERTED.IDENTITYCOL  values (@ENName,@ARName,@MessageEN,@MessageAR,@ScreenId)";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@ENName", pButton.enName));
                ScreenParams.Add(new SqlParameter("@ARName", pButton.arName));
                ScreenParams.Add(new SqlParameter("@Type", pButton.type));
                ScreenParams.Add(new SqlParameter("@MessageEN", pButton.messageEN));
                ScreenParams.Add(new SqlParameter("@MessageAR", pButton.messageAR));
                ScreenParams.Add(new SqlParameter("@ScreenId", pButton.screenId));
                dBHelper = new DBHelper.DALDBHelper();
                pButton.id = Convert.ToInt32(dBHelper.ExecuteScalar(pquery, ScreenParams));
                return pButton;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessObjects.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.ShowMessageButton UpdateShowMessageButton(BusinessObjects.Models.ShowMessageButton pButton)
        {
            try
            {
                string pquery = "update tblShowMessage set ENName = @ENName,ARName = @ARName,Type = @Type,MessageAR = @MessageAR,MessageEN = @MessageEN,issueTicketType = @issueTicketType where id = @id";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@id", pButton.id));
                ScreenParams.Add(new SqlParameter("@ENName", pButton.enName));
                ScreenParams.Add(new SqlParameter("@ARName", pButton.arName));
                ScreenParams.Add(new SqlParameter("@Type", pButton.type));
                ScreenParams.Add(new SqlParameter("@MessageEN", pButton.messageEN));
                ScreenParams.Add(new SqlParameter("@MessageAR", pButton.messageAR));
                dBHelper = new DBHelper.DALDBHelper();
                dBHelper.ExecuteNonQuery(pquery, ScreenParams);
                return pButton;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessObjects.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.IssueTicketButton InsertIssueTicketButton(BusinessObjects.Models.IssueTicketButton pButton)
        {
            try
            {
                string pquery = "insert into tblIssueTicket OUTPUT INSERTED.IDENTITYCOL  values (@ENName,@ARName,@ServiceId,@ScreenId)";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@ENName", pButton.enName));
                ScreenParams.Add(new SqlParameter("@ARName", pButton.arName));
                ScreenParams.Add(new SqlParameter("@ServiceId", pButton.serviceId));
                ScreenParams.Add(new SqlParameter("@ScreenId", pButton.screenId));
                dBHelper = new DBHelper.DALDBHelper();
                pButton.id = Convert.ToInt32(dBHelper.ExecuteScalar(pquery, ScreenParams));
                return pButton;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessObjects.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.IssueTicketButton UpdateIssueTicketButton(BusinessObjects.Models.IssueTicketButton pButton)
        {
            try
            {
                string pquery = "update tblIssueTicket set ENName = @ENName,ARName = @ARName,ServiceId = @ServiceId where id = @id";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@id", pButton.id));
                ScreenParams.Add(new SqlParameter("@ENName", pButton.enName));
                ScreenParams.Add(new SqlParameter("@ARName", pButton.arName));
                ScreenParams.Add(new SqlParameter("@ServiceId", pButton.type));
                dBHelper = new DBHelper.DALDBHelper();
                dBHelper.ExecuteNonQuery(pquery, ScreenParams);
                return pButton;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessObjects.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public int DeleteButtonWhere(IDictionary<int, string> pButtonsIds, string ConditionColumn)
        {
            try
            {
                foreach (KeyValuePair<int, string> item in pButtonsIds)
                {
                    string pquery = string.Empty;
                    pquery = "delete from tbl" + item.Value.ToString() + " where " + ConditionColumn + " = @id";
                    List<SqlParameter> ScreenParams = new List<SqlParameter>();
                    ScreenParams.Add(new SqlParameter("@id", item.Key));
                    dBHelper = new DBHelper.DALDBHelper();
                    dBHelper.ExecuteNonQuery(pquery, ScreenParams);
                }
                return 1;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessObjects.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return 0;
            }
        }
        public int DeleteAllButtonByScreenId(int pScreenId)
        {
            try
            {
                string pquery = "delete from tblShowMessage where ScreenId = @ScreenId";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@ScreenId", pScreenId));
                dBHelper.ExecuteNonQuery(pquery, ScreenParams);
                pquery = "delete from tblIssueTicket where ScreenId = @ScreenId";
                ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@ScreenId", pScreenId));
                dBHelper = new DBHelper.DALDBHelper();
                dBHelper.ExecuteNonQuery(pquery, ScreenParams);
                return 1;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessObjects.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return 0;
            }
        }
    }
}
