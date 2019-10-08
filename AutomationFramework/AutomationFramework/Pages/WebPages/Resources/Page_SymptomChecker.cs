using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;

namespace AutomationFramework.Pages.WebPages
{
    public class Page_SymptomChecker
    {
        String pageName;
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_SymptomChecker()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        /// <summary>
        /// This method click on Health content option in left menu
        /// </summary>
        private void ClickOnSymptomCheckerInMenu()
        {
            SeleniumKeywords.Click(pageName, "leftmenu_resources_symptomchecker_lnk");

        }
        private void ClickOnHealthContentTab()
        {
            SeleniumKeywords.Click(pageName, "healthcontent_tab");

        }
        private string GetHealthContentTabAriaExpandedValue()
        {
           return(SeleniumKeywords.GetAttributeValue(pageName, "healthcontent_tab","aria-expanded"));

        }
        public void ClickOnSymptomCheckerTab()
        {

            SeleniumKeywords.NavigateToDefaultContent();
            SeleniumKeywords.Click(pageName, "symptomchecker_tab");
        }
     
        public string VerifySymptomCheckerPage()
        {
            ClickOnSymptomCheckerInMenu();
            SeleniumKeywords.NavigateToIFrame("iframeSymptomChecker");
            Common cmn = new Common();
            string value = VerifySymptomCheckerPageheading();
            SeleniumKeywords.NavigateToDefaultContent();
            Console.WriteLine("Value: " + value);
            return value;
        }

        public string VerifySymptomCheckerPageheading()
        {
            //SeleniumKeywords.NavigateToIFrame("iframeSymptomChecker");
            Common cmn = new Common();
            string value = cmn.GetResourcesPageTxt("Page_SymptomChecker", "symptomcheckert_page_text");
            Console.WriteLine("Value: " + value);
            return value;
        }

        public string VerifySymptomCheckerPageFromTab()
        {
            ClickOnSymptomCheckerTab();
            System.Threading.Thread.Sleep(10000);
            SeleniumKeywords.NavigateToIFrame("iframeSymptomChecker");
            System.Threading.Thread.Sleep(5000);
            Common cmn = new Common();
            string value = VerifySymptomCheckerPageheading();
            Console.WriteLine("Value: " + value);
            return value;
        }

    }
}
