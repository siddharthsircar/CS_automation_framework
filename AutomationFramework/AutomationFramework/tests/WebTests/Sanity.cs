using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages.Coaching;
using AutomationFramework.Pages.WebPages.DevicesAndApps;
using AutomationFramework.Pages.WebPages.Journey;
using AutomationFramework.Pages.WebPages.ProgressCheckin;
using AutomationFramework.Pages.WebPages.Trackers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationFramework.Tests.WebTests.Journies;
using AutomationFramework.Pages.WebPages.FinancialWellBeing;
using AutomationFramework.Pages.WebPages;
using AutomationFramework.Pages.WebPages.Notifications;
using AutomationFramework.Tests.WebPages.dashboard;
using AutomationFramework.Pages.WebPages.Courses;
using AutomationFramework.Tests.WebTests.Courses;
using AutomationFramework.Tests.WebTests.HealthAssessment;
using AutomationFramework.Tests.WebTests.Devices;
using AutomationFramework.Tests.WebTests.Notifications;
using AutomationFramework.Tests.WebTests.zIncentive;
using AutomationFramework.Tests.WebTests.Trackers;
using AutomationFramework.Pages.WebPages.Challenges;
using AutomationFramework.Keywords;

namespace AutomationFramework.Tests.WebTests
{
    /// <summary>
    /// Sanity Test Suite
    /// </summary>
    [TestFixture]
    public class Sanity : Base
    {
        string clientname ;
        string journeyEnabled;
        string coachingEnabled;
        string deviceEnabled;
        string nutritionEnabled;
        public static int points;
        string incentiveEnabled;

        Common cmn = new Common();
        List<String[]> menuitems = new List<String[]>();

       
        [Test, Order(1)]
        [Category("ProdSanityAllInOne")]
        public void TC_VerifyAtDashboard()
        {
            clientname = GlobalVariables.clientname.ToLower();
            Console.WriteLine("ClientName : " + clientname);
            //To call the Page Login Method
            Page_Login plogin = new Page_Login();
            plogin.Login();

            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();

            Page_Dashboard Dashboard = new Page_Dashboard();
            Assert.IsTrue(Dashboard.AtDashboard());
        }
        [Test, Order(2)]
        [Category("ProdSanityAllInOne")]
        public void TC_VerifyJourneyBanner()
        {

            Journey nmjb = new Journey();
            nmjb.TC_VerifyJourneyBanners();
        }
        [Test, Order(3)]
        [Category("ProdSanityAllInOne")]
        public void TC_VerifyJourneyTiles()
        {
            Journey nmj = new Journey();
            nmj.TC_VerifyNewMemberJourneyTiles();
        }
        [Test, Order(4)]
        [Category("ProdSanityAllInOne")]
        public void TC_VerifyEstream()
        {
            journeyEnabled = cmn.GetConfig("JourneyEnabled").ElementAt(0)[1].ToLower();
            if (journeyEnabled.Equals("false"))
            {
                Assert.Ignore("Journeys not available for the client");
            }
            Page_Dashboard dashbrd = new Page_Dashboard();
            Assert.IsTrue(dashbrd.EstreamDisplayed(), "Recommendation section missing");
        }
        [Test, Order(5)]
        [Category("ProdSanityAllInOne")]
        public void TC_VerifyEligibleActivity()
        {
            EligibleActivities ea = new EligibleActivities();
            ea.TC_ValidateIncentiveEligibleActivities();
        }
        [Test, Order(6)]
        [Category("ProdSanityAllInOne")]
        public void TC_VerifyAnnouncements()
        {
            Announcements nma = new Announcements();
            nma.TC_VerifyAnnouncements();
        }

