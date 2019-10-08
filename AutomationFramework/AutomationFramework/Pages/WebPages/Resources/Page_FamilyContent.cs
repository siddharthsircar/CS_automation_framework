using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;

namespace AutomationFramework.Pages.WebPages
{
    public class Page_FamilyContent
    {
        String pageName;
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_FamilyContent()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        /// <summary>
        /// This method click on Health content option in left menu
        /// </summary>
        private void ClickOnFamilyContentInMenu()
        {
            SeleniumKeywords.Click(pageName, "leftmenu_resources_FamilyContent_lnk");

        }/// <summary>
        /// click on famility content tab
        /// </summary>
        private void ClickOnFamilyContentTab()
        {
            SeleniumKeywords.NavigateToDefaultContent();
            SeleniumKeywords.Click(pageName, "familycontent_tab");

        }
      /// <summary>
      /// verify family content page form left menu
      /// </summary>
      /// <returns></returns>
     
        public string VerifyFamilyContentPage()
        {
            ClickOnFamilyContentInMenu();
            //SeleniumKeywords.NavigateToIFrame("iframeSymptomChecker");
            Common cmn = new Common();
            string value = cmn.GetResourcesPageTxt(pageName, "family_page_text");
            Console.WriteLine("Value: " + value);
            return value;
        }
        /// <summary>
        /// Verify family content form family content tab
        /// </summary>
        /// <returns></returns>
        public string VerifyFamilyContentFromTab()
        {
            ClickOnFamilyContentTab();
            //SeleniumKeywords.NavigateToIFrame("iframeSymptomChecker");
            Common cmn = new Common();
            string value = cmn.GetResourcesPageTxt(pageName, "family_page_text");
            Console.WriteLine("Value: " + value);
            return value;
        }
    }
}
