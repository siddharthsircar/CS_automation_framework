using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages
{
    class Page_HIPAA
    {
        String pageName;
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_HIPAA()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Boolean VerifyContentSectionPresent()
        {
            bool status = SeleniumKeywords.IsElementPresent(pageName, "contentsection");
            SeleniumKeywords.NavigateToPreviousPage();
            return status;
        }
        public List<string[]> VerifyHeader()
        {
            List<string[]> ExpectedText = new List<string[]>();
            ExpectedText = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "header");
            Console.WriteLine("ExpectedText length : " + ExpectedText.Count);
            List<string[]> result = new List<string[]>();
            for (int i = 0; i < ExpectedText.Count; i++)
            {
                string elementname = ExpectedText.ElementAt(i)[2];
                string locatorname = ExpectedText.ElementAt(i)[3];
                string expectedtext = ExpectedText.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, locatorname);
                bool textmatch = SeleniumKeywords.VerifyTextContains(actualtext, expectedtext);
                string msg = "Element : " + elementname + " , Expected : " + expectedtext + " , Actual : " + actualtext;
                result.Add(new string[] { msg, textmatch.ToString() });
            }
            return result;
        }
    }
}