        [Test, Order(7)]
        [Category("ProdSanityAllInOne")]
        public void TC_FillHighRiskHA()
        {

            Page_FillHA ha = new Page_FillHA(softassertions);
            Common cm = new Common();
            int hraid = cm.GetHRAID(clientname);
            Console.WriteLine("HRAID : " + hraid);
            if (hraid == 89 || hraid == 66 || hraid == 81)
                ha.setInputFileName("HRAID(89,66)HighRiskHAData");
            //else if (hraid == 84)
            //    ha.setInputFileName("HRAID(Nucor)HighRiskHAData");
            else if (hraid == 87)
                ha.setInputFileName("HRAID(DG)HighRiskHAData");


            is_soft_assert = true;
            ha.FillHA();
            softassertions.AssertAll();
            cmn.RefreshWebPage();
        }
        [Test, Order(8)]
        [Category("ProdSanityAllInOne")]
        public void TC_JourneyRecommendation()
        {
            Journey nmj = new Journey();
            nmj.TC_VerifyClinicalJourney();
        }
        [Test, Order(9)]
        [Category("ProdSanityAllInOne")]
        public void TC_TrackerRecommendation()
        {
            Page_TrackerLearnMore ptlm = new Page_TrackerLearnMore(Base.softassertions);
            cmn.ClickTrackerMenu();
            cmn.ClickOnLearnMore();
            List<String[]> devicename = CSVReaderDataTable.GetCSVData("CommonContent", "Page_TrackerLearnMore", "recomandationtile");
            // pdevices.ClickOnFitnessCheckBox();
            is_soft_assert = true;
            for (int i = 0; i < devicename.Count; i++)
            {
                ptlm.VerifyRecommandedTrackers(devicename.ElementAt(i)[4]);
            }
            softassertions.AssertAll();
        }
        [Test, Order(10)]
        [Category("ProdSanityAllInOne")]
        public void TC_CoachingRecommendation()
        {
            FillHighRiskHA coachrecomm = new FillHighRiskHA();
            coachrecomm.VerifyRecommendedCoaching();
        }
        [Test, Order(11)]
        [Category("ProdSanityAllInOne")]
        public void TC_VerifyLeftMenu()
        {
            Common cmn = new Common();
            cmn.OpenHamMenu();

            Page_Dashboard dashbrd = new Page_Dashboard(softassertions);
            menuitems = CSVReaderDataTable.GetCSVData("LeftmenuContent", "Dashboard", "menuitems", clientname);
            dashbrd.Verify_LeftMenuCommonOptions(menuitems);
            cmn.CloseHamMenu();
            is_soft_assert = true;
            softassertions.AssertAll();
        }
        [Test, Order(12)]
        [Category("ProdSanityAllInOne")]
        public void TC_VerifyTrackersSubMenu()
        {
            SeleniumKeywords.RefreshPage();
            System.Threading.Thread.Sleep(3000);

            cmn.ClickTrackerMenu();

            int optionno = 27;
            Page_Dashboard dashbrd = new Page_Dashboard(softassertions);
            List<String[]> popupmsg = CSVReaderDataTable.GetCSVData("LeftmenuContent", "Dashboard", "trackersubmenu", "Common");
            dashbrd.Verify_LeftSubMenuOptions(popupmsg, optionno);
            is_soft_assert = true;
            softassertions.AssertAll();
        }
        [Test, Order(13)]
        [Category("ProdSanityAllInOne")]
        public void TC_WeightTracker()
        {
            incentiveEnabled = cmn.GetConfig("IncentiveEnabled").ElementAt(0)[1].ToLower();

            if (incentiveEnabled.Equals("true"))
            {
                points = cmn.GetPoints(clientname);
            }

            Page_WeightTracker pWeightTracker = new Page_WeightTracker(softassertions);
            pWeightTracker.GoToWeightTracker();
            pWeightTracker.VerifyWeightTracker(clientname);
            CommonTracker ct = new CommonTracker(softassertions);
            ct.VerifyTrackerChart();
            

            if (!incentiveEnabled.Equals("false"))
            {
                //Assert.Ignore("Incentives not enabled for client");
                

                WeightTracker wt = new WeightTracker();
                wt.TC_ValidateTrackerIncentiveHistory();

                int awardedpoints = cmn.GetPoints(clientname);

                int points_tracker = Convert.ToInt32(cmn.GetInstancePointsValue(clientname, "Tracker"));
                int expectedtotalpoints = points + points_tracker;

                Console.WriteLine("Expected : " + expectedtotalpoints);
                Console.WriteLine("Awarded : " + awardedpoints);
                Assert.AreEqual(expectedtotalpoints, awardedpoints);
            }

            is_soft_assert = true;
            softassertions.AssertAll();


        }

        [Test, Order(14)]
        [Category("ProdSanityAllInOne")]
        public void TC_VerifyGoalSubMenu()
        {
            SeleniumKeywords.RefreshPage();
            System.Threading.Thread.Sleep(3000);

            coachingEnabled = cmn.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (coachingEnabled.Equals("false"))
            {
                Assert.Ignore("Coaching not available for client");
            }
            cmn.ClickGoalMenu();
            int optionno = 16;
            Page_Dashboard dashbrd = new Page_Dashboard(softassertions);
            List<String[]> popupmsg = CSVReaderDataTable.GetCSVData("LeftmenuContent", "Dashboard", "coachingsubmenu", "Common");
            dashbrd.Verify_LeftSubMenuOptions(popupmsg, optionno);
            is_soft_assert = true;
            softassertions.AssertAll();
        }

