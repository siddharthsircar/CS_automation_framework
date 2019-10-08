using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages.Coaching;
using AutomationFramework.Pages.WebPages.Trackers;
using NUnit.Framework;
using System.Linq;

namespace AutomationFramework.Tests.WebTests.Coaching
{
    [TestFixture]
    [Order(17)]
    public class MaintainTobaccoFreeStatusGoal : Base
    {
        Common cmn = new Common();
        CommonGoals cmngoal = new CommonGoals(softassertions);
        Page_MaintainTobaccoFreeStatusGoal mtfs = new Page_MaintainTobaccoFreeStatusGoal(softassertions);

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
            mtfs.SetGoal();
        }
        public void TC_VerifyPlanScreen()
        {
            cmngoal.ClickStep1NextBtn();
            is_soft_assert = true;
            mtfs.VerifyPlanScreen();
            softassertions.AssertAll();
        }
        public void TC_AddPlans()
        {
            is_soft_assert = true;
            mtfs.VerifyActionsAdded();
            softassertions.AssertAll();

        }
        public void TC_VerifyGoalSetUpScreen()
        {
            cmngoal.ClickStep2NextBtn();
            is_soft_assert = true;
            mtfs.VerifySetUpScreen();
            softassertions.AssertAll();
        }
        public void TC_VerifyGoalCompletionScreen()
        {
            cmngoal.ClickConfirmBtn();
            is_soft_assert = true;
            mtfs.VerifyGoalComplete();
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
        //[Category("CoachingReg")]
        //[Category("Regression")]
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
        //[Category("CoachingReg")]
        //[Category("Regression")]
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
        //[Category("CoachingReg")]
        //[Category("Regression")]
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
            mtfs.ValidateAtActionsAdded();
        }
        [Test, Order(6)]
        [Category("Regression")]
        [Category("CoachingReg")]
        public void TC_SetMaintainTobaccoFreeGoal()
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
        public void TC_FilTobaccoTracker()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            Page_TobaccoTracker tob = new Page_TobaccoTracker(softassertions);
            tob.VerifyTobaccoTrackerFromGoal();
            is_soft_assert = true;
            softassertions.AssertAll();
        }


        [Test, Order(8)]
        [Category("Regression")]
        [Category("CoachingReg")]
        public void TC_RemoveMaintainTobaccoFreeGoal()
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
