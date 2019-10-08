using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages.Login;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace AutomationFramework.Tests.WebTests.Registration
{
    /// <summary>
    /// Test Class: Registration
    /// </summary>
    [TestFixture]
    [Order(2)]
    public class Registration : Base
    {
        String pageName;
        List<string> userinfo;        
        Common config = null;
        List<string[]> isSSO;
        //List<string[]> JourneyEnabled;
        List<string[]> additionalDetails;

        /// <summary>
        /// Initializes pageName with class name
        /// </summary>
        public Registration()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
            userinfo = new List<string>();
            isSSO = new List<string[]>();
            additionalDetails = new List<string[]>();
        }

        /// <summary>
        /// Test Case: Verify UI Elements and Button functionality of Getting Started page
        /// </summary>
        [Test, Order(1)]
        [Category("AllClientRegistration")]
        public void TC_VerifyGetStartedPage()
        {
            config = new Common();
            //Navigate to Get Started Page
            Page_Registration reg = new Page_Registration(softassertions);
            reg.ClickGetStarted();

            //Assert: User at Get Started Page
            Assert.IsTrue(reg.AtGetStartedPage(), "User not redirected to Get Started Page");
            List<String[]> elements = CSVReaderDataTable.GetCSVData("RegistrationUIData", pageName, "uielements");

            is_soft_assert = true;

            //Verify UI elments
            reg.VerifyPageUI(elements);

            // Verify Cancel and Back button functionality
            reg.VerifyButtonFunctionality();
            softassertions.AssertAll();
        }

        /// <summary>
        /// Test Case: Verify Blank field error messages
        /// </summary>
        [Test, Order(2)]
        [Category("AllClientRegistration")]
        public void TC_VerifyBlankErrorMsgs()
        {
            //Navigate to Get Started Page
            Page_Registration reg = new Page_Registration(softassertions);
            reg.ClickGetStarted();

            is_soft_assert = true;

            //Validate blank field errors
            List<String[]> errmsgs = CSVReaderDataTable.GetCSVData("RegistrationUIData", pageName, "errormsg");
            reg.ValidateBlankErrorMsgs(errmsgs);

            softassertions.AssertAll();
        }

        /// <summary>
        /// Test Case: Verify registration with invalid data
        /// </summary>
        [Test, Order(3)]
        [Category("AllClientRegistration")]
        public void TC_VerifyInvalidRegistration()
        {
            //Navigate to Get Started Page
            Page_Registration reg = new Page_Registration(softassertions);
            reg.ClickGetStarted();

            List<String[]> invaliddata = CSVReaderDataTable.GetCSVData("InputDataContent", pageName, "invaliduserdata");
            List<string> userinfo = new List<string>();
            userinfo.Add(invaliddata.ElementAt(0)[4]);
            userinfo.Add(invaliddata.ElementAt(1)[4]);
            userinfo.Add(invaliddata.ElementAt(2)[4]);
            userinfo.Add(invaliddata.ElementAt(3)[4]);

            List<String[]> expectederr = CSVReaderDataTable.GetCSVData("RegistrationUIData", pageName, "eligibilityerror");
            is_soft_assert = true;

            //Validate Error text on invalid data entry
            reg.ValidateInvalidRegistration(userinfo, expectederr);
            softassertions.AssertAll();
        }

        /// <summary>
        /// Test Case: Verify functionality of buttons on User Agreement page
        /// </summary>
        [Test, Order(4)]
        [Category("AllClientRegistration")]
        public void TC_VerifyUserAgreement()
        {
            //Navigate to Get Started Page
            Page_Registration reg = new Page_Registration(softassertions);
            reg.ClickGetStarted();

            userinfo.Add(GlobalVariables.firstname);
            userinfo.Add(GlobalVariables.lastname);
            userinfo.Add(GlobalVariables.dob);
            userinfo.Add(GlobalVariables.zipcode);
            userinfo.Add(GlobalVariables.popseg1);
            userinfo.Add(GlobalVariables.ssn);

            additionalDetails = config.GetConfig("AdditionalField");
            reg.FillGetStartedDetails(userinfo, additionalDetails);
            Assert.IsTrue(reg.AtUserAgreement(), "User Agreement Page not displayed");

            is_soft_assert = true;
            reg.ValidateUserAgreementButtons(userinfo, additionalDetails);
            softassertions.AssertAll();
        }

        /// <summary>
        /// Test Case: Verify login preference page
        /// </summary>
        [Test, Order(5)]
        [Category("AllClientRegistration")]
        public void TC_VerifyLoginPreference()
        {
            
            //Navigate to Get Started Page
            Page_Registration reg = new Page_Registration(softassertions);
            reg.ClickGetStarted();

            userinfo.Add(GlobalVariables.firstname);
            userinfo.Add(GlobalVariables.lastname);
            userinfo.Add(GlobalVariables.dob);
            userinfo.Add(GlobalVariables.zipcode);
            userinfo.Add(GlobalVariables.popseg1);
            userinfo.Add(GlobalVariables.ssn);

            // Enter valid details in GetStarted Page
            reg.FillGetStartedDetails(userinfo, additionalDetails);
            reg.ClickUAContinue();

            Assert.IsTrue(reg.AtLoginPreference(), "Login Preference Page not displayed");

            is_soft_assert = true;
            
            //Verify Login Prefrence elements
            reg.VerifyPageUI();

            //Verify error messages
            List<String[]> errmsgs = CSVReaderDataTable.GetCSVData("RegistrationUIData", "Registration", "loginpref_errormsg");
            reg.VerifyErrorMsgs(errmsgs);

            softassertions.AssertAll();
        }

        /// <summary>
        /// Test Case: Verify user is registered successfully
        /// </summary>
        [Test, Order(6)]
        [Category("AllClientRegistration")]
        public void TC_VerifySuccessfullRegistration()
        {
            is_soft_assert = false;

            //Navigate to Get Started Page
            Page_Registration reg = new Page_Registration(softassertions);
            reg.ClickGetStarted();

            userinfo.Add(GlobalVariables.firstname);
            userinfo.Add(GlobalVariables.lastname);
            userinfo.Add(GlobalVariables.dob);
            userinfo.Add(GlobalVariables.zipcode);
            userinfo.Add(GlobalVariables.popseg1);
            userinfo.Add(GlobalVariables.ssn);


            // Enter details in get started page
            reg.FillGetStartedDetails(userinfo, additionalDetails);

            // Click User Agreement next button
            reg.ClickUAContinue();

            isSSO = config.GetConfig("SSO");

            //Update required fields
            reg.EnterRequiredFields(isSSO);

            //Click Save and Continue
            reg.ClickSaveAndContinue();

            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();

            Page_Dashboard Dashboard = new Page_Dashboard();

            //Verify At Dashboard page
            Assert.IsTrue(Dashboard.AtDashboard());

            Common cmn = new Common();

            string sso = isSSO.ElementAt(0)[1];
            TestContext.WriteLine("SSO: " + sso);
            if (sso.ToLower().Equals("true"))
            {
                //Create User for SSO Clients
                reg.CreateUsernamePassword();
            }

            //Log Out
            //Common cmn = new Common();
            cmn.LogOut();
        }
    }
}
