using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.Trackers
{
    class Page_NutritionTracker
    {
        string pageName;
        SoftAssertions softAssertions = null;

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_NutritionTracker()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_NutritionTracker(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }

        /// <summary>
        /// Clicks on Nutrition Tracker menu item in hamburger menu
        /// </summary>
        public void NavigateToNutritionTracker()
        {
            SeleniumKeywords.Click(pageName, "nutracker");
        }
        /// <summary>
        /// 1. Enters Data in the specified locators
        /// 2. Verifies History Header
        /// 3. Verifies first 3 values of Tracker in history
        /// </summary>
        /// <returns></returns>
        private void InputAndUpdateTrackerData()
        {
            List<string[]> trackerdata = CSVReaderDataTable.GetCSVData("InputDataContent", pageName, "nutdata");
            List<string[]> historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "trackerhistoryheader");
            Common date = new Common(softAssertions);
            CommonTracker nut = new CommonTracker(softAssertions);

            date.VerifyDate();

            int fruitvalue = Convert.ToInt32(trackerdata.ElementAt(0)[4]);
            int vegvalue = Convert.ToInt32(trackerdata.ElementAt(1)[4]);
            int dairyvalue = Convert.ToInt32(trackerdata.ElementAt(2)[4]);
            int grainsvalue = Convert.ToInt32(trackerdata.ElementAt(3)[4]);
            int proteinvalue = Convert.ToInt32(trackerdata.ElementAt(4)[4]);
            for (int i = 0; i <= 2; i++)
            {
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(0)[3], fruitvalue.ToString());
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(1)[3], vegvalue.ToString());
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(2)[3], dairyvalue.ToString());
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(3)[3], grainsvalue.ToString());
                SeleniumKeywords.SetText(pageName, trackerdata.ElementAt(4)[3], proteinvalue.ToString());
                JavaScriptKeywords.SetTextByControlId("DataPointDate", date.GetDate()[i]);
                nut.ClickUpdateButton();
                nut.ClickViewHistory();
                if (i == 0)
                {
                    nut.ValidateHistoryHeader(historydata, pageName);
                    historydata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "trackerhistoryvalue");
                }

                String historytabledata = Convert.ToDateTime(date.GetDate()[i]).ToString("dddd, MMM dd yyyy") + "|" + fruitvalue.ToString() + "|" + vegvalue.ToString() + "|" + dairyvalue.ToString() + "|" + grainsvalue.ToString() + "|" + proteinvalue.ToString();
                historydata.ElementAt(0)[4] = historytabledata;
                
                nut.ValidateHistoryData(historydata, pageName);
                fruitvalue += 5;
                vegvalue += 4;
                dairyvalue += 3;
                grainsvalue += 2;
                proteinvalue += 1;
            }
        }
        public void GoToNutritionTracker()
        {
            NavigateToNutritionTracker();

        }
        /// <summary>
        /// 1. Navigates to Physical Activity Tracker
        /// 2. Enters Values in Textboxes
        /// 3. Validate History
        /// </summary>
        /// <returns></returns>
        public void VerifyNutritionTracker()
        {
            InputAndUpdateTrackerData();           
        }
    }
}
