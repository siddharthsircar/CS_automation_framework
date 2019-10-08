using AutomationFramework.Framework;
using AutomationFramework.Pages.MobileAPIs;
using AutomationFramework.Pages.WebPages;
using AutomationFramework.Pages.WebPages.Journey;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests.MobileAPIs
{
    class MobileAPIsTests : BaseAPI
    {


        //public MobileAPIsTests()
        //{
        //    MAPI_Login mal = new MAPI_Login(softassertions);
        //    mal.VerifyAuthToken();
        //    //Mobile Login Detail Api
        //    mal.VerifyLoginDetailsAPI(GlobalVariables.username);

        //}
       
        [Test]
        [Order(1)]
        public void TC_MobileTokenAPIs()
        {
            MAPI_Login mal = new MAPI_Login(softassertions);
            //Mobile Token API return the Authorization token  
            mal.VerifyAuthToken();
            //Assert.Contains("Barer ", , "Tocken is missing");
            mal.getstatuscode("Token API response code");
            softassertions.AssertAll();
        }
        [Test]
        [Order(2)]
        public void TC_MobileLoginDetailsAPIs()
        {

            // Mobile Login Detail Api
            MAPI_Login mal = new MAPI_Login(softassertions);
            mal.VerifyLoginDetailsAPI(GlobalVariables.username);
            mal.getstatuscode("Login Details API response code");
            softassertions.AssertAll();
        }
        [Test]
        [Order(3)]
        public void TC_MobileTosAPIs()
        {

            //Mobile TOS API 
            MAPI_Login mal = new MAPI_Login(softassertions);
            mal.VerifyMobileTosAPI();
            mal.getstatuscode("TOC API response code");
            softassertions.AssertAll();

        }

        [Test, Order(4)]
        public void TC_MobileConfiguratonsAPIs()
        {

            //Mobile Configuration API
            MAPI_Dashboard mad = new MAPI_Dashboard(softassertions);
            mad.VerifyMobileConfigurationAPI(GlobalVariables.username);
            mad.getstatuscode("Configuration API response code");
            softassertions.AssertAll();
        }
        [Test, Order(5)]
        public void TC_MobileNotificationCountAPIs()
        {
            MAPI_Dashboard mad = new MAPI_Dashboard(softassertions);
            //Notification Count
            mad.VerifyNotificationCountAPI();
            mad.getstatuscode("Notification count API response code");
            softassertions.AssertAll();
        }
        [Test, Order(6)]
        public void TC_MobileDashboardTileAPIs()
        {

            //Dashboad Tile
            MAPI_Dashboard mad = new MAPI_Dashboard(softassertions);
            mad.VerifyDashboardTile();
            mad.getstatuscode("Dashboard Tile API response code");
            softassertions.AssertAll();
        }

        [Test, Order(7)]
        public void TC_MobileNewMemberJourneyAPIs()
        {
            //New Member Journey
            MAPI_Dashboard mad = new MAPI_Dashboard(softassertions);
            mad.VerifyNewMemberJourney();
            mad.getstatuscode("Journey Tiles API response code");
            // mad.VerifyJourneyTiles("MobileJourneyAPIContent");
            softassertions.AssertAll();
        }
        [Test, Order(8)]
        public void TC_MobileEngagementStreamAPIs()
        {
            MAPI_Dashboard mad = new MAPI_Dashboard(softassertions);
            mad.VerifyEstream();
            mad.getstatuscode("Engagement Stream API response code");
            softassertions.AssertAll();

        }
        [Test, Order(9)]
        public void MobileTrckersListOnProfile()
        {
            String[] trackers = { "Stress", "Quit Tobacco", "Weight", "Blood Pressure", "Cholesterol", "Nutrition", "Water", "Physical Activity", "Sleep", "Oral Health" };
            MAPI_Trackers mat = new MAPI_Trackers(softassertions);
            mat.VerifyAllmemberTrakerListAPI(trackers);
            mat.getstatuscode(" MemberTrckersListOnProfile API response code");
            softassertions.AssertAll();
        }

        [Test, Order(10)]
        public void MobileStressTrckers()
        {
           
            MAPI_Trackers mat = new MAPI_Trackers(softassertions);
           mat.VerifyTakerAPIs("Stress","Manage Stress");
            mat.SaveTrcker("Stress");
            mat.getstatuscode(" Save Stress Tracker API response code");
            mat.VerifySavedTrcker();
            mat.getstatuscode(" Saved Stress Tracker API response code");
            softassertions.AssertAll();

        }



    }
}
