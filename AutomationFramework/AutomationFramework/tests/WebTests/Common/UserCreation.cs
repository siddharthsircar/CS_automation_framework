using AutomationFramework.Framework;
using AutomationFramework.Pages;
using NUnit.Framework;
using System;

namespace AutomationFramework.tests

{
    /// <summary>
    /// User Creation test class
    /// </summary>
    [TestFixture]
    [Order(1)]
    public class UserCreation : Base
    {
        /// <summary>
        /// Test Case: To create new user
        /// </summary>
        [Test]
        [Category("BuildSanity")]
        [Category("Createuser")]
        //[Category("ProdSanity")]
        [Category("Regression")]
        //[Category("AllClientReg")]
        public void TC_CreateUser()
        {
            ExtentTestManager Report = new ExtentTestManager();
            CreateNewUser cn = new CreateNewUser();
            //Boolean userstatus = cn.getNewUser();
            //Console.WriteLine("status  " + userstatus);
            try
            {
                Assert.IsTrue(cn.getNewUser());
            }
            catch (Exception e)
            {
                Report.Fail("User not created");
                OneTimeTearDown();
                Environment.Exit(0);
            }

        }
    }
}
