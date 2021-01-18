using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects.Models
{
    public class IssueTicketButton : Button
    {
        public int serviceId { get; set; }
        public IssueTicketButton()
        {
            id = 0;
            enName = string.Empty;
            arName = string.Empty;
            serviceId = 0;
            screenId = 0;
            type = "IssueTicket";
            updated = false;
            indexUpdated = -1;
            isDeleted = false;
        }
        public IssueTicketButton(int pid, string penName, string parName, int pserviceId, int pscreenId, bool pupdated = false, int pindexUpdated = -1)
        {
            id = pid;
            enName = penName;
            arName = parName;
            serviceId = pserviceId;
            screenId = pscreenId;
            type = "IssueTicket";
            updated = pupdated;
            indexUpdated = pindexUpdated;
            isDeleted = false;
        }
    }
}
