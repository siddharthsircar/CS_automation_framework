using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace AutomationFramework.Framework
{
    public class SoftAssertions :Base
    {
        private static ExtentTestManager Report = new ExtentTestManager();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly List<SingleAssert> verifications = new List<SingleAssert>();

        private static Base b = new Base();
        private static Utilities util = new Utilities();

        /// <summary>
        /// Created overloaded "Add" method to add a softassertion entry
        /// takescreenshot papameter is use in api testing where screenshot is not required
        /// </summary>
        /// <param name="element"></param>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <param name="op"></param>
        public void Add(string element, string expected, string actual,string op,params string[] takescreenshot)
        {
            
            verifications.Add(new SingleAssert(element, expected, actual, op, takescreenshot));
        }

        public void Add(string element, bool expected, bool actual, string op, params string[] takescreenshot)
        {
            //Report.Info("Element Name: " + element + "Expected :" + expected + " Actual :" + actual);
           Add(element, expected.ToString(), actual.ToString(), op, takescreenshot);
        }

        public void Add(string element, int expected, int actual, string op, params string[] takescreenshot)
        {
           // Report.Info("Element Name: "+ element+ "Expected :"+ expected+ " Actual :"+ actual);
           Add(element, expected.ToString(), actual.ToString(), op, takescreenshot);
            
        }

        //public void AddTrue(string message, bool actual)
        //{
        //    verifications
        //        .Add(new SingleAssert(message, true.ToString(), actual.ToString()));
        //}

        /// <summary>
        /// AssertAll will check whether there is a failure. If yes, it will fail that particular testcase
        /// </summary>
        public void AssertAll()
        {
            var failed = verifications.Where(v => v.Failed).ToList();

            failed.Should().BeEmpty();
            
        }

        /// <summary>
        /// This class will match expectedresult with actual, if there is a failure, it will add that entry in a list and simultaneously take screenshot of available page in browser
        /// </summary>
        private class SingleAssert
        {
            private readonly string element;
            private readonly string expected;
            private readonly string actual;
            private readonly string screenshotpath;
            
            public bool Failed = false;

            public SingleAssert(string ele, string exp, string act,string op, params string[] takescreenshot)
            {
                element = ele.Trim().ToLower();
                expected = exp.Trim().ToLower();
                actual = act.Trim().ToLower();

                if(op.ToLower().Equals("equals"))
                {
                    Failed = !(actual.Equals(expected));
                }
                else if (op.ToLower().Equals("contains"))
                {
                    Failed = !(actual.Contains(expected));
                }

                if (Failed == true)
                {
                    

                    string report_msg = element + " Expected : " + expected + " , Actual : " + actual;
                    log.Info("Failed Message : " + report_msg);
                    ExtentTestManager.GetTest().Log(Status.Fail, report_msg);
                    //take screenshot is use for api and will not take screen shot when test fail, api test pass value 'yes'
                    if (takescreenshot.Length == 0)
                    {
                        screenshotpath = util.TakeScreenshot(driver);
                        Console.WriteLine("Path : " + screenshotpath);
                        ExtentTestManager.GetTest().Log(Status.Fail, "Screenshot: \n", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotpath).Build());
                    }
                    //ExtentTestManager.GetTest().Log(Status.Fail, MarkupHelper.CreateLabel(TestContext.CurrentContext.Test.Name + " :: " + Status.Fail, ExtentColor.Red));

                }
                else if(Failed == false)
                {
                    Report.Pass("Element Name: " + element + " Expected :" + expected + " Actual :" + actual);
                }
            }

            public override string ToString()
            {
                //Report.Fail($"'{_message}' assert was expected to be '{_expected}' " +
                //    $"but was '{_actual}'");
                string msg = element + " Expected : "+expected+" , Actual : "+actual;
                return msg;
             }
        }
    }
}
