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
    class AbortAPI_TB : BaseTest
    {
        LoginPage loginPage = new LoginPage(DriverAdministration.getDriver());
        DashBoard dashboardPage = new DashBoard(DriverAdministration.getDriver());
        EnumMapping enumMapping = new EnumMapping();


        [Test, TestCaseSource(typeof(TestDataMethods), nameof(TestDataMethods.LoginAdmin)), Order(1)]
        public void StartAPI_TB_Activity(string username, string password)
        {
            loginPage.Login(username, password);
            dashboardPage.StartAPIActivity(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB),
                "DemoAssay_003_ReagentTroughToPlate96", "");
            Assert.True(dashboardPage.IsProgressBarExist(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB)));
        }
        [Test, Order(2)]
        public void AssertAbortActivity()
        {
            //Assert Abort activity from dashboard
            dashboardPage.AbortApiActivity(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB));
            Assert.True(dashboardPage.IsStartAPIActivityBtnExist(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB)));
        }
    }
}
