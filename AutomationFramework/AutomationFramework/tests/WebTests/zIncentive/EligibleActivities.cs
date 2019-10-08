using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages;
using AutomationFramework.Pages.WebPages.Incentive;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests.WebTests.zIncentive
{
    [TestFixture]
    [Order(501)]
    public class EligibleActivities : Base
    {
        /// <summary>
        /// Test Case: To verify Incentive History page
        /// </summary>
        [Test, Order(1)]
        //[Category("Regression")]
        //[Category("ProdSanity")]
        //[Category("PointReg")]
        public void TC_ValidateIncentiveEligibleActivities()
        {
            Page_EligibleActivities peligible = new Page_EligibleActivities(softassertions);
            is_soft_assert = true;

            CommonApi cma = new CommonApi();
            String token = cma.GetToken();
            peligible.InitializeEligibleActivitiesRequest();

            peligible.SetHeader(token);
            peligible.SetMethod();
            peligible.SendRequest();
            peligible.VerifyEligibleActivities();
            softassertions.AssertAll();

        }
    }
}
