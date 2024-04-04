using NUnit.Framework;
using OpenQA.Selenium;
using ServoFramework.Base;
using ServoFramework.Pages;
using ServoTestProject.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServoTestProject.Tests.Investigations
{
    [Parallelizable(ParallelScope.All)]
    public class ServoInvestigation : BaseTest
    {
        string workCellName = "Eslam_Demo_Auto" + new Random().Next();
        string viewName = "Eslam_Demo_Auto" + new Random().Next();
        string duration = "1";
        string jobName = "Eslam_Demo_Auto" + new Random().Next();
        [Test]
        [Parallelizable(ParallelScope.All), TestCaseSource(typeof(TestDataMethods), nameof(TestDataMethods.LoginAdmin))]
        public void AddMethod(string username, string password)
        {
            LoginPage loginPage = new LoginPage(DriverAdministration.getDriver());
            loginPage.Login(username, password);
            DriverAdministration.getDriver().FindElement(By.XPath("//a[contains(text(),'LH-Automation FIFO')]/parent::div/preceding::button/preceding::button[@icon='fa fa-play']")).Click();
        }
    }
}
