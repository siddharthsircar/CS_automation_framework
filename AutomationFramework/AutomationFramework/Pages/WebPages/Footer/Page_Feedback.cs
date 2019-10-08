using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages
{
    class Page_Feedback
    {
        String pageName;
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_Feedback()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        private void FillFeedbackForm()
        {
            SeleniumKeywords.Click(pageName, "contentexcellentrb");
            SeleniumKeywords.Click(pageName, "designexcellentrb");
            SeleniumKeywords.Click(pageName, "usabilityexcellentrb");
            SeleniumKeywords.Click(pageName, "overallexcellentrb");
            SeleniumKeywords.SetText(pageName, "commenttxtarea", "Performing Automated Test");
            SeleniumKeywords.Click(pageName, "submitbtn");
            SeleniumKeywords.Click(pageName, "closebtn");
        }
        private List<string[]> VerifyFormContent()
        {
            System.Threading.Thread.Sleep(4000);
            List<string[]> ExpectedText = new List<string[]>();
            ExpectedText = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "headers");
            Console.WriteLine("ExpectedText length : " + ExpectedText.Count);
            List<string[]> result = new List<string[]>();
            for (int i = 0; i < ExpectedText.Count; i++)
            {
                string elementname = ExpectedText.ElementAt(i)[2];
                string locatorname = ExpectedText.ElementAt(i)[3];
                string expectedtext = ExpectedText.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, locatorname);
                bool textmatch = SeleniumKeywords.VerifyText(actualtext, expectedtext);
                string msg = "Element : " + elementname + " , Expected : " + expectedtext + " , Actual : " + actualtext;
                result.Add(new string[] { msg, textmatch.ToString() });
            }
            List<string[]> options = new List<string[]>();
            options = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "labels");
            for (int i = 0; i < options.Count; i++)
            {
                string elementname = options.ElementAt(i)[2];
                string locatorname = options.ElementAt(i)[3];
                bool status = SeleniumKeywords.IsElementPresent(pageName, locatorname);
                string msg = "Element : " + elementname + " not displayed";
                result.Add(new string[] { msg, status.ToString() });
            }
            return result;
        }
        public List<string[]> VerifyFeedBackForm()
        {
            List<string[]> result = VerifyFormContent();
            FillFeedbackForm();
            return result;
        }
    }
}
