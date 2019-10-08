using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.AndroidPages.Challenges
{
    class Page_AndroidChallenges
    {
        String pageName;
        SoftAssertions softAssertions = null;
        string pckgname = ConfigurationManager.AppSettings["apppackage"];

        public Page_AndroidChallenges()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_AndroidChallenges(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }
        private void TapToChallenges()
        {
            AppiumKeywords.Tap(pageName, "challengeslbl");
        }
        public void NavigateToChallenges()
        {
            Common cmn = new Common();
            cmn.TapFanMenu();
            TapToChallenges();
            cmn.CloseHelpPopup();

        }
    }
}
