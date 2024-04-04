using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ServoFramework.ProjectConstants
{
    public class Constants
    {
        private readonly static int _implictWait = 30;
        private readonly static int _explicitWait = 30;
        private readonly static int _fluentWait = 60;
        private readonly static int _polliingInterval = 2;
        private readonly static string URL = "https://neurocpweb.azurewebsites.net/";
        private readonly static string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        private readonly static string _currentDateNtime = string.Format("{0:yyyy_mm_dd hh_mm_ss}", DateTime.Now);



        public static int GetExplicitWait()
        {
            return _explicitWait;
        }
        public static string GetURL()
        {
            return URL;
        }
        public static string OpenDataFile(string fileName)
        {
            return projectDirectory + "//Data//" + fileName;
        }
        public static string ReportsFolder()
        {
            return projectDirectory + "//Attachments//Reports//";
        }
        public static string LogFolder()
        {
            return projectDirectory + "//Attachments//Logs//";
        }
        public static string ScreenshotFolder()
        {
            return projectDirectory + "//Attachments//Screenshots//";
        }
        public string GetProjectPath()
        {
            return projectDirectory;
        }
        public static int GetFluentWait()
        {
            return _fluentWait;
        }
        public static int GetPollingInterval()
        {
            return _polliingInterval;
        }
        public static int GetImplictWait()
        {
            return _explicitWait;
        }
        public static string getCuurentDateNtime()
        {
            return _currentDateNtime;
        }
    }
}
