using AutomationFramework.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using WaitHelper = SeleniumExtras.WaitHelpers;
using System;
using NUnit.Framework;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Android;
using System.Configuration;
using OpenQA.Selenium.Appium.iOS;

namespace AutomationFramework.Keywords
{
    /// <summary>
    /// Appium keywords class
    /// Contains all appium wrapper functions
    /// </summary>
    public class AppiumKeywords : Base
    {
        private static ExtentTestManager Report = new ExtentTestManager();
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        /// <summary>
        /// This method Taps on a control 
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="locatorName"></param>
        /// <param name="values"></param>
        public static void Tap(string pageName, string locatorName, params String[] values)
        {            
            By element = null;
            if (values.Length == 0)
            {
                element = ElementLocator.GetLocator(pageName, locatorName);
            }
            else
            {
                element = ElementLocator.GetLocator(pageName, locatorName, values);
            }
            if (element != null)
            {
                TouchAction touch = null;
                if (ConfigurationManager.AppSettings["os"].ToLower().Equals("android"))
                {
                    AndroidDriver<AndroidElement> androidDriver = (AndroidDriver<AndroidElement>)driver;
                    touch = new TouchAction(androidDriver);
                }
                else if (ConfigurationManager.AppSettings["os"].ToLower().Equals("ios"))
                {
                    IOSDriver<IOSElement> iOSDriver = (IOSDriver<IOSElement>)driver;
                    touch = new TouchAction(iOSDriver);
                }

                wait.Until(WaitHelper.ExpectedConditions.ElementToBeClickable(element));
                IWebElement el = driver.FindElement(element);

                touch.Tap(el).Perform();

                //el.Click();
                Report.Pass("Tapped On " + locatorName);
                log.Info("Tapped On " + locatorName);
            }
            else
            {
                Report.Fail(locatorName + " not found");
                log.Error(TestContext.CurrentContext.Test.MethodName + " " + locatorName + " not found");
            }
        }

