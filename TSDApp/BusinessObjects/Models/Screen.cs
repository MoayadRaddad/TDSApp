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
        public bool isActive { get; set; }
        public int BankId { get; set; }
        public Screen()
        {
            id = 0;
            Name = string.Empty;
            isActive = false;
            BankId = 0;
        }
        public Screen(int pId, string pName, bool pisActive, int pBankId)
        {
            id = pId;
            Name = pName;
            isActive = pisActive;
            BankId = pBankId;
        }
    }
}
