using AutomationFramework.Framework;
using AutomationFramework.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.tests
{
    [TestFixture]
    [Order(19)]
    // [Parallelizable(ParallelScope.Fixtures)]

    public class MaintainWeightGoal:Base
    {
        Common cmn = new Common();
        [Test,Order(1)]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        public void TC_VerifyMaintainWeightGoal()
        {
            Page_Login plogin = new Page_Login();
            plogin.Login();
            ExtentTestManager.GetTest().Info("Successfully Logged In");
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            Common cmn = new Common();
            cmn.ClickGoalMenu();
            Page_MaintainWeightGoal mwgoal = new Page_MaintainWeightGoal();
            List<string[]> result = mwgoal.ValidateElements();
            Assert.Multiple(() =>
            {
                for (int i = 0; i < result.Count; i++)
                {
                    bool textmatchresult = Convert.ToBoolean(result.ElementAt(i)[1]);
                    string msg = result.ElementAt(i)[0];
                    Assert.IsTrue(textmatchresult, msg);
                }
            }
            );
        }
        [Test,Order(2)]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        public void TC_VerifyActionItems()
        {
            Page_MaintainWeightGoal mwgoal = new Page_MaintainWeightGoal();
            List<string[]> result = mwgoal.ValidateActionItems();
            Assert.Multiple(() =>
            {
                for (int i = 0; i < result.Count; i++)
                {
                    bool textmatch = Convert.ToBoolean(result.ElementAt(i)[1]);
                    string msg = result.ElementAt(i)[0];
                    Assert.IsTrue(textmatch, msg);

                }
            }
            );
        }
        [Test,Order(3)]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        public void TC_AddAndVerifyActions()
        {
            Page_MaintainWeightGoal goal = new Page_MaintainWeightGoal();
            List<string[]> result= goal.ActionsAdded();
            Assert.Multiple(() =>
            {
                for (int i = 0; i < result.Count; i++)
                {
                    bool textmatch = Convert.ToBoolean(result.ElementAt(i)[1]);
                    string msg = result.ElementAt(i)[0];
                    Assert.IsTrue(textmatch, msg);

                }
            }
            );

        }
        [Test, Order(4)]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        public void TC_VerifyGoalSetupCompletionScreen()
        {
            Page_MaintainWeightGoal set = new Page_MaintainWeightGoal();
            List<string[]> result=set.VerifySetupScreen();
            Assert.Multiple(() =>
            {
                for (int i = 0; i < result.Count; i++)
                {
                    bool textmatch = Convert.ToBoolean(result.ElementAt(i)[1]);
                    string msg = result.ElementAt(i)[0];
                    Assert.IsTrue(textmatch, msg);

                }
            }
            );
        }
        [Test,Order(5)]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        public void TC_VerifyPlanScreen()
        {
            Page_MaintainWeightGoal plan = new Page_MaintainWeightGoal();
            List<string[]> result = plan.VerifyPlanScreen();
            Assert.Multiple(() =>
            {
                for (int i = 0; i < result.Count; i++)
                {
                    bool textmatch = Convert.ToBoolean(result.ElementAt(i)[1]);
                    string msg = result.ElementAt(i)[0];
                    Assert.IsTrue(textmatch, msg);

                }
            }
            );
        }
        [Test,Order(6)]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        public void TC_VerifyRemovePopUp()
        {
            Page_MaintainWeightGoal remove = new Page_MaintainWeightGoal();
            List<string[]> result = remove.VerifyRemoveGoalPopUp();
            Assert.Multiple(() =>
            {
                for (int i = 0; i < result.Count; i++)
                {
                    bool textmatch = Convert.ToBoolean(result.ElementAt(i)[1]);
                    string msg = result.ElementAt(i)[0];
                    Assert.IsTrue(textmatch, msg);

                }
            }
            );
        }
        [Test,Order(7)]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        public void TC_RemoveGoal()
        {
            Page_MaintainWeightGoal screen = new Page_MaintainWeightGoal();
            screen.RemoveGoal();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            cmn.LogOut();
        }
    }
}
