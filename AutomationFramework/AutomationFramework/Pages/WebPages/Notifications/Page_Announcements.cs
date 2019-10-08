using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace AutomationFramework.Pages.WebPages.Notifications
{
    /// <summary>
    /// Announcements page class
    /// </summary>
    public class Page_Announcements
    {
        String pageName;
        SoftAssertions softAssertions = null;

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_Announcements()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }


        public Page_Announcements(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }

        
        public void FetchAnnouncements(List<string[]> announcementData)
        {
            int length;
            CommonApi cmna = new CommonApi();
            string userid = cmna.GetUserID();
            Common cmn = new Common();
            string env = cmn.GetEnvFromUrl();
            string baseurl = cmna.GetServices2Url();
            string apiendpoint = baseurl + "/internalapi/v1.0/member/"+ userid +"/allnotifications";
            Console.WriteLine(apiendpoint);
            ApiKeywords.InitializeRequest(apiendpoint);
            ApiKeywords.SetMethod("GET");
            string jsonresponse = ApiKeywords.SendRequest();
            string[] parentObjPath = { "Notification" };
            string[] subObjPath = { "Description" };
            List<string[]> description = ApiKeywords.GetResponse(jsonresponse, parentObjPath, subObjPath);
            Console.WriteLine("Announcements Result: "+description.Count);
            if (description.Count() != 0 && announcementData.Count() != 0)
            {
                if (announcementData.Count() < description.Count())
                    length = announcementData.Count();
                else
                    length = description.Count();
                Console.WriteLine("Length: "+length);
                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < description.ElementAt(0).Count(); j++)
                    {
                        Console.WriteLine("Description: "+ description.ElementAt(i)[j]);
                        string elementname = "Announcement "+(i+1);
                        string expectedtext = announcementData.ElementAt(i)[3];
                        string actualtext = description.ElementAt(i)[j];
                        Console.WriteLine(elementname + "\tExpectedText : " + expectedtext + "\tActualText : " + actualtext);
                        softAssertions.Add(elementname, expectedtext, actualtext, "contains");
                    }
                }
            }
            else
                softAssertions.Add("Total tiles Doesn't match", announcementData.Count(), description.Count(), "equals");

        }


    }
}