        [Test, Order(15)]
        [Category("ProdSanityAllInOne")]
        public void TC_MaintainWeightGoal()
        {
            coachingEnabled = cmn.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (coachingEnabled.Equals("false"))
            {
                Assert.Ignore("Coaching not available for client");
            }
            CommonGoals cmngoal = new CommonGoals(softassertions);
            Page_MaintainWeightGoal mt = new Page_MaintainWeightGoal(softassertions);
            //cmn.ClickGoalMenu();
            mt.InputWeight();
            cmngoal.ClickStep1NextBtn();
            is_soft_assert = true;
            mt.VerifyPlanScreen();
            mt.VerifyActionsAdded();
            System.Threading.Thread.Sleep(3000);
            cmngoal.ClickStep2NextBtn();
            mt.VerifySetUpScreen();
            cmngoal.ClickConfirmBtn();
            mt.VerifyGoalComplete();
            cmngoal.ClickRemoveBtn();
            cmngoal.VerifyRemovePopUp();
            cmngoal.ClickRemoveScreenYesBtn();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            Page_Dashboard dashbrd = new Page_Dashboard();
            Assert.IsTrue(dashbrd.AtDashboard(), "Not Navigated to Dashboard");
        }

        [Test, Order(16)]
        [Category("ProdSanityAllInOne")]
        public void TC_VerifyCoursesSubMenu()
        {
            Dashboard db = new Dashboard();
            db.TC_VerifyCourseSubMenuOptions();
        }

        [Test, Order(17)]
        [Category("ProdSanityAllInOne")]
        public void TC_FillWeightProgressCheckinFromCourse()
        {
            SeleniumKeywords.RefreshPage();
            System.Threading.Thread.Sleep(3000);
            if (incentiveEnabled.Equals("true"))
            {
                points = cmn.GetPoints(clientname);
            }
            Common coursemenu = new Common();
            coursemenu.ClickCourseMenu();
            Page_WeightManagementCourse pcourse = new Page_WeightManagementCourse();
            pcourse.NavigateToCourse();
            CommonCourses cmnc = new CommonCourses();
            cmnc.GoToProgressCheckin();
            Page_WeightProgressCheckIn pc = new Page_WeightProgressCheckIn();
            List<string[]> result = pc.VerifyProgressCheckIn();
            is_soft_assert = false;
            Assert.Multiple(() =>
            {
                for (int i = 0; i < result.Count; i++)
                {
                    bool textmatchresult = Convert.ToBoolean(result.ElementAt(i)[1]);
                    string msg = result.ElementAt(i)[0];
                    Assert.IsTrue(textmatchresult, msg);
                }
            }
            );
        }

        [Test, Order(18)]
        [Category("ProdSanityAllInOne")]
        public void TC_WeightManagementCourse()
        {
            WeightManagementCourse wmc = new WeightManagementCourse();
            wmc.TC_VerifyWeightManagementCourse();
        }
        [Test, Order(19)]
        [Category("ProdSanityAllInOne")]
        public void TC_VerifyCoursePoints()
        {
            WeightManagementCourse wm = new WeightManagementCourse();
            wm.TC_ValidateCourseAndProgressCheckInIncentiveHistory();
            if (incentiveEnabled.Equals("false"))
            {
                Assert.Ignore("Incentives not enabled for client");
            }
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            int awardedpoints = cmn.GetPoints(clientname);
            int points_progresscheckin = Convert.ToInt32(cmn.GetInstancePointsValue(clientname, "ProgressCheckIn"));
            int points_course = Convert.ToInt32(cmn.GetInstancePointsValue(clientname, "Course"));
            int expectedtotalpoints = points + points_progresscheckin+points_course;
            is_soft_assert = false;
            Assert.AreEqual(expectedtotalpoints, awardedpoints);
        }

