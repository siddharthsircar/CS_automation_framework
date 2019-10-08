using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages.Coaching;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using AutomationFramework.Pages.WebPages.Trackers;

namespace AutomationFramework.Tests.WebTests.Coaching
{
    [TestFixture]
    [Order(18)]
    public class ImproveDietaryHabitsGoal : Base
    {
        Common cmn = new Common();
        CommonGoals cmngoal = new CommonGoals();
        //[Test, Order(1)]
        //[Category("Regression")]
        public void GoToDashboard()
        {
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();

        }
        public void TC_SetDietaryHabitsGoalForFutureDate()
        {

            cmn.ClickGoalMenu();
            Page_ImproveDietaryHabitsGoal dietaryHabitsGoal = new Page_ImproveDietaryHabitsGoal();
            dietaryHabitsGoal.SetGoal();
 
        }
        //[Test, Order(2)]
        //[Category("Regression")]

        public void TC_VerifyPlanScreen()
        {
            CommonGoals cg = new CommonGoals();
            cg.ClickStep1NextBtn();
            Page_ImproveDietaryHabitsGoal fitnessLevelGoal = new Page_ImproveDietaryHabitsGoal(softassertions);
            is_soft_assert = true;
            fitnessLevelGoal.VerifyPlanScreen();
            softassertions.AssertAll();
        }
        //[Test, Order(3)]
        //[Category("Regression")]

        public void TC_AddPlans()
        {
            Page_ImproveDietaryHabitsGoal dietaryHabitsGoal = new Page_ImproveDietaryHabitsGoal(softassertions);
            is_soft_assert = true;
            dietaryHabitsGoal.VerifyActionsAdded();
            softassertions.AssertAll();
            
        }
        //[Test, Order(4)]
        //[Category("Regression")]
        public void TC_VerifyGoalSetUpScreen()
        {
            CommonGoals cg = new CommonGoals();
            cg.ClickStep2NextBtn();
            Page_ImproveDietaryHabitsGoal dietaryHabitsGoal = new Page_ImproveDietaryHabitsGoal(softassertions);
            is_soft_assert = true;
            dietaryHabitsGoal.VerifySetUpScreen();
            softassertions.AssertAll();
            
        }
        //[Test, Order(5)]
        //[Category("Regression")]
        public void TC_VerifyGoalCompletionScreen()
        {
            CommonGoals cg = new CommonGoals();
            cg.ClickConfirmBtn();
            Page_ImproveDietaryHabitsGoal dietaryHabitsGoal = new Page_ImproveDietaryHabitsGoal(softassertions);
            is_soft_assert = true;
            dietaryHabitsGoal.VerifyGoalComplete();
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
            TC_SetDietaryHabitsGoalForFutureDate();
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
            TC_SetDietaryHabitsGoalForFutureDate();
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
            TC_SetDietaryHabitsGoalForFutureDate();
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
            Thread.Sleep(4000);
            TC_SetDietaryHabitsGoalForFutureDate();
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
            TC_SetDietaryHabitsGoalForFutureDate();
            TC_VerifyPlanScreen();
            TC_AddPlans();
            TC_VerifyGoalSetUpScreen();
            cmngoal.ClickStep2BackBtn();
            System.Threading.Thread.Sleep(3000);
            Page_ImproveDietaryHabitsGoal idh = new Page_ImproveDietaryHabitsGoal(softassertions);
            idh.VerifyActionsAdded();
            softassertions.AssertAll();
        }

        [Test, Order(6)]
        [Category("Regression")]
        [Category("CoachingReg")]
        public void TC_ImproveDietaryHabits()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            GoToDashboard();//Removed once the bug is resolved(DELTA-340)
            TC_SetDietaryHabitsGoalForFutureDate();
            TC_VerifyPlanScreen();
            TC_AddPlans();
            TC_VerifyGoalSetUpScreen();
            TC_VerifyGoalCompletionScreen();
           
        }
        [Test, Order(7)]
        [Category("Regression")]
        [Category("CoachingReg")]
        public void TC_FillNutritionTracker()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            Page_NutritionTracker nt = new Page_NutritionTracker(softassertions);
            nt.VerifyNutritionTracker();
            is_soft_assert = true;
            softassertions.AssertAll();
        }
        [Test, Order(8)]
        [Category("Regression")]
        [Category("CoachingReg")]
        public void TC_RemoveImproveDietaryHabitsGoal()
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
