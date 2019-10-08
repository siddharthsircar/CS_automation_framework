using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace AutomationFramework.Pages.WebPages.Notifications
{
    class Page_Notifications:Base
    {
        String pageName;
        List<string[]> notificationdata = new List<string[]>();

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_Notifications()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        
        private List<string[]> VerifyNotifications()
        {
            List<string[]> result = new List<string[]>();
            notificationdata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "msg");
           
            for (int i = 0; i < notificationdata.Count; i++)
            {
                string elementname = notificationdata.ElementAt(i)[2];
                string elementlocatorname = notificationdata.ElementAt(i)[3];
                string expectedtext = notificationdata.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                bool textmatch = actualtext.Trim().Contains(expectedtext.Trim());
                string msg = "Element: " + elementname + " , Expected: " + expectedtext + " , Actual: " + actualtext;
                result.Add(new string[] { msg, textmatch.ToString() });
            }
            return result;
        }
        public void ClickViewAllLink()
        {
            Common cmn = new Common();
            cmn.ClickBellIcon();
            Thread.Sleep(2000);
            cmn.ClickViewAllLnk();
        }
        public List<string[]> VerifyAllNotifications()
        {
            Thread.Sleep(3000);
            List<string[]> result = VerifyNotifications();
            return result;
        }
    }
}
