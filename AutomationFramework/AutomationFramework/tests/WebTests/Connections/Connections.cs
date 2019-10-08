using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages.Connections;
using AutomationFramework.Pages.WebPages.Trackers;
using NUnit.Framework;
using System.Configuration;

namespace AutomationFramework.Tests.WebTests.Connections
{
    /// <summary>
    /// Connections Test Class
    /// </summary>
    [TestFixture]
    [Order(45)]
    public class Connections : Base
    {
        Page_Connections cn = null;
        static string message = "Hello";

        /// <summary>
        /// Test Case: (USER A) To convert language settings to Spanish and verify spanich text displayed
        /// </summary>
        [Test, Order(1)]
        //[Category("BuildSanity")]
        //[Category("ProdSanity")]
        //[Category("Regression")]
        public void TC_ConvertToSpanish()
        {
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();

            cn = new Page_Connections(softassertions);
            TestContext.WriteLine("Navigating to Settings");
            Common settings = new Common();
            settings.GoToSettings();

            Page_Settings myinf = new Page_Settings(softassertions);
            myinf.ConvertToSpanish();
            is_soft_assert = true;
            softassertions.AssertAll();
        }

        /// <summary>
        /// Test Case: (USER A) To verify HIPPA in spanish and decline TOS
        /// </summary>
        [Test, Order(2)]
        //[Category("ProdSanity")]
        //[Category("Regression")]
        public void TC_VerifySpanishHippa()
        {
            Common cmn = new Common();
            cmn.ClickConnectionsMenu();
            cn.VerifySpanishHippa();
            softassertions.AssertAll();

            // Decline HIPPA
            cn.DeclineHippa();
        }

        /// <summary>
        /// Test Case: (USER A) To convert language to english and verify and accept HIPPA
        /// </summary>
        [Test, Order(3)]
        //[Category("ProdSanity")]
        //[Category("Regression")]
        public void TC_ConvertToEnglish()
        {
            Common settings = new Common();
            settings.GoToSettings();
            Page_Settings myinf = new Page_Settings();
            myinf.ConvertToEnglish();

            Common cmn = new Common();
            cmn.ClickConnectionsMenu();

            //// Verify english HIPPA
            cn.VerifyHippaInEnglish();
            softassertions.AssertAll();

            //// Accept HIPPA
            cn.AcceptHippa();
        }

        /// <summary>
        /// Test Case: (USER A) To search for user and send connection request and logout
        /// </summary>
        [Test, Order(4)]
        public void TC_SendConnectionRequest()
        {
            Common cmn = new Common();
            cmn.ClickConnectionsMenu();

            // Search for user
            string emailid = ConfigurationManager.AppSettings["emailid"];
            string searchtext = cn.SearchUser(emailid);
            Assert.AreEqual(emailid, searchtext);

            // Send Connection request
            cn.VerifyUserRequest(ConfigurationManager.AppSettings["emailid"]);
            softassertions.AssertAll();

            Common logout = new Common();
            logout.LogOut();
        }

        /// <summary>
        /// Test Case: (USer B) Login with USER B and Accept HIPPA in english
        /// </summary>
        [Test, Order(5)]
        public void TC_VerifyAndAcceptHippa()
        {
            //cn = new Page_Connections(softassertions);

            string url = ConfigurationManager.AppSettings["baseurl"];
            string username = ConfigurationManager.AppSettings["username2"];
            string password = ConfigurationManager.AppSettings["password"];
            cn.LoginUser(url, username, password);

            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();

            Common cmn = new Common();
            cmn.ClickConnectionsMenu();

            //// Verify HIPPA in english
            cn.VerifyHippaInEnglish();
            cn.AcceptHippa();
        }

        /// <summary>
        /// Test Case: (USER B) To verify connection request recieved and decline the request
        /// </summary>
        [Test, Order(6)]
        public void TC_VerifyAndDeclineRequest()
        {
            string firstname = ConfigurationManager.AppSettings["firstname"];
            string lastname = ConfigurationManager.AppSettings["lastname"];
            string name = firstname + " " + lastname;
            string username = cn.GetUserFullName();
            Assert.AreEqual(name, username);

            // Decline request
            cn.ClickDeclineButton();
            Common logout = new Common();
            logout.LogOut();
        }

        /// <summary>
        /// Test Case: Login with USER A and resend request to USER B and logout
        /// </summary>
        [Test, Order(7)]
        public void TC_ResendRequest()
        {
            string url = ConfigurationManager.AppSettings["baseurl"];
            string username = ConfigurationManager.AppSettings["username"];
            string password = ConfigurationManager.AppSettings["password"];
            cn.LoginUser(url, username, password);
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();

            Common cmn = new Common();
            cmn.ClickConnectionsMenu();

            // Search user
            string emailid = ConfigurationManager.AppSettings["emailid"];
            string searchtext = cn.SearchUser(emailid);
            Assert.AreEqual(emailid, searchtext);

            // Send Request
            cn.VerifyUserRequest(ConfigurationManager.AppSettings["emailid"]);
            softassertions.AssertAll();
            Common logout = new Common();
            logout.LogOut();
        }

