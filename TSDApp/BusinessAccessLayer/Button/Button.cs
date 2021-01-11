using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessAccessLayer.Button
{
    public static class Button
    {
        public static List<T> SelectButtonsbyScreenId<T>(int pScreenId, BusinessObjects.Models.btnType btnType)
        {
            try
            {
                return TSD.DataAccessLayer.Button.Button.SelectButtonsbyScreenId<T>(pScreenId, btnType);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static BusinessObjects.Models.ShowMessage InsertShowMessageButton(BusinessObjects.Models.ShowMessage pButton)
        {
            try
            {
                return TSD.DataAccessLayer.Button.Button.InsertShowMessageButton(pButton);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static BusinessObjects.Models.ShowMessage UpdateShowMessageButton(BusinessObjects.Models.ShowMessage pButton)
        {
            try
            {
                return TSD.DataAccessLayer.Button.Button.UpdateShowMessageButton(pButton);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static BusinessObjects.Models.IssueTicket InsertIssueTicketButton(BusinessObjects.Models.IssueTicket pButton)
        {
            try
            {
                return TSD.DataAccessLayer.Button.Button.InsertIssueTicketButton(pButton);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static BusinessObjects.Models.IssueTicket UpdateIssueTicketButton(BusinessObjects.Models.IssueTicket pButton)
        {
            try
            {
                return TSD.DataAccessLayer.Button.Button.UpdateIssueTicketButton(pButton);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public static int DeleteButtonWhere(IDictionary<int, string> pButtonsIds, string ConditionColumn)
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
        public static int DeleteAllButtonByScreenId(int pScreenId)
        {
            try
            {
                return TSD.DataAccessLayer.Button.Button.DeleteAllButtonByScreenId(pScreenId);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return 0;
            }
        }
    }
}
