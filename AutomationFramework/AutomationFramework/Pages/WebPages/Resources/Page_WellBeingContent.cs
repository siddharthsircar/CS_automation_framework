using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;

namespace AutomationFramework.Pages.WebPages
{
    public class Page_WellBeingContent
    {
        String pageName;
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_WellBeingContent()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        /// <summary>
        /// This method click on Health content option in left menu
        /// </summary>
        private void ClickOnWellBeingContentInMenu()
        {
            SeleniumKeywords.Click(pageName, "leftmenu_resources_wellbeingcontent_lnk");

        }
        private void ClickOnWellBeingContentTab()
        {
            SeleniumKeywords.NavigateToDefaultContent();
            SeleniumKeywords.Click(pageName, "wellbeingcontent_tab");

        }
      
       
        public string VerifyWellBeingContentPage()
        {
            ClickOnWellBeingContentInMenu();
            //SeleniumKeywords.NavigateToIFrame("iframeSymptomChecker");
            Common cmn = new Common();
            string value = cmn.GetResourcesPageTxt(pageName, "wellbeing_page_text");
            Console.WriteLine("Value: " + value);
            return value;
        }
        public string VerifyWellBeingFromTab()
        {
            ClickOnWellBeingContentTab();

            Common cmn = new Common();
            string value = cmn.GetResourcesPageTxt(pageName, "wellbeing_page_text");
            Console.WriteLine("Value: " + value);
            return value;
        }

    }
}
