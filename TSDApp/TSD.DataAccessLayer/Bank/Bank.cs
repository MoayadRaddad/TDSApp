﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TSD.DataAccessLayer.Bank
{
    public static class Bank
    {
        public static TSDApp.Models.Bank CheckBankExist(string pquery, TSDApp.Models.Bank pBank)
        {
            try
            {
                SqlParameter BankName = new SqlParameter("@Name", pBank.Name);
                object[] BankParams = new object[] { BankName.ParameterName,BankName.SqlValue };
                DataTable QueryResult = DBHelper.DBHelper.ExecuteQuery(pquery, BankParams);
                pBank.id = Convert.ToInt32(QueryResult.Rows[0][0]);
                return pBank;
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
                SqlParameter BankName = new SqlParameter("@Name", pBank.Name);
                object[] BankParams = new object[] { BankName.ParameterName, BankName.SqlValue };
                pBank.id = Convert.ToInt32(DBHelper.DBHelper.ExecuteScalar(pquery, BankParams));
                return pBank;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
    }
}