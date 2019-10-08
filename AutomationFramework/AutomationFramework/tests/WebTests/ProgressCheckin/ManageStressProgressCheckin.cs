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
    [Order(29)]
    public class ManageStressProgressCheckin:Base
    {
        Common cmn = new Common();
        string isenabled;
        //string clientname = GlobalVariables.clientname;
        int points;
        [Test,Order(1)]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyManagingStressProgressCheckin()
        {

            //To call the Page Login Method
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            isenabled = cmn.GetConfig("IncentiveEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("true"))
            {
                points = cmn.GetPoints(GlobalVariables.clientname);
            }
            cmn.ClickProgressCheckinMenu();
            Page_ManagingStressProgressCheckIn mspc = new Page_ManagingStressProgressCheckIn(softassertions);
            is_soft_assert = true;
            mspc.CompleteProgressCheckIn();

            softassertions.AssertAll();
        }
        //[Test,Order(2)]
        //[Category("BuildSanity")]
        //[Category("ProdSanity")]
        //[Category("Regression")]
        //[Category("AllClientReg")]
        //public void TC_ValidateProgressCheckInReport()
        //{
        //    Page_ManagingStressProgressCheckIn pprogress = new Page_ManagingStressProgressCheckIn(softassertions);
        //    is_soft_assert = true;
        //    pprogress.VerifyReport();

        //    softassertions.AssertAll();
        //}
        
        //[Test, Order(3)]
        //[Category("Regression")]
        //[Category("AllClientReg")]
        public void TC_ValidatePoints()
        {
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Incentives not enabled for client");
            }
            int awardedpoints = cmn.GetPoints(GlobalVariables.clientname);
            Assert.AreEqual(points, awardedpoints);
            

        }
        [Test, Order(2)]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_ValidateProgressCheckInReportButtomLink()
        {
            //Page_Login plogin = new Page_Login();
            //plogin.Login();
            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();
            //cmn.ClickProgressCheckinMenu();
            Page_ManagingStressProgressCheckIn pprogress = new Page_ManagingStressProgressCheckIn(softassertions);
            is_soft_assert = true;
            pprogress.VerifyProgressCheckinReportBottomLinks(GlobalVariables.clientname);
            softassertions.AssertAll();
            cmn.ClickFooterDashboardLink();
            cmn.LogOut();
        }
    }
}
