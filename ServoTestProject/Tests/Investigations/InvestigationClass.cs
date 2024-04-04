using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using ServoFramework.Base;
using ServoFramework.Pages;
using ServoTestProject.Data;
using System;
using System.Collections.Generic;
using System.Text;
using WebDriverManager.DriverConfigs.Impl;

namespace ServoTestProject.Tests.Investigations
{
    public class InvestigationClass
    {
        private IWebDriver _driver;
        public string homeURL;
        [Test]
        public void InvestigateLogin()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            ChromeDriver driver = new ChromeDriver();
            homeURL = "https://neurocpweb.azurewebsites.net/";
            driver.Navigate().GoToUrl(homeURL);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }
    }
}
