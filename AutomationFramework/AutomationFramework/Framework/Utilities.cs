using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.IO;

namespace AutomationFramework.Framework
{
    class Utilities
    {
        /// <summary>
        /// Takes screenshot of the page where a test fails
        /// </summary>
        public string TakeScreenshot(IWebDriver driver)
        {
            string imgreportpath, imgpath;
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            string reportpath = GlobalVariables.reportpath;
            try
            {
                Directory.CreateDirectory(reportpath + "\\Screenshots_" + GlobalVariables.environment + "\\");
            }
            catch (IOException e)
            {
                reportpath = projectPath + "Reports\\BuildSanityReport\\";
                Directory.CreateDirectory(reportpath + "\\Screenshots_" + GlobalVariables.environment + "\\");
                Console.WriteLine(e.Message);
            }
            string imgname = TestContext.CurrentContext.Test.MethodName + DateTime.Now.ToString("yyyyMMdd") + "T" + DateTime.Now.ToString("HHmmss");

            imgreportpath = reportpath + "\\Screenshots_" + GlobalVariables.environment + "\\" + imgname + ".jpeg";
            imgpath = "Screenshots_" + GlobalVariables.environment + "\\" + imgname + ".jpeg";
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(imgreportpath, ScreenshotImageFormat.Jpeg);

            return imgpath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }

        /// <summary>
        /// Utility to write failed tests to text file
        /// </summary>
        /// <param name="text"></param>
        public void WriteToFile(string text)
        {
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            string fileloc = projectPath + "FailedTest\\" + GlobalVariables.environment + "\\" + GlobalVariables.clientname + "\\";
            Directory.CreateDirectory(fileloc);
            string path = fileloc + "TestsFailed-" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            try
            {
                if (!File.Exists(path))
                {
                    using (var tw = new StreamWriter(path, true))
                    {
                        tw.WriteLine("AutomationFramework.tests.UserCreation.TC_CreateUser");
                        tw.WriteLine(text);
                        tw.Close();
                    }
                }
                else if (File.Exists(path))
                {
                    using (var tw = new StreamWriter(path, true))
                    {
                        tw.WriteLine(text);
                        tw.Close();
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
    }
}
