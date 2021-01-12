using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAccessLayer.ConnectionString
{
    public class BALConnectionString
    {
        public int SetConnectionString()
        {
            try
            {
                TSD.DataAccessLayer.DBHelper.DALDBHelper dBHelper = new TSD.DataAccessLayer.DBHelper.DALDBHelper();
                return dBHelper.SetConnectionString();
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessObjects.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return 0;
            }
        }
    }
}