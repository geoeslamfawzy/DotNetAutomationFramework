using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using ServoFramework.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;

namespace ServoFramework.Pages
{
    public class DashBoard : SideBar
    {
        UIActions ui = new UIActions();
        private readonly IWebDriver driver;
        public DashBoard(IWebDriver driver)
        {
            this.driver = driver;

        }
        private static readonly string value = "value";
        private static readonly string option = "option";
        private static readonly string _apiPaueResumeStartBTN = $"//a[contains(text(), '{value}')]/parent::div/following-sibling::div//button";
        private static readonly string _startNonApiActivity = $"//a[contains(text(), '{value}')]/parent::div/parent::div//app-work-cell-card-actions//button[@ptooltip='Start activity']";
        private static readonly string _workcellName = $"//a[contains(text(),'{value}')]";
        private static readonly string _checkBox = $"//label[contains(text(), '{value}')]/parent::div/parent::div//p-checkbox";
        private static readonly string _eventMSG = $"//div[contains(text(), 'Activity')  and contains(text(), '{value}')]";
        private static readonly string _progressBar = $"//a[contains(text(),'{value}')]/parent::div/following-sibling::div//div[@role='progressbar']";
        private static readonly string _BTNAbortNonAPI = $"//a[contains(text(),'{value}')]/parent::div/following-sibling::div/div//button[@ptooltip='Abort Activity']";
        private static readonly string _ContextMenu = $"//a[contains(text(),'{value}')]/parent::div/preceding-sibling::div/app-work-cell-card-actions/div/div/button[@class='context-menu']";
        private static readonly string _completeActivityBtn = $"//a[contains(text(), '{value}')]/parent::div/following-sibling::div//button[@ptooltip='Complete Activity']";

