using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessAccessLayer.Screen
{
    public class BALScreen
    {
        TSD.DataAccessLayer.Screen.DALScreen screen;
        public List<TSDApp.Models.Screen> SelectScreensByBankId(TSDApp.Models.Bank pBank)
        {
            try
            {
                screen = new TSD.DataAccessLayer.Screen.DALScreen();
                return screen.SelectScreensByBankId(pBank);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessObjects.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public int DeleteScreenById(int pScreenId)
        {
            try
            {
                screen = new TSD.DataAccessLayer.Screen.DALScreen();
                return screen.DeleteScreenById(pScreenId);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessObjects.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return 0;
            }
        }
        public TSDApp.Models.Screen InsertScreen(TSDApp.Models.Screen pScreen)
        {
            try
            {
                screen = new TSD.DataAccessLayer.Screen.DALScreen();
                return screen.InsertScreen(pScreen);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessObjects.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public TSDApp.Models.Screen UpdateScreen(TSDApp.Models.Screen pScreen)
        {
            try
            {
                screen = new TSD.DataAccessLayer.Screen.DALScreen();
                return screen.UpdateScreen(pScreen);
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
