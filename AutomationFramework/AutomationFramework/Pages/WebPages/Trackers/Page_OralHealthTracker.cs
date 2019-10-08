using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationFramework.Pages.WebPages.Trackers
{
    class Page_OralHealthTracker
    {        
        string pageName;
        SoftAssertions softAssertions = null;

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_OralHealthTracker()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        public Page_OralHealthTracker(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }

        /// <summary>
        /// Clicks on Cholesterol Tracker menu item in hamburger menu
        /// </summary>
        public void NavigateToOralHealthTracker()
        {
            SeleniumKeywords.Click(pageName, "oralhlthtracker");
        }
        /// <summary>
        /// 1. Enters Data in the specified locators
        /// 2. Verifies History Header
        /// 3. Verifies first 3 values of Tracker in history
        /// </summary>
        /// <returns></returns>
        private void InputAndUpdateTrackerData()
        {
            List<string[]> trackerdata = CSVReaderDataTable.GetCSVData("InputDataContent", pageName, "oraldata");
            List<string[]> historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "trackerhistoryheader");


            Common date = new Common(softAssertions);
            CommonTracker oral = new CommonTracker(softAssertions);

            date.VerifyDate();

            int brushing = Convert.ToInt32(trackerdata.ElementAt(0)[4]);
            int flossing = Convert.ToInt32(trackerdata.ElementAt(1)[4]);
            for (int i = 0; i <= 2; i++)
            {
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(0)[3], brushing.ToString());
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(1)[3], flossing.ToString());
                JavaScriptKeywords.SetTextByControlId("DataPointDate", date.GetDate()[i]);
                oral.ClickUpdateButton();
                oral.ClickViewHistory();
                if (i == 0)
                {
                    oral.ValidateHistoryHeader(historydata, pageName);
                    historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "trackerhistoryvalue");
                }

                String historytabledata = Convert.ToDateTime(date.GetDate()[i]).ToString("dddd, MMM dd yyyy") + "|" + brushing.ToString() + "|" + flossing.ToString();
                historydata.ElementAt(0)[4] = historytabledata;
                
                oral.ValidateHistoryData(historydata, pageName);
                brushing += 2;
                flossing += 1;
            }
        }
        /// <summary>
        /// 1. Navigates to Physical Activity Tracker
        /// 2. Enters Values in Textboxes
        /// 3. Validate History
        /// </summary>
        /// <returns></returns>
        public void VerifyOralHealthTracker()
        {
            NavigateToOralHealthTracker();
            InputAndUpdateTrackerData();                     

        }
    }
}
