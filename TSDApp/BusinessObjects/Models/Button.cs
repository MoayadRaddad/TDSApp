using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSDApp.Models
{
    //Button model
    public class Button
    {
        public int id { get; set; }
        public string ENName { get; set; }
        public string ARName { get; set; }
        public string Type { get; set; }
        public string MessageAR { get; set; }
        public string MessageEN { get; set; }
        public int? issueTicketType { get; set; }
        public int ScreenId { get; set; }
        public bool Updated { get; set; }
        public int LstIndex { get; set; }
        public SqlDbType Name { get; set; }

        public Button()
        {
            id = 0;
            ENName = string.Empty;
            ARName = string.Empty;
            Type = string.Empty;
            MessageAR = string.Empty;
            MessageEN = string.Empty;
            issueTicketType = null;
            ScreenId = 0;
            Updated = false;
            LstIndex = -1;
        }
        public Button(int pId, string pENName, string pARName, string pType, string pMessageAR, string pMessageEN, int? pissueTicketType, int pScreenId, bool pUpdated = false, int pLstIndex = -1)
        {
            id = pId;
            ENName = pENName;
            ARName = pARName;
            Type = pType;
            MessageAR = pMessageAR;
            MessageEN = pMessageEN;
            issueTicketType = pissueTicketType;
            ScreenId = pScreenId;
            Updated = pUpdated;
            LstIndex = pLstIndex;
        }
    }
}
