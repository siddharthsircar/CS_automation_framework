using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.Login
{
    class Page_Registration
    {
        String pageName;
        SoftAssertions softAssertion = null;
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_Registration()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_Registration(SoftAssertions softAssertion) : this()
        {
            this.softAssertion = softAssertion;
        }

        /// <summary>
        /// Click Get Started button to navigate to registration page
        /// </summary>
        public void ClickGetStarted()
        {
            if (GlobalVariables.environment.ToLower() == "sa" || GlobalVariables.environment.ToLower() == "staging" || GlobalVariables.environment.ToLower() == "prod" || GlobalVariables.environment.ToLower() == "production")
            {
                SeleniumKeywords.Click("Page_Login", "loginbtn");
                SeleniumKeywords.Click("Page_Login", "getstartedbtn");
            }
            else
            {
                SeleniumKeywords.Click("Page_Login", "olduigetstartedbtn");
            }
        }

        /// <summary>
        /// Verify whether user is at Get Started page
        /// </summary>
        /// <returns></returns>
        public Boolean AtGetStartedPage()
        {
            bool status = SeleniumKeywords.IsElementPresent(pageName, "pageheader");
            return status;
        }

        /// <summary>
        /// Click cancel button in Get Started 
        /// </summary>
        private void ClickCancelButton()
        {
            SeleniumKeywords.Click(pageName, "cancelbtn");
        }

        private void CancelButtonFunctionality()
        {
            bool status;
            ClickCancelButton();
            if (GlobalVariables.environment.ToLower() == "sa" || GlobalVariables.environment.ToLower() == "staging" || GlobalVariables.environment.ToLower() == "prod" || GlobalVariables.environment.ToLower() == "production")
            {
                status = SeleniumKeywords.IsElementPresent("Page_Login", "loginbtn");
            }
            else
            {
                status = SeleniumKeywords.IsElementPresent("Page_Login", "oldloginbtn");
            }
            softAssertion.Add("Login Button", true, status, "equals");
            ClickGetStarted();
        }

        private void ClickBackButton()
        {
            SeleniumKeywords.Click(pageName, "backbtn");
        }

        private void BackButtonFunctionality()
        {
            bool status;
            ClickBackButton();
            if (GlobalVariables.environment.ToLower() == "sa" || GlobalVariables.environment.ToLower() == "staging" || GlobalVariables.environment.ToLower() == "prod" || GlobalVariables.environment.ToLower() == "production")
            {
                status = SeleniumKeywords.IsElementPresent("Page_Login", "loginbtn");
            }
            else
            {
                status = SeleniumKeywords.IsElementPresent("Page_Login", "oldloginbtn");
            }
            softAssertion.Add("Login Button", true, status, "equals");
        }

        private void ClickVerifyButton()
        {
            SeleniumKeywords.Click(pageName, "verifybtn");
        }

        /// <summary>
        /// Verify UI elements on the getstarted page
        /// </summary>
        public void VerifyPageUI(List<String[]> elements)
        {
            for (int i = 0; i < elements.Count(); i++)
            {
                string locatorname = elements.ElementAt(i)[3];
                string elementname = elements.ElementAt(i)[2];
                
                bool actualstatus = SeleniumKeywords.IsElementPresent(pageName, locatorname);
                softAssertion.Add(elementname, true, actualstatus, "equals");
            }
        }

        /// <summary>
        /// Verify UI elements on the getstarted page
        /// </summary>
        public void VerifyPageUI()
        {
            Common cmn = new Common();
            string clientname = GlobalVariables.clientname.ToLower();
            string isSSO = cmn.GetConfig("SSO").ElementAt(0)[1].ToLower();

            if (isSSO.Equals("false") || clientname.Equals("health trust") || clientname.Equals("dollar general"))
            {
                List<String[]> uielements = CSVReaderDataTable.GetCSVData("RegistrationUIData", pageName, "nonsso_loginpref_uielem");
                for (int i = 0; i < uielements.Count(); i++)
                {
                    string locatorname = uielements.ElementAt(i)[3];
                    string elementname = uielements.ElementAt(i)[2];

                    bool actualstatus = SeleniumKeywords.IsElementPresent(pageName, locatorname);
                    softAssertion.Add(elementname, true, actualstatus, "equals");
                }
            }

            List<String[]> elements = CSVReaderDataTable.GetCSVData("RegistrationUIData", pageName, "loginpref_uielem");
            for (int i = 0; i < elements.Count(); i++)
            {
                string locatorname = elements.ElementAt(i)[3];
                string elementname = elements.ElementAt(i)[2];

                bool actualstatus = SeleniumKeywords.IsElementPresent(pageName, locatorname);
                softAssertion.Add(elementname, true, actualstatus, "equals");
            }
        }

        /// <summary>
        /// Verify user is navigated to login page on click of Cancel button and Back button
        /// </summary>
        public void VerifyButtonFunctionality()
        {
            CancelButtonFunctionality();
            BackButtonFunctionality();
        }

        /// <summary>
        /// Validate the error message when verify button is clicked without entering data
        /// </summary>
        public void ValidateBlankErrorMsgs(List<String[]> errmsgs)
        {
            ClickVerifyButton();
            for (int i = 0; i < errmsgs.Count(); i++)
            {
                string elementname = errmsgs.ElementAt(i)[2];
                string locatorname = errmsgs.ElementAt(i)[3];
                string expectedmsg = errmsgs.ElementAt(i)[4];
                string actualmsg = SeleniumKeywords.GetText(pageName, locatorname);
                softAssertion.Add(elementname, expectedmsg, actualmsg, "equals");
            }
            ClickBackButton();
        }

        private void EnterFirstName(string firstname)
        {
            SeleniumKeywords.SetText(pageName, "fntxtbx", firstname);
        }

        private void EnterLastName(string lastname)
        {
            SeleniumKeywords.SetText(pageName, "lntxtbx", lastname);
        }

        private void EnterDateofBirth(string dob)
        {
            SeleniumKeywords.SetText(pageName, "dobcalendar", dob);
        }

        private void EnterZipCode(string zip)
        {
            SeleniumKeywords.SetText(pageName, "ziptxtbx", zip);
        }

        /// <summary>
        /// Enter First name, last name, Dob and Zip Code
        /// </summary>
        private void EnterUserDetails(List<String> userdetails)
        {
            string firstname = userdetails.ElementAt(0);
            string lastname = userdetails.ElementAt(1);
            string dob = userdetails.ElementAt(2);
            string zip = userdetails.ElementAt(3);

            EnterFirstName(firstname);
            EnterLastName(lastname);
            EnterDateofBirth(dob);
            EnterZipCode(zip);
        }

        /// <summary>
        /// Enter NTID for OLH and G44 or SSN for DG
        /// </summary>
        private void EnterAdditionalDetails(string additional_info)
        {
            SeleniumKeywords.SetText(pageName, "additional_field", additional_info);
            SeleniumKeywords.Click(pageName, "additional_continuebtn");
        }

        /// <summary>
        /// Validate eligibility error displayed after incorrect data entry
        /// </summary>
        /// <param name="invaliddetails"></param>
        /// <param name="errormsg"></param>
        public void ValidateInvalidRegistration(List<String> invaliddetails, List<String[]> errormsg)
        {
            EnterUserDetails(invaliddetails);
            ClickVerifyButton();
            string locatorname = errormsg.ElementAt(0)[3];
            string expected = errormsg.ElementAt(0)[4];
            string actual = SeleniumKeywords.GetText(pageName, locatorname);
            softAssertion.Add("Eligibility Error Message", expected, actual, "Contains");
            ClickCancelButton();
        }

        /// <summary>
        /// Register user with valid details to navigate to User Agreement page
        /// </summary>
        /// <param name="validdetails"></param>
        /// <param name="clientconfig"></param>
        public void FillGetStartedDetails(List<String> validdetails, List<string[]> clientconfig)
        {
            string additionalField = clientconfig.ElementAt(0)[1];
            EnterUserDetails(validdetails);
            ClickVerifyButton();
            if (additionalField.ToLower().Equals("ntid"))
            {
                string ntid = validdetails.ElementAt(4);
                EnterAdditionalDetails(ntid);
            }
            else if (additionalField.ToLower().Equals("ssn"))
            {
                string ssn = validdetails.ElementAt(5);
                ssn = ssn.Substring(ssn.Length - 4, 4);
                EnterAdditionalDetails(ssn);
            }
        }

        /// <summary>
        /// Status of whether User Agreement displayed
        /// </summary>
        /// <returns>Agreement Header status</returns>
        public Boolean AtUserAgreement()
        {
            bool status = SeleniumKeywords.IsElementPresent(pageName, "agreementheader");
            return status;
        }

        /// <summary>
        /// Verify Buttons are displayed on the User Agreement Page
        /// </summary>
        private void VerifyUAButtonsDisplayed()
        {
            bool status;
            // Verify Cancel button displayed
            status = SeleniumKeywords.IsElementPresent(pageName, "uacancelbtn");
            softAssertion.Add("Cancel Button", true, status, "equals");

            //Verify Back Button displayed
            status = SeleniumKeywords.IsElementPresent(pageName, "uabackbtn");
            softAssertion.Add("Back Button", true, status, "equals");

            //Verify Continue button not displayed
            status = SeleniumKeywords.IsElementNotPresent(pageName, "uacontinuebtn");
            softAssertion.Add("Continue Button before Checkbox", false, status, "equals");

            //Click Agreement check box
            SeleniumKeywords.Click(pageName, "uacheckbx");

            //Verify Continue button displayed
            status = SeleniumKeywords.IsElementPresent(pageName, "uacontinuebtn");
            softAssertion.Add("Continue Button after Checkbox", true, status, "equals");
        }

        /// <summary>
        /// Click User Agreement Cancel Button
        /// </summary>
        private void ClickUACancelButton()
        {
            SeleniumKeywords.Click(pageName, "uacancelbtn");
        }

        /// <summary>
        /// Verify cancel button functionality
        /// </summary>
        private void VerifyUACancelFunctionality()
        {
            bool status;
            ClickUACancelButton();
            if (GlobalVariables.environment.ToLower() == "sa" || GlobalVariables.environment.ToLower() == "staging" || GlobalVariables.environment.ToLower() == "prod" || GlobalVariables.environment.ToLower() == "production")
            {
                status = SeleniumKeywords.IsElementPresent("Page_Login", "loginbtn");
            }
            else
            {
                status = SeleniumKeywords.IsElementPresent("Page_Login", "oldloginbtn");
            }
            softAssertion.Add("Login Button", true, status, "equals");
        }

        /// <summary>
        /// Click Back button in user agreement page
        /// </summary>
        private void ClickUABackButton()
        {
            SeleniumKeywords.Click(pageName, "uabackbtn");
        }

        /// <summary>
        /// Verify back button functionality
        /// </summary>
        private void VerifyUABackFunctionality()
        {
            ClickUABackButton();
            bool status = SeleniumKeywords.IsElementPresent(pageName, "pageheader");
            softAssertion.Add("Get Started Page Header", true, status, "equals");
            ClickCancelButton();
        }

        /// <summary>
        /// Click user agreement Continue button
        /// </summary>
        public void ClickUAContinue()
        {
            Thread.Sleep(3000);
            if (SeleniumKeywords.GetPageTitle().Contains("Agreement"))
            {
                SeleniumKeywords.Click(pageName, "uacheckbx");
                SeleniumKeywords.Click(pageName, "uacontinuebtn");
            }            
        }

        /// <summary>
        /// Verify buttons and functionality in UserAgreement page
        /// </summary>
        public void ValidateUserAgreementButtons(List<String> userdata, List<string[]> clientconfig)
        {
            // Verify Buttons in user agreement page
            VerifyUAButtonsDisplayed();
            // Click back button and verify functionality and click cancel on get started page
            VerifyUABackFunctionality();
            // Click Get Started on login page
            ClickGetStarted();
            //Enter User data
            FillGetStartedDetails(userdata, clientconfig);
            //Validate Cancel button functionality
            VerifyUACancelFunctionality();
        }
        /// <summary>
        /// Verify user is at Login Preference page
        /// </summary>
        /// <returns></returns>
        public Boolean AtLoginPreference()
        {
            bool status = SeleniumKeywords.IsElementPresent(pageName, "loginpref_header");
            return status;
        }

        /// <summary>
        /// Click Cancel button in login preference page
        /// </summary>
        public void ClickLoginPrefCancel()
        {
            Thread.Sleep(2000);
            SeleniumKeywords.Click(pageName, "loginpref_cancelbtn");
        }
        /// <summary>
        /// Click save and continue button on Login Preference
        /// </summary>
        public void ClickSaveAndContinue()
        {
            Thread.Sleep(2000);
            SeleniumKeywords.Click(pageName, "loginpref_continuebtn");
        }
        /// <summary>
        /// Verify error messages on Login Preference page
        /// </summary>
        /// <param name="errmsgs"></param>
        public void VerifyErrorMsgs(List<String[]> errmsgs)
        {
            ClickSaveAndContinue();
            Thread.Sleep(2000);
            for (int i = 0; i < errmsgs.Count(); i++)
            {
                string elementname = errmsgs.ElementAt(i)[2];
                string locatorname = errmsgs.ElementAt(i)[3];
                string expectedmsg = errmsgs.ElementAt(i)[4];
                string actualmsg = SeleniumKeywords.GetText(pageName, locatorname);
                softAssertion.Add(elementname, expectedmsg, actualmsg, "equals");
            }
            Thread.Sleep(2000);
            if (GlobalVariables.clientname.ToLower().Equals("nissan"))
            {
                //Click Login Preference Back button
                SeleniumKeywords.Click(pageName, "loginpref_backbtn");
                //Click User Agreement Cancel Button
                ClickUACancelButton();
            }
            else
            {
                //Click Login Preference Cancel Button
                ClickLoginPrefCancel();
            }
        }

        private void SetUsername()
        {
            SeleniumKeywords.SetText(pageName, "username_textbx", GlobalVariables.username);
        }

        private void SetPassword()
        {
            SeleniumKeywords.SetText(pageName, "password_textbx", GlobalVariables.password);
        }

        private void ConfirmPassword()
        {
            SeleniumKeywords.SetText(pageName, "confirmpwd_textbx", GlobalVariables.password);
        }

        private void EnterUsernamePassword()
        {
            SetUsername();
            SetPassword();
            ConfirmPassword();
        }
        /// <summary>
        /// Enter valid primary phone number
        /// </summary>
        private void EnterValidPrimPhNum()
        {
            SeleniumKeywords.SetText(pageName, "loginpref_primaryphnum", "9999999999");
        }
        /// <summary>
        /// Select Contact Preference radio
        /// </summary>
        private void SelectContactPref()
        {
            SeleniumKeywords.Click(pageName, "loginpref_contactprefT");
        }

        /// <summary>
        /// Enter Primary Phone Number and Select Contact Preference
        /// </summary>
        public void EnterRequiredFields(List<string[]> clientconfig)
        {
            string isSSO = clientconfig.ElementAt(0)[1];
            if (isSSO.ToLower().Equals("false"))
            {
                EnterUsernamePassword();
            }
            EnterValidPrimPhNum();
            SelectContactPref();
        }

        private void NavigateToDownloadAlwaysOnPage()
        {
            Common cmn = new Common();
            
            string isJourneyGrp = cmn.GetConfig("JourneyEnabled").ElementAt(0)[1];
            if (isJourneyGrp.ToLower().Equals("true"))
            {
                bool elementpresent = SeleniumKeywords.IsElementPresent("Page_Journey", "journeytile_gobtn", "Download", "Download");
                while (elementpresent != true)
                {
                    Thread.Sleep(3000);
                    //For Journey Clients: Click Journey Next Button
                    SeleniumKeywords.Click("Page_Journey", "journeyslidernextbtn");
                    Thread.Sleep(3000);
                    elementpresent = SeleniumKeywords.IsElementPresent("Page_Journey", "journeytile_gobtn", "Download", "Download");
                    Console.WriteLine("Element Status: " + elementpresent);
                }
                //For Journey Clients: Click Download app Recommendation Tile CTA
                SeleniumKeywords.Click("Page_Journey", "journeytile_gobtn", "Download", "Download"); 
            }
            else
            {
                bool elementpresent = SeleniumKeywords.IsElementPresent("Common", "downloadapp_btn");
                while (elementpresent != true)
                {
                    Thread.Sleep(3000);
                    //For Journey Clients: Click Journey Next Button
                    SeleniumKeywords.Click("Common", "marketingtilenext_btn");
                    Thread.Sleep(3000);
                    elementpresent = SeleniumKeywords.IsElementPresent("Common", "downloadapp_btn");
                    Console.WriteLine("Element Status: " + elementpresent);
                }
                //For Journey Clients: Click Download app Recommendation Tile CTA
                SeleniumKeywords.Click("Common", "downloadapp_btn");
            }
        }

        /// <summary>
        /// Set valid mobile number in Mobile Field
        /// </summary>
        private void SetMobileNumber()
        {
            string mobilenumber = CommonUtilityKeywords.RandomUSNumberGenerator();
            SeleniumKeywords.SetText(pageName, "alwayson_phnnum_txtbx", mobilenumber);
        }

        /// <summary>
        /// Check accpet TOS checkbox
        /// </summary>
        private void CheckTOSAccept()
        {
            SeleniumKeywords.Click(pageName, "alwayson_tos_chckbx");
            SeleniumKeywords.Click("Common", "tosagree_btn");
        }

        /// <summary>
        /// Click Download link button
        /// </summary>
        private void ClickSendDownloadLinkBtn()
        {
            SeleniumKeywords.Click(pageName, "alwayson_send_btn");
            SeleniumKeywords.Click(pageName, "alwayson_send_btn");
        }

        /// <summary>
        /// Method registers SSO users from Doanload App page
        /// Navigates user to Download AlwaysOn page
        /// Enters Username
        /// Enters Password
        /// Enters US Number
        /// Clicks on Send download link button
        /// </summary>
        /// <param name="clientconfig"></param>
        public void CreateUsernamePassword()
        {
            NavigateToDownloadAlwaysOnPage();
            SetUsername();
            SetPassword();
            SetMobileNumber();
            CheckTOSAccept();
            ClickSendDownloadLinkBtn();
        }
    }
}
