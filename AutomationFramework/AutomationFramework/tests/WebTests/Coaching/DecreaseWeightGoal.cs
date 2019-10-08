using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages.Coaching;
using AutomationFramework.Tests.WebTests.Trackers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests.WebTests.Coaching
{
    [TestFixture]
    public class DecreaseWeightGoal:Base
    {
        Common cmn = new Common();
        CommonGoals cmngoal = new CommonGoals();
        public void GoToDashboard()
        {
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            
        }
        public void TC_InputWeight()
        {
            cmn.ClickGoalMenu();
            Page_DecreaseWeightGoal dwg = new Page_DecreaseWeightGoal();
            dwg.InputWeightStatus();
           
        }
        public void TC_VerifyPlanScreen()
        {
            CommonGoals cg = new CommonGoals();
            cg.ClickStep1NextBtn();
            Page_DecreaseWeightGoal dwg = new Page_DecreaseWeightGoal(softassertions);
            is_soft_assert = true;
            dwg.VerifyPlanScreen();
            softassertions.AssertAll();
        }
        public void TC_AddPlans()
        {
            Page_DecreaseWeightGoal dwg = new Page_DecreaseWeightGoal(softassertions);
            is_soft_assert = true;
            dwg.VerifyActionsAdded();
            softassertions.AssertAll();
           
        }
        public void TC_VerifyGoalSetUpScreen()
        {
            CommonGoals cg = new CommonGoals();
            System.Threading.Thread.Sleep(3000);
            cg.ClickStep2NextBtn();

            Page_DecreaseWeightGoal dwg = new Page_DecreaseWeightGoal(softassertions);
            is_soft_assert = true;
            dwg.VerifySetUpScreen();
            softassertions.AssertAll();
        }
        public void TC_VerifyGoalCompletionScreen()
        {
            CommonGoals cg = new CommonGoals();

            cg.ClickConfirmBtn();
            Page_DecreaseWeightGoal dwg = new Page_DecreaseWeightGoal(softassertions);
            is_soft_assert = true;
            dwg.VerifyGoalComplete();
            softassertions.AssertAll();
        }
        public void TC_VerifyRemovePopUp()
        {
            CommonGoals cg = new CommonGoals(softassertions);
            is_soft_assert = true;
            cg.ClickRemoveBtn();
            cg.VerifyRemovePopUp();
            softassertions.AssertAll();
        }
        public void TC_RemoveGoal()
        {
            CommonGoals cg = new CommonGoals(softassertions);
            cg.ClickRemoveScreenYesBtn();
        }
        public void NavigateToDashboard()
        {
            Page_Dashboard dashbrd = new Page_Dashboard();
            Assert.IsTrue(dashbrd.AtDashboard(), "Not Navigated to Dashboard");
        }
        //[Test,Order(1)]
        //[Category("Regression")]
        //[Category("CoachingReg")]
        public void TC_ClickInputScreenCancelButton()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            GoToDashboard();
            TC_InputWeight();
            cmngoal.ClickModalWindowCancelbutton();
            Assert.IsFalse(cmngoal.VerfiyModalWindowNotExist());
            cmngoal.ClickModalWindowOkButton();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            NavigateToDashboard();
            softassertions.AssertAll();
            
        }
        //[Test,Order(2)]
        //[Category("Regression")]
        //[Category("CoachingReg")]
        public void TC_ClickPlanScreenCancelButton()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            TC_InputWeight();
            TC_VerifyPlanScreen();
            cmngoal.ClickModalWindowCancelbutton();
            Assert.IsFalse(cmngoal.VerfiyModalWindowNotExist());
            cmngoal.ClickModalWindowOkButton();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            NavigateToDashboard();
            softassertions.AssertAll();
        }
        //[Test,Order(3)]
        //[Category("Regression")]
        //[Category("CoachingReg")]

        public void TC_ClickPlansBackBtn()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            TC_InputWeight();
            TC_VerifyPlanScreen();
            cmngoal.ClickStep1BackBtn();
        }
        //[Test, Order(4)]
        //[Category("Regression")]
        //[Category("CoachingReg")]
        public void TC_ClickGoalSetUpScreenCancelBtn()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            TC_InputWeight();
            TC_VerifyPlanScreen();
            TC_AddPlans();
            TC_VerifyGoalSetUpScreen();
            cmngoal.ClickModalWindowCancelbutton();
            Assert.IsFalse(cmngoal.VerfiyModalWindowNotExist());
            cmngoal.ClickModalWindowOkButton();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            NavigateToDashboard();
            softassertions.AssertAll();
        }
        //[Test, Order(5)]
        //[Category("Regression")]
        //[Category("CoachingReg")]
        public void TC_ClickGoalSetUpBackBtn()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            TC_InputWeight();
            TC_VerifyPlanScreen();
            TC_AddPlans();
            TC_VerifyGoalSetUpScreen();
            cmngoal.ClickStep2BackBtn();
            System.Threading.Thread.Sleep(3000);
            Page_DecreaseWeightGoal dwg = new Page_DecreaseWeightGoal(softassertions);
            dwg.ValidateAtActionsAdded();
            softassertions.AssertAll();
        }
        [Test,Order(6)]
        [Category("Regression")]
        [Category("CoachingReg")]
        public void TC_DecreaseWeightGoal()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            GoToDashboard();//Removed once the bug is resolved(DELTA-340)
            TC_InputWeight();
            TC_VerifyPlanScreen();
            TC_AddPlans();
            TC_VerifyGoalSetUpScreen();
            TC_VerifyGoalCompletionScreen();
            cmngoal.ClickEditBtn();
            cmn.ClickGoalMenu();
            Page_DecreaseWeightGoal dwg = new Page_DecreaseWeightGoal();
            dwg.ClickDecreaseWeightGoal();
        }
        [Test, Order(7)]
        [Category("Regression")]
        [Category("CoachingReg")]
        public void TC_FillWeightTracker()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            Page_WeightTracker pWeightTracker = new Page_WeightTracker(softassertions);
            pWeightTracker.VerifyWeightTracker(GlobalVariables.clientname);
            is_soft_assert = true;
            System.Threading.Thread.Sleep(3000);
            softassertions.AssertAll();
        }
        [Test, Order(8)]
        [Category("Regression")]
        [Category("CoachingReg")]
        public void TC_RemoveDecreaseWeightGoal()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            TC_VerifyRemovePopUp();
            TC_RemoveGoal();
            Page_HAPrompt haprompt = new Page_HAPrompt();//Removed once the bug is resolved(DELTA-340)
            haprompt.GoToDashboard();//Removed once the bug is resolved(DELTA-340)
            NavigateToDashboard();//Removed once the bug is resolved(DELTA-340)
            cmn.LogOut();
        }

    }
}
