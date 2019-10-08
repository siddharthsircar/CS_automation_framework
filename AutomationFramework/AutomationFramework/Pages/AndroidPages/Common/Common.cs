using AutomationFramework.Keywords;
using System;
using System.Configuration;
using System.Threading;

namespace AutomationFramework.Pages.AndroidPages
{
    class Common
    {
        String pageName;
        string pckgname = ConfigurationManager.AppSettings["apppackage"];

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Mob.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Common()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        /// <summary>
        /// This method click on the back button on the top of the screen
        /// </summary>
        public void TabBackButton()
        {
            AppiumKeywords.Tap(pageName, "backbtn");
        }
        /// <summary>
        /// This method closes the Overlays
        /// </summary>
        public void CloseHelpPopup()
        {
            Thread.Sleep(2000);
            AppiumKeywords.Tap(pageName, "helppopup_close", pckgname);
        }

        /// <summary>
        /// Click mobile footer additional menu icon
        /// </summary>
        public void TapFanMenu()
        {
            AppiumKeywords.Tap(pageName, "footermenuicon", pckgname);
        }

        /// <summary>
        /// Click Quick Tracker Menu icon in additional menu
        /// </summary>
        public void TapQuickTrackerMenu()
        {            
            AppiumKeywords.Tap(pageName, "quicktrackermenuicon");
        }

        public void NavigateToQuickTracker()
        {
            TapFanMenu();
            TapQuickTrackerMenu();
        }

        /// <summary>
        /// Click My Profile icon in additional menu
        /// </summary>
        public void TapMyProfileMenu()
        {
            TapFanMenu();
            AppiumKeywords.Tap(pageName, "myprofilemenuicon");
        }
        public void TapCoachIcon()
        {
            //TapFooterMenu();
            AppiumKeywords.Tap(pageName, "footercoachicon");
        }

        /// <summary>
        /// This method taps on the Settings footer menu
        /// </summary>
        public void TapSettingsIcon()
        {
            AppiumKeywords.Tap(pageName, "footersettings");
        }

        /// <summary>
        /// This method taps on the Settings footer menu
        /// </summary>
        public void TapHAIcon()
        {
            AppiumKeywords.Tap(pageName, "haicon");
        }

        public void GetConfig()
        {

        }
    }
}
