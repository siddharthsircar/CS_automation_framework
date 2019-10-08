using AutomationFramework.Framework;
using AutomationFramework.Pages.AndroidPages.Dashboard;
using AutomationFramework.Pages.AndroidPages.HealthAssessment;
using AutomationFramework.Pages.AndroidPages.Login;
using NUnit.Framework;

namespace AutomationFramework.Tests.AndroidTests
{
    /// <summary>
    /// Health Assessment Test Class
    /// </summary>
    [TestFixture]
    public class MobileHA : Base
    {
        Page_MHA ha = null;

        /// <summary>
        /// 
        /// </summary>
        [Test, Order(1)]
        [Category("BuildSmoke")]
        //[Category("Regression")]
        public void TC_FillNormalHA()
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

            ha = new Page_MHA(softassertions);
            ha.setInputFileName("NormalHAData");

            is_soft_assert = true;
            ha.FillHA();
        }
        
    }
}
