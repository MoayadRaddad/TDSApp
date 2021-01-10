using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects.Models
{
    public class IssueTicketType
    {
        public int id { get; set; }
        public string Name { get; set; }
        public IssueTicketType()
        {
            id = 0;
            Name = string.Empty;
        }
        public IssueTicketType(int pId, string pName)
        {
            id = pId;
            Name = pName;
        }
    }
}
