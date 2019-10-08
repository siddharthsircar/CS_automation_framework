using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.ProgressCheckin
{
    class Page_ManagingStressProgressCheckIn
    {
        String pageName;
        SoftAssertions softassertions = null;

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_ManagingStressProgressCheckIn()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        public Page_ManagingStressProgressCheckIn(SoftAssertions softassertions) : this()
        {
            this.softassertions = softassertions;
        }
        private void ClickManagineStressProgressCheckin()
        {
            SeleniumKeywords.Click(pageName, "managingstressprogresscheckinlbl");
        }
        private void ValidateReportData()
        {
            List<string[]> reportdata = CSVReaderDataTable.GetCSVData("ProgressCheckinData", pageName, "managestressreportdata");
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
            softassertions.Add("Element: Stress_frame_title - ", "Managing Stress Progress Check-in", frametitle, "equals");

        }
        private void FillProgressCheckIn()
        {
            Console.WriteLine("Switching to iframe ..... ");
            System.Threading.Thread.Sleep(8000);
            SeleniumKeywords.NavigateToIFrame("assessmentFrame");
            System.Threading.Thread.Sleep(12000);
            SeleniumKeywords.Click(pageName, "physicalhealthconcerncheckbox");
            SeleniumKeywords.Click(pageName, "financeproblemyesradiobtn");
            SeleniumKeywords.Click(pageName, "yourworkimpactchkbox");
            SeleniumKeywords.Click(pageName, "betterwaytolearningstressradiobtn");
            SeleniumKeywords.Click(pageName, "successfullyreducingstressradiobtn");
            SeleniumKeywords.Click(pageName, "howabletocontrolstressradiobtn");
            SeleniumKeywords.Click(pageName, "howconfidenttocontrolstressradiobtn");
            SeleniumKeywords.Click(pageName, "thingsabletocontrolstressradiobtn");
            SeleniumKeywords.Click(pageName, "thingspileupstressradiobtn");
            SeleniumKeywords.Click("CommonProgressCheckIn", "framecontinuebtn"); //Click on 'Continue' button

        }

        public void CompleteProgressCheckIn()
        {

            ClickManagineStressProgressCheckin();
            ValidateFrameElement();
            FillProgressCheckIn();
        }
        public void VerifyReport()
        {
            ValidateReportData();
        }
        public void VerifyProgressCheckinReportBottomLinks(String clientname)
        {

            //ClickManagineStressProgressCheckin();
            //FillProgressCheckIn();
            Common cmn = new Common(softassertions);
            cmn.ValidateReportBottomLinks(clientname, pageName);

        }
        public void FillProgresscheckinFromCourse()
        {
            ValidateFrameElement();
            FillProgressCheckIn();
        }
    }
}
