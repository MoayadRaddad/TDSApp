using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessAccessLayer.Screen
{
    public static class Screen
    {
        public static DataTable SelectScreensByBankId(string pquery, TSDApp.Models.Bank pBank)
        {
            try
            {
                return TSD.DataAccessLayer.Screen.Screen.SelectScreensByBankId(pquery, pBank);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static void DeleteScreenById(string pquery, int pScreenId)
        {
            try
            {
                TSD.DataAccessLayer.Screen.Screen.DeleteScreenById(pquery, pScreenId);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
            }
        }
        public static TSDApp.Models.Screen SelectScreenbyId(string pquery, int pScreenId)
        {
            try
            {
                return TSD.DataAccessLayer.Screen.Screen.SelectScreenbyId(pquery, pScreenId);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static TSDApp.Models.Screen InsertScreen(string pquery, TSDApp.Models.Screen pScreen)
        {
            try
            {
                return TSD.DataAccessLayer.Screen.Screen.InsertScreen(pquery, pScreen);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static TSDApp.Models.Screen UpdateScreen(string pquery, TSDApp.Models.Screen pScreen)
        {
            try
            {
                return TSD.DataAccessLayer.Screen.Screen.UpdateScreen(pquery, pScreen);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
    }
}
