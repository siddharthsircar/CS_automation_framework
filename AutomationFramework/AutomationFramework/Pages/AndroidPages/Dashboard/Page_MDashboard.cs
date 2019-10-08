using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace AutomationFramework.Pages.AndroidPages.Dashboard
{
    class Page_MDashboard
    {
        String pageName;
        SoftAssertions softAssertions = null;
        string pckgname = ConfigurationManager.AppSettings["apppackage"];

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Mob.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_MDashboard()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        public Page_MDashboard(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }

        /// <summary>
        /// This method verified a dashboard element 
        /// </summary>
        /// <returns></returns>
        public Boolean AtDashboard()
        {
            Thread.Sleep(4000);
            bool status;
            status = AppiumKeywords.IsElementPresent(pageName, "welcomepagetitle", pckgname);
            return status;
        }
        /// <summary>
        /// This method closes the welcome overlay
        /// </summary>
        private void CloseWelcomePopup()
        {
            AppiumKeywords.Tap(pageName, "welcomepageclose", pckgname);
        }

        /// <summary>
        /// This method taps No button in Notification popup
        /// </summary>
        private void DisableNotification()
        {
            AppiumKeywords.Tap(pageName, "disablenotification");
        }
        /// <summary>
        /// This method taps Yes button in notification popup
        /// </summary>
        private void EnableNotification()
        {
            AppiumKeywords.Tap(pageName, "enablenotification");
        }

        /// <summary>
        /// Click No Thanks on S Health Pop up
        /// </summary>
        private void DisableSHealthConnection()
        {
            AppiumKeywords.Tap(pageName, "shealth_nothnks", pckgname);
        }

        /// <summary>
        /// Click No Thanks on S Health Pop up
        /// </summary>
        private void DisableAppleHealthConnection()
        {
            AppiumKeywords.Tap(pageName, "ahealth_no");
        }

        /// <summary>
        /// This method closes the Overlays
        /// </summary>
        private void CloseHelpPopup()
        {
            AppiumKeywords.Tap(pageName, "helppopup_close", pckgname);
        }
        /// <summary>
        /// This method closes all the overlays
        /// </summary>
        public void CloseAllDashboardOverlays()
        {
            List<string[]> clientconfig = CSVReaderDataTable.GetCSVData("ClientConfig", GlobalVariables.clientname.ToLower());
            string isJourneyGroup = clientconfig.ElementAt(0)[3];
            string DeviceEnabled = clientconfig.ElementAt(0)[4];
            CloseWelcomePopup();
            DisableNotification();

            if (DeviceEnabled.ToLower().Equals("true") && ConfigurationManager.AppSettings["os"].ToLower().Equals("android"))
            {
                DisableSHealthConnection();
            }
            else if (DeviceEnabled.ToLower().Equals("true") && ConfigurationManager.AppSettings["os"].ToLower().Equals("ios"))
            {
                DisableAppleHealthConnection();
            }
            if (isJourneyGroup.ToLower().Equals("true") && ConfigurationManager.AppSettings["os"].ToLower().Equals("android"))
            {
                CloseHelpPopup();
            }
        }

        public void VerifyDashboardHeader(List<string[]> dashboardui)
        {
            // Verify client logo, notification icon are displayed 
            for (int i = 0; i < dashboardui.Count; i++)
            {
                string elementname = dashboardui.ElementAt(i)[2];
                string locatorname = dashboardui.ElementAt(i)[3];
                bool status = AppiumKeywords.IsElementPresent(pageName, locatorname, pckgname);
                softAssertions.Add(" Element : " + elementname, true, status, "equals");
            }
        }

        public Boolean VerifyJourneySectionPresent()
        {
            bool status = AppiumKeywords.IsElementPresent(pageName, "journeysection", pckgname);
            return status;
        }

        public Boolean VerifyEstrmSectionPresent()
        {
            bool status = AppiumKeywords.IsElementPresent(pageName, "estrmsection", pckgname);
            return status;
        }
    }
}
