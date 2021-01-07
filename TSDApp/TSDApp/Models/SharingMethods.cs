using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSDApp.Models
{
    /// <summary>
    /// public class to share some usefull methods across forms
    /// </summary>
    public static class SharingMethods
    {
        #region Usefull methods
        /// <summary>
        /// public method get exception,handle it and save it to log file with json type
        /// </summary>
        public static void SaveExceptionToLogFile(Exception ex)
        {
            BusinessObjects.ExceptionsWriter.ExceptionsWriter.SaveExceptionToLogFile(ex);
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// public method get read connection string from file and get it
        /// </summary>
        public static void SetConnectionString()
        {
            try
            {
                string txtpath = System.AppDomain.CurrentDomain.BaseDirectory + "ConnectionString.txt";
                string ConnectionString = "";
                //Check if file exist
                if (File.Exists(txtpath))
                {
                    //Reader to read file
                    using (StreamReader reader = new StreamReader(txtpath))
                    {
                        if (reader.Peek() >= 0)
                        {
                            ConnectionString = reader.ReadLine();
                        }
                    }
                    BusinessAccessLayer.ConnectionString.ConnectionString.SetConnectionString(ConnectionString);
                }
                else
                {
                    MessageBox.Show("The File for connection sting is missing or not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        /// <summary>
        /// Set column width
        /// </summary>
        /// <param name="pGridView"></param>
        /// <param name="pColumnNumber"></param>
        public static void ChangeColumnWidth(DataGridView pGridView, int pColumnNumber)
        {
            try
            {
                DataGridViewColumn column = null;
                for (int columnIndex = 0; columnIndex < pGridView.ColumnCount; columnIndex++)
                {
                    column = pGridView.Columns[columnIndex];
                    column.Width = (pGridView.Width / pColumnNumber) - (44 / pColumnNumber);
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
            }
        }
        public static IEnumerable<T> GetIEnumrable<T>(List<T> pList)
        {
            List<T> ListT = pList;
            return ListT;
        }
        #endregion
    }
}