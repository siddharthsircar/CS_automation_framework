using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using AutomationFramework.Pages.WebPages.Trackers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.Challenges
{
    class Page_Challenges
    {
        String pageName;
        SoftAssertions softAssertion = null;
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_Challenges()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        /// <summary>
        /// The constructor will use to assign local softassertion value to global

        /// </summary>
        public Page_Challenges(SoftAssertions softAssertion) : this()
        {
            this.softAssertion = softAssertion;
        }

        private Boolean verifyPageHeader()
        {
            return (SeleniumKeywords.IsElementPresent(pageName, "challenges_header"));
        }
        private Boolean verifyJoinedChallengePageHeader(String challengename)
        {
            return (SeleniumKeywords.IsElementPresent(pageName, "joined_challengePage_header", challengename));
        }
        private void ClickOnJoinChallenge(String challengesName)
        {
            SeleniumKeywords.Click(pageName, "joinchallenges_btn", challengesName);
        }

        private void JoinChallenge()
        {
            ClickOnJoinChallenge("The Mindful Bite");
        }
        
        private void trackMindfulBiteChallenge()
        {

        }

        public Boolean VerifyChallengesPage()
        {
            if (GlobalVariables.clientname.ToLower().Equals("meabt"))
            {
                SeleniumKeywords.Click(pageName, "submenuchallenges");
            }
            Boolean result;            
            result= verifyPageHeader();
            return result;
            //softAssertion.Add("Challenge Page Header", true, result, "equal");
        }
        public Boolean VerifyChallengeJoined()
        {
            Boolean result;
            ClickOnJoinChallenge("The Mindful Bite");
            result = verifyJoinedChallengePageHeader("The Mindful Bite");
            return result;
        }
        private void ClickOnInProgressChallenge(String cname)
        {
            SeleniumKeywords.Click(pageName, "Inprogress_challenge", cname);
        }
        private void ClickOnLeaveChallengeBtn()
        {
            SeleniumKeywords.Click(pageName, "leave_challenge_btn");
        }
        private void ClickOnLeaveChallengeCancelBtn()
        {
            SeleniumKeywords.Click(pageName, "cancel_btn");
        }
        private void ClickOnLeaveChallengeConfirmBtn()
        {
            SeleniumKeywords.Click(pageName, "confirmation_btn");
        }
        private String VerifyLeavePopupTxt()
        {
            return (SeleniumKeywords.GetText(pageName, "leave_challenge_popup_txt"));
        }
        private Boolean VerifyLeavePopupHeader()
        {
          return(SeleniumKeywords.IsElementPresent(pageName,"leave_challenge_popup_header", "Leave Challenge"));
        }
        private Boolean VerifyChallengeTileVisible(string cname)
        {
            return (SeleniumKeywords.IsElementPresent(pageName, "joinchallenges_btn",cname));
        }
        public void SetDate()
        {
            Common cdate = new Common();
            JavaScriptKeywords.SetTextByControlId("DataPointDate", cdate.GetCurrentDate());
        }
        
        public void ClickOnYesBtn()
        {
            SeleniumKeywords.Click(pageName, "yes_btn");
        }

        public void ClickOnUpdateBtn()
        {
            SeleniumKeywords.Click(pageName, "update_btn");
        }
        public Boolean VerifyLeaveChallenge()
        {
            Boolean result;
            Common com = new Common();
            com.ClickChallengesMenu();
            if (GlobalVariables.clientname.ToLower().Equals("meabt"))
            {
                SeleniumKeywords.Click(pageName, "submenuchallenges");
            }
            ClickOnInProgressChallenge("The Mindful Bite");
            ClickOnLeaveChallengeBtn();
            System.Threading.Thread.Sleep(3000);
            result =VerifyLeavePopupHeader();
            Console.WriteLine("Header verified " + result);
            return result;
            
        }
        public void VerifyandConfirmLeaveChanllengePopup(string popuptxt)
        {
            string txt = VerifyLeavePopupTxt();
            softAssertion.Add("Popup Header", txt, popuptxt, "equal");
            System.Threading.Thread.Sleep(3000);
            //ClickOnLeaveChallengeCancelBtn();

            //ClickOnLeaveChallengeBtn();
           
            ClickOnLeaveChallengeConfirmBtn();
           
            Boolean result = VerifyChallengeTileVisible("The Mindful Bite");
            softAssertion.Add("Challenge Tile",true, result, "equal");
        }
        public void trackChallenge(List<string[]> historyheader, List<string[]> historydata)
        {            
            Common com = new Common();
            com.ClickChallengesMenu();
            if (GlobalVariables.clientname.ToLower().Equals("meabt"))
            {
                SeleniumKeywords.Click(pageName, "submenuchallenges");
            }
            ClickOnInProgressChallenge("The Mindful Bite");
            CommonTracker ct = new CommonTracker();
            DateTime currentdate = DateTime.Today;      
            string calenderdefaultdate = SeleniumKeywords.GetAttributeValue("Common", "calenderdatepicker", "value");
            string currentdt = String.Format("{0:MM/dd/yyyy}", currentdate);
            softAssertion.Add("Date Picker",currentdt,calenderdefaultdate,"equal");
            ClickOnYesBtn();
            JavaScriptKeywords.SetTextByControlId("DataPointDate", com.GetDate()[2]);
            ClickOnUpdateBtn();
            ct.ClickViewHistory();
            ValidateHistoryHeader(historyheader);
            // Step 2: Verify History data
            historydata.ElementAt(0)[4] = Convert.ToDateTime(com.GetDate()[2]).ToString("dddd, MMM dd yyyy");
            ValidateHistoryData(historydata);            
        }

        private void ValidateHistoryHeader(List<string[]> historyheader)
        {
            for (int i = 0; i < historyheader.Count; i++)
            {
                string elementname = historyheader.ElementAt(i)[2];
                string elementlocatorname = historyheader.ElementAt(i)[3];
                string expected = historyheader.ElementAt(i)[4];
                System.Threading.Thread.Sleep(1000);
                string actual = SeleniumKeywords.GetText(pageName, elementlocatorname);
                softAssertion.Add(elementname, expected, actual, "Contains");
            }
        }

        private void ValidateHistoryData(List<string[]> historydata)
        {                        
            System.Threading.Thread.Sleep(3000);            
            for (int i = 0; i < historydata.Count; i++)
            {
                string elementname = historydata.ElementAt(i)[2];
                string elementlocatorname = historydata.ElementAt(i)[3];
                string expected = historydata.ElementAt(i)[4];
                System.Threading.Thread.Sleep(1000);
                string actual = SeleniumKeywords.GetText(pageName, elementlocatorname);
                softAssertion.Add(elementname,expected,actual,"Contains");
            }            
        }/// <summary>
        /// Verify the thisr party challenge popup for Nucore client
        /// </summary>
        public void VerifyThirdPartypopup()
        {
            Boolean result;
            result=SeleniumKeywords.IsElementPresent(pageName, "thirdpartychallengespopup");
            softAssertion.Add("Third Party Challenges popup", true, result, "equals");
            SeleniumKeywords.Click(pageName, "thirdpartychallengespopupok");
            
        }
        /// <summary>
        /// Verify the health trail challenges page loaded for HCSC groups
        /// </summary>
        /// <returns></returns>

        public Boolean verifyHealthTrait()
        {
            Console.WriteLine("Switch to second tab");
            SeleniumKeywords.SwitchToTab(2);
            Boolean result;
            result = SeleniumKeywords.IsElementPresent(pageName, "healthTrailpageheader");
            Console.WriteLine("Close Current Tab");
            SeleniumKeywords.CloseCurrentTab();
            SeleniumKeywords.SwitchToTab(1);
            Console.WriteLine("Switch to first tab");
            return result;
            

        }
    }
}



