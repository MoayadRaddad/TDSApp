﻿using System;
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
            try
            {
                TSD.DataAccessLayer.DBHelper.DBHelper.SetConnectionString(pConnectionString);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
            }
        }
        public static bool IsServerConnected()
        {
            try
            {
                return TSD.DataAccessLayer.DBHelper.DBHelper.IsServerConnected();
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return false;
            }
        }
    }
}