using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAccessLayer.BALService
{
    public class BALService
    {
        public List<BusinessObjects.Models.Service> SelectIssueTicketType()
        {
            try
            {
                DataAccessLayer.DALService.DALService service = new DataAccessLayer.DALService.DALService();
                return service.SelectIssueTicketType();
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
    }
}
