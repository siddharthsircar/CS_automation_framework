using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;

namespace AutomationFramework.Pages.WebPages
{
    /// <summary>
    /// Health Content page class
    /// </summary>
    public class Page_HealthContent
    {
        String pageName;
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_HealthContent()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        /// <summary>
        /// This method click on Health content option in left menu
        /// </summary>
        public void ClickOnHealthContentInMenu()
        {
            SeleniumKeywords.Click(pageName, "leftmenu_resources_healthcontent_lnk");

        }
        private void ClickOnHealthContentTab()
        {
            SeleniumKeywords.Click(pageName, "healthcontent_tab");

        }
        private string GetHealthContentTabAriaExpandedValue()
        {
           return(SeleniumKeywords.GetAttributeValue(pageName, "healthcontent_tab","aria-expanded"));

        }
     
        /// <summary>
        /// Method to verify Health Content page
        /// </summary>
        /// <returns></returns>
        public string VerifyHealthContentPage()
        {
            ClickOnHealthContentInMenu();
            SeleniumKeywords.NavigateToIFrame("iframeHealthArticle");
            Common cmn = new Common();
            string value = cmn.GetResourcesPageTxt(pageName, "healthcontent_page_text");
            Console.WriteLine("Value: " + value);
            return value;
        }
    }
}
