using AutomationFramework.Framework;
using AutomationFramework.Pages.IOSPages.Dashboard;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationFramework.tests.IOSTests
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
            Page_IDashboard headerUi = new Page_IDashboard(softassertions);
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
                Page_IDashboard jsection = new Page_IDashboard(softassertions);
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
                Page_IDashboard esection = new Page_IDashboard(softassertions);
                Assert.IsTrue(esection.VerifyEstrmSectionPresent(), "Engagement Stream not displayed");
            }
            else
            {
                Assert.Ignore("Engagement Stream not available for client: " + GlobalVariables.clientname);
            }
        }


    }
}
