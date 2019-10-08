using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.Trackers
{
    class Page_TobaccoTracker
    {
        string pageName;
        SoftAssertions softAssertions = null;

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_TobaccoTracker()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        public Page_TobaccoTracker(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }
        /// <summary>
        /// Clicks on Tobacco Tracker menu item in hamburger menu
        /// </summary>
        public void NavigateToTobaccoTracker()
        {
            SeleniumKeywords.Click(pageName, "tobtracker");
        }
        /// <summary>
        /// 1. Enters Data in the specified locators
        /// 2. Verifies History Header
        /// 3. Verifies first 3 values of Tracker in history
        /// </summary>
        /// <returns></returns>
        private void InputAndUpdateTrackerData()
        {
            List<string[]> trackerdata = CSVReaderDataTable.GetCSVData("InputDataContent", pageName, "tobdata");
            List<string[]> historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "trackerhistoryheader");
            Common date = new Common(softAssertions);
            CommonTracker tob = new CommonTracker(softAssertions);

            date.VerifyDate();

            int changevalueindex = 0;

            int cigvalue = Convert.ToInt32(trackerdata.ElementAt(0)[4]);
            int cigarvalue = Convert.ToInt32(trackerdata.ElementAt(1)[4]);
            int chewsvalue = Convert.ToInt32(trackerdata.ElementAt(2)[4]);
            int dipsvalue = Convert.ToInt32(trackerdata.ElementAt(3)[4]);
            int pipesvalue = Convert.ToInt32(trackerdata.ElementAt(4)[4]);
            for (int i = 0; i <= 2; i++)
            {
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(0)[3], cigvalue.ToString());
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(1)[3], cigarvalue.ToString());
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(2)[3], chewsvalue.ToString());
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(3)[3], dipsvalue.ToString());
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(4)[3], pipesvalue.ToString());
                JavaScriptKeywords.SetTextByControlId("DataPointDate", date.GetDate()[i]);                
                tob.ClickUpdateButton();
                tob.ClickViewHistory();
                if (i == 0)
                {
                    tob.ValidateHistoryHeader(historydata, pageName);
                    historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "trackerhistoryvalue");
                }

                String historytabledata = Convert.ToDateTime(date.GetDate()[i]).ToString("dddd, MMM dd yyyy") + "|" + cigvalue.ToString() + "|" + cigarvalue.ToString() + "|" + chewsvalue.ToString() + "|" + dipsvalue.ToString() + "|" + pipesvalue.ToString();
                historydata.ElementAt(0)[4] = historytabledata;
                
                tob.ValidateHistoryData(historydata, pageName);
                cigvalue += 5;
                cigarvalue += 3;
                chewsvalue += 2;
                dipsvalue += -1;
                pipesvalue += -1;
            }
        }
        private void InputAndUpdateTrackerDataFromGoal()
        {
            List<string[]> trackerdata = CSVReaderDataTable.GetCSVData("InputDataContent", pageName, "tobdata");
            List<string[]> historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "trackerhistoryheader");
            Common date = new Common(softAssertions);
            CommonTracker tob = new CommonTracker(softAssertions);

            date.VerifyDate();

            int changevalueindex = 0;

            int cigvalue = Convert.ToInt32(trackerdata.ElementAt(0)[4]);
            int cigarvalue = Convert.ToInt32(trackerdata.ElementAt(1)[4]);
            int chewsvalue = Convert.ToInt32(trackerdata.ElementAt(2)[4]);
            int dipsvalue = Convert.ToInt32(trackerdata.ElementAt(3)[4]);
            int pipesvalue = Convert.ToInt32(trackerdata.ElementAt(4)[4]);
            for (int i = 0; i <= 2; i++)
            {
                SeleniumKeywords.SelectValueFromDropdown(pageName, "tobaccofreedd", "No");
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(0)[3], cigvalue.ToString());
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(1)[3], cigarvalue.ToString());
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(2)[3], chewsvalue.ToString());
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(3)[3], dipsvalue.ToString());
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(4)[3], pipesvalue.ToString());
                JavaScriptKeywords.SetTextByControlId("DataPointDate", date.GetDate()[i]);
                tob.ClickUpdateButton();
                tob.ClickViewHistory();
                if (i == 0)
                {
                    tob.ValidateHistoryHeader(historydata, pageName);
                    historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "trackerhistoryvalue");
                }

                String historytabledata = Convert.ToDateTime(date.GetDate()[i]).ToString("dddd, MMM dd yyyy") + "|" + cigvalue.ToString() + "|" + cigarvalue.ToString() + "|" + chewsvalue.ToString() + "|" + dipsvalue.ToString() + "|" + pipesvalue.ToString();
                historydata.ElementAt(0)[4] = historytabledata;

                tob.ValidateHistoryData(historydata, pageName);
                cigvalue += 5;
                cigarvalue += 3;
                chewsvalue += 2;
                dipsvalue += -1;
                pipesvalue += -1;
            }
        }
        public void GoToTobaccoTracker()
        {
            NavigateToTobaccoTracker();
        }
        /// <summary>
        /// 1. Navigates to Tobacco Tracker
        /// 2. Enters Values in Textboxes
        /// 3. Validate History
        /// </summary>
        /// <returns></returns>
        public void VerifyTobaccoTracker()
        {
            InputAndUpdateTrackerData();
        }
        public void VerifyTobaccoTrackerFromGoal()
        {
            InputAndUpdateTrackerDataFromGoal();
        }
    }
}
