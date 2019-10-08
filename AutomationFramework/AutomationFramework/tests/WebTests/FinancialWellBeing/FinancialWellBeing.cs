
using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages.FinancialWellBeing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests.WebTests.FinancialWellBeing
{
    [TestFixture]
    [Order(37)]
    public class FinancialWellBeing:Base
    {
        [Test,Order(1)]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyFinancialWellBeing()
        {
            Common cm = new Common();
            List<string[]> fwbenabled = cm.GetConfig("FinancialWellBeing");
            if(fwbenabled.ElementAt(0)[1].ToLower().Equals("false"))
            {
                Assert.Ignore("Feature is not available for client");
            }
            else
            {
                Page_Login plogin = new Page_Login();
                plogin.Login();
                Page_HAPrompt haprompt = new Page_HAPrompt();
                haprompt.GoToDashboard();
                Common cmn = new Common();
                cmn.ClickFinancialWellBeingMenu();
                Page_FinancialWellBeing fwb = new Page_FinancialWellBeing(softassertions);
                fwb.VerifyFinancialWellBeingData(GlobalVariables.clientname);
                is_soft_assert = true;
                softassertions.AssertAll();
                Common logout = new Common();
                logout.LogOut();
            }
            
        }
    }
}
