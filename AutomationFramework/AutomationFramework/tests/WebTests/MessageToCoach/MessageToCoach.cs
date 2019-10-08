using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages;
using AutomationFramework.Pages.WebPages.Incentive;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationFramework.Tests.WebTests
{   /// <summary>
    /// Test Class: Message To Coach
    /// </summary>
    [TestFixture]
   // [Parallelizable(ParallelScope.Fixtures)]
    [Order(8)]
    public class MessageToCoach : Base
    {
        int points;
        string expmsg;
        string clientname;
        Common cmn = new Common();
        string incentiveEnabled, pageName;
        string coachingEnabled;

        public MessageToCoach()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        /// <summary>
        /// Test Case: To verify Message UI
        /// </summary>
        [Test]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("CoachingReg")]
        [Order(1)]
        public void TC_VerifyMessageToCoachUI()
        {
            List<string[]> isEnabled = cmn.GetConfig("CoachingEnabled", "IncentiveEnabled");
            incentiveEnabled = isEnabled.ElementAt(0)[2].ToLower();
            coachingEnabled = isEnabled.ElementAt(0)[1].ToLower();
            clientname = GlobalVariables.clientname;
            if (coachingEnabled.Equals("false") && !GlobalVariables.clientname.ToLower().Equals("arc"))
            {
                Assert.Ignore("Message to coach not available for client");
            }

            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
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
            //Common logout = new Common();
            //logout.LogOut();
        }
        
        /// <summary>
        /// This test case is use to verify Send message functionality
        /// along with DropDown filter, Send, Active, Inboun, Delete, Archive.
        /// </summary>
        [Test]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("CoachingReg")]
        [Order(2)]
        public void TC_VerifySentMsgToCoach()
        {
            List<string[]> isEnabled = cmn.GetConfig("CoachingEnabled", "IncentiveEnabled");
            incentiveEnabled = isEnabled.ElementAt(0)[2].ToLower();
            coachingEnabled = isEnabled.ElementAt(0)[1].ToLower();
            if (coachingEnabled.Equals("false") && !GlobalVariables.clientname.ToLower().Equals("arc"))
            {
                Assert.Ignore("Message to coach not available for client");
            }
            //Page_Login plogin = new Page_Login();
            //plogin.Login();
            //Common pcommon = new Pages.Common();
            //pcommon.GoToMessageToCoachPage();
            expmsg = "Hi Coach this is me";
            //this call send and verify the send message to coach
            Page_MessageToCoach pmsgcoach = new Page_MessageToCoach();
            String actmesg = pmsgcoach.VerifyMsgToCoach(expmsg);
            SeleniumKeywords.RefreshPage();

            //Console.WriteLine(expmsg.ElementAt(0)[0]);
            Assert.AreEqual(expmsg, actmesg);            
        }

        [Test, Order(3)]
        public void TC_ValidateMessageToCoachIncentiveHistory()
        {
            List<string[]> isEnabled = cmn.GetConfig("CoachingEnabled", "IncentiveEnabled");
            incentiveEnabled = isEnabled.ElementAt(0)[2].ToLower();
            coachingEnabled = isEnabled.ElementAt(0)[1].ToLower();
            if (coachingEnabled.Equals("false") && !GlobalVariables.clientname.ToLower().Equals("arc"))
            {
                Assert.Ignore("Message to coach not available for client");
            }
            if (incentiveEnabled.Equals("false"))
            {
                Assert.Ignore("Incentives not enabled for client");
            }

            string category = "I Contacted My Health Coach";
            List<string[]> incentivehistorydata = CSVReaderDataTable.GetCSVData("IncentiveHistoryData", pageName, category, GlobalVariables.clientname.ToLower());

            if (incentivehistorydata.Count > 0)
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
                Assert.Ignore("Incentives for Message To Coach is not available for Client");
            }
            

        }

        /// <summary>
        /// Test Case: To verify incectives after messaging to coach
        /// </summary>
        [Test, Order(4)]
        [Category("BuildSanity")]
        [Category("CoachingReg")]
        [Category("ProdSanity")]
        [Category("Regression")]
        public void TC_ValidatePoints()
        {
            List<string[]> isEnabled = cmn.GetConfig("CoachingEnabled", "IncentiveEnabled");
            incentiveEnabled = isEnabled.ElementAt(0)[2].ToLower();
            coachingEnabled = isEnabled.ElementAt(0)[1].ToLower();
            if (coachingEnabled.Equals("false") && !GlobalVariables.clientname.ToLower().Equals("arc"))
            {
                Assert.Ignore("Message to coach not available for client");
            }
            if (incentiveEnabled.Equals("false"))
            {
                Assert.Ignore("Incentives not enabled for client");
            }
            is_soft_assert = false;
            //if (GlobalVariables.clientname.ToLower().Equals("meabt") || GlobalVariables.clientname.ToLower().Equals("medicare advantage"))
            //{
            //    Assert.Ignore("Incentive for Mesage To Coach not available for " + GlobalVariables.clientname);
            //}
            //else
            //{
                int awardedpoints = cmn.GetPoints(GlobalVariables.clientname);
                int points_contacthealthcoach = Convert.ToInt32(cmn.GetInstancePointsValue(GlobalVariables.clientname.ToLower(), "ContactHealthCoach"));
                int expectedtotalpoints = points + points_contacthealthcoach;
                Console.WriteLine("Expected : " + expectedtotalpoints);
                Console.WriteLine("Awarded : " + awardedpoints);
                Assert.AreEqual(expectedtotalpoints, awardedpoints);
            //}
        }

        /// <summary>
        /// This test case is use to verify Send message functionality
        /// along with DropDown filter, Send, Active, Inbound, Delete, Archive.
        /// </summary>
        [Test]
        //[Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("CoachingReg")]
        [Order(4)]
        public void TC_VerifyMessageFilters()
        {
            if (coachingEnabled.Equals("false") && !GlobalVariables.clientname.ToLower().Equals("arc"))
            {
                Assert.Ignore("Message to coach not available for client");
            }
            List<string[]> result = new List<string[]>();
            Page_MessageToCoach pmsgcoach = new Page_MessageToCoach();

            //This softAssert is use to verify Archive message, Delete message and all the filter Sent, Deleted, Archived, Inbound and All  
            result = pmsgcoach.VerfySendMsgDropDown();
            string actualmsg;
            is_soft_assert = false;
            Assert.Multiple(() =>
            {
                for (int i = 0; i < result.Count; i++)
                {
                    actualmsg = result.ElementAt(i)[0];
                    if (actualmsg == "You have no inbound messages.")
                    {
                        Assert.AreEqual("You have no inbound messages.", actualmsg);
                    }
                    else
                    {
                        Assert.AreEqual(actualmsg, expmsg);
                    }
                }
            }
            );
          
        }
    }
}
