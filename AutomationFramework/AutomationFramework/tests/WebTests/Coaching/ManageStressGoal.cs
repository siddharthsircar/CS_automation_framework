using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages.Coaching;
using AutomationFramework.Pages.WebPages.Trackers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests.WebTests.Coaching
{
    [TestFixture]
    [Order(18)]
    public class ManageStressGoal : Base
    {
        Common cmn = new Common();
        CommonGoals cmngoal = new CommonGoals(softassertions);

        //[Test, Order(1)]
        //[Category("Regression")]
        public void GoToDashboard()
        {
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
        }
        public void TC_SetStressGoalForFutureDate()
        {
            cmn.ClickGoalMenu();
            Page_ManageStressGoal stressGoal = new Page_ManageStressGoal();
            stressGoal.SetGoal();
            
        }
        //[Test, Order(2)]
        //[Category("Regression")]

        public void TC_VerifyPlanScreen()
        {
            CommonGoals cg = new CommonGoals();
            cg.ClickStep1NextBtn();
            Page_ManageStressGoal stressGoal = new Page_ManageStressGoal(softassertions);
            is_soft_assert = true;
            stressGoal.VerifyPlanScreen();
            softassertions.AssertAll();
        }
        //[Test, Order(3)]
        //[Category("Regression")]

        public void TC_AddPlans()
        {
            Page_ManageStressGoal stressGoal = new Page_ManageStressGoal(softassertions);
            is_soft_assert = true;
            stressGoal.VerifyActionsAdded();
            softassertions.AssertAll();
            
        }
        //[Test, Order(4)]
        //[Category("Regression")]
        public void TC_VerifyGoalSetUpScreen()
        {
            CommonGoals cg = new CommonGoals();
            cg.ClickStep2NextBtn();
            Page_ManageStressGoal stressGoal = new Page_ManageStressGoal(softassertions);
            is_soft_assert = true;
            stressGoal.VerifySetUpScreen();
            softassertions.AssertAll();
            
        }
        //[Test, Order(5)]
        //[Category("Regression")]
        public void TC_VerifyGoalCompletionScreen()
        {
            CommonGoals cg = new CommonGoals();
            cg.ClickConfirmBtn();
            Page_ManageStressGoal stressGoal = new Page_ManageStressGoal(softassertions);
            is_soft_assert = true;
            stressGoal.VerifyGoalComplete();
            softassertions.AssertAll();

        }
        //[Test, Order(6)]
        //[Category("Regression")]
        public void TC_VerifyRemovePopUp()
        {
            //cmngoal.ClickRemoveBtn();
            CommonGoals cg = new CommonGoals(softassertions);
            is_soft_assert = true;
            cg.ClickRemoveBtn();
            cg.VerifyRemovePopUp();
            softassertions.AssertAll();
        }
        //[Test, Order(7)]
        //[Category("Regression")]
        public void TC_RemoveGoal()
        {
            CommonGoals cg = new CommonGoals(softassertions);
            cg.ClickRemoveScreenYesBtn();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            cmn.LogOut();
        }
        public void NavigateToDashboard()
        {
            Page_Dashboard dashbrd = new Page_Dashboard();
            Assert.IsTrue(dashbrd.AtDashboard(), "Not Navigated to Dashboard");
        }
        //[Test, Order(1)]
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
            TC_SetStressGoalForFutureDate();
            cmngoal.ClickModalWindowCancelbutton();
            Assert.IsFalse(cmngoal.VerfiyModalWindowNotExist());
            cmngoal.ClickModalWindowOkButton();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            NavigateToDashboard();
            softassertions.AssertAll();
        }
        //[Test, Order(2)]
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
            TC_SetStressGoalForFutureDate();
            TC_VerifyPlanScreen();
            cmngoal.ClickModalWindowCancelbutton();
            Assert.IsFalse(cmngoal.VerfiyModalWindowNotExist());
            cmngoal.ClickModalWindowOkButton();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            NavigateToDashboard();
            softassertions.AssertAll();
        }
        //[Test, Order(3)]
        //[Category("CoachingReg")]
        //[Category("Regression")]
        public void TC_ClickPlansBackBtn()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            TC_SetStressGoalForFutureDate();
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
            TC_SetStressGoalForFutureDate();
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
            TC_SetStressGoalForFutureDate();
            TC_VerifyPlanScreen();
            TC_AddPlans();
            TC_VerifyGoalSetUpScreen();
            cmngoal.ClickStep2BackBtn();
            System.Threading.Thread.Sleep(3000);
            Page_ManageStressGoal stressGoal = new Page_ManageStressGoal(softassertions);
            stressGoal.ValidateAtActionsAdded();
        }
        [Test, Order(6)]
        [Category("Regression")]
        [Category("CoachingReg")]
        public void TC_ManageStressGoal()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            GoToDashboard();//Removed once the bug is resolved(DELTA-340)
            TC_SetStressGoalForFutureDate();
            TC_VerifyPlanScreen();
            TC_AddPlans();
            TC_VerifyGoalSetUpScreen();
            TC_VerifyGoalCompletionScreen();
        }
        [Test, Order(7)]
        [Category("Regression")]
        [Category("CoachingReg")]
        public void TC_FillManageStressTracker()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            Page_StressTracker stress = new Page_StressTracker(softassertions);
            stress.VerifyStressTracker();
            is_soft_assert = true;
            softassertions.AssertAll();
        }
        [Test, Order(8)]
        [Category("Regression")]
        [Category("CoachingReg")]
        public void TC_ManageStressRemoveGoal()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            TC_VerifyRemovePopUp();
            TC_RemoveGoal();
        }

    }
}
