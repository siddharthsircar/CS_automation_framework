using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages.Footer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests.WebTests
{
    /// <summary>
    /// Test Class
    /// </summary>
    [TestFixture]
    //[Ignore("Test Case Disabled due to bug DEF-2617")]
    [Order(48)]
    // [Parallelizable(ParallelScope.Fixtures)]
    public class Footer : Base
    {
        
        /// <summary>
        /// Verifies Dashboard link successfully navigates to Dashboard page
        /// </summary>
        [Test]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        [Order(1)]
        public void TC_VerifyDashboardLink()
        {
            //To call the Page Login Method
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            Common dblink = new Common();
            dblink.ClickFooterDashboardLink();
            haprompt.GoToDashboard();
            Page_Dashboard dashbrd = new Page_Dashboard();
            Assert.IsTrue(dashbrd.JourneyBannerDisplayed(), "Journey Banner missing");
            //Assert.Ignore("Test Case Disabled due to bug DEF - 2617");
            
        }


        /// <summary>
        /// Verifies tracker link navigates to tracker learn more page
        /// Verify tracker tiles
        /// </summary>
        [Test]
        //[Ignore("Test Case Disabled due to bug DEF-2617")]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        [Order(2)]
        public void TC_VerifyTrackerLink()
        {
            //To call the Page Login Method
            //Page_Login plogin = new Page_Login();
            //plogin.Login();
            Common trackerlink = new Common();
            trackerlink.ClickFooterTrackerLink();
            Page_TrackerLearnMore learnmore = new Page_TrackerLearnMore();
            List<string[]> result = learnmore.LearnMorePage();
            Assert.Multiple(() =>
            {
                for (int i = 0; i < result.Count; i++)
                {
                    Assert.IsTrue(Convert.ToBoolean(result.ElementAt(i)[1]), result.ElementAt(i)[0]);
                }
            }
            );
            //Assert.Ignore("Test Case Disabled due to bug DEF-2617");

            //Report.Skip("Test Case Disabled due to bug DEF-2617");


        }
        /// <summary>
        /// Verifies My Health link
        /// </summary>
        [Test]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        [Order(3)]
        public void TC_VerifyMyHealthLink()
        {
            //To call the Page Login Method
            //Page_Login plogin = new Page_Login();
            //plogin.Login();
            Common healthlink = new Common();
            healthlink.ClickFooterHealthLink();
            Page_MyHealth myhealth = new Page_MyHealth();
            List<string[]> result = myhealth.MyHealthPage();
            Assert.Multiple(() =>
            {
                for (int i = 0; i < result.Count; i++)
                {
                    Assert.IsTrue(Convert.ToBoolean(result.ElementAt(i)[1]), result.ElementAt(i)[0]);
                }
            }
            );
            //Assert.Ignore("Test Case Disabled due to bug DEF - 2617");
            
        }
        /// <summary>
        /// Verifies HIPAA link
        /// </summary>
        [Test]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        [Order(4)]
        public void TC_VerifyHIPAALink()

        {

            // To call the Page Login Method
            //Page_Login plogin = new Page_Login();
            //plogin.Login();
            if ((GlobalVariables.clientname.ToLower().Equals("arc")) || (GlobalVariables.clientname.ToLower().Equals("self directed")))
                {
                Assert.Ignore("HIPPA is not available for " + GlobalVariables.clientname);

            }
            else
            {
                
                Common hipaa = new Common();
                hipaa.ClickFooterHipaaLink();
                Page_HIPAA rights = new Page_HIPAA();
                List<string[]> result = rights.VerifyHeader();
                Assert.Multiple(() =>
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        Assert.IsTrue(Convert.ToBoolean(result.ElementAt(i)[1]), result.ElementAt(i)[0]);
                    }
                }
                );
                Assert.IsTrue(rights.VerifyContentSectionPresent(), "HIPAA Rights content not found");
                //Assert.Ignore("Test Case Disabled due to bug DEF - 2617");
            }
            
        }
        /// <summary>
        /// Verifies Privacy Policy link
        /// </summary>
        [Test]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        [Order(5)]
        public void TC_VerifyPrivacyPolLink()
        {
            //To call the Page Login Method
            //Page_Login plogin = new Page_Login();
            //plogin.Login();
            Common privacypol = new Common();
            privacypol.ClickFooterPolicyLink();
            Page_PrivacyPolicy policy = new Page_PrivacyPolicy();
            Assert.IsTrue(policy.VerifyContentSectionPresent(), "Privacy Policy Content not found");
            List<string[]> result = policy.VerifyHeaders();
            Assert.Multiple(() =>
            {
                for (int i = 0; i < result.Count; i++)
                {
                    Assert.IsTrue(Convert.ToBoolean(result.ElementAt(i)[1]), result.ElementAt(i)[0]);
                }
            }
            );
            //Assert.Ignore("Test Case Disabled due to bug DEF - 2617");
            
        }
        /// <summary>
        /// Verifies TOS Link
        /// </summary>
        [Test]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        [Order(6)]
        public void TC_VerifyTOSLink()
        {
            //To call the Page Login Method
            //Page_Login plogin = new Page_Login();
            //plogin.Login();
            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();
            Common toslink = new Common();
            toslink.ClickFooterTOSLink();

            Page_TermsOfService tos = new Page_TermsOfService();
            List<string[]> result = tos.VerifyTosPage();
            Assert.Multiple(() =>
            {
                for (int i = 0; i < result.Count; i++)
                {
                    Assert.IsTrue(Convert.ToBoolean(result.ElementAt(i)[1]), result.ElementAt(i)[0]);
                }
            }
            );
            //Assert.Ignore("Test Case Disabled due to bug DEF - 2617");
            
        }

        /// <summary>
        /// Test Case: Verifies certificate page from the dashboard
        /// </summary>
        [Test]
        //[Category("BuildSanity")]
        //[Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]

        [Order(7)]
        public void TC_VerifyCertificateLnk()
        {
            Common cmn = new Common();
            if (cmn.IsCertificateEnable(GlobalVariables.clientname) == false)
            {
                Assert.Ignore("Feature not available for client: " + GlobalVariables.clientname);
            }
            else
            {
                //Page_Login plogin = new Page_Login();
                //plogin.Login();

                //Page_HAPrompt haprompt = new Page_HAPrompt();
                //haprompt.GoToDashboard();
                Page_Certificates pce = new Page_Certificates(softassertions);
                pce.verifyCertificatePage();
                is_soft_assert = true;
                softassertions.AssertAll();
            }
        }

        /// <summary>
        /// Verifies Feedback Link: opens feedback page
        /// completes feedback
        /// </summary>
        [Test]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("AllClientReg")]
        [Category("Regression")]
        [Order(8)]
        public void TC_VerifyFeedbackLink()
        {
            //To call the Page Login Method
            //Page_Login plogin = new Page_Login();
            //plogin.Login();
            Common feedbacklink = new Common();
            feedbacklink.ClickFooterFeedbackLink();
            Page_Feedback feedback = new Page_Feedback();
            List<string[]> result = feedback.VerifyFeedBackForm();
            Assert.Multiple(() =>
            {
                for (int i = 0; i < result.Count; i++)
                {
                    Assert.IsTrue(Convert.ToBoolean(result.ElementAt(i)[1]), result.ElementAt(i)[0]);
                }
            }
            );

            //Assert.Ignore("Test Case Disabled due to bug DEF - 2617");
          }

    }
}
