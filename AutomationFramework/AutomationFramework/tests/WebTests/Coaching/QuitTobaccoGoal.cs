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
    public class QuitTobaccoGoal:Base
    {
        Common cmn = new Common();
        CommonGoals cmngoal = new CommonGoals(softassertions);
        Page_QuitTobaccoGoal qt = new Page_QuitTobaccoGoal(softassertions);
        public void GoToDashboard()
        {
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
        }
        public void TC_InputCurrentTobaccoStatus()
        {
            cmn.ClickGoalMenu();
            qt.InputTobaccoStatus();
        }
        public void TC_VerifyPlanScreen()
        {
            cmngoal.ClickStep1NextBtn();
            is_soft_assert = true;
            qt.VerifyPlanScreen();
            softassertions.AssertAll();
        }
        public void TC_AddPlans()
        {
            is_soft_assert = true;
            qt.VerifyActionsAdded();
            softassertions.AssertAll();
            
        }
        public void TC_VerifyGoalSetUpScreen()
        {
            is_soft_assert = true;
            System.Threading.Thread.Sleep(3000);
            cmngoal.ClickStep2NextBtn();
            qt.VerifySetUpScreen();
            softassertions.AssertAll();
            
        }
        public void TC_VerifyGoalCompletionScreen()
        {
            cmngoal.ClickConfirmBtn();
            is_soft_assert = true;
            qt.VerifyGoalComplete();
            softassertions.AssertAll();
        }
        public void TC_VerifyRemovePopUp()
        {
            
            is_soft_assert = true;
            cmngoal.ClickRemoveBtn();
            cmngoal.VerifyRemovePopUp();
            softassertions.AssertAll();
        }
        public void TC_RemoveGoal()
        {
            cmngoal.ClickRemoveScreenYesBtn();
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
            TC_InputCurrentTobaccoStatus();
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
            TC_InputCurrentTobaccoStatus();
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
            TC_InputCurrentTobaccoStatus();
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
            TC_InputCurrentTobaccoStatus();
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
            TC_InputCurrentTobaccoStatus();
            TC_VerifyPlanScreen();
            TC_AddPlans();
            TC_VerifyGoalSetUpScreen();
            cmngoal.ClickStep2BackBtn();
            System.Threading.Thread.Sleep(3000);
            qt.ValidateAtActionsAdded();
        }
        [Test,Order(6)]
        [Category("Regression")]
        [Category("CoachingReg")]
        public void TC_QuitTobaccoGoal()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            GoToDashboard();//Removed once the bug is resolved(DELTA-340)
            TC_InputCurrentTobaccoStatus();
            TC_VerifyPlanScreen();
            TC_AddPlans();
            TC_VerifyGoalSetUpScreen();
            TC_VerifyGoalCompletionScreen();
            
        }
        [Test, Order(7)]
        [Category("Regression")]
        [Category("CoachingReg")]
        public void TC_FillTobaccoTracker()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            Page_TobaccoTracker tob = new Page_TobaccoTracker(softassertions);
            tob.VerifyTobaccoTracker();
            is_soft_assert = true;
            softassertions.AssertAll();
        }
        [Test, Order(8)]
        [Category("Regression")]
        [Category("CoachingReg")]
        public void TC_RemoveQuitTobaccoGoal()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            TC_VerifyRemovePopUp();
            TC_RemoveGoal();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            cmn.LogOut();
        }
    }
}
