using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ServoFramework.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ServoFramework.Pages
{
    public class MethodsPage 
    {
        UIActions ui = new UIActions();
        SideBar sideBar;
        private readonly IWebDriver driver;
        public MethodsPage(IWebDriver driver)
        {
            this.driver = driver;
            //if (sideBar == null)
            //{
            //    this.sideBar = new SideBar(driver);
            //}

        }
        private By methodsList = By.XPath("//span[contains(text(), 'Methods')]/parent::a");
        private By openAddMethodModal = By.XPath("//button[@label='Add Non-API Method']/span");
        private By methodNameField = By.Id("name");
        private By viewNameField = By.Id("viewName");
        private By durationFiled = By.XPath("//span[@class='input-number p-inputnumber p-component']/input");
        private By addMethodBtn = By.XPath("//span[not(contains(text(), 'Method')) and contains(text(), 'Add')]");
        static string dynamicMethodLocation = "//*[contains(text(), 'value')]"; 

        public void AddMethod(string methodName, string viewName, string duration)
        {
            ui.ClickByJS(methodsList);
            ui.clickOn(openAddMethodModal);
            ui.InsertText(methodNameField, methodName);
            ui.InsertText(viewNameField, viewName);
            ui.InsertText(durationFiled, duration);
            ui.clickOn(addMethodBtn);
        }
        public void NavigateToMethods()
        {
            ui.Refresh();
            ui.ClickByJS(methodsList);
        }

        public bool AssertThatMethodHasAdded(string methodName)
        {
            By methodname = By.XPath(dynamicMethodLocation.Replace("value", methodName));
            try
            {
                ui.Find(methodname);
                Console.WriteLine("I have Found the method called : " + methodName);
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("I didn't Find the method called " + methodName);
                return false;
            }
        }
    }
}
