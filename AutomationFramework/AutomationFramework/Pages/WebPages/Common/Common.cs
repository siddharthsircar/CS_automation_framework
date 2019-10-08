using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationFramework.Pages
{
    class Common
    {
        String pageName;
        SoftAssertions softAssertions = null;
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Common()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Common(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }
        // Header Actions
        /// <summary>
        /// Verified Header elements are displayed
        /// </summary>
        /// <returns></returns>
        public void VerifyHeader(List<string[]> FooterElements)
        {
            for (int i = 0; i < FooterElements.Count; i++)
            {
                if ((FooterElements.ElementAt(2)[3] == "coachicon") && (VerifyCoachingEnable()==false))
                {
                    Console.WriteLine("Coaching is not available for this client");
                }
                else
                {
                    string elementname = FooterElements.ElementAt(i)[2];
                    string locatorname = FooterElements.ElementAt(i)[3];
                    bool elementexistence = SeleniumKeywords.IsElementPresent(pageName, locatorname);
                    softAssertions.Add("Element : " + elementname, true, elementexistence, "equals");
                }
            }
        }

        public Boolean VerifyCoachingEnable()
        {
            Boolean result;
            if ((GlobalVariables.clientname.ToLower().Equals("dollar general")) || (GlobalVariables.clientname.ToLower().Equals("fully insured")) || (GlobalVariables.clientname.ToLower().Equals("fully insured")) || (GlobalVariables.clientname.ToLower().Equals("nucor")) || (GlobalVariables.clientname.ToLower().Equals("hcsc")) || (GlobalVariables.clientname.ToLower().Equals("health trust")) || (GlobalVariables.clientname.ToLower().Equals("arrow ford")))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        public Boolean VerifyIncentiveEnable()
        {
            Boolean result;
            if ((GlobalVariables.clientname.ToLower().Equals("medicare advantage"))|| (GlobalVariables.clientname.ToLower().Equals("group 44"))||(GlobalVariables.clientname.ToLower().Equals("arc")) || (GlobalVariables.clientname.ToLower().Equals("onlife health")) || (GlobalVariables.clientname.ToLower().Equals("nucor")) || (GlobalVariables.clientname.ToLower().Equals("hcsc")) || (GlobalVariables.clientname.ToLower().Equals("health trust")) || (GlobalVariables.clientname.ToLower().Equals("meabt")) || (GlobalVariables.clientname.ToLower().Equals("arrow ford")))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public void ClickBellIcon()
        {
            SeleniumKeywords.Click(pageName, "bellicon");
        }
        public void ClickViewAllLnk()
        {
            SeleniumKeywords.Click(pageName, "viewall");
        }
        ///<summary>
        ///Click on Message To Coach Icon
        /// </summary>
        private void ClickMessageToCoach()
        {
            Thread.Sleep(5000);
            SeleniumKeywords.Click(pageName, "coachicon");
        }
        /// <summary>
        /// Click on Avatar Icon in header
        /// </summary>
        private void ClickAvtar()
        {
            Thread.Sleep(3000);
            SeleniumKeywords.Click(pageName, "avataricon");
        }

        /// <summary>
        /// Click on settings link in Avatar Menu
        /// </summary>
        private void ClickSettings()
        {
            SeleniumKeywords.Click(pageName, "settingslink");
        }
        /// <summary>
        /// It click the logout label present in avtar dropdown
        /// </summary>
        private void ClickLogOut()
        {
            SeleniumKeywords.Click(pageName, "logoutlnk");
        }

        /// <summary>
        /// Clicks on the My Journey sub menu
        /// </summary>
        private void ClickMyJourney()
        {
            SeleniumKeywords.Click(pageName, "myjourneylnk");
        }


        /// <summary>
        /// Clicks on the My Profile sub menu
        /// </summary>
        private void ClickMyProfile()
        {
            SeleniumKeywords.Click(pageName, "myprofilelink");
            Thread.Sleep(3000);
        }
        /// <summary>
        /// Navigates to the My Journey page
        /// </summary>
        public void GoToMyJourney()
        {
            ClickAvtar();
            ClickMyJourney();
        }

        /// <summary>
        /// Navigates to the My Profile page
        /// </summary>
        public void GoToMyProfile()
        {
            ClickAvtar();
            ClickMyProfile();
        }


        /// <summary>
        /// This function gets the value of point
        /// </summary>
        /// <returns>This returns the value of points in integer</returns>
        public int GetPoints(string clientname)
        {
            //SeleniumKeywords.RefreshPage();
            Thread.Sleep(3000);
            string points = SeleniumKeywords.GetText(pageName, "pointslbl").Trim();
            int pts = 0;
            //if (VerifyIncentiveEnable()==true)
            //{
                if (clientname.ToLower().Equals("health trust"))
                {
                    Console.WriteLine("Points : " + points);
                    int index = points.IndexOf("$");
                    Console.WriteLine("Index : " + index);
                    Console.WriteLine("Points Length : " + (points.Length - 1));
                    pts = Int32.Parse(points.Substring(index + 1, points.Length - 1));
                    Console.WriteLine("Next Points : " + pts);
                }
                else
                {
                    pts = Int32.Parse(points);
                }
            //}
                
            return pts;
        }
        /// <summary>
        /// This method refresh the web page
        /// </summary>
        public void RefreshWebPage()
        {
            SeleniumKeywords.RefreshPage();
        }
        /// <summary>
        /// Navigates to the Settings page by calling ClickAvtar() and ClickSettings()
        /// </summary>
        public void GoToSettings()
        {
            ClickAvtar();
            ClickSettings();
        }
        /// <summary>
        /// Logs out of the portal
        /// </summary>
        /// <summary>
        /// Navigates to the Message to coach page 
        /// </summary>
        public void GoToMessageToCoachPage()
        {
            ClickMessageToCoach();
        }
        public void LogOut()
        {
            ClickAvtar();
            ClickLogOut();
        }
        // Menu Actions
        /// <summary>
        /// This method clicks on HemBurger Menu
        /// </summary>
        public void OpenHamMenu()
        {
            Thread.Sleep(3000);
            SeleniumKeywords.Click(pageName, "hemmenu");
            System.Threading.Thread.Sleep(2000);
        }
        /// <summary>
        /// This method clicks on HemBurger Menu
        /// </summary>
        public void CloseHamMenu()
        {
            Thread.Sleep(3000);
            SeleniumKeywords.Click(pageName, "closehammenu");
        }
        /// <summary>
        /// this method is use to close HemBurger Menu
        /// </summary>
        public void ClickOnCloseHemMenu()
        {
            System.Threading.Thread.Sleep(5000);
            SeleniumKeywords.Click("Common", "closeleftmenu");
        }
        /// <summary>
        /// This method return the Heading text of the pages available 
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="locatorname"></param>
        /// <returns></returns>
        public string GetResourcesPageTxt(string pageName, string locatorname)
        {
            string GetResourcesPageTxt = SeleniumKeywords.GetText(pageName, locatorname);
            Console.WriteLine(GetResourcesPageTxt);
            return GetResourcesPageTxt;

        }
        public void ClickConnectionsMenu()
        {
            OpenHamMenu();
            SeleniumKeywords.Click(pageName, "connectionlnk");
            Thread.Sleep(3000);
        }
        //public void ClickFinancialWellBeing()
        //{
        //    SeleniumKeywords.Click(pageName, "finwellbeignmenuitem");
        //}
        public void ClickFinancialWellBeingMenu()
        {
            OpenHamMenu();
            SeleniumKeywords.Click(pageName, "finwellbeignmenuitem");
        }
        /// <summary>
        /// This method clicks on the tracker menu item
        /// </summary>
        public void ClickTrackerMenu()
        {
            OpenHamMenu();
            SeleniumKeywords.Click(pageName, "tracker");
            Thread.Sleep(1000);
        }
        /// <summary>
        /// This method clicks on the Challenges menu item
        /// </summary>
        public void ClickChallengesMenu()
        {
            OpenHamMenu();
            SeleniumKeywords.Click(pageName, "menu_challenges_lnk");
        }

        public void ClickOnResourcesMenu()
        {
            SeleniumKeywords.Click(pageName, "menu_resources_lnk");
            Thread.Sleep(1000);
        }
        /// <summary>
        /// This method clicks on the Resources menu item
        /// </summary>
        public void ClickOnResources()
        {
            OpenHamMenu();
            SeleniumKeywords.Click(pageName, "menu_resources_lnk");
            Thread.Sleep(1000);
        }
        /// <summary>
        /// This method clicks on Progress Checkin menu item
        /// </summary>
        public void ClickProgressCheckinMenu()
        {
            OpenHamMenu();
            SeleniumKeywords.Click(pageName, "menuprogresscheckinlnk");
            Thread.Sleep(1000);
        }
        /// <summary>
        /// This method clicks on Course menu item
        /// </summary>
        public void ClickCourseMenu()
        {
            OpenHamMenu();
            Thread.Sleep(2000);
            SeleniumKeywords.Click(pageName, "menucourselnk");
            Thread.Sleep(2000);
        }
        public void ClickGoalMenu()
        {
            OpenHamMenu();
            SeleniumKeywords.Click(pageName, "menugoallnk");
            Thread.Sleep(1000);
        }
        public void ClickDevicesAndApps()
        {
            OpenHamMenu();
            SeleniumKeywords.Click(pageName, "devicesandappslbl");
            Thread.Sleep(2000);
        }
        public void ClickAddRemoveDevicesLabel()
        {
            SeleniumKeywords.Click(pageName, "addremovedevicelbl");
        }

        //Grey Tile Actions
        /// <summary>
        /// Verifies whether Grey Tiles section is displayed or not
        /// </summary>
        /// <returns> Grey Tiles existance status: true/false</returns>
        public Boolean GreyTileExistence()

        {
            bool gtstatus;
            if (GlobalVariables.clientname =="Dollar General")
            {
                gtstatus = SeleniumKeywords.IsElementPresent(pageName, "greytilesectiondg");

            } else
            {
                gtstatus = SeleniumKeywords.IsElementPresent(pageName, "greytilesection");
            }
            return gtstatus;
        }
        /// <summary>
        /// This method is use to click on Learn More from Leftmenu
        /// </summary>
        public void ClickOnLearnMore()
        {
            SeleniumKeywords.Click(pageName, "learnmore_lnktext");
        }
        /// <summary>
        /// Verifies Grey tile section existence [Assert]
        /// Grey tiles elements existence and Text [Methods for different tiles]
        /// </summary>
        public void GreyTiles(List<string[]> ExpectedText)
        {
            for (int i = 0; i < ExpectedText.Count; i++)
            {
                string elementname = ExpectedText.ElementAt(i)[2];
                string locatorname = ExpectedText.ElementAt(i)[3];
                string expectedtext = ExpectedText.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, locatorname);
                softAssertions.Add("Element : " + elementname, expectedtext, actualtext, "equals");
            }
        }
        //Contact Us 
        /// <summary>
        /// Verifies whether Contact Us  section is displayed in footer or not
        /// </summary>
        /// <returns> Grey Tiles existence status: true/false</returns>
        public Boolean ContactUsDisplayed()
        {
            bool gtstatus = SeleniumKeywords.IsElementPresent(pageName, "contactussection");
            return gtstatus;
        }
        /// <summary>
        /// Verifies Contact Us Section footer elements
        /// </summary>
        public void ContactUsElements(List<string[]> ExpectedText)
        {
            for (int i = 0; i < ExpectedText.Count; i++)
            {
                string elementname = ExpectedText.ElementAt(i)[3];
                string locatorname = ExpectedText.ElementAt(i)[4];
                string expectedtext = ExpectedText.ElementAt(i)[5];
                string actualtext = SeleniumKeywords.GetText(pageName, locatorname);
                bool textmatch = SeleniumKeywords.VerifyText(actualtext, expectedtext);
                softAssertions.Add("Element : " + elementname, expectedtext, actualtext, "equals");
            }
        }

        /// <summary>
        /// Verifies Footer links existence
        /// </summary>
        public void FooterLinksDisplayed(List<string[]> FooterElements)
        {
            for (int i = 0; i < FooterElements.Count; i++)
            {
                string elementname = FooterElements.ElementAt(i)[2];
                string locatorname = FooterElements.ElementAt(i)[3];
                bool elementpresent = SeleniumKeywords.IsElementPresent(pageName, locatorname);
                softAssertions.Add("Element : " + elementname, true, elementpresent, "equals");
            }
        }
        
        /// <summary>
        /// This method click on the Footer Dashboard Link
        /// </summary>
        public void ClickFooterDashboardLink()
        {
            Page_HAPrompt hAPrompt = new Page_HAPrompt();
            Thread.Sleep(6000);
            SeleniumKeywords.Click(pageName, "fdashboardlnk");
            hAPrompt.GoToDashboard();
        }
        
        /// <summary>
        /// This method click on the Footer Tracker Link
        /// </summary>
        public void ClickFooterTrackerLink()
        {
            Thread.Sleep(5000);
            SeleniumKeywords.Click(pageName, "ftrackerlnk");
        }
        
        /// <summary>
        /// This method click on the Footer My Health Link
        /// </summary>
        public void ClickFooterHealthLink()
        {
            Thread.Sleep(5000);
            SeleniumKeywords.Click(pageName, "fmyhealthlnk");
        }
        
        /// <summary>
        /// This method click on the Footer My Company Link
        /// </summary>
        public void ClickFooterCompanyLink()
        {
            Thread.Sleep(5000);
            SeleniumKeywords.Click(pageName, "fmycompanylnk");
        }
        
        /// <summary>
        /// This method click on the Footer HIPAA Rights Link
        /// </summary>
        public void ClickFooterHipaaLink()
        {
            Thread.Sleep(5000);
            SeleniumKeywords.Click(pageName, "fhiparightslnk");
        }
        
        /// <summary>
        /// This method click on the Footer Privacy Policy Link
        /// </summary>
        public void ClickFooterPolicyLink()
        {
            Thread.Sleep(5000);
            SeleniumKeywords.Click(pageName, "fprivacypollnk");
        }
        
        /// <summary>
        /// This method click on the Footer Terms Of Service Link
        /// </summary>
        public void ClickFooterTOSLink()
        {
            Thread.Sleep(5000);
            SeleniumKeywords.Click(pageName, "ftoslnk");
        }
        
        /// <summary>
        /// This method click on the Footer Feedback Link
        /// </summary>
        public void ClickFooterFeedbackLink()
        {
            Thread.Sleep(5000);
            SeleniumKeywords.Click(pageName, "ffeedbacklnk");
        }
        public string[] GetDate()
        {
            //Today date
            DateTime currentdate = DateTime.Today;
            string[] trackerdate = new string[3];

            trackerdate[0] = String.Format("{0:MM/dd/yyyy}", currentdate.AddDays(-2).Date);
            trackerdate[1] = String.Format("{0:MM/dd/yyyy}", currentdate.AddDays(-1).Date);
            trackerdate[2] = String.Format("{0:MM/dd/yyyy}", currentdate);
            return trackerdate;
        }

        public string[] GetTrackerDateTime()
        {
            //Today date
            DateTime currentdate = DateTime.Now;
            currentdate = new DateTime(currentdate.Year, currentdate.Month, currentdate.Day, currentdate.Hour, 0, 0);
            string[] trackerdate = new string[3];

            trackerdate[0] = String.Format("{0:MM/dd/yyyy HH:mm}", currentdate.AddDays(-2).AddHours(-2));
            trackerdate[1] = String.Format("{0:MM/dd/yyyy HH:mm}", currentdate.AddDays(-1).AddHours(-1));
            trackerdate[2] = String.Format("{0:MM/dd/yyyy HH:mm}", currentdate);
            return trackerdate;
        }
        public string GetCurrentDate()
        {
            DateTime currentdate = DateTime.Today;
            return (String.Format("{0:MM/dd/yyyy}", currentdate));

        }

        public string GetCurrentDate(string format)
        {
            DateTime currentdate = DateTime.Today;
            return (String.Format("{0:"+ format + "}", currentdate));

        }

        public string AddDaysInCurrentDate(int noofdays)
        {
            DateTime currentdate = DateTime.Today;
            string newdate = String.Format("{0:MM/dd/yyyy}", currentdate.AddDays(noofdays).Date);
            return newdate;
        }

        public string GetCurrentDay()
        {
            return (DateTime.Today.Day.ToString());
        }

        public string GetCurrentMonth()
        {
            return (DateTime.Today.ToString("MMMM"));
        }

        public string GetCurrentYear()
        {
            return (DateTime.Today.Year.ToString());
        }
        public void VerifyDate()
        {
            //Today date
            //DateTime currentdate = DateTime.Today;
            //Thread.Sleep(2000);
            //string calenderdefaultdate = SeleniumKeywords.GetAttributeValue(pageName, "calenderdatepicker", "value");

            //string currentdt = String.Format("{0:MM/dd/yyyy}", currentdate);
            //softAssertions.Add("Calendar", currentdt, calenderdefaultdate, "contains");
        }

        public void VerifyTimeAndDate()
        {
            //Today date
            DateTime currentdate = DateTime.Now;
            currentdate = new DateTime(currentdate.Year, currentdate.Month, currentdate.Day, currentdate.Hour, 0, 0);

            string calenderdefaultdate = SeleniumKeywords.GetAttributeValue(pageName, "calenderdatepicker", "value");
            string currentdt = String.Format("{0:MM/dd/yyyy HH:mm}", currentdate);
            softAssertions.Add("Calendar", currentdt, calenderdefaultdate, "equals");
        }

        public void ClickJourneyNextButton()
        {
            SeleniumKeywords.Click(pageName, "journeyslidernextbtn");
        }
        /// <summary>
        /// method is used to check wheter coacing available for client
        /// </summary>
        /// <param name="clientname"></param>
        /// <param name="pageName1"></param>
        public Boolean IsCoachingEnable(string clientname)
        {
            if ((clientname.ToLower().Equals("health trust")) || (clientname.ToLower().Equals("meabt")) || (clientname.ToLower().Equals("nucor")) || (clientname.ToLower().Equals("dollar general")))
            {
                return(true);
            }
            else
            {
                return (false);
            }

        }
        /// <summary>
        /// method is used to check whether certificate is available for client
        /// </summary>
        /// <param name="clientname"></param>
        /// <param name="pageName1"></param>
        public Boolean IsCertificateEnable(string clientname)
        {
            if ((clientname.ToLower().Equals("health trust")) || (clientname.ToLower().Equals("meabt")) || (clientname.ToLower().Equals("nucor")) || (clientname.ToLower().Equals("dollar general"))|| (clientname.ToLower().Equals("onlife health")) || (clientname.ToLower().Equals("medical advantage"))|| (clientname.ToLower().Equals("group 44")) || (clientname.ToLower().Equals("nissan")) || (clientname.ToLower().Equals("tva")))
            {
                return (false);
            }
            else
            {
                return (true);
            }

        }
        public void ValidateReportBottomLinks(string clientname, string pageName1)
        {
            if (IsCoachingEnable(clientname))
            {
                clientname = "Health Trust";
            }
            else
            {
               //clientname = "Onlife Health";
            }
            List<string[]> reportbottomlinks = CSVReaderDataTable.GetCSVData("ProgressCheckinReportData", pageName1, "reportbottomlinks", clientname);
            for (int i = 0; i < reportbottomlinks.Count; i++)
            {
                string reportbottomlinklocatorname = reportbottomlinks.ElementAt(i)[3];
                string explinktext = reportbottomlinks.ElementAt(i)[4];
                string actuallinktext = SeleniumKeywords.GetText("CommonProgressCheckIn", reportbottomlinklocatorname);

                softAssertions.Add("Element : " + reportbottomlinklocatorname, explinktext, actuallinktext, "contains");


                SeleniumKeywords.Click("CommonProgressCheckIn", reportbottomlinklocatorname);
                if (explinktext == "RETURN TO DASHBOARD")
                {
                    Page_HAPrompt pha = new Page_HAPrompt();
                    pha.GoToDashboard();
                }
                string report_bootomlinks_locatorclass = reportbottomlinks.ElementAt(i)[5];
                string navigated_page_element_locatorname = reportbottomlinks.ElementAt(i)[6];
                string exp_navigated_page_elementtext = reportbottomlinks.ElementAt(i)[7];
                string actual_navigated_page_elementtext = SeleniumKeywords.GetText(report_bootomlinks_locatorclass, navigated_page_element_locatorname);

                softAssertions.Add("Element : " + navigated_page_element_locatorname, exp_navigated_page_elementtext, actual_navigated_page_elementtext, "equals");
                Console.WriteLine("Element: " + navigated_page_element_locatorname + " " + exp_navigated_page_elementtext + " " + actual_navigated_page_elementtext);
                if (explinktext != "RETURN TO DASHBOARD")
                {
                    SeleniumKeywords.NavigateToPreviousPage();
                }
            }
        }

        public string GetInstancePointsValue(string clientname, string category)
        {
            string instancepointsvalue = "";
            List<string[]> instancepointslist = CSVReaderDataTable.GetCSVData("InstancePointsValue", pageName, category, clientname.ToLower());
            Console.WriteLine("instancepointslist.count : " + instancepointslist.Count);
            if (instancepointslist.Count == 1)
            {
                instancepointsvalue = instancepointslist.ElementAt(0)[3];
            }
            Console.WriteLine("Instance points value : " + instancepointsvalue);
            return instancepointsvalue;
        }

        public int GetHRAID(string clientname)
        {
            int hraid = 0;
            clientname = clientname.ToLower();
            if (clientname.Equals("health trust"))
                hraid = 89;
            else if (clientname.Equals("meabt") || clientname.Equals("tva") || clientname.Equals("nissan") || clientname.Equals("arc"))
                hraid = 66;
            else if (clientname.Equals("dollar general"))
                hraid = 87;
            else if (clientname.Equals("nucor"))
                hraid = 84;
            else if (clientname.Equals("onlife health") || clientname.Equals("group 44"))
                hraid = 81;
            return hraid;
        }

        /// <summary>
        /// Clicks on the sharing menu icon of the mentioned tracker
        /// </summary>
        /// <param name="trackername"></param>
        private void ClickTrackerSharingMenuIcon(string trackername)
        {
            SeleniumKeywords.Click(pageName, "sharingmenu", trackername);
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Updates circle of a tracker
        /// </summary>
        /// <param name="trackername"></param>
        /// <param name="circle"></param>
        private void SelectCircle(string trackername, string circle)
        {
            SeleniumKeywords.Click(pageName, "concircleoptn", trackername, circle);
            Thread.Sleep(3000);
        }

        /// <summary>
        /// Shares a given tracker with a given circle
        /// </summary>
        /// <param name="trackername"></param>
        /// <param name="circle"></param>
        public void Sharetracker(string trackername, string circle)
        {
            Thread.Sleep(2000);
            ClickTrackerSharingMenuIcon(trackername);
            SelectCircle(trackername, circle);
        }

        public string GetEnvFromUrl()
        {
           
            string url = GlobalVariables.baseurl;
            string environment = GlobalVariables.environment;//e.g Staging
            string env = "";
            if (environment.ToLower().Equals("prod") || environment.ToLower().Equals("production"))
            {
                env = "";
            }
            else
            {
                int startindex = url.IndexOf("/");
                int endindex = url.IndexOf('.');
                startindex = startindex + 2;
                int index = endindex - startindex;
                env = url.Substring(startindex, index); // e.g, qa2012
                env = env + "-";
            }
            
            return env;
        }

        /// <summary>
        /// Retrieve required client config
        /// </summary>
        public List<String[]> GetConfig(params string[] configName)
        {
            string clientName = GlobalVariables.clientname.ToLower();
            List<String[]> clientconfig = CSVReaderDataTable.GetCSVData("ClientConfig", clientName, configName);
            foreach (var col in clientconfig)
            {
                foreach (var data in col)
                {
                    Console.WriteLine(data);
                }
            }
            return clientconfig;
        }
    }
}
