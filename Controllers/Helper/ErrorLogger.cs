using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace CRDownloader.Controllers.Helper
{
    public class ErrorLogger
    {
        public static void LogException(Exception ex)
        {
            using (StreamWriter file = new StreamWriter(""))
            {
                file.WriteLine();
                file.Close();
            }
        }
    }
}