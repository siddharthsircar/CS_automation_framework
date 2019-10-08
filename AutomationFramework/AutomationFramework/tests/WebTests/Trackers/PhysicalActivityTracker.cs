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
    [Order(27)]
    public class PhysicalActivityTracker : Base
    {
        /// <summary>
        /// Test Case: Verifies Physical Activity Tracker
        /// </summary>
        [Test, Order(1)]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyPhysActTracker()
        {
            //To call the Page Login Method
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();

            Common trackermenu = new Common();
            trackermenu.ClickTrackerMenu();
            Page_PhysActivityTracker pa = new Page_PhysActivityTracker(softassertions);
            pa.GoToPhysicalTracker();
            pa.VerifyPhysicalActivityTracker();
            
            is_soft_assert = true;
            softassertions.AssertAll();
            //Common logout = new Common();
            //logout.LogOut();
        }
        /// <summary>
        /// Test Case:Verifies Tracker Chart
        /// </summary>
        [Test, Order(2)]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyTrackerChart()
        {
            //Page_Login plogin = new Page_Login();
            //plogin.Login();
            //Page_HAPrompt haprompt = new Page_HAPrompt();            
            //haprompt.GoToDashboard();

            //Common trackermenu = new Common();
            //trackermenu.ClickTrackerMenu();
            CommonTracker ct = new CommonTracker(softassertions);
            //Page_PhysActivityTracker pa = new Page_PhysActivityTracker(softassertions);
            //pa.NavigateToPhysicalActivityTracker();
            ct.VerifyTrackerChart();

            is_soft_assert = true;
            softassertions.AssertAll();

            Common logout = new Common();
            logout.LogOut();
        }
    }
}
