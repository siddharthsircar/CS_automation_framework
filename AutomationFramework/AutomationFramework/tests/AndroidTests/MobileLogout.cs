using AutomationFramework.Framework;
using AutomationFramework.Pages.AndroidPages;
using AutomationFramework.Pages.AndroidPages.Dashboard;
using AutomationFramework.Pages.AndroidPages.Login;
using AutomationFramework.Pages.AndroidPages.Settings;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests.AndroidTests
{
    /// <summary>
    /// Logout Test Class
    /// </summary>
    [TestFixture, Order(2)]
    public class MobileLogout : Base
    {
        /// <summary>
        /// Test Case to verify successful logout
        /// </summary>
        [Test]
        public void TC_MobileLogout()
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
            
            // Navigate to Settings
            Common settings = new Common();
            settings.TapSettingsIcon();
            
            // Logout
            Page_MSettings logout = new Page_MSettings();
            logout.ClickLogout();
            
            // Verify Successful Logout
            Page_MLogin loginpage = new Page_MLogin();
            Assert.IsTrue(loginpage.AtLoginPage(), "Not navigated to Login Page");
        }
    }
}
