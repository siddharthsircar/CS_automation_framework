using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;

namespace AutomationFramework.Pages.WebPages
{
    public class Page_PHR
    {
        String pageName;
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_PHR()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        
        /// <summary>
        /// This method click on Health content option in left menu
        /// </summary>
        private void ClickOnPHRInMenu()
        {
            SeleniumKeywords.Click(pageName, "leftmenu_resources_phr_lnk");

        }
        
        /// <summary>
        /// Method click on Medical Information of PHI information
        /// </summary>
        public void ClickOnMedicalInformation()
        {
            SeleniumKeywords.Click(pageName, "phr_headlink_MedicalInformation");

        }

        /// <summary>
        /// Call by Test case Verify the PHI information is loaded
        /// </summary>
        /// <returns></returns>
        public string VerifyPHRPage()
        {
           // ClickOnPHRInMenu();
            Common cmn = new Common();
            SeleniumKeywords.NavigateToIFrame("PHRFrame");
            ClickOnMedicalInformation();
            string value = cmn.GetResourcesPageTxt(pageName, "phr_controldiv_MedicalInformation");
            Console.WriteLine("Value: " + value);
            SeleniumKeywords.NavigateToDefaultContent();
            return value;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string VerifyPHRPageHeader()
        {
            ClickOnPHRInMenu();
            Common cmn = new Common();
            string value = cmn.GetResourcesPageTxt(pageName, "phr_page_header");
            Console.WriteLine("Value: " + value);
            return value;
        }
    }
}
