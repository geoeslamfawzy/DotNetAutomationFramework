using OpenQA.Selenium.Chrome;
using ServoFramework.Base;
using ServoFramework.ProjectConstants;
using System;
using System.Collections.Generic;
using System.Text;
using WebDriverManager.DriverConfigs.Impl;

namespace ServoFramework.Driver
{
    public class DriverActions : BrowserFactory
    {
        public void InitDriver()
        {
            if(DriverAdministration.getDriver() == null)
            {
                InitBrowser(BrowserType.Chrome);
                DriverAdministration.getDriver().Manage().Window.Maximize();
                DriverAdministration.getDriver().Url = Constants.GetURL();
            }
        }

        public void quitDriver()
        {
            if(DriverAdministration.getDriver() != null)
            {
                DriverAdministration.getDriver().Quit();
                DriverAdministration.unloadDriver();
            }
        }
    }
}
