using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.FinancialWellBeing
{
    class Page_FinancialWellBeing
    {
        String pageName;
        SoftAssertions softAssertions = null;
        CommonUtilityKeywords utilityKeywords = new CommonUtilityKeywords();

        public Page_FinancialWellBeing()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_FinancialWellBeing(SoftAssertions softAssertion) : this()
        {
            this.softAssertions = softAssertion;
        }
        private void VerifyFinancialWellBeingScreen(string clientname)
        {
            VerifyFinancialWellBeingTilesData("tiledata");

            if (clientname.ToLower().Equals("health trust"))
            {
                VerifyFinancialWellBeingTilesData("moreresourcesusernametiledata");
            }
            else
            {
                VerifyFinancialWellBeingTilesData("moreresourcestiledata");
            }
        }

        private void VerifyFinancialWellBeingTilesData(string category)
        {
            List<string[]> finwellbeingscreendata = new List<string[]>();
            finwellbeingscreendata = CSVReaderDataTable.GetCSVData("FinancialWellBeingContent", pageName, category);

            System.Threading.Thread.Sleep(4000);
            for (int i = 0; i < finwellbeingscreendata.Count; i++)
            {
                string elementname = finwellbeingscreendata.ElementAt(i)[2];
                string elementlocatorname = finwellbeingscreendata.ElementAt(i)[3];
                string expectedtext = finwellbeingscreendata.ElementAt(i)[4];
                string inputvariable = finwellbeingscreendata.ElementAt(i)[5];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname, inputvariable);
                softAssertions.Add("Element : " + elementlocatorname, expectedtext, actualtext, "contains");
            }
        }

        private void VerifyFinancialWellBeingTiles(string clientname)
        {
            List<string[]> finwellbeingviewbtn = new List<string[]>();
            finwellbeingviewbtn = CSVReaderDataTable.GetCSVData("FinancialWellBeingContent", pageName, "tileviewbtn");
            List<string[]> finwellbeingpdfpages = new List<string[]>();
            finwellbeingpdfpages = CSVReaderDataTable.GetCSVData("FinancialWellBeingContent", pageName, "pdfpages");
            List<string[]> finwellbeingpdfcontent = new List<string[]>();
            finwellbeingpdfcontent = CSVReaderDataTable.GetCSVData("FinancialWellBeingContent", pageName, "pdfcontent");

            int len = finwellbeingviewbtn.Count;
            int totalpdf = len;
            if(clientname.ToLower().Equals("health trust") || clientname.ToLower().Equals("meabt"))
            {
                totalpdf = len - 1;
            }

            Console.WriteLine("Total pdf pages : "+ totalpdf);
            for (int i = 0; i < totalpdf; i++)
            {
                string viewbtn_elementname = finwellbeingviewbtn.ElementAt(i)[2];
                string viewbtn_elementlocatorname = finwellbeingviewbtn.ElementAt(i)[3];
                string viewbtn_varinputvalue = finwellbeingviewbtn.ElementAt(i)[5];

                SeleniumKeywords.Click(pageName, viewbtn_elementlocatorname, viewbtn_varinputvalue);

                Console.WriteLine("Switch to second tab");
                SeleniumKeywords.SwitchToTab(2);
                System.Threading.Thread.Sleep(5000);

                string actual_page_url = SeleniumKeywords.GetPageUrl();

                string pdfpages_elementname = finwellbeingpdfpages.ElementAt(i)[2];
                string pdfpages_urlvalue = finwellbeingpdfpages.ElementAt(i)[3];
                string pdfpages_expected_no_of_pages = finwellbeingpdfpages.ElementAt(i)[4];

                Console.WriteLine("Actual URL : "+actual_page_url);
                Console.WriteLine("Expected URL : " + pdfpages_urlvalue);
                if (actual_page_url.ToLower().Contains(pdfpages_urlvalue.ToLower()))
                {
                    int pdfpages_actual_no_of_pages = utilityKeywords.GetTotalNoOfPagesInPDFFile(actual_page_url);
                    Console.WriteLine("PDF pages : " + pdfpages_actual_no_of_pages);
                    softAssertions.Add("Element : " + pdfpages_elementname, pdfpages_expected_no_of_pages, pdfpages_actual_no_of_pages.ToString(), "equals");

                    string pdfcontent_elementname = finwellbeingpdfcontent.ElementAt(i)[2];
                    string pdfcontent_expectedtext = finwellbeingpdfcontent.ElementAt(i)[4];

                    string pdfcontent_actualtext = utilityKeywords.GetPDFContentFromURL(actual_page_url);
                    Console.WriteLine("PDF content : " + pdfcontent_actualtext);
                    softAssertions.Add("Element : " + pdfcontent_elementname, pdfcontent_expectedtext, pdfcontent_actualtext.ToString(), "contains");

                }
                else
                {
                    softAssertions.Add("Element : Pdf URl " , pdfpages_urlvalue, actual_page_url.ToString(), "contains");
                }
                Console.WriteLine("Close Current Tab");
                SeleniumKeywords.CloseCurrentTab();
                SeleniumKeywords.SwitchToTab(1);
                Console.WriteLine("Switch to first tab");
            }

            if (clientname.ToLower().Equals("health trust"))
            {
                VerifyMoreResourcesTile(finwellbeingviewbtn.ElementAt(len - 1)[3], finwellbeingviewbtn.ElementAt(len - 1)[5], "moreresourcesusername");
            }
            else if (clientname.ToLower().Equals("meabt"))
            {
                VerifyMoreResourcesTile(finwellbeingviewbtn.ElementAt(len - 1)[3], finwellbeingviewbtn.ElementAt(len - 1)[5], "moreresources");
            }
            

        }


        private void VerifyMoreResourcesTile(string viewbtn_elementlocatorname,string viewbtn_varinputvalue,string category)
        {
            SeleniumKeywords.Click(pageName, viewbtn_elementlocatorname, viewbtn_varinputvalue);

            Console.WriteLine("Switch to second tab");
            SeleniumKeywords.SwitchToTab(2);

            List<string[]> finwellbeingmoreresources = new List<string[]>();
            finwellbeingmoreresources = CSVReaderDataTable.GetCSVData("FinancialWellBeingContent", pageName, category);


            System.Threading.Thread.Sleep(4000);

            for (int i = 0; i < finwellbeingmoreresources.Count; i++)
            {
                string elementname = finwellbeingmoreresources.ElementAt(i)[2];
                string elementlocatorname = finwellbeingmoreresources.ElementAt(i)[3];
                string expected_display_status = finwellbeingmoreresources.ElementAt(i)[4];

                Boolean actual_display_status = SeleniumKeywords.IsElementPresent(pageName, elementlocatorname);
                softAssertions.Add("Element : " + elementname, expected_display_status, actual_display_status.ToString(), "equals");
            }
            Console.WriteLine("Close Current Tab");
            SeleniumKeywords.CloseCurrentTab();
            SeleniumKeywords.SwitchToTab(1);
            Console.WriteLine("Switch to first tab");
        }

        public void VerifyFinancialWellBeingData(string clientname)
        {
            VerifyFinancialWellBeingScreen(clientname);
            VerifyFinancialWellBeingTiles(clientname);
        }
    }
}
