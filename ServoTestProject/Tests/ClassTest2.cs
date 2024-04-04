using NUnit.Framework;
using ServoFramework.Base;
using ServoFramework.Helpers;
using ServoFramework.Pages;
using ServoTestProject.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServoFramework.Tests
{
    [Parallelizable(ParallelScope.All)]
    public class ClassTest2 : BaseTest
    {
        LoginPage loginPage = new LoginPage(DriverAdministration.getDriver());
        string jobName = "Eslam_Demo_Auto" + new Random().Next();
        [Test, Parallelizable(ParallelScope.Self), TestCaseSource(typeof(TestDataMethods), nameof(TestDataMethods.LoginAdminOperator))]
        public void TestMaintenanceStore(string username, string password)
        {
            loginPage.Login(username, password);
            //SideBar side = new SideBar(DriverAdministration.getDriver());
            //side.NavigateToMaintenanceStore();

            MaintenanceStore maintenanceStore = new MaintenanceStore(DriverAdministration.getDriver());
            maintenanceStore.openAddAuditMaintenancemenue();
            maintenanceStore.MakeMaintenanceAudit(jobName);
            Assert.That(maintenanceStore.GetPageTitle().Contains("Maintenance / Store"));
        }

        [Test, Parallelizable(ParallelScope.Self), TestCaseSource(typeof(TestDataMethods), nameof(TestDataMethods.LoginAdminOperator))]
        public void TestNavigationToActivityHistory(string username, string password)
        {
            loginPage.Login(username, password);
            //SideBar side = new SideBar(DriverAdministration.getDriver());
            //side.NavigateToActivityHistory();
            ActivityHistory activityHistory = new ActivityHistory(DriverAdministration.getDriver());
            Assert.That(activityHistory.GetPageTitle().Contains("Activity History"));
        }

    }
}

//app-non-api-work-cell-card//button[@ptooltip='Abort Activity']/following-sibling::div