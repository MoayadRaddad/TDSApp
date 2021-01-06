using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSDApp.Models
{
    //Bank model
    public class Bank
    {
        public int id { get; set; }
        public string Name { get; set; }
        public Bank()
        {
            id = 0;
            Name = string.Empty;
        }
        public Bank (int pId, string pName)
        {
            id = pId;
            Name = pName;
        }
    }
}
