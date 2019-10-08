using AutomationFramework.Framework;
using NUnit.Framework;
using AutomationFramework.Pages.WebPages.HealthAssessment;
using AutomationFramework.Pages;
using System.Collections.Generic;
using System;

namespace AutomationFramework.Tests.WebTests.DBFillHA
{
    /// <summary>
    /// Test class
    /// </summary>
    [TestFixture]
    public class DBFillHA : Base
    {
        string pageName;
        public DBFillHA()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        /// <summary>
        /// Test method
        /// </summary>
        [Test]
        public void TC_VerifyFirstModuleQuestions()
        {
            //To call the Page Login Method
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            //Assert.IsTrue(haprompt.AtHaPrompt(), "Not at HA Prompt Page");
            haprompt.GoToDashboard();

            //To call the Page Login Method
            Page_Dashboard Dashboard = new Page_Dashboard();
            Assert.IsTrue(Dashboard.AtDashboard());

            Page_NewHA ha = new Page_NewHA(softassertions);
            ha.FillHA();
        }
    }
}
