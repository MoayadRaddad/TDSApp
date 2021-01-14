using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Transactions;

namespace BusinessAccessLayer.BALButton
{
    public class BALButton
    {
        public List<T> SelectButtonsbyScreenId<T>(int pScreenId, BusinessObjects.Models.btnType btnType)
        {
            try
            {
                DataAccessLayer.DALButton.DALButton button = new DataAccessLayer.DALButton.DALButton();
                return button.SelectButtonsbyScreenId<T>(pScreenId, btnType);
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
                DataAccessLayer.DALButton.DALButton button = new DataAccessLayer.DALButton.DALButton();
                return button.DeleteButtonWhere(pButtonsIds, ConditionColumn);
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return 0;
            }
        }
        public int DeleteScreenAndButtonByScreenId(int pScreenId)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DataAccessLayer.DALButton.DALButton button = new DataAccessLayer.DALButton.DALButton();
                    DataAccessLayer.DALScreen.DALScreen screen = new DataAccessLayer.DALScreen.DALScreen();
                    button.DeleteAllButtonByScreenId(pScreenId);
                    var check = screen.DeleteScreenById(pScreenId);
                    scope.Complete();
                    return check;
                }
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return 0;
            }
        }
        public bool CheckIfButtonIsDeleted(int pButtonId, BusinessObjects.Models.btnType btnType)
        {
            DataAccessLayer.DALButton.DALButton dALButton = new DataAccessLayer.DALButton.DALButton();
            return dALButton.CheckIfButtonIsDeleted(pButtonId, btnType);
        }
    }
}
