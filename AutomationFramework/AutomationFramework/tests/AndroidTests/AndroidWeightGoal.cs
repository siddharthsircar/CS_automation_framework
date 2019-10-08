using AutomationFramework.Framework;
using AutomationFramework.Pages.AndroidPages.Dashboard;
using AutomationFramework.Pages.AndroidPages.Login;
using AutomationFramework.Pages.AndroidPages.MyProfile;
using AutomationFramework.Pages.AndroidPages.Trackers;
using NUnit.Framework;


namespace AutomationFramework.Tests.AndroidTests
{
    /// <summary>
    /// Mobile weight goal class
    /// </summary>
    [TestFixture]
    public class AndroidWeightGoal : Base
    {
        /// <summary>
        /// Test Case: Set up a 
        /// </summary>
        [Test]
        public void TC_CompleteWeightGoal()
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
        }
    }
}
