using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAccessLayer.BALService
{
    public class BALService
    {
        public List<BusinessObjects.Models.Service> selectIssueTicketType()
        {
            try
            {
                DataAccessLayer.DALService.DALService service = new DataAccessLayer.DALService.DALService();
                return service.selectIssueTicketType();
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
    }
}
