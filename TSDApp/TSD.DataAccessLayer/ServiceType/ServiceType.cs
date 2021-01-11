using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace TSD.DataAccessLayer.IssueTicketType
{
    public static class ServiceType
    {
        public static List<BusinessObjects.Models.IssueServiceType> SelectIssueTicketType()
        {
            try
            {
                List<BusinessObjects.Models.IssueServiceType> lstIssueTicketTypes = new List<BusinessObjects.Models.IssueServiceType>();
                using (SqlConnection con = new SqlConnection(DBHelper.DBHelper.GetConnectionString()))
                {
                    SqlCommand go = new SqlCommand();
                    con.Open();
                    go.Connection = con;
                    go.CommandText = "SELECT id,name FROM tblServiceType";

                    SqlDataReader reader = go.ExecuteReader();
                    while (reader.Read())
                    {
                        lstIssueTicketTypes.Add(new BusinessObjects.Models.IssueServiceType(
                            reader["id"] != null ? Convert.ToInt32(reader["id"]) : 0,
                            reader["Name"] != null ? Convert.ToString(reader["Name"]) : string.Empty));
                    }
                    con.Close();
                }
                return lstIssueTicketTypes;
            }
            catch (Exception ex)
            {
                BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
                return null;
            }
        }
    }
}
