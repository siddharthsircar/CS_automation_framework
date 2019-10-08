using AutomationFramework.Keywords;
using AutomationFramework.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AutomationFramework.Pages
{
    class Page_WeightProgressCheckIn
    {
        String pageName;
        SoftAssertions softassertions = null;

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_WeightProgressCheckIn()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        public Page_WeightProgressCheckIn(SoftAssertions softassertions) : this()
        {
            this.softassertions = softassertions;
        }

        /// <summary>
        /// This method will navigate to 'Progress Check In' frame with following steps -
        /// this will click on - Menu -> ProgressCheckIn -> Weight Management
        /// </summary>
        private void ClickWeightProgressCheckin()
        {
            SeleniumKeywords.Click(pageName, "menuweightmanagementlnk");
        }

        /// <summary>
        /// This method will validate 'Progress Check In' frame.
        /// This will validate frame title, close button, cancel button and continue button
        /// </summary>
        private void ValidateFrameElement()
        {
            System.Threading.Thread.Sleep(5000);
            
            string frametitle = SeleniumKeywords.GetText("CommonProgressCheckIn", "frametitle");
            softassertions.Add("Element: Weight_frame_title - ", "Weight Management Progress Check-in", frametitle,"equals");

        }
        
        /// <summary>
        /// This method will fill Progress Check In Questionnaire
        /// then submit form by clicking 'continue' button
        /// </summary>
        private void FillProgressCheckIn()
        {
            Console.WriteLine("Switching to iframe ..... ");
            System.Threading.Thread.Sleep(4000);
            SeleniumKeywords.NavigateToIFrame("assessmentFrame");

            System.Threading.Thread.Sleep(5000);
            
            //Fill ProgressCheckIn questionnaire
            SeleniumKeywords.SetText(pageName, "heightfeettxt", "4"); //Fill height in feet
            SeleniumKeywords.SetText(pageName, "heightinchestxt", "10"); //Fill height in inches
            SeleniumKeywords.SetText(pageName, "weightpoundstxt", "380"); //Fill weight in pounds
            SeleniumKeywords.SetText(pageName, "waistinchestxt", "40"); //Fill waist in inches
            SeleniumKeywords.Click(pageName, "dietarylotsofimprovementradio");//Select 'lots of improvement' for dietary
            SeleniumKeywords.SetText(pageName, "moderatehourstxt", "0"); //Fill moderate hours
            SeleniumKeywords.SetText(pageName, "moderateminstxt", "0"); //Fill moderate minutes
            SeleniumKeywords.SetText(pageName, "vigoroushourstxt", "0"); //Fill vigorous hours
            SeleniumKeywords.SetText(pageName, "vigorousmintxt", "0"); //Fill vigorous minutes
            SeleniumKeywords.Click(pageName, "currentloseweightradio"); //Select 'lose weight'
            SeleniumKeywords.Click(pageName, "manageweightyesradio"); //Select 'yes' for manage weight
            SeleniumKeywords.Click(pageName, "beginrightnowradio"); //Select 'right now'
            SeleniumKeywords.Click(pageName, "confidentyesradio"); //Select 'yes'
            SeleniumKeywords.Click(pageName, "dietschallengecheckbx"); //Check 'diets'
            SeleniumKeywords.Click("CommonProgressCheckIn", "framecontinuebtn"); //Click on 'Continue' button

        }
        /// <summary>
        /// This method will validate Report page that will generate automatically after successfully completing a progress check in
        /// Validate report header, progrescheck in name, all links appearing at the bottom of report page like -
        /// coaching program, track progress, course complete
        /// </summary>
        private void ValidateReport()
        {
            ValidateReportData();
        }

        public void VerifyProgressCheckinReportBottomLinks(String clientname)
        {

            //ClickWeightProgressCheckin();
            //FillProgressCheckIn();
            Common cmn = new Common(softassertions);
            cmn.ValidateReportBottomLinks(clientname,pageName);
            
        }
        private void ValidateReportData()
        { 
            List<string[]> reportdata = CSVReaderDataTable.GetCSVData("ProgressCheckinData", pageName, "reportdata");
            for (int i = 0; i < reportdata.Count; i++)
            {
                string elementlocatorname = reportdata.ElementAt(i)[2];
                string expreportdata = reportdata.ElementAt(i)[3];
                string actualreportdata = SeleniumKeywords.GetText("CommonProgressCheckIn", elementlocatorname);

                softassertions.Add("Element : "+elementlocatorname, expreportdata, actualreportdata,"contains");
           }

        }
        //private void ValidateReportBottomLinks(string clientname, string pageName1)
        //{
        //    List<string[]> reportbottomlinks = CSVReaderDataTable.GetCSVData("ProgressCheckinReportData", pageName1, "reportbottomlinks",clientname);
        //    for (int i = 0; i < reportbottomlinks.Count; i++)
        //    {
        //        string reportbottomlinklocatorname = reportbottomlinks.ElementAt(i)[3];
        //        string explinktext = reportbottomlinks.ElementAt(i)[4];
        //        string actuallinktext = SeleniumKeywords.GetText("CommonProgressCheckIn", reportbottomlinklocatorname);

        //        softassertions.Add("Element : "+reportbottomlinklocatorname, explinktext, actuallinktext,"contains");


        //        SeleniumKeywords.Click("CommonProgressCheckIn", reportbottomlinklocatorname);
        //        if(explinktext== "RETURN TO DASHBOARD")
        //        {
        //            Page_HAPrompt pha = new Page_HAPrompt();
        //            pha.GoToDashboard();
        //        }
        //        string report_bootomlinks_locatorclass = reportbottomlinks.ElementAt(i)[5];
        //        string navigated_page_element_locatorname = reportbottomlinks.ElementAt(i)[6];
        //        string exp_navigated_page_elementtext = reportbottomlinks.ElementAt(i)[7];
        //        string actual_navigated_page_elementtext = SeleniumKeywords.GetText(report_bootomlinks_locatorclass, navigated_page_element_locatorname);

        //        softassertions.Add("Element : " + navigated_page_element_locatorname, exp_navigated_page_elementtext, actual_navigated_page_elementtext,"equals");
        //        Console.WriteLine("Element: " + navigated_page_element_locatorname + " " + exp_navigated_page_elementtext +" " + actual_navigated_page_elementtext);
        //        if (explinktext != "RETURN TO DASHBOARD")
        //        {
        //            SeleniumKeywords.NavigateToPreviousPage();
        //        }
        //    }

        //}
 
        /// <summary>
        /// This Method contains various steps to complete a progress check in
        /// It will, in turn, call some methods to complete and validate progress check in
        /// </summary>
        public void CompleteProgressCheckIn()
        {
            
            ClickWeightProgressCheckin();
            ValidateFrameElement();
            FillProgressCheckIn();
            

        }  
        public void VerifyReport()
        {
            ValidateReport();
            
        }
        
        public List<string[]> GetQuestionErrorMessage()

        {
            SeleniumKeywords.NavigateToIFrame("assessmentFrame");
            System.Threading.Thread.Sleep(6000);

            Console.WriteLine("GetQuestionErrorMessage   function started.....");
           
            SeleniumKeywords.Click("CommonProgressCheckIn", "framecontinuebtn");
            List<string[]> expectedresult = new List<string[]>();
            List<string[]> result = new List<string[]>();
            Console.WriteLine("GetQuestionErrorMessage   data read function started.....");
            expectedresult = CSVReaderDataTable.GetCSVData("ProgressCheckinData", pageName, "errormsg");
           
          
            for (int i = 0; i < expectedresult.Count; i++)
            {
                string elementname = expectedresult.ElementAt(i)[2];
                string expelementtxt = expectedresult.ElementAt(i)[3];
                string actualelementtxt = SeleniumKeywords.GetText(pageName, elementname);

                bool textmatch = SeleniumKeywords.VerifyText(actualelementtxt, expelementtxt);
                string msg = "Element : " + elementname + " , Expected : " + expelementtxt + " , Actual : " + actualelementtxt;
                result.Add(new string[] { msg, textmatch.ToString() });
                Console.WriteLine(msg);
            }
            
            
            return result;

        }
        public List<string[]> VerifyMandatoryQuestionsErrorMessage()
        {
            ClickWeightProgressCheckin();
           
            List<string[]> resultTxt = GetQuestionErrorMessage();
            //SeleniumKeywords.NavigateToDefaultContent();
            SeleniumKeywords.Click("CommonProgressCheckIn", "framecancelbtn");
            SeleniumKeywords.HandelAlerts("OK");
            //SeleniumKeywords.NavigateToDefaultContent();

            return resultTxt;
        }
        public List<string[]> VerifyProgressCheckIn()
        {
            List<string[]> result = new List<string[]>();
            FillProgressCheckIn();
            //result.AddRange(ValidateReportData());
            
            return result;
        }

    }

    
}
