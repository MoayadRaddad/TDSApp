using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    //Screen model
    public class Screen
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isActive { get; set; }
        public int bankId { get; set; }
        public Screen()
        {
            id = 0;
            name = string.Empty;
            isActive = false;
            bankId = 0;
        }
        public Screen(int pId, string pname, bool pisActive, int pbankId)
        {
            id = pId;
            name = pname;
            isActive = pisActive;
            bankId = pbankId;
        }
    }
}
