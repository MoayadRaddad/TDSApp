using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects.Models
{
    public class IssueServiceType
    {
        public int id { get; set; }
        public string Name { get; set; }
        public IssueServiceType()
        {
            id = 0;
            Name = string.Empty;
        }
        public IssueServiceType(int pId, string pName)
        {
            id = pId;
            Name = pName;
        }
    }
}
