using AutomationFramework.Keywords;
using AutomationFramework.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomationFramework.Pages.WebPages.Coaching;

namespace AutomationFramework.Pages
{
    class Page_MaintainWeightGoal
    {
        String pageName;
        List<string[]> labeldata = new List<string[]>();
        List<string[]> actiondata = new List<string[]>();
        List<string[]> actions = new List<string[]>();
        List<string[]> set = new List<string[]>();
        List<string[]> plan = new List<string[]>();
        List<string[]> remove = new List<string[]>();
        CommonGoals cmn = new CommonGoals();
        SoftAssertions softassertions = null;
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_MaintainWeightGoal()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_MaintainWeightGoal(SoftAssertions softassertions) : this()
        {
            this.softassertions = softassertions;
        }
        private void ClickMaintainWeightGoal()
        {
            SeleniumKeywords.Click(pageName, "maintainweighttxt");
        }

        private void InputCurrentWeight()
        {
            List<string[]> goaldata = CSVReaderDataTable.GetCSVData("InputDataContent", pageName, "weightgoaldata");

            int weightvalue = Convert.ToInt32(goaldata.ElementAt(0)[4]);

            SeleniumKeywords.SetText(pageName, "weighttb", weightvalue.ToString());
        }


        private void ValidatePlanScreen()
        {
            List<string[]> result = new List<string[]>();
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
        private void AddActions()
        {
            for (int i = 1; i <= 4; i++)
            {
                //System.Threading.Thread.Sleep(1000);
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
        private List<String[]> VerifyElements()
        {
            System.Threading.Thread.Sleep(3000);
            List<string[]> result = new List<string[]>();
            labeldata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "controls");
            for (int i = 0; i < labeldata.Count; i++)
            {
                string elementname = labeldata.ElementAt(i)[2];
                string elementlocatorname = labeldata.ElementAt(i)[3];
                string expectedtext = labeldata.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                bool textmatch = SeleniumKeywords.VerifyText(actualtext, expectedtext);
                string msg = "Element: " + elementname + "Expected: " + expectedtext + "Actual: " + actualtext;
                result.Add(new string[] { msg, textmatch.ToString() });
            }
            return result;
        }



        private void ValidateSetUpScreen()
        {
            System.Threading.Thread.Sleep(5000);
            List<string[]> result = new List<string[]>();
            set = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "setup");
            for (int i = 0; i < set.Count; i++)
            {
                string elementname = set.ElementAt(i)[2];
                string elementlocatorname = set.ElementAt(i)[3];
                string expectedtext = set.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                softassertions.Add(elementname, expectedtext, actualtext, "contains");

            }

        }
        private void ValidateGoalCompletionScreen()
        {
            System.Threading.Thread.Sleep(4000);
            List<string[]> result = new List<string[]>();
            remove = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "plan");
            for (int i = 0; i < remove.Count; i++)
            {
                string elementname = remove.ElementAt(i)[2];
                string elementlocatorname = remove.ElementAt(i)[3];
                string expectedtext = remove.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                softassertions.Add(elementname, expectedtext, actualtext, "contains");
            }
        }
        public void InputWeight()
        {
            ClickMaintainWeightGoal();
            System.Threading.Thread.Sleep(3000);
            InputCurrentWeight();
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
