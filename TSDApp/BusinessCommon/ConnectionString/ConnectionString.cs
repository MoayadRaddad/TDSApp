using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessCommon.ConnectionString
{
    public static class ConnectionString
    {
        public static string connectionString;
        static ConnectionString()
        {
            try
            {
                setConnectionString();
            }
            catch (Exception ex)
            {
                ExceptionsWriter.ExceptionsWriter.saveExceptionToLogFile(ex);
            }
        }
        public static void setConnectionString()
        {
            try
            {
                string txtpath = System.AppDomain.CurrentDomain.BaseDirectory + "ConnectionString.txt";
                if (File.Exists(txtpath))
                {
                    using (StreamReader reader = new StreamReader(txtpath))
                    {
                        if (reader.Peek() >= 0)
                        {
                            connectionString = reader.ReadLine();
                        }
                    }
                }
                else
                {
                    connectionString = null;
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.ExceptionsWriter.saveExceptionToLogFile(ex);
            }
        }
        public static int checkConnectionStringStatus()
        {
            try
            {
                if (connectionString == null)
                {
                    return 0;
                }
                else if (connectionString == "")
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                ExceptionsWriter.ExceptionsWriter.saveExceptionToLogFile(ex);
                return 0;
            }
        }
    }
}
