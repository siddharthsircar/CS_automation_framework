using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.AndroidPages
{
    
    class Page_AndroidMessageToCoach
    {
        String pageName;
        SoftAssertions softAssertions = null;
        string pckgname = ConfigurationManager.AppSettings["apppackage"];

        public Page_AndroidMessageToCoach()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_AndroidMessageToCoach(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }
        public void VerifyMessageToCoachScreen(List<string[]> messagetocoachui)
        {
            // Verify coach label, Hours of operation, send message and call now button 
            for (int i = 0; i < messagetocoachui.Count; i++)
            {
                string elementname = messagetocoachui.ElementAt(i)[2];
                string locatorname = messagetocoachui.ElementAt(i)[3];
                string expectedtext = messagetocoachui.ElementAt(i)[4];
                string actualtext = AppiumKeywords.GetText(pageName, locatorname, pckgname);
                softAssertions.Add(elementname, expectedtext, actualtext, "contains");
            }
        }
        
        public void TapToSendMessage()
        {
            AppiumKeywords.Tap(pageName, "sendmessagebtntxt", pckgname);
        }
        public void InputText()
        {
            AppiumKeywords.SetText(pageName, "messagebox", "Hi Coach", pckgname);
        }
        public void SendTextToCoach()
        {
            AppiumKeywords.Tap(pageName, "messagesendbtn", pckgname);
        }
        public string GetText()
        {
           return(AppiumKeywords.GetText(pageName, "messageareatxt",pckgname));
        }
        public void NavigateToMessageToCoach()
        {
            Common cmn = new Common();
            cmn.TapCoachIcon();
        }

    }
}
