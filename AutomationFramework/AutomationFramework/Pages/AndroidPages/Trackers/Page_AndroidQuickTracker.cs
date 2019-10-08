using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.AndroidPages.Trackers
{
    [TestFixture]
    public class Page_AndroidQuickTracker
    {
        String pageName;
        SoftAssertions softAssertions = null;
        string pckgname = ConfigurationManager.AppSettings["apppackage"];


        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Mob.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_AndroidQuickTracker()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_AndroidQuickTracker(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }
        private void TapQuickTracker()
        {
            Thread.Sleep(2000);
            AppiumKeywords.Tap(pageName, "quicktracker");
        }
        public void ValidatePageUI(List<string[]> elements)
        {
            Thread.Sleep(3000);
            for (int i = 0; i < elements.Count(); i++)
            {
                string locatorname = elements.ElementAt(i)[3];
                string elementname = elements.ElementAt(i)[2];
                string action = elements.ElementAt(i)[4];
                if (action.ToLower().Equals("verifytext"))
                {
                    string expectedtxt = elements.ElementAt(i)[5];
                    string actual = AppiumKeywords.GetText(pageName, locatorname, pckgname);
                    softAssertions.Add(elementname, expectedtxt, actual, "contains");
                }
                else if (action.ToLower().Equals("verifyexists"))
                {
                    bool actualstatus = AppiumKeywords.IsElementPresent(pageName, locatorname, pckgname);
                    softAssertions.Add(elementname, true, actualstatus, "equals");
                }
            }
        }
                //public void clickplusbutton()
                //{
                //    AppiumKeywords.Tap(pageName, "quicktrackerplussign", pckgname);
                //}
                public void NavigateToQuickTrackerScreen()
                {
                    Common cmn = new Common();
                    cmn.TapFanMenu();
                    TapQuickTracker();
                    cmn.CloseHelpPopup();
                }

    }
}
