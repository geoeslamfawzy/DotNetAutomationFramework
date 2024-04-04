using NUnit.Framework;
using ServoFramework.Base;
using ServoFramework.Pages;
using ServoTestProject.Data;
using ServoTestProject.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServoTestProject.Tests.pauesResumeAbort.Dashboard
{
    class AbortAPI_FIFO : BaseTest
    {
        LoginPage loginPage = new LoginPage(DriverAdministration.getDriver());
        DashBoard dashboardPage = new DashBoard(DriverAdministration.getDriver());
        EnumMapping enumMapping = new EnumMapping();


        [Test, TestCaseSource(typeof(TestDataMethods), nameof(TestDataMethods.LoginAdmin)), Order(1)]
        public void startAPIFIFOActivity(string username, string password)
        {
            loginPage.Login(username, password);
            dashboardPage.StartAPIActivity(enumMapping.MapEnumToString(WorkCellNames.Test),
                "DemoAssay_005_Aliquote_TroughTo12ColMTP", "");
            Assert.That(dashboardPage.GetButtonStatus(enumMapping.MapEnumToString(WorkCellNames.Test)).Equals("pause icon"));
        }
        [Test, Order(2)]
        public void AssertAbortActivity()
        {
            //Assert Abort activity from dashboard
            dashboardPage.AbortApiActivity(enumMapping.MapEnumToString(WorkCellNames.Test));
            Assert.True(dashboardPage.IsStartAPIActivityBtnExist(enumMapping.MapEnumToString(WorkCellNames.Test)));
        }
    }
}
