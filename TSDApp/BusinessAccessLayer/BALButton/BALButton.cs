using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessAccessLayer.Button
{
    public class BALButton
    {
        TSD.DataAccessLayer.Button.DALButton button;
        public List<T> SelectButtonsbyScreenId<T>(int pScreenId, BusinessObjects.Models.btnType btnType)
        {
            try
            {
                button = new TSD.DataAccessLayer.Button.DALButton();
                return button.SelectButtonsbyScreenId<T>(pScreenId, btnType);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessObjects.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.ShowMessageButton InsertShowMessageButton(BusinessObjects.Models.ShowMessageButton pButton)
        {
            try
            {
                button = new TSD.DataAccessLayer.Button.DALButton();
                return button.InsertShowMessageButton(pButton);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessObjects.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.ShowMessageButton UpdateShowMessageButton(BusinessObjects.Models.ShowMessageButton pButton)
        {
            try
            {
                button = new TSD.DataAccessLayer.Button.DALButton();
                return button.UpdateShowMessageButton(pButton);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessObjects.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.IssueTicketButton InsertIssueTicketButton(BusinessObjects.Models.IssueTicketButton pButton)
        {
            try
            {
                button = new TSD.DataAccessLayer.Button.DALButton();
                return button.InsertIssueTicketButton(pButton);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessObjects.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.IssueTicketButton UpdateIssueTicketButton(BusinessObjects.Models.IssueTicketButton pButton)
        {
            try
            {
                button = new TSD.DataAccessLayer.Button.DALButton();
                return button.UpdateIssueTicketButton(pButton);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessObjects.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public int DeleteButtonWhere(IDictionary<int, string> pButtonsIds, string ConditionColumn)
        {
            try
            {
                button = new TSD.DataAccessLayer.Button.DALButton();
                return button.DeleteButtonWhere(pButtonsIds, ConditionColumn);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessObjects.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return 0;
            }
        }
        public int DeleteAllButtonByScreenId(int pScreenId)
        {
            try
            {
                button = new TSD.DataAccessLayer.Button.DALButton();
                return button.DeleteAllButtonByScreenId(pScreenId);
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessObjects.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return 0;
            }
        }
    }
}
