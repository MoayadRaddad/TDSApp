using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects.Models
{
    public enum btnType
    {
        ShowMessage,
        IssueTicket
    }
    public class Button
    {
        public int id { get; set; }
        public string enName { get; set; }
        public string arName { get; set; }
        public int screenId { get; set; }
        public string type { get; set; }
        public bool updated { get; set; }
        public int indexUpdated { get; set; }
        public Button(){}
        public Button(int pid, string penName, string parName, int pscreenId, string ptype)
        {
            id = pid;
            enName = penName;
            arName = parName;
            screenId = pscreenId;
            type = ptype;
        }
    }
}
