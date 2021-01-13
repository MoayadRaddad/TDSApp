using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessAccessLayer.BALButton
{
    public class BALButton
    {
        DataAccessLayer.DALButton.DALButton button;
        public List<T> SelectButtonsbyScreenId<T>(int pScreenId, BusinessObjects.Models.btnType btnType)
        {
            try
            {
                button = new DataAccessLayer.DALButton.DALButton();
                return button.SelectButtonsbyScreenId<T>(pScreenId, btnType);
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.ShowMessageButton InsertShowMessageButton(BusinessObjects.Models.ShowMessageButton pButton)
        {
            try
            {
                button = new DataAccessLayer.DALButton.DALButton();
                return button.InsertShowMessageButton(pButton);
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.ShowMessageButton UpdateShowMessageButton(BusinessObjects.Models.ShowMessageButton pButton)
        {
            try
            {
                button = new DataAccessLayer.DALButton.DALButton();
                return button.UpdateShowMessageButton(pButton);
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.IssueTicketButton InsertIssueTicketButton(BusinessObjects.Models.IssueTicketButton pButton)
        {
            try
            {
                button = new DataAccessLayer.DALButton.DALButton();
                return button.InsertIssueTicketButton(pButton);
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.IssueTicketButton UpdateIssueTicketButton(BusinessObjects.Models.IssueTicketButton pButton)
        {
            try
            {
                button = new DataAccessLayer.DALButton.DALButton();
                return button.UpdateIssueTicketButton(pButton);
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public int DeleteButtonWhere(IDictionary<int, string> pButtonsIds, string ConditionColumn)
        {
            try
            {
                button = new DataAccessLayer.DALButton.DALButton();
                return button.DeleteButtonWhere(pButtonsIds, ConditionColumn);
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return 0;
            }
        }
        public int DeleteAllButtonByScreenId(int pScreenId)
        {
            try
            {
                button = new DataAccessLayer.DALButton.DALButton();
                return button.DeleteAllButtonByScreenId(pScreenId);
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return 0;
            }
        }
    }
}
