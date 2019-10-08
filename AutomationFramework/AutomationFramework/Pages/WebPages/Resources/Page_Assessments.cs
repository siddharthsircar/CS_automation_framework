using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;

namespace AutomationFramework.Pages.WebPages
{
    public class Page_Assessments
    {
        String pageName;
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_Assessments()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        /// <summary>
        /// This method click on Health content option in left menu
        /// </summary>
        private void ClickOnAssessmentsInMenu()
        {
            SeleniumKeywords.Click(pageName, "leftmenu_resources_assessments_lnk");

        }
      
        private string GetReportsTabAriaExpandedValue()
        {
           return(SeleniumKeywords.GetAttributeValue(pageName, "Reports_tab","aria-expanded"));

        }
        
        public string VerifyAssessmentsPage()
        {
           // ClickOnReportsInMenu();
            Common cmn = new Common();
            string value = cmn.GetResourcesPageTxt("Common", "reports/assessments_page_text");
            Console.WriteLine("Value: " + value);
            return value;
        }
        public string VerifyAssessmentPageHeader()
        {
            ClickOnAssessmentsInMenu();
            Common cmn = new Common();
            string value = cmn.GetResourcesPageTxt("Page_Reports", "reportpage_header");
            Console.WriteLine("Value: " + value);
            return value;
        }
    }
}
