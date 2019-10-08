using AutomationFramework.Framework;
using AutomationFramework.Pages.AndroidPages.Dashboard;
using AutomationFramework.Pages.AndroidPages.Login;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests.AndroidTests
{
    [TestFixture]
    public class AndroidChallenges:Base
    {
        string pageName;
        /// <summary>
        /// Initializes pageName with class name
        /// </summary>
        public AndroidChallenges()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        [Test, Order(1)]
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
        public void TC_VerifyChallenges()
        {

        }
    }
}
