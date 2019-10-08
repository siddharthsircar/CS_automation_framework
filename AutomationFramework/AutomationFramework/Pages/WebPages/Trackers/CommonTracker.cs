using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.Trackers
{
    class CommonTracker
    {
        String pageName;
        //public static SoftAssertions softassertions;
        SoftAssertions softAssertions = null;

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public CommonTracker()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public CommonTracker(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }

        /// <summary>
        /// This method is use to click on update Button on tracker page
        /// </summary>
        public void ClickUpdateButton()
        {
            //SeKeywords.MoveToElement(ElementLocator.GetLocator(pageName, "updatebtn"));
            Thread.Sleep(4000);
            SeleniumKeywords.Click(pageName, "updatebtn");
            Thread.Sleep(4000);
        }
        
        /// <summary>
        /// This method is used to click view History Btn
        /// </summary>
         public void ClickViewHistory()
        {
            Thread.Sleep(5000);
            SeleniumKeywords.Click(pageName, "historybtn");
        }

        /// <summary>
        /// This method validates the tracker history headers
        /// </summary>
        /// <param name="historydata"></param>
        /// <returns></returns>
        public void ValidateHistoryHeader(List<string[]> historydata,string pname)
        {
            string pagename = pageName;
            //ClickViewHistory();
            System.Threading.Thread.Sleep(2000);
            for (int i = 0; i < historydata.Count; i++)
            {
                string elementname = historydata.ElementAt(i)[2];
                string elementlocatorname = historydata.ElementAt(i)[3];
                string expectedtext = historydata.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pagename, elementlocatorname);
                if (i == 2)
                {
                    pagename = pname;
                }
                softAssertions.Add("Element : " + elementname, expectedtext, actualtext, "contains");               
            }
        }

        /// <summary>
        /// This method validates tracker data in tracker history
        /// </summary>
        /// <param name="historydata"></param>
        /// <returns></returns>
        public void ValidateHistoryData(List<string[]> historydata,string pname)
        {
            //ClickViewHistory();
            string pagename = pageName;
            for (int i = 0; i < historydata.Count; i++)
            {                
                string elementname = historydata.ElementAt(i)[2];
                string elementlocatorname = historydata.ElementAt(i)[3];
                string variablevalue = historydata.ElementAt(i)[4];
                string[] varinputvalue = variablevalue.Split('|');
                Thread.Sleep(2000);
                bool historydatapresent = SeleniumKeywords.IsElementPresent(pname, elementlocatorname, varinputvalue);
                
                softAssertions.Add("Element : " + elementname, true, historydatapresent, "equals");
            }
        }

        public void ValidateTrackerLatestValues(List<string[]> latestvaluedata)
        {
            string pagename = pageName;
            for (int i = 0; i < latestvaluedata.Count; i++)
            {
                string elementname = latestvaluedata.ElementAt(i)[2];
                string elementlocatorname = latestvaluedata.ElementAt(i)[3];
                string expectedvalue = latestvaluedata.ElementAt(i)[4];
                string variablevalue = latestvaluedata.ElementAt(i)[5];
                //Thread.Sleep(1000);
                string actualvalue = "";
                if (variablevalue.Equals("novalue"))
                {
                    JavaScriptKeywords.ScrollToAnElement("CommonTracker", elementlocatorname);
                    actualvalue = SeleniumKeywords.GetText("CommonTracker", elementlocatorname);
                }
                else
                {
                    JavaScriptKeywords.ScrollToAnElement("CommonTracker", elementlocatorname, variablevalue);
                    actualvalue = SeleniumKeywords.GetText("CommonTracker", elementlocatorname, variablevalue);
                }
                    

                Console.WriteLine("elementname : "+ elementname);
                Console.WriteLine("expectedvalue : " + expectedvalue);
                Console.WriteLine("variablevalue : " + variablevalue);
                Console.WriteLine("actualvalue : " + actualvalue);
                softAssertions.Add("Element : " + elementname, expectedvalue, actualvalue, "contains");
            }
        }

        /// <summary>
        /// This method is used to validate existance of chart
        /// </summary>
        /// <returns></returns>
        private void ValidateChartExistence()
        {
            bool chartvisible = SeleniumKeywords.IsElementPresent(pageName, "chartcontainer");
            softAssertions.Add("Tracker Chart", true, chartvisible, "equals");
        }

        private void ValidateChartFilters()
        {            
            List<string[]> chartfilterdata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "trackerchartfilters");
            for (int i = 0; i < chartfilterdata.Count; i++)
            {
                string elementname = chartfilterdata.ElementAt(i)[2];
                string elementlocatorname = chartfilterdata.ElementAt(i)[3];
                string expectedtext = chartfilterdata.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                softAssertions.Add("Element : " + elementname, expectedtext, actualtext, "contains");               
            }
        }
        public void VerifyTrackerChart()
        {

            ValidateChartExistence();
            ValidateChartFilters();
            SeleniumKeywords.RefreshPage();
            Thread.Sleep(2000);
        }



    }
}
