using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSDApp.Models
{
    public class Button
    {
        public int id { get; set; }
        public string ENName { get; set; }
        public string ARName { get; set; }
        public string Type { get; set; }
        public string MessageAR { get; set; }
        public string MessageEN { get; set; }
        public string issueType { get; set; }
        public int ScreenId { get; set; }
        public Button(int pId, string pENName, string pARName, string pType, string pMessageAR, string pMessageEN, string pissueType, int pScreenId)
        {
            id = pId;
            ENName = pENName;
            ARName = pARName;
            Type = pType;
            MessageAR = pMessageAR;
            MessageEN = pMessageEN;
            issueType = pissueType;
            ScreenId = pScreenId;
        }
    }
}