        [Test, Order(20)]
        [Category("ProdSanityAllInOne")]
        public void TC_VerifyDevicesSubMenu()
        {
            SeleniumKeywords.RefreshPage();
            System.Threading.Thread.Sleep(3000);

            List<string[]> isEnabled = cmn.GetConfig("FitnessDevice", "NutritionApp");
            deviceEnabled = isEnabled.ElementAt(0)[1].ToLower();
            nutritionEnabled = isEnabled.ElementAt(0)[2].ToLower();
            if (deviceEnabled.Equals("false") && nutritionEnabled.Equals("false"))
            {
                Assert.Ignore("Device and Apps not available for client");
            }
            if (GlobalVariables.isregistered.ToLower().Equals("false"))
            {
                Page_DevicesAndApps pdevice = new Page_DevicesAndApps();
                pdevice.AcceptTOS();
            }
            if (clientname.Equals("dollar general"))
            {
                Assert.Ignore("Feature no availabe for the client");
            }
            else
            {
                cmn.ClickDevicesAndApps();

                int optionno = 47;
                Page_Dashboard dashbrd = new Page_Dashboard(softassertions);
                List<String[]> popupmsg = null;

                if (clientname.Equals("health trust"))
                    popupmsg = CSVReaderDataTable.GetCSVData("LeftmenuContent", "Dashboard", "devicesandapppssubmenu", clientname);
                else
                    popupmsg = CSVReaderDataTable.GetCSVData("LeftmenuContent", "Dashboard", "devicesandapppssubmenu", "Common");

                dashbrd.Verify_LeftSubMenuOptions(popupmsg, optionno);
            }
        }
        
        [Test, Order(21)]
        [Category("ProdSanityAllInOne")]
        public void TC_Resources()
        {
            SeleniumKeywords.RefreshPage();
            System.Threading.Thread.Sleep(3000);
            cmn.OpenHamMenu();
            cmn.ClickOnResourcesMenu();
            Page_HealthContent phc = new Page_HealthContent();
            List<string[]> actualtxt = CSVReaderDataTable.GetCSVData("ResourcesContent", "HealthContent", "pageheading");
            string atrval = phc.VerifyHealthContentPage();
            Console.WriteLine("actualtxt" + atrval + "expected" + actualtxt.ElementAt(0)[4]);
            Assert.AreEqual(actualtxt.ElementAt(0)[4], atrval);

            // Symptom Checker Tab
            Page_SymptomChecker psc = new Page_SymptomChecker();
            atrval = psc.VerifySymptomCheckerPageFromTab();
            actualtxt = CSVReaderDataTable.GetCSVData("ResourcesContent", "SymptomChecker", "pageheading");
            Console.WriteLine("actualtxt" + atrval + "expected" + actualtxt.ElementAt(0)[4]);
            Assert.AreEqual(actualtxt.ElementAt(0)[4], atrval);

            // Well Being Content Tab
            Page_WellBeingContent wbc = new Page_WellBeingContent();
            atrval = wbc.VerifyWellBeingFromTab();
            actualtxt = CSVReaderDataTable.GetCSVData("ResourcesContent", "Common", "pageheading");
            Console.WriteLine("actualtxt" + atrval + "expected" + actualtxt.ElementAt(0)[4]);
            Assert.AreEqual(actualtxt.ElementAt(0)[4], atrval);

            //Family Content
            Page_FamilyContent fc = new Page_FamilyContent();
            atrval = fc.VerifyFamilyContentFromTab();
            actualtxt = CSVReaderDataTable.GetCSVData("ResourcesContent", "Common", "pageheading");
            TestContext.WriteLine("expected" + actualtxt.ElementAt(0)[4]);
            TestContext.WriteLine("actualtxt" + atrval + "expected" + actualtxt.ElementAt(0)[4]);
            Assert.AreEqual(actualtxt.ElementAt(0)[4], atrval);

            Resources rs = new Resources();
            rs.TC_VerifySymptomCheckerPage();
            rs.TC_VerifyWellBeingContentPage();
            rs.TC_VerifyReportsPage();
            rs.TC_VerifyAssessmentsPage();
            rs.TC_VerifyPHRPage();
        }

