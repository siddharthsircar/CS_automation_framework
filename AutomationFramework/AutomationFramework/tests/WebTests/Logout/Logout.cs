using System;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using AutomationFramework.Framework;
using AutomationFramework.Pages;

namespace AutomationFramework.Tests.WebTests.logout
{
    /// <summary>
    /// Test Class
    /// </summary>
    [TestFixture]    
    [Order(49)]
    public class Logout : Base
    {
        /// <summary>
        /// Test Case to verify successful logout
        /// </summary>
        [Test]
        [Order(1)]
        [Category("BuildSanity")]
        //[Category("ProdSanity")]
        [Category("Regression")]
        [Category("JourClientReg")]
        public void TC_VerifyLogout()
        {
            //To call the Page Login Method
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            Page_Dashboard Dashboard = new Page_Dashboard();
            Assert.IsTrue(Dashboard.AtDashboard());
            Common logout = new Common();
            logout.LogOut();
        }
    }
}