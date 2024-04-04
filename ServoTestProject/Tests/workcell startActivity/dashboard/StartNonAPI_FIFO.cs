using NUnit.Framework;
using ServoFramework.Base;
using ServoFramework.Helpers;
using ServoFramework.Pages;
using ServoFramework.ProjectConstants;
using ServoTestProject.Data;
using ServoTestProject.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServoTestProject.Tests.Dashboard
{
    class StartNonAPI_FIFO : BaseTest
    {
        LoginPage loginPage = new LoginPage(DriverAdministration.getDriver());
        DashBoard dashboardPage = new DashBoard(DriverAdministration.getDriver());
        WorkCellsDetailsPage WorkCellsDetailsPage = new WorkCellsDetailsPage(DriverAdministration.getDriver());
        Calender calender = new Calender(DriverAdministration.getDriver()); 
        EnumMapping enumMapping = new EnumMapping();


        [Test, TestCaseSource(typeof(TestDataMethods), nameof(TestDataMethods.LoginAdmin)), Order(1)]
        public void startNonAPIFIFOActivity(string username, string password)
        {
            loginPage.Login(username, password);
            //Start Activity
            dashboardPage.StartNonApiActivity(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_FIFO), "Method2", 313, "WL1");
            //Assert Activity has started on dashboard
            Assert.AreEqual(dashboardPage.GetNonAPIActivityStatus(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_FIFO)),
               enumMapping.MapEnumToString(ActivityStatus.Running));
        }
        [Test, Order(2)]
        public void AssertActivityRunInWorkCellPageDetails()
        {
            //Assert Activity has started on workcell page details
            dashboardPage.OpenWorkCellsDetailsPage(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_FIFO));
            Assert.AreEqual(dashboardPage.GetPageTitle(), enumMapping.MapEnumToString(PageTitles.WorkCellDetailsPage));
            Assert.AreEqual(WorkCellsDetailsPage.GetNonAPIActivityStatus(), enumMapping.MapEnumToString(ActivityStatus.Running));
        }
        [Test, Order(3)]
        public void AssertActivityRunInCalendar()
        {
            //Assert Activity has started on calendar
            WorkCellsDetailsPage.NavigateToCalender();
            Assert.AreEqual(calender.GetPageTitle(), enumMapping.MapEnumToString(PageTitles.Calendar));
            Assert.AreEqual(calender.GetWorkCellStatus(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_FIFO), ActivityStatus.Running), enumMapping.MapEnumToString(ActivityStatus.Running));
        }

        [Test, Order(4)]
        public void AssertAbortActivity()
        {
            //Assert Abort activity from dashboard
            calender.NavigateToDashboard();
            dashboardPage.AbortNonApiActivity(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_FIFO));
            Assert.True(dashboardPage.IsStartNonAPIActivityBtnExist(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_FIFO)));
        }
    }
}
