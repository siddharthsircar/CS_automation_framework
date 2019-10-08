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
    class Page_ImproveFitnessLevelGoal
    {
        string pageName;
        SoftAssertions softassertions = null;
        List<string[]> inputdata;
        private static string tdate;
        List<string[]> plan = new List<string[]>();
        List<string[]> actions = new List<string[]>();
        List<string[]> plandata = new List<string[]>();
        List<string[]> goalcompletiondata = new List<string[]>();

        Common cmn = new Common();
        public Page_ImproveFitnessLevelGoal()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_ImproveFitnessLevelGoal(SoftAssertions softassertions) : this()
        {
            this.softassertions = softassertions;
        }
        private void ClickImproveFitnessLevelSubmenuItem()
        {
            SeleniumKeywords.Click(pageName, "submenucoaching_improvefitnesslevel");
        }
        private void SetFitnessActivityGoalForFutureDate()
        {
            List<string[]>currentstatus = CSVReaderDataTable.GetCSVData("InputDataContent", pageName, "currentstatus");
            for (int i = 0; i < currentstatus.Count; i++)
            {
                string elementname = currentstatus.ElementAt(i)[2];
                string elementlocatorname = currentstatus.ElementAt(i)[3];
                string inputvalue = currentstatus.ElementAt(i)[4];
                SeleniumKeywords.SetText(pageName, elementlocatorname, inputvalue);
            }

            List<string[]> setgoalstatus = CSVReaderDataTable.GetCSVData("InputDataContent", pageName, "setgoalstatus");
            for (int i = 0; i < setgoalstatus.Count; i++)
            {
                string elementname = setgoalstatus.ElementAt(i)[2];
                string elementlocatorname = setgoalstatus.ElementAt(i)[3];
                string inputvalue = setgoalstatus.ElementAt(i)[4];
                SeleniumKeywords.SetText(pageName, elementlocatorname, inputvalue);
            }

            tdate = cmn.AddDaysInCurrentDate(20);
            Console.WriteLine("Goal Date : "+tdate);
            JavaScriptKeywords.SetTextByControlId("DataPointDate", tdate);
        }
        private void ValidatePlanScreen()
        {

            System.Threading.Thread.Sleep(3000);
            plan = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "actions");
            for (int i = 0; i < plan.Count; i++)
            {
                string elementname = plan.ElementAt(i)[2];
                string elementlocatorname = plan.ElementAt(i)[3];
                string expectedtext = plan.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                softassertions.Add(elementname, expectedtext, actualtext, "contains");
            }

        }
        private void AddActions()
        {
            for (int i = 0; i < 4; i++)
            {
                System.Threading.Thread.Sleep(3000);
                SeleniumKeywords.Click(pageName, "actionitem");
            }
        }
        private void ValidateActionsAdded()
        {
            System.Threading.Thread.Sleep(2000);
            List<string[]> result = new List<string[]>();
            actions = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "actionitems");
            for (int i = 0; i < actions.Count; i++)
            {
                string elementname = actions.ElementAt(i)[2];
                string elementlocatorname = actions.ElementAt(i)[3];
                string expectedtext = actions.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                softassertions.Add(elementname, expectedtext, actualtext, "contains");
            }
        }
        private void ValidateSetUpScreen()
        {
            System.Threading.Thread.Sleep(6000);
            plandata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "setup");
            for (int i = 0; i < plandata.Count; i++)
            {
                string elementname = plandata.ElementAt(i)[2];
                string elementlocatorname = plandata.ElementAt(i)[3];
                string expectedtext = plandata.ElementAt(i)[4];

                if (i == 5)
                {
                    expectedtext = Convert.ToDateTime(tdate).ToString("MMMM d, yyyy");
                }
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                softassertions.Add(elementname, expectedtext, actualtext, "contains");
            }
        }
        private void ValidateGoalCompletionScreen()
        {
            Thread.Sleep(3000);
            goalcompletiondata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "completesetup");
            for (int i = 0; i < goalcompletiondata.Count; i++)
            {
                string elementname = goalcompletiondata.ElementAt(i)[2];
                string elementlocatorname = goalcompletiondata.ElementAt(i)[3];
                string expectedtext = goalcompletiondata.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                softassertions.Add(elementname, expectedtext, actualtext, "contains");
            }
        }

        public void SetGoal()
        {
            ClickImproveFitnessLevelSubmenuItem();
            System.Threading.Thread.Sleep(4000);
            SetFitnessActivityGoalForFutureDate();
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
            ValidateSetUpScreen();
        }
        public void VerifyGoalComplete()
        {
            ValidateGoalCompletionScreen();
        }
    }
}
