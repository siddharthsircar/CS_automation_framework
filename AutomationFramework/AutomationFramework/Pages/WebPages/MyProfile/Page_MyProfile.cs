using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Threading;

namespace AutomationFramework.Pages.WebPages.MyProfile
{
    class Page_MyProfile
    {
        string pageName;
        SoftAssertions softAssertions = null;

        public Page_MyProfile()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_MyProfile(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }
        /// <summary>
        /// Return the default palceholder value of status message 
        /// </summary>
        private string GetStatusPaceholderValue()
        {
            return (SeleniumKeywords.GetAttributeValue(pageName, "statusmessage_textarea", "placeholder"));
        }
        /// <summary>
        /// This method verify the First and Last name of connect member 
        /// </summary>
        /// <returns></returns>
        public string VerifyMemberName()
        {
            Common cmn = new Common();
            cmn.GoToMyProfile();
            String result = SeleniumKeywords.GetText(pageName, "connection_namelbl");
            result = result.Substring(6);
            Console.WriteLine("Result");
            return (result);
        }
        private void clickOnEditStatusBtn()
        {
            SeleniumKeywords.Click(pageName, "edit_statusmessagebtn");
        }

        private void setstatusmessage()
        {
            SeleniumKeywords.SetText(pageName, "statusmessage_textarea", "I am Rocking");
        }
        private string charactercount()
        {
            string count =SeleniumKeywords.GetText(pageName, "statusmessage_inputcharcount");
            return count="characters: " + count + " / 70";
        }
        private void clickOnClearIcon()
        {
            SeleniumKeywords.Click(pageName, "statusmessage_clearicon");
        }
        private void clickOnUpdate()
        {
            SeleniumKeywords.Click(pageName, "statusmessage_updatebtn");
        }
        private void ClickOnBallVisible()
        {
            SeleniumKeywords.Click(pageName, "statusmessage_metball");
        }
        private string GetStatusMessage()
        {
            return (SeleniumKeywords.GetText(pageName, "statusmessage_textarea"));
        }
       public string VerifyClearStatusMessage()
        {
            Common cmn = new Common();
            cmn.GoToMyProfile();
            System.Threading.Thread.Sleep(1000);
            clickOnEditStatusBtn();
            setstatusmessage();
            String count = charactercount();
            softAssertions.Add("CharacterCount", "characters: 12 / 70", count, "equals");
            clickOnClearIcon();

            return(GetStatusPaceholderValue());
            
        }
        public string UpdateStatus()
        {
            Common cmn = new Common();
            cmn.GoToMyProfile();
            Thread.Sleep(1000);
            clickOnEditStatusBtn();
            setstatusmessage();
            clickOnUpdate();
            String msg =GetStatusMessage();
            ClickOnBallVisible();
            return (msg);
        }

        /// <summary>
        /// Click on tracker tab to bring user's trackers in view
        /// </summary>
        public void ClickTrackerTab()
        {
            SeleniumKeywords.Click(pageName,"trackertab");
            Thread.Sleep(1000);
        }

    }
}
