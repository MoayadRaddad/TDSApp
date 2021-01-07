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
        public string issueType { get; set; }
        public int ScreenId { get; set; }
        public bool Updated { get; set; }
        public SqlDbType Name { get; set; }

        public Button()
        {
            id = 0;
            ENName = string.Empty;
            ARName = string.Empty;
            Type = string.Empty;
            MessageAR = string.Empty;
            MessageEN = string.Empty;
            issueType = string.Empty;
            ScreenId = 0;
            Updated = false;
        }
        public Button(int pId, string pENName, string pARName, string pType, string pMessageAR, string pMessageEN, string pissueType, int pScreenId, bool pUpdated = false)
        {
            id = pId;
            ENName = pENName;
            ARName = pARName;
            Type = pType;
            MessageAR = pMessageAR;
            MessageEN = pMessageEN;
            issueType = pissueType;
            ScreenId = pScreenId;
            Updated = pUpdated;
        }
    }
}
