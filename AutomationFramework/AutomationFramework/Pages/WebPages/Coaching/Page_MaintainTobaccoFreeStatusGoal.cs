using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AutomationFramework.Pages.WebPages.Coaching
{
    class Page_MaintainTobaccoFreeStatusGoal
    {
        SoftAssertions softassertions = null;
        String pageName;
        List<string[]> actiondata = new List<string[]>();
        List<string[]> actions = new List<string[]>();
        List<string[]> plandata = new List<string[]>();
        List<string[]> plan = new List<string[]>();
        CommonGoals cmn = new CommonGoals();

        public Page_MaintainTobaccoFreeStatusGoal()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_MaintainTobaccoFreeStatusGoal(SoftAssertions softassertions) : this()
        {
            this.softassertions = softassertions;
        }

        private void ClickMaintainTobaccoFreeStatusGoal()
        {
            SeleniumKeywords.Click(pageName, "tobaccofreestatus");
        }
        private void InputNoOfDays()
        {
            List<string[]> goaldata = CSVReaderDataTable.GetCSVData("InputDataContent", pageName, "tobaccofreedata");
            SeleniumKeywords.SetText(pageName, "daysquittb", goaldata.ElementAt(0)[4]);
  
        }
        private void ValidatePlanScreen()
        {
            
            actiondata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "actions");
            for (int i = 0; i < actiondata.Count; i++)
            {

                string elementname = actiondata.ElementAt(i)[2];
                string elementlocatorname = actiondata.ElementAt(i)[3];
                string expectedtext = actiondata.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                softassertions.Add(elementname, expectedtext, actualtext, "contains");
            }
          
        }

        private void ValidateSetUpScreen()
        {
            System.Threading.Thread.Sleep(3000);
            List<string[]> result = new List<string[]>();
            plandata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "setup");
            for(int i=0;i<plandata.Count;i++)
            {
                string elementname = plandata.ElementAt(i)[2];
                string elementlocatorname = plandata.ElementAt(i)[3];
                string expectedtext = plandata.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                softassertions.Add(elementname, expectedtext, actualtext, "contains");
            }
        }
        private void AddActions()
        {
            for (int i = 1; i <= 4; i++)
            {
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
        private List<String[]> ValidateGoalCompletionScreen()
        {
            System.Threading.Thread.Sleep(5000);
            List<string[]> result = new List<string[]>();
            plan = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "completesetup");
            for (int i = 0; i < plan.Count; i++)
            {
                string elementname = plan.ElementAt(i)[2];
                string elementlocatorname = plan.ElementAt(i)[3];
                string expectedtext = plan.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                softassertions.Add(elementname, expectedtext, actualtext, "contains");
            }
            return result;
        }

        public void SetGoal()
        {
            ClickMaintainTobaccoFreeStatusGoal();
            Thread.Sleep(4000);
            InputNoOfDays();
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
        public void ValidateAtActionsAdded()
        {
            AddActions();
            ValidateActionsAdded();
        }
    }
}
