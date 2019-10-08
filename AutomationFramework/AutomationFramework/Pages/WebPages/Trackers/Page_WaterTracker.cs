using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.Trackers
{
    class Page_WaterTracker
    {        
        string pageName;
        SoftAssertions softAssertions = null;

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_WaterTracker()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        public Page_WaterTracker(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }
        /// <summary>
        /// Clicks on Cholesterol Tracker menu item in hamburger menu
        /// </summary>
        public void NavigateToWaterTracker()
        {
            SeleniumKeywords.Click(pageName, "watertracker");
        }
        /// <summary>
        /// 1. Enters Data in the specified locators
        /// 2. Verifies History Header
        /// 3. Verifies first 3 values of Tracker in history
        /// </summary>
        /// <returns></returns>
        private void InputAndUpdateTrackerData()
        {
            List<string[]> trackerdata = CSVReaderDataTable.GetCSVData("InputDataContent", pageName, "waterdata");
            List<string[]> historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "trackerhistoryheader");
            List<string[]> latestvaluedata = CSVReaderDataTable.GetCSVData("TrackerContent", pageName, "latestvalues");
            Common date = new Common(softAssertions);
            CommonTracker water = new CommonTracker(softAssertions);

            date.VerifyDate();

            int changevalueindex = 0;

            int watervalue = Convert.ToInt32(trackerdata.ElementAt(0)[4]);
            for (int i = 0; i <= 2; i++)
            {
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(0)[3], watervalue.ToString());
                JavaScriptKeywords.SetTextByControlId("DataPointDate", date.GetDate()[i]);
                water.ClickUpdateButton();
                System.Threading.Thread.Sleep(10000);

                //latestvaluedata.ElementAt(1)[4] = watervalue.ToString();
                //latestvaluedata.ElementAt(3)[4] = Convert.ToDateTime(date.GetDate()[i]).ToString("MMMM dd, yyyy");
                //water.ValidateTrackerLatestValues(latestvaluedata);

                water.ClickViewHistory();
                if (i == 0)
                {
                    water.ValidateHistoryHeader(historydata, pageName);
                    historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "trackerhistoryvalue");
                }

                String historytabledata = Convert.ToDateTime(date.GetDate()[i]).ToString("dddd, MMM dd yyyy")+"|"+ watervalue.ToString();
                historydata.ElementAt(changevalueindex)[4] = historytabledata;

                water.ValidateHistoryData(historydata, pageName);
                watervalue += 10;
            }
        }
        /// <summary>
        /// 1. Navigates to Water Tracker
        /// 2. Enters Values in Textboxes
        /// 3. Validate History
        /// </summary>
        /// <returns></returns>
        public void VerifyWaterTracker()
        {
            NavigateToWaterTracker();
            InputAndUpdateTrackerData();
        }
    }
}
