using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages
{
    class Page_TrackerLearnMore
    {
        String pageName;
         SoftAssertions softAssertion = null;
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_TrackerLearnMore()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_TrackerLearnMore(SoftAssertions softAssertion) : this()
        {
            this.softAssertion = softAssertion;
        }
        public List<string[]> LearnMorePage()
        {
            List<string[]> tiles = new List<string[]>();
            tiles = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "learnmore");
            Console.WriteLine("ExpectedText length : " + tiles.Count);
            List<string[]> result = new List<string[]>();
            for (int i = 0; i < tiles.Count; i++)
            {
                string elementname = tiles.ElementAt(i)[2];
                string locatorname = tiles.ElementAt(i)[3];
                //string expectedtext = ExpectedText.ElementAt(i)[4];
                //string actualtext = SeKeywords.GetText(ElementLocator.GetLocator(pageName, locatorname), elementname);
                //bool textmatch = SeKeywords.VerifyText(actualtext, expectedtext);
                //string msg = "Element : " + elementname + " , Expected : " + expectedtext + " , Actual : " + actualtext;
                bool status = SeleniumKeywords.IsElementPresent(pageName, locatorname);
                string msg = "Element : " + elementname + "not found";
                result.Add(new string[] { msg, status.ToString() });
            }
            SeleniumKeywords.NavigateToPreviousPage();
            return result;
        }
        private Boolean RecommandedTrackerAfterHA(String trackername)
        {
            return(SeleniumKeywords.IsElementPresent(pageName, "tracker_recommended",trackername));
        }

        private Boolean NotRecommandedTrackerAfterHA(String trackername)
        {
            return (SeleniumKeywords.IsElementNotPresent(pageName, "tracker_recommended", trackername));
        }
        public void VerifyRecommandedTrackers(String trackername)
        {
           Boolean result= RecommandedTrackerAfterHA(trackername);
           softAssertion.Add("Recommanded Trackers",true, result, "contains");
        }

        public void VerifyNotRecommandedTrackers(String trackername)
        {
            Boolean result = NotRecommandedTrackerAfterHA(trackername);
            softAssertion.Add("Not Recommanded Trackers", false, result, "contains");
        }
    }
}
