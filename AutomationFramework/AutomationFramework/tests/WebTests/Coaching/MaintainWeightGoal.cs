using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages.Coaching;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests.WebTests.Coaching

{
    [TestFixture]
    [Order(19)]
    // [Parallelizable(ParallelScope.Fixtures)]

    public class MaintainWeightGoal : Base
    {
        Common cmn = new Common();
        CommonGoals cmngoal = new CommonGoals(softassertions);
        Page_MaintainWeightGoal mt = new Page_MaintainWeightGoal(softassertions);
        public void GoToDashboard()
        {
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
        }
        public void TC_InputCurrentWeightStatus()
        {
            cmn.ClickGoalMenu();
            mt.InputWeight();
        }
        public void TC_VerifyPlanScreen()
        {
            cmngoal.ClickStep1NextBtn();
            is_soft_assert = true;
            mt.VerifyPlanScreen();
            softassertions.AssertAll();
        }
        public void TC_AddPlans()
        {
            is_soft_assert = true;
            mt.VerifyActionsAdded();
            softassertions.AssertAll();

        }
        public void TC_VerifyGoalSetUpScreen()
        {
            is_soft_assert = true;
            //System.Threading.Thread.Sleep(3000);
            cmngoal.ClickStep2NextBtn();
            mt.VerifySetUpScreen();
            softassertions.AssertAll();

        }
        public void TC_VerifyGoalCompletionScreen()
        {
            cmngoal.ClickConfirmBtn();
            is_soft_assert = true;
            mt.VerifyGoalComplete();
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
            TC_InputCurrentWeightStatus();
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
            TC_InputCurrentWeightStatus();
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
            TC_InputCurrentWeightStatus();
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
            TC_InputCurrentWeightStatus();
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
            TC_InputCurrentWeightStatus();
            TC_VerifyPlanScreen();
            TC_AddPlans();
            TC_VerifyGoalSetUpScreen();
            cmngoal.ClickStep2BackBtn();
            System.Threading.Thread.Sleep(3000);
            mt.ValidateAtActionsAdded();
        }
        [Test, Order(6)]
        [Category("Regression")]
        [Category("CoachingReg")]
        public void TC_MaintainWeightGoal()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            GoToDashboard();//Removed once the bug is resolved(DELTA-340)
            TC_InputCurrentWeightStatus();
            TC_VerifyPlanScreen();
            TC_AddPlans();
            TC_VerifyGoalSetUpScreen();
            TC_VerifyGoalCompletionScreen();
        }
        [Test, Order(7)]
        [Category("Regression")]
        [Category("CoachingReg")]
        public void FillWeightTracker()
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
            softassertions.AssertAll();
        }
        [Test, Order(8)]
        [Category("Regression")]
        [Category("CoachingReg")]
        public void TC_RemoveMaintainWeightGoal()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            System.Threading.Thread.Sleep(3000);
            TC_VerifyRemovePopUp();
            TC_RemoveGoal();
            Page_HAPrompt haprompt = new Page_HAPrompt();//Removed once the bug is resolved(DELTA-340)
            haprompt.GoToDashboard();//Removed once the bug is resolved(DELTA-340)
            NavigateToDashboard();//Removed once the bug is resolved(DELTA-340)
            cmn.LogOut();
        }

        [Test, Order(9)]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        public void TC_CompleteMaintainWeightGoal()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            GoToDashboard();//Removed once the bug is resolved(DELTA-340)
            TC_InputCurrentWeightStatus();
            TC_VerifyPlanScreen();
            TC_AddPlans();
            TC_VerifyGoalSetUpScreen();
            TC_VerifyGoalCompletionScreen();
            TC_VerifyRemovePopUp();
            TC_RemoveGoal();
            softassertions.AssertAll();
            Page_HAPrompt haprompt = new Page_HAPrompt();//Removed once the bug is resolved(DELTA-340)
            haprompt.GoToDashboard();//Removed once the bug is resolved(DELTA-340)
            NavigateToDashboard();//Removed once the bug is resolved(DELTA-340)
            cmn.LogOut();
        }

    }
}
