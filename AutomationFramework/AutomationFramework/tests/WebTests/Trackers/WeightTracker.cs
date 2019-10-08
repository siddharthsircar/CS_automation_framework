using System;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using AutomationFramework.Framework;
using AutomationFramework.Pages;
using System.Collections.Generic;
using System.Linq;
using AutomationFramework.Pages.WebPages.Trackers;
using AutomationFramework.Pages.WebPages.Incentive;
using AutomationFramework.Pages.WebPages;

namespace AutomationFramework.Tests.WebTests.Trackers
{
    
    /// <summary>
    /// Test Class
    /// </summary>
    [TestFixture]
  //  [Parallelizable(ParallelScope.Fixtures)]
    [Order(16)]
    public class WeightTracker : Base
    {
        private static ExtentTestManager Report = new ExtentTestManager();
        Common cmn = new Common();
        public static int points;
        string clientname, pageName;
        string isenabled;

        public WeightTracker()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);

        }

        /// <summary>
        /// Test Case: Verifies Weight Tracker
        /// </summary>
        [Test,Order(1)]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyWeightTracker()
        {
            Common config = new Common();
            isenabled = config.GetConfig("IncentiveEnabled").ElementAt(0)[1].ToLower();

            clientname = GlobalVariables.clientname;
            //To call the Page Login Method
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();

            if (isenabled.Equals("true"))
            {
                points = cmn.GetPoints(clientname);
            }

            Common trackermenu = new Common();
            trackermenu.ClickTrackerMenu();
            

            Page_WeightTracker pWeightTracker = new Page_WeightTracker(softassertions);
            pWeightTracker.GoToWeightTracker();
            pWeightTracker.VerifyWeightTracker(GlobalVariables.clientname);
            
            is_soft_assert = true;
            softassertions.AssertAll();
            //Common logout = new Common();
            //logout.LogOut();

        }

        /// <summary>
        /// Test Case: Verifies Tracker graph
        /// </summary>
        [Test, Order(2)]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyTrackerChart()
        {
            //Page_Login plogin = new Page_Login();
            //plogin.Login();
            //Page_HAPrompt haprompt = new Page_HAPrompt();            
            //haprompt.GoToDashboard();

            CommonTracker wt = new CommonTracker(softassertions);
            wt.VerifyTrackerChart();

            is_soft_assert = true;
            softassertions.AssertAll();
        }
        [Test, Order(3)]
        public void TC_ValidateTrackerIncentiveHistory()
        {
            Common config = new Common();
            isenabled = config.GetConfig("IncentiveEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Incentives not enabled for client");
            }

            string category = "I Tracked My Maintain Weight";
            List<string[]> incentivehistorydata = CSVReaderDataTable.GetCSVData("IncentiveHistoryData", pageName, category, GlobalVariables.clientname.ToLower());

            if(incentivehistorydata.Count > 0)
            {
                Page_EligibleActivities peligible = new Page_EligibleActivities(softassertions);
                is_soft_assert = true;

                CommonApi cma = new CommonApi();
                String token = cma.GetToken();
                peligible.InitializeIncentiveHistoryRequest();

                peligible.SetHeader(token);
                peligible.SetMethod();
                peligible.SendRequest();

                peligible.VerifyHistoryData(incentivehistorydata, category);
                softassertions.AssertAll();
            }
            else
            {
                Assert.Ignore("Incentives for Tracker is not available for Client");
            }
        }
        /// <summary>
        /// Test Case: Validate incentives awarded after tracking
        /// </summary>
        [Test, Order(4)]
        public void TC_ValidatePoints()
        {
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Incentives not enabled for client");
            }
            
            int awardedpoints = cmn.GetPoints(clientname);

            int points_tracker = Convert.ToInt32(cmn.GetInstancePointsValue(clientname, "Tracker"));
            int expectedtotalpoints = points + points_tracker;

            Console.WriteLine("Expected : "+expectedtotalpoints);
            Console.WriteLine("Awarded : "+awardedpoints);
            Assert.AreEqual(expectedtotalpoints, awardedpoints);
            Common logout = new Common();
            logout.LogOut();

        }
    }
}