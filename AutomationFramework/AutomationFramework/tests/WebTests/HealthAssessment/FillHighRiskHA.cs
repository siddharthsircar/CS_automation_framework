using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages.Journey;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationFramework.Tests.WebTests.HealthAssessment
{
    /// <summary>
    /// Test Class: High Risk HA
    /// </summary>
    [TestFixture]
    [Order(11)]
    public class FillHighRiskHA : Base
    {
        string journeyEnabled;
        Common cmn = new Common();
        Page_FillHA ha = null;

        /// <summary>
        /// Fill high risk HA
        /// </summary>
        [Test, Order(1)]
        [Category("Regression")]
        [Category("ProdSanity")]
        [Category("AllClientReg")]
        public void TC_FillHighRiskHA()
        {
            Page_Login plogin = new Page_Login();
            plogin.Login();

            Page_HAPrompt haprompt = new Page_HAPrompt();
            Assert.IsTrue(haprompt.AtHaPrompt(), "Not at HA Prompt Page");
            haprompt.GoToDashboard();
            ha = new Page_FillHA(softassertions);
            Common cm = new Common();
            int hraid = cm.GetHRAID(GlobalVariables.clientname.ToLower());
            Console.WriteLine("HRAID : "+hraid);
            if(hraid == 89 || hraid == 66 || hraid == 81)
                ha.setInputFileName("HRAID(89,66)HighRiskHAData");
            //else if (hraid == 84)
            //    ha.setInputFileName("HRAID(Nucor)HighRiskHAData");
            else if (hraid == 87)
                ha.setInputFileName("HRAID(DG)HighRiskHAData");


            is_soft_assert = true;
            ha.FillHA();
            softassertions.AssertAll();
        }
        
        /// <summary>
        /// Verify Recommendations after High Risk HA completion
        /// </summary>
        [Test, Order(2)]
        [Category("Regression")]
        [Category("ProdSanity")]
        [Category("AllClientReg")]
        public void TC_ValidateJourneyWithRecommendation()
        {
            journeyEnabled = cmn.GetConfig("JourneyEnabled").ElementAt(0)[1].ToLower();
            if (journeyEnabled.Equals("false"))
            {
                Assert.Ignore("Journeys not available for the client");
            }
            //Page_Login plogin = new Page_Login();
            //plogin.Login();

            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //Assert.IsTrue(haprompt.AtHaPrompt(), "Not at HA Prompt Page");
            //haprompt.GoToDashboard();
            //Page_CoachingLearnMore ptlm = new Page_CoachingLearnMore(softassertions);
            //Common com = new Common();
            //com.ClickFooterDashboardLink();
            Page_Journey pjourney = new Page_Journey(softassertions);
            pjourney.VerifyJournerWithRecommendation();
            is_soft_assert = true;
            softassertions.AssertAll();
        }

        /// <summary>
        /// Verify Tracker are recommendaed based on HA response
        /// </summary>
        [Test, Order(3)]
        [Category("Regression")]
        [Category("ProdSanity")]
        [Category("AllClientReg")]
        public void VerifyRecommendedTracker()
        {
            //Login lgn = new Login();
            //lgn.TC_VerifyLogin();
            //Page_HAPrompt haprompt = new Page_HAPrompt();
            Page_TrackerLearnMore ptlm = new Page_TrackerLearnMore(softassertions);
            Common com = new Common();
            com.ClickTrackerMenu();
            com.ClickOnLearnMore();
            List<String[]> devicename = CSVReaderDataTable.GetCSVData("CommonContent", "Page_TrackerLearnMore", "recomandationtile");
            // pdevices.ClickOnFitnessCheckBox();
            is_soft_assert = true;
            for (int i = 0; i < devicename.Count; i++)
            {
                ptlm.VerifyRecommandedTrackers(devicename.ElementAt(i)[4]);
            }
            softassertions.AssertAll();
        }

        /// <summary>
        /// Verify trackers are not recommended for issues not marked as high risk in HA
        /// </summary>
        [Test, Order(4)]
        [Category("Regression")]
        //[Category("ProdSanity")]
        [Category("AllClientReg")]
        public void VerifyNonRecommendedTracker()
        {
            //Page_Login plogin = new Page_Login();
            //plogin.Login();
            Page_TrackerLearnMore ptlm = new Page_TrackerLearnMore(softassertions);
            Common com = new Common();
            //com.ClickTrackerMenu();
            //com.ClickOnLearnMore();
            com.ClickFooterTrackerLink();
            List<String[]> devicename = CSVReaderDataTable.GetCSVData("CommonContent", "Page_TrackerLearnMore", "notrecomandationtile");
            // pdevices.ClickOnFitnessCheckBox();
            is_soft_assert = true;
            for (int i = 0; i < devicename.Count; i++)
            {
                ptlm.VerifyNotRecommandedTrackers(devicename.ElementAt(i)[4]);
            }
            softassertions.AssertAll();
        }

        /// <summary>
        /// Verify Goals are recommendaed based on HA response
        /// </summary>
        [Test, Order(5)]
        [Category("Regression")]
        [Category("ProdSanity")]
        [Category("AllClientReg")]
        public void VerifyRecommendedCoaching()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            //Login lgn = new Login();
            //lgn.TC_VerifyLogin();
            //Page_HAPrompt haprompt = new Page_HAPrompt();
            Page_CoachingLearnMore ptlm = new Page_CoachingLearnMore(softassertions);
            Common com = new Common();
            com.ClickGoalMenu();
            com.ClickOnLearnMore();
            List<String[]> devicename = CSVReaderDataTable.GetCSVData("CoachingContent", "Page_CoachingLearnMore", "recomandationtile");
            // pdevices.ClickOnFitnessCheckBox();
            is_soft_assert = true;
            for (int i = 0; i < devicename.Count; i++)
            {
                ptlm.VerifyRecommandedCoaching(devicename.ElementAt(i)[4]);
            }
            softassertions.AssertAll();
        }

        /// <summary>
        /// Verify goals are not recommended for issues not marked as high risk in HA
        /// </summary>
        [Test, Order(6)]
        [Category("Regression")]
        //[Category("ProdSanity")]
        [Category("AllClientReg")]
        public void VerifyNotRecommendedCoaching()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            //Login lgn = new Login();
            //lgn.TC_VerifyLogin();
            //Page_HAPrompt haprompt = new Page_HAPrompt();
            Page_CoachingLearnMore ptlm = new Page_CoachingLearnMore(softassertions);
            Common com = new Common();
            com.ClickGoalMenu();
            com.ClickOnLearnMore();
            List<String[]> devicename = CSVReaderDataTable.GetCSVData("CoachingContent", "Page_CoachingLearnMore", "notrecomandationtile");
            // pdevices.ClickOnFitnessCheckBox();
            is_soft_assert = true;
            for (int i = 0; i < devicename.Count; i++)
            {
                ptlm.VerifyNotRecommandedCoaching(devicename.ElementAt(i)[4]);
            }
            softassertions.AssertAll();
        }

        /// <summary>
        /// Validate reports generated after HA completion
        /// </summary>
        [Test, Order(7)]
        [Category("Regression")]
        [Category("ProdSanity")]
        [Category("AllClientReg")]
        public void TC_VerifyHAReport()
        {

            ha.ValidateHAReport();
            softassertions.AssertAll();
        }


    }


}

