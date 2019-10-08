using System;
using NUnit.Framework;
using AutomationFramework.Framework;
using AutomationFramework.Pages;

namespace AutomationFramework.Tests.WebTests
{    /// <summary>
     /// Test Class
     /// </summary>
    [TestFixture]
    //[Parallelizable(ParallelScope.Fixtures)]
    [Order(6)]
    public class Setting : Base
    {
        /// <summary>
        /// This Test case is used to verify the error message in my profile section
        /// </summary>
        [Test, Order(1)]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyMyProfErrorMessages()
        {
            //To call the Page Login Method
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            Console.WriteLine("Navigating to Settings");
            Common settings = new Common();
            settings.GoToSettings();
            Page_Settings myinf = new Page_Settings(softassertions);
            myinf.VerifyMyProfileErrors();
            is_soft_assert = true;
            softassertions.AssertAll();
        }

        /// <summary>
        /// this test case used to verify my profile updated data
        /// </summary>
        [Test, Order(2)]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyMyProfileData()
        {
            Page_Settings myinf = new Page_Settings(softassertions);
            myinf.VerifyMyProfUpdatedInfo();
            is_soft_assert = true;
            softassertions.AssertAll();
        }

        /// <summary>
        /// Test Case: Verifies Settings page
        /// </summary>
        [Test, Order(3)]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyErrorMessages()
        {
            Page_Settings myinf = new Page_Settings(softassertions);
            myinf.VerifyMyInformationSetting();
            is_soft_assert = true;
            softassertions.AssertAll();
        }

        /// <summary>
        /// This test case call verify update information from page class
        /// All the validation are put in soft assertion using assert.multiple
        /// </summary>
        [Test, Order(4)]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyUpdatedData()
        {
            Page_Settings myinf = new Page_Settings(softassertions);
            myinf.VerifyUpdatedInformation();
            is_soft_assert = true;
            softassertions.AssertAll();
            
        }

    }
}

