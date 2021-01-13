using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.DALService
{
    public class DALService
    {
        public List<BusinessObjects.Models.Service> SelectIssueTicketType()
        {
            try
            {
                List<BusinessObjects.Models.Service> lstIssueTicketTypes = new List<BusinessObjects.Models.Service>();
                string pquery = "SELECT id,name FROM tblService";
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.ExecuteAdapter(pquery, new List<SqlParameter>());
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    lstIssueTicketTypes.Add(new BusinessObjects.Models.Service(Convert.ToInt32(dataRow["id"]), dataRow["Name"].ToString()));
                }
                return lstIssueTicketTypes;
            }
            catch (Exception ex)
            {
                BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
    }
}
