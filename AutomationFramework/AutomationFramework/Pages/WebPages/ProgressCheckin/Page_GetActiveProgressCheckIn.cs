using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AutomationFramework.Pages.WebPages.ProgressCheckin
{
    class Page_GetActiveProgressCheckIn
    {
        string pageName;
        SoftAssertions softassertions = null;

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_GetActiveProgressCheckIn()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        public Page_GetActiveProgressCheckIn(SoftAssertions softassertions) : this()
        {
            this.softassertions = softassertions;
        }
        private void ClickGetActiveProgressCheckin()
        {
            SeleniumKeywords.Click(pageName, "getactiveprogresscheckinlbl");
        }
        private void ValidateFrameElement()
        {
            System.Threading.Thread.Sleep(5000);

            string frametitle = SeleniumKeywords.GetText("CommonProgressCheckIn", "frametitle");
            softassertions.Add("Element: Weight_frame_title - ", "Get Active Progress Check-in", frametitle, "equals");
        }
        private void ValidateReportData()
        {
            List<string[]> reportdata = CSVReaderDataTable.GetCSVData("ProgressCheckinData", pageName, "ghreportdata");
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
            SeleniumKeywords.SetText(pageName, "modhrsacticitytb", "5");
            SeleniumKeywords.SetText(pageName, "modminacticitytb", "5");
            SeleniumKeywords.SetText(pageName, "vighrsacticitytb", "1");
            SeleniumKeywords.SetText(pageName, "vigminacticitytb", "10");
            SeleniumKeywords.Click(pageName, "musclebuildingstatusradiobtn");
            SeleniumKeywords.IframeScrollDown();
            Thread.Sleep(2000);
            SeleniumKeywords.Click(pageName, "stretchingstatusradiobtn");
            SeleniumKeywords.Click(pageName, "durationhealthylevelradiobtn");
            SeleniumKeywords.Click(pageName, "statusofphysicalactivityradiobtn");
            SeleniumKeywords.Click(pageName, "confidencelevelmaintainactivityradiobtn");
            SeleniumKeywords.IframeScrollDown();
            Thread.Sleep(2000);
            SeleniumKeywords.Click("CommonProgressCheckIn", "framecontinuebtn"); //Click on 'Continue' button

        }
        public void CompleteProgressCheckIn()
        {

            ClickGetActiveProgressCheckin();
            ValidateFrameElement();
            FillProgressCheckIn();
        }
        public void VerifyReport()
        {
            ValidateReportData();
        }
        public void VerifyProgressCheckinReportBottomLinks(String clientname)
        {

            //ClickGetActiveProgressCheckin();
            //FillProgressCheckIn();
            Common cmn = new Common(softassertions);
            cmn.ValidateReportBottomLinks(clientname, pageName);

        }
    }

}
