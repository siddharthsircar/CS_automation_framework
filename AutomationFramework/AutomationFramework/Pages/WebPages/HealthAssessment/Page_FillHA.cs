using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages
{
    class Page_FillHA
    {
        String pageName;
        SoftAssertions softAssertions = null;
        String inputfilename, CommonInputDataContentAttributeName;
        
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_FillHA()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_FillHA(SoftAssertions softAssertions):this()
        {
            this.softAssertions = softAssertions;
        }

        public void setInputFileName(string filename)
        {
            inputfilename = filename;
            if(inputfilename.ToLower().Equals("normalhadata"))
            {
                CommonInputDataContentAttributeName = "yourinfosecondsection";
            }
            else if(inputfilename.ToLower().Contains("highrisk"))
            {
                CommonInputDataContentAttributeName = "highrisk_yourinfosecondsection";
            }
        }
        private void ClickStartLink()
        {
            System.Threading.Thread.Sleep(5000);
            if (GlobalVariables.clientname.ToLower().Equals("medicare advantage"))
            {
                SeleniumKeywords.Click("Page_Journey", "journeytile_gobtn", "Health Needs Assessment","Take me there");
            }
            else
            {
                SeleniumKeywords.Click(pageName, "startlnk");
            }
            //SeleniumKeywords.Click(pageName, "hacontinuebtn");
            
        }

        private void FillYourInfoModule()
        {
            //System.Threading.Thread.Sleep(3000);
            List<string[]> haelements = new List<string[]>();

            haelements = CSVReaderDataTable.GetCSVData(inputfilename, pageName, "yourinfofirstsection");

            for (int i = 0; i < haelements.Count; i++)
            {
                string elementname = haelements.ElementAt(i)[2];
                string elementlocatorname = haelements.ElementAt(i)[3];
                string variablevalue = haelements.ElementAt(i)[4];
                JavaScriptKeywords.ScrollToAnElement(pageName, elementlocatorname, variablevalue);
                SeleniumKeywords.Click(pageName, elementlocatorname, variablevalue);
            }

            //SeleniumKeywords.Click(pageName, "yourinfo_overallhealth");
            //SeleniumKeywords.Click(pageName, "yourinfo_eatinghabits");
            //SeleniumKeywords.Click(pageName, "yourinfo_drinkhabits");
            //SeleniumKeywords.Click(pageName, "yourinfo_smokehabits");
            //SeleniumKeywords.Click(pageName, "yourinfo_cigarhabits");
            //SeleniumKeywords.Click(pageName, "yourinfo_pipeshabits");
            //SeleniumKeywords.Click(pageName, "yourinfo_smokelesshabits");

            //SeleniumKeywords.MoveToElement(pageName, "yourinfo_moderatehours");
            haelements = CSVReaderDataTable.GetCSVData("InputDataContent", pageName, CommonInputDataContentAttributeName);

            for (int i = 0; i < haelements.Count; i++)
            {
                string elementname = haelements.ElementAt(i)[2];
                string elementlocatorname = haelements.ElementAt(i)[3];
                string elementvalue = haelements.ElementAt(i)[4];
                JavaScriptKeywords.ScrollToAnElement(pageName, elementlocatorname);
                SeleniumKeywords.SetText(pageName, elementlocatorname, elementvalue);
            }

            haelements = CSVReaderDataTable.GetCSVData(inputfilename, pageName, "yourinfothirdsection");

            for (int i = 0; i < haelements.Count; i++)
            {
                string elementname = haelements.ElementAt(i)[2];
                string elementlocatorname = haelements.ElementAt(i)[3];
                string variablevalue = haelements.ElementAt(i)[4];
                JavaScriptKeywords.ScrollToAnElement(pageName, elementlocatorname, variablevalue);
                SeleniumKeywords.Click(pageName, elementlocatorname, variablevalue);
            }

            //SeleniumKeywords.Click(pageName, "yourinfo_stresseffect");
            //SeleniumKeywords.Click(pageName, "yourinfo_diabeticstatus");
            SeleniumKeywords.Click(pageName, "saveandcontinuebtn");
            System.Threading.Thread.Sleep(12000);
        }

        private void VerifySnaphotReport()
        {
            System.Threading.Thread.Sleep(8000);
            //SeleniumKeywords.NavigateToIFrame("assessmentFrame");
            //JavaScriptKeywords.Click(pageName, "sanpshot_continuebtn");
            List<string[]> hasnapshotdata = new List<string[]>();
            hasnapshotdata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "hasnapshotreport");
            for (int i = 0; i < hasnapshotdata.Count; i++)
            {
                string elementname = hasnapshotdata.ElementAt(i)[2];
                string elementlocatorname = hasnapshotdata.ElementAt(i)[3];
                string expectedtext = hasnapshotdata.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                //bool textmatch = actualtext.Contains(expectedtext);
                //string msg = "Element: " + elementname + "Expected: " + expectedtext + "Actual: " + actualtext;
                //result.Add(new string[] { msg, textmatch.ToString() });

                softAssertions.Add("Element : "+elementlocatorname,expectedtext,actualtext,"contains");
            }
            SeleniumKeywords.Click(pageName, "snapshot_continuebtn");
        
        }

        private void FillAboutYouModule()
        {
            //System.Threading.Thread.Sleep(3000);
            //SeleniumKeywords.NavigateToIFrame("assessmentFrame");

            List<string[]> haelements = new List<string[]>();

            haelements = CSVReaderDataTable.GetCSVData(inputfilename, pageName, "aboutyoumodule");

            for (int i = 0; i < haelements.Count; i++)
            {
                string elementname = haelements.ElementAt(i)[2];
                string elementlocatorname = haelements.ElementAt(i)[3];
                string variablevalue = haelements.ElementAt(i)[4];
                JavaScriptKeywords.ScrollToAnElement(pageName, elementlocatorname, variablevalue);
                SeleniumKeywords.Click(pageName, elementlocatorname, variablevalue);
            }
            //SeleniumKeywords.Click(pageName, "aboutyou_americanindian_racegroup");
            //SeleniumKeywords.Click(pageName, "aboutyou_always_safetybelt");
            //SeleniumKeywords.Click(pageName, "aboutyou_5_10years_screening");
            SeleniumKeywords.IframeScrollDown();
            SeleniumKeywords.Click(pageName, "saveandcontinuebtn");
        }

        private void FillYourVitalModule()
        {
            System.Threading.Thread.Sleep(8000);
            //SeleniumKeywords.NavigateToIFrame("assessmentFrame");

            List<string[]> haelements = new List<string[]>();

            haelements = CSVReaderDataTable.GetCSVData(inputfilename, pageName, "yourvitalsmodule");

            for (int i = 0; i < haelements.Count; i++)
            {
                string elementname = haelements.ElementAt(i)[2];
                string elementlocatorname = haelements.ElementAt(i)[3];
                string variablevalue = haelements.ElementAt(i)[4];
                JavaScriptKeywords.ScrollToAnElement(pageName, elementlocatorname, variablevalue);
                SeleniumKeywords.Click(pageName, elementlocatorname, variablevalue);
            }

            //SeleniumKeywords.Click(pageName, "yourvitals_1_3years_weightrange");
            //SeleniumKeywords.Click(pageName, "yourvitals_no_highbp");
            //SeleniumKeywords.Click(pageName, "yourvitals_no_highcholesterol");
            SeleniumKeywords.IframeScrollDown();
            SeleniumKeywords.Click(pageName, "saveandcontinuebtn");
        }

        private void FillDietModule()
        {
            System.Threading.Thread.Sleep(3000);
            //SeleniumKeywords.NavigateToIFrame("assessmentFrame");

            List<string[]> haelements = new List<string[]>();

            haelements = CSVReaderDataTable.GetCSVData(inputfilename, pageName, "dietmodule");

            for (int i = 0; i < haelements.Count; i++)
            {
                string elementname = haelements.ElementAt(i)[2];
                string elementlocatorname = haelements.ElementAt(i)[3];
                string variablevalue = haelements.ElementAt(i)[4];
                JavaScriptKeywords.ScrollToAnElement(pageName, elementlocatorname, variablevalue);
                SeleniumKeywords.Click(pageName, elementlocatorname, variablevalue);
            }

            //SeleniumKeywords.Click(pageName, "diet_3servings_fruit");
            //SeleniumKeywords.Click(pageName, "diet_3servings_vegetables");
            //SeleniumKeywords.Click(pageName, "diet_3servings_wholegrains");
            //SeleniumKeywords.Click(pageName, "diet_2servings_diaryproducts");
            //SeleniumKeywords.Click(pageName, "diet_3servings_fish");
            //SeleniumKeywords.Click(pageName, "diet_yes_continueeatinghabits");
            SeleniumKeywords.IframeScrollDown();
            SeleniumKeywords.Click(pageName, "saveandcontinuebtn");
        }

        private void FillTobaccoModule()
        {
            System.Threading.Thread.Sleep(3000);
            //SeleniumKeywords.NavigateToIFrame("assessmentFrame");

            List<string[]> haelements = new List<string[]>();

            haelements = CSVReaderDataTable.GetCSVData(inputfilename, pageName, "tobaccomodule");

            for (int i = 0; i < haelements.Count; i++)
            {
                string elementname = haelements.ElementAt(i)[2];
                string elementlocatorname = haelements.ElementAt(i)[3];
                string variablevalue = haelements.ElementAt(i)[4];
                JavaScriptKeywords.ScrollToAnElement(pageName, elementlocatorname, variablevalue);
                SeleniumKeywords.Click(pageName, elementlocatorname, variablevalue);
            }

            //SeleniumKeywords.Click(pageName, "tobacco_occasionally_secondhandsmoke");
            SeleniumKeywords.IframeScrollDown();
            SeleniumKeywords.Click(pageName, "saveandcontinuebtn");
        }

        private void FillHistoryModule()
        {
            System.Threading.Thread.Sleep(5000);
            //SeleniumKeywords.NavigateToIFrame("assessmentFrame");

            List<string[]> haelements = new List<string[]>();

            haelements = CSVReaderDataTable.GetCSVData(inputfilename, pageName, "historymodule");

            for (int i = 0; i < haelements.Count; i++)
            {
                string elementname = haelements.ElementAt(i)[2];
                string elementlocatorname = haelements.ElementAt(i)[3];
                string variablevalue = haelements.ElementAt(i)[4];
                JavaScriptKeywords.ScrollToAnElement(pageName, elementlocatorname, variablevalue);
                SeleniumKeywords.Click(pageName, elementlocatorname, variablevalue);
            }

            //SeleniumKeywords.Click(pageName, "history_no_cancer");
            //SeleniumKeywords.Click(pageName, "history_no_heartdisease");
            //SeleniumKeywords.Click(pageName, "history_no_osteoporosis");
            //SeleniumKeywords.Click(pageName, "history_no_migraine");
            //SeleniumKeywords.Click(pageName, "history_no_arthritis");
            //SeleniumKeywords.Click(pageName, "history_no_asthma");
            //SeleniumKeywords.Click(pageName, "history_no_backpain");
            SeleniumKeywords.IframeScrollDown();
            SeleniumKeywords.Click(pageName, "saveandcontinuebtn");
        }

        private void FillActivityModule()
        {
            System.Threading.Thread.Sleep(5000);
            //SeleniumKeywords.NavigateToIFrame("assessmentFrame");
            List<string[]> haelements = new List<string[]>();

            haelements = CSVReaderDataTable.GetCSVData(inputfilename, pageName, "activitymodule");

            for (int i = 0; i < haelements.Count; i++)
            {
                string elementname = haelements.ElementAt(i)[2];
                string elementlocatorname = haelements.ElementAt(i)[3];
                string variablevalue = haelements.ElementAt(i)[4];
                JavaScriptKeywords.ScrollToAnElement(pageName, elementlocatorname, variablevalue);
                SeleniumKeywords.Click(pageName, elementlocatorname, variablevalue);
            }

            //SeleniumKeywords.Click(pageName, "activity_morethan1year_healthylevel");
            SeleniumKeywords.IframeScrollDown();
            SeleniumKeywords.Click(pageName, "saveandcontinuebtn");
        }

        private void FillEmotionalHealthModule()
        {
            System.Threading.Thread.Sleep(3000);
            //SeleniumKeywords.NavigateToIFrame("assessmentFrame");

            List<string[]> haelements = new List<string[]>();

            haelements = CSVReaderDataTable.GetCSVData(inputfilename, pageName, "emotionalhealthmodule");

            for (int i = 0; i < haelements.Count; i++)
            {
                string elementname = haelements.ElementAt(i)[2];
                string elementlocatorname = haelements.ElementAt(i)[3];
                string variablevalue = haelements.ElementAt(i)[4];
                JavaScriptKeywords.ScrollToAnElement(pageName, elementlocatorname, variablevalue);
                SeleniumKeywords.Click(pageName, elementlocatorname, variablevalue);
            }

            //SeleniumKeywords.Click(pageName, "emotionalhealth_no_lostallinterest");
            //SeleniumKeywords.Click(pageName, "emotionalhealth_no_feltdepressed_feltokaysometimes");
            //SeleniumKeywords.Click(pageName, "emotionalhealth_no_feltdepressed_muchofthetime");
            //SeleniumKeywords.Click(pageName, "emotionalhealth_sometimes_unabletocontrolthings");
            //SeleniumKeywords.Click(pageName, "emotionalhealth_sometimes_handlepersonalproblems");
            //SeleniumKeywords.Click(pageName, "emotionalhealth_sometimes_thingsgoingyourway");
            //SeleniumKeywords.Click(pageName, "emotionalhealth_sometimes_difficultiespillingup");
            SeleniumKeywords.IframeScrollDown();
            SeleniumKeywords.Click(pageName, "saveandcontinuebtn");
        }

        private void FillAtWorkModule()
        {
            System.Threading.Thread.Sleep(5000);
            //SeleniumKeywords.NavigateToIFrame("assessmentFrame");

            List<string[]> haelements = new List<string[]>();

            haelements = CSVReaderDataTable.GetCSVData(inputfilename, pageName, "atworkmodule");

            for (int i = 0; i < haelements.Count; i++)
            {
                string elementname = haelements.ElementAt(i)[2];
                string elementlocatorname = haelements.ElementAt(i)[3];
                string variablevalue = haelements.ElementAt(i)[4];
                JavaScriptKeywords.ScrollToAnElement(pageName, elementlocatorname, variablevalue);
                SeleniumKeywords.Click(pageName, elementlocatorname, variablevalue);
            }

            //SeleniumKeywords.Click(pageName, "atwork_no_currentlyemployed");
            SeleniumKeywords.IframeScrollDown();
            SeleniumKeywords.Click(pageName, "saveandcontinuebtn");
        }

        private void FillReviewModule()
        {
           // System.Threading.Thread.Sleep(3000);
            //SeleniumKeywords.NavigateToIFrame("assessmentFrame");

            List<string[]> haelements = new List<string[]>();

            haelements = CSVReaderDataTable.GetCSVData(inputfilename, pageName, "reviewmodule");

            for (int i = 0; i < haelements.Count; i++)
            {
                string elementname = haelements.ElementAt(i)[2];
                string elementlocatorname = haelements.ElementAt(i)[3];
                string variablevalue = haelements.ElementAt(i)[4];
                JavaScriptKeywords.ScrollToAnElement(pageName, elementlocatorname, variablevalue);
                SeleniumKeywords.Click(pageName, elementlocatorname, variablevalue);
            }

            //SeleniumKeywords.Click(pageName, "atwork_no_currentlyemployed");
            SeleniumKeywords.IframeScrollDown();
            SeleniumKeywords.Click(pageName, "saveandcontinuebtn");
        }

        private void FillHAModules()
        {
            List<string[]> haelementdata = new List<string[]>();
            haelementdata = CSVReaderDataTable.GetCSVData(inputfilename, pageName, "hamoduletitle");
            for (int i = 0; i < haelementdata.Count; i++)
            {

                System.Threading.Thread.Sleep(3000);
                SeleniumKeywords.NavigateToDefaultContent();
                System.Threading.Thread.Sleep(5000);

                string elementname = haelementdata.ElementAt(i)[2];
                string elementlocatorname = haelementdata.ElementAt(i)[3];
                string expectedtext = haelementdata.ElementAt(i)[5];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                //bool textmatch = actualtext.Contains(expectedtext);
                //string msg = "Element: " + elementname + "Expected: " + expectedtext + "Actual: " + actualtext;
                //result.Add(new string[] { msg, textmatch.ToString() });

                softAssertions.Add("Element : " + elementlocatorname, expectedtext, actualtext, "contains");

                SeleniumKeywords.NavigateToIFrame("assessmentFrame");
                System.Threading.Thread.Sleep(3000);

                switch (elementname)
                {
                    case "YOUR INFO HAFrameTitle":
                        FillYourInfoModule();
                        break;
                    case "Your Snapshot Report HAFrameTitle":
                        VerifySnaphotReport();
                        break;
                    case "ABOUT YOU HAFrameTitle":
                        FillAboutYouModule();
                        break;
                    case "YOUR VITALS HAFrameTitle":
                        FillYourVitalModule();
                        break;
                    case "DIET HAFrameTitle":
                        FillDietModule();
                        break;
                    case "TOBACCO HAFrameTitle":
                        FillTobaccoModule();
                        break;
                    case "HISTORY HAFrameTitle":
                        FillHistoryModule();
                        break;
                    case "ACTIVITY HAFrameTitle":
                        FillActivityModule();
                        break;
                    case "EMOTIONAL HEALTH HAFrameTitle":
                        FillEmotionalHealthModule();
                        break;
                    case "AT WORK HAFrameTitle":
                        FillAtWorkModule();
                        break;
                    case "REVIEW  HAFrameTitle":
                        FillReviewModule();
                        break;

                }
                System.Threading.Thread.Sleep(4000);

            }

           

        }
        private void CompleteHA()
        {
            System.Threading.Thread.Sleep(20000);
            string ha_congratulations_text = SeleniumKeywords.GetText(pageName, "ha_congratulations_text");
            //bool textmatch = ha_congratulations_text.Contains("CONGRATULATIONS!");
            //string msg = "Element: HA_Congratulation_Text Expected: CONGRATULATIONS! Actual: " + ha_congratulations_text;
            //result.Add(new string[] { msg, textmatch.ToString() });
            softAssertions.Add("Element : ha_congratulations_text", "CONGRATULATIONS!", ha_congratulations_text, "contains");

            SeleniumKeywords.NavigateToDefaultContent();
            System.Threading.Thread.Sleep(5000);
            SeleniumKeywords.Click(pageName, "hacompletedclosebtn");
        }

        private void ClickContinueToGoToDashboard()
        {

            List<string[]> continuebtnelement = new List<string[]>();

            continuebtnelement = CSVReaderDataTable.GetCSVData(inputfilename, pageName, "continuebtntogotodashboard");

            for (int i = 0; i < continuebtnelement.Count; i++)
            {
                string elementname = continuebtnelement.ElementAt(i)[2];
                string elementlocatorname = continuebtnelement.ElementAt(i)[3];
                JavaScriptKeywords.ScrollToAnElement(pageName, elementlocatorname);
                SeleniumKeywords.Click(pageName, elementlocatorname);
            }

            //string ha_congratulations_text = SeleniumKeywords.GetText(pageName, "ha_congratulations_text");
            ////bool textmatch = ha_congratulations_text.Contains("CONGRATULATIONS!");
            ////string msg = "Element: HA_Congratulation_Text Expected: CONGRATULATIONS! Actual: " + ha_congratulations_text;
            ////result.Add(new string[] { msg, textmatch.ToString() });
            //softAssertions.Add("Element : ha_congratulations_text", "CONGRATULATIONS!", ha_congratulations_text, "contains");

            //SeleniumKeywords.NavigateToDefaultContent();
            //System.Threading.Thread.Sleep(5000);
            //SeleniumKeywords.Click(pageName, "hacompletedclosebtn");

        }

        private void VerifyHAIsCompleted()
        {
            System.Threading.Thread.Sleep(5000);
            string dashboard_ha_completion_percentage = SeleniumKeywords.GetText(pageName, "dashboard_ha_100per");
            //bool textmatch = dashboard_ha_completion_percentage.Equals("100%");
            //string msg = "Element: Dashboard_HA_Completion_Percentage Expected: 100% Actual: " + dashboard_ha_completion_percentage;
            //result.Add(new string[] { msg, textmatch.ToString() });

            softAssertions.Add("Element : dashboard_ha_100per", "100%", dashboard_ha_completion_percentage, "contains");
            
        }
        
        public void FillHA()
        {
            ClickStartLink();
            FillHAModules();
            CompleteHA();
            ClickContinueToGoToDashboard();
            VerifyHAIsCompleted();
        }

        public void ValidateHAReport()
        {
            System.Threading.Thread.Sleep(3000);
            SeleniumKeywords.Click(pageName, "dashboard_ha_viewreportlink");
            System.Threading.Thread.Sleep(3000);
            List<string[]> hareports = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "hareports");
            for (int i = 0; i < hareports.Count; i++)
            {
                string elementlocatorname = hareports.ElementAt(i)[3];
                string expreportdata = hareports.ElementAt(i)[4];
                string actualreportdata = SeleniumKeywords.GetText(pageName, elementlocatorname);

                //bool textmatch = actualreportdata.Trim().Contains(expreportdata.Trim());
                //string msg = "Element : " + elementlocatorname + " , Expected : " + expreportdata + " , Actual : " + actualreportdata;
                //result.Add(new string[] { msg, textmatch.ToString() });

                softAssertions.Add("Element : "+ elementlocatorname, expreportdata, actualreportdata, "contains");

            }
            
        }
    }
}
