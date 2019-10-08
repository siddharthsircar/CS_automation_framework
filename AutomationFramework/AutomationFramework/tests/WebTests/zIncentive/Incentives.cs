using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages.Incentive;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests.WebTests.zIncentive
{
    [TestFixture]
    [Order(500)]
    public class Incentives : Base
    {
        /// <summary>
        /// Test Case: To verify Incentive History page
        /// </summary>
        [Test, Order(1)]
        [Category("Regression")]
        [Category("ProdSanity")]
        [Category("PointReg")]
        public void TC_ValidateIncentiveHistory()
        {
            Common cmn = new Common();
            string incentiveEnabled = cmn.GetConfig("IncentiveEnabled").ElementAt(0)[1].ToLower();
            if (incentiveEnabled.Equals("false"))
            {
                Assert.Ignore("Incentives not enabled for client");
            }
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();

            System.Threading.Thread.Sleep(20000);
            Page_Incentive pincentive = new Page_Incentive(softassertions);
            is_soft_assert = true;
            pincentive.ValidateHistoryData();
            softassertions.AssertAll();

            Common logout = new Common();
            logout.LogOut();
        }

        /// <summary>
        /// Test Case: To Verify Eligible Activities for a client
        /// </summary>
        [Test, Order(2)]
        public void TC_ValidateIncentiveEligibleActivities()
        {
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            Page_Incentive pincentive = new Page_Incentive(softassertions);
            is_soft_assert = true;
            pincentive.ValidateEligibleActivities();
            softassertions.AssertAll();

            Common logout = new Common();
            logout.LogOut();
        }
    }
}
