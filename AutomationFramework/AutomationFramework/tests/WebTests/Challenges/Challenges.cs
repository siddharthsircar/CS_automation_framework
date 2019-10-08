using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages;
using AutomationFramework.Pages.WebPages.Challenges;
using AutomationFramework.Pages.WebPages.Incentive;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AutomationFramework.Tests.WebTests
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    [Order(12)]
    public class Challenges : Base
    {
        String pageName;
        int points;
        string challengeEnabled;
        Common cmn = new Common();
        string clientname = GlobalVariables.clientname;
        string incentiveEnabled;
        string thirdPartyChallenge;
        public Challenges()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        [Test, Order(1)]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("ChalleReg")]
        public void TC_VerifyAtChallengePage()
        {
            challengeEnabled = cmn.GetConfig("ChallengesEnabled").ElementAt(0)[1].ToLower();
            incentiveEnabled = cmn.GetConfig("IncentiveEnabled").ElementAt(0)[1].ToLower();
            if (challengeEnabled.Equals("false"))
            {
                Assert.Ignore("Challenges not available for the client");
            }
            //To call the Page Login Method
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();

            if (incentiveEnabled.Equals("true"))
            {
                points = cmn.GetPoints(clientname);
            }
			Common com = new Common();
            com.ClickChallengesMenu();
            Page_Challenges pchal = new Page_Challenges();
            Assert.IsTrue(pchal.VerifyChallengesPage());
        }

        [Test, Order(2)]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("ChalleReg")]
        public void TC_VerifyChallengeJoined()
        {
            challengeEnabled = cmn.GetConfig("ChallengesEnabled").ElementAt(0)[1].ToLower();

            if (challengeEnabled.Equals("false"))
            {
                Assert.Ignore("Challenges not available for the client");
            }
            //To call the Page Login Method
            //Page_Login plogin = new Page_Login();
            //plogin.Login();
            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();
            //Common com = new Common();
            //com.ClickChallengesMenu();
            Page_Challenges pchal = new Page_Challenges();
            Assert.IsTrue(pchal.VerifyChallengeJoined());
        }

        /// <summary>
        /// Test Case to track in Challenge
        /// </summary>
        [Test, Order(3)]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("ChalleReg")]
        public void TC_VerifyTrackChallenge()
        {
            challengeEnabled = cmn.GetConfig("ChallengesEnabled").ElementAt(0)[1].ToLower();

            if (challengeEnabled.Equals("false"))
            {
                Assert.Ignore("Challenges not available for the client");
            }
            ////To call the Page Login Method
            //Page_Login plogin = new Page_Login();
            //plogin.Login();
            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();
            //Common com = new Common();
            //com.ClickChallengesMenu();
            Page_Challenges pchal = new Page_Challenges(softassertions);
            //Assert.IsTrue(pchal.VerifyChallengeJoined());
            List<string[]> historyheader = CSVReaderDataTable.GetCSVData("ChallengesContent", pageName, "trackerhistoryheader");
            List<string[]> historydata = CSVReaderDataTable.GetCSVData("ChallengesContent", pageName, "trackerhistoryvalue");
            pchal.trackChallenge(historyheader, historydata);
            softassertions.AssertAll();
            is_soft_assert = true;
        }

        [Test, Order(4)]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("ChalleReg")]
        public void TC_VerifyLeaveChallenge()
        {
            challengeEnabled = cmn.GetConfig("ChallengesEnabled").ElementAt(0)[1].ToLower();

            if (challengeEnabled.Equals("false"))
            {
                Assert.Ignore("Challenges not available for the client");
            }
            ////To call the Page Login Method
            //Page_Login plogin = new Page_Login();
            //plogin.Login();
            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();
            List<String[]> popupmsg = CSVReaderDataTable.GetCSVData("ChallengesContent", pageName, "popuptxt");
            Page_Challenges pchal = new Page_Challenges(softassertions);
            is_soft_assert = true;
            Assert.IsTrue(pchal.VerifyLeaveChallenge());
            pchal.VerifyandConfirmLeaveChanllengePopup(popupmsg.ElementAt(0)[4]);
            softassertions.AssertAll();
            
        }

        [Test, Order(5)]
        public void TC_ValidatJoinChallengeIncentiveHistory()
        {
            challengeEnabled = cmn.GetConfig("ChallengesEnabled").ElementAt(0)[1].ToLower();
            incentiveEnabled = cmn.GetConfig("IncentiveEnabled").ElementAt(0)[1].ToLower();
            if (challengeEnabled.Equals("false"))
            {
                Assert.Ignore("Challenges not available for the client");
            }
            if (incentiveEnabled.Equals("false"))
            {
                Assert.Ignore("Incentives not enabled for client");
            }

            string category = "I Participated in a Personal Challenge";
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
                Assert.Ignore("Incentives for Participated in a Personal Challenge is not available for Client");
            }


        }

        /// <summary>
        /// Test Case to validate incentives after joining challenge
        /// </summary>
        [Test, Order(6)]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("ChalleReg")]
        public void TC_ValidatePoints()
        {
            challengeEnabled = cmn.GetConfig("ChallengesEnabled").ElementAt(0)[1].ToLower();
            incentiveEnabled = cmn.GetConfig("IncentiveEnabled").ElementAt(0)[1].ToLower();
            if (challengeEnabled.Equals("false"))
            {
                Assert.Ignore("Challenges not available for the client");
            }
            if (incentiveEnabled.Equals("false"))
            {
                Assert.Ignore("Incentives not enabled for client");
            }
            //if (GlobalVariables.clientname.ToLower().Equals("meabt"))
            //{
            //    Assert.Ignore("Point not awarded for Join chalenge in  Client:  " + GlobalVariables.clientname);
            //}
            //else
            //{
                int awardedpoints = cmn.GetPoints(clientname);
                int points_challenge = Convert.ToInt32(cmn.GetInstancePointsValue(clientname, "JoinChallenge"));
                int expectedtotalpoints = points + points_challenge;

                Console.WriteLine("Expected : " + expectedtotalpoints);
                Console.WriteLine("Awarded : " + awardedpoints);
                Assert.AreEqual(expectedtotalpoints, awardedpoints);
            //}
            cmn.LogOut();
        }
        /// <summary>
        /// Verify the thisr party challenge popup for Nucore client
        /// </summary>
        [Test, Order(7)]
        //[Category("ProdSanity")]
        //[Category("Regression")]
        [Category("ThirdPartyChalleReg")]
        public void TC_VerifyThirdPartyChallengePage()
        {
            thirdPartyChallenge = cmn.GetConfig("ThirdPartyChallenges").ElementAt(0)[1].ToLower();
            challengeEnabled = cmn.GetConfig("ChallengesEnabled").ElementAt(0)[1].ToLower();
            if (challengeEnabled.Equals("false") && thirdPartyChallenge.Equals("false"))
            {
                Assert.Ignore("Challenges not available for the client");
            }
            else if (challengeEnabled.Equals("false") && thirdPartyChallenge.Equals("true"))
            {
                //To call the Page Login Method
                Page_Login plogin = new Page_Login();
                plogin.Login();
                Page_HAPrompt haprompt = new Page_HAPrompt();
                haprompt.GoToDashboard();

                Common com = new Common();
                com.ClickChallengesMenu();
                Page_Challenges pch = new Page_Challenges(softassertions);
                is_soft_assert = true;
                if (GlobalVariables.clientname.ToLower().Equals("nucor"))
                {
                    pch.VerifyThirdPartypopup();

                }
                else
                {
                    pch.verifyHealthTrait();
                }
                cmn.CloseHamMenu();
                cmn.LogOut();
            }
            
        }
    }   
}
