using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using AutomationFramework.Pages.WebPages.MyProfile;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.Connections
{
    class Page_Connections
    {
        String pageName;
        List<string[]> remove = new List<string[]>();
        List<string[]> pendingdata = new List<string[]>();

        SoftAssertions softAssertion = null;

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_Connections()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        /// <param name="softAssertion"></param>
        public Page_Connections(SoftAssertions softAssertion) : this()
        {

            this.softAssertion = softAssertion;
        }

        /// <summary>
        /// Method to verify HIPPA in Spanish
        /// </summary>
        private void VerifySpHippa()
        {
            //Thread.Sleep(4000);
            remove = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "spdata");
            for (int i = 0; i < remove.Count; i++)
            {
                string elementname = remove.ElementAt(i)[2];
                string elementlocatorname = remove.ElementAt(i)[3];
                string expectedtext = remove.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                softAssertion.Add("Element : "+elementlocatorname,expectedtext,actualtext,"contains");
            }
        }

        /// <summary>
        /// Method to verify HIPPA
        /// </summary>
        private void VerifyHippa()
        {
            //Thread.Sleep(4000);
            remove = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "data");
            for (int i = 0; i < remove.Count; i++)
            {
                string elementname = remove.ElementAt(i)[2];
                string elementlocatorname = remove.ElementAt(i)[3];
                string expectedtext = remove.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                softAssertion.Add("Element: " + elementlocatorname, expectedtext, actualtext, "contains");
            }
        }

        /// <summary>
        /// Method verfies pending request displayed after request sent
        /// </summary>
        /// <param name="emailid"></param>
        private void VerifyPendingRequest(string emailid)
        {
            //Thread.Sleep(5000);
            string eid=SeleniumKeywords.GetText(pageName, "pendinguserid");
            
            softAssertion.Add("Element: pendinguserid",emailid, eid, "contains");
            pendingdata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "pendingdata");
            for (int i = 0; i < remove.Count; i++)
            {
                string elementname = remove.ElementAt(i)[2];
                string elementlocatorname = remove.ElementAt(i)[3];
                string expectedtext = remove.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                softAssertion.Add("Element: " + elementlocatorname, expectedtext, actualtext, "contains");
            }

        }

        /// <summary>
        /// Blank
        /// </summary>
        private void VerifyUserAdded()
        {
            //Thread.Sleep(4000);

        }

        /// <summary>
        /// Method to send Connection Request
        /// </summary>
        /// <param name="email"></param>
        private void SendConnectionRequest(string email)
        {
            Thread.Sleep(2000);
            //JavaScriptKeywords.SetTextInElement(pageName, "connectionstextbox", email);
            SeleniumKeywords.SetText(pageName, "connectionstextbox", email);
            SeleniumKeywords.Click(pageName, "searchbtn");
        }

        /// <summary>
        /// Method verifies searched email is found
        /// </summary>
        /// <returns></returns>
        private string UserPresent()
        {
            Thread.Sleep(4000);
            string userid = SeleniumKeywords.GetText(pageName, "searchedemailid");
            return userid;
        }

        /// <summary>
        /// Method click on Add button
        /// </summary>
        private void ClickAddBtn()
        {
            SeleniumKeywords.Click(pageName, "addbtn");
        }

        /// <summary>
        /// Method navigates to LiveOn login page
        /// </summary>
        /// <param name="url"></param>
        private void NavigateToLoginPage(string url)
        {
            SeleniumKeywords.NavigateToUrl(url);
        }


        private void Clickelipseball()
        {   
            SeleniumKeywords.Click(pageName, "connection_membermenu");
        }

        private void ClickMessageLabel()
        {
            SeleniumKeywords.Click(pageName, "messagelbl");
        }

        private string GetUserName()
        {
            //Thread.Sleep(3000);
            string username = SeleniumKeywords.GetText(pageName, "username").Trim() ;
            return username;
        }
        private void InputMessage(string message)
        {
            SeleniumKeywords.SetText(pageName, "messagebox", message);
        }
        private void PressEnter()
        {
            SeleniumKeywords.PressKey(pageName,"messagebox","Enter");
        }
        private string GetMessage()
        {
            return (SeleniumKeywords.GetText(pageName, "sendmessage").Trim());
        }
        private void ClickMessageTab()
        {
            //Thread.Sleep(3000);
            SeleniumKeywords.Click(pageName, "messagetab");
        }
        private void ClickConnectionsTab()
        {
            SeleniumKeywords.Click(pageName, "connectiontab");
            //Thread.Sleep(3000);
        }
        private void ClickDisconnect()
        {
            //Thread.Sleep(3000);
            SeleniumKeywords.Click(pageName, "disconnectlbl");
        }
        private void VerifyDisconnectModalWindow()
        {
            //Thread.Sleep(4000);
            remove = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "modaldata");
            for (int i = 0; i < remove.Count; i++)
            {
                string elementname = remove.ElementAt(i)[2];
                string elementlocatorname = remove.ElementAt(i)[3];
                string expectedtext = remove.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                softAssertion.Add("Element: " + elementlocatorname, expectedtext, actualtext, "contains");
            }
        }
        public void VerifySpanishHippa()
        {            
            //Thread.Sleep(3000);
            VerifySpHippa();
        }
        public void DeclineHippa()
        {
            SeleniumKeywords.Click(pageName, "dismissbtn");
        }
        public void AcceptHippa()
        {
            SeleniumKeywords.Click(pageName, "acceptbtn");
        }
        public void ClickModalWindowYesBtn()
        {
            SeleniumKeywords.Click(pageName, "modalyesbtn");
        }

        public void VerifyHippaInEnglish()
        {            
            //Thread.Sleep(2000);
            VerifyHippa();
        }
        public string SearchUser(string emailid)
        {
            SendConnectionRequest(emailid);
            string userid = UserPresent();
            return userid;
        }
        public void VerifyUserRequest(string emailid)
        {
            ClickAddBtn();
            VerifyPendingRequest(emailid);
        }
        public void LoginUser(string url,string user,string password)
        {
            NavigateToLoginPage(url);
            Page_Login pl = new Page_Login();
            pl.Login(user, password);
        }
        public string GetUserFullName()
        {
            return (GetUserName());
        }
        public void ClickDeclineButton()
        {
            SeleniumKeywords.Click(pageName, "declinebtn");
        }
        public void ClickAcceptButton()
        {
            //Thread.Sleep(1000);
            SeleniumKeywords.Click(pageName, "userrequestacceptbtn");
            //Thread.Sleep(2000);
        }
        public string SendMessage(string message)
        {
            //Thread.Sleep(4000);
            Clickelipseball();
            ClickMessageLabel();
            //Thread.Sleep(3000);
            InputMessage(message);
            PressEnter();
            string sendmessage = GetMessage();
            return sendmessage;
        }

        public string VerifyMessage()
        {
            ClickMessageTab();
            return (GetMessage());
        }

        public void DisconnectUser()
        {
            ClickConnectionsTab();
            Clickelipseball();
            ClickDisconnect();
            VerifyDisconnectModalWindow();
            ClickModalWindowYesBtn();
        }

        /// <summary>
        /// Method shares 2 trackers: 1 From Tracker learn more page and 1 from My Profile page
        /// </summary>
        public void ShareTrackers()
        {
            Common cmn = new Common();
            // Navigate to tracker Learn More page and share tracker
            cmn.ClickFooterTrackerLink();
            cmn.Sharetracker("Stress", "inner");

            // Navigate to My Profile and share tracker
            cmn.GoToMyProfile();
            Page_MyProfile mp = new Page_MyProfile();
            mp.ClickTrackerTab();
            cmn.Sharetracker("Weight","outer");
        }

        public void ChangeConnectionCircle(string circle)
        {
            // Click connected member option
            SeleniumKeywords.Click(pageName, "connection_membermenu");
            // Select Circle
            SeleniumKeywords.Click(pageName, "connection_circle", circle);
            //Thread.Sleep(3000);
            SeleniumKeywords.Click(pageName, "connection_membermenu");
        }

        public void NavigateToConnectionProfile(string fullname)
        {          
            SeleniumKeywords.Click(pageName, "connection_name", fullname);
        }
    }
}
