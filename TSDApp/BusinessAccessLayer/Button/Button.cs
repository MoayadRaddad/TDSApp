using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessAccessLayer.Button
{
    public static class Button
    {
        public static List<TSDApp.Models.Button> SelectButtonsbyScreenId(int pScreenId)
        {
            try
            {
                return TSD.DataAccessLayer.Button.Button.SelectButtonsbyScreenId(pScreenId);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static TSDApp.Models.Button InsertButton(TSDApp.Models.Button pButton)
        {
            try
            {
                return TSD.DataAccessLayer.Button.Button.InsertButton(pButton);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static TSDApp.Models.Button UpdateButton(TSDApp.Models.Button pButton)
        {
            try
            {
                return TSD.DataAccessLayer.Button.Button.UpdateButton(pButton);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static int DeleteButtonWhere(List<int> pButtonsIds, string ConditionColumn)
        {
            try
            {
                return TSD.DataAccessLayer.Button.Button.DeleteButtonWhere(pButtonsIds, ConditionColumn);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return 0;
            }
        }
    }
}
