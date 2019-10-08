using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.Trackers
{
    class Page_BPTracker
    {
        string pageName;
        bool trackerfrommenu = false;
        SoftAssertions softAssertions = null;

        public Page_BPTracker()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_BPTracker(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }
        /// <summary>
        /// Clicks on BP Tracker menu item in hamburger menu
        /// </summary>
        public void NavigateToBPTracker()
        {
            trackerfrommenu = true;
            SeleniumKeywords.Click(pageName, "bptracker");
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
            List<string[]> trackerdata = CSVReaderDataTable.GetCSVData("InputDataContent", pageName, "bpdata");
            List<string[]> historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "trackerhistoryheader");
            List<string[]> latestvaluedata = CSVReaderDataTable.GetCSVData("TrackerContent", pageName, "latestvalues");
            Common date = new Common(softAssertions);
            CommonTracker bp = new CommonTracker(softAssertions);

            date.VerifyDate();
            
            int sysvalue = Convert.ToInt32(trackerdata.ElementAt(0)[4]);
            int diavalue = Convert.ToInt32(trackerdata.ElementAt(1)[4]);
            for (int i = 0; i <= 2; i++)
            {
                if (i > 0)
                {
                    sysvalue += 10;
                    diavalue += 10;
                }
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(0)[3], sysvalue.ToString());
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(1)[3], diavalue.ToString());
                JavaScriptKeywords.SetTextByControlId("DataPointDate", date.GetDate()[i]);
                bp.ClickUpdateButton();
                System.Threading.Thread.Sleep(2000);                            
                
                bp.ClickViewHistory();
                if (i == 0)
                {
                    bp.ValidateHistoryHeader(historydata, pageName);
                    historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "trackerhistoryvalue");
                }
                String historytabledata = Convert.ToDateTime(date.GetDate()[i]).ToString("dddd, MMM dd yyyy") + "|" + sysvalue.ToString() + " / " + diavalue.ToString();
                historydata.ElementAt(0)[4] = historytabledata;
                bp.ValidateHistoryData(historydata,pageName);                
            }

            if (trackerfrommenu == true)
            {
                latestvaluedata.ElementAt(1)[4] = sysvalue.ToString() + "/" + diavalue.ToString();
                latestvaluedata.ElementAt(3)[4] = Convert.ToDateTime(date.GetDate()[2]).ToString("MMMM d, yyyy");
                bp.ValidateTrackerLatestValues(latestvaluedata);
            }
        }
       public void GoToBPTracker()
       {
            NavigateToBPTracker();
       }
        /// <summary>
        /// 1. Navigates to BP Tracker
        /// 2. Enters Values in BP Textboxes
        /// 3. Validate History
        /// </summary>
        /// <returns></returns>
        public void VerifyBPTracker()
        {
            InputAndUpdateTrackerData();            
        }
    }
}
