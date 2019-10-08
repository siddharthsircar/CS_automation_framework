using AutomationFramework.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using WaitHelper = SeleniumExtras.WaitHelpers;
using System;
using System.Threading;
using NUnit.Framework;

namespace AutomationFramework.Keywords
{
    /// <summary>
    /// Selenium keywords class
    /// </summary>
    public class SeleniumKeywords : Base
    {
        private static IWebElement element = null;
        private static ExtentTestManager Report = new ExtentTestManager();
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static ITargetLocator currentframe;

        //private static IWebDriver driver;

        //static SeleniumKeywords()
        //{
        //    driver = DriverFactory.GetInstance().GetDriver();
        //}
        /// <summary>
        /// This method sets the text in a text box 
        /// Third parameter is use to parametrazied the object reposatory, if value length is 1 means that OR is not parametrazied. 
        /// if grater than one means OR is parameterazied and we need to pass 4 variables for Page Class.
        /// </summary>
        /// <param name="by">Element locator</param>
        /// <param name="text">Text to be set</param>
       
        public static void SetText(String pageName, String locatorName , String text, params String[] values)
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
                    //JavaScriptKeywords.HighlightElement(driver.FindElement(element));
                    driver.FindElement(element).Clear();
                    driver.FindElement(element).SendKeys(text);
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
                log.Error(TestContext.CurrentContext.Test.MethodName+ " " +locatorName + " not found");
            }
        }
        /// <summary>
        /// Handle browser alert messages
        /// </summary>
        /// <param name="alert"></param>
        public static void HandelAlerts(String alert)
        {
            switch(alert)
            {
                case "OK":
                    driver.SwitchTo().Alert().Accept();
                    break;
                case "Cancel":
                    driver.SwitchTo().Alert().Dismiss();
                    break;

            }
            driver.SwitchTo().ActiveElement();

        }

        /// <summary>
        /// This method gets the text of a control
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="locatorName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string GetText(string pageName, string locatorName, params string[] values )
        {
            string text = "";
            By element = null;
            
            if (values.Length == 0)
            {
                element = ElementLocator.GetLocator(pageName, locatorName);
                //JavaScriptKeywords.HighlightElement(driver.FindElement(element));
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
                    text = driver.FindElement(element).Text;
                    Report.Info("Innertext of " + locatorName + " : " + text);
                    log.Info("Innertext of " + locatorName + " : " + text);
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
            
            return text;
        }

        /// <summary>
        /// This method click on a control 
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="locatorName"></param>
        /// <param name="values"></param>
        public static void Click(string pageName,string locatorName, params string[] values)
        {
            By element = null;
            if (values.Length == 0)
            {
                element = ElementLocator.GetLocator(pageName, locatorName);
            }
            else
            {
                element = ElementLocator.GetLocator(pageName, locatorName,values);
            }
            if (element != null)
            {
                //try
                //{
                wait.Until(WaitHelper.ExpectedConditions.ElementToBeClickable(element));
                JavaScriptKeywords.HighlightElement(driver.FindElement(element));
                driver.FindElement(element).Click();
                Report.Pass("Clicked On " + locatorName);
                log.Info("Clicked On " + locatorName);
                //}catch(WebDriverException wd)
                //{
                //    Report.Fail("Exception on the element "+ locatorName+"  "+wd.Message);
                //    log.Error(TestContext.CurrentContext.Test.MethodName + " Exception " + wd.Message);
                //}
            }
            else
            {
                Report.Fail(locatorName + " not found");
                log.Error(TestContext.CurrentContext.Test.MethodName + " " + locatorName + " not found");
            }

        }

        ///// <summary>
        ///// This method click on a control 
        ///// </summary>
        ///// <param name="pageName"></param>
        ///// <param name="locatorName"></param>
        ///// <param name="elementname"></param>
        ///// <param name="values"></param>
        //public static void Click(string pageName, string locatorName, string elementname,params string[] values)
        //{
        //    By element = null;
        //    if (values.Length == 0)
        //    {
        //        element = ElementLocator.GetLocator(pageName, locatorName);
        //    }
        //    else
        //    {
        //        element = ElementLocator.GetLocator(pageName, locatorName, values);
        //    }
        //    if (element != null)
        //    {
        //        //try
        //        //{
        //        wait.Until(WaitHelper.ExpectedConditions.ElementToBeClickable(element));
        //        JavaScriptKeywords.HighlightElement(driver.FindElement(element));
        //        driver.FindElement(element).Click();
        //        Report.Pass("Clicked On " + elementname);
        //        log.Info("Clicked On " + elementname);
        //        //}catch(WebDriverException wd)
        //        //{
        //        //    Report.Fail("Exception on the element "+ locatorName+"  "+wd.Message);
        //        //    log.Error(TestContext.CurrentContext.Test.MethodName + " Exception " + wd.Message);
        //        //}
        //    }
        //    else
        //    {
        //        Report.Fail(elementname + " not found");
        //        log.Error(TestContext.CurrentContext.Test.MethodName + " " + elementname + " not found");
        //    }

        //}

        /// <summary>
        /// This method select the data from Drop Down 
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="locatorName"></param>
        /// <param name="selectText"></param>
        /// <param name="values"></param>
        public static void SelectValueFromDropdown(string pageName, string locatorName, String selectText, params String[] values)
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
                    wait.Until(WaitHelper.ExpectedConditions.ElementToBeClickable(element));
                    //element = wait.Until(WaitHelper.ExpectedConditions.ElementIsVisible(by));
                    SelectElement select = new SelectElement(driver.FindElement(element));//Used to select from the drop down
                    select.SelectByText(selectText);
                    Report.Pass("Element Selected :" + locatorName);
                    log.Info("Element Selected :" + locatorName);
                }
                catch(WebDriverException wd)
                {
                    Report.Fail("Exception on the element " + locatorName + "  " + wd.Message);
                    log.Error(TestContext.CurrentContext.Test.MethodName + " " + selectText + " not found");
                }
            }
            else
            {
                Report.Fail(values[0] + " not found");
                log.Error(TestContext.CurrentContext.Test.MethodName + " "+ values[0] + " not found");
            }

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
                    JavaScriptKeywords.HighlightElement(driver.FindElement(element));
                    driver.FindElement(element).Clear();
                    Report.Pass(locatorName + "Cleared");
                    log.Info(locatorName + "Cleared");
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

        /// <summary>
        /// This method will return value of given atttribute
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="locatorName"></param>
        /// <param name="attributeValue"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string GetAttributeValue(string pageName, string locatorName, String attributeValue, params String[] values)
        {
            string text = "false";
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
                    text = driver.FindElement(element).GetAttribute(attributeValue);
                    Report.Pass(locatorName + "Attribute " + attributeValue + " value is: " + text);
                    log.Info(locatorName + "Attribute " + attributeValue + " value is: " + text);
                }
                catch(WebDriverException wd)
                {
                    Report.Fail("Exception on the element " + locatorName + "  " + wd.Message);
                    log.Error(TestContext.CurrentContext.Test.MethodName + " " + locatorName + " not found");
                }
            }
            else
            {
                Report.Fail(locatorName + " not found ");
                log.Error(TestContext.CurrentContext.Test.MethodName + " " + locatorName + " not found");
            }
            return text;
        }
        
        /// <summary>
        /// This method check that the control is disbled
        /// </summary>
        /// <param name="by">Element locator</param>
        /// <returns>It return the boolean object</returns>
        public static Boolean IsDisabled(string pageName, string locatorName, params String[] values)
        {
            Boolean isdisabled = false;
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
                    string text = driver.FindElement(element).GetAttribute("disabled");
                    isdisabled = Convert.ToBoolean(text);
                    Report.Pass(locatorName + " is Disabled : " + isdisabled);
                    log.Info(locatorName + " is Disabled : " + isdisabled);
                }
                catch(WebDriverException wd)
                {
                    Report.Fail("Exception on the element " + locatorName + "  " + wd.Message);
                    log.Error(TestContext.CurrentContext.Test.MethodName + " " + locatorName + " not found");
                }
            }
            else
            {
                Report.Fail(locatorName + " not found ");
                log.Error(TestContext.CurrentContext.Test.MethodName + " " + locatorName + " not found");
            }
            return isdisabled;
        }

        /// <summary>
        /// This method check that the control is readonly
        /// </summary>
        /// <param name="by">Element locator</param>
        /// <returns>It return the boolean object</returns>
        public static Boolean IsReadOnly(string pageName, string locatorName, params String[] values)
        {
            Boolean isreadonly = false;
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
                    string text = driver.FindElement(element).GetAttribute("readonly");
                    isreadonly = Convert.ToBoolean(text);
                    Report.Pass(locatorName + " is ReadOnly : " + isreadonly);
                    log.Info(locatorName + " is ReadOnly : " + isreadonly);
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
            return isreadonly;
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
            bool exists=false;
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
                    IWebElement el = driver.FindElement(element);
                    wait.Until(WaitHelper.ExpectedConditions.ElementExists(element));
                    exists = el.Displayed;
                    if(exists == true)
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
                catch(NoSuchElementException e)
                {
                    Report.Fail("Unable to locate element: "+ locatorName);
                    log.Error(e.Message);
                }
                catch(Exception e)
                {
                    Report.Fail("Locator name : "+locatorName+" , "+e.Message);
                    log.Error("Locator name : " + locatorName + " , " + e.Message);
                }
                
                
            }
           
            return exists;
        }

        ///// <summary>
        ///// This method check the existence of a particular control
        ///// </summary>
        ///// <param name="pageName"></param>
        ///// <param name="locatorName"></param>
        ///// <param name="elementname"></param>
        ///// <param name="values"></param>
        ///// <returns></returns>
        //public static Boolean IsElementPresent(string pageName, string locatorName, string elementname, params String[] values)
        //{
        //    bool exists = false;
        //    By element = null;
        //    if (values.Length == 0)
        //    {
        //        element = ElementLocator.GetLocator(pageName, locatorName);
        //    }
        //    else
        //    {
        //        element = ElementLocator.GetLocator(pageName, locatorName, values);
        //    }
        //    if (element != null)
        //    {
        //        try
        //        {
        //            exists = driver.FindElement(element).Displayed;
        //            JavaScriptKeywords.HighlightElement(driver.FindElement(element));
        //            Report.Pass("Element " + elementname + " Exists on screen: " + exists.ToString());
        //            log.Info("Element" + elementname + " Exists on screen: " + exists.ToString());
        //        }
        //        catch (Exception e)
        //        {
        //            Report.Fail("Exception on Element:" + elementname + e.Message);
        //            log.Error(TestContext.CurrentContext.Test.MethodName + "Element locator:" + elementname + " not found");
        //        }


        //    }
        //    else
        //    {
        //        Report.Fail("Element locator: " + elementname + " not found");
        //        log.Error(TestContext.CurrentContext.Test.MethodName + "Element locator:" + elementname + " not found");
        //    }
        //    return exists;
        //}

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
                    IWebElement el = driver.FindElement(element);
                    exists = el.Displayed;
                    if(exists == true)
                    {
                        JavaScriptKeywords.HighlightElement(el);
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
        public static Boolean IsElementNotVisible (string pageName, string locatorName, params String[] values)
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
                    IWebElement el = driver.FindElement(element);
                    exists = el.Displayed;
                    if (exists == true)
                    {
                        JavaScriptKeywords.HighlightElement(el);
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
        public static void MoveToElement(string pageName,string locatorName, params String[] values)
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
                    Actions actions = new Actions(driver);
                    actions.MoveToElement(driver.FindElement(element)).Click().Build().Perform();
                    Report.Pass(locatorName + " is in Focus");
                    log.Info(locatorName + " is in Focus");
                }
                catch(WebDriverException wd)
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

        public static void MoveAndClickAtSpecificPosition(string pageName, string locatorName,int w,int widthmultiple,int h,int heightmultiple, params String[] values)
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
                    IWebElement ele = driver.FindElement(element);
                    int height = ele.Size.Height;
                    int width = ele.Size.Width;
                    
                    int move_width = (width / w) * widthmultiple;
                    int move_height = (height / h) * heightmultiple;
                    
                    Actions actions = new Actions(driver);
                    actions.MoveToElement(ele, move_width, move_height).Click().Build().Perform();
                    //actions.MoveToElement(ele).MoveByOffset(move_width, move_height).Click().Build().Perform();
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
        /// Refresh browser page
        /// </summary>
        public static void RefreshPage()
        {
            driver.Navigate().Refresh();
            Report.Info("Page Refreshed");
        }

        /// <summary>
        /// This method press the specified key from Keyboard
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="locatorName"></param>
        /// <param name="values"></param>
        public static void PressKey(string pageName, string locatorName, string keyAction, params String[] values)

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
            wait.Until(WaitHelper.ExpectedConditions.PresenceOfAllElementsLocatedBy(element));
            
            switch (keyAction)
            {
                //delete the text by selecting existing text
                case "DeleteAllText":
                    {

                        driver.FindElement(element).SendKeys(Keys.Control + "a");
                        driver.FindElement(element).SendKeys(Keys.Delete);
                        Report.Pass("Delete the text form "+locatorName);
                        log.Info("Delete the text form " + locatorName);
                        break;
                    }
                    //press enter key
                case "Enter":
                    {
                       driver.FindElement(element).SendKeys(Keys.Enter);
                       Report.Pass("Enter Key is press on  " + locatorName);
                        log.Info("Enter Key is press on  " + locatorName);
                        break;
                    }
                default:
                    {
                        Report.Fail(locatorName + " Text not cleared");
                        log.Error(TestContext.CurrentContext.Test.MethodName + " " + locatorName + " not found");
                        break;
                    }
            }
          
        }

        /// <summary>
        /// This method Navigate the driver to particular url
        /// </summary>
        /// <param name="url">url name</param>
        public static void NavigateToUrl(string url)
        {
            driver.Navigate().GoToUrl(url);
            log.Info("Navigate to url:" + url);
        }
        /// <summary>
        /// This method Navigate control to iframe
        /// </summary>
        /// <param name="frameid">FrameId</param>
 		public static void NavigateToIFrame(string frameid)
        {
            Thread.Sleep(2000);
            currentframe = driver.SwitchTo();
            currentframe.Frame(frameid);
            log.Info("Moved to IFrame");
            
        }

        /// <summary>
        /// Method to Scroll down the iFrame
        /// </summary>
        public static void IframeScrollDown()
        {
            currentframe.ActiveElement().SendKeys(Keys.Tab);
            currentframe.ActiveElement().SendKeys(Keys.PageDown);
        }

        /// <summary>
        /// This method is use to move back to default content from IFrame
        /// </summary>
        public static void NavigateToDefaultContent()
        {
            driver.SwitchTo().DefaultContent();
            //Report.Pass("Moved to DefaultContent");
            log.Info("Moved to DefaultContent");

        }
        /// <summary>
        /// This method moves to back screen
        /// </summary>
        public static void NavigateToPreviousPage()
        {
            driver.Navigate().Back();
            Report.Pass("Moved back to previous page");
        }
        /// <summary>
        /// this method verify whether the two passed strings are equal or not
        /// </summary>
        /// <param name="text1"></param>
        /// <param name="text2"></param>
        /// <returns></returns>
        public static Boolean VerifyText(string text1, string text2)
        {
            return (text1.Trim().Equals(text2.Trim()));
        }
        /// <summary>
        /// this method verify whether the string two is a part of string one 
        /// </summary>
        /// <param name="text1"></param>
        /// <param name="text2"></param>
        /// <returns></returns>
        public static Boolean VerifyTextContains(string text1, string text2)
        {
            return (text1.Trim().Contains(text2.Trim()));
        }

        /// <summary>
        /// This method returns the current page title
        /// </summary>
        /// <returns></returns>
        public static string GetPageTitle()
        { string title = "Null";
            try
            {
                title=driver.Title;
            }catch (WebDriverException wd)
            {
                Report.Info("Page is not loaded" + wd.Message);
                log.Info("Page is not loaded" + wd.Message);
            }
            return title;
        }

        /// <summary>
        /// Method to fetch URL of current page
        /// </summary>
        /// <returns></returns>
        public static string GetPageUrl()
        {
            string url = "Null";
            try
            {
                url = driver.Url;
            }
            catch (WebDriverException wd)
            {
                Report.Info("Page is not loaded" + wd.Message);
                log.Info("Page is not loaded" + wd.Message);
            }
            return url;
        }

        /// <summary>
        /// Method to switch between tabs
        /// </summary>
        /// <param name="index"></param>
        public static void SwitchToTab(int index)
        {
            try
            {
                //ArrayList<String> tabs2 = new ArrayList<String>(driver.getWindowHandles());
                //driver.switchTo().window(tabs2.get(1));
                //driver.SwitchTo().Window(driver.WindowHandles.First());
                driver.SwitchTo().Window(driver.WindowHandles[index-1]);
                log.Info("Focus is switched to tab"+index);
            }
            catch (WebDriverException wd)
            {
                Report.Info("Page is not loaded" + wd.Message);
                log.Info("Page is not loaded" + wd.Message);
            }

        }

        /// <summary>
        /// Method to close current tab
        /// </summary>
        public static void CloseCurrentTab()
        {    
            try
            {
                driver.Close();
                log.Info("Current tab closed");
            }
            catch (WebDriverException wd)
            {
                Report.Info("Page is not loaded" + wd.Message);
                log.Info("Page is not loaded" + wd.Message);
            }
           
        }

        /// <summary>
        /// Method to handle alert
        /// </summary>
        public static void HandleAlert()
        {
            if (wait == null)
            {
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            }

            try
            {
                IAlert alert = wait.Until(drv => {
                    try
                    {
                        return drv.SwitchTo().Alert();
                    }
                    catch (NoAlertPresentException)
                    {
                        return null;
                    }
                });
                alert.Accept();
            }
            catch (WebDriverTimeoutException) { /* Ignore */ }
        }
    }
}
