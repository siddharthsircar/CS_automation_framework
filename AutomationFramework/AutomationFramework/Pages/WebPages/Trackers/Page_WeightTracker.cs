using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using AutomationFramework.Pages.WebPages.Trackers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages
{
    class Page_WeightTracker
    {
        string pageName;
        SoftAssertions softAssertions = null;

        string pts;

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_WeightTracker()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_WeightTracker(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }

        /// <summary>
        /// Method navigates user to weight tracker
        /// </summary>
        private void NavigateToWeightTracker()
        {
            SeleniumKeywords.Click(pageName, "weighttracker");
        }
        /// <summary>
        /// 1. Enters Data in the specified locators
        /// 2. Verifies History Header
        /// 3. Verifies first 3 values of Tracker in history
        /// </summary>
        /// <returns></returns>
        private void InputAndUpdateBlendedTrackerData()
        {
            List<string[]> trackerdata = CSVReaderDataTable.GetCSVData("InputDataContent", pageName, "weightdata");
            List<string[]> historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "blendedtrackerhistoryheader");

            Common date = new Common(softAssertions);
            date.VerifyDate();


            CommonTracker wt = new CommonTracker(softAssertions);
            int changevalueindex = 0;

            int weightvalue = Convert.ToInt32(trackerdata.ElementAt(0)[4]);
            for (int i = 0; i <= 2; i++)
            {
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(0)[3], weightvalue.ToString());
                JavaScriptKeywords.SetTextByControlId("DataPointDate", date.GetDate()[i]);

                wt.ClickUpdateButton();
                System.Threading.Thread.Sleep(3000);
                wt.ClickViewHistory();
                //System.Threading.Thread.Sleep(3000);
                if (i == 0)
                {
                    wt.ValidateHistoryHeader(historydata, pageName);
                    historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "blendedtrackerhistoryvalue");
                }

                String historytabledata = Convert.ToDateTime(date.GetDate()[i]).ToString("dddd, MMM dd yyyy")+"|"+ weightvalue.ToString()+ "|Manual Entry";
                historydata.ElementAt(changevalueindex)[4] = historytabledata;

                wt.ValidateHistoryData(historydata, pageName);
                weightvalue += 50;
            }
        }

        private void InputAndUpdateNonBlendedTrackerData()
        {
            List<string[]> trackerdata = CSVReaderDataTable.GetCSVData("InputDataContent", pageName, "weightdata");
            List<string[]> historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "nonblendedtrackerhistoryheader");

            Common date = new Common(softAssertions);
            date.VerifyDate();


            CommonTracker wt = new CommonTracker(softAssertions);
            int changevalueindex = 0;

            int weightvalue = Convert.ToInt32(trackerdata.ElementAt(0)[4]);
            for (int i = 0; i <= 2; i++)
            {
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(0)[3], weightvalue.ToString());
                JavaScriptKeywords.SetTextByControlId("DataPointDate", date.GetDate()[i]);
                wt.ClickUpdateButton();
                wt.ClickViewHistory();
                //System.Threading.Thread.Sleep(3000);
                if (i == 0)
                {
                    wt.ValidateHistoryHeader(historydata, pageName);
                    historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "nonblendedtrackerhistoryvalue");
                }

                String historytabledata = Convert.ToDateTime(date.GetDate()[i]).ToString("dddd, MMM dd yyyy") + "|" + weightvalue.ToString();
                historydata.ElementAt(changevalueindex)[4] = historytabledata;

                wt.ValidateHistoryData(historydata, pageName);
                weightvalue += 50;
            }
        }
        //public string GetPoints()
        //{
        //    pts = GetPointsValue();
        //    int index = pts.IndexOf("$");
        //    pts = pts.Substring(1, pts.Length-1);
        //    return pts;
        //}
        public void GoToWeightTracker()
        {
            NavigateToWeightTracker();
        }
        /// <summary>
        /// This method verifies weight tracker
        /// </summary>

        public void VerifyWeightTracker(string clientname)
        {
            
            if(clientname.ToLower().Equals("health trust") || clientname.ToLower().Equals("onlife health"))
                InputAndUpdateBlendedTrackerData();
            else
                InputAndUpdateNonBlendedTrackerData();
        }

    }
}
