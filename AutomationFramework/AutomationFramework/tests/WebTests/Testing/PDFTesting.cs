using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using AutomationFramework.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests.WebTests.Testing
{
    [TestFixture]
    public class PDFTesting:Base
    {
        [Test]
        public void TC_PDFTestMethod()
        {
            PDFReaderUtility preader = new PDFReaderUtility();
            //string text = preader.GetTextFromPDFFile("E:\\pdfs\\PDFReport.pdf");
            //Console.WriteLine("----------------- PDF text : \n"+text);

            //string columntext = preader.GetColumnWiseTextFromPDFFile("E:\\pdfs\\PDFReport.pdf");
            //Console.WriteLine("\n\n----------------- PDF column text : \n" + columntext);

            //Page_Login plogin = new Page_Login();
            //plogin.Login();
            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();



            //(new WebClient()).DownloadFile("https://qa2012b-member.onlifehealth.com/content/pdf/Financial_Well_being/Financial_Well_Being.pdf", "E:\\pdfs\\PDFReportt.pdf");

            //byte[] b = File.ReadAllBytes("E:\\pdfs\\PDFReportt.pdf");
            //Console.WriteLine("File Contentttttt : "+b.ToString());

            //string linetext = preader.GetLineWiseTextFromPDFFile("E:\\pdfs\\PDFReportt.pdf");
            //Console.WriteLine("\n\n----------------- PDF line text : \n" + linetext);

            CommonUtilityKeywords utilityKeywords = new CommonUtilityKeywords();
            string pdfcontent = utilityKeywords.GetPDFContentFromURL("https://qa2012b-member.onlifehealth.com/content/pdf/Financial_Well_being/Financial_Well_Being.pdf");
            Console.WriteLine("PDF content : "+pdfcontent);

            
        }
    }
}
