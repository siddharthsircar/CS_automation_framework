using NUnit.Framework;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;
using System.Configuration;
using AventStack.ExtentReports.MarkupUtils;
using TestCases.Base;

namespace AutomationFramework.Framework
{
    /// <summary>
    /// Core class that consists of the pre-test and post test actions
    /// </summary>
    public class Base
    {
        /// <summary>
        /// WebDriver instance in Base class
        /// </summary>
        public static IWebDriver driver;

        /// <summary>
        /// WebDriver Wait instance in Base class
        /// </summary>
        public static WebDriverWait wait;
        private Utilities util = new Utilities();
        private Log4net log = new Log4net(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());
        
        /// <summary>
        /// VideoRecorder class object in Base class
        /// </summary>
        public static VideoRecorder rec;
        
        /// <summary>
        /// Softassertion class object in Base class
        /// </summary>
        public static SoftAssertions softassertions;

        /// <summary>
        /// Softassert flag in Base class to driver screenshots for failed test cases
        /// </summary>
        public Boolean is_soft_assert = false;

        /// <summary>
        /// Method initiates the browser before each test class
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            GlobalVariables globalVariables = new GlobalVariables();
            
            //initialize the global variables
            globalVariables.InitializeGlobalVariables();
            if (ConfigurationManager.AppSettings["setvideorecording"].ToLower().Equals("true"))
            {
                string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                string actualpath = pth.Substring(0, pth.LastIndexOf("bin"));
                string projectpath = new Uri(actualpath).LocalPath;
                string videopath = projectpath + ConfigurationManager.AppSettings["recordingpath"];
                System.IO.Directory.CreateDirectory(videopath);
                string videoname = ConfigurationManager.AppSettings["application"] + "_" + GlobalVariables.environment + DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss");
                rec = new VideoRecorder(new RecorderParams(videopath + videoname, 5, SharpAvi.KnownFourCCs.Codecs.MotionJpeg, 40));
                //DirectoryInfo dinfo = new DirectoryInfo(videopath);
                //long sizeofdirectory = DirSize(dinfo);
                //Console.WriteLine(sizeofdirectory);
                //if (sizeofdirectory / 1024 >= 2000)
                //    File.Delete();
            }

            //Create driver
            driver = DriverFactory.InitDriver(ConfigurationManager.AppSettings["platform"], GlobalVariables.browser, GlobalVariables.webdriverhost, GlobalVariables.webdriverport);

            if (driver != null)
            {                
                ExtentTestManager.CreateParentTest(GetType().Name);
                log.Info("Parent Test created in Test report ");
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));
                if (GlobalVariables.platform.ToLower() == "win")
                {
                    driver.Manage().Window.Maximize();
                    //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
                    driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(70);

                    DriverFactory.LoadApplication(driver);
                    log.Info("Driver loaded and Application launch");
                }                
            }
            else
            {
                Console.WriteLine("Driver Not initialized");
                log.Error("Driver is not loaded");
               
            }
        }

        /// <summary>
        /// Method save the extent report and closes the browser after every test class
        /// </summary>
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            //ExtentManager.Instance.Flush();
            if (ConfigurationManager.AppSettings["setvideorecording"].ToLower().Equals("true"))
            {
                rec.Dispose();

            }
            DriverFactory.QuitDriver(driver);            
        }

        /// <summary>
        /// Method creates a test node in the extent report before each test method
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            is_soft_assert = false;
            softassertions = new SoftAssertions();
            ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name);
            
            var categories = TestContext.CurrentContext.Test.Properties["Category"];
            foreach (var category in categories)
            {
                TestContext.WriteLine("Category: " + category);
                ExtentTestManager.GetTest().AssignCategory(category.ToString());
                log.Info("Category: " + category);
            }            
            log.Info("Created node in report with Test case name");
            log.Info("Test case execution start  : "+TestContext.CurrentContext.Test.Name);
        }

        /// <summary>
        /// Method logs the output of a test along with screenshot where necessary after each test method
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            PrintReport();
            ExtentManager.Instance.Flush();

        }

        private void PrintReport()
        {
            string imgpath;

            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = TestContext.CurrentContext.Result.StackTrace;
            var message = TestContext.CurrentContext.Result.Message;
            Status logstatus;
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                logstatus = Status.Fail; // red cross sign before test step 
                Console.WriteLine("Message : " + TestContext.CurrentContext.Result.Message);
                log.Error(TestContext.CurrentContext.Test.MethodName + " Test case fail");
                ExtentTestManager.GetTest().Log(logstatus, stacktrace + "\n" + message);
                if (is_soft_assert == false)
                {
                    if (ConfigurationManager.AppSettings["TestStatScrnshot"].ToLower() == "failed" || ConfigurationManager.AppSettings["TestStatScrnshot"].ToLower() == "both")
                    {
                        imgpath = util.TakeScreenshot(driver);
                        log.Info("Screenshot taken for failed test case : " + imgpath);
                        ExtentTestManager.GetTest().Log(logstatus, "Screenshot: \n", MediaEntityBuilder.CreateScreenCaptureFromPath(imgpath).Build());
                    }
                }
                ExtentTestManager.GetTest().Log(logstatus, MarkupHelper.CreateLabel(TestContext.CurrentContext.Test.Name + " :: " + logstatus, ExtentColor.Red));
                util.WriteToFile(TestContext.CurrentContext.Test.FullName);
            }
            else if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed)
            {
                logstatus = Status.Pass;
                log.Info(TestContext.CurrentContext.Test.Name + "  Test case pass");
                if (is_soft_assert == false)
                {
                    if (ConfigurationManager.AppSettings["TestStatScrnshot"].ToLower() == "passed" || ConfigurationManager.AppSettings["TestStatScrnshot"].ToLower() == "both")
                    {
                        imgpath = util.TakeScreenshot(driver);
                        log.Info("Screenshot taken for failed test case : " + imgpath);
                        ExtentTestManager.GetTest().Log(logstatus, "Screenshot: \n", MediaEntityBuilder.CreateScreenCaptureFromPath(imgpath).Build());
                    }
                }
                ExtentTestManager.GetTest().Log(logstatus, MarkupHelper.CreateLabel(TestContext.CurrentContext.Test.Name + " :: " + logstatus, ExtentColor.Green));

            }
            else if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Warning)
            {
                logstatus = Status.Warning;
                log.Warning(TestContext.CurrentContext.Test.MethodName + " Test case warn");
                ExtentTestManager.GetTest().Log(logstatus, message);
                ExtentTestManager.GetTest().Log(logstatus, MarkupHelper.CreateLabel(TestContext.CurrentContext.Test.Name + " :: " + logstatus, ExtentColor.Orange));
            }
            else if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Skipped)
            {
                logstatus = Status.Skip;
                log.Info(TestContext.CurrentContext.Test.MethodName + " Test case skip");
                ExtentTestManager.GetTest().Log(logstatus, message);
                ExtentTestManager.GetTest().Log(logstatus, MarkupHelper.CreateLabel(TestContext.CurrentContext.Test.Name + " :: " + logstatus, ExtentColor.Blue));
            }
        }
    }
}