using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages.Trackers;
using NUnit.Framework;

namespace AutomationFramework.Tests.WebTests.Trackers
{
    [TestFixture]
    [Order(22)]
    public class NutritionTracker : Base
    {
        /// <summary>
        /// Test Case: Verifies Nutrition Tracker
        /// </summary>
        [Test, Order(1)]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyNutritionTracker()
        {
            //To call the Page Login Method
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();

            Common trackermenu = new Common();
            trackermenu.ClickTrackerMenu();
            Page_NutritionTracker nt = new Page_NutritionTracker(softassertions);
            nt.GoToNutritionTracker();
            nt.VerifyNutritionTracker();
            
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

            CommonTracker ntc = new CommonTracker(softassertions);
            ntc.VerifyTrackerChart();

            is_soft_assert = true;
            softassertions.AssertAll();

            Common logout = new Common();
            logout.LogOut();
        }
    }
}
