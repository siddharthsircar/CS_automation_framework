using AutomationFramework.Framework;
using OpenQA.Selenium;
using System;
using WaitHelper = SeleniumExtras.WaitHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AutomationFramework.Keywords
{
    public class JavaScriptKeywords : Base
    {
        private static ExtentTestManager Report = new ExtentTestManager();
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //private static IWebDriver driver;

        //static JavaScriptKeywords()
        //{
        //    driver = DriverFactory.GetInstance().GetDriver();
        //}

        /// <summary>
        /// This method uses JavaScriptExecuter to highlight the element under action.
        /// Utilized in SeleniumKeywords
        /// </summary>
        /// <param name="element"></param>
        public static void HighlightElement(IWebElement element)
        {
            var jsDriver = (IJavaScriptExecutor)driver;
            string highlightJavascript = @"arguments[0].style.cssText = ""border-width: 1px; border-style: solid; border-color: red"";";
            jsDriver.ExecuteScript(highlightJavascript, new object[] { element });
        }
        public static void SetTextByControlId(string controlid, string value)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("document.getElementById('"+ controlid + "').value='" + value + "'");
            Report.Pass("Set : " + value + " in " + controlid);
            log.Info("Set : " + value + " in " + controlid);
        }

        public static void SetTextInElement(String pageName, String locatorName, String text)
        {
            By element = ElementLocator.GetLocator(pageName, locatorName);
            if (element != null)
            {
                try
                {
                    wait.Until(WaitHelper.ExpectedConditions.PresenceOfAllElementsLocatedBy(element));
                    IWebElement webelement = driver.FindElement(element);
                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                    //js.ExecuteScript("arguments[0].setAttribute('value','" + text + "')", webelement);
                    js.ExecuteScript("arguments[0].value='" + text + "'", webelement);
                    Report.Pass("Entered : " + text + " in " + locatorName);
                    log.Info("Entered : " + text + " in " + locatorName);
                }
                catch(WebDriverException wd)
                {
                    Report.Fail("Exception on the element " + locatorName + "  " + wd.Message);
                    log.Error(TestContext.CurrentContext.Test.MethodName + " " + locatorName + " not found");
                }
            }
            else
            {
                Report.Fail(locatorName + " not found");
                log.Error(TestContext.CurrentContext.Test.MethodName + " " + locatorName + " not found");
            }
        }

        public static void SetAttribute(String pageName, String locatorName,String attrname, String value)
        {
            By element = ElementLocator.GetLocator(pageName, locatorName);
            if (element != null)
            {
                try
                {
                    wait.Until(WaitHelper.ExpectedConditions.PresenceOfAllElementsLocatedBy(element));
                    IWebElement webelement = driver.FindElement(element);
                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                    js.ExecuteScript("arguments[0].setAttribute(arguments[1],arguments[2]);", webelement,attrname,value);
                    //js.ExecuteScript("arguments[0].value='" + text + "'", webelement);
                    Report.Pass("Entered : " + value + " in " + attrname + " of "+ attrname);
                    log.Info("Entered : " + value + " in " + attrname + " of " + attrname);
                }
                catch (WebDriverException wd)
                {
                    Report.Fail("Exception on the element " + locatorName + "  " + wd.Message);
                    log.Error(TestContext.CurrentContext.Test.MethodName + " " + locatorName + " not found");
                }
            }
            else
            {
                Report.Fail(locatorName + " not found");
                log.Error(TestContext.CurrentContext.Test.MethodName + " " + locatorName + " not found");
            }
        }

        public static void Click(String pageName, String locatorName)
        {
            By element = ElementLocator.GetLocator(pageName, locatorName);
            if (element != null)
            {
                try
                {
                    wait.Until(WaitHelper.ExpectedConditions.ElementToBeClickable(element));
                    IWebElement webelement = driver.FindElement(element);
                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                    js.ExecuteScript("arguments[0].click()", webelement);
                    Report.Pass("Clicked On " + locatorName);
                    log.Info("Clicked On " + locatorName);
                }
                catch(WebDriverException wd)
                {
                    Report.Fail("Exception on the element " + locatorName + "  " + wd.Message);
                    log.Error(TestContext.CurrentContext.Test.MethodName + " " + locatorName + " not found");
                }
            }
            else
            {
                Report.Fail(locatorName + " not found");
                log.Error(TestContext.CurrentContext.Test.MethodName + " " + locatorName + " not found");
            }
        }

        public static void ScrollToAnElement(string pageName, string locatorname, params String[] values)
        {
            By element = ElementLocator.GetLocator(pageName, locatorname,values);
            if (element != null)
            {
                try
                {
                    wait.Until(WaitHelper.ExpectedConditions.PresenceOfAllElementsLocatedBy(element));
                    IWebElement webelement = driver.FindElement(element);
                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                    js.ExecuteScript("arguments[0].scrollIntoView(true)", webelement);
                    log.Info("Scroll to " + locatorname);
                }
                catch(WebDriverException wd)
                {
                    Report.Fail("Exception on the element " + locatorname + "  " + wd.Message);
                    log.Error(TestContext.CurrentContext.Test.MethodName + " " + locatorname + " not found");
                }
            }
            else
            {
                log.Error(TestContext.CurrentContext.Test.MethodName + " " + locatorname + " not found");
            }
                
        }

        public static void ScrollToTopOfPage()
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("window.scrollTo(0, -(document.body.scrollHeight))");
                log.Info("Scroll to top of the page");
            }
            catch(WebDriverException wd)
            {
                Report.Fail(wd.Message);
            }
        }

        public static void ScrollToBottomOfPage()
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                log.Info("Scroll to bottom of the page");
            }
            catch (WebDriverException wd)
            {
                Report.Fail(wd.Message);
            }
        }

    }
}
