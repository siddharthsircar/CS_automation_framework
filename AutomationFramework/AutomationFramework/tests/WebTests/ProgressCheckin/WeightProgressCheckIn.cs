using System;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using AutomationFramework.Framework;
using AutomationFramework.Pages;
using System.Collections.Generic;
using System.Linq;

namespace AutomationFramework.Tests.WebTests.ProgressCheckin
{
    /// <summary>
    /// Test Class
    /// </summary>
    [TestFixture]
    [Order(35)]
    public class WeightProgressCheckIn : Base
    {
        private Common logout;
        int points;
        Common cmn = new Common();

        /// <summary>
        /// This testcase will Complete and Validate Weight Management Progress Check In
        /// This will first login to application and then start, complete and validate Progress Check In
        /// </summary>
        [Test,Order(2)]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyWeightManagementProgressCheckIn()
        {
            //To call the Page Login Method
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            cmn.ClickProgressCheckinMenu();
            Page_WeightProgressCheckIn pprogress = new Page_WeightProgressCheckIn(softassertions);
            is_soft_assert = true;
            pprogress.CompleteProgressCheckIn();
            softassertions.AssertAll();

        }
       
        /// <summary>
        /// Test case verify the client wise buttom link of Report page bottom links 
        /// </summary>
        [Test, Order(2)]
        [Category("Regression")]
        [Category("AllClientReg")]
        /// [Category("Regression")]
        public void TC_ValidateProgressCheckInReportBottomLink()
        {
         
            Page_WeightProgressCheckIn pprogress = new Page_WeightProgressCheckIn(softassertions);
            is_soft_assert = true;
            pprogress.VerifyProgressCheckinReportBottomLinks(GlobalVariables.clientname);
            softassertions.AssertAll();
            cmn.ClickFooterDashboardLink();
            cmn.LogOut();
        }
        /// <summary>
        /// This testcase will Complete and Validate Weight Management Progress Check In
        /// This will first login to application and then start, complete and validate Progress Check In
        /// </summary>
        //[Test,Order(1)]
        //[Category("Regression")]

        public void TC_VerifyMandatoryQuestionsMessage()
        {
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            cmn.ClickProgressCheckinMenu();
            Page_WeightProgressCheckIn pprogress = new Page_WeightProgressCheckIn();
            
            List<string[]> result = pprogress.VerifyMandatoryQuestionsErrorMessage();

            Assert.Multiple(() =>
            {
                for (int i = 0; i < result.Count; i++)
                {
                    bool textmatchresult = Convert.ToBoolean(result.ElementAt(i)[1]);
                    string msg = result.ElementAt(i)[0];
                    Assert.IsTrue(textmatchresult, msg);
                }

            }
            );
            cmn.CloseHamMenu();

            //cmn.LogOut();
        }

        
    }


}
