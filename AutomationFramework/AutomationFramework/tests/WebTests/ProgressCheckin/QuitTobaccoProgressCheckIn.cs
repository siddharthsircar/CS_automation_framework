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
    [Order(34)]
    public class QuitTobaccoProgressCheckIn:Base
    {
        Common cmn = new Common();

        [Test, Order(1)]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyQuitTobaccoProgressCheckIn()
        {
            //To call the Page Login Method
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            cmn.ClickProgressCheckinMenu();
            Page_QuitTobaccoProgressCheckIn pqt = new Page_QuitTobaccoProgressCheckIn(softassertions);
            is_soft_assert = true;
            pqt.CompleteProgressCheckIn();
            softassertions.AssertAll();
        }
        //[Test, Order(2)]
        //[Category("Regression")]
        //[Category("AllClientReg")]
        //public void TC_ValidateProgressCheckInReport()
        //{
        //    Page_QuitTobaccoProgressCheckIn pqt = new Page_QuitTobaccoProgressCheckIn(softassertions);
        //    is_soft_assert = true;
        //    pqt.VerifyReport();
        //    softassertions.AssertAll();
        //    //cmn.LogOut();

        //}
        [Test, Order(2)]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_ValidateProgressCheckInReportBottomLink()
        {
            //Page_Login plogin = new Page_Login();
            //plogin.Login();
            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();
            //cmn.ClickProgressCheckinMenu();
            Page_QuitTobaccoProgressCheckIn pprogress = new Page_QuitTobaccoProgressCheckIn(softassertions);
            is_soft_assert = true;
            pprogress.VerifyProgressCheckinReportBottomLinks(GlobalVariables.clientname);
            softassertions.AssertAll();
            cmn.ClickFooterDashboardLink();
            cmn.LogOut();
        }


    }
}
