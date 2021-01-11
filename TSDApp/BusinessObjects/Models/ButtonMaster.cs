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
    public class ButtonMaster
    {
        public int id { get; set; }
        public string ENName { get; set; }
        public string ARName { get; set; }
        public int ScreenId { get; set; }
        public string Type { get; set; }
        public bool Updated { get; set; }
        public int indexUpdated { get; set; }
        public ButtonMaster(){}
        public ButtonMaster(int pId, string pENName, string pARName, int pScreenId, string pType)
        {
            id = pId;
            ENName = pENName;
            ARName = pARName;
            ScreenId = pScreenId;
            Type = pType;
        }
    }
}
