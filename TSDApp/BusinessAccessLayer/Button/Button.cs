using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessAccessLayer.Button
{
    public static class Button
    {
        public static void DeleteButtonsByScreenId(int pScreenId)
        {
            try
            {
                TSD.DataAccessLayer.Button.Button.DeleteButtonsByScreenId(pScreenId);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
            }
        }
        public static DataTable SelectButtonsbyScreenId(int pScreenId)
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
        public static void DeleteButtonsByIds(List<int> pButtonsIds)
        {
            try
            {
                TSD.DataAccessLayer.Button.Button.DeleteButtonsByIds(pButtonsIds);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
            }
        }
        public static TSDApp.Models.Button SelectButtonById(string pquery, int pButtonId)
        {
            try
            {
                return TSD.DataAccessLayer.Button.Button.SelectButtonById(pquery, pButtonId);
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
    }
}
