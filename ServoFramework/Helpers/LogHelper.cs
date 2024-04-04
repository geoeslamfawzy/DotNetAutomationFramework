using NUnit.Framework;
using OpenQA.Selenium;
using ServoFramework.ProjectConstants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ServoFramework.Helpers
{
    public class LogHelper
    {
        private static StreamWriter _streamWriter = null;

        public static void CreateLogFile()
        {
            var _logFileName = TestContext.CurrentContext.Test.Name + " " + Constants.getCuurentDateNtime();
            string dir = Constants.LogFolder();
            try
            {
                _streamWriter = File.AppendText(dir + _logFileName + ".log");
            }
            catch (Exception)
            {
                Directory.CreateDirectory(dir);
                _streamWriter = File.AppendText(dir + _logFileName + ".log");
            }
        }

        //Create a method which can InsertTexts the text in the log file
        public static void Write(string logMessage)
        {
            _streamWriter.Write("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            _streamWriter.WriteLine("    {0}", logMessage);
            _streamWriter.Flush();
        }
    }
}
