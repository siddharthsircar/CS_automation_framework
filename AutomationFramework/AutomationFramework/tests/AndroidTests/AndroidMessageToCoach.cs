using AutomationFramework.Framework;
using AutomationFramework.Pages.AndroidPages;
using AutomationFramework.Pages.AndroidPages.Dashboard;
using AutomationFramework.Pages.AndroidPages.Login;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationFramework.Tests.AndroidTests
{
    [TestFixture]
    public class AndroidMessageToCoach:Base
    {
        string pageName;
        /// <summary>
        /// Initializes pageName with class name
        /// </summary>
        public AndroidMessageToCoach()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
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
        [Test,Order(2)]
        public void TC_VerifyMessageToCoachScreen()
        {
            Page_AndroidMessageToCoach mtc = new Page_AndroidMessageToCoach(softassertions);
            mtc.NavigateToMessageToCoach();
            Thread.Sleep(5000);
            List<string[]> messagetocoachui = new List<string[]>();
            messagetocoachui = CSVReaderDataTable.GetCSVData("MobileMessageToCoachData", pageName, "uielements");
            //Page_AndroidMessageToCoach messagecoach = new Page_AndroidMessageToCoach(softassertions);
            mtc.VerifyMessageToCoachScreen(messagetocoachui);
            softassertions.AssertAll();
        }
        [Test,Order(3)]
        public void TC_SendMessage()
        {
            Page_AndroidMessageToCoach mtc = new Page_AndroidMessageToCoach();
            mtc.TapToSendMessage();
            mtc.InputText();
            //Thread.Sleep(2000);
            mtc.SendTextToCoach();
            Assert.AreEqual("Hi Coach", mtc.GetText());
        }
    }
}
