using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.ProgressCheckin
{
    class Page_PreventiveHealthProgressCheckIn
    {
        String pageName;
        SoftAssertions softassertions = null;

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_PreventiveHealthProgressCheckIn()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        public Page_PreventiveHealthProgressCheckIn(SoftAssertions softassertions) : this()
        {
            this.softassertions = softassertions;
        }
        private void ClickPreventiveHealthProgressCheckin()
        {
            SeleniumKeywords.Click(pageName, "preventivehealthprogresscheckinlbl");
        }
        private void ValidateReportData()
        {
            List<string[]> reportdata = CSVReaderDataTable.GetCSVData("ProgressCheckinData", pageName, "phreportdata");
            for (int i = 0; i < reportdata.Count; i++)
            {
                string elementlocatorname = reportdata.ElementAt(i)[2];
                string expreportdata = reportdata.ElementAt(i)[3];
                string actualreportdata = SeleniumKeywords.GetText("CommonProgressCheckIn", elementlocatorname);

                softassertions.Add("Element : " + elementlocatorname, expreportdata, actualreportdata, "contains");
            }
        }
        private void ValidateFrameElement()
        {
            System.Threading.Thread.Sleep(5000);

            string frametitle = SeleniumKeywords.GetText("CommonProgressCheckIn", "frametitle");
            softassertions.Add("Element: Weight_frame_title - ", "Preventive Health Progress Check-in", frametitle, "equals");

        }
        private void FillProgressCheckIn()
        {
            Console.WriteLine("Switching to iframe ..... ");
            System.Threading.Thread.Sleep(8000);
            SeleniumKeywords.NavigateToIFrame("assessmentFrame");
            System.Threading.Thread.Sleep(12000);
            SeleniumKeywords.SetText(pageName, "heightfeettb", "5");
            SeleniumKeywords.SetText(pageName, "heightinchtb", "5");
            SeleniumKeywords.SetText(pageName, "weighttb", "180");
            SeleniumKeywords.SetText(pageName, "waistinchtb", "36");

            SeleniumKeywords.Click(pageName, "doctorstatusradiobtn");
            SeleniumKeywords.Click(pageName, "fluvaccinationradiobtn");
            SeleniumKeywords.Click(pageName, "fluupdateradiobtn");
            //SeleniumKeywords.Click(pageName, "asprindailyradiobtn");

            SeleniumKeywords.Click(pageName, "coloretralscreenradiobtn");
            SeleniumKeywords.Click(pageName, "typeofscreenradiobtn");
            SeleniumKeywords.Click(pageName, "bloodpressuremeasureradiobtn");
            SeleniumKeywords.Click(pageName, "cholesterolmeasureradiobtn");
            //SeleniumKeywords.Click(pageName, "mammogrammeasureradiobtn");
            //SeleniumKeywords.Click(pageName, "papsmearmmeasureradiobtn");
            //SeleniumKeywords.Click(pageName, "bonedensityradiobtn");
            //JavaScriptKeywords.ScrollToAnElement(pageName, "cigaretteradiobtn");
    
            SeleniumKeywords.Click(pageName, "cigaretteradiobtn");
            SeleniumKeywords.Click(pageName, "pipesradiobtn");
            SeleniumKeywords.Click(pageName, "cigarradiobtn");
            SeleniumKeywords.Click(pageName, "smokelessradiobtn");
            SeleniumKeywords.Click(pageName, "secondhandsmokeradiobtn");
            SeleniumKeywords.SetText(pageName, "modacthrstb", "5");
            SeleniumKeywords.SetText(pageName, "modactmintb", "25");
            SeleniumKeywords.SetText(pageName, "vigacthrstb", "2");
            SeleniumKeywords.SetText(pageName, "vigactmintb", "10");
            SeleniumKeywords.Click(pageName, "fruitconsumeradiobtn");
            SeleniumKeywords.Click(pageName, "vegconsumeradiobtn");
            SeleniumKeywords.Click(pageName, "drinkstatusradiobtn");
            SeleniumKeywords.Click("CommonProgressCheckIn", "framecontinuebtn"); //Click on 'Continue' button

        }
        public void CompleteProgressCheckIn()
        {

            ClickPreventiveHealthProgressCheckin();
            ValidateFrameElement();
            FillProgressCheckIn();
        }
        public void VerifyReport()
        {
            ValidateReportData();
        }
        public void VerifyProgressCheckinReportBottomLinks(String clientname)
        {

            //ClickPreventiveHealthProgressCheckin();
            //FillProgressCheckIn();
            Common cmn = new Common(softassertions);
            cmn.ValidateReportBottomLinks(clientname, pageName);

        }
        public void VerifyProgressCheckinCommonReportBottomLinks(String clientname)
        {
            
            //ClickPreventiveHealthProgressCheckin();
            //FillProgressCheckIn();
            Common cmn = new Common(softassertions);
            cmn.ValidateReportBottomLinks(clientname, pageName);

        }
    }
}
