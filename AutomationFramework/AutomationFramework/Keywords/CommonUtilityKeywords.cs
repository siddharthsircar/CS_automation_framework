using AutomationFramework.Framework;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Keywords
{
    /// <summary>
    /// Keywords class not derived from any test framework/tool
    /// </summary>
    public class CommonUtilityKeywords
    {
        private static ExtentTestManager Report = new ExtentTestManager();
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// this method verify whether the two passed strings are equal or not
        /// </summary>
        /// <param name="expectedTxt"></param>
        /// <param name="actualTxt"></param>
        /// <returns></returns>
        public static Boolean VerifyText(string expectedTxt, string actualTxt)
        {
            return (expectedTxt.Trim().Equals(actualTxt.Trim()));
        }

        /// <summary>
        /// this method verify whether the string two is a part of string one 
        /// </summary>
        /// <param name="actualTxt"></param>
        /// <param name="expectedTxt"></param>
        /// <returns></returns>
        public static Boolean VerifyTextContains(string actualTxt, string expectedTxt)
        {
            return (actualTxt.Trim().Contains(expectedTxt.Trim()));
        }

        /// <summary>
        /// Get PDF content from a metnioned URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetPDFContentFromURL(string url)
        {
            string filepath = GetTempPDFFilePath();
            StringBuilder text = new StringBuilder();
            Console.WriteLine("Filepath : "+filepath);
            Report.Pass("Getting PDF content ... "+filepath);
            log.Info("Getting PDF content ...");
            if (File.Exists(filepath))
            {
                //string[] lines;
                (new WebClient()).DownloadFile(url,filepath);

                Report.Pass("PDf File : "+filepath + " exists");
                log.Info("PDf File : " + filepath + " exists");

                PdfReader reader = new PdfReader(filepath);

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    ITextExtractionStrategy strategy = new LocationTextExtractionStrategy();

                    string page = PdfTextExtractor.GetTextFromPage(reader, i, strategy);
                    string[] lines = page.Split('\n');
                    for (int j = 0; j < lines.Length; j++)
                    {
                        string line = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(lines[j]));
                        text.Append(line);
                    }

                }
                reader.Close();
                RemoveTempFile(filepath);
            }
            else
            {
                Report.Fail("PDf File :" + filepath+" does not exists");
                log.Error(TestContext.CurrentContext.Test.MethodName + "PDf File :" + filepath + " does not exists");
            }

            return text.ToString();
        }

        private string GetTempPDFFilePath()
        { 
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            string pdf_path = new Uri(actualPath).LocalPath;
            pdf_path = pdf_path.ToString() + "tempforpdf\\pdfdatafile1.pdf";
            FileInfo fileinfo = new FileInfo(pdf_path);
            //pdf_path = pdf_path.ToString() + "tempforpdf";
            Directory.CreateDirectory(fileinfo.Directory.FullName);
            //pdf_path = pdf_path+ "\\pdfdatafile.pdf";
            var myfile = File.Create(pdf_path);
            myfile.Close();
            Console.WriteLine("PDFpath  : "+pdf_path);
            return pdf_path;
        }

        private void RemoveTempFile(string filepath)
        {
            File.Delete(filepath);
        }

        /// <summary>
        /// Gets the total no of pages in a PDF file
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public int GetTotalNoOfPagesInPDFFile(string url)
        {
            int total_no_of_pages = 0;
            string filepath = GetTempPDFFilePath();
            Console.WriteLine("Filepath : " + filepath);
            
            Report.Pass("Getting Total No. Of Pages in PDF File ..." + filepath);
            log.Info("Getting Total No. Of Pages in PDF File ...");
            if (File.Exists(filepath))
            {
                (new WebClient()).DownloadFile(url, filepath);
                //string[] lines;
                Report.Pass("PDf File : " + filepath + " exists");
                log.Info("PDf File : " + filepath + " exists");

                PdfReader reader = new PdfReader(filepath);
                total_no_of_pages = reader.NumberOfPages;
                
                reader.Close();
                //RemoveTempFile(filepath);
            }
            else
            {
                Report.Fail("PDf File :" + filepath + " does not exists");
                log.Error(TestContext.CurrentContext.Test.MethodName + "PDf File :" + filepath + " does not exists");
            }

            return total_no_of_pages;
        }

        /// <summary>
        /// Returns a random valid US number
        /// </summary>
        /// <returns></returns>
        public static string RandomUSNumberGenerator()
        {
            Random rand = new Random();
            StringBuilder telNo = new StringBuilder(12);
            int number;
            number = 615; // digit between 0 (incl) and 8 (excl)
            telNo = telNo.Append(number.ToString());
            //telNo = telNo.Append("-");
            number = rand.Next(1, 743); // number between 0 (incl) and 743 (excl)
            telNo = telNo.Append(String.Format("{0:D3}", number));
            //telNo = telNo.Append("-");
            number = rand.Next(0, 10000); // number between 0 (incl) and 10000 (excl)
            telNo = telNo.Append(String.Format("{0:D4}", number));
            return telNo.ToString();
        }
    }
}
