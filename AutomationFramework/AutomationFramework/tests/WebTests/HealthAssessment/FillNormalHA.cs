using AutomationFramework.Framework;
using AutomationFramework.Pages;
using NUnit.Framework;

namespace AutomationFramework.Tests.WebTests.HealthAssessment
{
    [TestFixture]
    [Order(10)]
    public class FillNormalHA : Base
    {
        Page_FillHA ha = null;

        [Test,Order(1)]
        [Category("BuildSanity")]
        //[Category("Regression")]
        public void TC_FillNormalHA()
        {
            Page_Login plogin = new Page_Login();
            plogin.Login();

            Page_HAPrompt haprompt = new Page_HAPrompt();
            Assert.IsTrue(haprompt.AtHaPrompt(), "Not at HA Prompt Page");
            haprompt.GoToDashboard();

            ha  = new Page_FillHA(softassertions);
            ha.setInputFileName("NormalHAData");

            is_soft_assert = true;
            ha.FillHA();
            softassertions.AssertAll();
        }

        [Test, Order(2)]
        [Category("BuildSanity")]
        //[Category("Regression")]
        public void TC_VerifyHAReport()
        {
            ha.ValidateHAReport();
            Common logout = new Common();
            logout.LogOut();
            softassertions.AssertAll();
        }
    }
}
