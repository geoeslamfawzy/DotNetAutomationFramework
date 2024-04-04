using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using ServoFramework.Base;
using ServoFramework.ProjectConstants;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace ServoFramework.Helpers
{
    public class ScreenshotHelper
    {
        public static void TakeScreenshot()
        {
            var _screenName = TestContext.CurrentContext.Test.Name + " " + Constants.getCuurentDateNtime();
            var dir = Constants.ScreenshotFolder();
            string scName = Constants.ScreenshotFolder() + _screenName + ".png";
            try
            {
                ((ITakesScreenshot)DriverAdministration.getDriver()).GetScreenshot().SaveAsFile(scName.Replace('"', ' '), ScreenshotImageFormat.Png);
            }
            catch (Exception)
            {
                Directory.CreateDirectory(dir);
                ((ITakesScreenshot)DriverAdministration.getDriver()).GetScreenshot().SaveAsFile(scName.Replace('"', ' '), ScreenshotImageFormat.Png);
            }
        }
    }
}
