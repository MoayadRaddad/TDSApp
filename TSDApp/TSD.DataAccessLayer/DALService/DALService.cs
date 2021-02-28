using BusinessCommon.ExceptionsWriter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.DALService
{
    public class DALService
    {
        public List<BusinessObjects.Models.Service> selectIssueTicketType()
        {
            try
            {
                List<BusinessObjects.Models.Service> lstIssueTicketTypes = new List<BusinessObjects.Models.Service>();
                string pquery = "SELECT id,enName FROM tblService";
                DALDBHelper.DALDBHelper dBHelper = new DALDBHelper.DALDBHelper();
                DataSet dataSet = dBHelper.executeAdapter(pquery, new List<SqlParameter>());
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    lstIssueTicketTypes.Add(new BusinessObjects.Models.Service(Convert.ToInt32(dataRow["id"]), dataRow["enName"].ToString()));
                }
                return lstIssueTicketTypes;
            }
            catch (Exception ex)
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                return null;
            }
        }
    }
}
