using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessCommon.ExceptionsWriter
{
    public class ExceptionsWriter
    {
        /// <summary>
        /// public method get exception,handle it and save it to log file with json type
        /// </summary>
        public static void saveExceptionToLogFile(Exception ex)
        {
            try
            {
                string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "Exceptions.json";
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    while (ex != null)
                    {
                        BusinessObjects.Models.ApplicationException applicationException = new BusinessObjects.Models.ApplicationException();
                        applicationException.Date = DateTime.Now;
                        applicationException.Type = ex.GetType().FullName;
                        applicationException.Message = ex.Message;
                        applicationException.StackTrace = ex.StackTrace;
                        var JException = JsonConvert.SerializeObject(applicationException);
                        writer.WriteLine(JException);
                        ex = ex.InnerException;
                    }
                }
            }
            catch (Exception exception)
            {
                saveExceptionToLogFile(exception);
                throw exception;
            }
        }
    }
}
