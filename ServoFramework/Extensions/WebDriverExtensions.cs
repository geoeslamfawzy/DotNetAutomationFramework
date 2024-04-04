using OpenQA.Selenium;
using ServoFramework.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ServoFramework.Extensions
{
    public static class WebDriverExtensions
    {
        //public static void WaitForPageToBeLoaded()
        //{
        //    DriverAdministration.getDriver().WaitForCondition(dri =>
        //    {
        //        string state = dri.ExecuteJs("return document.readyState").ToString();
        //        return state == "complete";
        //    }, 20);
        //}
        public static void WaitForCondition<T>(this T obj, Func<T, bool> condition, int timeOut)
        {
            Func<T, bool> execute =
                (arg) =>
                {
                    try
                    {
                        return condition(arg);
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                };

            var stopWatch = Stopwatch.StartNew();
            while (stopWatch.ElapsedMilliseconds < timeOut)
            {
                if (execute(obj))
                {
                    break;
                }
            }
        }

        public static object ExecuteJs(string script)
        {
            return ((IJavaScriptExecutor)DriverAdministration.getDriver()).ExecuteScript(script);
        }

    }
}
