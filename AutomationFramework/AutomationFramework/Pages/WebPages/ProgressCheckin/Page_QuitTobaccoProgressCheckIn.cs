using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.ProgressCheckin
{
    class Page_QuitTobaccoProgressCheckIn
    {
        String pageName;
        SoftAssertions softassertions = null;

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_QuitTobaccoProgressCheckIn()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        public Page_QuitTobaccoProgressCheckIn(SoftAssertions softassertions) : this()
        {
            this.softassertions = softassertions;
        }
        private void ClickQuitTobaccoProgressCheckin()
        {
            SeleniumKeywords.Click(pageName, "quittobaccoprogresscheckinlbl");
        }
        private void ValidateReportData()
        {
            List<string[]> reportdata = CSVReaderDataTable.GetCSVData("ProgressCheckinData", pageName, "qtreportdata");
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
            softassertions.Add("Element: Weight_frame_title - ", "Quit Tobacco Progress Check-in", frametitle, "equals");

        }
        private void FillProgressCheckIn()
        {
            Console.WriteLine("Switching to iframe ..... ");
            System.Threading.Thread.Sleep(8000);
            SeleniumKeywords.NavigateToIFrame("assessmentFrame");
            System.Threading.Thread.Sleep(12000);
            SeleniumKeywords.Click(pageName, "cighabitradiobtn");
            SeleniumKeywords.Click(pageName, "cigarhabitradiobtn");
            SeleniumKeywords.Click(pageName, "pipeshabitradiobtn");
            SeleniumKeywords.Click(pageName, "smokelesshabitradiobtn");
            SeleniumKeywords.Click("CommonProgressCheckIn", "framecontinuebtn"); //Click on 'Continue' button
        }
        public void CompleteProgressCheckIn()
        {

            ClickQuitTobaccoProgressCheckin();
            ValidateFrameElement();
            FillProgressCheckIn();
        }
        public void VerifyReport()
        {
            ValidateReportData();
        }

        public void VerifyProgressCheckinReportBottomLinks(String clientname)
        {

            //ClickQuitTobaccoProgressCheckin();
            //FillProgressCheckIn();
            Common cmn = new Common(softassertions);
            cmn.ValidateReportBottomLinks(clientname, pageName);

        }
    }

}
