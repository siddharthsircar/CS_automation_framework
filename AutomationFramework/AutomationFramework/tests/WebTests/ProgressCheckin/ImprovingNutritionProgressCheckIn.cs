using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages.ProgressCheckin;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests.WebTests.ProgressCheckin
{
    [TestFixture]
    [Order(32)]
    public class ImprovingNutritionProgressCheckIn:Base
    {
        Common cmn = new Common();

        [Test, Order(1)]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyImprovingNutritionProgressCheckIn()
        {
            //To call the Page Login Method
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            cmn.ClickProgressCheckinMenu();
            Page_ImprovingNutritionProgressCheckIn pin = new Page_ImprovingNutritionProgressCheckIn(softassertions);
            is_soft_assert = true;
            pin.CompleteProgressCheckIn();
            //pin.VerifyProgressCheckinReportBottomLinks(GlobalVariables.clientname);
            softassertions.AssertAll();
            
        }
        //[Test, Order(2)]
        //[Category("Regression")]
        //[Category("AllClientReg")]
        //public void TC_ValidateProgressCheckInReport()
        //{
        //    Page_ImprovingNutritionProgressCheckIn pin = new Page_ImprovingNutritionProgressCheckIn(softassertions);
        //    is_soft_assert = true;
        //    pin.VerifyReport();
        //    softassertions.AssertAll();
        //    //cmn.LogOut();
     //}        
    [Test, Order(2)]
    [Category("Regression")]
    [Category("AllClientReg")]
    public void TC_ValidateProgressCheckInReportBottomLink()
        {
            
            Page_ImprovingNutritionProgressCheckIn pprogress = new Page_ImprovingNutritionProgressCheckIn(softassertions);
            is_soft_assert = true;
            pprogress.VerifyProgressCheckinReportBottomLinks(GlobalVariables.clientname);
            softassertions.AssertAll();
            cmn.ClickFooterDashboardLink();
            cmn.LogOut();
        }

    }
}
