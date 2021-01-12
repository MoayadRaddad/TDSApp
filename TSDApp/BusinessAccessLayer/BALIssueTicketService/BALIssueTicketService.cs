using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAccessLayer.IssueTicketType
{
    public class BALIssueTicketService
    {
        public List<BusinessObjects.Models.IssueTicketService> SelectIssueTicketType()
        {
            try
            {
                TSD.DataAccessLayer.IssueTicketType.DALIssueTicketService service = new TSD.DataAccessLayer.IssueTicketType.DALIssueTicketService();
                return service.SelectIssueTicketType();
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessObjects.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
    }
}
