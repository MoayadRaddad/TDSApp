using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessAccessLayer.Bank
{
    public class BALBank
    {
        TSD.DataAccessLayer.Bank.DALBank bank;
        public TSDApp.Models.Bank CheckBankExist(TSDApp.Models.Bank pBank)
        {
            try
            {
                bank = new TSD.DataAccessLayer.Bank.DALBank();
                return bank.CheckBankExist(pBank);
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
                bank = new TSD.DataAccessLayer.Bank.DALBank();
                return bank.InsertBank(pBank);
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