using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Text;
using WebDriverManager.DriverConfigs.Impl;

namespace ServoFramework.Base
{
    public class BrowserFactory
    {
        //string browserName;

        protected void InitBrowser(BrowserType browserType = BrowserType.Chrome)
        {
            //browserName = TestContext.Parameters["browserName"];
            //if (browserName == null)
            //{
            //    //if I didn't give you a browsername in command line, go an get it from terminal
            //    browserName = System.Configuration.ConfigurationManager.AppSettings["browser"];
            //}

            switch (browserType)
            {
                case BrowserType.FireFox:
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    DriverAdministration.setDriver(new FirefoxDriver());
                    break;
                case BrowserType.Chrome:
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    DriverAdministration.setDriver(new ChromeDriver());
                    break;
                case BrowserType.Edge:
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    DriverAdministration.setDriver(new EdgeDriver());
                    break;
            }
        }
    }
    public enum BrowserType
    {
        Chrome,
        FireFox,
        Edge
    }
}