        private By title = By.XPath("//h3[contains(text(),'Dashboard')]");
        private By lnkAll = By.Id("p-tabpanel-2-label");
        private By lnkLiquidHandeler = By.Id("p-tabpanel-2-label");
        private By lnkStore = By.XPath("//a[@id='p-tabpanel-3-label']");
        private By lnkOthers = By.XPath("//a[@id='p-tabpanel-4-label']/span");
        private By lnkUpcomingActivities = By.XPath("//a[@id='p-tabpanel-4-label']/span");
        private By _methodsDDL = By.XPath("//label[contains(text(), 'Select Method')]/parent::div//p-dropdown");
        private By _worklistsDDL = By.XPath("//label[contains(text(), 'Select Worklist')]/parent::div//p-dropdown");
        private By _durationField = By.XPath("//label[contains(text(), 'Duration')]/parent::div//input");
        private By _BTNStart = By.XPath("//span[text()='Start']");
        private By _activityStatus = By.XPath("//app-non-api-work-cell-card//button[@ptooltip='Abort Activity']/following-sibling::div");
        private By pageTitle = By.TagName("h3");
        private By _dialogHeader = By.XPath("//div[@role='dialog']/div/span");
        private By _BTNAbortAPI = By.XPath("//a[text()=' Abort activity ']");
        public void waitForPageDisplayed()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(title));
        }
        public DashBoard NavigateAllActivities()
        {
            ui.clickOn(lnkAll);
            return this;
        }

        public DashBoard NavigateLiquidHandeler()
        {
            ui.clickOn(lnkLiquidHandeler);
            return this;
        }

        public DashBoard NavigateStores()
        {
            ui.clickOn(lnkStore);
            return this;
        }

        public DashBoard NavigateOtherActivities()
        {
            ui.clickOn(lnkOthers);
            return this;
        }

        public DashBoard NavigateUpcomingActivites()
        {
            ui.clickOn(lnkUpcomingActivities); 
            return this;
        }

        public DashBoard StartNonApiActivity(string workcellName, string methodName, int duration = 1, string worklistName = "")
        {
            ui.clickOn(ui.GetLocator(_startNonApiActivity, workcellName));
            ui.ChooseFromMenue(_methodsDDL, methodName);
            if (!String.IsNullOrWhiteSpace(worklistName))
                ui.ChooseWorklistsFromMenue(_worklistsDDL, worklistName);
            if (!duration.Equals(1))
                ui.InsertText(_durationField, duration.ToString());
            ui.clickOn(_dialogHeader);
            ui.clickOn(_BTNStart);
            return this;
        }
        public DashBoard StartAPIActivity(string workcellName, string methodName, 
            string worklistName = "", bool simulation = true, bool isAutomateload = true, bool isAutomateUnload = true)
        {
            ui.clickOn(ui.GetLocator(_apiPaueResumeStartBTN, workcellName));
            ui.ChooseFromMenue(_methodsDDL, methodName);
            if (!String.IsNullOrWhiteSpace(worklistName))
                ui.ChooseWorklistsFromMenue(_worklistsDDL, worklistName);
            if(simulation == true)
                ui.SelectCheckBox(_checkBox, "Simulation");
            if (isAutomateload == true)
                ui.SelectCheckBox(_checkBox, "Automate Loading Dialog");
            if(isAutomateUnload == true)
                ui.SelectCheckBox(_checkBox, "Automate UnLoad Dialog");
            ui.clickOn(_BTNStart);
            return this;
        }
        public WorkCellsDetailsPage OpenWorkCellsDetailsPage(string workcellName)
        {
            By _workcellname = By.XPath(_workcellName.Replace(value, workcellName));
            ui.clickOn(_workcellname);
            ui.WaitFor(3);
            return new WorkCellsDetailsPage(DriverAdministration.getDriver());
        }

        public string GetNonAPIActivityStatus(string workcellName)
        {
            By _workcellStatus = By.XPath(_workcellName.Replace(value, workcellName) + "/parent::div/following-sibling::div/div/following-sibling::div/div");
            try
            {
                return ui.Find(_workcellStatus).Text;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return "I didn't find the status on the work cell card";
            }
        }

        public void waitFortoastDisplayed()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));
            wait.Until(ExpectedConditions.ElementIsVisible(_activityStatus));
        }
        public string ValidateActivityCompleting()
        {
            return ui.Find(_activityStatus).Text;
        }
        public string GetButtonStatus(string workcellName)
        {
            return ui.GetAttribute(ui.GetLocator(_apiPaueResumeStartBTN + "/img", workcellName), "alt");
        }
        public string GetMessage(string workcellName)
        {
            return ui.Find(ui.GetLocator(_eventMSG, workcellName)).ToString();
        }
        public bool IsProgressBarExist(string workcellName)
        {
            return ui.Find(ui.GetLocator(_progressBar, workcellName)).Displayed;
        }
        public DashBoard AbortNonApiActivity(string workcellName)
        {
            try
            {
                ui.clickOn(ui.GetLocator(_BTNAbortNonAPI, workcellName));
                Confirm();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());

            }
            return this;
        }



        public bool IsStartNonAPIActivityBtnExist(string workcellName)
        {
            return ui.Find(ui.GetLocator(_startNonApiActivity, workcellName)).Displayed;
        }



        public void NavigateToWorkcellDetailsPage(string workcellName)
        {
            ui.clickOn(ui.GetLocator(_workcellName, workcellName));
        }



        public DashBoard AbortApiActivity(string workcellName)
        {
            try
            {
                ui.clickOn(ui.GetLocator(_ContextMenu, workcellName));
                ui.clickOn(_BTNAbortAPI);
                Confirm();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
            return this;
        }
        public bool IsStartAPIActivityBtnExist(string workcellName)
        {
            return ui.Find(ui.GetLocator(_apiPaueResumeStartBTN, workcellName)).Displayed;
        }
        public DashBoard PauseResumeActivity(string workcellname)
        {
            ui.ClickByJS(ui.GetLocator(_apiPaueResumeStartBTN, workcellname));
            return this;
        }
        public DashBoard CompleteActivity(string workcellName)
        {
            ui.clickOn(ui.GetLocator(_completeActivityBtn, workcellName));
            Confirm();
            return this;
        }

    }
}