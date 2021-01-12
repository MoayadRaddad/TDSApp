﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessObjects.ExceptionsWriter
{
    public class ExceptionsWriter
    {
        /// <summary>
        /// public method get exception,handle it and save it to log file with json type
        /// </summary>
        public void SaveExceptionToLogFile(Exception ex)
        {
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "Exceptions.json";
            using (StreamWriter writer = File.AppendText(filePath))
            {
                while (ex != null)
                {
                    TSDApp.Models.ApplicationException applicationException = new TSDApp.Models.ApplicationException();
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
    }
}
