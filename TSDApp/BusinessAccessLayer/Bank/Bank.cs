using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessAccessLayer.Bank
{
    public static class Bank
    {
        public static TSDApp.Models.Bank CheckBankExist(string pquery, TSDApp.Models.Bank pBank)
        {
            try
            {
                return TSD.DataAccessLayer.Bank.Bank.CheckBankExist(pquery, pBank);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static TSDApp.Models.Bank InsertBank(string pquery, TSDApp.Models.Bank pBank)
        {
            try
            {
                return TSD.DataAccessLayer.Bank.Bank.InsertBank(pquery, pBank);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
    }
}