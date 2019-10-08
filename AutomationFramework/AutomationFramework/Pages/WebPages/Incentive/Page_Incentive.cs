 using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.Incentive
{
    class Page_Incentive
    {
        String pageName;
        SoftAssertions softAssertions = null;
        public Page_Incentive()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_Incentive(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }

        private void NavigateToHistoryPage()
        {
            System.Threading.Thread.Sleep(2000);
            SeleniumKeywords.Click("Common", "historylink");
        }

        private void ValidateIncentiveHistoryUiData()
        {
            List<string[]> historyuidata = new List<string[]>();
            historyuidata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "incentivehistoryuidata");
            for (int i = 0; i < historyuidata.Count; i++)
            {
                string elementname = historyuidata.ElementAt(i)[2];
                string elementlocatorname = historyuidata.ElementAt(i)[3];
                string expectedtext = historyuidata.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                
                softAssertions.Add("Element : " + elementlocatorname, expectedtext, actualtext, "contains");
            }
        }

        private void ValidateIncentiveHistoryDate()
        {
            List<string[]> historydate = new List<string[]>();
            historydate = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "incentivehistorydate");
            List<string> expdate_data = new List<string>();
            Common cmn = new Common();
            expdate_data.Add(cmn.GetCurrentYear());
            expdate_data.Add(cmn.GetCurrentMonth());
            expdate_data.Add(cmn.GetCurrentDay());
            for (int i = 0; i < historydate.Count; i++)
            {
                string elementname = historydate.ElementAt(i)[2];
                string elementlocatorname = historydate.ElementAt(i)[3];
                string expectedtext = expdate_data.ElementAt(i);
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);

                softAssertions.Add("Element : " + elementlocatorname, expectedtext, actualtext, "equals");
            }
        }


        private void ValidateIncentiveHistoryActivities()
        {
            List<string[]> historyuidata = new List<string[]>();
            historyuidata = CSVReaderDataTable.GetCSVData("IncentiveHistoryActivities", pageName, "incentivehistoryitems",GlobalVariables.clientname.ToLower());
            for (int i = 0; i < historyuidata.Count; i++)
            {
                string elementname = historyuidata.ElementAt(i)[3];
                string elementlocatorname = historyuidata.ElementAt(i)[4];
                bool expelementpresence = Convert.ToBoolean(historyuidata.ElementAt(i)[5]);
                string varinputvalue1 = historyuidata.ElementAt(i)[6];
                string varinputvalue2 = historyuidata.ElementAt(i)[7];
                string varinputvalue3 = historyuidata.ElementAt(i)[8];
                string varinputvalue4 = historyuidata.ElementAt(i)[9];
                bool actualelementpresence = SeleniumKeywords.IsElementPresent(pageName, elementlocatorname,varinputvalue1,varinputvalue2,varinputvalue3,varinputvalue4);
                
                softAssertions.Add("Element : " + elementlocatorname, expelementpresence, actualelementpresence, "equals");
            }
        }

        public void ValidateHistoryData()
        {
            NavigateToHistoryPage();
            if (GlobalVariables.clientname.ToLower().ToString().Equals("health trust"))
            {
               
                ValidateIncentiveHistoryUiData();
                ValidateIncentiveHistoryDate();
                ValidateIncentiveHistoryActivities();
            }
            else
            {
                ValidateIncentiveHistoryDate();
                ValidateIncentiveHistoryActivities();
            }
        }

        public void ValidateEligibleActivities()
        {
            NavigateToEligibleActivitiesPage();
            ValidateNotCompletedWithActivityBtn();
            ValidateNotCompletedWithoutActivityBtn();
            ValidateCompletedWithActivityBtn();
            ValidateCompletedWithoutActivityBtn();
        }

        private void NavigateToEligibleActivitiesPage()
        {
            System.Threading.Thread.Sleep(3000);
            SeleniumKeywords.Click("Common", "earnlink");
        }
        private void ValidateNotCompletedWithActivityBtn()
        {
            List<string[]> activitiesdata = new List<string[]>();
            activitiesdata = CSVReaderDataTable.GetCSVData("IncentiveEligibleActivities", pageName, "eligibleactivities_notcompletedwithactivitybtn");
            for (int i = 0; i < activitiesdata.Count; i++)
            {
                string elementname = activitiesdata.ElementAt(i)[2];
                string elementlocatorname = activitiesdata.ElementAt(i)[3];
                Boolean expelementdisplayedstatus = Convert.ToBoolean(activitiesdata.ElementAt(i)[4]);

                string inputvalue1 = activitiesdata.ElementAt(i)[5];
                string inputvalue2 = activitiesdata.ElementAt(i)[6];
                string inputvalue3 = activitiesdata.ElementAt(i)[7];
                string inputvalue4 = activitiesdata.ElementAt(i)[8];
                string inputvalue5 = activitiesdata.ElementAt(i)[9];

                Boolean actualelementdisplayedstatus = SeleniumKeywords.IsElementPresent(pageName, elementlocatorname,inputvalue1,inputvalue2,inputvalue3,inputvalue4,inputvalue5);

                softAssertions.Add("Element : " + elementname, expelementdisplayedstatus, actualelementdisplayedstatus, "equals");
            }
        }

        private void ValidateNotCompletedWithoutActivityBtn()
        {
            List<string[]> activitiesdata = new List<string[]>();
            activitiesdata = CSVReaderDataTable.GetCSVData("IncentiveEligibleActivities", pageName, "eligibleactivities_notcompletedwithoutactivitybtn");
            for (int i = 0; i < activitiesdata.Count; i++)
            {
                string elementname = activitiesdata.ElementAt(i)[2];
                string elementlocatorname = activitiesdata.ElementAt(i)[3];
                Boolean expelementdisplayedstatus = Convert.ToBoolean(activitiesdata.ElementAt(i)[4]);

                string inputvalue1 = activitiesdata.ElementAt(i)[5];
                string inputvalue2 = activitiesdata.ElementAt(i)[6];
                string inputvalue3 = activitiesdata.ElementAt(i)[7];
                string inputvalue4 = activitiesdata.ElementAt(i)[8];
                
                Boolean actualelementdisplayedstatus = SeleniumKeywords.IsElementPresent(pageName, elementlocatorname, inputvalue1, inputvalue2, inputvalue3, inputvalue4);

                softAssertions.Add("Element : " + elementname, expelementdisplayedstatus, actualelementdisplayedstatus, "equals");
            }
        }

        private void ValidateCompletedWithActivityBtn()
        {
            List<string[]> activitiesdata = new List<string[]>();
            activitiesdata = CSVReaderDataTable.GetCSVData("IncentiveEligibleActivities", pageName, "eligibleactivities_completedwithactivitybtn");
            for (int i = 0; i < activitiesdata.Count; i++)
            {
                string elementname = activitiesdata.ElementAt(i)[2];
                string elementlocatorname = activitiesdata.ElementAt(i)[3];
                Boolean expelementdisplayedstatus = Convert.ToBoolean(activitiesdata.ElementAt(i)[4]);

                string inputvalue1 = activitiesdata.ElementAt(i)[5];
                string inputvalue2 = activitiesdata.ElementAt(i)[6];
                string inputvalue3 = activitiesdata.ElementAt(i)[7];
                string inputvalue4 = activitiesdata.ElementAt(i)[8];
                string inputvalue5 = activitiesdata.ElementAt(i)[9];

                Boolean actualelementdisplayedstatus = SeleniumKeywords.IsElementPresent(pageName, elementlocatorname, inputvalue1, inputvalue2, inputvalue3, inputvalue4, inputvalue5);

                softAssertions.Add("Element : " + elementname, expelementdisplayedstatus, actualelementdisplayedstatus, "equals");
            }
        }

        private void ValidateCompletedWithoutActivityBtn()
        {
            List<string[]> activitiesdata = new List<string[]>();
            activitiesdata = CSVReaderDataTable.GetCSVData("IncentiveEligibleActivities", pageName, "eligibleactivities_completedwithoutactivitybtn");
            for (int i = 0; i < activitiesdata.Count; i++)
            {
                string elementname = activitiesdata.ElementAt(i)[2];
                string elementlocatorname = activitiesdata.ElementAt(i)[3];
                Boolean expelementdisplayedstatus = Convert.ToBoolean(activitiesdata.ElementAt(i)[4]);

                string inputvalue1 = activitiesdata.ElementAt(i)[5];
                string inputvalue2 = activitiesdata.ElementAt(i)[6];
                string inputvalue3 = activitiesdata.ElementAt(i)[7];
                string inputvalue4 = activitiesdata.ElementAt(i)[8];
                
                Boolean actualelementdisplayedstatus = SeleniumKeywords.IsElementPresent(pageName, elementlocatorname, inputvalue1, inputvalue2, inputvalue3, inputvalue4);

                softAssertions.Add("Element : " + elementname, expelementdisplayedstatus, actualelementdisplayedstatus, "equals");
            }
        }
    }
}
