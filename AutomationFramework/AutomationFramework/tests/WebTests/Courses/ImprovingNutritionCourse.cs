using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages.Courses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests.WebTests.Courses
{
    [TestFixture]
    public class ImprovingNutritionCourse:Base
    {
        Common cmn = new Common();
        [Test]

        public void TC_VerifyImprovingNutritionCourse()
        {
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            Common coursemenu = new Common();
            coursemenu.ClickCourseMenu();
            Page_ImprovingNutritionCourse incourse = new Page_ImprovingNutritionCourse(softassertions);
            is_soft_assert = true;
            incourse.CompleteImprovingNutritionCourse();
            softassertions.AssertAll();

        }
    }
}
