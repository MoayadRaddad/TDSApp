using BusinessCommon.ExceptionsWriter;
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
        public static void saveExceptionToLogFile(Exception ex)
        {
            try
            {
                ExceptionsWriter.saveExceptionToLogFile(ex);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception exception)
            {
                ExceptionsWriter.saveExceptionToLogFile(exception);
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
                ExceptionsWriter.saveExceptionToLogFile(ex);
            }
        }
        #endregion
    }
}