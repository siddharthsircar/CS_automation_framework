using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationFramework.Pages.WebPages.Trackers
{
    class Page_StressTracker
    {
        string pageName;
        SoftAssertions softAssertions = null;

        public Page_StressTracker()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_StressTracker(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }
        /// <summary>
        /// Clicks on Stress Tracker menu item in hamburger menu
        /// </summary>
        public void NavigateToStressTracker()
        {
            SeleniumKeywords.Click(pageName, "menu_stresstracker");
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
            List<string[]> trackerdata = CSVReaderDataTable.GetCSVData("InputDataContent", pageName, "stressdata");
            List<string[]> historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "trackerhistoryheader");
            Common date = new Common(softAssertions);
            CommonTracker stress = new CommonTracker(softAssertions);

            //date.VerifyTimeAndDate();

            int changevalueindex = 0;


            for (int i = 0; i < 3; i++)
            {
                SeleniumKeywords.RefreshPage();
                int widthmultiple = 1;
                switch (trackerdata.ElementAt(i)[4])
                {
                    case "Barely any or none":
                        widthmultiple = 0;
                        break;
                    case "A little bit":
                        widthmultiple = 1;
                        break;
                    case "Some":
                        widthmultiple = 2;
                        break;
                    case "Quite a bit":
                        widthmultiple = 3;
                        break;
                    case "A lot":
                        widthmultiple = 4;
                        break;

                }
                System.Threading.Thread.Sleep(10000);
                SeleniumKeywords.MoveAndClickAtSpecificPosition(pageName, trackerdata.ElementAt(i)[3], 4, widthmultiple, 1, 1);
                System.Threading.Thread.Sleep(3000);
                string inputdatetime = date.GetTrackerDateTime()[i];
                Console.WriteLine("InputDateTime : " + inputdatetime);
                JavaScriptKeywords.SetTextByControlId("DataPointDate", inputdatetime);
                stress.ClickUpdateButton();
                //System.Threading.Thread.Sleep(3000);
                stress.ClickViewHistory();
                if (i == 0)
                {
                    stress.ValidateHistoryHeader(historydata, pageName);
                    historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "trackerhistoryvalue");
                }

                String historytabledata = Convert.ToDateTime(inputdatetime).ToString("dddd, MMM dd yyyy hh:mm tt") + "|" + trackerdata.ElementAt(i)[4];

                //historydata.ElementAt(changevalueindex)[4] = Convert.ToDateTime(date.GetDate()[i]).ToString("dddd, MMM dd yyyy HH:mm tt");
                historydata.ElementAt(0)[4] = historytabledata;
                stress.ValidateHistoryData(historydata, pageName);

            }
        }
        public void GoToStressTracker()
        {
            NavigateToStressTracker();
        }
        /// <summary>
        /// 1. Navigates to Stress Tracker
        /// 2. Enters Values in Stress Textboxes
        /// 3. Validate History
        /// </summary>
        /// <returns></returns>
        public void VerifyStressTracker()
        {
            InputAndUpdateTrackerData();
        }
    }
}
