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
    class Page_ImproveFitnessLevel
    {
        String pageName;
        List<string[]> labeldata = new List<string[]>();
        List<string[]> actiondata = new List<string[]>();
        List<string[]> actions = new List<string[]>();
        List<string[]> set = new List<string[]>();
        List<string[]> plan = new List<string[]>();
        List<string[]> remove = new List<string[]>();
        CommonGoals cmn = new CommonGoals();
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_ImproveFitnessLevel()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        private void ClickMaintainWeightGoal()
        {
            SeleniumKeywords.Click(pageName, "maintainweighttxt");
        }

        private void InputWeight()
        {
            List<string[]> goaldata = CSVReaderDataTable.GetCSVData("CommonInputDataContent", pageName, "weightgoaldata");

            int weightvalue = Convert.ToInt32(goaldata.ElementAt(0)[4]);

            SeleniumKeywords.SetText(pageName, "weighttb",weightvalue.ToString());
        }


        private List<String[]> VerifyActions()
        {
            List<string[]> result = new List<string[]>();
            actiondata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "actions");
            for(int i=0;i<actiondata.Count;i++)
            {

                string elementname = actiondata.ElementAt(i)[2];
                string elementlocatorname = actiondata.ElementAt(i)[3];
                string expectedtext = actiondata.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                bool textmatch = SeleniumKeywords.VerifyTextContains(actualtext, expectedtext);
                string msg = "Element: " + elementname + "Expected: " + expectedtext + "Actual: " + actualtext;
                result.Add(new string[] { msg, textmatch.ToString() });
            }
            return result;
        }
        private List<String[]> VerifyElements()
        {
            System.Threading.Thread.Sleep(3000);
            List<string[]> result = new List<string[]>();
            labeldata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "controls");
            for(int i=0;i<labeldata.Count;i++)
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
        private void ClickActionTiles()
        {
            for (int i=1;i<=4;i++)
            {
                System.Threading.Thread.Sleep(2000);
                SeleniumKeywords.Click(pageName, "actionitem");
            }
        }
        private List<String[]> ValidateActionsAdded()
        {
            System.Threading.Thread.Sleep(2000);
            List<string[]> result = new List<string[]>();
            actions = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "actionitem");
            for(int i=0;i<actions.Count;i++)
            {
                string elementname = actions.ElementAt(i)[2];
                string elementlocatorname = actions.ElementAt(i)[3];
                string expectedtext = actions.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                bool textmatch = SeleniumKeywords.VerifyTextContains(actualtext, expectedtext);
                string msg = "Element: " + elementname + "Expected: " + expectedtext + "Actual: " + actualtext;
                result.Add(new string[] { msg, textmatch.ToString() });

            }
            return result;
        }

        private List<String[]> ValidateSetUpScreen()
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
                bool textmatch = SeleniumKeywords.VerifyTextContains(actualtext, expectedtext);
                string msg = "Element: " + elementname + "Expected: " + expectedtext + "Actual: " + actualtext;
                result.Add(new string[] { msg, textmatch.ToString() });

            }
            return result;
        }

        private List<String[]> ValidatePlanScreen()
        {
            System.Threading.Thread.Sleep(5000);
            List<string[]> result = new List<string[]>();
            plan = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "plan");
            for (int i = 0; i < plan.Count; i++)
            {
                string elementname = plan.ElementAt(i)[2];
                string elementlocatorname = plan.ElementAt(i)[3];
                string expectedtext = plan.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                bool textmatch = SeleniumKeywords.VerifyTextContains(actualtext, expectedtext);
                string msg = "Element: " + elementname + "Expected: " + expectedtext + "Actual: " + actualtext;
                result.Add(new string[] { msg, textmatch.ToString() });
            }
            return result;
        }

        private List<String[]> VerifyRemoveScreen()
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
                bool textmatch = SeleniumKeywords.VerifyTextContains(actualtext, expectedtext);
                string msg = "Element: " + elementname + "Expected: " + expectedtext + "Actual: " + actualtext;
                result.Add(new string[] { msg, textmatch.ToString() });
            }
            return result;
        }

        public List<String[]>ValidateElements()
        {
            ClickMaintainWeightGoal();
            List<string[]> result = VerifyElements();
            return result;
        }
        public List<String[]>ValidateActionItems()
        {
            InputWeight();
            cmn.ClickStep1NextBtn();
            System.Threading.Thread.Sleep(5000);
            List<string[]> result = VerifyActions();
            return result;
        }
        public List<string[]> ActionsAdded()
        {
            ClickActionTiles();
            System.Threading.Thread.Sleep(2000);
            List<string[]> result = ValidateActionsAdded();
            return result;
        }
        public List<string[]> VerifySetupScreen()
        {
            cmn.ClickStep2NextBtn();
            List<string[]> result = ValidateSetUpScreen();
            return result;
        }
        public List<string[]>VerifyPlanScreen()
        {
            cmn.ClickConfirmBtn();
            List<string[]> result = ValidatePlanScreen();
            return result;
        }
        public List<string[]> VerifyRemoveGoalPopUp()
        {
            
            System.Threading.Thread.Sleep(4000);
            List<string[]> result = VerifyRemoveScreen();
            cmn.ClickRemoveBtn();
            return result;
        }
        public void RemoveGoal()
        {
            cmn.ClickRemoveScreenYesBtn();
        }
    }
}