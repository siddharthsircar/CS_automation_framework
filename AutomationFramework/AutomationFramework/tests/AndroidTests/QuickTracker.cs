using AutomationFramework.Framework;
using AutomationFramework.Pages.AndroidPages.Dashboard;
using AutomationFramework.Pages.AndroidPages.Login;
using AutomationFramework.Pages.AndroidPages.Trackers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests.AndroidTests
{
    [TestFixture]
    public class QuickTracker:Base
    {
        String pageName;
        SoftAssertions softAssertions = null;
        string pckgname = ConfigurationManager.AppSettings["apppackage"];

        /// <summary>
        /// Initializes pageName with class name
        /// </summary>
        public QuickTracker()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public QuickTracker(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }
        

        [Test,Order(1)]
        public void TC_Login()
        {
            // Skip Intro
            Page_SkipIntro skipIntro = new Page_SkipIntro();
            skipIntro.ClickSkipIntro();
            // Perform Login
            Page_MLogin plogin = new Page_MLogin();
            plogin.moblogin();
            // Close all Overlays (Popups)
            Page_MDashboard dashboard = new Page_MDashboard();
            dashboard.CloseAllDashboardOverlays();
            
        }
        [Test, Order(2)]
        public void TC_VerifyQuickTrackerScreen()
        {
            Page_AndroidQuickTracker qt = new Page_AndroidQuickTracker(softassertions);
            qt.NavigateToQuickTrackerScreen();
            List<string[]> quicktrackerui = new List<string[]>();
            quicktrackerui = CSVReaderDataTable.GetCSVData("MobileQuickTracker", pageName, "uielements");
            qt.ValidatePageUI(quicktrackerui);
            softassertions.AssertAll();

        }
    }
}
