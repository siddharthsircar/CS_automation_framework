using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace AutomationFramework.Pages.AndroidPages.Trackers
{
    class Page_MWeightTracker
    {
        String pageName;
        SoftAssertions softAssertions = null;
        string pckgname = ConfigurationManager.AppSettings["apppackage"];
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Mob.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_MWeightTracker()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_MWeightTracker(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }      
        
        /// <summary>
        /// Method to update tracker
        /// </summary>
        public void UpdateWeightTracker(List<string[]> trackerdata)
        {
            string weightvalue = trackerdata.ElementAt(0)[4];
            Thread.Sleep(3000);
            //Enter weight
            AppiumKeywords.SetText(pageName,"weight_inputbx", weightvalue ,pckgname);
            //Tap update button
            AppiumKeywords.Tap(pageName, "trackerupdate_btn", pckgname);
            //Thread.Sleep(2000);
        }

        public void VerifyTrackerHistory(List<string[]> trackerhistory)
        {
            DateTime today = DateTime.Today;
            string currentdt = today.ToString("dddd, dd MMM, yyyy");
            string currentdt1 = String.Format("{0:MM/dd/yy}", today);

            for (int i = 0; i < trackerhistory.Count; i++)
            {
                if(i == 3)
                {
                    trackerhistory.ElementAt(3)[5] = currentdt;
                }
                else if (i == 5)
                {
                    trackerhistory.ElementAt(5)[5] = currentdt1;
                }
            }
            Thread.Sleep(3000);
            AppiumKeywords.Swipe(1300,1552,100,1552);

            CommonTrackers ct = new CommonTrackers(softAssertions);
            ct.ValidateTrackerHistory(trackerhistory);
        }
    }
}
