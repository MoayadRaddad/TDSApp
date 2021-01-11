using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects.Models
{
    public class ShowMessage : ButtonMaster

    {
        public string MessageAR { get; set; }
        public string MessageEN { get; set; }
        public ShowMessage()
        {
            id = 0;
            ENName = string.Empty;
            ARName = string.Empty;
            MessageAR = string.Empty;
            MessageEN = string.Empty;
            ScreenId = 0;
            Type = "ShowMessage";
            Updated = false;
            indexUpdated = -1;
        }
        public ShowMessage(int pId, string pENName, string pARName, string pMessageAR, string pMessageEN, int pScreenId, bool pUpdated = false, int pindexUpdated = -1)
        {
            id = pId;
            ENName = pENName;
            ARName = pARName;
            MessageAR = pMessageAR;
            MessageEN = pMessageEN;
            ScreenId = pScreenId;
            Type = "ShowMessage";
            Updated = pUpdated;
            indexUpdated = pindexUpdated;
        }
    }
}
