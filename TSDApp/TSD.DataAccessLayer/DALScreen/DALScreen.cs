using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.DALScreen
{
    public class DALScreen
    {
        public List<BusinessObjects.Models.Screen> selectScreensByBankId(BusinessObjects.Models.Bank pBank)
        {
            try
            {
                List<BusinessObjects.Models.Screen> lstScreens = new List<BusinessObjects.Models.Screen>();
                string pquery = "SELECT id,name,isActive,bankId FROM tblScreens where bankId = @bankId";
                List<SqlParameter> screenParams = new List<SqlParameter>();
                screenParams.Add(new SqlParameter("@bankId", pBank.id));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapter(pquery, screenParams);
                if(dataSet != null)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        lstScreens.Add(new BusinessObjects.Models.Screen(Convert.ToInt32(dataRow["id"]), Convert.ToString(dataRow["name"]),
                            Convert.ToBoolean(dataRow["isActive"]), Convert.ToInt32(dataRow["bankId"])));
                    }
                    return lstScreens;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public int deleteScreenById(int pScreenId)
        {
            try
            {
                string pquery = "delete from tblScreens where id = @id";
                List<SqlParameter> screenParams = new List<SqlParameter>();
                screenParams.Add(new SqlParameter("@id", pScreenId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.executeNonQuery(pquery, screenParams);
                return 1;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return 0;
            }
        }
        public BusinessObjects.Models.Screen insertScreen(BusinessObjects.Models.Screen pScreen)
        {
            try
            {
                string pquery = "insert into tblScreens OUTPUT INSERTED.IDENTITYCOL  values (@name,@isActive,@bankId,0)";
                List<SqlParameter> screenParams = new List<SqlParameter>();
                screenParams.Add(new SqlParameter("@name", pScreen.name));
                screenParams.Add(new SqlParameter("@isActive", pScreen.isActive));
                screenParams.Add(new SqlParameter("@bankId", pScreen.bankId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                pScreen.id = Convert.ToInt32(dBHelper.executeScalar(pquery, screenParams));
                if (pScreen.isActive)
                {
                    updateActiveScreen(pScreen.id);
                }
                return pScreen;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.Screen updateScreen(BusinessObjects.Models.Screen pScreen)
        {
            try
            {
                string pquery = "update tblScreens set name = @name,isActive = @isActive where id = @id";
                List<SqlParameter> screenParams = new List<SqlParameter>();
                screenParams.Add(new SqlParameter("@name", pScreen.name));
                screenParams.Add(new SqlParameter("@isActive", pScreen.isActive));
                screenParams.Add(new SqlParameter("@id", pScreen.id));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.executeNonQuery(pquery, screenParams);
                if (pScreen.isActive)
                {
                    updateActiveScreen(pScreen.id);
                }
                return pScreen;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public void updateActiveScreen(int pScreenId)
        {
            try
            {
                string pquery = "update tblScreens set isActive = 0 where id != @id";
                List<SqlParameter> screenParams = new List<SqlParameter>();
                screenParams.Add(new SqlParameter("@id", pScreenId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                dBHelper.executeScalar(pquery, screenParams);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
            }
        }
        public bool checkIfScreenIsBusy(int pScreenId)
        {
            try
            {
                string pquery = "select * from tblScreens where id = @id and isBusy = 1";
                List<SqlParameter> screenParams = new List<SqlParameter>();
                screenParams.Add(new SqlParameter("@id", pScreenId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                var rowEffected = dBHelper.executeScalar(pquery, screenParams);
                return rowEffected == null ? false : true;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return false;
            }
        }
        public bool checkIfScreenIsDeleted(int pScreenId)
        {
            try
            {
                string pquery = "select * from tblScreens where id = @id";
                List<SqlParameter> screenParams = new List<SqlParameter>();
                screenParams.Add(new SqlParameter("@id", pScreenId));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                var rowEffected = dBHelper.executeScalar(pquery, screenParams);
                return rowEffected == null ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return false;
            }
        }
    }
}
