using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessAccessLayer.Screen
{
    public static class Screen
    {
        public static List<TSDApp.Models.Screen> SelectScreensByBankId(TSDApp.Models.Bank pBank)
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
        public static int DeleteScreenById(int pScreenId)
        {
            try
            {
                return TSD.DataAccessLayer.Screen.Screen.DeleteScreenById(pScreenId);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return 0;
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
