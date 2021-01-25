using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Transactions;

namespace BusinessAccessLayer.BALScreen
{
    public class BALScreen
    {
        public List<BusinessObjects.Models.Screen> selectScreensByBankId(BusinessObjects.Models.Bank pBank)
        {
            try
            {
                DataAccessLayer.DALScreen.DALScreen screenDAL = new DataAccessLayer.DALScreen.DALScreen();
                return screenDAL.selectScreensByBankId(pBank);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public int deleteScreenById(int pScreenId)
        {
            try
            {
                DataAccessLayer.DALScreen.DALScreen screenDAL = new DataAccessLayer.DALScreen.DALScreen();
                return screenDAL.deleteScreenById(pScreenId);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return 0;
            }
        }
        public BusinessObjects.Models.Screen insertScreenAndEditButtons(BusinessObjects.Models.Screen pScreen
            , List<BusinessObjects.Models.ShowMessageButton> lstShowMessageButtons, List<BusinessObjects.Models.IssueTicketButton> lstIssueTicketButtons)
        {
            try
            {
                DataAccessLayer.DALScreen.DALScreen screenDAL = new DataAccessLayer.DALScreen.DALScreen();
                BusinessObjects.Models.Screen screen;
                using (TransactionScope scope = new TransactionScope())
                {
                    screen = screenDAL.insertScreen(pScreen);
                    DataAccessLayer.DALButton.DALButton button = new DataAccessLayer.DALButton.DALButton();
                    foreach (BusinessObjects.Models.IssueTicketButton pbutton in lstIssueTicketButtons)
                    {
                        if (pbutton.id == 0)
                        {
                            pbutton.screenId = screen.id;
                            BusinessObjects.Models.IssueTicketButton btnInsertCheck = button.insertIssueTicketButton(pbutton);
                            if (btnInsertCheck == null)
                            {
                                return null;
                            }
                        }
                        else if (pbutton.updated == true)
                        {
                            BusinessObjects.Models.IssueTicketButton btnUpdateCheck = button.updateIssueTicketButton(pbutton);
                            if (btnUpdateCheck == null)
                            {
                                return null;
                            }
                        }
                    }
                    foreach (BusinessObjects.Models.ShowMessageButton pbutton in lstShowMessageButtons)
                    {
                        if (pbutton.id == 0)
                        {
                            pbutton.screenId = screen.id;
                            BusinessObjects.Models.ShowMessageButton btnInsertCheck = button.insertShowMessageButton(pbutton);
                            if (btnInsertCheck == null)
                            {
                                return null;
                            }
                        }
                        else if (pbutton.updated == true)
                        {
                            BusinessObjects.Models.ShowMessageButton btnUpdateCheck = button.updateShowMessageButton(pbutton);
                            if (btnUpdateCheck == null)
                            {
                                return null;
                            }
                        }
                    }
                    scope.Complete();
                }
                return screen;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public BusinessObjects.Models.Screen updateScreenAndEditButtons(BusinessObjects.Models.Screen pScreen
            , List<BusinessObjects.Models.ShowMessageButton> lstShowMessageButtons, List<BusinessObjects.Models.IssueTicketButton> lstIssueTicketButtons)
        {
            try
            {
                DataAccessLayer.DALScreen.DALScreen screenDAL = new DataAccessLayer.DALScreen.DALScreen();
                BusinessObjects.Models.Screen screen;
                using (TransactionScope scope = new TransactionScope())
                {
                    screen = screenDAL.updateScreen(pScreen);
                    DataAccessLayer.DALButton.DALButton button = new DataAccessLayer.DALButton.DALButton();
                    IDictionary<int, string> pButtonsDetailsIds = new Dictionary<int, string>();
                    foreach (BusinessObjects.Models.IssueTicketButton pbutton in lstIssueTicketButtons)
                    {
                        if (pbutton.id == 0)
                        {
                            pbutton.screenId = screen.id;
                            BusinessObjects.Models.IssueTicketButton btnInsertCheck = button.insertIssueTicketButton(pbutton);
                            if (btnInsertCheck == null)
                            {
                                return null;
                            }
                        }
                        else if (pbutton.isDeleted)
                        {
                            pButtonsDetailsIds.Add(pbutton.id, BusinessObjects.Models.btnType.IssueTicket.ToString());
                        }
                        else if (pbutton.updated)
                        {
                            BusinessObjects.Models.IssueTicketButton btnUpdateCheck = button.updateIssueTicketButton(pbutton);
                            if (btnUpdateCheck == null)
                            {
                                return null;
                            }
                        }
                    }
                    foreach (BusinessObjects.Models.ShowMessageButton pbutton in lstShowMessageButtons)
                    {
                        if (pbutton.id == 0)
                        {
                            pbutton.screenId = screen.id;
                            BusinessObjects.Models.ShowMessageButton btnInsertCheck = button.insertShowMessageButton(pbutton);
                            if (btnInsertCheck == null)
                            {
                                return null;
                            }
                        }
                        else if (pbutton.isDeleted)
                        {
                            pButtonsDetailsIds.Add(pbutton.id, BusinessObjects.Models.btnType.ShowMessage.ToString());
                        }
                        else if (pbutton.updated == true)
                        {
                            BusinessObjects.Models.ShowMessageButton btnUpdateCheck = button.updateShowMessageButton(pbutton);
                            if (btnUpdateCheck == null)
                            {
                                return null;
                            }
                        }
                    }
                    int DeleteCheck = button.deleteButtonsConditional(pButtonsDetailsIds, "id");
                    if (DeleteCheck != 1)
                    {
                        return null;
                    }
                    scope.Complete();
                }
                return screen;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
        public bool checkIfScreenIsBusy(int pScreenId)
        {
            try
            {
                DataAccessLayer.DALScreen.DALScreen screenDAL = new DataAccessLayer.DALScreen.DALScreen();
                return screenDAL.checkIfScreenIsBusy(pScreenId);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return false;
            }
        }
        public bool checkIfScreenIsDeleted(int pScreenId)
        {
            try
            {
                DataAccessLayer.DALScreen.DALScreen screenDAL = new DataAccessLayer.DALScreen.DALScreen();
                return screenDAL.checkIfScreenIsDeleted(pScreenId);
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return false;
            }
        }
    }
}
