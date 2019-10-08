using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.IOSPages.Login
{
    class Page_ILogin
    {
        String pageName;
        SoftAssertions softAssertions = null;
        string pckgname = ConfigurationManager.AppSettings["apppackage"];
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Mob.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_ILogin()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_ILogin(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }
        /// <summary>
        /// This method verifies user is at login page
        /// </summary>
        /// <returns></returns>
        public Boolean AtLoginPage()
        {
            bool status = AppiumKeywords.IsElementPresent(pageName, "loginbtn",pckgname);
            return status;
        }
        /// <summary>
        /// Verifies elements are displayed on login page
        /// </summary>
        /// <returns></returns>
        public void VerifyElements()
        {
            List<string[]> options = new List<string[]>();
            options = CSVReaderDataTable.GetCSVData("MobileLoginUI", pageName, "elements");
            for (int i = 0; i < options.Count; i++)
            {
                string elementname = options.ElementAt(i)[2];
                string locatorname = options.ElementAt(i)[3];
                bool status = AppiumKeywords.IsElementPresent(pageName, locatorname,pckgname);
                softAssertions.Add(" Element : " + elementname, true, status, "equals");
            }
        }
        /// <summary>
        /// This method sets the environment in the enivronment selector 
        /// </summary>
        private void SelectEnvironment()
        {
            AppiumKeywords.Tap(pageName, "envselector", pckgname);
            switch (ConfigurationManager.AppSettings["environment"].ToLower())
            {
                case "qa2012b":
                    AppiumKeywords.Tap(pageName, "envqa12b");
                    break;
                case "qa2012":
                    AppiumKeywords.Tap(pageName, "envqa12");
                    break;
                case "qb":
                    AppiumKeywords.Tap(pageName, "envqb");
                    break;
                case "staging":
                    AppiumKeywords.Tap(pageName, "envstage");
                    break;
                case "sa":
                    AppiumKeywords.Tap(pageName, "envstage");
                    break;
                case "qc":
                    AppiumKeywords.Tap(pageName, "envqc");
                    break;
            }

        }
        /// <summary>
        /// This method sets the value of username in User Name field
        /// </summary>
        /// <param name="username">User Name is read from LoginCredential.CSV file</param>
        private void SetUsername(string username)
        {
            AppiumKeywords.SetText(pageName, "usernametxt", username, pckgname);
        }

        /// <summary>
        /// This method sets the value of password in password field
        /// </summary>
        /// <param name="password">Password is read from LoginCredential.CSV file</param>
        private void SetPassword(string password)
        {
            AppiumKeywords.SetText(pageName, "passwordtxt" ,password, pckgname);
        }

        /// <summary>
        /// This method will click on Login Button
        /// It passes the locator and name of the login btn to SeKeyword click Btn 
        /// </summary>
        private void ClickLogin()
        {
            AppiumKeywords.Tap(pageName, "loginbtn", pckgname);
            Thread.Sleep(3000);
        }
        /// <summary>
        /// This method sets pin after login
        /// </summary>
        private void EnterPin()
        {
            Thread.Sleep(5000);
            for (int i = 1; i <= 2; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    AppiumKeywords.Tap(pageName, "pin");
                }
            }
            Thread.Sleep(2000);
        }
        /// <summary>
        /// Method to perform login
        /// </summary>
        public void moblogin()
        {
            //SelectEnvironment();
            string username = GlobalVariables.username;
            string password = GlobalVariables.password;
            Console.WriteLine(username + " , " + password);
            SelectEnvironment();
            SetUsername(username);
            SetPassword(password);
            ClickLogin();
            Thread.Sleep(3000);
            EnterPin();
            Thread.Sleep(3000);
        }
    }
}
