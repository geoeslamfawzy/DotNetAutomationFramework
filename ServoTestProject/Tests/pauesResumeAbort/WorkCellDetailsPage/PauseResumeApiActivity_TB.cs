using NUnit.Framework;
using ServoFramework.Base;
using ServoFramework.Pages;
using ServoTestProject.Data;
using ServoTestProject.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ServoTestProject.Tests.pauesResumeAbort.WorkCellDetailsPage
{
    public class PauseAPI_TB_Activity : BaseTest
    {
        public class StartAPI_TB : BaseTest
        {
            LoginPage loginPage = new LoginPage(DriverAdministration.getDriver());
            DashBoard dashboardPage = new DashBoard(DriverAdministration.getDriver());
            WorkCellsDetailsPage workCellsDetailsPage = new WorkCellsDetailsPage(DriverAdministration.getDriver());
            Calender calender = new Calender(DriverAdministration.getDriver());
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
            public void PauseFromWorkCellDetailsPage()
            {
                dashboardPage.OpenWorkCellsDetailsPage(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB));
                Assert.That(workCellsDetailsPage.GetPageTitle(),
                    Is.EqualTo(enumMapping.MapEnumToString(PageTitles.WorkCellDetailsPage)));

                Assert.That(workCellsDetailsPage.GetCurrentActiveStatus(),
                 Is.EqualTo(enumMapping.MapEnumToString(ActivityStatus.Running)));
                workCellsDetailsPage.PauseOrResumeActivity();
            }
            [Test, Order(3)]
            public void AssertThatActivityIsPausedInCalendar()
            {
                workCellsDetailsPage.NavigateToCalender();
                Assert.That(calender.GetPageTitle(),
                    Is.EqualTo(enumMapping.MapEnumToString(PageTitles.Calendar)));
                Assert.That(calender.GetWorkCellStatus(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB)),
                Is.EqualTo(enumMapping.MapEnumToString(ActivityStatus.Paused)));
            }

            [Test, Order(4)]
            public void AssertActivityPausedInDashboard()
            {
                calender.NavigateToDashboard();
                Assert.That(calender.GetPageTitle(),
                    Is.EqualTo(enumMapping.MapEnumToString(PageTitles.Dashboard)));
                Assert.That(dashboardPage.GetButtonStatus(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB)), Does.Contain("resume"));
            }

            [Test, Order(5)]
            public void ResumeFromWorkcellDetailsPage()
            {
                dashboardPage.OpenWorkCellsDetailsPage(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB));
                workCellsDetailsPage.PauseOrResumeActivity();
                Assert.That(workCellsDetailsPage.GetCurrentActiveStatus(),
                     Is.EqualTo(enumMapping.MapEnumToString(ActivityStatus.Running)));
            }

            [Test, Order(6)]
            public void AssertThatActivityIsRunningInCalendar()
            {
                workCellsDetailsPage.NavigateToCalender();
                Assert.That(calender.GetPageTitle(),
                    Is.EqualTo(enumMapping.MapEnumToString(PageTitles.Calendar)));
                Assert.That(calender.GetWorkCellStatus(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB)),
                Is.EqualTo(enumMapping.MapEnumToString(ActivityStatus.Running)));
            }
            [Test, Order(7)]
            public void AssertThatActivityIsRunningInDashboard()
            {
                calender.NavigateToDashboard();
                Assert.That(calender.GetPageTitle(),
                    Is.EqualTo(enumMapping.MapEnumToString(PageTitles.Dashboard)));
                Assert.That(dashboardPage.GetButtonStatus(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB)), Does.Contain("pause"));
            }
            //[Test, Order(5)]
            //public void AssertActivityRunningInDashboard()
            //{
            //    calender.NavigateToDashboard();
            //    Assert.That(calender.GetPageTitle(),
            //        Is.EqualTo(enumMapping.MapEnumToString(PageTitles.Dashboard)));
            //    Assert.That(dashboardPage.GetButtonStatus(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB)), Does.Contain("pause"));
            //}

            //[Test, Order(6)]
            //public void PauseFromDashboard()
            //{
            //    dashboardPage.PauseResumeActivity(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB));
            //    Assert.That(dashboardPage.GetButtonStatus(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB)), Does.Contain("resume"));
            //}
            //[Test, Order(7)]
            //public void AssertActivityIsRunningInWorkcellDetailsPage()
            //{
            //    dashboardPage.NavigateToWorkcellDetailsPage(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB));
            //    Assert.That(workCellsDetailsPage.GetPageTitle(),
            //                        Is.EqualTo(enumMapping.MapEnumToString(PageTitles.WorkCellDetailsPage)));
            //    Assert.That(workCellsDetailsPage.GetButtonStatus(), Does.Contain("resume"));
            //}

            //[Test, Order(8)]
            //public void AssertActivityIsPausedInActiveTab()
            //{
            //    Assert.That(workCellsDetailsPage.GetCurrentActiveStatus(), Does.Contain("resume"));
            //}
            //[Test, Order(9)]
            //public void ResumeFromWorkcellDetailsPage()
            //{
            //    workCellsDetailsPage.PauseOrResumeActivity(); //resume
            //    Assert.That(workCellsDetailsPage.GetButtonStatus(), Does.Contain("pause"));
            //}

            //[Test, Order(10)]
            //public void AssertThatActivityIsRunningOnCalendar()
            //{
            //    workCellsDetailsPage.NavigateToCalender();
            //    Assert.That(calender.GetWorkCellStatus(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB)),
            //    Is.EqualTo(enumMapping.MapEnumToString(ActivityStatus.Running)));
            //    calender.GetWorkCellStatus(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB));
            //}
            //[Test, Order(11)]
            //public void PauseFromCalendar()
            //{
            //    calender.PauseResumeActivity(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB));
            //    Assert.That(dashboardPage.GetButtonStatus(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB)), Does.Contain("pause"));
            //}
            //[Test, Order(12)]
            //public void AssertActivityPausedInDashboard()
            //{
            //    Assert.That(dashboardPage.GetButtonStatus(enumMapping.MapEnumToString(WorkCellNames.API_LH_TB)), Does.Contain("resume"));
            //}
        }
    }
}