        [Test, Order(22)]
        [Category("ProdSanityAllInOne")]
        public void TC_VerifyFooter()
        {
            SeleniumKeywords.RefreshPage();
            System.Threading.Thread.Sleep(3000);

            //Common dblink = new Common();
            cmn.ClickFooterDashboardLink();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            Page_Dashboard dashbrd = new Page_Dashboard();
            Assert.IsTrue(dashbrd.JourneyBannerDisplayed(), "Journey Banner missing");
            Footer ftr = new Footer();
            ftr.TC_VerifyTrackerLink();
            ftr.TC_VerifyMyHealthLink();
            if ((!clientname.Equals("arc")) || (!clientname.Equals("self directed")))
            {
                //Assert.Ignore("Certificate is not available for " + clientname);
                Common hipaa = new Common();
                hipaa.ClickFooterHipaaLink();
                Page_HIPAA rights = new Page_HIPAA();
                List<string[]> result = rights.VerifyHeader();
                Assert.Multiple(() =>
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        Assert.IsTrue(Convert.ToBoolean(result.ElementAt(i)[1]), result.ElementAt(i)[0]);
                    }
                }
                );
                Assert.IsTrue(rights.VerifyContentSectionPresent(), "HIPAA Rights content not found");
            }
            ftr.TC_VerifyPrivacyPolLink();
            ftr.TC_VerifyTOSLink();
            ftr.TC_VerifyCertificateLnk();
            ftr.TC_VerifyFeedbackLink();
        }

        [Test, Order(23)]
        [Category("ProdSanityAllInOne")]
        public void TC_Settings()
        {
            SeleniumKeywords.RefreshPage();
            System.Threading.Thread.Sleep(3000);

            Common settings = new Common();
            settings.GoToSettings();
            Page_Settings myinf = new Page_Settings(softassertions);
            myinf.VerifyMyProfileErrors();
            is_soft_assert = true;
            softassertions.AssertAll();
            Setting set = new Setting();
            set.TC_VerifyMyProfileData();
            set.TC_VerifyErrorMessages();
            set.TC_VerifyUpdatedData();
        }

        [Test, Order(24)]
        [Category("ProdSanityAllInOne")]
        public void TC_MessageToCoach()
        {
            SeleniumKeywords.RefreshPage();
            System.Threading.Thread.Sleep(3000);

            coachingEnabled = cmn.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            incentiveEnabled = cmn.GetConfig("IncentiveEnabled").ElementAt(0)[1].ToLower();
            if (coachingEnabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for clients");
            }
            if (incentiveEnabled.Equals("true"))
            {
                points = cmn.GetPoints(clientname);
            }
            //Click on Message to coach icon on Dashboard
            Common pcommon = new Common();
            pcommon.GoToMessageToCoachPage();
            //Call Verify Message Page UI Elements
            Page_MessageToCoach pmsgcoach = new Page_MessageToCoach(softassertions);
            pmsgcoach.VerifyMessagetoCoachPage();
            is_soft_assert = true;
            softassertions.AssertAll();
            MessageToCoach mtc = new MessageToCoach();
            mtc.TC_VerifySentMsgToCoach();
            if (incentiveEnabled.Equals("true"))
            {
                mtc.TC_ValidatePoints();
            }
            mtc.TC_ValidateMessageToCoachIncentiveHistory();
            mtc.TC_VerifyMessageFilters();

        }

        [Test, Order(25)]
        [Category("ProdSanityAllInOne")]
        public void TC_Notifications()
        {
            SeleniumKeywords.RefreshPage();
            System.Threading.Thread.Sleep(3000);

            Page_Notifications pn = new Page_Notifications();
            pn.ClickViewAllLink();
            List<string[]> result = pn.VerifyAllNotifications();
            Assert.Multiple(() =>
            {
                for (int i = 0; i < result.Count; i++)
                {
                    bool textmatch = Convert.ToBoolean(result.ElementAt(i)[1]);
                    string msg = result.ElementAt(i)[0];
                    Assert.IsTrue(textmatch, msg);

                }
            }
           );
        }

        [Test, Order(26)]
        [Category("ProdSanityAllInOne")]
        public void TC_VerifyProgressCheckInSubMenu()
        {
            SeleniumKeywords.RefreshPage();
            System.Threading.Thread.Sleep(3000);

            Common cmn = new Common();
            cmn.ClickProgressCheckinMenu();
            int optionno = 39;
            Page_Dashboard dashbrd = new Page_Dashboard(softassertions);
            List<String[]> popupmsg = CSVReaderDataTable.GetCSVData("LeftmenuContent", "Dashboard", "progresschechinsubmenu", "Common");
            dashbrd.Verify_LeftSubMenuOptions(popupmsg, optionno);
            is_soft_assert = true;
            softassertions.AssertAll();
        }
       

        [Test, Order(27)]
        [Category("ProdSanityAllInOne")]
        public void TC_FillManageStressProgressCheckIn()
        {
            Page_ManagingStressProgressCheckIn mspc = new Page_ManagingStressProgressCheckIn(softassertions);
            is_soft_assert = true;
            mspc.CompleteProgressCheckIn();
            softassertions.AssertAll();
        }
       

        [Test, Order(28)]
        public void TC_VerifyProgressCheckinReport()
        {
            Page_ManagingStressProgressCheckIn pprogress = new Page_ManagingStressProgressCheckIn(softassertions);
            is_soft_assert = true;
            pprogress.VerifyProgressCheckinReportBottomLinks(clientname);
            softassertions.AssertAll();
        }
       
        [Test, Order(29)]
        [Category("ProdSanityAllInOne")]
        public void TC_VerifyFinancialWellBeingContent()
        {
            SeleniumKeywords.RefreshPage();
            System.Threading.Thread.Sleep(3000);

            if ((clientname.Equals("group 44")))
            {
                Assert.Ignore("Feature is not available for client");
            }

            Common cmn = new Common();
            cmn.ClickFinancialWellBeingMenu();
            Page_FinancialWellBeing fwb = new Page_FinancialWellBeing(softassertions);
            fwb.VerifyFinancialWellBeingData(clientname);
            is_soft_assert = true;
            softassertions.AssertAll();
        }

        [Test,Order(30)]
        [Category("ProdSanityAllInOne")]
        public void TC_Devices()
        {
            SeleniumKeywords.RefreshPage();
            System.Threading.Thread.Sleep(3000);

            string incentiveEnabled;
            string fitnessEnabled;
            string nutritionEnabled;
            List<string[]> isEnabled = cmn.GetConfig("FitnessDevice", "IncentiveEnabled", "NutritionApp");
            fitnessEnabled = isEnabled.ElementAt(0)[1].ToLower();
            incentiveEnabled = isEnabled.ElementAt(0)[2].ToLower();
            nutritionEnabled = isEnabled.ElementAt(0)[3].ToLower();
            
            if (fitnessEnabled.Equals("false") && nutritionEnabled.Equals("false"))
            {
                Assert.Ignore("Device and Apps not available for Client");
            }
            if (incentiveEnabled.Equals("true"))
            {
                points = cmn.GetPoints(clientname);
            }
            Page_DevicesAndApps pdevices = new Page_DevicesAndApps();
            //pdevices.AcceptTOS();
            Common devices = new Common();
            //devices.ClickDevicesAndApps();
            string expected = "Start Tracking Your Activity Today";

            if (nutritionEnabled.Equals("true") && fitnessEnabled.Equals("false"))
            {
                expected = "Start Tracking Your Nutrition Today";
            }
            devices.ClickAddRemoveDevicesLabel();
            string actual = pdevices.NavigateToManageDevices();
            Assert.AreEqual(expected, actual);

            DevicesAndApps da = new DevicesAndApps();
            da.TC_VerifySearchFilter();
            da.TC_VerifyDeviceCountBeforeConnect();
            da.TC_VerifyListOfNutritionDevicesAndApps();
            da.TC_VerifyListOfWeightDevicesAndApps();
            da.TC_VerifyListOfFitnessDevicesAndApps();
            da.TC_VerifyLoginPageOfDevices();
            da.TC_VerifyConnectFitbitDevice();
            da.TC_VerifyDeviceCountAfterDeviceConnected();
            da.TC_VerifyRefreshConnection();
            da.TC_VerifyDisConnectDevice();
            //da.TC_VerifyDeviceFAQs();
            da.TC_ValidateDeviceAppsIncentiveHistory();
            da.TC_ValidatePoints();
        }

        [Test, Order(31)]
        [Category("ProdSanityAllInOne")]
        public void TC_VerifyChallenge()
        {
            string challengeEnabled = cmn.GetConfig("ChallengesEnabled").ElementAt(0)[1].ToLower();
            string incentiveEnabled = cmn.GetConfig("IncentiveEnabled").ElementAt(0)[1].ToLower();
            if (challengeEnabled.Equals("false"))
            {
                Assert.Ignore("Challenges not available for the client");
            }
            if (incentiveEnabled.Equals("true"))
            {
                points = cmn.GetPoints(clientname);
            }
            Common com = new Common();
            com.ClickChallengesMenu();
            Page_Challenges pchal = new Page_Challenges();
            Assert.IsTrue(pchal.VerifyChallengesPage());
            Challenges ch = new Challenges();
            ch.TC_VerifyChallengeJoined();
            ch.TC_VerifyTrackChallenge();
            ch.TC_VerifyLeaveChallenge();
            ch.TC_ValidatJoinChallengeIncentiveHistory();
            ch.TC_ValidatePoints();
            

        }
    }
}
