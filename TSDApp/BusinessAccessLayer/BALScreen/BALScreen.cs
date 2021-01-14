using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Transactions;

namespace BusinessAccessLayer.BALScreen
{
    public class BALScreen
    {
        public List<BusinessObjects.Models.Screen> SelectScreensByBankId(BusinessObjects.Models.Bank pBank)
        {
            try
            {
                DataAccessLayer.DALScreen.DALScreen screenDAL = new DataAccessLayer.DALScreen.DALScreen();
                return screenDAL.SelectScreensByBankId(pBank);
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
                DataAccessLayer.DALScreen.DALScreen screenDAL = new DataAccessLayer.DALScreen.DALScreen();
                return screenDAL.DeleteScreenById(pScreenId);
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return 0;
            }
        }
        public BusinessObjects.Models.Screen InsertScreenAndEditButtons(BusinessObjects.Models.Screen pScreen
            , List<BusinessObjects.Models.ShowMessageButton> LstShowMessageButtons, List<BusinessObjects.Models.IssueTicketButton> LstIssueTicketButtons)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DataAccessLayer.DALScreen.DALScreen screenDAL = new DataAccessLayer.DALScreen.DALScreen();
                    BusinessObjects.Models.Screen screen = screenDAL.InsertScreen(pScreen);
                    DataAccessLayer.DALButton.DALButton button = new DataAccessLayer.DALButton.DALButton();
                    foreach (BusinessObjects.Models.IssueTicketButton pbutton in LstIssueTicketButtons)
                    {
                        if (pbutton.id == 0)
                        {
                            pbutton.screenId = screen.id;
                            BusinessObjects.Models.IssueTicketButton btnInsertCheck = button.InsertIssueTicketButton(pbutton);
                            if (btnInsertCheck == null)
                            {
                                return null;
                            }
                        }
                        else if (pbutton.updated == true)
                        {
                            BusinessObjects.Models.IssueTicketButton btnUpdateCheck = button.UpdateIssueTicketButton(pbutton);
                            if (btnUpdateCheck == null)
                            {
                                return null;
                            }
                        }
                    }
                    foreach (BusinessObjects.Models.ShowMessageButton pbutton in LstShowMessageButtons)
                    {
                        if (pbutton.id == 0)
                        {
                            pbutton.screenId = screen.id;
                            BusinessObjects.Models.ShowMessageButton btnInsertCheck = button.InsertShowMessageButton(pbutton);
                            if (btnInsertCheck == null)
                            {
                                return null;
                            }
                        }
                        else if (pbutton.updated == true)
                        {
                            BusinessObjects.Models.ShowMessageButton btnUpdateCheck = button.UpdateShowMessageButton(pbutton);
                            if (btnUpdateCheck == null)
                            {
                                return null;
                            }
                        }
                    }
                    scope.Complete();
                    return screen;
                }
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.Screen UpdateScreenAndEditButtons(BusinessObjects.Models.Screen pScreen
            , List<BusinessObjects.Models.ShowMessageButton> LstShowMessageButtons, List<BusinessObjects.Models.IssueTicketButton> LstIssueTicketButtons)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DataAccessLayer.DALScreen.DALScreen screenDAL = new DataAccessLayer.DALScreen.DALScreen();
                    BusinessObjects.Models.Screen screen = screenDAL.UpdateScreen(pScreen);
                    DataAccessLayer.DALButton.DALButton button = new DataAccessLayer.DALButton.DALButton();
                    foreach (BusinessObjects.Models.IssueTicketButton pbutton in LstIssueTicketButtons)
                    {
                        if (pbutton.id == 0)
                        {
                            pbutton.screenId = screen.id;
                            BusinessObjects.Models.IssueTicketButton btnInsertCheck = button.InsertIssueTicketButton(pbutton);
                            if (btnInsertCheck == null)
                            {
                                return null;
                            }
                        }
                        else if (pbutton.updated == true)
                        {
                            BusinessObjects.Models.IssueTicketButton btnUpdateCheck = button.UpdateIssueTicketButton(pbutton);
                            if (btnUpdateCheck == null)
                            {
                                return null;
                            }
                        }
                    }
                    foreach (BusinessObjects.Models.ShowMessageButton pbutton in LstShowMessageButtons)
                    {
                        if (pbutton.id == 0)
                        {
                            pbutton.screenId = screen.id;
                            BusinessObjects.Models.ShowMessageButton btnInsertCheck = button.InsertShowMessageButton(pbutton);
                            if (btnInsertCheck == null)
                            {
                                return null;
                            }
                        }
                        else if (pbutton.updated == true)
                        {
                            BusinessObjects.Models.ShowMessageButton btnUpdateCheck = button.UpdateShowMessageButton(pbutton);
                            if (btnUpdateCheck == null)
                            {
                                return null;
                            }
                        }
                    }
                    scope.Complete();
                    return screen;
                }
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
        public bool CheckIfScreenIsBusy(int pScreenId)
        {
            DataAccessLayer.DALScreen.DALScreen screenDAL = new DataAccessLayer.DALScreen.DALScreen();
            return screenDAL.CheckIfScreenIsBusy(pScreenId);
        }
        public bool CheckIfScreenIsDeleted(int pScreenId)
        {
            DataAccessLayer.DALScreen.DALScreen screenDAL = new DataAccessLayer.DALScreen.DALScreen();
            return screenDAL.CheckIfScreenIsDeleted(pScreenId);
        }
    }
}
