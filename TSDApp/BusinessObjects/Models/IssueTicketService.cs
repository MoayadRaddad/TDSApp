using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects.Models
{
    public class IssueTicketService
    {
        public int id { get; set; }
        public string name { get; set; }
        public IssueTicketService()
        {
            id = 0;
            name = string.Empty;
        }
        public IssueTicketService(int pId, string pname)
        {
            id = pId;
            name = pname;
        }
    }
}
