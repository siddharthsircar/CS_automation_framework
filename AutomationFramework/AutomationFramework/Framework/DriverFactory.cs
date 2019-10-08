using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;
using System.Configuration;
using System.Diagnostics;

namespace AutomationFramework.Framework
{
    class DriverFactory
    {        
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static IWebDriver driver=null;
        //private static Process process = new Process();
        private static ExtentTestManager Report = new ExtentTestManager();

        private static DriverFactory instance = new DriverFactory();
        private static string processname = "";

        public static DriverFactory GetInstance()
        {
            return instance;
        }

        /// <summary>
        /// Instantiates a browser based on the browser name
        /// </summary>
        /// <param name="platform"></param>
        /// <param name="browsername"></param>
        /// <param name="hostname"></param>
        /// <param name="portname"></param>
        /// <returns></returns>
        public static IWebDriver InitDriver(String platform, String browsername, string hostname, string portname)
        {
            try
            {
                string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
                string projectPath = new Uri(actualPath).LocalPath;
                DesiredCapabilities capabilities = new DesiredCapabilities();
                if (platform.ToLower().Equals("win"))
                {
                    //KillDriverProcess();
                    if (ConfigurationManager.AppSettings["useseleniumgrid"].ToLower().Equals("true"))
                    {
                        
                        switch (browsername.ToLower())
                        {
                            case "firefox":
                                capabilities.SetCapability("browserName", "firefox");
                                break;
                            case "chrome":
                                capabilities.SetCapability("browserName", "chrome");
                                break;
                            case "ie":
                                capabilities.SetCapability("browserName", "internet explorer");
                                break;
                        }
                        driver = new RemoteWebDriver(new Uri("http://" + hostname + ":" + portname + "/wd/hub"), capabilities);
                    }
                    else
                    {
                        switch (browsername.ToLower())
                        {
                            case "firefox":
                                processname = "geckodriver";
                                driver = new FirefoxDriver();
                                break;
                            case "chrome":
                                ChromeOptions chromeOptions = new ChromeOptions();
                                //chromeOptions.AddArgument("-no-sandbox");
                                //chromeOptions.UnhandledPromptBehavior = UnhandledPromptBehavior.Accept;
                                //processname = "chromedriver";
                                driver = new ChromeDriver();
                                break;
                            case "ie":
                                driver = new InternetExplorerDriver();
                                break;
                        }
                    }
                }
                else if (platform.ToLower() == "mobile" || platform.ToLower() == "mob")
                {
                    //process.StartInfo.FileName = "cmd";
                    //process.StartInfo.Arguments = "/c appium -a " + ConfigurationManager.AppSettings["address"] + " -p " + ConfigurationManager.AppSettings["port"];
                    //process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                    //process.Start();
                    //Thread.Sleep(6000);
                    //log.Info("Appium Server Started at URI: " + ConfigurationManager.AppSettings["appiumhub"]);

                    // Setting Capabilities for Appium            
                    capabilities.SetCapability("platformName", GlobalVariables.os);
                    capabilities.SetCapability("platformVersion", ConfigurationManager.AppSettings["version"]);
                    capabilities.SetCapability("deviceName", ConfigurationManager.AppSettings["devicename"]);

                    if (ConfigurationManager.AppSettings["os"].ToLower() == "android")
                    {
                        //If commented, will use specified App Package and Activity else will install the apk present in the configured location
                        //capabilities.SetCapability("app", ConfigurationManager.AppSettings["apploc"]);
                        capabilities.SetCapability("appPackage", ConfigurationManager.AppSettings["apppackage"]);
                        capabilities.SetCapability("appActivity", ConfigurationManager.AppSettings["appactivity"]);
                        capabilities.SetCapability("autoGrantPermissions", true);
                        capabilities.SetCapability("automationName", "UIAutomator2");                        
                        log.Info("Android driver capability set");

                        driver = new AndroidDriver<AndroidElement>(new Uri("http://" + ConfigurationManager.AppSettings["address"] + ":" + ConfigurationManager.AppSettings["port"] + "/wd/hub"), capabilities, TimeSpan.FromMinutes(3));
                        log.Info("Android driver initiated");
                    }
                    else if (ConfigurationManager.AppSettings["os"].ToLower() == "ios")
                    {
                        capabilities.SetCapability("automationName", "XCUITest");
                        capabilities.SetCapability("app", ConfigurationManager.AppSettings["apploc"]);
                        capabilities.SetCapability("udid", ConfigurationManager.AppSettings["udid"]);
                        //capabilities.SetCapability("bundleId", ConfigurationManager.AppSettings["bundleid"]);
                        //capabilities.SetCapability("xcodeOrgId", ConfigurationManager.AppSettings["xcodeOrgId"]);

                        driver = new IOSDriver<IOSElement>(new Uri("http://" + ConfigurationManager.AppSettings["address"] + ":" + ConfigurationManager.AppSettings["port"] + "/wd/hub"), capabilities, TimeSpan.FromMinutes(3));
                        log.Info("Android driver initiated");
                    }
                    Console.WriteLine(driver);

                }
                
            }
            catch(WebDriverException wd)
            {
                Report.Fail(wd.Message);
                driver.Quit();
            }
         	return driver;
        }
        
        public IWebDriver GetDriver()
        {
            return driver;
        }

        /// <summary>
        /// Opens the specified url after the browser is opened
        /// </summary>
        /// <param name="driver"></param>
        public static void LoadApplication(IWebDriver driver)
        {
            Console.WriteLine(GlobalVariables.baseurl);
            driver.Url = GlobalVariables.baseurl;
        }


        private static void KillDriverProcess()
        {
            //Console.WriteLine("DDDDDDDDDDDDDriver Process : " + processname);
            Process[] driverprocesses = Process.GetProcessesByName(processname);
            foreach(var driverprocess in driverprocesses)
            {
                driverprocess.Kill();
            }
        }
        /// <summary>
        /// Method closes the browser
        /// </summary>
        /// <param name="driver"></param>
        public static void QuitDriver(IWebDriver driver)
        {
            if (GlobalVariables.platform.ToLower().Equals("win"))
            {
                
                driver.Close();
            }                
            //driver.Dispose();
            driver.Quit();  
            //if (GlobalVariables.platform.ToLower() == "mobile")
            //{                
            //    //process.Close();
            //    log.Info("Process Closed");
            //}
        }
    }
}
