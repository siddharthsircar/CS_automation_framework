using AutomationFramework.Framework;
using AutomationFramework.Pages.IOSPages;
using AutomationFramework.Pages.IOSPages.Dashboard;
using AutomationFramework.Pages.IOSPages.Login;
using AutomationFramework.Pages.IOSPages.Settings;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests.IOSTests
{
    /// <summary>
    /// Logout Test Class
    /// </summary>
    [TestFixture, Order(2)]
    public class IOSLogout : Base
    {
        /// <summary>
        /// Test Case to verify successful logout
        /// </summary>
        [Test]
        public void TC_MobileLogout()
        {
            // Skip Intro
            Page_ISkipIntro skipIntro = new Page_ISkipIntro();
            skipIntro.ClickSkipIntro();

            // Perform Login
            Page_ILogin plogin = new Page_ILogin();
            plogin.moblogin();

            // Close all Overlays (Popups)
            Page_IDashboard dashboard = new Page_IDashboard();
            dashboard.CloseAllDashboardOverlays();
            
            // Navigate to Settings
            Common settings = new Common();
            settings.TapSettingsIcon();
            
            // Logout
            Page_ISettings logout = new Page_ISettings();
            logout.ClickLogout();
            
            // Verify Successful Logout
            Page_ILogin loginpage = new Page_ILogin();
            Assert.IsTrue(loginpage.AtLoginPage(), "Not navigated to Login Page");
        }
    }
}
