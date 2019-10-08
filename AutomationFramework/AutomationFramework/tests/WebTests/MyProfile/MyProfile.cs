using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages;
using AutomationFramework.Pages.WebPages.MyProfile;
//using AutomationFramework.Pages.WebPages;
using AutomationFramework.tests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests.WebTests.MyProfile
{
    [TestFixture]
    [Order(42)]
    public class MyProfile: Base
    {
        String pageName;
        public MyProfile()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        [Test]
        [Order(1)]
        [Category("Regression")]
        public void  TC_VerifyConnectionName()
        {
            string expected = ConfigurationManager.AppSettings["firstname"] + " " + ConfigurationManager.AppSettings["lastname"]+"!";
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            Page_MyProfile mp = new Page_MyProfile();
            String actual=mp.VerifyMemberName();
            Console.WriteLine("Expected user name is " + expected + " Actual is " + actual);
            Assert.AreEqual(expected, actual.Trim());

        }
        [Test]
        [Order(2)]
        [Category("Regression")]
        public void TC_ClearStatusMessage()
        {
            if ((GlobalVariables.clientname == "Health Trust") || (GlobalVariables.clientname == "Onlife Health") || (GlobalVariables.clientname == "ARC") || (GlobalVariables.clientname == "Group44") || (GlobalVariables.clientname == "Spoucse"))
            {

               // string expected = "What's on your mind, " + ConfigurationManager.AppSettings["firstname"] + " " + ConfigurationManager.AppSettings["lastname"] + "?";
                //Page_Login plogin = new Page_Login();
                //plogin.Login();
                //Page_HAPrompt haprompt = new Page_HAPrompt();
                //haprompt.GoToDashboard();
                string expected= "Characters: 12/70";

                Page_MyProfile mp = new Page_MyProfile(softassertions);
                String actual = mp.VerifyClearStatusMessage();
                is_soft_assert = true;
                softassertions.AssertAll();
                //Assert.AreEqual(expected, actual);
            }
            else
            {

                Assert.Ignore("The current testcase is not available for the client" + GlobalVariables.clientname);
            }
        }
        [Test]
        [Order(3)]
        [Category("Regression")]
        public void TC_UpdagteStatusMessage()
        {
            //Page_Login plogin = new Page_Login();
            //plogin.Login();
            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();
            if ((GlobalVariables.clientname == "Health Trust") || (GlobalVariables.clientname == "Onlife Health") || (GlobalVariables.clientname == "ARC") || (GlobalVariables.clientname == "Group44") || (GlobalVariables.clientname == "Spoucse"))
            {
                Page_MyProfile mp = new Page_MyProfile();
                Assert.AreEqual("I am Rocking", mp.UpdateStatus());
            }
            else
            {

                Assert.Ignore("The current testcase is not available for the client" + GlobalVariables.clientname);
            }
        }
    }
}
