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
    public class QuitTobaccoCourse:Base
    {
        Common cmn = new Common();
        [Test]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_TakeQuitTobaccoCourse()
        {
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            Common coursemenu = new Common();
            coursemenu.ClickCourseMenu();
            Page_QuitTobaccoCourse pcourse = new Page_QuitTobaccoCourse(softassertions);
            is_soft_assert = true;
            pcourse.CompleteQuitTobaccoCourse();
            softassertions.AssertAll();

        }
    }
}
