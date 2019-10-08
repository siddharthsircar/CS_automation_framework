using AutomationFramework.Framework;
using AutomationFramework.Pages.AndroidPages.Dashboard;
using AutomationFramework.Pages.AndroidPages.Login;
using AutomationFramework.Pages.AndroidPages.MyProfile;
using AutomationFramework.Pages.AndroidPages.Trackers;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AutomationFramework.Tests.AndroidTests
{
    /// <summary>
    /// Mobile Weight Tracker Test Class
    /// </summary>
    [TestFixture]
    public class MobileWeightTracker : Base
    {
        String pageName;

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Mob.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public MobileWeightTracker()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        /// <summary>
        /// TestCase: Verify Weight Tracker page UI
        /// </summary>
        [Test]
        [Order(1)]
        public void TC_VerifyWeightTrackerUIElements()
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

            //Navigate to Tracker Home Page
            Page_MProfile trcker = new Page_MProfile();
            trcker.NavigateToTrackerHomePage();

            //Navigate to Weight Tracker
            CommonTrackers ct = new CommonTrackers(softassertions);
            ct.NavigateToTracker("Weight");

            //Validate Page UI Elements            
            List<string[]> uielements = CSVReaderDataTable.GetCSVData("MobileTrackerData", pageName, "uielements");
            ct.ValidatePageUI(uielements, "Page_MWeightTracker");
            softassertions.AssertAll();
        }

        /// <summary>
        /// TestCase: Verify Weight Tracker
        /// </summary>
        [Test]
        [Order(2)]
        public void TC_VerifyWeightTracker()
        {
            // Enter Tracker value and Update
            List<string[]> trackerdata = CSVReaderDataTable.GetCSVData("MobileInputData", pageName, "weightdata");
            Page_MWeightTracker weightTracker = new Page_MWeightTracker(softassertions);
            weightTracker.UpdateWeightTracker(trackerdata);

            // Verify Tracker History
            List<string[]> trackerhistory = CSVReaderDataTable.GetCSVData("MobileTrackerData", pageName, "trackerhistory");
            TestContext.WriteLine("trackerhistory count" + trackerhistory.Count);
            weightTracker.VerifyTrackerHistory(trackerhistory);
            softassertions.AssertAll();
        }        
    }
}
