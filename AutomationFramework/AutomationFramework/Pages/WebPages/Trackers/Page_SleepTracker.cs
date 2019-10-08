using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.Trackers
{
    class Page_SleepTracker
    {
        string pageName;
        SoftAssertions softAssertions = null;

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_SleepTracker()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_SleepTracker(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }
        /// <summary>
        /// Clicks on Sleep Tracker menu item in hamburger menu
        /// </summary>
        public void NavigateToSleepTracker()
        {
            SeleniumKeywords.Click(pageName, "sltracker");
        }
        /// <summary>
        /// 1. Enters Data in the specified locators
        /// 2. Verifies History Header
        /// 3. Verifies first 3 values of Tracker in history
        /// </summary>
        /// <returns></returns>
        private void InputAndUpdateTrackerData()
        {
            List<string[]> result = new List<string[]>();
            List<string[]> trackerdata = CSVReaderDataTable.GetCSVData("InputDataContent", pageName, "sleepdata");
            List<string[]> historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "trackerhistoryheader");
            Common date = new Common(softAssertions);
            CommonTracker sl = new CommonTracker(softAssertions);

            date.VerifyDate();

            int changevalueindex = 0;

            int sleepvalue = Convert.ToInt32(trackerdata.ElementAt(0)[4]);           
            for (int i = 0; i <= 2; i++)
            {
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(0)[3], sleepvalue.ToString());
                JavaScriptKeywords.SetTextByControlId("DataPointDate", date.GetDate()[i]);
                sl.ClickUpdateButton();
                sl.ClickViewHistory();
                if (i == 0)
                {
                    sl.ValidateHistoryHeader(historydata, pageName);
                    historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "trackerhistoryvalue");
                }

                String historytabledata = Convert.ToDateTime(date.GetDate()[i]).ToString("dddd, MMM dd yyyy") + "|" + sleepvalue.ToString();
                historydata.ElementAt(0)[4] = historytabledata;

                sl.ValidateHistoryData(historydata, pageName);
                sleepvalue += 2;
            }
        }
        /// <summary>
        /// 1. Navigates to Sleep Tracker
        /// 2. Enters Values in Textboxes
        /// 3. Validate History
        /// </summary>
        /// <returns></returns>
        public void VerifySleepTracker()
        {
            NavigateToSleepTracker();
            InputAndUpdateTrackerData();
        }
    }
}
