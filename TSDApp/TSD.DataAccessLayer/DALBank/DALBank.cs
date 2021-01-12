using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TSD.DataAccessLayer.Bank
{
    public class DALBank
    {
        DBHelper.DALDBHelper dBHelper;
        public TSDApp.Models.Bank CheckBankExist(TSDApp.Models.Bank pBank)
        {
            try
            {
                string pquery = "SELECT id,Name FROM tblBanks WHERE Name = @Name";
                List<SqlParameter> BankParams = new List<SqlParameter>();
                BankParams.Add(new SqlParameter("@Name", pBank.name));
                dBHelper = new DBHelper.DALDBHelper();
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
                BusinessObjects.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessObjects.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public TSDApp.Models.Bank InsertBank(TSDApp.Models.Bank pBank)
        {
            try
            {
                string pquery = "insert into tblBanks OUTPUT INSERTED.IDENTITYCOL  values (@Name)";
                List<SqlParameter> BankParams = new List<SqlParameter>();
                BankParams.Add(new SqlParameter("@Name", pBank.name));
                dBHelper = new DBHelper.DALDBHelper();
                pBank.id = Convert.ToInt32(dBHelper.ExecuteScalar(pquery, BankParams));
                return pBank;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessObjects.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
    }
}
