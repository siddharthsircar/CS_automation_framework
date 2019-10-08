using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages.Coaching;
using AutomationFramework.Pages.WebPages.Trackers;
//using AutomationFramework.Pages.WeCholesterolages.Coaching;
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
    public class ImproveCholesterolGoal:Base
    {
        Common cmn = new Common();
        CommonGoals cmngoal = new CommonGoals(softassertions);
        //[Test,Order(1)]
        //[Category("Regression")]
        public void GoToDashboard()
        {
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
        }
        public void TC_InputCholesterolStatus()
        {
            cmn.ClickGoalMenu();
            Page_ImproveCholesterolGoal iCholesterolg = new Page_ImproveCholesterolGoal();
            iCholesterolg.InputCholesterolValues();

        }
        //[Test,Order(2)]
        //[Category("Regression")]

        public void TC_VerifyPlanScreen()
        {
            CommonGoals cg = new CommonGoals();
            cg.ClickStep1NextBtn();
            Page_ImproveCholesterolGoal iCholesterolg = new Page_ImproveCholesterolGoal(softassertions);
            is_soft_assert = true;
            iCholesterolg.VerifyPlanScreen();
            softassertions.AssertAll();
        }

        //[Test,Order(3)]
        //[Category("Regression")]

        public void TC_AddPlans()
        {
            Page_ImproveCholesterolGoal iCholesterolg = new Page_ImproveCholesterolGoal(softassertions);
            is_soft_assert = true;
            iCholesterolg.VerifyActionsAdded();
           
            softassertions.AssertAll();
        }
        
        //[Test,Order(4)]
        //[Category("Regression")]
        public void TC_VerifyGoalSetUpScreen()
        {
            CommonGoals cg = new CommonGoals();
            cg.ClickStep2NextBtn();
            Page_ImproveCholesterolGoal iCholesterolg = new Page_ImproveCholesterolGoal(softassertions);
            is_soft_assert = true;
            iCholesterolg.VerifySetUpScreen();
            
            softassertions.AssertAll();
        }
        
        //[Test, Order(5)]
        //[Category("Regression")]
        public void TC_VerifyGoalCompletionScreen()
        {
            CommonGoals cg = new CommonGoals();
            cg.ClickConfirmBtn();
            Page_ImproveCholesterolGoal iCholesterolg = new Page_ImproveCholesterolGoal(softassertions);
            is_soft_assert = true;
            iCholesterolg.VerifyGoalComplete();
            softassertions.AssertAll();

        }
        //[Test, Order(6)]
        //[Category("Regression")]
        public void TC_VerifyRemovePopUp()
        {
            //cmngoal.ClickRemoveBtn();
            CommonGoals cg = new CommonGoals(softassertions);
            is_soft_assert = false;
            cg.ClickRemoveBtn();
            is_soft_assert = true;
            cg.VerifyRemovePopUp();
            softassertions.AssertAll();
        }
        //[Test, Order(7)]
        //[Category("Regression")]
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

        //[Test, Order(1)]
        //[Category("Regression")]
        //[Category("BuildSanity")]
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
            TC_InputCholesterolStatus();
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
        //[Category("BuildSanity")]
        //[Category("CoachingReg")]
        public void TC_ClickPlanScreenCancelButton()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            TC_InputCholesterolStatus();
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
        //[Category("BuildSanity")]
        //[Category("CoachingReg")]
        public void TC_ClickPlansBackBtn()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            TC_InputCholesterolStatus();
            TC_VerifyPlanScreen();
            cmngoal.ClickStep1BackBtn();
        }
        //[Test, Order(4)]
        //[Category("Regression")]
        //[Category("BuildSanity")]
        //[Category("CoachingReg")]
        public void TC_ClickGoalSetUpScreenCancelBtn()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            TC_InputCholesterolStatus();
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
        //[Category("BuildSanity")]
        //[Category("CoachingReg")]
        public void TC_ClickGoalSetUpBackBtn()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            TC_InputCholesterolStatus();
            TC_VerifyPlanScreen();
            TC_AddPlans();
            TC_VerifyGoalSetUpScreen();
            cmngoal.ClickStep2BackBtn();
            System.Threading.Thread.Sleep(3000);
            Page_ImproveCholesterolGoal iCholesterolg = new Page_ImproveCholesterolGoal(softassertions);
            iCholesterolg.VerifyActionsAdded();
        }

        [Test, Order(6)]
        [Category("Regression")]
        [Category("BuildSanity")]
        [Category("CoachingReg")]
        public void TC_ImproveCholesterolGoal()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            GoToDashboard();//Removed once the bug is resolved(DELTA-340)
            TC_InputCholesterolStatus();
            TC_VerifyPlanScreen();
            TC_AddPlans();
            TC_VerifyGoalSetUpScreen();
            TC_VerifyGoalCompletionScreen();
            
        }
        [Test, Order(7)]
        [Category("Regression")]
        [Category("BuildSanity")]
        [Category("CoachingReg")]
        public void TC_FillCholesterolTracker()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("CoachingEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Coaching not enabled for client");
            }
            Page_CholesterolTracker chol = new Page_CholesterolTracker(softassertions);
            chol.VerifyCholesterolTracker();
            is_soft_assert = true;
            softassertions.AssertAll();
        }
        [Test, Order(8)]
        [Category("Regression")]
        [Category("BuildSanity")]
        [Category("CoachingReg")]
        public void TC_RemoveCholesterolGoal()
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
