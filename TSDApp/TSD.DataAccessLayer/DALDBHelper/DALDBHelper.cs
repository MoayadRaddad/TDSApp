using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Reflection;
using System.IO;

namespace TSD.DataAccessLayer.DBHelper
{
    public class DALDBHelper
    {
        private static string connectionString = string.Empty;
        public string GetConnectionString()
        {
            return connectionString;
        }
        public int SetConnectionString()
        {
            try
            {
                string txtpath = System.AppDomain.CurrentDomain.BaseDirectory + "ConnectionString.txt";
                if (File.Exists(txtpath))
                {
                    using (StreamReader reader = new StreamReader(txtpath))
                    {
                        if (reader.Peek() >= 0)
                        {
                            connectionString = reader.ReadLine();
                        }
                    }
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExecuteNonQuery(string query, List<SqlParameter> parametros)
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

        public object ExecuteScalar(string query, List<SqlParameter> parametros)
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
        public DataSet ExecuteAdapter( string commandText, List<SqlParameter> commandParameters)
        {
            try
            {
                DALDBHelper dBHelper = new DALDBHelper();
                var cmd = new SqlCommand();
                SqlConnection con = new SqlConnection(dBHelper.GetConnectionString());
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
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #region Private Methods

        private int NonQuery(string query, List<SqlParameter> parametros)
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

        private object Scalar(string query, List<SqlParameter> parametros)
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