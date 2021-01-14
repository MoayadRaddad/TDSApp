using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.DALButton
{
    public class DALButton
    {
        public List<T> SelectButtonsbyScreenId<T>(int pScreenId, BusinessObjects.Models.btnType btnType)
        {
            try
            {
                List<T> lstButtons = new List<T>();
                string pquery = "SELECT * FROM tbl" + btnType.ToString() + "Button where screenId = @screenId";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@screenId", pScreenId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.ExecuteAdapter(pquery, ScreenParams);
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    if (btnType == BusinessObjects.Models.btnType.ShowMessage)
                    {
                        var btn = new BusinessObjects.Models.ShowMessageButton(Convert.ToInt32(dataRow["id"]), dataRow["enName"].ToString(), dataRow["arName"].ToString(),
                            dataRow["messageAR"].ToString(), dataRow["messageEN"].ToString(), Convert.ToInt32(dataRow["screenId"]));
                        lstButtons.Add((T)(object)btn);
                    }
                    else
                    {
                        var btn = new BusinessObjects.Models.IssueTicketButton(Convert.ToInt32(dataRow["id"]), dataRow["enName"].ToString(), dataRow["arName"].ToString(),
                            Convert.ToInt32(dataRow["serviceId"]), Convert.ToInt32(dataRow["screenId"]));
                        lstButtons.Add((T)(object)btn);
                    }
                }
                return lstButtons;
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.ShowMessageButton InsertShowMessageButton(BusinessObjects.Models.ShowMessageButton pButton)
        {
            try
            {
                string pquery = "insert into tblShowMessageButton OUTPUT INSERTED.IDENTITYCOL  values (@enName,@arName,@messageEN,@messageAR,@screenId,0)";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@enName", pButton.enName));
                ScreenParams.Add(new SqlParameter("@arName", pButton.arName));
                ScreenParams.Add(new SqlParameter("@messageEN", pButton.messageEN));
                ScreenParams.Add(new SqlParameter("@messageAR", pButton.messageAR));
                ScreenParams.Add(new SqlParameter("@screenId", pButton.screenId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                pButton.id = Convert.ToInt32(dBHelper.ExecuteScalar(pquery, ScreenParams));
                return pButton;
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.ShowMessageButton UpdateShowMessageButton(BusinessObjects.Models.ShowMessageButton pButton)
        {
            try
            {
                string pquery = "update tblShowMessageButton set enName = @enName,arName = @arName,type = @type,messageAR = @messageAR,messageEN = @messageEN where id = @id";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@id", pButton.id));
                ScreenParams.Add(new SqlParameter("@enName", pButton.enName));
                ScreenParams.Add(new SqlParameter("@arName", pButton.arName));
                ScreenParams.Add(new SqlParameter("@type", pButton.type));
                ScreenParams.Add(new SqlParameter("@messageEN", pButton.messageEN));
                ScreenParams.Add(new SqlParameter("@messageAR", pButton.messageAR));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.ExecuteNonQuery(pquery, ScreenParams);
                return pButton;
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.IssueTicketButton InsertIssueTicketButton(BusinessObjects.Models.IssueTicketButton pButton)
        {
            try
            {
                string pquery = "insert into tblIssueTicketButton OUTPUT INSERTED.IDENTITYCOL  values (@enName,@arName,@serviceId,@screenId,0)";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@enName", pButton.enName));
                ScreenParams.Add(new SqlParameter("@arName", pButton.arName));
                ScreenParams.Add(new SqlParameter("@serviceId", pButton.serviceId));
                ScreenParams.Add(new SqlParameter("@screenId", pButton.screenId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                pButton.id = Convert.ToInt32(dBHelper.ExecuteScalar(pquery, ScreenParams));
                return pButton;
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.IssueTicketButton UpdateIssueTicketButton(BusinessObjects.Models.IssueTicketButton pButton)
        {
            try
            {
                string pquery = "update tblIssueTicketButton set enName = @enName,ARName = @arName,serviceId = @serviceId where id = @id";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@id", pButton.id));
                ScreenParams.Add(new SqlParameter("@enName", pButton.enName));
                ScreenParams.Add(new SqlParameter("@arName", pButton.arName));
                ScreenParams.Add(new SqlParameter("@serviceId", pButton.serviceId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.ExecuteNonQuery(pquery, ScreenParams);
                return pButton;
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public int DeleteButtonWhere(IDictionary<int, string> pButtonsIds, string ConditionColumn)
        {
            try
            {
                foreach (var item in pButtonsIds)
                {
                    if(Convert.ToInt32(item.Key) != 0)
                    {
                        string pquery = string.Empty;
                        pquery = "delete from tbl" + item.Value.ToString() + "Button where " + ConditionColumn + " = @id";
                        List<SqlParameter> ScreenParams = new List<SqlParameter>();
                        ScreenParams.Add(new SqlParameter("@id", item.Key));
                        DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                        dBHelper.ExecuteNonQuery(pquery, ScreenParams);
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return 0;
            }
        }
        public int DeleteAllButtonByScreenId(int pScreenId)
        {
            try
            {
                string pquery = "delete from tblShowMessageButton where screenId = @screenId";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@screenId", pScreenId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.ExecuteNonQuery(pquery, ScreenParams);
                pquery = "delete from tblIssueTicketButton where screenId = @screenId";
                ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@screenId", pScreenId));
                dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.ExecuteNonQuery(pquery, ScreenParams);
                return 1;
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return 0;
            }
        }
        public bool CheckIfButtonIsDeleted(int pButtonId, BusinessObjects.Models.btnType btnType)
        {
            string pquery = "select * from tbl" + btnType.ToString() + "Button where id = @id";
            List<SqlParameter> ScreenParams = new List<SqlParameter>();
            ScreenParams.Add(new SqlParameter("@id", pButtonId));
            DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
            var rowEffected = dBHelper.ExecuteScalar(pquery, ScreenParams);
            return rowEffected == null ? true : false;
        }
    }
}
