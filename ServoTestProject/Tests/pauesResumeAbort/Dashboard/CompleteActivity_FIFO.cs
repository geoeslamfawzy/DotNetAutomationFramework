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
    public class CompleteActivity_FIFO : BaseTest
    {
        LoginPage loginPage = new LoginPage(DriverAdministration.getDriver());
        DashBoard dashboardPage = new DashBoard(DriverAdministration.getDriver());
        WorkCellsDetailsPage workCellsDetailsPage = new WorkCellsDetailsPage(DriverAdministration.getDriver());
        Calender calender = new Calender(DriverAdministration.getDriver());
        EnumMapping enumMapping = new EnumMapping();
        static string date;

        [Test, TestCaseSource(typeof(TestDataMethods), nameof(TestDataMethods.LoginAdmin)), Order(1)]
        public void startNonAPIFIFOActivity(string username, string password)
        {
            loginPage.Login(username, password);
            dashboardPage.StartNonApiActivity(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_FIFO), "Method2", 313, "WL1");
            date = DateTime.Now.ToString("MM/dd/yyy h:mm");
            Assert.AreEqual(dashboardPage.GetNonAPIActivityStatus(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_FIFO)),
               enumMapping.MapEnumToString(ActivityStatus.Running));
        }

        [Test, Order(2)]
        public void CompleteActivityFromDashboard()
        {
            dashboardPage.CompleteActivity(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_FIFO));
            //Assert.That(dashboardPage.GetToastMessage().Contains(enumMapping.MapEnumToString(ActivityStatus.Completed)));
        }

        [Test, Order(3)]
        public void AssertThatActivityCompletedOnWorkcellDetailsPage()
        {
            dashboardPage.OpenWorkCellsDetailsPage(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_FIFO));
            Assert.AreEqual(dashboardPage.GetPageTitle(), enumMapping.MapEnumToString(PageTitles.WorkCellDetailsPage));
            Assert.That(workCellsDetailsPage.GetActivityStatueFromHistory(date), Is.EqualTo(enumMapping.MapEnumToString(ActivityStatus.Completed)));
        }
        [Test, Order(4)]
        public void AssertThatActivityCompletedOnCalendar()
        {
            //This test will be failed when we create the schedule userstory
            dashboardPage.NavigateToCalender();
            Assert.That(calender.GetWorkCellStatus(enumMapping.MapEnumToString(WorkCellNames.NON_API_LH_Automation_FIFO)),
                Is.EqualTo(enumMapping.MapEnumToString(ActivityStatus.Completed)));
        }

    }
}
