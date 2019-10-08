using AutomationFramework.Framework;
using AutomationFramework.Pages.AndroidPages;
using AutomationFramework.Pages.AndroidPages.Dashboard;
using AutomationFramework.Pages.AndroidPages.Login;
using AutomationFramework.Pages.AndroidPages.Settings;
using NUnit.Framework;

namespace AutomationFramework.Tests.AndroidTests
{
    /// <summary>
    /// Mobile Login Test Class
    /// </summary>
    [TestFixture,Order(1)]
    public class MobileLogin : Base
    {

        /// <summary>
        /// Test Case: To click skip intro and verify navigated to Login Page
        /// </summary>
        [Test, Order(1)]
        [Category("MobileSanity")]
        public void TC_VerifySkipIntro()
        {
            Page_SkipIntro skipIntro = new Page_SkipIntro(softassertions);
            skipIntro.VerifyElements();

            is_soft_assert = false;
            softassertions.AssertAll();

            skipIntro.ClickSkipIntro();

            Page_MLogin loginpage = new Page_MLogin();
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
            Page_MLogin uielements = new Page_MLogin(softassertions);
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
            Page_MLogin plogin = new Page_MLogin();
            plogin.moblogin();

            //Verify User is at dashboard
            Page_MDashboard Dashboard = new Page_MDashboard();
            Assert.IsTrue(Dashboard.AtDashboard(), "Not at dashboard");

            // Close all Overlays (Popups)
            Page_MDashboard dashboard = new Page_MDashboard();
            dashboard.CloseAllDashboardOverlays();

            // Navigate to Settings
            Common settings = new Common();
            settings.TapSettingsIcon();

            // Logout
            Page_MSettings logout = new Page_MSettings();
            logout.ClickLogout();
        }

    }
}
