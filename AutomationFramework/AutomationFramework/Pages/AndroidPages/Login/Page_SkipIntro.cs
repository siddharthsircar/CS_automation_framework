using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Configuration;

namespace AutomationFramework.Pages.AndroidPages.Login
{
    class Page_SkipIntro
    {
        String pageName;
        SoftAssertions softAssertions = null;
        string pckgname = ConfigurationManager.AppSettings["apppackage"];

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Mob.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_SkipIntro()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_SkipIntro(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }

        /// <summary>
        /// Verify Skip intro UI elements present
        /// </summary>
        /// <returns></returns>
        public void VerifyElements()
        {
            List<string[]> options = new List<string[]>();
            List<string[]> result = new List<string[]>();
            options = CSVReaderDataTable.GetCSVData("MobileLoginUI", pageName, "elements");
            for (int i = 0; i < options.Count; i++)
            {
                string elementname = options.ElementAt(i)[2];
                string locatorname = options.ElementAt(i)[3];
                bool status = AppiumKeywords.IsElementPresent(pageName, locatorname,pckgname);
                softAssertions.Add(" Element : " + elementname, true, status, "equals");
            }
        }

        /// <summary>
        /// Click Skip Intro button
        /// </summary>
        public void ClickSkipIntro()
        {
            AppiumKeywords.Tap(pageName, "skipintrolnk", pckgname);
            Thread.Sleep(2000);
        }

    }
}
