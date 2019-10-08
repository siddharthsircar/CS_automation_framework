using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace AutomationFramework.Pages.AndroidPages.Trackers
{
    class CommonTrackers
    {
        String pageName;
        SoftAssertions softAssertions = null;
        string pckgname = ConfigurationManager.AppSettings["apppackage"];
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Mob.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public CommonTrackers()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public CommonTrackers(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }

        /// <summary>
        /// Click Weight Tracker tile
        /// </summary>
        public void NavigateToTracker(string trackerName)
        {
            Thread.Sleep(4000);
            AppiumKeywords.Tap("Page_TrackerHome", "trackertile", trackerName);
        }

        /// <summary>
        /// Method to verify Weight tracker page ui
        /// </summary>
        public void ValidatePageUI(List<string[]> elements, string trackerPageName)
        {
            string pagename = pageName;
            Thread.Sleep(3000);
            for (int i = 0; i < elements.Count(); i++)
            {
                string locatorname = elements.ElementAt(i)[3];
                string elementname = elements.ElementAt(i)[2];
                string action = elements.ElementAt(i)[4];
                if (action.ToLower().Equals("verifytext"))
                {
                    string expectedtxt = elements.ElementAt(i)[5];
                    string actual = AppiumKeywords.GetText(pagename, locatorname, pckgname);
                    softAssertions.Add(elementname, expectedtxt, actual, "contains");
                }
                else if (action.ToLower().Equals("verifyexists"))
                {
                    bool actualstatus = AppiumKeywords.IsElementPresent(pagename, locatorname, pckgname);
                    softAssertions.Add(elementname, true, actualstatus, "equals");
                }
                if (i == 6)
                {
                    pagename = trackerPageName;
                }
            }
        }

        /// <summary>
        /// Method to verify Weight tracker page ui
        /// </summary>
        public void ValidateTrackerHistory(List<string[]> elements)
        {
            //string pagename = pageName;
            Thread.Sleep(3000);
            for (int i = 0; i < elements.Count(); i++)
            {
                string locatorname = elements.ElementAt(i)[3];
                string elementname = elements.ElementAt(i)[2];
                string action = elements.ElementAt(i)[4];
                if (action.ToLower().Equals("verifytext"))
                {
                    string expectedtxt = elements.ElementAt(i)[5];
                    string actual = AppiumKeywords.GetText(pageName, locatorname, pckgname);
                    softAssertions.Add(elementname, expectedtxt, actual, "contains");
                }
                else if (action.ToLower().Equals("verifyexists"))
                {
                    bool actualstatus = AppiumKeywords.IsElementPresent(pageName, locatorname, pckgname);
                    softAssertions.Add(elementname, true, actualstatus, "equals");
                }
            }
        }

        /// <summary>
        /// Method select date based on the parameter passed
        /// </summary>
        public void SetTrackerDate(string date)
        {
            //Open calendar
            AppiumKeywords.Tap(pageName, "trackerdate_cntrl", pckgname);
            //Select Date
            AppiumKeywords.Tap(pageName, "trackercalendar_date", date);
            //Click calendar OK
            AppiumKeywords.Tap(pageName, "trackercalendar_ok");
        }
    }
}
