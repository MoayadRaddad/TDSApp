using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Reflection;

namespace TSD.DataAccessLayer.DBHelper
{
    public static class DBHelper
    {
        private static string connectionString;
        public static string GetConnectionString()
        {
            return connectionString;
        }
        public static void SetConnectionString(string pConnectionString)
        {
            connectionString = pConnectionString;
        }

        public static int ExecuteNonQuery(string query, List<SqlParameter> parametros)
        {
            try
            {
                return NonQuery(query, parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static object ExecuteScalar(string query, List<SqlParameter> parametros)
        {
            try
            {
                return Scalar(query, parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region Private Methods

        private static int NonQuery(string query, List<SqlParameter> parametros)
        {
            try
            {
                DataSet dt = new DataSet();
                SqlConnection connection = new SqlConnection(connectionString);
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static object Scalar(string query, List<SqlParameter> parametros)
        {
            try
            {
                DataSet dt = new DataSet();
                SqlConnection connection = new SqlConnection(connectionString);
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }

}