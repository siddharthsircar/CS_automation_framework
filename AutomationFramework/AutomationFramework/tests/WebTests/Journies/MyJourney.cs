using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages.Journey;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests.WebTests.Journies
{
    /// <summary>
    /// Test Class: My Journey page
    /// </summary>
    [TestFixture]
    
    public class MyJourney : Base
    {
        string journeyEnabled;
        Common cmn = new Common();
        String pageName;

        public MyJourney()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        /// <summary>
        /// Test Case to verify new member journey recommendations are displayed in the My Journey page
        /// </summary>
        [Test]
        [Category("ProdSanity")]

        [Order(1)]
        public void TC_VerifyNewMemberJourney()
        {
            journeyEnabled = cmn.GetConfig("JourneyEnabled").ElementAt(0)[1].ToLower();
            if (journeyEnabled.Equals("false"))
            {
                Assert.Ignore("Journeys not available for the client");
            }
            //Login
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();

            //Verify Recommendations displayed
            Page_Dashboard dashbrd = new Page_Dashboard();
            Assert.IsTrue(dashbrd.JourneySectionDisplayed(), "Recommendation section missing");
            
            // Navigate to My Journey page
            Common navigate = new Common();
            navigate.GoToMyJourney();

            //Verify Recommendations
            Page_MyJourney journey = new Page_MyJourney(softassertions);
            List<string[]> journeydata = CSVReaderDataTable.GetCSVData("JourneyContent", pageName , "newmember_journey");
            journey.VerifyNewMemberJourney(journeydata);
            is_soft_assert = true;
            softassertions.AssertAll();

            //Logout
            Common logout = new Common();
            logout.LogOut();
        }
    }
}
