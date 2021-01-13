using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects.Models
{
    public class Service
    {
        public int id { get; set; }
        public string name { get; set; }
        public Service()
        {
            id = 0;
            name = string.Empty;
        }
        public Service(int pId, string pname)
        {
            id = pId;
            name = pname;
        }
    }
}
