using NUnit.Framework;
using ServoFramework.Base;
using ServoFramework.Pages;
using ServoTestProject.Data;
using ServoTestProject.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServoTestProject.Tests.pauesResumeAbort
{
    public class PauseActivity : BaseTest
    {
        LoginPage loginPage = new LoginPage(DriverAdministration.getDriver());
        DashBoard dashboardPage = new DashBoard(DriverAdministration.getDriver());
        WorkCellsDetailsPage workCellsDetailsPage = new WorkCellsDetailsPage(DriverAdministration.getDriver());
        Calender calender = new Calender(DriverAdministration.getDriver());
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
        public void PauseActivityFromDashboard()
        {
            //Assert Activity has started on workcell page details
            dashboardPage.PauseResumeActivity(enumMapping.MapEnumToString(WorkCellNames.Test));
            Assert.That(dashboardPage.GetButtonStatus(enumMapping.MapEnumToString(WorkCellNames.Test)).Equals("resume icon"));
        }

        [Test, Order(3)]
        public void AssertActivityIsPausedInWorkCellPageDetails()
        {
            //Assert Activity has started on workcell page details
            dashboardPage.OpenWorkCellsDetailsPage(enumMapping.MapEnumToString(WorkCellNames.Test));
            Assert.AreEqual(dashboardPage.GetPageTitle(), enumMapping.MapEnumToString(PageTitles.WorkCellDetailsPage));
            Assert.AreEqual(workCellsDetailsPage.GetButtonStatus(), "resume icon");
        }
    }
}
