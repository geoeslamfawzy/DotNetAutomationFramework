using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using ServoFramework.Factory;
using ServoFramework.ProjectConstants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace ServoFramework.Base
{
    public class UIActions
    {
        

        public IWebElement Find(By locator, WaitType waitType = WaitType.Explicit, WaitStrategy waitStrategy = WaitStrategy.PRESENCE)
        {
            switch (waitType)
            {
                case WaitType.Fluent:
                    return WaitFactory.PerformFluentWait(locator);
                case WaitType.Explicit:
                    return WaitFactory.performExplicitWait(waitStrategy, locator);
                case WaitType.Implicit:
                    WaitFactory.PerformImplicitWait();
                    break;
            }
            return DriverAdministration.getDriver().FindElement(locator);
        }
        public By GetLocator(string genericLocator, string value)
        {
            return By.XPath(genericLocator.Replace("value", value));
        }
        public List<IWebElement> FindElements(By locator, WaitType waitType = WaitType.Implicit, WaitStrategy waitStrategy = WaitStrategy.PRESENCE)
        {
            switch (waitType)
            {
                case WaitType.Implicit:
                    WaitFactory.PerformImplicitWait();
                    return new WebDriverWait(DriverAdministration.getDriver(), TimeSpan.FromSeconds(Constants.GetExplicitWait()))
                   .Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator)).ToList();
                case WaitType.Explicit:
                    return new WebDriverWait(DriverAdministration.getDriver(), TimeSpan.FromSeconds(Constants.GetExplicitWait()))
                   .Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator)).ToList();
                case WaitType.Fluent:
                    WaitFactory.PerformImplicitWait();
                    break;
            }
            return new WebDriverWait(DriverAdministration.getDriver(), TimeSpan.FromSeconds(Constants.GetExplicitWait()))
                  .Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator)).ToList();
        }
        public void clickOn(By locator, WaitType waitType = WaitType.Fluent, WaitStrategy waitStrategy = WaitStrategy.CLICKABLE)
        {
            switch (waitType)
            {
                case WaitType.Fluent:
                    WaitFactory.performExplicitWait(waitStrategy, locator).Click();
                    break;
                case WaitType.Explicit:
                    WaitFactory.performExplicitWait(waitStrategy, locator).Click();
                    break;
                case WaitType.Implicit:
                    WaitFactory.PerformImplicitWait();
                    Find(locator);
                    break;
            }
        }
        public void doubleClickOn(By locator, WaitType waitType = WaitType.Fluent, WaitStrategy waitStrategy = WaitStrategy.CLICKABLE)
        {
            Actions act = new Actions(DriverAdministration.getDriver());
            switch (waitType)
            {
                case WaitType.Implicit:
                    WaitFactory.PerformImplicitWait();
                    act.DoubleClick(Find(locator));
                    break;
                case WaitType.Explicit:
                    act.DoubleClick(WaitFactory.performExplicitWait(waitStrategy, locator));
                    break;
                case WaitType.Fluent:
                    act.DoubleClick(WaitFactory.PerformFluentWait(locator));
                    break;
            }
        }
        public void ClickByJS(By locator, WaitType waitType = WaitType.Fluent, WaitStrategy waitStrategy = WaitStrategy.CLICKABLE)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)DriverAdministration.getDriver();
            switch (waitType)
            {
                case WaitType.Fluent:
                    js.ExecuteScript("arguments[0].click()", WaitFactory.PerformFluentWait(locator));
                    break;
                case WaitType.Explicit:
                    js.ExecuteScript("arguments[0].click()", WaitFactory.performExplicitWait(waitStrategy, locator));
                    break;
                case WaitType.Implicit:
                    WaitFactory.PerformImplicitWait();
                    js.ExecuteScript("arguments[0].click()", DriverAdministration.getDriver().FindElement(locator));
                    break;
            }            
        }
        public void DoubleClickByJS(By locator, WaitType waitType = WaitType.Fluent, WaitStrategy waitStrategy = WaitStrategy.CLICKABLE)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)DriverAdministration.getDriver();
            switch (waitType)
            {
                case WaitType.Fluent:
                    js.ExecuteScript("arguments[0].click()", WaitFactory.PerformFluentWait(locator));
                    js.ExecuteScript("arguments[0].click()", WaitFactory.PerformFluentWait(locator));
                    break;
                case WaitType.Explicit:
                    js.ExecuteScript("arguments[0].click()", WaitFactory.performExplicitWait(waitStrategy, locator));
                    js.ExecuteScript("arguments[0].click()", WaitFactory.performExplicitWait(waitStrategy, locator));
                    break;
                case WaitType.Implicit:
                    WaitFactory.PerformImplicitWait();
                    js.ExecuteScript("arguments[0].click()", DriverAdministration.getDriver().FindElement(locator));
                    js.ExecuteScript("arguments[0].click()", DriverAdministration.getDriver().FindElement(locator));
                    break;
            }
        }
        public void doubleClickUsingJS(By locator, WaitType waitType = WaitType.Fluent, WaitStrategy waitStrategy = WaitStrategy.CLICKABLE)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)DriverAdministration.getDriver();
            switch (waitType)
            {
                case WaitType.Implicit:
                    WaitFactory.PerformImplicitWait();
                    js.ExecuteScript("arguments[0].click()", DriverAdministration.getDriver().FindElement(locator));
                    js.ExecuteScript("arguments[0].click()", DriverAdministration.getDriver().FindElement(locator));
                    break;
                case WaitType.Explicit:
                    js.ExecuteScript("arguments[0].click()", WaitFactory.performExplicitWait(waitStrategy, locator));
                    js.ExecuteScript("arguments[0].click()", WaitFactory.performExplicitWait(waitStrategy, locator));
                    break;
                case WaitType.Fluent:
                    js.ExecuteScript("arguments[0].click()", WaitFactory.PerformFluentWait(locator));
                    js.ExecuteScript("arguments[0].click()", WaitFactory.PerformFluentWait(locator));
                    break;
            }
        }
        public void NavigateTo(string url)
        {
            DriverAdministration.getDriver().Url = url;
        }
       
        public void Refresh()
        {
            DriverAdministration.getDriver().Navigate().Refresh();
        }
        public void ChooseFromMenue(By menueLocator, string element)
        {
            clickOn(menueLocator, WaitType.Explicit, WaitStrategy.VISIBLE);
            List<IWebElement> options = FindElements(By.XPath("//p-dropdownitem/li/div"));
            foreach (var option in options)
            {
                if (option.Text.Contains(element))
                {
                    option.Click();
                    break;
                }
            }
        }
        public void ChooseWorklistsFromMenue(By menueLocator, string element)
        {
            clickOn(menueLocator, WaitType.Explicit, WaitStrategy.VISIBLE);
            List<IWebElement> options = FindElements(By.XPath("//p-dropdownitem/li/span"));
            foreach (var option in options)
            {
                if (option.Text.Contains(element))
                {
                    option.Click();
                    break;
                }
            }
        }

        /* public void ChooseFromDropDown(By menueLocator, Enum option)
         {
             clickOn(menueLocator, WaitType.Explicit, WaitStrategy.VISIBLE);
             List<IWebElement> options = FindElements(By.TagName("p-dropdownitem"));
             foreach (var option in options)
             {
                 if (option.Text == element)
                 {
                     option.Click();
                     break;
                 }
             }
         }*/
        public JToken ReadDataFile(string fileName)
        {
            String file = File.ReadAllText(Constants.OpenDataFile(fileName));
            return JToken.Parse(file);
        }
        public void InsertText(By fieldLocation, String text,  WaitType waitType = WaitType.Fluent, WaitStrategy waitStrategy = WaitStrategy.VISIBLE)
        {
            switch (waitType)
            {
                case WaitType.Implicit :
                    WaitFactory.PerformImplicitWait();
                    Find(fieldLocation).Clear();
                    Find(fieldLocation).SendKeys(text);
                    break;
                case WaitType.Explicit:
                    WaitFactory.performExplicitWait(waitStrategy, fieldLocation).Clear();
                    WaitFactory.performExplicitWait(waitStrategy, fieldLocation).SendKeys(text);
                    break;
                case WaitType.Fluent:
                    WaitFactory.PerformFluentWait(fieldLocation).Clear();
                    WaitFactory.PerformFluentWait(fieldLocation).SendKeys(text);
                    break;
            }
        }
        
        public void SelectDropDownList(By locator, String value, WaitStrategy waitStrategy = WaitStrategy.VISIBLE)
        {
            IWebElement element = WaitFactory.performExplicitWait(waitStrategy, locator);
            SelectElement dropDownList = new SelectElement(element);
            dropDownList.SelectByText(value);
        }

        public string GetSelectDropDownList(By locator)
        {
            new WebDriverWait(DriverAdministration.getDriver(), TimeSpan.FromSeconds(Constants.GetExplicitWait()))
                   .Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator)).ToList();
            SelectElement dropDownList = new SelectElement(Find(locator));
            return dropDownList.AllSelectedOptions.First().ToString();
        }

        public IList<IWebElement> GetSelectedOptions(By locator, WaitStrategy waitStrategy = WaitStrategy.VISIBLE)
        {
            new WebDriverWait(DriverAdministration.getDriver(), TimeSpan.FromSeconds(Constants.GetExplicitWait()))
                   .Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator)).ToList();
            SelectElement dropDownList = new SelectElement(Find(locator));
            return dropDownList.AllSelectedOptions;
        }
        public IWebElement FindElementInListByVisibleText(string visibleText, By locator)
        {
            IWebElement element;
            List<IWebElement> elements = FindElements(locator);
            foreach (var ele in elements)
            {
                if (ele.Text == visibleText)
                {
                    element = ele;
                    return element;
                }
            }
            return null;
        }
        public IWebElement FindElementInListByIndex(int index, By locator)
        {
            List<IWebElement> elements = FindElements(locator);
            return elements[index];
        }
        
        public By IsCheckBoxSelected(string genericLocator, string value, bool isChecked = true)
        {
            string selection = genericLocator.Replace("value", value);
            if (isChecked)
            {
                return By.XPath(selection.Replace("option", "true"));
            }
            else
            {
                return By.XPath(selection.Replace("option", "false"));
            }
        }
        public void SelectCheckBox(string genericPath, string checkBoxName) 
        {
            By checkBox = GetLocator(genericPath, checkBoxName);
            if (GetAttribute(GetLocator((genericPath + "//input"), checkBoxName), "aria-checked") == "false")
                
            {
                clickOn(checkBox);
            }
            else
                Console.WriteLine("The Box is Already Selected");
        }

        public void UnSelectCheckBox(string genericPath, string checkBoxName)
        {
            By _isSelected = IsCheckBoxSelected(genericPath + "//input[@aria-checked='true']", checkBoxName, false);
            By _checkBox = GetLocator(genericPath, checkBoxName);
            
            try
            {
                Find(_isSelected);
                clickOn(_checkBox);
            }
            catch (Exception)
            {

                Console.WriteLine("The box is already not selected");
            }
        }

        public void WaitFor(int seconds)
        {
            Thread.Sleep(seconds*1000);
        }
        public string GetAttribute(By locator, string attributeName)
        {
            return Find(locator).GetAttribute(attributeName);
        }

    }
}
