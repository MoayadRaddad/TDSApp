using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.DALBank
{
    public class DALBank
    {
        DALDBHelper.DALDBHelper dBHelper;
        public BusinessObjects.Models.Bank CheckBankExist(BusinessObjects.Models.Bank pBank)
        {
            try
            {
                string pquery = "SELECT id,Name FROM tblBanks WHERE Name = @Name";
                List<SqlParameter> BankParams = new List<SqlParameter>();
                BankParams.Add(new SqlParameter("@Name", pBank.name));
                dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.ExecuteAdapter(pquery, BankParams);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    pBank.id = Convert.ToInt32(dataSet.Tables[0].Rows[0][0].ToString());
                }
                else
                {
                    pBank.id = 0;
                }
                return pBank;
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.Bank InsertBank(BusinessObjects.Models.Bank pBank)
        {
            try
            {
                string pquery = "insert into tblBanks OUTPUT INSERTED.IDENTITYCOL  values (@Name)";
                List<SqlParameter> BankParams = new List<SqlParameter>();
                BankParams.Add(new SqlParameter("@Name", pBank.name));
                dBHelper = new DALDBHelper.DALDBHelper();
                pBank.id = Convert.ToInt32(dBHelper.ExecuteScalar(pquery, BankParams));
                return pBank;
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
    }
}
