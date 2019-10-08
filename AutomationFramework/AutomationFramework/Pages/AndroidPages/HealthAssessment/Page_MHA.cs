using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationFramework.Pages.AndroidPages.HealthAssessment
{
    class Page_MHA
    {
        String pageName;
        SoftAssertions softAssertions = null;
        String inputfilename, CommonInputDataContentAttributeName;

        List<string[]> hainputvalue = new List<string[]>();
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_MHA()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        public Page_MHA(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }

        private void TapStartLink()
        {
            AppiumKeywords.Tap("Common", "haicon");
        }

        public void setInputFileName(string filename)
        {
            inputfilename = filename;
            if (inputfilename.ToLower().Equals("normalhadata"))
            {
                CommonInputDataContentAttributeName = "LowRisk_HAInfoSection";
            }
            else if (inputfilename.ToLower().Contains("highrisk"))
            {
                CommonInputDataContentAttributeName = "HighRisk_HAInfoSection";
            }
        }

        private void FillYourInfoModule()
        {
            string elementType="";
            if (elementType.ToLower().Equals("radiobutton"))
            {
                //AppiumKeywords.Tap();
            }
            else if (elementType.ToLower().Equals("textbox"))
            {
                //AppiumKeywords.SetText();
            }
        }

        private void VerifySanphotReport()
        {
            System.Threading.Thread.Sleep(3000);
            //AppiumKeywords.NavigateToIFrame("assessmentFrame");
            //JavaScriptKeywords.Tap(pageName, "sanpshot_continuebtn");
            AppiumKeywords.Tap(pageName, "sanpshot_continuebtn");
        }

        private void FillAboutYouModule()
        {
            //System.Threading.Thread.Sleep(3000);
            //AppiumKeywords.NavigateToIFrame("assessmentFrame");
            AppiumKeywords.Tap(pageName, "aboutyou_americanindian_racegroup");
            AppiumKeywords.Tap(pageName, "aboutyou_always_safetybelt");
            AppiumKeywords.Tap(pageName, "aboutyou_5_10years_screening");
            AppiumKeywords.Tap(pageName, "saveandcontinuebtn");
        }

        private void FillYourVitalModule()
        {
            //System.Threading.Thread.Sleep(5000);
            //AppiumKeywords.NavigateToIFrame("assessmentFrame");

            AppiumKeywords.Tap(pageName, "yourvitals_1_3years_weightrange");
            AppiumKeywords.Tap(pageName, "yourvitals_no_highbp");
            AppiumKeywords.Tap(pageName, "yourvitals_no_highcholesterol");
            AppiumKeywords.Tap(pageName, "saveandcontinuebtn");
        }

        private void FillDietModule()
        {
            //System.Threading.Thread.Sleep(3000);
            //AppiumKeywords.NavigateToIFrame("assessmentFrame");
            AppiumKeywords.Tap(pageName, "diet_3servings_fruit");
            AppiumKeywords.Tap(pageName, "diet_3servings_vegetables");
            AppiumKeywords.Tap(pageName, "diet_3servings_wholegrains");
            AppiumKeywords.Tap(pageName, "diet_2servings_diaryproducts");
            AppiumKeywords.Tap(pageName, "diet_3servings_fish");
            AppiumKeywords.Tap(pageName, "diet_yes_continueeatinghabits");
            AppiumKeywords.Tap(pageName, "saveandcontinuebtn");
        }

        private void FillTobaccoModule()
        {
            //System.Threading.Thread.Sleep(3000);
            //AppiumKeywords.NavigateToIFrame("assessmentFrame");
            AppiumKeywords.Tap(pageName, "tobacco_occasionally_secondhandsmoke");
            AppiumKeywords.Tap(pageName, "saveandcontinuebtn");
        }

        private void FillHistoryModule()
        {
            //System.Threading.Thread.Sleep(5000);
            //AppiumKeywords.NavigateToIFrame("assessmentFrame");
            AppiumKeywords.Tap(pageName, "history_no_cancer");
            AppiumKeywords.Tap(pageName, "history_no_heartdisease");
            AppiumKeywords.Tap(pageName, "history_no_osteoporosis");
            AppiumKeywords.Tap(pageName, "history_no_migraine");
            AppiumKeywords.Tap(pageName, "history_no_arthritis");
            AppiumKeywords.Tap(pageName, "history_no_asthma");
            AppiumKeywords.Tap(pageName, "history_no_backpain");
            AppiumKeywords.Tap(pageName, "saveandcontinuebtn");
        }

        private void FillActivityModule()
        {
            //System.Threading.Thread.Sleep(5000);
            //AppiumKeywords.NavigateToIFrame("assessmentFrame");
            AppiumKeywords.Tap(pageName, "activity_morethan1year_healthylevel");
            AppiumKeywords.Tap(pageName, "saveandcontinuebtn");
        }

        private void FillEmotionalHealthModule()
        {
            //System.Threading.Thread.Sleep(3000);
            //AppiumKeywords.NavigateToIFrame("assessmentFrame");
            AppiumKeywords.Tap(pageName, "emotionalhealth_no_lostallinterest");
            AppiumKeywords.Tap(pageName, "emotionalhealth_no_feltdepressed_feltokaysometimes");
            AppiumKeywords.Tap(pageName, "emotionalhealth_no_feltdepressed_muchofthetime");
            AppiumKeywords.Tap(pageName, "emotionalhealth_sometimes_unabletocontrolthings");
            AppiumKeywords.Tap(pageName, "emotionalhealth_sometimes_handlepersonalproblems");
            AppiumKeywords.Tap(pageName, "emotionalhealth_sometimes_thingsgoingyourway");
            AppiumKeywords.Tap(pageName, "emotionalhealth_sometimes_difficultiespillingup");
            AppiumKeywords.Tap(pageName, "saveandcontinuebtn");
        }

        private void FillAtWorkModule()
        {
            //System.Threading.Thread.Sleep(3000);
            //AppiumKeywords.NavigateToIFrame("assessmentFrame");
            AppiumKeywords.Tap(pageName, "atwork_no_currentlyemployed");
            AppiumKeywords.Tap(pageName, "saveandcontinuebtn");
        }

        private List<string[]> FillHAModules()
        {
            List<string[]> result = new List<string[]>();
            List<string[]> haelementdata = new List<string[]>();
            haelementdata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "hamoduletitle");
            for (int i = 0; i < haelementdata.Count; i++)
            {
                
                string elementname = haelementdata.ElementAt(i)[2];
                string elementlocatorname = haelementdata.ElementAt(i)[3];
                string expectedtext = haelementdata.ElementAt(i)[4];
                string actualtext = AppiumKeywords.GetText(pageName, elementlocatorname);
                bool textmatch = actualtext.Contains(expectedtext);
                string msg = "Element: " + elementname + "Expected: " + expectedtext + "Actual: " + actualtext;
                result.Add(new string[] { msg, textmatch.ToString() });

                System.Threading.Thread.Sleep(3000);

                switch (i)
                {
                    case 0:
                        FillYourInfoModule();
                        break;
                    case 1:
                        VerifySanphotReport();
                        break;
                    case 2:
                        FillAboutYouModule();
                        break;
                    case 3:
                        FillYourVitalModule();
                        break;
                    case 4:
                        FillDietModule();
                        break;
                    case 5:
                        FillTobaccoModule();
                        break;
                    case 6:
                        FillHistoryModule();
                        break;
                    case 7:
                        FillActivityModule();
                        break;
                    case 8:
                        FillEmotionalHealthModule();
                        break;
                    case 9:
                        FillAtWorkModule();
                        break;

                }
                System.Threading.Thread.Sleep(2000);

            }

            result.AddRange(CompleteHA());

            return result;
        }
        private List<string[]> CompleteHA()
        {
            List<string[]> result = new List<string[]>();

            string ha_congratulations_text = AppiumKeywords.GetText(pageName, "ha_congratulations_text");
            bool textmatch = ha_congratulations_text.Contains("CONGRATULATIONS!");
            string msg = "Element: HA_Congratulation_Text Expected: CONGRATULATIONS! Actual: " + ha_congratulations_text;
            result.Add(new string[] { msg, textmatch.ToString() });

            System.Threading.Thread.Sleep(3000);
            AppiumKeywords.Tap(pageName, "hacompletedclosebtn");

            return result;

        }

        private List<string[]> VerifyHAIsCompleted()
        {
            List<string[]> result = new List<string[]>();

            string dashboard_ha_completion_percentage = AppiumKeywords.GetText(pageName, "dashboard_ha_100per");
            bool textmatch = dashboard_ha_completion_percentage.Equals("100%");
            string msg = "Element: Dashboard_HA_Completion_Percentage Expected: 100% Actual: " + dashboard_ha_completion_percentage;
            result.Add(new string[] { msg, textmatch.ToString() });

            return result;
        }

        public List<string[]> FillHA()
        {
            List<string[]> result = new List<string[]>();

            TapStartLink();

            result.AddRange(FillHAModules());

            CompleteHA();
            result.AddRange(VerifyHAIsCompleted());

            return result;
        }

        public List<string[]> ValidateHAReport()
        {
            List<string[]> result = new List<string[]>();
            AppiumKeywords.Tap(pageName, "dashboard_ha_viewreportlink");

            List<string[]> hareports = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "hareports");
            for (int i = 0; i < hareports.Count; i++)
            {
                string elementlocatorname = hareports.ElementAt(i)[2];
                string expreportdata = hareports.ElementAt(i)[3];
                string actualreportdata = AppiumKeywords.GetText(pageName, elementlocatorname);

                bool textmatch = actualreportdata.Trim().Contains(expreportdata.Trim());
                string msg = "Element : " + elementlocatorname + " , Expected : " + expreportdata + " , Actual : " + actualreportdata;
                result.Add(new string[] { msg, textmatch.ToString() });
            }
            return result;
        }

    }
}
