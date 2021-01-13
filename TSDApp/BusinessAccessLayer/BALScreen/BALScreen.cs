using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessAccessLayer.BALScreen
{
    public class BALScreen
    {
        DataAccessLayer.DALScreen.DALScreen screen;
        public List<BusinessObjects.Models.Screen> SelectScreensByBankId(BusinessObjects.Models.Bank pBank)
        {
            try
            {
                screen = new DataAccessLayer.DALScreen.DALScreen();
                return screen.SelectScreensByBankId(pBank);
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public int DeleteScreenById(int pScreenId)
        {
            try
            {
                screen = new DataAccessLayer.DALScreen.DALScreen();
                return screen.DeleteScreenById(pScreenId);
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return 0;
            }
        }
        public BusinessObjects.Models.Screen InsertScreen(BusinessObjects.Models.Screen pScreen)
        {
            try
            {
                screen = new DataAccessLayer.DALScreen.DALScreen();
                return screen.InsertScreen(pScreen);
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.Screen UpdateScreen(BusinessObjects.Models.Screen pScreen)
        {
            try
            {
                screen = new DataAccessLayer.DALScreen.DALScreen();
                return screen.UpdateScreen(pScreen);
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
    }
}
