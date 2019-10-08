using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using System;
using System.Configuration;
using System.IO;

namespace AutomationFramework.Framework
{
    class ExtentManager
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly Lazy<ExtentReports> lazy = new Lazy<ExtentReports>(() => new ExtentReports());

        public static ExtentReports Instance { get { return lazy.Value; } }

        static ExtentManager()
        {
            //To obtain the current solution path/project path
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            log.Info("Report Path1 : " + GlobalVariables.reportpath);
            string reportpath = GlobalVariables.reportpath;

            try
            {
                Directory.CreateDirectory(reportpath);
            }
            catch (IOException e)
            {
                reportpath = projectPath + "Reports\\BuildSanityReport\\";
                Directory.CreateDirectory(reportpath);
            }
            log.Info("Report Path2 : "+reportpath);
            //Append the html report file to current project path
            string reportname = ConfigurationManager.AppSettings["application"] + "_" + GlobalVariables.environment+ "_" + GlobalVariables.clientname + DateTime.Now.ToString("yyyyMMdd") + "T" + DateTime.Now.ToString("HHmmss");
            string reportfilename = reportpath + reportname + ".html";
            log.Info("Report Filename : "+reportfilename);
            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(reportfilename);
            //htmlReporter.Configuration().DocumentTitle = "Automation Report";
            //htmlReporter.Configuration().Theme = Theme.Dark;
            //htmlReporter.Configuration().ChartVisibilityOnOpen = false;
            Instance.AddSystemInfo("Environment", GlobalVariables.environment);
            Instance.AddSystemInfo("Application", ConfigurationManager.AppSettings["application"]);
            Instance.AddSystemInfo("Group Name", GlobalVariables.clientname);

            Instance.AddSystemInfo("Platform", GlobalVariables.platform);
            if (GlobalVariables.platform.ToLower() == "mobile")
            {
                Instance.AddSystemInfo("OS", GlobalVariables.os);
                Instance.AddSystemInfo("OS Version", ConfigurationManager.AppSettings["version"]);
            }
            else if (GlobalVariables.platform.ToLower() == "win")
            {
                Instance.AddSystemInfo("Browser", GlobalVariables.browser);
            }            
            Instance.AddSystemInfo("Date", DateTime.Now.ToString());
            Instance.AttachReporter(htmlReporter);
        }        
    }
}