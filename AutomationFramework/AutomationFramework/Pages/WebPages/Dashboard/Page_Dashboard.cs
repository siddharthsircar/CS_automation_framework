using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using AventStack.ExtentReports.MarkupUtils;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AutomationFramework.Pages
{
    class Page_Dashboard
    {
        String pageName;
        SoftAssertions softAssertion = null;
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_Dashboard()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_Dashboard(SoftAssertions softAssertion) : this()
        {
            this.softAssertion = softAssertion;
        }

        /// <summary>
        /// Verifies whether Journey Banner section is displayed or not
        /// </summary>
        /// <returns> Journey Banner existance status: true/false</returns>
        public Boolean JourneyBannerDisplayed()
        {
            bool jbstatus = SeleniumKeywords.IsElementPresent(pageName, "jbsection");
            return jbstatus;
        }
        /// <summary>
        /// Verifies whether Journey Recommendations section is displayed or not
        /// </summary>
        /// <returns> Existance status: true/false</returns>
        public Boolean JourneySectionDisplayed()
        {
            bool jstatus = SeleniumKeywords.IsElementPresent(pageName, "recomsection");
            return jstatus;
        }
        //public void JourneyTileContactToCoach()
        //{
        //    SikuliKeywords.ImageVisible(pageName, "Welcome_JourneyTile");
            
        //}
        /// <summary>
        /// Verifies whether Explore section is displayed or not
        /// </summary>
        /// <returns> Existence status: true/false</returns>
        public Boolean EstreamDisplayed()
        {
            bool estatus = SeleniumKeywords.IsElementPresent(pageName, "exploresection");
            return estatus;
        }

        /// <summary>
        /// This method will verify the onlife logo Image
        /// This method Sikuli Keyword 
        /// It passes the locator and name of the login btn to SeKeyword click Btn 
        /// </summary>
        private Boolean VerifyImg(String locatorname)
        {
            return SikuliKeywords.ImageVisible(pageName, "locatorname");
        }
        public Boolean AtDashboard()
        {
            bool status;
            Thread.Sleep(2000);
            status = SeleniumKeywords.IsElementPresent("Common", "avataricon");
            //JourneyTileContactToCoach();
            return status;
        }
        public Boolean HAJourneyTile()
        {

            return(SikuliKeywords.ImageVisible(pageName, "HA_JourneyTile"));

        }
        /// <summary>
        /// Method is use to verify order of Left Menu option
        /// </summary>
        /// <param name="expected"></param>
        public void Verify_LeftMenuOptionsOrder(List<String[]> expected)
        {
            string actualtxt;
            string elementname,locatorname;
            string locatorindex;
            string expectedtxt;
            for (int i = 0; i < expected.Count; i++)
            {
                elementname = expected.ElementAt(i)[3];
                locatorname = expected.ElementAt(i)[4];
                expectedtxt = expected.ElementAt(i)[5];
                locatorindex = expected.ElementAt(i)[6];
                actualtxt = SeleniumKeywords.GetText(pageName, locatorname, locatorindex);
                softAssertion.Add(elementname, expectedtxt, actualtxt, "contains");
            }            
        }

        public void Verify_LeftMenuCommonOptions(List<String[]> expected)
        {
            bool actualdisplaystatus;
            bool expecteddisplaystatus;
            string elementname, locatorname;
            string locatorindex1;
            //Console.WriteLine("Verify_LeftMenuCommonOptions moducle called");
            for (int i = 0; i < expected.Count; i++)
            {
                elementname = expected.ElementAt(i)[3];
                locatorname = expected.ElementAt(i)[4];
                expecteddisplaystatus = Convert.ToBoolean(expected.ElementAt(i)[5]);
                locatorindex1 = expected.ElementAt(i)[6];
               // locatorindex2 = expected.ElementAt(i)[7];
                Console.WriteLine(elementname+"  "+locatorname + "  " + locatorindex1);
                if (expecteddisplaystatus == true)
                {
                    //if ((locatorindex1.ToLower().Equals("financial well-being")) && ((GlobalVariables.clientname.ToLower().Equals("spouses")) || (GlobalVariables.clientname.ToLower().Equals("onlife health"))|| (GlobalVariables.clientname.ToLower().Equals("dollar general")) || (GlobalVariables.clientname.ToLower().Equals("group 44"))))
                    //{
                    //    actualdisplaystatus = true;
                    //}
                    //else if((locatorindex1.ToLower().Equals("devices and apps")) && (GlobalVariables.clientname.ToLower().Equals("dollar general")))
                    //{
                    //    actualdisplaystatus = true;
                    //}
                    //else
                    //{
                        actualdisplaystatus = SeleniumKeywords.IsElementPresent(pageName, locatorname, locatorindex1);
                    //}
                }
                else
                    actualdisplaystatus = SeleniumKeywords.IsElementNotPresent(pageName, locatorname, locatorindex1);

                //Console.WriteLine("The Actual menu option title: " + actualdisplaystatus + "  Expected  :" + expected.ElementAt(i)[4]);
                softAssertion.Add(elementname, expecteddisplaystatus, actualdisplaystatus, "equals");
                //Console.WriteLine("Pro get count   " + count);
                //System.Threading.Thread.Sleep(1000);
            }
        }
        /// <summary>
        /// Method is use to verify sub menu option
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="count"></param>
        public void Verify_LeftSubMenuOptions(List<String[]> expected,int count)
        {
            bool actualdisplaystatus;
            bool expecteddisplaystatus;
            string elementname,locatorname;
            string locatorindex1, locatorindex2;
            string enabled, clientname;
            for (int i = 0; i < expected.Count; i++)
            {
                clientname = expected.ElementAt(i)[0];
                enabled = expected.ElementAt(i)[3];
                elementname = expected.ElementAt(i)[4];
                locatorname = expected.ElementAt(i)[5];
                expecteddisplaystatus = Convert.ToBoolean(expected.ElementAt(i)[6]);
                locatorindex1 = expected.ElementAt(i)[7];
                locatorindex2 = expected.ElementAt(i)[8];

                if(clientname.ToLower().Equals("allclients") || FeatureEnabledForCurrentClient(enabled, clientname))
                {
                    if (expecteddisplaystatus == true)
                        actualdisplaystatus = SeleniumKeywords.IsElementPresent(pageName, locatorname, locatorindex1, locatorindex2);
                    else
                        actualdisplaystatus = SeleniumKeywords.IsElementNotPresent(pageName, locatorname, locatorindex1, locatorindex2);

                    softAssertion.Add(elementname, expecteddisplaystatus, actualdisplaystatus, "equals");
                }
                
              
            }
        }
        /// <summary>
        /// Verifies Common Footer links existence for all client
        /// </summary>
        public void CommonFooterLinksDisplayed(List<string[]> FooterElements)
        {
            for (int i = 0; i < FooterElements.Count; i++)
            {
                string elementname = FooterElements.ElementAt(i)[3];
                string locatorname = FooterElements.ElementAt(i)[4];
                Console.WriteLine("Element Name" + elementname + "   Locator  " + locatorname);
                bool elementpresent = SeleniumKeywords.IsElementPresent("Common", locatorname);
                softAssertion.Add("Element : " + elementname, true, elementpresent, "equals");
            }
        }

        /// <summary>
        /// Verifies Client Footer links existence for all client
        /// </summary>
        public void ClientFooterLinksDisplayed(List<string[]> FooterElements)
        {
            for (int i = 0; i < FooterElements.Count; i++)
            {
                string elementname = FooterElements.ElementAt(i)[3];
                string locatorname = FooterElements.ElementAt(i)[4];
                string linktext = FooterElements.ElementAt(i)[5];
                Console.WriteLine("Element Name" + elementname + "   Locator  " + locatorname);
                bool elementpresent = SeleniumKeywords.IsElementPresent("Common", "fclintlnk", linktext);
                softAssertion.Add("Element : " + elementname, true, elementpresent, "contains");
            }
        }
        public void Verify_DashboardHeaderIcons()
        {
            Boolean actual;
            actual = VerifyImg("connecttocoachImg");
                softAssertion.Add("HT Logo",true, actual, "equals");
        }
        
        public void Verify_CoachingSection()
        {
            string expectedtxt;
            expectedtxt = SeleniumKeywords.GetText(pageName, "coachingsection");
            Console.WriteLine(expectedtxt);
            softAssertion.Add("Coaching Section header", expectedtxt, "COACHING", "equals");
            expectedtxt = SeleniumKeywords.GetText(pageName, "coachingsectionsubheading");
            softAssertion.Add("Coaching Section header", expectedtxt, "Ask your Coach", "equals");
            expectedtxt = SeleniumKeywords.GetText(pageName, "coachingsectiontilesheading");
            softAssertion.Add("Coaching Section header", expectedtxt, "SCROLL THROUGH AVAILABLE GOALS", "equals");
            SeleniumKeywords.IsElementPresent(pageName, "coachingsectiontile");
        }

        public void Verify_CoursesSection()
        {
            string expectedtxt;
            expectedtxt = SeleniumKeywords.GetText(pageName, "coursessection");
            Console.WriteLine(expectedtxt);
            softAssertion.Add("Courses Section header", expectedtxt, "COURSES", "equals");
            expectedtxt = SeleniumKeywords.GetText(pageName, "coursessectionsubheading");
            softAssertion.Add("Courses Section header", expectedtxt, "Get Started Today", "equals");
            expectedtxt = SeleniumKeywords.GetText(pageName, "coursessectiontilesheading");
            softAssertion.Add("Courses Section header", expectedtxt, "SCROLL THROUGH AVAILABLE COURSES", "equals");
            SeleniumKeywords.IsElementPresent(pageName, "coursessectiontile");
        }

        public void Verify_TrackersSection()
        {
            string expectedtxt;
            expectedtxt = SeleniumKeywords.GetText(pageName, "trackingsection");
            Console.WriteLine(expectedtxt);
            softAssertion.Add("Tracker Section header", expectedtxt, "TRACKING", "equals");
            expectedtxt = SeleniumKeywords.GetText(pageName, "trackingsectionsubheading");
            softAssertion.Add("Tracker Section header", expectedtxt, "Track Your Activity", "equals");
            SeleniumKeywords.Click(pageName, "trackingsectiondropdownbtn");
            SeleniumKeywords.IsElementPresent(pageName, "trackingsectiondropdown_value");
        }

        public void Verify_UpdateProgressSection()
        {
            string expectedtxt;
            expectedtxt = SeleniumKeywords.GetText(pageName, "updateprogresssection");
            Console.WriteLine(expectedtxt);
            softAssertion.Add("Update Trogress Section header", expectedtxt, "UPDATE PROGRESS", "equals");
            expectedtxt = SeleniumKeywords.GetText(pageName, "updateprogresssectionsubheading");
            softAssertion.Add("Tracker Section header", expectedtxt, "Record Your Activities", "equals");
            SeleniumKeywords.Click(pageName, "updateprogresssectiondropdownbtn");
            SeleniumKeywords.IsElementPresent(pageName, "updateprogresssectiondropdown_value");
        }

        private bool FeatureEnabledForCurrentClient(string enabled,string clname)
        {
            bool featureanabled = false;
            int ccount = 0;
            List<string> clientname = new List<string>();
            if (clname.Contains("|"))
            {
                clientname = clname.Split('|').ToList();
            }
            else
            {
                clientname.Add(clname);
            }

            if (enabled.ToLower().Equals("yes"))
            {
                foreach (string cl in clientname)
                {
                    if (cl.ToLower().Equals(GlobalVariables.clientname.ToLower()))
                    {
                        featureanabled = true;
                        break;
                    }
                }
            }
            else if (enabled.ToLower().Equals("no"))
            {
                foreach (string cl in clientname)
                {
                    if (cl.ToLower().Equals(GlobalVariables.clientname.ToLower()))
                    {
                        featureanabled = false;
                        break;
                    }
                    ccount++;
                }
                if (ccount == clientname.Count)
                {
                    featureanabled = true;
                }
            }
            Console.WriteLine("FeatureEnabled : "+featureanabled);
            return featureanabled;
        }

    }
}

