using NUnit.Framework;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;
using System.Configuration;
using AventStack.ExtentReports.MarkupUtils;
using System.IO;
using TestCases.Base;
using System.Collections.Generic;
using System.Collections;
using TechTalk.SpecFlow;

namespace AutomationFramework.Framework
{
    [Binding]
    public class SpecBase
    {
        public  static IWebDriver driver;
        private static Log4net log = new Log4net(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());
        public  static WebDriverWait wait;


        [BeforeTestRun]
        public  static void OneTimeSetUp()
        {
            InitializeGlobalVariables();

            driver = DriverFactory.InitDriver(ConfigurationManager.AppSettings["platform"], ConfigurationManager.AppSettings["browser"], ConfigurationManager.AppSettings["webdriverhost"], ConfigurationManager.AppSettings["webdriverport"]);
            if (driver != null)
            {
                //ExtentTestManager.CreateParentTest(GetType().Name);
                log.Info("Parent Test created in Test report ");
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
                if (ConfigurationManager.AppSettings["platform"].ToLower() == "win")
                {
                    driver.Manage().Window.Maximize();
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);

                    DriverFactory.LoadApplication(driver);
                    log.Info("Driver loaded and Application launch");
                }

            }
        }
        private static void InitializeGlobalVariables()
        {
            string defaultbaseurl = ConfigurationManager.AppSettings["baseurl"];
            string defaultenvironment = ConfigurationManager.AppSettings["environment"];
            string defaultclientname = ConfigurationManager.AppSettings["clientname"];
            string defaultbrowser = ConfigurationManager.AppSettings["browser"];
            string defaultplatform = ConfigurationManager.AppSettings["platform"];
            string defaultos = ConfigurationManager.AppSettings["os"];
            string defaultreportpath = ConfigurationManager.AppSettings["reportpath"];
            string defaultusername = ConfigurationManager.AppSettings["username"];
            string defaultpassword = ConfigurationManager.AppSettings["password"];
            string defaultwebdriverhost = ConfigurationManager.AppSettings["webdriverhost"];
            string defaultwebdriverport = ConfigurationManager.AppSettings["webdriverport"];

            GlobalVariables.baseurl = TestContext.Parameters.Get("baseurl", defaultbaseurl);
            GlobalVariables.environment = TestContext.Parameters.Get("env", defaultenvironment);
            GlobalVariables.clientname = TestContext.Parameters.Get("clientname", defaultclientname);
            GlobalVariables.browser = TestContext.Parameters.Get("browser", defaultbrowser);
            GlobalVariables.platform = TestContext.Parameters.Get("platform", defaultplatform);
            GlobalVariables.os = TestContext.Parameters.Get("os", defaultos);
            GlobalVariables.reportpath = TestContext.Parameters.Get("reportpath", defaultreportpath);
            GlobalVariables.reportpath = GlobalVariables.reportpath + "\\BuildSanityReport\\";
            GlobalVariables.username = TestContext.Parameters.Get("username", defaultusername);
            GlobalVariables.password = TestContext.Parameters.Get("password", defaultpassword);
            log.Info("Report PathBase : " + GlobalVariables.reportpath);
            GlobalVariables.webdriverhost = TestContext.Parameters.Get("webdriverhost", defaultwebdriverhost);
            GlobalVariables.webdriverport = TestContext.Parameters.Get("webdriverport", defaultwebdriverport);
        }


    }
}