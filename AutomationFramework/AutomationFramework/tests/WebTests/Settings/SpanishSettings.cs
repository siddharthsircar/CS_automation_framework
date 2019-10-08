using AutomationFramework.Framework;
using AutomationFramework.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests.WebTests.Settings
{
    [TestFixture]
    // [Parallelizable(ParallelScope.Fixtures)]
    [Order(7)]
    public class SpanishSettings:Base
    {
        [Test,Order(1)]
        [Category("BuildSanity")]
        //[Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifySpanishSettings()
        {
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            Console.WriteLine("Navigating to Settings");
            Common settings = new Common();
            settings.GoToSettings();
            Page_Settings myinf = new Page_Settings(softassertions);
            myinf.ConvertToSpanish();
            is_soft_assert = true;
            softassertions.AssertAll();
        }

        [Test,Order(2)]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_ConvertToEnglish()
        {
            Page_Settings myinf = new Page_Settings();
            myinf.ConvertToEnglish();
            System.Threading.Thread.Sleep(3000);
        }
    }
}
