using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Reflection;
using System.IO;

namespace DataAccessLayer.DALDBHelper
{
    public class DALDBHelper
    {
        public int executeNonQuery(string query, List<SqlParameter> parametros)
        {
            try
            {
                return nonQuery(query, parametros);
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public object executeScalar(string query, List<SqlParameter> parametros)
        {
            try
            {
                return scalar(query, parametros);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public DataSet executeAdapter( string commandText, List<SqlParameter> commandParameters)
        {
            try
            {
                var cmd = new SqlCommand();
                SqlConnection con = new SqlConnection(BusinessCommon.ConnectionString.ConnectionString.connectionString);
                cmd.Connection = con;
                cmd.CommandText = commandText;
                foreach (SqlParameter param in commandParameters)
                {
                    cmd.Parameters.Add(param);
                }
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                return dataSet;
            }
            catch(Exception)
            {
                return null;
            }
        }
        #region Private Methods
        private int nonQuery(string query, List<SqlParameter> parametros)
        {
            try
            {
                SqlConnection connection = new SqlConnection(BusinessCommon.ConnectionString.ConnectionString.connectionString);
                SqlCommand command = new SqlCommand();

                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = query;
                    command.Parameters.AddRange(parametros.ToArray());
                    return command.ExecuteNonQuery();

                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }

            }
            catch (Exception)
            {
                return -1;
            }
        }
        private object scalar(string query, List<SqlParameter> parametros)
        {
            try
            {
                SqlConnection connection = new SqlConnection(BusinessCommon.ConnectionString.ConnectionString.connectionString);
                SqlCommand command = new SqlCommand();

                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = query;
                    command.Parameters.AddRange(parametros.ToArray());
                    return command.ExecuteScalar();

                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }

            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }

}