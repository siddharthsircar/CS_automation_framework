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
    /// <summary>
    /// Test Class
    /// </summary>
    [TestFixture]
    [Order(20)]
    public class BPTracker : Base
    {
        /// <summary>
        /// Test Case: Verifies BP Tracker
        /// </summary>
        [Test, Order(1)]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyBPTracker()
        {
            //To call the Page Login Method
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();

            Common trackermenu = new Common();
            trackermenu.ClickTrackerMenu();
            Page_BPTracker bp = new Page_BPTracker(softassertions);

            bp.NavigateToBPTracker();
            bp.VerifyBPTracker();
            
            is_soft_assert = true;
            softassertions.AssertAll();
            //Common logout = new Common();
            //logout.LogOut();
        }
        /// <summary>
        /// Verifies Tracker Chart
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

            CommonTracker bptracker = new CommonTracker(softassertions);
            bptracker.VerifyTrackerChart();

            is_soft_assert = true;
            softassertions.AssertAll();

            Common logout = new Common();
            logout.LogOut();
        }
    }
}
