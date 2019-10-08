using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.ProgressCheckin
{
    class Page_BloodPressureProgressCheckIn
    {
        String pageName;
        SoftAssertions softassertions = null;

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_BloodPressureProgressCheckIn()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        public Page_BloodPressureProgressCheckIn(SoftAssertions softassertions) : this()
        {
            this.softassertions = softassertions;
        }
        private void ClickBPProgressCheckin()
        {
            SeleniumKeywords.Click(pageName, "bpprogresscheckinlbl");
        }
        private void ValidateFrameElement()
        {
            System.Threading.Thread.Sleep(5000);

            string frametitle = SeleniumKeywords.GetText("CommonProgressCheckIn", "frametitle");
            softassertions.Add("Element: Weight_frame_title - ", "Blood Pressure Progress Check-in", frametitle, "equals");

        }
        private void ValidateReportData()
        {
            List<string[]> reportdata = CSVReaderDataTable.GetCSVData("ProgressCheckinData", pageName, "bpreportdata");
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
            SeleniumKeywords.SetText(pageName, "heightfeettb", "5");
            SeleniumKeywords.SetText(pageName, "heightinchtb", "10");
            SeleniumKeywords.SetText(pageName, "weighttb", "180");
            SeleniumKeywords.Click(pageName, "highbloodpressurestatusradiobtn");
            SeleniumKeywords.SetText(pageName, "systolictb", "120");
            SeleniumKeywords.SetText(pageName, "diastolictb", "80");
            SeleniumKeywords.Click(pageName, "diabetesstatusradiobtn");
            SeleniumKeywords.Click(pageName, "alcoholstatusradiobtn");
            SeleniumKeywords.SetText(pageName, "modacthourstb", "6");
            SeleniumKeywords.SetText(pageName, "modactmintb", "40");
            SeleniumKeywords.SetText(pageName, "vigoroushourstb", "2");
            SeleniumKeywords.SetText(pageName, "vigorousmintb", "15");
            SeleniumKeywords.Click(pageName, "fruitsstatusradiobtn");
            SeleniumKeywords.Click(pageName, "vegitablestatusradiobtn");
            SeleniumKeywords.Click(pageName, "dairystatusradiobtn");
            SeleniumKeywords.Click("CommonProgressCheckIn", "framecontinuebtn"); //Click on 'Continue' button
        }
        public void CompleteProgressCheckIn()
        {

            ClickBPProgressCheckin();
            ValidateFrameElement();
            FillProgressCheckIn();
        }
        public void VerifyReport()
        {
            ValidateReportData();
        }
        public void VerifyProgressCheckinReportBottomLinks(String clientname)
        {

            //ClickBPProgressCheckin();
            //FillProgressCheckIn();
            Common cmn = new Common(softassertions);
            cmn.ValidateReportBottomLinks(clientname, pageName);

        }
    }
}
