using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.ProgressCheckin
{
    class Page_ImprovingNutritionProgressCheckIn
    {
        string pageName;
        SoftAssertions softassertions = null;

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_ImprovingNutritionProgressCheckIn()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        public Page_ImprovingNutritionProgressCheckIn(SoftAssertions softassertions) : this()
        {
            this.softassertions = softassertions;
        }
        private void ClickImprovingNutritionProgressCheckin()
        {
            SeleniumKeywords.Click(pageName, "improvingnutritionprogresscheckinlbl");
        }
        private void ValidateFrameElement()
        {
            System.Threading.Thread.Sleep(5000);

            string frametitle = SeleniumKeywords.GetText("CommonProgressCheckIn", "frametitle");
            softassertions.Add("Element: Weight_frame_title - ", "Improving Nutrition Progress Check-in", frametitle, "equals");
        }
        private void ValidateReportData()
        {
            List<string[]> reportdata = CSVReaderDataTable.GetCSVData("ProgressCheckinData", pageName, "inreportdata");
            for (int i = 0; i < reportdata.Count; i++)
            {
                string elementlocatorname = reportdata.ElementAt(i)[2];
                string expreportdata = reportdata.ElementAt(i)[3];
                string actualreportdata = SeleniumKeywords.GetText("CommonProgressCheckIn", elementlocatorname);

                softassertions.Add("Element : " + elementlocatorname, expreportdata, actualreportdata, "contains");
            }
        }
        private void FillProgressCheckIn()
        {
            Console.WriteLine("Switching to iframe ..... ");
            System.Threading.Thread.Sleep(8000);
            SeleniumKeywords.NavigateToIFrame("assessmentFrame");
            System.Threading.Thread.Sleep(12000);
            SeleniumKeywords.Click(pageName, "fruitconsumeradiobtn");
            SeleniumKeywords.Click(pageName, "fruitjuicestatusradiobtn");
            SeleniumKeywords.Click(pageName, "vegetablesconsumeradiobtn");
            SeleniumKeywords.Click(pageName, "DietVegiecontainsradiobtn");
            SeleniumKeywords.Click(pageName, "grainsconsumeradiobtn");
            SeleniumKeywords.Click(pageName, "wholegrainsconsumeradiobtn");
            SeleniumKeywords.Click(pageName, "dairyprodconsumeradiobtn");
            SeleniumKeywords.Click(pageName, "poultryconsumeradiobtn");
            SeleniumKeywords.Click(pageName, "unsaturatedfatconsumeradiobtn");
            SeleniumKeywords.Click(pageName, "saturatedfatconsumeradiobtn");
            SeleniumKeywords.Click(pageName, "sodiumconsumeradiobtn");
            SeleniumKeywords.Click(pageName, "sugarconsumeradiobtn");
            SeleniumKeywords.Click(pageName, "alcoholconsumeradiobtn");
            SeleniumKeywords.Click(pageName, "currentweightsatisfactionstatusradiobtn");
            SeleniumKeywords.SetText(pageName, "currentweighttb", "180");
            SeleniumKeywords.SetText(pageName, "heightinfeettb", "5");
            SeleniumKeywords.SetText(pageName, "heightininchtb", "5");
            SeleniumKeywords.SetText(pageName, "modactivityhrstb", "5");
            SeleniumKeywords.SetText(pageName, "modactivitymintb", "5");
            SeleniumKeywords.SetText(pageName, "vigactivityhrstb", "5");
            SeleniumKeywords.SetText(pageName, "vigactivitymintb", "5");
            SeleniumKeywords.Click(pageName, "improvinghabitstatusradiobtn");
            SeleniumKeywords.Click(pageName, "wellbalancedliferadiobtn");
            SeleniumKeywords.Click(pageName, "wellbalancedeatingradiobtn");
            SeleniumKeywords.Click(pageName, "challengeeatingcheckbox");
            SeleniumKeywords.Click("CommonProgressCheckIn", "framecontinuebtn"); //Click on 'Continue' button
        }
        public void CompleteProgressCheckIn()
        {

            ClickImprovingNutritionProgressCheckin();
            ValidateFrameElement();
            FillProgressCheckIn();
        }
        public void VerifyReport()
        {
            ValidateReportData();
        }

        public void VerifyProgressCheckinReportBottomLinks(String clientname)
        {

            //ClickImprovingNutritionProgressCheckin();
            //FillProgressCheckIn();
            Common cmn = new Common(softassertions);
            cmn.ValidateReportBottomLinks(clientname, pageName);

        }
    }


}
