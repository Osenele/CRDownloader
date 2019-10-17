using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;

namespace CRDownloader.Controllers.Helper
{
    public class ErrorLogger
    {
        private static string LogFilePath = @ConfigurationManager.AppSettings["LogFilePath"];
        public static void LogException(Exception ex)
        {
            using (StreamWriter file = new StreamWriter(LogFilePath))
            {
                file.WriteLine();
                file.Close();
            }
        }
    }
}