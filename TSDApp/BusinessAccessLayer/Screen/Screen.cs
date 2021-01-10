using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessAccessLayer.Screen
{
    public static class Screen
    {
        public static DataTable SelectScreensByBankId(TSDApp.Models.Bank pBank)
        {
            try
            {
                return TSD.DataAccessLayer.Screen.Screen.SelectScreensByBankId(pBank);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static void DeleteScreenById(int pScreenId)
        {
            try
            {
                TSD.DataAccessLayer.Screen.Screen.DeleteScreenById(pScreenId);
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
        public static TSDApp.Models.Screen InsertScreen(TSDApp.Models.Screen pScreen)
        {
            try
            {
                return TSD.DataAccessLayer.Screen.Screen.InsertScreen(pScreen);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static TSDApp.Models.Screen UpdateScreen(TSDApp.Models.Screen pScreen)
        {
            try
            {
                return TSD.DataAccessLayer.Screen.Screen.UpdateScreen(pScreen);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
    }
}
