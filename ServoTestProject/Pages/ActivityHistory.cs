using OpenQA.Selenium;
using ServoFramework.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServoFramework.Pages
{
    public class ActivityHistory 
    {
        UIActions ui = new UIActions();
        SideBar sideBar;
        private readonly IWebDriver driver;

        private By pageTitle = By.TagName("h3");
        
        public ActivityHistory(IWebDriver driver)
        {
            this.driver = driver;
            //if (sideBar == null)
            //{
            //    this.sideBar = new SideBar(driver);
            //}
        }
        
        public string GetPageTitle()
        {
            return ui.Find(pageTitle).Text;
        }
    }
}
