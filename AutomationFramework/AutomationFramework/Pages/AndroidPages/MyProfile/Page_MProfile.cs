using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.AndroidPages.MyProfile
{
    class Page_MProfile
    {
        string pageName;
        SoftAssertions softAssertions = null;
        Common cmn = new Common();
        string pckgname = ConfigurationManager.AppSettings["apppackage"];

        public Page_MProfile()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_MProfile(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }

        private void TapTrackerViewAllButton()
        {
            AppiumKeywords.Tap(pageName, "trackerviewall_link", pckgname);
        }
        /// <summary>
        /// Method to Tap on View All link in My Profile page
        /// Navigates user to Tracker Home Page
        /// </summary>
        public void NavigateToTrackerHomePage()
        {
            cmn.TapMyProfileMenu();
            cmn.CloseHelpPopup();
            TapTrackerViewAllButton();
            cmn.CloseHelpPopup();
        }
    }
}

