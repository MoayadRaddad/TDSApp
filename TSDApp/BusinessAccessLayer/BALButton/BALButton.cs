using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Transactions;

namespace BusinessAccessLayer.BALButton
{
    public class BALButton
    {
        public List<T> selectButtonsbyScreenId<T>(int pScreenId, BusinessObjects.Models.btnType btnType)
        {
            try
            {
                DataAccessLayer.DALButton.DALButton button = new DataAccessLayer.DALButton.DALButton();
                return button.selectButtonsbyScreenId<T>(pScreenId, btnType);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public int deleteScreenAndButtonByScreenId(int pScreenId)
        {
            try
            {
                int check;
                using (TransactionScope scope = new TransactionScope())
                {
                    DataAccessLayer.DALButton.DALButton button = new DataAccessLayer.DALButton.DALButton();
                    DataAccessLayer.DALScreen.DALScreen screen = new DataAccessLayer.DALScreen.DALScreen();
                    button.deleteAllButtonByScreenId(pScreenId);
                    check = screen.deleteScreenById(pScreenId);
                    scope.Complete();
                }
                return check;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return 0;
            }
        }
        public bool checkIfButtonIsDeleted(int pButtonId, BusinessObjects.Models.btnType btnType)
        {
            try
            {
                DataAccessLayer.DALButton.DALButton dALButton = new DataAccessLayer.DALButton.DALButton();
                return dALButton.checkIfButtonIsDeleted(pButtonId, btnType);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return false;
            }
        }
    }
}
