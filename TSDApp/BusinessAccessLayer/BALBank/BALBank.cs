using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Transactions;

namespace BusinessAccessLayer.BALBank
{
    public class BALBank
    {
        public BusinessObjects.Models.Bank CheckBankExist(BusinessObjects.Models.Bank pBank)
        {
            try
            {
                DataAccessLayer.DALBank.DALBank DALBank = new DataAccessLayer.DALBank.DALBank();
                return DALBank.CheckBankExist(pBank);
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
                DataAccessLayer.DALBank.DALBank DALbank = new DataAccessLayer.DALBank.DALBank();
                return DALbank.InsertBank(pBank);
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