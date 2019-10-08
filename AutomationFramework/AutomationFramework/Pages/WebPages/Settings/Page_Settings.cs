using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationFramework.Pages
{
    class Page_Settings
    {
        String pageName;
        SoftAssertions softAssertions = null;
        List<string[]> errordata = new List<string[]>();
        List<string[]> mperrordata = new List<string[]>();
        List<string[]> updatedata = new List<string[]>();
        List<string[]> attributedata = new List<string[]>();

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_Settings()
        {
            pageName = this.GetType().Name;                     //Gets the name of the class
            Console.WriteLine("Current class : " + pageName);   // Prints the name of class on console
        }

        public Page_Settings(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }

        /// <summary>
        /// This is used to click on the edit btn
        /// Edit button is present in the my information section in settings
        /// </summary>
        private void ClickMyInfoEditBtn()
        {
            //System.Threading.Thread.Sleep(2000);                                        
            SeleniumKeywords.Click(pageName, "myinfoeditbtn");  //Click the Edit Btn using selenium keyword class
        }

        /// <summary>
        /// This method select the spanish language from language drop down from settings
        /// </summary>
        private void SelectSpanishLanguageFromDD()
        {
            //System.Threading.Thread.Sleep(2000);
            SeleniumKeywords.SelectValueFromDropdown(pageName, "language", "Español");
        }

        /// <summary>
        /// This method select the spanish language from language drop down from settings
        /// </summary>
        private void SelectEnglishLanguageFromDD()
        {
            //System.Threading.Thread.Sleep(2000);
            SeleniumKeywords.SelectValueFromDropdown(pageName, "language", "English");
        }

        /// <summary>
        /// This is used to click My profile edit btn
        /// </summary>
        private void ClickMyProfEditBtn()
        {
            //System.Threading.Thread.Sleep(2000);
            SeleniumKeywords.Click(pageName, "myproeditbtn");
        }

        /// <summary>
        /// This method clears the text from Online Display Name text box
        /// </summary>
        private void ClearOnlineDisplayName()
        {
            SeleniumKeywords.PressKey(pageName, "oldispnametb","DeleteAllText");
        }

        /// <summary>
        /// This method clears the email id text field
        /// </summary>
        private void ClearEmailId()
        {
            SeleniumKeywords.PressKey(pageName, "emailtb", "DeleteAllText");
        }

        /// <summary>
        /// This method clears the text from First Name text Box
        /// </summary>
        private void ClearFirstNameTxtBox()
        {
           
            SeleniumKeywords.PressKey(pageName, "firstnametb", "DeleteAllText");
        }

        /// <summary>
        /// This method clears the Primary phone number text field
        /// </summary>
        private void ClearPrimaryPhoneNumber()
        {
            SeleniumKeywords.PressKey(pageName, "primphnotb", "DeleteAllText");  
        }

        /// <summary>
        /// This method clears the Secondary Phone number text field
        /// </summary>
        private void ClearSecondaryPhoneNumber()
        {
            SeleniumKeywords.PressKey(pageName, "secphnotb", "DeleteAllText");
        }

        /// <summary>
        /// This method click on the Save button of My Profile Section
        /// </summary>
        private void ClickMyProfSaveBtn()
        {
            SeleniumKeywords.Click(pageName, "myprofsavebtn");
            System.Threading.Thread.Sleep(2000);
        }

        /// <summary>
        /// This method clicks on the Save Btn
        /// Save Btn appears once edit btn is clicked in my information part in settings
        /// </summary>
        private void ClickSaveBtn()
        {
            SeleniumKeywords.Click(pageName, "savebtn");
            System.Threading.Thread.Sleep(2000);
        }

        /// <summary>
        /// This method validate all the blank error messages(Online display name and Email id) in My profile section
        /// </summary>
        /// <returns></returns>
        private void ValidateMyProfBlankErroMsg()
        {
            List<string[]> result = new List<string[]>();
            mperrordata = CSVReaderDataTable.GetCSVData("CommonContent",pageName,"mperrormsg");
            ClickMyProfEditBtn();
            ClearOnlineDisplayName();
            ClearEmailId();
            for(int i=0;i<mperrordata.Count;i++)
            {
                string elementname = mperrordata.ElementAt(i)[2];
                string elementlocatorname = mperrordata.ElementAt(i)[3];
                string errortext = mperrordata.ElementAt(i)[4];
                string actualerrortext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                softAssertions.Add("Element : " + mperrordata.ElementAt(i)[2], errortext, actualerrortext, "equals");
            }
        }

        /// <summary>
        /// This method verify all the error messages(First Name, Primary Phone no, Secondary Phone No)
        /// First it will call all the methods which will blank the text fields
        /// </summary>
        private void ValidateBlankFieldsErrorMsg()
        {
            List<string[]> result = new List<string[]>();
            errordata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "errormsg");

            ClickMyInfoEditBtn();
            ClearFirstNameTxtBox();
            ClearPrimaryPhoneNumber();
            ClearSecondaryPhoneNumber();

            for (int i = 0; i < errordata.Count; i++)
            {
                string elementname = errordata.ElementAt(i)[2];
                string elementlocatorname = errordata.ElementAt(i)[3];
                string experrortext = errordata.ElementAt(i)[4];
                if ((i == errordata.Count-1) && (GlobalVariables.clientname.ToLower().Equals("meabt")))
                {
                    //
                }
                else
                {
                    string actualerrortext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                    softAssertions.Add("Element : " + errordata.ElementAt(i)[2], experrortext, actualerrortext, "equals");
                }
            }
        }

        /// <summary>
        /// This method validate the updated data in Onlinedisplayname and Email id
        /// </summary>
        /// <returns></returns>
        private void ValidateUpdateMyProfData()
        {
            updatedata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "pdata");
            List<string[]> result = new List<string[]>();
            for(int i=0;i<updatedata.Count;i++)
            {
                string updatetext = SeleniumKeywords.GetText(pageName, updatedata.ElementAt(i)[3]);
                softAssertions.Add("Element : " + updatedata.ElementAt(i)[2], updatedata.ElementAt(i)[4], updatetext, "equals");
            }
            System.Threading.Thread.Sleep(5000);
        }

        private void ValidateSpanishConversion()
        {
            updatedata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "spdata");
            List<string[]> result = new List<string[]>();
            System.Threading.Thread.Sleep(5000);
            for (int i=0 ; i<updatedata.Count;i++)
            {
                string elementname = updatedata.ElementAt(i)[2];
                string expectedtext = updatedata.ElementAt(i)[4];
                string locatorname = updatedata.ElementAt(i)[3];
                Console.WriteLine(updatedata.Count);
                string updatetext = SeleniumKeywords.GetText(pageName, locatorname);
                softAssertions.Add("Element : " + elementname, expectedtext, updatetext, "contains");
            }
        }

        /// <summary>
        /// This method validate updated data(First Name, Primary Phone no, Secondary Phone No) in my information field
        /// </summary>
        /// <returns></returns>
        private void ValidateUpdatedData()
        {
            updatedata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "data");
            List<string[]> result = new List<string[]>();
            for(int i=0;i<updatedata.Count;i++)
            {
                string updatetext = SeleniumKeywords.GetText(pageName, updatedata.ElementAt(i)[3]);
                softAssertions.Add("Element : " + updatedata.ElementAt(i)[2], updatedata.ElementAt(i)[4], updatetext, "equals");
            }
            System.Threading.Thread.Sleep(2000);
        }

        /// <summary>
        /// This method validate attributes value(readonly property of city, zip and disable property of state) of control
        /// </summary>
        /// <returns></returns>
        public void ValidateAttributeValue()
        {
            attributedata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "property");
            List<string[]> result = new List<string[]>();
            for(int i=0;i<attributedata.Count;i++)
            {
                String attvalue = SeleniumKeywords.GetAttributeValue(pageName, attributedata.ElementAt(i)[3],attributedata.ElementAt(i)[4]);
                bool attributevalue = Convert.ToBoolean(attvalue);
                if (GlobalVariables.clientname.ToLower().Equals("meabt"))
                {
                    softAssertions.Add("Element : " + attributedata.ElementAt(i)[2], false, attributevalue, "equals");
                }
                else if (GlobalVariables.clientname.ToLower().Equals("health trust"))
                {
                    softAssertions.Add("Element : " + attributedata.ElementAt(i)[2], true, attributevalue, "equals");
                }
            }
        }

        /// <summary>
        /// This method sets the value in Online Display Name field in My Profile Section
        /// </summary>
        private void InputOnlineDisplayName()
        {
            SeleniumKeywords.SetText(pageName, "oldispnametb", "Automation");
        }

        /// <summary>
        /// This method sets the value in Email id field
        /// </summary>
        private void InputEmailId()
        {
            SeleniumKeywords.SetText(pageName, "emailtb", "testautomation@onlifehealth.com");
        }

        /// <summary>
        /// This will set the First Name
        /// </summary>
        private void InputFirstName()
        {
            SeleniumKeywords.SetText(pageName, "firstnametb", "Autobots");
        }

        /// <summary>
        /// This will set the Primary Phone no
        /// </summary>
        private void InputPrimaryPh()
        {
            SeleniumKeywords.SetText(pageName, "primphnotb", "9999999999");
        }

        /// <summary>
        /// This will set the Secondary Phone no
        /// </summary>
        private void InputSecondaryPh()
        {
            SeleniumKeywords.SetText(pageName, "secphnotb", "9999999999");
        }

        /// <summary>
        /// This keyword sets the value of all the fields in My profile section
        /// It calls InputOnlineDisplayName and InputEmailId
        /// </summary>
        private void InputMyProfileData()
        {
            InputOnlineDisplayName();
            InputEmailId();
            System.Threading.Thread.Sleep(2000);
        }

        /// <summary>
        /// This keyword input data in my information section
        /// This calls FirstName, PrimaryPhone, SecondayPhone No method
        /// </summary>
        private void InputData()
        {
            InputFirstName();
            InputPrimaryPh();
            InputSecondaryPh(); 
        }

        /// <summary>
        /// This method verify My profile text box(Online display name and email id) blank errors 
        /// </summary>
        /// <returns>Returns the list of errors</returns>
        public void VerifyMyProfileErrors()
        {
            ValidateMyProfBlankErroMsg();
        }

        /// <summary>
        /// This method verify the blank error message in my information section
        /// error message appers in first name, primary phone no, secondary phone no
        /// </summary>
        /// <returns>returns the list of errors</returns>
        public void VerifyMyInformationSetting()
        {
            ValidateBlankFieldsErrorMsg();
        }

        /// <summary>
        /// This method verify the updated information in my profile section
        /// it check the online display name and email id updated value
        /// </summary>
        /// <returns>returns the list of updated values</returns>
        public void VerifyMyProfUpdatedInfo()
        {
            InputMyProfileData();
            ClickMyProfSaveBtn();
            ValidateUpdateMyProfData();
        }

        /// <summary>
        /// This method verify updated information in my information section
        /// </summary>
        /// <returns>returns the list of allt the updated values</returns>
        public void VerifyUpdatedInformation()
        {
            InputData();
            ClickSaveBtn();
            ValidateUpdatedData();
        }

        /// <summary>
        /// This keyword select the spanish language from the drop down
        /// It calls ClickEdit Button, Spanish selection and save Btn Methods
        /// </summary>
        /// <returns>It returns the list of all the spanish conversion text</returns>
        public void ConvertToSpanish()
        {
            ClickMyInfoEditBtn();
            SelectSpanishLanguageFromDD();
            ClickSaveBtn();
            ValidateSpanishConversion();
        }

        /// <summary>
        /// This keyword select the English language from the drop down
        /// It calls ClickEdit Button, English selection and save Btn Methods
        /// </summary>
        /// <returns>It returns the list of all the spanish conversion text</returns>
        public void ConvertToEnglish()
        {
            ClickMyInfoEditBtn();
            SelectEnglishLanguageFromDD();
            ClickSaveBtn();
        }
    }
}
