using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages.Notifications;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AutomationFramework.Tests.WebTests.Notifications
{
    /// <summary>
    /// Test Class: Announcements
    /// </summary>
    [TestFixture]
    [Order(9)]
    public class Announcements : Base
    {
        string className;

        /// <summary>
        /// Constructor to get ClassName
        /// </summary>
        public Announcements()
        {
            className = this.GetType().Name;
            Console.WriteLine("Current class : " + className);
        }
        /// <summary>
        /// Test Case: Validate announcements for new member
        /// </summary>
        [Test]
        public void TC_VerifyAnnouncements()
        {
            List<string[]> announcementdata = CSVReaderDataTable.GetCSVData("AnnouncementContent", className, "new_member_announcements", GlobalVariables.clientname.ToLower());
            Page_Announcements pannounce = new Page_Announcements(softassertions);
            pannounce.FetchAnnouncements(announcementdata);
            softassertions.AssertAll();
        }


    }
}
