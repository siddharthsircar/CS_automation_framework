using AutomationFramework.Framework;
using AutomationFramework.Pages.IOSPages;
using AutomationFramework.Pages.IOSPages.Dashboard;
using AutomationFramework.Pages.IOSPages.Login;
using AutomationFramework.Pages.IOSPages.Settings;
using NUnit.Framework;

namespace AutomationFramework.Tests.IOSTests
{
    /// <summary>
    /// Mobile Login Test Class
    /// </summary>
    [TestFixture,Order(1)]
    public class IOSLogin : Base
    {

        /// <summary>
        /// Test Case: To click skip intro and verify navigated to Login Page
        /// </summary>
        [Test, Order(1)]
        [Category("MobileSanity")]
        public void TC_VerifySkipIntro()
        {
            Page_ISkipIntro skipIntro = new Page_ISkipIntro(softassertions);
            skipIntro.VerifyElements();

            is_soft_assert = false;
            softassertions.AssertAll();

            skipIntro.ClickSkipIntro();

            Page_ILogin loginpage = new Page_ILogin();
            Assert.IsTrue(loginpage.AtLoginPage(), "Not navigated to Login Page");
        }
        /// <summary>
        /// Test Case: To verify Login page UI
        /// </summary>
        [Test,Order(2)]
        [Category("MobileSanity")]
        public void TC_VerifyLoginUIElements()
        {
            //Page_SkipIntro skipIntro = new Page_SkipIntro();
            //skipIntro.ClickSkipIntro();
            Page_ILogin uielements = new Page_ILogin(softassertions);
            is_soft_assert = false;
            uielements.VerifyElements();            
            softassertions.AssertAll();
        }
        /// <summary>
        /// Test Case: To verify Login
        /// </summary>
        [Test, Order(3)]
        [Category("MobileSanity")]
        public void TC_VerifyLogin()
        {
            //Page_SkipIntro skipIntro = new Page_SkipIntro();
            //skipIntro.ClickSkipIntro();

            //To call the Page Login Method
            Page_ILogin plogin = new Page_ILogin();
            plogin.moblogin();

            //Verify User is at dashboard
            Page_IDashboard Dashboard = new Page_IDashboard();
            Assert.IsTrue(Dashboard.AtDashboard(), "Not at dashboard");

            // Close all Overlays (Popups)
            Page_IDashboard dashboard = new Page_IDashboard();
            dashboard.CloseAllDashboardOverlays();

            // Navigate to Settings
            Common settings = new Common();
            settings.TapSettingsIcon();

            // Logout
            Page_ISettings logout = new Page_ISettings();
            logout.ClickLogout();
        }

    }
}
