using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using ServoFramework.Base;
using ServoFramework.ProjectConstants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ServoFramework.Helpers
{
    public class ExtentReporterHelper
    {
        public static ExtentReports extent;
        public static ExtentTest test;
        public ExtentReporterHelper()
        {

        }
        public static void setupReport()
        {
            string _reportFile = Constants.getCuurentDateNtime();
            //string _reportFile = string.Format("{0:yyyy_mm_dd hh_mm_ss}", DateTime.Now);
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(Constants.ReportsFolder()+_reportFile+".html");
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host Name", "Local host");
            extent.AddSystemInfo("Environment", "Quality Team");
            extent.AddSystemInfo("Username", "Eslam Fawzy");
        }

        public static void MakeReportOnMethod()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name); //Name the test in the method with the test name
        }
        public static void ReportFaliureResults()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace; //Log of your error
            String fileName = Constants.getCuurentDateNtime()+"Reprot";
            if (status == TestStatus.Failed)
            {
                test.Fail("Test failed", captureScreenShot(DriverAdministration.getDriver(), fileName));
                test.Log(Status.Fail, "test failed with logtrace" + stackTrace);
            }
            else if (status == TestStatus.Passed)
            {
                test.Pass("Test Passed", captureScreenShot(DriverAdministration.getDriver(), fileName));
            }
            LogHelper.Write("The Test has ended");
            extent.Flush();
        }

        private static MediaEntityModelProvider captureScreenShot(IWebDriver driver, String screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = Regex.Replace(ts.GetScreenshot().AsBase64EncodedString, "'[^']+'(?=!MyUDF)", "");
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }
    }
}