        /// <summary>
        /// This method sets the text in a text box
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="locatorName"></param>
        /// <param name="text"></param>
        /// <param name="values"></param>
        public static void SetText(string pageName, string locatorName, string text, params String[] values)
        {
            By element = null;
            if (values.Length == 0)
            {
                element = ElementLocator.GetLocator(pageName, locatorName);
            }
            else
            {
                element = ElementLocator.GetLocator(pageName, locatorName, values);
            }

            if (element != null)
            {
                try
                {
                    wait.Until(WaitHelper.ExpectedConditions.PresenceOfAllElementsLocatedBy(element));
                    driver.FindElement(element).Clear();
                    driver.FindElement(element).SendKeys(text);
                    Report.Pass("Entered : " + text + " in " + locatorName);
                    log.Info("Entered : " + text + " in " + locatorName);
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

        /// <summary>
        /// This method gets the text of a control
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="locatorName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string GetText(string pageName, string locatorName, params string[] values)
        {
            string text = "";
            By element = null;

            if (values.Length == 0)
            {
                element = ElementLocator.GetLocator(pageName, locatorName);
            }
            else
            {
                element = ElementLocator.GetLocator(pageName, locatorName, values);
            }
            if (element != null)
            {
                try
                {
                    wait.Until(WaitHelper.ExpectedConditions.ElementExists(element));
                    text = driver.FindElement(element).Text;
                    Report.Info("Innertext of " + locatorName + " : " + text);
                    log.Info("Innertext of " + locatorName + " : " + text);
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

            return text;
        }

        /// <summary>
        /// This method clear the text from a control
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="locatorName"></param>
        /// <param name="values"></param>
        public static void ClearText(string pageName, string locatorName, params String[] values)
        {
            By element = null;
            if (values.Length == 0)
            {
                element = ElementLocator.GetLocator(pageName, locatorName);
            }
            else
            {
                element = ElementLocator.GetLocator(pageName, locatorName, values);
            }
            if (element != null)
            {
                try
                {
                    wait.Until(WaitHelper.ExpectedConditions.PresenceOfAllElementsLocatedBy(element));
                    driver.FindElement(element).Clear();
                    Report.Pass(locatorName + "Cleared");
                    log.Info(locatorName + "Cleared");
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
        /// <summary>
        /// Gets a value indicating whether or not this element is present.
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="locatorName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Boolean IsElementPresent(string pageName, string locatorName, params String[] values)
        {
            bool exists = false;
            By element = null;
            if (values.Length == 0)
            {
                element = ElementLocator.GetLocator(pageName, locatorName);
            }
            else
            {
                element = ElementLocator.GetLocator(pageName, locatorName, values);
            }
            if (element != null)
            {
                try
                {
                    exists = driver.FindElement(element).Displayed;                 
                    if (exists == true)
                    {
                        Report.Pass("Element " + locatorName + " Exists on screen: " + exists.ToString());
                        log.Info("Element" + locatorName + " Exists on screen: " + exists.ToString());
                    }
                    else
                    {
                        Report.Fail("Element " + locatorName + " Exists on screen: " + exists.ToString());
                        log.Error("Element" + locatorName + " Exists on screen: " + exists.ToString());
                    }

                }
                catch (NoSuchElementException e)
                {
                    Report.Fail("Unable to locate element");
                    log.Error(e.Message);
                }
                catch (Exception e)
                {
                    Report.Fail("Locator name : " + locatorName + " , " + e.Message);
                    log.Error("Locator name : " + locatorName + " , " + e.Message);
                }


            }

            return exists;
        }

        /// <summary>
        /// Gets a value indicating whether or not this element is present.
        /// When expected result is false
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="locatorName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Boolean IsElementNotPresent(string pageName, string locatorName, params String[] values)
        {
            bool exists = false;
            By element = null;
            if (values.Length == 0)
            {
                element = ElementLocator.GetLocator(pageName, locatorName);
            }
            else
            {
                element = ElementLocator.GetLocator(pageName, locatorName, values);
            }
            if (element != null)
            {
                try
                {
                    exists = driver.FindElement(element).Displayed;;
                    if (exists == true)
                    {
                        Report.Fail("Element " + locatorName + " Exists on screen: " + exists.ToString());
                        log.Error("Element" + locatorName + " Exists on screen: " + exists.ToString());
                    }
                    else
                    {
                        Report.Pass("Element " + locatorName + " Exists on screen: " + exists.ToString());
                        log.Info("Element" + locatorName + " Exists on screen: " + exists.ToString());
                    }

                }
                catch (NoSuchElementException e)
                {
                    Report.Pass("Element " + locatorName + " exists on screen: " + exists.ToString());
                    log.Info("Element" + locatorName + " Exists on screen: " + exists.ToString());
                }
                catch (Exception e)
                {
                    Report.Fail("Locator name : " + locatorName + " , " + e.Message);
                    log.Error("Locator name : " + locatorName + " , " + e.Message);
                }

            }

            return exists;
        }

        /// <summary>
        /// Gets a value indicating whether or not this element is visible.
        /// When expected result is false
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="locatorName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Boolean IsElementNotVisible(string pageName, string locatorName, params String[] values)
        {
            bool exists = false;
            By element = null;
            if (values.Length == 0)
            {
                element = ElementLocator.GetLocator(pageName, locatorName);
            }
            else
            {
                element = ElementLocator.GetLocator(pageName, locatorName, values);
            }
            if (element != null)
            {
                try
                {
                    exists = driver.FindElement(element).Displayed;
                    if (exists == true)
                    {
                        Report.Fail("Element " + locatorName + " Exists on screen: " + exists.ToString());
                        log.Error("Element" + locatorName + " Exists on screen: " + exists.ToString());
                    }
                    else
                    {
                        Report.Pass("Element " + locatorName + " Exists on screen: " + exists.ToString());
                        log.Info("Element" + locatorName + " Exists on screen: " + exists.ToString());
                    }

                }
                catch (NoSuchElementException e)
                {
                    Report.Pass("Element " + locatorName + " exists on screen: " + exists.ToString());
                    log.Info("Element" + locatorName + " Exists on screen: " + exists.ToString());
                }
                catch (Exception e)
                {
                    Report.Fail("Locator name : " + locatorName + " , " + e.Message);
                    log.Error("Locator name : " + locatorName + " , " + e.Message);
                }

            }

            return exists;
        }
        /// <summary>
        /// This method moves the control to an element
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="locatorName"></param>
        /// <param name="values"></param>
        public static void MoveToElement(string pageName, string locatorName, params String[] values)
        {
            By element = null;
            if (values.Length == 0)
            {
                element = ElementLocator.GetLocator(pageName, locatorName);
            }
            else
            {
                element = ElementLocator.GetLocator(pageName, locatorName, values[0]);
            }
            if (element != null)
            {
                try
                {
                    wait.Until(WaitHelper.ExpectedConditions.PresenceOfAllElementsLocatedBy(element));
                    Actions actions = new Actions(driver);
                    actions.MoveToElement(driver.FindElement(element)).Click().Build().Perform();
                    Report.Pass(locatorName + " is in Focus");
                    log.Info(locatorName + " is in Focus");
                }
                catch (WebDriverException wd)
                {
                    Report.Fail("Exception on the element " + locatorName + "  " + wd.Message);
                    log.Error(TestContext.CurrentContext.Test.MethodName + " " + locatorName + " not found");
                }
            }
            else
            {
                Report.Fail((locatorName + " not found"));
                log.Error(TestContext.CurrentContext.Test.MethodName + " " + locatorName + " not found");
            }
        }

        /// <summary>
        /// Method to perform swipe action
        /// </summary>
        public static void Swipe(double x1, double y1, double x2, double y2) //string pageName, string locatorName, params string[] values
        {
            TouchAction touch = null;
            if (ConfigurationManager.AppSettings["os"].ToLower().Equals("android"))
            {
                AndroidDriver<AndroidElement> androidDriver = (AndroidDriver<AndroidElement>)driver;
                touch = new TouchAction(androidDriver);
            }
            else if (ConfigurationManager.AppSettings["os"].ToLower().Equals("ios"))
            {
                IOSDriver<IOSElement> iOSDriver = (IOSDriver<IOSElement>)driver;
                touch = new TouchAction(iOSDriver);
            }
            
            touch.Press(x1, y1).MoveTo(x2, y2).Release().Perform();
            Report.Pass("Page scrolled");
            log.Info("Page scrolled");            
        }
    }
}
