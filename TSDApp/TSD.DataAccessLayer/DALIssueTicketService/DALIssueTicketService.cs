using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace TSD.DataAccessLayer.IssueTicketType
{
    public class DALIssueTicketService
    {
        DBHelper.DALDBHelper dBHelper;
        public List<BusinessObjects.Models.IssueTicketService> SelectIssueTicketType()
        {
            try
            {
                List<BusinessObjects.Models.IssueTicketService> lstIssueTicketTypes = new List<BusinessObjects.Models.IssueTicketService>();
                dBHelper = new DBHelper.DALDBHelper();
                using (SqlConnection con = new SqlConnection(dBHelper.GetConnectionString()))
                {
                    SqlCommand go = new SqlCommand();
                    con.Open();
                    go.Connection = con;
                    go.CommandText = "SELECT id,name FROM tblService";

                    SqlDataReader reader = go.ExecuteReader();
                    while (reader.Read())
                    {
                        lstIssueTicketTypes.Add(new BusinessObjects.Models.IssueTicketService(
                            reader["id"] != null ? Convert.ToInt32(reader["id"]) : 0,
                            reader["Name"] != null ? Convert.ToString(reader["Name"]) : string.Empty));
                    }
                    con.Close();
                }
                return lstIssueTicketTypes;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessObjects.ExceptionsWriter.ExceptionsWriter();
                exceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
    }
}
