using AutomationFramework.Framework;
using AutomationFramework.Pages.AndroidPages.Dashboard;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationFramework.tests.AndroidTests
{
    /// <summary>
    /// Mobile Dashboard Test Class 
    /// </summary>
    [TestFixture]
    public class MobileDashboard : Base
    {
        String pageName;

        /// <summary>
        /// Initializes pageName with class name
        /// </summary>
        public MobileDashboard()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        /// <summary>
        /// Test Case: Validates dashboard header elements
        /// </summary>
        [Test]
        public void TC_DashboardHeader()
        {
            List<string[]> dashboardui = new List<string[]>();
            dashboardui = CSVReaderDataTable.GetCSVData("MobileDashboardData", pageName, "headerelements");
            Page_MDashboard headerUi = new Page_MDashboard(softassertions);
            headerUi.VerifyDashboardHeader(dashboardui);
            softassertions.AssertAll();
        }

        /// <summary>
        /// Test Case: Validates Journeys
        /// </summary>
        [Test]
        public void TC_VerifyRecommendation()
        {
            List<string[]> clientconfig = CSVReaderDataTable.GetCSVData("ClientConfig", GlobalVariables.clientname.ToLower());
            string isJourneyGroup = clientconfig.ElementAt(0)[3];
            if (isJourneyGroup.ToLower().Equals("true"))
            {
                Page_MDashboard jsection = new Page_MDashboard(softassertions);
                Assert.IsTrue(jsection.VerifyJourneySectionPresent(), "Journeys not displayed"); 
            }
            else
            {
                Assert.Ignore("Journey not available for client: " + GlobalVariables.clientname);
            }
        }

        /// <summary>
        /// Test Case: Validates Engagement Stream
        /// </summary>
        [Test]
        public void TC_VerifyEStream()
        {            
            List<string[]> clientconfig = CSVReaderDataTable.GetCSVData("ClientConfig", GlobalVariables.clientname.ToLower());
            string isJourneyGroup = clientconfig.ElementAt(0)[3];
            if (isJourneyGroup.ToLower().Equals("true"))
            {
                Page_MDashboard esection = new Page_MDashboard(softassertions);
                Assert.IsTrue(esection.VerifyEstrmSectionPresent(), "Engagement Stream not displayed");
            }
            else
            {
                Assert.Ignore("Engagement Stream not available for client: " + GlobalVariables.clientname);
            }
        }


    }
}
