using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    //Bank model
    public class Bank
    {
        public int id { get; set; }
        public string name { get; set; }
        public Bank()
        {
            id = 0;
            name = string.Empty;
        }
        public Bank (int pid, string pname)
        {
            id = pid;
            name = pname;
        }
    }
}
