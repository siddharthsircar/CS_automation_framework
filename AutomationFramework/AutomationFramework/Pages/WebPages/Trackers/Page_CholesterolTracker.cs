using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.Trackers
{
    class Page_CholesterolTracker
    {
        string pageName;
        SoftAssertions softAssertions = null;

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_CholesterolTracker()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        public Page_CholesterolTracker(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }
        /// <summary>
        /// Clicks on Cholesterol Tracker menu item in hamburger menu
        /// </summary>
        public void NavigateToCholTracker()
        {
            SeleniumKeywords.Click(pageName, "choltracker");
        }
        /// <summary>
        /// 1. Enters Data in the specified locators
        /// 2. Verifies History Header
        /// 3. Verifies first 3 values of Tracker in history
        /// </summary>
        /// <returns></returns>
        private void InputAndUpdateTrackerData()
        {
            List<string[]> trackerdata = CSVReaderDataTable.GetCSVData("InputDataContent", pageName, "choldata");
            List<string[]> historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "trackerhistoryheader");
            Common date = new Common(softAssertions);
            CommonTracker chol = new CommonTracker(softAssertions);

            date.VerifyDate();

            int totcholvalue = Convert.ToInt32(trackerdata.ElementAt(0)[4]);
            int ldlvalue = Convert.ToInt32(trackerdata.ElementAt(1)[4]);
            int hdlvalue = Convert.ToInt32(trackerdata.ElementAt(2)[4]);
            int trygvalue = Convert.ToInt32(trackerdata.ElementAt(3)[4]);
            for (int i = 0; i <= 2; i++)
            {
                SeleniumKeywords.RefreshPage();
                System.Threading.Thread.Sleep(3000);
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(0)[3], totcholvalue.ToString());
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(1)[3], ldlvalue.ToString());
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(2)[3], hdlvalue.ToString());
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(3)[3], trygvalue.ToString());
                JavaScriptKeywords.SetTextByControlId("DataPointDate", date.GetDate()[i]);
                chol.ClickUpdateButton();
                chol.ClickViewHistory();
                if (i == 0)
                {
                    chol.ValidateHistoryHeader(historydata, pageName);
                    historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "trackerhistoryvalue");
                }

                String historytabledata = Convert.ToDateTime(date.GetDate()[i]).ToString("dddd, MMM dd yyyy") + "|"+totcholvalue.ToString() + "|"+ldlvalue.ToString() + "|"+ hdlvalue.ToString() + "|"+ trygvalue.ToString();
                historydata.ElementAt(0)[4] = historytabledata;

                chol.ValidateHistoryData(historydata, pageName);
                totcholvalue += 50;
                ldlvalue += 30;
                hdlvalue += 20;
                trygvalue += -10;
            }
        }
        public void GoToCholesterolTracker()
        {
            NavigateToCholTracker();
        }
        /// <summary>
        /// 1. Navigates to Tobacco Tracker
        /// 2. Enters Values in Textboxes
        /// 3. Validate History
        /// </summary>
        /// <returns></returns>
        public void VerifyCholesterolTracker()
        {
            InputAndUpdateTrackerData();            
        }
    }
}
