using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages.Trackers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests.WebTests.Trackers
{
    [TestFixture]
    [Order(25)]
    public class SleepTracker : Base
    {
        /// <summary>
        /// Test Case: Verifies Sleep Tracker
        /// </summary>
        [Test, Order(1)]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifySleepTracker()
        {
            
            if (GlobalVariables.clientname == "NUCOR")
            {
                Assert.Ignore("The current testcase is not available for the client" + GlobalVariables.clientname);
            }
            else
            {
                //To call the Page Login Method
                Page_Login plogin = new Page_Login();
                plogin.Login();
                Page_HAPrompt haprompt = new Page_HAPrompt();
                haprompt.GoToDashboard();

                Common trackermenu = new Common();
                trackermenu.ClickTrackerMenu();
                Page_SleepTracker sl = new Page_SleepTracker(softassertions);
                sl.VerifySleepTracker();
                
                is_soft_assert = true;
                softassertions.AssertAll();
                //Common logout = new Common();
                //logout.LogOut();
            }
        }

        /// <summary>
        /// Test Case:Verifies Tracker Chart
        /// </summary>
        [Test, Order(2)]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyTrackerChart()
        {
            if (GlobalVariables.clientname == "NUCOR")
            {
                Assert.Ignore("The current testcase is not available for the client" + GlobalVariables.clientname);
            }
            else
            {
                //Page_Login plogin = new Page_Login();
                //plogin.Login();
                //Page_HAPrompt haprompt = new Page_HAPrompt();            
                //haprompt.GoToDashboard();

                CommonTracker sl = new CommonTracker(softassertions);
                sl.VerifyTrackerChart();

                is_soft_assert = true;
                softassertions.AssertAll();

                Common logout = new Common();
                logout.LogOut();
            }
        }
    }
}