        /// <summary>
        /// Test Case: Login with USER B and accept request from USER A
        /// </summary>
        [Test, Order(8)]
        public void TC_AcceptRequest()
        {
            string url = ConfigurationManager.AppSettings["baseurl"];
            string username = ConfigurationManager.AppSettings["username2"];
            string password = ConfigurationManager.AppSettings["password"];
            cn.LoginUser(url, username, password);

            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();

            Common cmn = new Common();
            cmn.ClickConnectionsMenu();

            // Verify request recieved
            string firstname = ConfigurationManager.AppSettings["firstname"];
            string lastname = ConfigurationManager.AppSettings["lastname"];
            string name = firstname + " " + lastname;
            string uname = cn.GetUserFullName();
            Assert.AreEqual(name, uname);

            // Accept request
            cn.ClickAcceptButton();
        }

        /// <summary>
        /// Test Case: (USER B) To send message to USER A
        /// </summary>
        [Test, Order(9)]
        public void TC_SendMessage()
        {
            string sendmessage = cn.SendMessage(message);
            Assert.AreEqual(message, sendmessage);
            
            //Common logout = new Common();
            //logout.LogOut();
        }

        /// <summary>
        /// Test Case: (USER B) Update User A's connection circle and share trackers
        /// </summary>
        [Test, Order(10)]
        public void TC_ShareTrackers()
        {
            Common cmn = new Common();
            cmn.ClickConnectionsMenu();

            // Update user A's circle
            cn.ChangeConnectionCircle("inner");

            // Shares 2 trackers: 1 with outer circle and 1 with inner circle
            cn.ShareTrackers();

            //// Fill Tracker entries
            is_soft_assert = true;
            //Stress Tracker
            Common trackermenu = new Common();
            trackermenu.ClickTrackerMenu();

            Page_StressTracker stress = new Page_StressTracker(softassertions);
            stress.GoToStressTracker();
            stress.VerifyStressTracker();
            // Weight Tracker
            trackermenu.ClickTrackerMenu();

            Page_WeightTracker pWeightTracker = new Page_WeightTracker(softassertions);
            pWeightTracker.GoToWeightTracker();
            pWeightTracker.VerifyWeightTracker(GlobalVariables.clientname);

            softassertions.AssertAll();

            // Logout
            Common logout = new Common();
            logout.LogOut();
        }

        /// <summary>
        /// Test Case: (USER A) To verify message recieved from USER B
        /// </summary>
        [Test, Order(11)]
        public void TC_VerifyMessage()
        {
            string url = ConfigurationManager.AppSettings["baseurl"];
            string username = ConfigurationManager.AppSettings["username"];
            string password = ConfigurationManager.AppSettings["password"];
            cn.LoginUser(url, username, password);

            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();

            Common cmn = new Common();
            cmn.ClickConnectionsMenu();

            string recmessage = cn.VerifyMessage();
            Assert.AreEqual(message, recmessage);
        }

        /// <summary>
        /// Test Case: (USER A) To verify tracker shared by USER B visible
        /// </summary>
        [Test, Order(12)]
        public void TC_VerifySharedTracker()
        {
            Common cmn = new Common();
            cmn.ClickConnectionsMenu();

            // Click user profile to send request
            string firstname = ConfigurationManager.AppSettings["FirstNameUser2"];
            string lastname = ConfigurationManager.AppSettings["LastNameUser2"];
            string fullname = firstname + " " + lastname;

            cn.NavigateToConnectionProfile(fullname);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test, Order(13)]
        public void TC_DisconnectUser()
        {
            cn.DisconnectUser();
            softassertions.AssertAll();

            Common logout = new Common();
            logout.LogOut();
        }

        /// <summary>
        /// Test Case: (User B) Update USER A's circle and share trackers
        /// </summary>
        //[Test, Order(14)]
        public void TC_ShareTrackerETE()
        {
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();

            Common cmn = new Common();
            cmn.ClickConnectionsMenu();

            Page_Connections cn = new Page_Connections(softassertions);
            // Search for user
            string emailid = ConfigurationManager.AppSettings["emailid"];
            string searchtext = cn.SearchUser(emailid);
            Assert.AreEqual(emailid, searchtext);

            // Send Connection request
            cn.VerifyUserRequest(ConfigurationManager.AppSettings["emailid"]);
            softassertions.AssertAll();

            Common logout = new Common();
            logout.LogOut();

            // Login User B
            string url = ConfigurationManager.AppSettings["baseurl"];
            string username = ConfigurationManager.AppSettings["username2"];
            string password = ConfigurationManager.AppSettings["password"];
            cn.LoginUser(url, username, password);

            haprompt.GoToDashboard();
            cmn.ClickConnectionsMenu();

            // Verify request recieved
            string firstname = ConfigurationManager.AppSettings["firstname"];
            string lastname = ConfigurationManager.AppSettings["lastname"];
            string name = firstname + " " + lastname;
            string uname = cn.GetUserFullName();
            Assert.AreEqual(name, uname);

            // Accept request
            cn.ClickAcceptButton();

            // Update user A's circle
            cn.ChangeConnectionCircle("inner");

            // Shares 2 trackers: 1 with outer circle and 1 with inner circle
            cn.ShareTrackers();

            // User B Logout
            logout.LogOut();

            //User A Login
            plogin.Login();
            haprompt.GoToDashboard();
            cmn.ClickConnectionsMenu();

            // Click user profile to send request
            firstname = ConfigurationManager.AppSettings["FirstNameUser2"];
            lastname = ConfigurationManager.AppSettings["LastNameUser2"];
            string fullname = firstname + " " + lastname;
            cn.NavigateToConnectionProfile(fullname);
        }
    }
}
