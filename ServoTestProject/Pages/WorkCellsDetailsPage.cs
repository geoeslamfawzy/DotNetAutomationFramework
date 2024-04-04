using AngleSharp.Dom;
using OpenQA.Selenium;
using ServoFramework.Base;
using ServoTestProject.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ServoFramework.Pages
{
    public class WorkCellsDetailsPage : SideBar
    {
        UIActions ui = new UIActions();
        SideBar sideBar;
        private readonly IWebDriver driver;
        public WorkCellsDetailsPage(IWebDriver driver)
        {
            this.driver = driver;
            //if (sideBar == null)
            //{
            //    this.sideBar = new SideBar(driver);
            //}
        }
        private static readonly string value = "value";
        private static readonly string _pauseResumeBtn = $"//div[contains(text(), '{value}')]/following-sibling::div//button";
        private static readonly string _checkBox = $"//label[contains(text(), '{value}')]/parent::div/parent::div//p-checkbox";
        private static readonly string _activityStatusinHistory = $"//td[contains(text(), '{value}')]/following-sibling::td[4]";

        private By newWorkCellsBtn = By.XPath("//button[@label='Create New Work Cell']");
        private By _nonAPIStatus = By.XPath("//button[@ptooltip='Abort Activity']/following-sibling::div");
        private By _startActivityBtn = By.XPath("//app-work-cell-card-actions/div/button[@ptooltip='Start activity']");
        private By _dialogHeader = By.XPath("//div[@role='dialog']/div/span");
        private By _methodsDDL = By.XPath("//label[contains(text(), 'Select Method')]/parent::div//p-dropdown");
        private By _worklistsDDL = By.XPath("//label[contains(text(), 'Select Worklist')]/parent::div//p-dropdown");
        private By _durationField = By.XPath("//label[contains(text(), 'Duration')]/parent::div//input");
        private By BTNStart = By.XPath("//span[text()='Start']");
        private By _pauesResumeBtn = By.XPath("//img[contains(@alt, 'resume icon') or contains(@alt, 'pause icon')]/parent::button");
        private By _BtnStatus = By.XPath("//img[contains(@alt, 'resume icon') or contains(@alt, 'pause icon')]");
        private By _progressBar = By.TagName("p-progressbar");
        private By _BTNAbort = By.XPath("//button[@ptooltip='Abort Activity']");
        private By _ActivityStatus = By.XPath("//div[@class='data' and text()=' Aborted ']");
        private By _activitiesTab = By.XPath("//span[contains(text(), 'Activities')]/parent::a");
        private By _historySubtag = By.XPath("//ul[@class='p-tabview-nav']/li/a/span[contains(text(), 'History')]");
        private By _completeBtn = By.XPath("//button[@ptooltip='Complete Activity']");
        private By _hideSimulateActivitieCheckBox = By.CssSelector("div.p-checkbox-box");
        private By _activeActivityStatus = By.XPath("//td[contains(text(), 'Paused') or contains(text(), 'Running')]");
        private By _isSimulatedActivitiesCheckboxAttribute = By.XPath("//input[@aria-checked='true' or @aria-checked='false']");

        public WorkCellForm openWorkCellForm()
        {
            ui.clickOn(newWorkCellsBtn);
            return new WorkCellForm(driver);
        }

        public string GetNonAPIActivityStatus()
        {
            try
            {
                return ui.Find(_nonAPIStatus).Text;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return "Activity is not running in Work Cell Details";
            }
        }
        public string GetButtonStatus()
        {
            Thread.Sleep(1000);
            return ui.GetAttribute(_BtnStatus, "alt");
        }

        public WorkCellsDetailsPage StartNonApiActivity(string methodName, int duration = 1, string worklistName = "")
        {
            ui.ClickByJS(_startActivityBtn);
            ui.ChooseFromMenue(_methodsDDL, methodName);
            if (!String.IsNullOrWhiteSpace(worklistName))
                ui.ChooseWorklistsFromMenue(_worklistsDDL, worklistName);
            if (!duration.Equals(1))
                ui.InsertText(_durationField, duration.ToString());
            ui.clickOn(_dialogHeader);
            ui.clickOn(BTNStart);
            return this;
        }
        public WorkCellsDetailsPage StartAPIActivity(string methodName,
            string worklistName = "", bool simulation = true, bool isAutomateload = true, bool isAutomateUnload = true)
        {
            ui.ClickByJS(_startActivityBtn);
            ui.ChooseFromMenue(_methodsDDL, methodName);
            if (!String.IsNullOrWhiteSpace(worklistName))
                ui.ChooseWorklistsFromMenue(_worklistsDDL, worklistName);
            if (simulation == true)
                ui.SelectCheckBox(_checkBox, "Simulation");
            if (isAutomateload == true)
                ui.SelectCheckBox(_checkBox, "Automate Loading Dialog");
            if (isAutomateUnload == true)
                ui.SelectCheckBox(_checkBox, "Automate UnLoad Dialog");
            ui.clickOn(BTNStart);
            return this;
        }
        public bool IsProgressBarExist()
        {
            return ui.Find(_progressBar).Displayed;
        }

        public WorkCellsDetailsPage AbortNonApiActivity()
        {
            try
            {
                ui.ClickByJS(_BTNAbort);
                Confirm();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());

            }
            return this;
        }
        public bool IsActivitystatusAborted()
        {
            return ui.Find(_ActivityStatus).Displayed;
        }
        private WorkCellsDetailsPage NavigateToHistoryData()
        {
            ui.ClickByJS(_activitiesTab);
            ui.ClickByJS(_historySubtag);
            return this;
        }
        public string GetActivityStatueFromHistory(string date)
        {
            NavigateToHistoryData();
            return ui.Find(ui.GetLocator(_activityStatusinHistory, date)).Text.ToString();
        }
        public WorkCellsDetailsPage CompleteActivity()
        {
            ui.ClickByJS(_completeBtn);
            Confirm();
            return this;
        }
        public WorkCellsDetailsPage PauseOrResumeActivity()
        {
            ui.ClickByJS(_pauesResumeBtn);
            ui.WaitFor(5);
            return this;
        }

        public string GetCurrentActiveStatus() 
        {
            ui.ClickByJS(_activitiesTab);
            if (ui.GetAttribute(_isSimulatedActivitiesCheckboxAttribute, "aria-checked") == "true")
            {
                ui.ClickByJS(_hideSimulateActivitieCheckBox);
                return ui.Find(_activeActivityStatus).Text.ToString();
            }
            else
                return ui.Find(_activeActivityStatus).Text.ToString();
        }
    }
}
