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
    [Order(23)]
    public class CholesterolTracker : Base
    {
        /// <summary>
        /// Test Case: Verifies Cholesterol Tracker
        /// </summary>
        [Test, Order(1)]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyCholTracker()
        {
            //To call the Page Login Method
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();

            Common trackermenu = new Common();
            trackermenu.ClickTrackerMenu();
            Page_CholesterolTracker chol = new Page_CholesterolTracker(softassertions);
            chol.NavigateToCholTracker();
            chol.VerifyCholesterolTracker();
            //CommonTracker choll = new CommonTracker(softassertions);
            //choll.VerifyTrackerChart();
            is_soft_assert = true;
            softassertions.AssertAll();
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

            CommonTracker chol = new CommonTracker(softassertions);
            chol.VerifyTrackerChart();

            Common logout = new Common();
            logout.LogOut();

            is_soft_assert = true;
            softassertions.AssertAll();

        }
    }
}
