using OpenQA.Selenium;
using ServoFramework.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ServoFramework.Pages
{
    public class MaintenanceStore 
    {
        UIActions ui = new UIActions();
        SideBar sideBar;
        private readonly IWebDriver driver;
        public MaintenanceStore(IWebDriver driver)
        {
            this.driver = driver;
            //if (sideBar == null)
            //{
            //    this.sideBar = new SideBar(driver);
            //}
        }

        private By pageTitle = By.TagName("h3");
        private By addAuditMaintenanceBtn = By.XPath("//span[contains(text(), 'Add Audit Maintenance Job')]");
        private By workCellMenue = By.XPath("//span[contains(text(), 'Select Work Cell ')]");
        private By workCellOptions, jobPriorityOptions, usersOptions = By.TagName("p-dropdownitem");
        private By jobNameField = By.Name("jobName");
        private By jobPriorityMenue = By.XPath("//p-dropdown[@placeholder = 'Select Job Priority ']/div/span");
        private By userCreator = By.XPath("//p-dropdown[@placeholder = 'Select user']//div[@role ='button']/span");
        private By checkBoxMaterialLocation = By.CssSelector("div.p-checkbox-box");
        private By addBtn = By.XPath("//span[text() = 'Add']");
        public void openAddAuditMaintenancemenue()
        {
            ui.clickOn(addAuditMaintenanceBtn);
        }
        public MaintenanceStore MakeMaintenanceAudit(string jobName)
        {
            ui.ChooseFromMenue(workCellMenue, "Verso 177");
            ui.InsertText(jobNameField, jobName);
            ui.ChooseFromMenue(jobPriorityMenue, "Exclusive");

            ui.clickOn(checkBoxMaterialLocation);
            Thread.Sleep(2000);
            ui.ChooseFromMenue(userCreator, "Administrator admin");
            ui.clickOn(addBtn);
            return this;
        }
        public string GetPageTitle()
        {
            return ui.Find(pageTitle).Text;
        }
    }
}
