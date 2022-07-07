using System;
using System.IO;

namespace Admin.Utilities
{
	public class Logger
    {
        public static void WriteLog(Exception ex)
        {
            string filePath = GetErrorLogFilePath();

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                   "" + Environment.NewLine + "Date :" + DateTime.UtcNow.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }
        }

        private static string GetErrorLogFilePath()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "ErrorLogs\\";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            filePath = filePath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
            }

            return filePath;
        }
    }
}
