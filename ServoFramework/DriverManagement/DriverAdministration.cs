using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using WebDriverManager.DriverConfigs.Impl;

namespace ServoFramework.Base
{
    public class DriverAdministration 
    {
        private static ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        public DriverAdministration()
        {

        }
        public static IWebDriver getDriver()
        {
            return driver.Value;
        }
        public static void unloadDriver()
        {
            driver.Dispose();
        }
        public static void setDriver(IWebDriver driverRef)
        {
            driver.Value = driverRef;
        }

    }
}
