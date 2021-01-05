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
    //public class to share some usefull methods across forms
    public class SharingMethods
    {
        //public method get exception,handle it and save it to log file with json type
        public static void SaveExceptionToLogFile(Exception ex)
        {
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "Exceptions.json";
            using (StreamWriter writer = File.AppendText(filePath))
            {
                while (ex != null)
                {
                    ApplicationException applicationException = new ApplicationException();
                    applicationException.Date = DateTime.Now;
                    applicationException.Type = ex.GetType().FullName;
                    applicationException.Message = ex.Message;
                    applicationException.StackTrace = ex.StackTrace;
                    var JException = JsonConvert.SerializeObject(applicationException);
                    writer.WriteLine(JException);
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ex = ex.InnerException;
                }
            }
        }
        //public method get read connection string from file and get it
        public static string SetConnectionString()
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
                    return ConnectionString;
                }
                else
                {
                    MessageBox.Show("The File for connection sting is missing or not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                Models.SharingMethods.SaveExceptionToLogFile(ex);
                return string.Empty;
            }
        }
    }
}
