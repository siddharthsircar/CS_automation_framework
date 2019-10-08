using NUnit.Framework;
using AutomationFramework.Framework;
using AutomationFramework.Pages;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Configuration;
using SharpAvi;

namespace AutomationFramework.Tests.WebTests.wLogin
{
    /// <summary>
    /// Test Class
    /// </summary>
    [TestFixture]
    //[Parallelizable(ParallelScope.Fixtures)]
    //Temporary fix for execution order
    // To Do: Pass execution order from external file
    [Order(3)]
    public class Login : Base
    {
        /// <summary>
        /// Test Case: Verify UI element of Login section
        /// </summary>
        [Test]
        //[Category("BuildSanity")]
        //[Category("ProdSanity")]
        //[Category("Regression")]
        [Order(1)]
        public void TC_ValidateLoginUIElements()
        {
            Page_Login plogin = new Page_Login();
            //Call verify login method 
            List<string[]> result = plogin.VerifyLoginUIElements();
            //applied soft assertion to check all ui element in a test case
            Assert.Multiple(() =>
            {
                for (int i = 0; i < result.Count; i++)
                {
                    bool textmatchresult = Convert.ToBoolean(result.ElementAt(i)[1]);
                    string msg = result.ElementAt(i)[0];
                    Assert.IsTrue(textmatchresult, msg);
                }

            }
            );

        }

        /// <summary>
        /// Test Case: Verify Error Message for Invalid Username and Password  
        /// </summary>
        [Test]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Order(2)]
        public void TC_InvalidLogin()
        {
            //To call the Invalid Username Password Message Method
            Page_Login plogin = new Page_Login();
            string errmsg = plogin.InvaidLogin();
            Console.WriteLine("error messsage : " + errmsg);
            Assert.AreEqual(errmsg, "Invalid username or password!");
        }
        [Test]
        public void execommand()
        {
            SQLConnect sq = new SQLConnect();
            sq.OpenConnection();
            sq.ExecuteStoreProcedure();
        }

       
        /// <summary>
        /// Test Case: Verifies successful Login
        /// </summary>
        [Test]
       // [Category("BuildSanity")]
        //[Category("ProdSanity")]
        //[Category("Regression")]
        [Order(3)]
        public void TC_ForgotUserName()
        {
            //To call the Page Login Method
            Page_Login plogin = new Page_Login();
            string username=plogin.forgotUserName();
            Assert.AreEqual(GlobalVariables.username,username);
            Page_HAPrompt haprompt = new Page_HAPrompt();
            //Assert.IsTrue(haprompt.AtHaPrompt(), "Not at HA Prompt Page");
            haprompt.GoToDashboard();

            //To call the Page Login Method
            Page_Dashboard Dashboard = new Page_Dashboard();
            Assert.IsTrue(Dashboard.AtDashboard());
        }

        [Test]
        // [Category("BuildSanity")]
        //[Category("ProdSanity")]
        //[Category("Regression")]
        [Order(3)]
        public void TC_ForgotPasswordName()
        {
            //To call the Page Login Method
            Page_Login plogin = new Page_Login(softassertions);
            plogin.forgotPassword();
            is_soft_assert = true;
            softassertions.AssertAll();
            
        }
    }
}
