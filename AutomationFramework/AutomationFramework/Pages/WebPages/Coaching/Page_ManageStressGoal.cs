using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.Coaching
{
    class Page_ManageStressGoal
    {
        string pageName;
        SoftAssertions softassertions = null;
        private static string tdate;
        List<string[]> plan = new List<string[]>();
        List<string[]> actions = new List<string[]>();
        List<string[]> plandata = new List<string[]>();
        List<string[]> goalcompletiondata = new List<string[]>();

        Common cmn = new Common();
        public Page_ManageStressGoal()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_ManageStressGoal(SoftAssertions softassertions) : this()
        {
            this.softassertions = softassertions;
        }
        private void ClickManageStressGoal()
        {
            SeleniumKeywords.Click(pageName, "submenucoaching_managestress");
        }
        private void SetStressGoalForFutureDate()
        {
            List<string[]>currentstatus = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "currentstatus");
            for (int i = 0; i < currentstatus.Count; i++)
            {
                string elementname = currentstatus.ElementAt(i)[2];
                string elementlocatorname = currentstatus.ElementAt(i)[3];
                string expvalue = currentstatus.ElementAt(i)[4];
                int widthmultiple = 1;
                switch(expvalue)
                {
                    case "Barely any or none":
                        widthmultiple =0;
                        break;
                    case "A little bit":
                        widthmultiple =1;
                        break;
                    case "Some":
                        widthmultiple =2;
                        break;
                    case "Quite a bit":
                        widthmultiple =3;
                        break;
                    case "A lot":
                        widthmultiple =4;
                        break;

                }
                System.Threading.Thread.Sleep(5000);
                SeleniumKeywords.MoveAndClickAtSpecificPosition(pageName, elementlocatorname, 4, widthmultiple, 1,1);
                System.Threading.Thread.Sleep(2000);
                
            }

            //verify GOAL 


            tdate = cmn.AddDaysInCurrentDate(20);
            Console.WriteLine("Goal Date : "+tdate);
            JavaScriptKeywords.SetTextByControlId("DataPointDate", tdate);
        }
        private void ValidatePlanScreen()
        {

            System.Threading.Thread.Sleep(3000);
            plan = CSVReaderDataTable.GetCSVData("GoalsContent", pageName, "actions");
            for (int i = 0; i < plan.Count; i++)
            {
                string elementname = plan.ElementAt(i)[2];
                string locatorclassname = plan.ElementAt(i)[3];
                string elementlocatorname = plan.ElementAt(i)[4];
                string expectedtext = plan.ElementAt(i)[5];
                string actualtext = SeleniumKeywords.GetText(locatorclassname, elementlocatorname);
                softassertions.Add(elementname, expectedtext, actualtext, "contains");
            }

        }
        private void AddActions()
        {
            for (int i = 0; i < 4; i++)
            {
                System.Threading.Thread.Sleep(3000);
                SeleniumKeywords.Click("CommonGoals", "actionitem");
            }
        }
        private void ValidateActionsAdded()
        {
            System.Threading.Thread.Sleep(2000);
            List<string[]> result = new List<string[]>();
            actions = CSVReaderDataTable.GetCSVData("GoalsContent", pageName, "actionitems");
            for (int i = 0; i < actions.Count; i++)
            {
                string elementname = actions.ElementAt(i)[2];
                string locatorclassname = actions.ElementAt(i)[3];
                string elementlocatorname = actions.ElementAt(i)[4];
                string expectedtext = actions.ElementAt(i)[5];
                string actualtext = SeleniumKeywords.GetText(locatorclassname, elementlocatorname);
                softassertions.Add(elementname, expectedtext, actualtext, "contains");
            }
        }
        private void ValidateSetUpScreen()
        {
            System.Threading.Thread.Sleep(3000);
            plandata = CSVReaderDataTable.GetCSVData("GoalsContent", pageName, "setup");
            for (int i = 0; i < plandata.Count; i++)
            {
                string elementname = plandata.ElementAt(i)[2];
                string locatorclassname = plandata.ElementAt(i)[3];
                string elementlocatorname = plandata.ElementAt(i)[4];
                string expectedtext = plandata.ElementAt(i)[5];

                if (i == 5)
                {
                    expectedtext = Convert.ToDateTime(tdate).ToString("MMMM d, yyyy");
                }
                string actualtext = SeleniumKeywords.GetText(locatorclassname, elementlocatorname);
                softassertions.Add(elementname, expectedtext, actualtext, "contains");
            }
        }
        private void ValidateGoalCompletionScreen()
        {
            System.Threading.Thread.Sleep(3000);
            goalcompletiondata = CSVReaderDataTable.GetCSVData("GoalsContent", pageName, "completesetup");
            for (int i = 0; i < goalcompletiondata.Count; i++)
            {
                string elementname = goalcompletiondata.ElementAt(i)[2];
                string locatorclassname = goalcompletiondata.ElementAt(i)[3];
                string elementlocatorname = goalcompletiondata.ElementAt(i)[4];
                string expectedtext = goalcompletiondata.ElementAt(i)[5];
                string actualtext = SeleniumKeywords.GetText(locatorclassname, elementlocatorname);
                softassertions.Add(elementname, expectedtext, actualtext, "contains");
            }
        }

        public void SetGoal()
        {
            ClickManageStressGoal();
            SetStressGoalForFutureDate();
        }
        public void VerifyPlanScreen()
        {
            ValidatePlanScreen();
        }
        public void VerifyActionsAdded()
        {
            AddActions();
            ValidateActionsAdded();
        }
        public void VerifySetUpScreen()
        {
            System.Threading.Thread.Sleep(3000);
            ValidateSetUpScreen();
        }
        public void VerifyGoalComplete()
        {
            ValidateGoalCompletionScreen();
        }
        public void ValidateAtActionsAdded()
        {
            ValidateActionsAdded();
        }
    }
}
