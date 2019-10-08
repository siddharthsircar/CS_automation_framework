using NUnit.Framework;
using System;
using System.Collections.Generic;
using AutomationFramework.Pages;
using AutomationFramework.Framework;
using AutomationFramework.Pages.WebPages.DevicesAndApps;
using AutomationFramework.Tests.WebTests.Journies;
using System.Linq;

namespace AutomationFramework.Tests.WebPages.dashboard
{
    /// <summary>
    /// Class contains Dashboard test cases
    /// </summary>
    [TestFixture]
    [Order(4)]
    //[Parallelizable(ParallelScope.Fixtures)]
    public class Dashboard : Base
    {
        string journeyEnabled;
        Common cmn = new Common();
        string pageName;
        string locatorindex;
        List<String[]> menuitems = new List<String[]>();
        string coachingEnabled;
        string fitnessEnabled;
        string nutritionEnabled;
        /// <summary>
        /// Initializes pageName with class name
        /// </summary>
        public Dashboard()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        /// <summary>
        /// Test Case: Verifies whether user is at dahsboard
        /// </summary>
        [Test]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        [Order(1)]
        public void TC_VerifyAtDashboard()
        {
            coachingEnabled = cmn.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            journeyEnabled = cmn.GetConfig("JourneyEnabled").ElementAt(0)[1].ToLower();
            //To call the Page Login Method
            Page_Login plogin = new Page_Login();
            plogin.Login();

            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();

            //Page_Dashboard Dashboard = new Page_Dashboard();
           // Assert.IsTrue(Dashboard.AtDashboard());
        }

        /// <summary>
        /// Test Case: Verifies dashboard header elements
        /// </summary>
        [Test]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        [Order(2)]
        public void TC_VerifyDashboardHeader()
        {
            //Page_Login plogin = new Page_Login();
            //plogin.Login();

            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();

            Common header = new Common(softassertions);
            List<string[]> FooterElements = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "headerelements");
            header.VerifyHeader(FooterElements);
            is_soft_assert = true;
            softassertions.AssertAll();
        }

