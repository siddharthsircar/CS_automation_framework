using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace AutomationFramework.Pages.WebPages.Coaching
{
    class Page_ImproveCholesterolGoal
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
        public Page_ImproveCholesterolGoal()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_ImproveCholesterolGoal(SoftAssertions softassertions) : this()
        {
            this.softassertions = softassertions;
        }
        private void ClickImproveCholesterolGoal()
        {
            SeleniumKeywords.Click(pageName, "improve_cholestrolgoal_lbl");
        }
        private void InputBPStatus()
        {
            SeleniumKeywords.SetText(pageName, "current_totalcholestero_ltb", "250");
            SeleniumKeywords.SetText(pageName, "current_ldl_tb", "100");
            SeleniumKeywords.SetText(pageName, "current_hdl_tb", "55");
            SeleniumKeywords.SetText(pageName, "current_triglycerides_tb", "222");
            SeleniumKeywords.SetText(pageName, "goal_totalcholesterol_tb", "180");
            SeleniumKeywords.SetText(pageName, "goal_ldl_tb", "80");
            SeleniumKeywords.SetText(pageName, "goal_hdl_tb", "65");
            SeleniumKeywords.SetText(pageName, "goal_triglycerides_tb", "140");
            tdate =cmn.AddDaysInCurrentDate(20);
            JavaScriptKeywords.SetTextByControlId("DataPointDate", tdate);
        }
        private void ValidatePlanScreen()
        {
            
            System.Threading.Thread.Sleep(10000);
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
            for(int i=0;i<4;i++)
            {
                System.Threading.Thread.Sleep(1000);
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
            System.Threading.Thread.Sleep(3000);
            plandata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "setup");
            for (int i = 0; i < plandata.Count; i++)
            {
                string elementname = plandata.ElementAt(i)[2];
                string elementlocatorname = plandata.ElementAt(i)[3];
                string expectedtext="";

                if (i == 5)
                {
                    try
                    {
                        expectedtext = Convert.ToDateTime(tdate).ToString("MMMM d, yyyy", CultureInfo.InvariantCulture);
                    }
                    catch (System.FormatException fe)
                    {
                        Console.WriteLine("Date format is not correct");
                    }
                }
                else
                {
                    expectedtext = plandata.ElementAt(i)[4];
                }
               
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                softassertions.Add(elementname, expectedtext, actualtext, "contains");
            }
        }
        private void ValidateGoalCompletionScreen()
        {
            Thread.Sleep(3000);
            goalcompletiondata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "completesetup");
            for(int i=0;i<goalcompletiondata.Count;i++)
            {
                string elementname = goalcompletiondata.ElementAt(i)[2];
                string elementlocatorname = goalcompletiondata.ElementAt(i)[3];
                string expectedtext = goalcompletiondata.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                softassertions.Add(elementname, expectedtext, actualtext, "contains");
            }
        }

        public void InputCholesterolValues()
        {
            ClickImproveCholesterolGoal();
            System.Threading.Thread.Sleep(3000);
            InputBPStatus();
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
            ValidateActionsAdded();
        }
    }
}
