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
    [TestFixture]
    [Order(5)]
    public class Journey : Base
    {
        string journeyEnabled;
        Common cmn = new Common();
        string pageName;

        [Test]
        //[Category("Regression")]
        //[Category("JourClientReg")]
        public void TC_VerifyNewMemberJourneyTiles()
        {
            journeyEnabled = cmn.GetConfig("JourneyEnabled").ElementAt(0)[1].ToLower();
            if (journeyEnabled.Equals("false"))
            {
                Assert.Ignore("Journeys not available for the client");
            }
            Page_Journey pjourney = new Page_Journey(softassertions);
            is_soft_assert = true;
            pjourney.InitializeJourneyRequest("/journey?isMobile=false");
            pjourney.SetMethod();
            pjourney.SendRequest();
            pjourney.VerifyJourneyTiles("JourneyAPIContent");
            softassertions.AssertAll();
        }
        [Test]
        //[Category("Regression")]
        //[Category("JourClientReg")]
        public void TC_VerifyJourneyBanners()
        {
            journeyEnabled = cmn.GetConfig("JourneyEnabled").ElementAt(0)[1].ToLower();
            if (journeyEnabled.Equals("false"))
            {
                Assert.Ignore("Journeys not available for the client");
            }
            Page_Journey pjourney = new Page_Journey(softassertions);
            is_soft_assert = true;
            pjourney.InitializeJourneyRequest("/memberjourney/language/1033/banner");
            pjourney.SetMethod();
            pjourney.SendRequest();
            pjourney.VerifyJourneyBanner();
            softassertions.AssertAll();
        }

        /// <summary>
        /// Test Case: Verify clinical journies aassigned after completing HA
        /// </summary>
        [Test]
        public void TC_VerifyClinicalJourney()
        {
            journeyEnabled = cmn.GetConfig("JourneyEnabled").ElementAt(0)[1].ToLower();
            if (journeyEnabled.Equals("false"))
            {
                Assert.Ignore("Journeys not available for the client");
            }
            Page_Journey pjourney = new Page_Journey(softassertions);
            is_soft_assert = true;
            pjourney.InitializeJourneyRequest("/journey?isMobile=false");
            pjourney.SetMethod();
            pjourney.SendRequest();
            pjourney.VerifyClinicalJourney();
            softassertions.AssertAll();               
        }
    }
}
