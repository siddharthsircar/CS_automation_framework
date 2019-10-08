using AutomationFramework.Framework;
using NUnit.Framework;
using System.IO;
using Sikuli4Net.sikuli_REST;
using Sikuli4Net.sikuli_UTIL;
using SikuliModule;
//using Sikuli4Net.sikuli_REST;
//using Sikuli4Net.sikuli_REST;
//using Sikuli4Net;
//using Sikuli4Net.sikuli_UTIL;
using SikuliSharp;
using System;

namespace AutomationFramework.Keywords
{
    class SikuliKeywords: Base

    {
        
        private static ExtentTestManager Report = new ExtentTestManager();
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static float FOREVER { get; private set; }
        /// <summary>
        /// Keyword use to click on the Image
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="locatorName"></param>
        public static void Click(string pageName, string locatorName)
        {

            
                string element = ElementLocator.GetElementforSikkli(pageName, locatorName);
            if(element!=null)
            {
                try
                {
                    using (var session = Sikuli.CreateSession())
                    {
                        var clickme = Patterns.FromFile(element, 0.9f);
                        session.Click(clickme);
                        session.Dispose();
                    }

                    //System.Console.WriteLine("launch started found...");
                    Report.Pass("Clicked On " + locatorName);
                    log.Info("Clicked On " + locatorName);
                }
                catch(FileNotFoundException fe)
                {
                    Report.Fail(fe.Message);
                    log.Error(TestContext.CurrentContext.Test.MethodName + " " + locatorName + " not found");

                }

            }
            else
            {
                Report.Fail(locatorName + " not found");
                log.Error(TestContext.CurrentContext.Test.MethodName + " " + locatorName + " not found");
            }
            


        }
        /// <summary>
        /// Key is use to virify the Visibility of an Image
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="locatorName"></param>
        /// <returns></returns>

        public static Boolean ImageVisible(string pageName, string locatorName)
        {

            Boolean status=false;
            string element = ElementLocator.GetElementforSikkli(pageName, locatorName);

            if (element != null)
            {

                using (var session = Sikuli.CreateSession())
                {
                    var clickme = Patterns.FromFile(element, 0.9f);
                   status= session.Exists(clickme, 60);
                    if (status==true)
                    {
                        log.Info(locatorName + " is present ");
                        session.Hover(clickme);
                    }
                    else
                    {
                        log.Info(locatorName + "  not found");
                        status = false;
                    }

                    session.Dispose();
                }

                
            }
            else
            {
                Report.Fail("Image of  " + locatorName + " is not Found");
                log.Error(TestContext.CurrentContext.Test.MethodName + " " + locatorName + " not found");
                status = false;
            }

            return status;
        }
        
    }
}
