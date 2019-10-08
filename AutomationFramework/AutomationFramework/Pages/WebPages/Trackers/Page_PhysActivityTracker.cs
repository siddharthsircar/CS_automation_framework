using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.Trackers
{
    class Page_PhysActivityTracker
    {
        string pageName;
        SoftAssertions softAssertions = null;

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_PhysActivityTracker()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        public Page_PhysActivityTracker(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }
        /// <summary>
        /// Clicks on Cholesterol Tracker menu item in hamburger menu
        /// </summary>
        private void NavigateToPATracker()
        {
            SeleniumKeywords.Click(pageName, "patracker");
        }
        /// <summary>
        /// 1. Enters Data in the specified locators
        /// 2. Verifies History Header
        /// 3. Verifies first 3 values of Tracker in history
        /// </summary>
        /// <returns></returns>
        private void InputAndUpdateTrackerData()
        {
            List<string[]> trackerdata = CSVReaderDataTable.GetCSVData("InputDataContent", pageName, "padata");
            List<string[]> historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "trackerhistoryheader");
            Common date = new Common(softAssertions);
            CommonTracker pa = new CommonTracker(softAssertions);

            date.VerifyDate();

            int cardiovalue = Convert.ToInt32(trackerdata.ElementAt(0)[4]);
            int strengthvalue = Convert.ToInt32(trackerdata.ElementAt(1)[4]);
            int stretchvalue = Convert.ToInt32(trackerdata.ElementAt(2)[4]);
            for (int i = 0; i <= 2; i++)

            {
                System.Threading.Thread.Sleep(2000);
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(0)[3], cardiovalue.ToString());
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(1)[3], strengthvalue.ToString());
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(2)[3], stretchvalue.ToString());
                JavaScriptKeywords.SetTextByControlId("DataPointDate", date.GetDate()[i]);
                pa.ClickUpdateButton();
                System.Threading.Thread.Sleep(10000);
                pa.ClickViewHistory();
                if (i == 0)
                {
                    pa.ValidateHistoryHeader(historydata, pageName);
                    historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "trackerhistoryvalue");
                }

                String historytabledata = Convert.ToDateTime(date.GetDate()[i]).ToString("dddd, MMM dd yyyy") + "|" + cardiovalue.ToString() + "|" + strengthvalue.ToString() + "|" + stretchvalue.ToString();
                historydata.ElementAt(0)[4] = historytabledata;

                pa.ValidateHistoryData(historydata, pageName);
                cardiovalue += 10;
                strengthvalue += 15;
                stretchvalue += 5;
            }
        }
        public void GoToPhysicalTracker()
        {
            NavigateToPATracker();
        }
        /// <summary>
        /// 1. Navigates to Physical Activity Tracker
        /// 2. Enters Values in Textboxes
        /// 3. Validate History
        /// </summary>
        /// <returns></returns>
        public void VerifyPhysicalActivityTracker()
        {
            InputAndUpdateTrackerData();          

        }
        public void NavigateToPhysicalActivityTracker()
        {
            NavigateToPATracker();
        }
    }
}