        /// <summary>
        /// Test Case: Verifies grey tiles on the dashboard
        /// </summary>
        [Test]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Order(3)]
        [Category("AllClientReg")]
        
        public void TC_VerifyGreyTile()
        {
            //Page_Login plogin = new Page_Login();
            //plogin.Login();


            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();

            Common dashbrd = new Common(softassertions);
            
            Assert.IsTrue(dashbrd.GreyTileExistence(), "Grey Tiles missing");
            //List<string[]> ExpectedText = CSVReaderDataTable.GetCSVData("GrayTileData", pageName, "greytiles");
            List<string[]> ExpectedText = CSVReaderDataTable.GetCSVData("GrayTileData", pageName, GlobalVariables.clientname);
            Console.WriteLine("ExpectedText length : " + ExpectedText.Count);
            dashbrd.GreyTiles(ExpectedText);
            is_soft_assert = true;
            softassertions.AssertAll();
        }

        /// <summary>
        /// Test Case: Verified Journey Banner in dashboard
        /// </summary>
        [Test]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("JourClientReg")]
        [Order(4)]
        public void TC_VerifyJourneyBanner()
        {
            journeyEnabled = cmn.GetConfig("JourneyEnabled").ElementAt(0)[1].ToLower();

            if (journeyEnabled.Equals("false"))
            {
                Assert.Ignore("Journeys not available for the client");
            }
            Journey nmjb = new Journey();
            nmjb.TC_VerifyJourneyBanners();
            //Page_Login plogin = new Page_Login();
            //plogin.Login();

            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();
            //if (GlobalVariables.clientname.ToLower().ToString().Equals("MEABT"))
            //{
            //    Assert.Ignore("Journey not verified for MEABT");
            //}
            //else
            //{
            //    Page_Dashboard dashbrd = new Page_Dashboard();
            //    Assert.IsTrue(dashbrd.JourneyBannerDisplayed(), "Journey Banner missing");
            //    Page_Journey banner = new Page_Journey(softassertions);
            //    List<string[]> journeydata = CSVReaderDataTable.GetCSVData("JourneyContent", pageName, "newmember_journey");
            //    banner.VerifyJourneyBanner(journeydata);
            //    is_soft_assert = true;
            //    softassertions.AssertAll();
            //}
        }

        /// <summary>
        /// Test Case: Verified Journey Recommendations in dashboard
        /// </summary>
        [Test]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("JourClientReg")]
        [Order(5)]
         public void TC_VerifyRecommendations()
        {
            if (journeyEnabled.Equals("false"))
            {
                Assert.Ignore("Journeys not available for the client");
            }
            Journey nmj = new Journey();
            nmj.TC_VerifyNewMemberJourneyTiles();
            //Page_Login plogin = new Page_Login();
            //plogin.Login();

            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();
            //if (GlobalVariables.clientname.ToLower().ToString().Equals("MEABT"))
            //{
            //    Assert.Ignore("Journey not verified for MEABT");
            //}
            //else
            //{
            //    Page_Dashboard dashbrd = new Page_Dashboard();
            //    Assert.IsTrue(dashbrd.JourneySectionDisplayed(), "Recommendation section missing");
            //    Page_Journey journey = new Page_Journey(softassertions);
            //    List<string[]> journeydata = CSVReaderDataTable.GetCSVData("JourneyContent", pageName, "newmember_journey");
            //    journey.VerifyNewMemberJourney(journeydata);
            //    is_soft_assert = true;
            //    softassertions.AssertAll();
            //}
        }

        /// <summary>
        /// Test Case: Verified Engagementstream in dashboard
        /// </summary>
        [Test]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("JourClientReg")]
        [Order(6)]
        public void TC_VerifyEstream()
        {
            if (journeyEnabled.Equals("false"))
            {
                Assert.Ignore("Journeys not available for the client");
            }
            //Page_Login plogin = new Page_Login();
            //plogin.Login();

            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();

            Page_Dashboard dashbrd = new Page_Dashboard();
            Assert.IsTrue(dashbrd.EstreamDisplayed(), "Explore section missing");
        }

        /// <summary>
        /// Test case is use to verify Order of Left menu items
        /// </summary>
        [Test]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        [Order(7)]
        public void TC_VerifyLeftMenuOptions()
        {
            //Page_Login plogin = new Page_Login();
            //plogin.Login();

            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();

            if (GlobalVariables.isregistered.ToLower().Equals("false"))
            {
                Page_DevicesAndApps pdevice = new Page_DevicesAndApps();
                pdevice.AcceptTOS();
            }
            Common cmn = new Common();
            cmn.OpenHamMenu();

            Page_Dashboard dashbrd = new Page_Dashboard(softassertions);
            //menuitems = CSVReaderDataTable.GetCSVData("LeftmenuContent", pageName, "menuitems", GlobalVariables.clientname);
            // if ((GlobalVariables.clientname.ToLower().Equals("health trust")) || (GlobalVariables.clientname.ToLower().Equals("meabt")))
            //if ((GlobalVariables.clientname.ToLower().Equals("health trust")))
            //     {
            //    dashbrd.Verify_LeftMenuOptionsOrder(menuitems);
            //    }
            //else
            //{
            
                menuitems = CSVReaderDataTable.GetCSVData("LeftmenuContent",pageName, "menuitems","Common");
                //Console.WriteLine("in else section");
                dashbrd.Verify_LeftMenuCommonOptions(menuitems);
                menuitems = CSVReaderDataTable.GetCSVData("LeftmenuContent", pageName, "menuitems", GlobalVariables.clientname.ToLower());
                dashbrd.Verify_LeftMenuCommonOptions(menuitems);

            //}
            cmn.CloseHamMenu();

            is_soft_assert = true;
            softassertions.AssertAll();
            
        }

        /// <summary>
        /// Test case is use to verify Course Sub Menu
        /// </summary>
        [Test]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        [Order(8)]
        public void TC_VerifyCourseSubMenuOptions()
        {
            //Page_Login plogin = new Page_Login();
            //plogin.Login();

            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();

            Common cmn = new Common();
            cmn.ClickCourseMenu();

            int optionno = 4;
            Page_Dashboard dashbrd = new Page_Dashboard(softassertions);
            List<String[]> coursesubmenu = CSVReaderDataTable.GetCSVData("LeftSubmenuContent", pageName, "coursessubmenu", "allclients");
            dashbrd.Verify_LeftSubMenuOptions(coursesubmenu, optionno);

            List<String[]> specific_coursesubmenu = CSVReaderDataTable.GetCSVData("LeftSubmenuContent", pageName, "specific_coursessubmenu");
            dashbrd.Verify_LeftSubMenuOptions(specific_coursesubmenu, optionno);
            
            
            cmn.CloseHamMenu();
            is_soft_assert = true;
            softassertions.AssertAll();
        }

        /// <summary>
        /// Test case is use to verify Tracker Sub Menu
        /// </summary>
        [Test]
        [Order(9)]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyTrackerSubMenuOptions()
        {
            //Page_Login plogin = new Page_Login();
            //plogin.Login();

            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();
            
            cmn.ClickTrackerMenu();

            int optionno = 27;
            Page_Dashboard dashbrd = new Page_Dashboard(softassertions);
            List<String[]> trackersubmenu = CSVReaderDataTable.GetCSVData("LeftSubmenuContent", pageName, "trackersubmenu", "allclients");
            dashbrd.Verify_LeftSubMenuOptions(trackersubmenu, optionno);

            List<String[]> specific_trackersubmenu = CSVReaderDataTable.GetCSVData("LeftSubmenuContent", pageName, "specific_trackersubmenu");
            dashbrd.Verify_LeftSubMenuOptions(specific_trackersubmenu, optionno);
            cmn.CloseHamMenu();

            is_soft_assert = true;
            softassertions.AssertAll();

        }


        /// <summary>
        /// Test case is use to verify Tracker Sub Menu
        /// </summary>
        [Test]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("CoachingReg")]
        [Order(10)]
        public void TC_VerifyCoachingSubMenuOptions()
        {
            //Page_Login plogin = new Page_Login();
            //plogin.Login();

            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();
            coachingEnabled = cmn.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (coachingEnabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }            
            cmn.ClickGoalMenu();

            int optionno = 16;
            Page_Dashboard dashbrd = new Page_Dashboard(softassertions);
            List<String[]> popupmsg = CSVReaderDataTable.GetCSVData("LeftSubmenuContent", pageName, "coachingsubmenu", "allclients");
            dashbrd.Verify_LeftSubMenuOptions(popupmsg, optionno);

            cmn.CloseHamMenu();

            is_soft_assert = true;
            softassertions.AssertAll();
            
        }

        /// <summary>
        /// Test case is use to verify Tracker Sub Menu
        /// </summary>
        [Test]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        [Order(11)]
        public void TC_VerifyProgressCheckinSubMenuOptions()
        {
            //Page_Login plogin = new Page_Login();
            //plogin.Login();

            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();
            
            cmn.ClickProgressCheckinMenu();

            int optionno = 39;
            Page_Dashboard dashbrd = new Page_Dashboard(softassertions);
            List<String[]> popupmsg = CSVReaderDataTable.GetCSVData("LeftSubmenuContent", pageName, "progresschechinsubmenu", "allclients");

            dashbrd.Verify_LeftSubMenuOptions(popupmsg, optionno);

            cmn.CloseHamMenu();

            is_soft_assert = true;
            softassertions.AssertAll();
        }

        /// <summary>
        /// Test case is use to verify Tracker Sub Menu
        /// </summary>
        [Test]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        [Order(12)]
        public void TC_VerifyDevicesAndAppsSubMenuOptions()
        {
            List<string[]> isEnabled = cmn.GetConfig("FitnessDevice", "NutritionApp");
            fitnessEnabled = isEnabled.ElementAt(0)[1].ToLower();
            nutritionEnabled = isEnabled.ElementAt(0)[2].ToLower();
            if (fitnessEnabled.Equals("false") && nutritionEnabled.Equals("false"))
            {
                Assert.Ignore("Device and Apps not available for Client");
            }
            //Page_Login plogin = new Page_Login();
            //plogin.Login();

            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();
            if (GlobalVariables.clientname.ToLower().Equals("dollar general"))
            {
                Assert.Ignore("Feature not availabe for the client");
            }
            else
            {
                cmn.ClickDevicesAndApps();

                int optionno = 47;
                Page_Dashboard dashbrd = new Page_Dashboard(softassertions);
                List<String[]> devicesubmenu = CSVReaderDataTable.GetCSVData("LeftSubmenuContent", pageName, "devicesandappssubmenu", "allclients") ;
                dashbrd.Verify_LeftSubMenuOptions(devicesubmenu, optionno);

                List<String[]> specific_devicesandappssubmenu = CSVReaderDataTable.GetCSVData("LeftSubmenuContent", pageName, "specific_devicesandappssubmenu");
                dashbrd.Verify_LeftSubMenuOptions(specific_devicesandappssubmenu, optionno);

                //if (GlobalVariables.clientname.ToLower().Equals("health trust"))
                //{
                //    devicesubmenu = CSVReaderDataTable.GetCSVData("LeftmenuContent", pageName, "orderdevice_devicesandapppssubmenu", "Common"); ;
                //    dashbrd.Verify_LeftSubMenuOptions(devicesubmenu, optionno);
                //}

                //if (!(GlobalVariables.clientname.ToLower().Equals("nucor")))
                //{
                //    devicesubmenu = CSVReaderDataTable.GetCSVData("LeftmenuContent", pageName, "myfitness_devicesandapppssubmenu", "Common"); ;
                //    dashbrd.Verify_LeftSubMenuOptions(devicesubmenu, optionno);
                //}

                //if (!(GlobalVariables.clientname.ToLower().Equals("comprehensivecoaching")) || !(GlobalVariables.clientname.ToLower().Equals("opt out satc")) || !(GlobalVariables.clientname.ToLower().Equals("physical activity")) || !(GlobalVariables.clientname.ToLower().Equals("self directed")) || !(GlobalVariables.clientname.ToLower().Equals("weight management")) || !(GlobalVariables.clientname.ToLower().Equals("first fleet")))
                //{
                //    devicesubmenu = CSVReaderDataTable.GetCSVData("LeftmenuContent", pageName, "mynutrition_devicesandapppssubmenu", "Common"); ;
                //    dashbrd.Verify_LeftSubMenuOptions(devicesubmenu, optionno);
                //}




                cmn.CloseHamMenu();

                is_soft_assert = true;
                softassertions.AssertAll();
            }
        }


        /// <summary>
        /// Test case is use to verify Tracker Sub Menu
        /// </summary>
        [Test]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        [Order(13)]
        public void TC_VerifyResourcesSubMenuOptions()
        {
            //Page_Login plogin = new Page_Login();
            //plogin.Login();

            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();

            Common cmn = new Common();
            cmn.ClickOnResources();

            int optionno = 54;
            Page_Dashboard dashbrd = new Page_Dashboard(softassertions);

            List<String[]> popupmsg = CSVReaderDataTable.GetCSVData("LeftSubmenuContent", pageName, "resourcessubmenu", "allclients");
            TestContext.WriteLine("Data: " + popupmsg.Count);
            dashbrd.Verify_LeftSubMenuOptions(popupmsg, optionno);

            List<String[]> specific_resourcessubmenu = CSVReaderDataTable.GetCSVData("LeftSubmenuContent", pageName, "specific_resourcessubmenu");
            dashbrd.Verify_LeftSubMenuOptions(specific_resourcessubmenu, optionno);

            cmn.CloseHamMenu();

            is_soft_assert = true;
            softassertions.AssertAll();
        }

        

        /// <summary>
        /// Test Case: Verifies grey tiles on the dashboard
        /// </summary>
        [Test]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]

        [Order(14)]
        public void TC_VerifyContactUs()
        {
            //Page_Login plogin = new Page_Login();
            //plogin.Login();

            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();

            Common contactus = new Common(softassertions);
            Assert.IsTrue(contactus.ContactUsDisplayed(), "Contact Use details not present");
            List<string[]> ExpectedText = CSVReaderDataTable.GetCSVData("DashboardData", pageName, "contactus", GlobalVariables.clientname);
            
            contactus.ContactUsElements(ExpectedText);
            is_soft_assert = true;
            softassertions.AssertAll();
        }

        /// <summary>
        /// Test Case: VerifiesFooter link existence on the dashboard
        /// </summary>
        [Test]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]

        [Order(15)]
        public void TC_VerifyfooterLinksPresent()
        {
            //Page_Login plogin = new Page_Login();
            //plogin.Login();

            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();

            Page_Dashboard footer = new Page_Dashboard(softassertions);
            List<string[]> FooterElements = CSVReaderDataTable.GetCSVData("DashboardData", pageName, "commonfooterlink","Common");
            footer.CommonFooterLinksDisplayed(FooterElements);
            //footer.
            List<string[]> clientFooterElements = CSVReaderDataTable.GetCSVData("DashboardData", pageName, "footerlink", GlobalVariables.clientname);
            footer.CommonFooterLinksDisplayed(FooterElements);
            footer.ClientFooterLinksDisplayed(clientFooterElements);
            is_soft_assert = true;
            softassertions.AssertAll();
            //Common logout = new Common();
            //logout.LogOut();
        }
        /// <summary>
        /// Verify the coanching section for non journey clients
        /// </summary>
        [Test]
        //[Category("ProdSanity")]
        //[Category("Regression")]
        [Category("NonJorRegression")]

        [Order(16)]
        public void TC_VerifyCoachingSection()
        {
            if (journeyEnabled.Equals("true"))
            {
                Assert.Ignore("Journeys available for the client");
            }
            else if(coachingEnabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            //Page_Login plogin = new Page_Login();
            //plogin.Login();

            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();
            Page_Dashboard pdb = new Page_Dashboard(softassertions);
            pdb.Verify_CoachingSection();
            is_soft_assert = true;
            softassertions.AssertAll();
        }

        /// <summary>
        /// Verify the courses section for non journey clients
        /// </summary>
        [Test]
        //[Category("ProdSanity")]
        //[Category("Regression")]
        [Category("NonJorRegression")]

        [Order(17)]
        public void TC_VerifyCoursesSection()
        {
            if (journeyEnabled.Equals("true"))
            {
                Assert.Ignore("Journeys available for the client");
            }
            //Page_Login plogin = new Page_Login();
            //plogin.Login();

            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();
            Page_Dashboard pdb = new Page_Dashboard(softassertions);
            pdb.Verify_CoursesSection();
            is_soft_assert = true;
            softassertions.AssertAll();
        }

        /// <summary>
        /// Verify the trackers section for non journey clients
        /// </summary>
        [Test]
        //[Category("ProdSanity")]
        //[Category("Regression")]
        [Category("NonJorRegression")]
        [Order(18)]
        public void TC_VerifyTrackersSection()
        {
            if (journeyEnabled.Equals("true"))
            {
                Assert.Ignore("Journeys available for the client");
            }
            //Page_Login plogin = new Page_Login();
            //plogin.Login();

            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();
            Page_Dashboard pdb = new Page_Dashboard(softassertions);
            pdb.Verify_TrackersSection();
            is_soft_assert = true;
            softassertions.AssertAll();
        }

        /// <summary>
        /// Verify the update section for non journey clients
        /// </summary>
        [Test]
        //[Category("ProdSanity")]
        //[Category("Regression")]
        [Category("NonJorRegression")]
        [Order(19)]
        public void TC_VerifyUpdateProgressSection()
        {
            if (journeyEnabled.Equals("true"))
            {
                Assert.Ignore("Journeys available for the client");
            }
            //Page_Login plogin = new Page_Login();
            //plogin.Login();

            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();
            Page_Dashboard pdb = new Page_Dashboard(softassertions);
            pdb.Verify_UpdateProgressSection();
            is_soft_assert = true;
            softassertions.AssertAll();
            
        }

       
        /*       
         *       /// <summary>
                /// Testcase is use to verify HA tile in Journey
                /// </summary>
                public void TC_VerifyHAJourneyTileVisibility()
                {
                    Page_Login plogin = new Page_Login();
                    plogin.Login();
                    Page_Dashboard dashbrd = new Page_Dashboard();
                    Assert.IsTrue(dashbrd.HAJourneyTile());
                }

                /// <summary>
                /// Test case is use to verify coach icon using sikuli
                /// </summary>
                public void TC_VerifyCoachIcon()
                {
                    Page_Login plogin = new Page_Login();
                    plogin.Login();

                    Page_Dashboard dashbrd = new Page_Dashboard(softassertions);
                    dashbrd.Verify_DashboardHeaderIcons();

                    is_soft_assert = true;
                    softassertions.AssertAll();
                } 
         */
    }

}
