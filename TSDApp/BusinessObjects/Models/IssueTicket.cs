using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects.Models
{
    public class IssueTicket : ButtonMaster
    {
        public int SreviceType { get; set; }
        public IssueTicket()
        {
            id = 0;
            ENName = string.Empty;
            ARName = string.Empty;
            SreviceType = 0;
            ScreenId = 0;
            Type = "IssueTicket";
            Updated = false;
            indexUpdated = -1;
        }
        public IssueTicket(int pId, string pENName, string pARName, int pSreviceType, int pScreenId, bool pUpdated = false, int pindexUpdated = -1)
        {
            id = pId;
            ENName = pENName;
            ARName = pARName;
            SreviceType = pSreviceType;
            ScreenId = pScreenId;
            Type = "IssueTicket";
            Updated = pUpdated;
            indexUpdated = pindexUpdated;
        }
    }
}
