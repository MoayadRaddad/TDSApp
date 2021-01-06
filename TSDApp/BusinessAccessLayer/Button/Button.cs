using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessAccessLayer.Button
{
    public static class Button
    {
        public static void DeleteButtonsByScreenId(string pquery, int pScreenId)
        {
            try
            {
                TSD.DataAccessLayer.Button.Button.DeleteButtonsByScreenId(pquery, pScreenId);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
            }
        }
        public static DataTable SelectButtonsbyScreenId(string pquery, int pScreenId)
        {
            try
            {
                return TSD.DataAccessLayer.Button.Button.SelectButtonsbyScreenId(pquery, pScreenId);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static void DeleteButtonsByIds(string pquery, List<int> pButtonsIds)
        {
            try
            {
                TSD.DataAccessLayer.Button.Button.DeleteButtonsByIds(pquery, pButtonsIds);
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
        public static TSDApp.Models.Button InsertButton(string pquery, TSDApp.Models.Button pButton)
        {
            try
            {
                return TSD.DataAccessLayer.Button.Button.InsertButton(pquery, pButton);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static TSDApp.Models.Button UpdateButton(string pquery, TSDApp.Models.Button pButton)
        {
            try
            {
                return TSD.DataAccessLayer.Button.Button.UpdateButton(pquery, pButton);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
    }
}
