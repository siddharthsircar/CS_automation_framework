using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationFramework.Tests.WebTests
{
    /// <summary>
    /// Resources Test class
    /// </summary>
    [TestFixture]
    public class Resources : Base
    {
        String pageName;
        public Resources()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        /// <summary>
        /// Test Case: To verify Health Content page and all tab headers
        /// </summary>
        [Test]
        [Category("Regression")]
        [Category("AllClientReg")]
        [Order(1)]
        public void TC_VerifyHealthContentPage()
        {
            //To call the Page Login Method
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            //Assert.IsTrue(haprompt.AtHaPrompt(), "Not at HA Prompt Page");
            haprompt.GoToDashboard();

            // Health Content
            Common cmn = new Common();
            cmn.ClickOnResources();
            Page_HealthContent phc = new Page_HealthContent();
            List<string[]> actualtxt = CSVReaderDataTable.GetCSVData("ResourcesContent", "HealthContent", "pageheading");
            string atrval = phc.VerifyHealthContentPage();
            Console.WriteLine("actualtxt" + atrval + "expected" + actualtxt.ElementAt(0)[4]);
            Assert.AreEqual(actualtxt.ElementAt(0)[4], atrval);

            // Symptom Checker Tab
            Page_SymptomChecker psc = new Page_SymptomChecker();
            atrval = psc.VerifySymptomCheckerPageFromTab();
            actualtxt = CSVReaderDataTable.GetCSVData("ResourcesContent", "SymptomChecker", "pageheading");
            Console.WriteLine("actualtxt" + atrval + "expected" + actualtxt.ElementAt(0)[4]);
            Assert.AreEqual(actualtxt.ElementAt(0)[4], atrval);

            // Well Being Content Tab
            Page_WellBeingContent wbc = new Page_WellBeingContent();
            atrval = wbc.VerifyWellBeingFromTab();
            actualtxt = CSVReaderDataTable.GetCSVData("ResourcesContent", "Common", "pageheading");
            Console.WriteLine("actualtxt" + atrval + "expected" + actualtxt.ElementAt(0)[4]);
            Assert.AreEqual(actualtxt.ElementAt(0)[4], atrval);

            //Family Content
            Page_FamilyContent fc = new Page_FamilyContent();
            atrval = fc.VerifyFamilyContentFromTab();
            actualtxt = CSVReaderDataTable.GetCSVData("ResourcesContent", "Common", "pageheading");
            TestContext.WriteLine("expected" + actualtxt.ElementAt(0)[4]);
            TestContext.WriteLine("actualtxt" + atrval + "expected" + actualtxt.ElementAt(0)[4]);
            Assert.AreEqual(actualtxt.ElementAt(0)[4], atrval);
        }

        /// <summary>
        /// Test Case: To verify Symptom Checker page
        /// </summary>
        [Test, Order(2)]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifySymptomCheckerPage()
        {
            ////To call the Page Login Method
            //Page_Login plogin = new Page_Login();
            //plogin.Login();
            //Page_HAPrompt haprompt = new Page_HAPrompt();
            ////Assert.IsTrue(haprompt.AtHaPrompt(), "Not at HA Prompt Page");
            //haprompt.GoToDashboard();

            Common cmn = new Common();
            cmn.ClickOnResources();
            Page_SymptomChecker phc = new Page_SymptomChecker();

            List<string[]> actualtxt = CSVReaderDataTable.GetCSVData("ResourcesContent", "SymptomChecker", "pageheading");

            string atrval = phc.VerifySymptomCheckerPage();
            Console.WriteLine("actualtxt" + atrval + "expected" + actualtxt.ElementAt(0)[4]);
            Assert.AreEqual(actualtxt.ElementAt(0)[4], atrval);
        }

        /// <summary>
        /// Test Case: To verify Well Being Content
        /// </summary>
        [Test, Order(3)]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyWellBeingContentPage()
        {
            ////To call the Page Login Method
            //Page_Login plogin = new Page_Login();
            //plogin.Login();
            //Page_HAPrompt haprompt = new Page_HAPrompt();
            ////Assert.IsTrue(haprompt.AtHaPrompt(), "Not at HA Prompt Page");
            //haprompt.GoToDashboard();

            Common cmn = new Common();
            cmn.ClickOnResources();
            Page_WellBeingContent phc = new Page_WellBeingContent();
            List<string[]> actualtxt = CSVReaderDataTable.GetCSVData("ResourcesContent", "Common", "pageheading");
            string atrval = phc.VerifyWellBeingContentPage();
            Console.WriteLine("actualtxt" + atrval + "expected" + actualtxt.ElementAt(0)[4]);
            Assert.AreEqual(actualtxt.ElementAt(0)[4], atrval);
        }

        /// <summary>
        /// Test Case: To verify Family content page
        /// </summary>
        [Test, Order(4)]
        [Category("AllClientReg")]
        [Category("Regression")]
        public void TC_VerifyFamilyContentPage()
        {
            //Page_Login plogin = new Page_Login();
            //plogin.Login();

            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();

            Common cmn = new Common();
            cmn.ClickOnResources();
            Page_FamilyContent phc = new Page_FamilyContent();
            List<string[]> actualtxt = CSVReaderDataTable.GetCSVData("ResourcesContent", "Common", "pageheading");
            string atrval = phc.VerifyFamilyContentPage();
            Console.WriteLine("actualtxt" + atrval + "expected" + actualtxt.ElementAt(0)[4]);
            Assert.AreEqual(actualtxt.ElementAt(0)[4], atrval);
        }

        /// <summary>
        /// Test Case: To verify Reports Page
        /// </summary>
        [Test]
        [Order(5)]
        [Category("Regression")]
        public void TC_VerifyReportsPage()
        {
            //Page_Login lgn = new Page_Login();
            //lgn.Login();

            Common cmn = new Common();
            cmn.ClickOnResources();
            Page_Reports phc = new Page_Reports();
            List<string[]> actualtxt = CSVReaderDataTable.GetCSVData("ResourcesContent", "Reports", "pageheader");
            string atrval = phc.VerifyReportsPageHeader();
            Console.WriteLine("actualtxt" + atrval + "expected" + actualtxt.ElementAt(0)[4]);
            Assert.AreEqual(actualtxt.ElementAt(0)[4], atrval);

            actualtxt = CSVReaderDataTable.GetCSVData("ResourcesContent", "Reports", "pageheading");
            atrval = phc.VerifyReportsPage();
            Console.WriteLine("actualtxt" + atrval + "expected" + actualtxt.ElementAt(0)[4]);
            Assert.AreEqual(actualtxt.ElementAt(0)[4], atrval);
        }

        /// <summary>
        /// Test Case: To verify assessments page under the resources submenu
        /// </summary>
        [Test]
        [Category("Regression")]
        [Order(6)]
        // [Category("AllClientReg")]
        public void TC_VerifyAssessmentsPage()
        {
            //Page_Login lgn = new Page_Login();
            //lgn.Login();
            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //////Assert.IsTrue(haprompt.AtHaPrompt(), "Not at HA Prompt Page");
            //haprompt.GoToDashboard();

            Common cmn = new Common();
            cmn.ClickOnResources();
            Page_Assessments phc = new Page_Assessments();
            List<string[]> actualtxt = CSVReaderDataTable.GetCSVData("ResourcesContent", "Reports", "pageheader");
            string atrval = phc.VerifyAssessmentPageHeader();
            Console.WriteLine("actualtxt" + atrval + "expected" + actualtxt.ElementAt(0)[4]);
            Assert.AreEqual(actualtxt.ElementAt(0)[4], atrval);

            actualtxt = CSVReaderDataTable.GetCSVData("ResourcesContent", "Assessments", "pageheading");
            atrval = phc.VerifyAssessmentsPage();
            Console.WriteLine("actualtxt" + atrval + "expected" + actualtxt.ElementAt(0)[4]);
            Assert.AreEqual(actualtxt.ElementAt(0)[4], atrval);
        }

        /// <summary>
        /// Test case for Verify PHR Page 
        /// </summary>
        [Test,Order(7)]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyPHRPage()
        {
            //Page_Login lgn = new Page_Login();
            //lgn.Login();

            Common cmn = new Common();
            string phrEnabled = cmn.GetConfig("PHREnabled").ElementAt(0)[1].ToLower();
            if(phrEnabled.Equals("false"))
            {
                Assert.Ignore("PHR not available for client");
            }
            cmn.ClickOnResources();

            Page_PHR phc = new Page_PHR();
            List<string[]> actualtxt = CSVReaderDataTable.GetCSVData("ResourcesContent", "PHR", "pageheading");
            string atrval = phc.VerifyPHRPageHeader();
            //Console.WriteLine("actualtxt" + atrval + "expected" + actualtxt.ElementAt(0)[4]);
            Assert.AreEqual(actualtxt.ElementAt(0)[4], atrval);

            actualtxt = CSVReaderDataTable.GetCSVData("ResourcesContent", "PHR", "divheading");
            atrval = phc.VerifyPHRPage();
            //Console.WriteLine("actualtxt" + atrval + "expected" + actualtxt.ElementAt(0)[4]);
            Assert.AreEqual(actualtxt.ElementAt(0)[4], atrval);

            
        }
        
    }
}
