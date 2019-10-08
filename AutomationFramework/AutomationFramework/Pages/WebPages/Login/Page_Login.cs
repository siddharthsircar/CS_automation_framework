using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace AutomationFramework.Pages
{
    /// <summary>
    /// Login page class
    /// </summary>
    public class Page_Login
    {
        String pageName;
        SoftAssertions softAssertion = null;

        List<string[]> uielementdata = new List<string[]>();

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_Login()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_Login(SoftAssertions softAssertion) : this()
        {
            this.softAssertion = softAssertion;
        }
        /// <summary>
        /// This method retun the text of the UI element and verify with the text specified in CommonContent file 
        /// </summary>
        /// <returns>Return True if text match with text specified in csv file </returns>
        public List<string[]> VerifyLoginUIText()
        {
            uielementdata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "elementuitxt");
            Console.WriteLine(uielementdata.Count);
            List<string[]> result = new List<string[]>();
            for (int i = 0; i < uielementdata.Count; i++)
            {
                string elementname = uielementdata.ElementAt(i)[2];
                string elementlocatorname = uielementdata.ElementAt(i)[3];
                string expelementtxt = uielementdata.ElementAt(i)[4];
                string actualelementtxt = SeleniumKeywords.GetText(pageName, elementlocatorname);

                bool textmatch = SeleniumKeywords.VerifyText(actualelementtxt, expelementtxt);
                string msg = "Element : " + elementname + " , Expected : " + expelementtxt + " , Actual : " + actualelementtxt;
                result.Add(new string[] { msg, textmatch.ToString() });
                Console.WriteLine(msg);
            }
            return result;
        }

        /// <summary>
        /// This method retrun the Placeholder value of Username and Password input box
        /// </summary>
        /// <returns>"Username" and "Password"</returns>
        public List<string[]> VerifyLoginUIAttribute()
        {
            uielementdata = CSVReaderDataTable.GetCSVData("AttributesContent", pageName, "elementuiattr");
            List<string[]> result = new List<string[]>();
            for (int i = 0; i < uielementdata.Count; i++)
            {
                string elementname = uielementdata.ElementAt(i)[2];
                string elementlocatorname = uielementdata.ElementAt(i)[3];
                string expelementtxt = uielementdata.ElementAt(i)[4];
                string expelementattr = uielementdata.ElementAt(i)[5];
                string actualelementtxt = SeleniumKeywords.GetAttributeValue(pageName, elementlocatorname, expelementattr);
                //string actualelementtxt = SeKeywords.GetText(ElementLocator.GetLocator(pageName, elementlocatorname), elementname);

                bool textmatch = SeleniumKeywords.VerifyText(actualelementtxt, expelementtxt);
                string msg = "Element : " + elementname + " , Attribute" + actualelementtxt + " , Expected : " + expelementtxt + " , Actual : " + actualelementtxt;
                result.Add(new string[] { msg, textmatch.ToString() });
                Console.WriteLine(msg);
            }
            return result;
        }

        /// <summary>
        /// This method sets the value of username in User Name field
        /// </summary>
        /// <param name="username">User Name is read from LoginCredential.CSV file</param>
        private void SetUsername(string username)
        {
            SeleniumKeywords.SetText(pageName, "usernametxt", username);
        }
        private void SetcUsername(string username)
        {
            SeleniumKeywords.SetText(pageName, "cusernametxt", username);
        }/// <summary>
        /// set user name on forgot password screen
        /// </summary>
        /// <param name="username"></param>
        private void SetpUsername(string username)
        {
            SeleniumKeywords.SetText(pageName, "forgotpass_username", username);
        }

        /// <summary>
        /// This method sets the value of password in password field
        /// </summary>
        /// <param name="password">Password is read from LoginCredential.CSV file</param>
        private void SetPassword(string password)
        {
            SeleniumKeywords.SetText(pageName, "passwordtxt", password);
        }

        /// <summary>
        /// Enter Password in Custom URL
        /// </summary>
        /// <param name="password"></param>
        private void SetcPassword(string password)
        {
            SeleniumKeywords.SetText(pageName, "cpasswordtxt", password);
        }

        /// <summary>
        /// This method will click on Login Button
        /// It passes the locator and name of the login btn to SeKeyword click Btn 
        /// </summary>
        private void ClickLogin()
        {
            if (GlobalVariables.environment.ToLower() == "sa" || GlobalVariables.environment.ToLower() == "staging" || GlobalVariables.environment.ToLower() == "prod" || GlobalVariables.environment.ToLower() == "production")
            {
                SeleniumKeywords.Click(pageName, "loginsubmitbtn");
            }
            else
            {
                SeleniumKeywords.Click(pageName, "oldloginbtn");
            }
        }

        /// <summary>
        /// Click on Custom URL Login button
        /// </summary>
        private void ClickOncLogin()
        {
           SeleniumKeywords.Click(pageName, "custurlloginbtn");
        }

        private void ClickSubmitLoginBtn()
        {            
            SeleniumKeywords.Click("Page_Login", "loginbtn");
        }

        /// <summary>
        /// This method will click on Login Button Image
        /// This method Sikuli Keyword 
        /// It passes the locator and name of the login btn to SeKeyword click Btn 
        /// </summary>
        private void ClickLoginSikuli()
        {
            SikuliKeywords.Click(pageName, "loginbtnImg");
            System.Threading.Thread.Sleep(5000);
        }

        /// <summary>
        /// This method will verify the onlife logo Image
        /// This method Sikuli Keyword 
        /// It passes the locator and name of the login btn to SeKeyword click Btn 
        /// </summary>
        private void VerifyOnlifeLog()
        {
            SikuliKeywords.ImageVisible(pageName, "OnlifelogoImg");
        }

        /// <summary>
        /// This method return the Error Message if invalid user name and password is provided
        /// </summary>
        /// <returns></returns>
        public string InvalidUsernamePasswordErrorMsg()
        {
            string emgs = SeleniumKeywords.GetText(pageName, "invalidloginmsg");
            return emgs;
        }

        /// <summary>
        /// Method performs login by calling SetUsername and SetPassword methods
        /// </summary>
        public void Login()
        {
            if (GlobalVariables.environment.ToLower() == "sa" || GlobalVariables.environment.ToLower() == "staging" || GlobalVariables.environment.ToLower() == "prod" || GlobalVariables.environment.ToLower() == "production")
            {
                SeleniumKeywords.Click("Page_Login", "loginbtn");
            }
            string username = GlobalVariables.username;
            string password = GlobalVariables.password;
            Console.WriteLine(username + " , " + password);
            SetUsername(username);
            SetPassword(password);
            ClickLogin();
        }

        /// <summary>
        /// Method performs login by calling SetUsername and SetPassword methods
        /// </summary>
        public void CustomURLLogin()
        {
           
            string username = GlobalVariables.username;
            string password = GlobalVariables.password;
            Console.WriteLine(username + " , " + password);
            SetcUsername(username);
            SetcPassword(password);
            ClickOncLogin();
        }

        /// <summary>
        /// Function used in Connections TC for login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void Login(string username, string password)
        {
            if (GlobalVariables.environment.ToLower() == "sa" || GlobalVariables.environment.ToLower() == "staging" || GlobalVariables.environment.ToLower() == "prod" || GlobalVariables.environment.ToLower() == "production")
            {
                ClickSubmitLoginBtn();
            }            
            SetUsername(username);
            SetPassword(password);
            ClickLogin();
        }

        ///<summary>
        ///Method performs login with invalid username and password
        ///</summary>
        public string InvaidLogin()
        {
            if (GlobalVariables.environment.ToLower() == "sa" || GlobalVariables.environment.ToLower() == "staging" || GlobalVariables.environment.ToLower() == "prod" || GlobalVariables.environment.ToLower() == "production")
            {
                SeleniumKeywords.Click("Page_Login", "loginbtn");
            }
            //VerifyOnlifeLog();
            SetUsername("abc234567");
            SetPassword("Password1");
            //ClickLoginSikuli();
            ClickLogin();
            string errmsg = InvalidUsernamePasswordErrorMsg();
            return errmsg;
        }

        /// <summary>
        /// Method is use to verify the Login screen UI elements 
        /// </summary>
        /// <returns>Retun true or false along with a message print in report</returns>
        public List<string[]> VerifyLoginUIElements()
        {
            if (GlobalVariables.environment.ToLower() == "sa" || GlobalVariables.environment.ToLower() == "staging" || GlobalVariables.environment.ToLower() == "prod" || GlobalVariables.environment.ToLower() == "production")
            {
                SeleniumKeywords.Click("Page_Login", "loginbtn");
            }
            List<string[]> resultTxt = VerifyLoginUIText();
            List<string[]> resultAttr = VerifyLoginUIAttribute();
            resultTxt.AddRange(resultAttr);
            //resultTxt.(resultTxt);
            return resultTxt;
        }
      

        private void clickOnForgotUserName()

        {
            System.Threading.Thread.Sleep(2000);
            SeleniumKeywords.Click(pageName, "forgotunameoptionbtn");
        }

        private void clickOnForgotPassword()
        {
            System.Threading.Thread.Sleep(2000);
            SeleniumKeywords.Click(pageName, "forgotpassowrdoptionbtn");
        }
        private void clickOnConfirmBtn()
        {
            System.Threading.Thread.Sleep(2000);
            SeleniumKeywords.Click(pageName, "forgotunameconfirm");
        }
        private void fillUserDetails()
        {
            SeleniumKeywords.SetText(pageName, "forgotuname_firstname", GlobalVariables.firstname);
            SeleniumKeywords.SetText(pageName, "forgotuname_lastname", GlobalVariables.lastname);
            SeleniumKeywords.SetText(pageName, "forgotuname_dob", GlobalVariables.dob);
            SeleniumKeywords.SetText(pageName, "forgotuname_zipcode", GlobalVariables.zipcode);
                      

        }
        private void clickOnVerifyBtn()
        {
            SeleniumKeywords.Click(pageName, "forgotuname_verifybtn");
        }
        private string getUserName()
        {
            string un;
            un = SeleniumKeywords.GetText(pageName, "forgotuname_lbl");
            return un;
        }

        private string getEmailId()
        {
            string eid;
            SeleniumKeywords.IsElementPresent(pageName, "forgotuname_emailidtxt");
            eid = SeleniumKeywords.GetText(pageName, "forgotuname_emailid");
            return eid;
        }
        private void setPassword()
            {
                SeleniumKeywords.SetText(pageName, "forgotuname_passtxt", "Password1");
            }
        /// <summary>
        /// call by forgot username test case
        /// </summary>
        public string forgotUserName()
        {
            if (GlobalVariables.environment.ToLower() == "sa" || GlobalVariables.environment.ToLower() == "staging" || GlobalVariables.environment.ToLower() == "prod" || GlobalVariables.environment.ToLower() == "production")
            {
                SeleniumKeywords.Click(pageName, "loginbtn");
            }
            SeleniumKeywords.Click(pageName, "forgotunamepasslnk");
            if (GlobalVariables.environment.ToLower() == "sa" || GlobalVariables.environment.ToLower() == "staging" || GlobalVariables.environment.ToLower() == "prod" || GlobalVariables.environment.ToLower() == "production")
            {
                SeleniumKeywords.SwitchToTab(2);
            }
            clickOnForgotUserName();
            clickOnConfirmBtn();
            fillUserDetails();
            clickOnVerifyBtn();
            string username = getUserName();
            setPassword();
            clickOnVerifyBtn();
            return username;


        }

        /// <summary>
        /// call by forgot Password test case
        /// </summary>
        public void forgotPassword()
        {
            if (GlobalVariables.environment.ToLower() == "sa" || GlobalVariables.environment.ToLower() == "staging" || GlobalVariables.environment.ToLower() == "prod" || GlobalVariables.environment.ToLower() == "production")
            {
                SeleniumKeywords.Click(pageName, "loginbtn");
            }
            SeleniumKeywords.Click(pageName, "forgotunamepasslnk");
            if (GlobalVariables.environment.ToLower() == "sa" || GlobalVariables.environment.ToLower() == "staging" || GlobalVariables.environment.ToLower() == "prod" || GlobalVariables.environment.ToLower() == "production")
            {
                SeleniumKeywords.SwitchToTab(2);
            }
            clickOnForgotPassword();
            clickOnConfirmBtn();
            SetpUsername(GlobalVariables.username);
            clickOnVerifyBtn();
            softAssertion.Add("Email header",true,emailheaderPresent(),"equals");
            string eid = getEmailId();
            softAssertion.Add("Email id of user", GlobalVariables.email, eid, "contains");
            clickOnOK();
            softAssertion.Add("Email id of user","Onlife", getHomePageTitle(), "contains");
        }
        private Boolean emailheaderPresent()
        {
            return(SeleniumKeywords.IsElementPresent(pageName, "forgotuname_emailidtxt"));
        }
        
        private void clickOnOK()
        {
            SeleniumKeywords.Click(pageName, "forgotpass_okbtn");
         
        }
        private string getHomePageTitle()
        {
            return (SeleniumKeywords.GetPageTitle());
        }
            
    }
}
