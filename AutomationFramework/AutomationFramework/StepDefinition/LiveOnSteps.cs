using AutomationFramework.Framework;
using AutomationFramework.Pages;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace AutomationFramework.StepDefinition
{
    [Binding]
    public class LiveOnSteps
    {
        [Given(@"I have logged into the application")]
        public void GivenIHaveLoggedIntoTheApplication()
        {
            Page_Login plogin = new Page_Login();
            plogin.Login();
        }
        
        [When(@"I am on the dashboard page")]
        public void WhenIAmOnTheDashboardPage()
        {
            Page_HAPrompt haprompt = new Page_HAPrompt();
            Assert.IsTrue(haprompt.AtHaPrompt(), "Not at HA Prompt Page");
            haprompt.GoToDashboard();
        }
        
        [Then(@"I press logout button")]
        public void ThenIPressLogoutButton()
        {
            Common cmn = new Common();
            cmn.LogOut();
        }
    }
}
