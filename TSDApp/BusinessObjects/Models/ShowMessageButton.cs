using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects.Models
{
    public class ShowMessageButton : Button

    {
        public string messageAR { get; set; }
        public string messageEN { get; set; }
        public ShowMessageButton()
        {
            id = 0;
            enName = string.Empty;
            arName = string.Empty;
            messageAR = string.Empty;
            messageEN = string.Empty;
            screenId = 0;
            type = "ShowMessage";
            updated = false;
            indexUpdated = -1;
            isDeleted = false;
        }
        public ShowMessageButton(int pid, string penName, string parName, string pmessageAR, string pmessageEN, int pdcreenId, bool pupdated = false, int pindexUpdated = -1)
        {
            id = pid;
            enName = penName;
            arName = parName;
            messageAR = pmessageAR;
            messageEN = pmessageEN;
            screenId = pdcreenId;
            type = "ShowMessage";
            updated = pupdated;
            indexUpdated = pindexUpdated;
            isDeleted = false;
        }
    }
}
