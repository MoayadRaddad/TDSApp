using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.DALScreen
{
    public class DALScreen
    {
        public List<BusinessObjects.Models.Screen> SelectScreensByBankId(BusinessObjects.Models.Bank pBank)
        {
            try
            {
                List<BusinessObjects.Models.Screen> lstScreens = new List<BusinessObjects.Models.Screen>();
                string pquery = "SELECT id,name,isActive,bankId FROM tblScreens where bankId = @bankId";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@bankId", pBank.id));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.ExecuteAdapter(pquery, ScreenParams);
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    lstScreens.Add(new BusinessObjects.Models.Screen(Convert.ToInt32(dataRow["id"]), Convert.ToString(dataRow["name"]),
                        Convert.ToBoolean(dataRow["isActive"]), Convert.ToInt32(dataRow["bankId"])));
                }
                return lstScreens;
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public int DeleteScreenById(int pScreenId)
        {
            try
            {
                string pquery = "delete from tblScreens where id = @id";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@id", pScreenId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
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
        public BusinessObjects.Models.Screen InsertScreen(BusinessObjects.Models.Screen pScreen)
        {
            try
            {
                string pquery = "insert into tblScreens OUTPUT INSERTED.IDENTITYCOL  values (@name,@isActive,@bankId,0)";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@name", pScreen.name));
                ScreenParams.Add(new SqlParameter("@isActive", pScreen.isActive));
                ScreenParams.Add(new SqlParameter("@bankId", pScreen.bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                pScreen.id = Convert.ToInt32(dBHelper.ExecuteScalar(pquery, ScreenParams));
                if (pScreen.isActive)
                {
                    UpdateActiveScreen(pScreen.id);
                }
                return pScreen;
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.Screen UpdateScreen(BusinessObjects.Models.Screen pScreen)
        {
            try
            {
                string pquery = "update tblScreens set name = @name,isActive = @isActive where id = @id";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@name", pScreen.name));
                ScreenParams.Add(new SqlParameter("@isActive", pScreen.isActive));
                ScreenParams.Add(new SqlParameter("@id", pScreen.id));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.ExecuteNonQuery(pquery, ScreenParams);
                if (pScreen.isActive)
                {
                    UpdateActiveScreen(pScreen.id);
                }
                return pScreen;
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public void UpdateActiveScreen(int pScreenId)
        {
            try
            {
                string pquery = "update tblScreens set isActive = 0 where id != @id";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@id", pScreenId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.ExecuteScalar(pquery, ScreenParams);
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
            }
        }
        public bool CheckIfScreenIsBusy(int pScreenId)
        {
            string pquery = "select * from tblScreens where id = @id and isBusy = 1";
            List<SqlParameter> ScreenParams = new List<SqlParameter>();
            ScreenParams.Add(new SqlParameter("@id", pScreenId));
            DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
            var rowEffected = dBHelper.ExecuteScalar(pquery, ScreenParams);
            return rowEffected == null ? false : true;
        }
        public bool CheckIfScreenIsDeleted(int pScreenId)
        {
            string pquery = "select * from tblScreens where id = @id";
            List<SqlParameter> ScreenParams = new List<SqlParameter>();
            ScreenParams.Add(new SqlParameter("@id", pScreenId));
            DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
            var rowEffected = dBHelper.ExecuteScalar(pquery, ScreenParams);
            return rowEffected == null ? true : false;
        }
    }
}
