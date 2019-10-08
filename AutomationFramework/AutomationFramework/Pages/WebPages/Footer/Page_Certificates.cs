using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.Footer
{
    class Page_Certificates
    {

        String pageName;
        SoftAssertions softAssertions = null;
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_Certificates()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        public Page_Certificates(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }
        /// <summary>
         /// call by TC_VerifyCertificate
         /// </summary>
        public void verifyCertificatePage()
        {
            Thread.Sleep(3000);
            Boolean result;
            SeleniumKeywords.Click("Common", "fcertificatelnk");
            result = SeleniumKeywords.IsElementPresent(pageName, "certificates_header");
            softAssertions.Add("Element : Certificate Page Header", true, result, "equals");
            result = SeleniumKeywords.IsElementPresent(pageName, "certificates_header");
            softAssertions.Add("Element : Certificate Page Text", true, result, "equals");
        }

    }
}
