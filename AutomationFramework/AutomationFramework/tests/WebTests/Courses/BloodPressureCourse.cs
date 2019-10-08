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
    [Order(15)]
    public class BloodPressureCourse:Base
    {
        [Test]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_CompleteBloodPressureCourse()
        {
            //To call the Page Login Method
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            Common coursemenu = new Common();
            coursemenu.ClickCourseMenu();
            Page_BloodPressureCourse pcourse = new Page_BloodPressureCourse(softassertions);
            is_soft_assert = true;
            pcourse.CompleteBloodPressureCourse();
            softassertions.AssertAll();
        }
    }
}
