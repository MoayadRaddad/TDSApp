using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.DALBank
{
    public class DALBank
    {
        public BusinessObjects.Models.Bank CheckBankExist(BusinessObjects.Models.Bank pBank)
        {
            try
            {
                string pquery = "SELECT id,name FROM tblBanks WHERE name = @name";
                List<SqlParameter> BankParams = new List<SqlParameter>();
                BankParams.Add(new SqlParameter("@name", pBank.name));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
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
            catch (Exception)
            {
                return null;
            }
        }
        public BusinessObjects.Models.Bank InsertBank(BusinessObjects.Models.Bank pBank)
        {
            try
            {
                string pquery = "insert into tblBanks OUTPUT INSERTED.IDENTITYCOL  values (@name)";
                List<SqlParameter> BankParams = new List<SqlParameter>();
                BankParams.Add(new SqlParameter("@name", pBank.name));
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                pBank.id = Convert.ToInt32(dBHelper.ExecuteScalar(pquery, BankParams));
                return pBank;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
