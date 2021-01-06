using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAccessLayer.ConnectionString
{
    public static class ConnectionString
    {
        public static string GetConnectionString()
        {
            try
            {
                return TSD.DataAccessLayer.DBHelper.DBHelper.GetConnectionString();
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return string.Empty;
            }
        }
        public static void SetConnectionString(string pConnectionString)
        {
            TSD.DataAccessLayer.DBHelper.DBHelper.SetConnectionString(pConnectionString);
        }
    }
}