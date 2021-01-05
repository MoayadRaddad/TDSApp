using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSDApp.Models
{
    //Screen model
    public class Screen
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string isActive { get; set; }
        public int BankId { get; set; }
        public Screen(int pId, string pName, string pisActive, int pBankId)
        {
            id = pId;
            Name = pName;
            isActive = pisActive;
            BankId = pBankId;
        }
    }
}
