using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Framework
{
    class ExtentTestManager
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static ExtentTest parentTest;
        private static ExtentTest childTest;
        
        /// <summary>
        /// Method creates a Parent node in the Extent Report
        /// </summary>
        /// <param name="testName"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static ExtentTest CreateParentTest(string testName, string description = null)
        {
            parentTest = ExtentManager.Instance.CreateTest(testName, description);
            return parentTest;
        }
        
        /// <summary>
        /// Method appends a child test node to the parent node in the extent report
        /// </summary>
        /// <param name="testName"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static ExtentTest CreateTest(string testName, string description = null)
        {
            childTest = parentTest.CreateNode(testName, description);
            return childTest;
        }

        /// <summary>
        /// Method returns the test node instance that is utilized in the tests for logging
        /// </summary>
        /// <returns></returns>
        public static ExtentTest GetTest()
        {
            return childTest;
        }

        public void Info(string message)
        {
            GetTest().Info(message);
        }
        public void Pass(string message)
        {
            GetTest().Pass(message);
        }
        public void Fail(string message)
        {
            GetTest().Fail(message);
        }
        public void Warning(string message)
        {
            GetTest().Warning(message);
            
        }

        public void Skip(string message)
        {
            GetTest().Skip(message);
            
        }

    }
}
