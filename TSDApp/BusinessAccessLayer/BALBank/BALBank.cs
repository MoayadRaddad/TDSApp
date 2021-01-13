using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessAccessLayer.BALBank
{
    public class BALBank
    {
        DataAccessLayer.DALBank.DALBank bank;
        public BusinessObjects.Models.Bank CheckBankExist(BusinessObjects.Models.Bank pBank)
        {
            try
            {
                bank = new DataAccessLayer.DALBank.DALBank();
                return bank.CheckBankExist(pBank);
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
                bank = new DataAccessLayer.DALBank.DALBank();
                return bank.InsertBank(pBank);
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