using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages
{
    class Page_TermsOfService
    {
        String currentClassName;
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_TermsOfService()
        {
            currentClassName = this.GetType().Name;
            Console.WriteLine("Current class : " + currentClassName);
        }

        public List<string[]> VerifyTosPage()
        {
            List<string[]> ExpectedText = new List<string[]>();
            System.Threading.Thread.Sleep(4000);
            ExpectedText = CSVReaderDataTable.GetCSVData("CommonContent", currentClassName, "headers");
            Console.WriteLine("ExpectedText length : " + ExpectedText.Count);
            List<string[]> result = new List<string[]>();
            for (int i = 0; i < ExpectedText.Count; i++)
            {
                string elementname = ExpectedText.ElementAt(i)[2];
                string locatorname = ExpectedText.ElementAt(i)[3];
                string expectedtext = ExpectedText.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(currentClassName, locatorname);
                bool textmatch = SeleniumKeywords.VerifyText(actualtext, expectedtext);
                string msg = "Element : " + elementname + " , Expected : " + expectedtext + " , Actual : " + actualtext;
                result.Add(new string[] { msg, textmatch.ToString() });
            }
            SeleniumKeywords.NavigateToPreviousPage();
            return result;
        }
    }
}
