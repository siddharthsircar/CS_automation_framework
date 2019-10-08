using AutomationFramework.Framework;
using AutomationFramework.Pages.IOSPages.Dashboard;
using AutomationFramework.Pages.IOSPages.HealthAssessment;
using AutomationFramework.Pages.IOSPages.Login;
using NUnit.Framework;

namespace AutomationFramework.Tests.IOSTests
{
    /// <summary>
    /// Health Assessment Test Class
    /// </summary>
    [TestFixture]
    public class IOSHA : Base
    {
        Page_IHA ha = null;

        /// <summary>
        /// 
        /// </summary>
        [Test, Order(1)]
        [Category("BuildSmoke")]
        //[Category("Regression")]
        public void TC_FillNormalHA()
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

            ha = new Page_IHA(softassertions);
            ha.setInputFileName("NormalHAData");

            is_soft_assert = true;
            ha.FillHA();
        }
        
    }
}
