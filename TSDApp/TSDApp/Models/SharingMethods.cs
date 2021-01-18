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
    public class SharingMethods
    {
        #region Usefull methods
        /// <summary>
        /// public method get exception,handle it and save it to log file with json type
        /// </summary>
        public void saveExceptionToLogFile(Exception ex)
        {
            BusinessCommon.ExceptionsWriter.ExceptionsWriter exceptionsWriter = new BusinessCommon.ExceptionsWriter.ExceptionsWriter();
            exceptionsWriter.saveExceptionToLogFile(ex);
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// Set column width
        /// </summary>
        /// <param name="pGridView"></param>
        /// <param name="pColumnNumber"></param>
        public void ChangeColumnWidth(DataGridView pGridView, int pColumnNumber)
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
                Models.SharingMethods sharingMethods = new Models.SharingMethods();
                sharingMethods.saveExceptionToLogFile(ex);
            }
        }
        public IEnumerable<T> GetIEnumrable<T>(List<T> pList)
        {
            List<T> ListT = pList;
            return ListT;
        }
        #endregion
    }
}