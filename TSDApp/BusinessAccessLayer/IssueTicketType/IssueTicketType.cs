using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAccessLayer.IssueTicketType
{
    public static class IssueTicketType
    {
        public static List<BusinessObjects.Models.IssueServiceType> SelectIssueTicketType()
        {
            try
            {
                return TSD.DataAccessLayer.IssueTicketType.ServiceType.SelectIssueTicketType();
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
    }
}
