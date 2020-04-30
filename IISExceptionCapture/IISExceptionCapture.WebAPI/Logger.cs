using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace IISExceptionCapture.WebAPI
{
    public class Logger
    {
        private static readonly string LoggingFileName = $@"C:\Logging\IISExceptionCapture-error-{DateTime.Now.ToShortDateString()}.data";

        public static void WriteToFile(string message, Exception exception = null)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(LoggingFileName));

                string exceptionStacktrace = exception == null ? "" : exception.ToString();

                string textToAppend = $@"Timestamp:
   [{DateTime.Now.ToString()}]
Message: 
   {message}
Inner exception message and stacktrace:
   {exceptionStacktrace}
{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}";

                File.AppendAllText(LoggingFileName, textToAppend);
            }
            catch (Exception ex)
            {
            }
        }
    }
}