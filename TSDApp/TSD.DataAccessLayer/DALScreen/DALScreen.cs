using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.DALScreen
{
    public class DALScreen
    {
        DALDBHelper.DALDBHelper dBHelper;
        public List<BusinessObjects.Models.Screen> SelectScreensByBankId(BusinessObjects.Models.Bank pBank)
        {
            try
            {
                List<BusinessObjects.Models.Screen> lstScreens = new List<BusinessObjects.Models.Screen>();
                string pquery = "SELECT id,name,isActive,BankId FROM tblScreens where BankId = @BankId";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@BankId", pBank.id));
                dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.ExecuteAdapter(pquery, ScreenParams);
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    lstScreens.Add(new BusinessObjects.Models.Screen(Convert.ToInt32(dataRow["id"]), Convert.ToString(dataRow["Name"]),
                        Convert.ToBoolean(dataRow["isActive"]), Convert.ToInt32(dataRow["BankId"])));
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
        public BusinessObjects.Models.Screen InsertScreen(BusinessObjects.Models.Screen pScreen)
        {
            try
            {
                string pquery = "insert into tblScreens OUTPUT INSERTED.IDENTITYCOL  values (@Name,@isActive,@BankId)";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@Name", pScreen.name));
                ScreenParams.Add(new SqlParameter("@isActive", pScreen.isActive));
                ScreenParams.Add(new SqlParameter("@BankId", pScreen.bankId));
                dBHelper = new DALDBHelper.DALDBHelper();
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
                string pquery = "update tblScreens set name = @Name,isActive = @isActive where id = @id";
                List<SqlParameter> ScreenParams = new List<SqlParameter>();
                ScreenParams.Add(new SqlParameter("@Name", pScreen.name));
                ScreenParams.Add(new SqlParameter("@isActive", pScreen.isActive));
                ScreenParams.Add(new SqlParameter("@id", pScreen.id));
                dBHelper = new DALDBHelper.DALDBHelper();
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
                dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.ExecuteScalar(pquery, ScreenParams);
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
            }
        }
    }
}
