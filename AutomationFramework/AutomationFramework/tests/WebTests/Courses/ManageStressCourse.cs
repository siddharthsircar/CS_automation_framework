using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages.Courses;
using AutomationFramework.Pages.WebPages.ProgressCheckin;
using AutomationFramework.Tests.WebTests.ProgressCheckin;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests.WebTests.Courses
{
    [TestFixture]
    [Order(14)]
    public class ManageStressCourse:Base
    {

        Common cmn = new Common();
        [Test, Order(1)]
        [Category("AllClientReg")]
        //[Category("BuildSanity")]
        [Category("Regression")]
        //[Category("ProdSanity")]
        public void TC_FillProgressCheckin()
        {
            //To call the Page Login Method
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            // points = cmn.GetPoints();
            Common coursemenu = new Common();
            coursemenu.ClickCourseMenu();
            //Page_WeightManagementCourse pcourse = new Page_WeightManagementCourse();
            Page_ManageStressCourse pcourse = new Page_ManageStressCourse();
            pcourse.NavigateToCourse();
            CommonCourses cmnc = new CommonCourses();
            cmnc.GoToProgressCheckin();
            Page_ManagingStressProgressCheckIn pc = new Page_ManagingStressProgressCheckIn(softassertions);
            pc.FillProgresscheckinFromCourse();
            is_soft_assert = true;
            softassertions.AssertAll();
            ////List<string[]> result = pc.VerifyProgressCheckIn();
            //is_soft_assert = false;
            //Assert.Multiple(() =>
            //{
            //    for (int i = 0; i < result.Count; i++)
            //    {
            //        bool textmatchresult = Convert.ToBoolean(result.ElementAt(i)[1]);
            //        string msg = result.ElementAt(i)[0];
            //        Assert.IsTrue(textmatchresult, msg);
            //    }
            //}
            //);
            //cmn.LogOut();
        }
        [Test, Order(2)]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_CompleteManagingStressCourse()
        {
            //To call the Page Login Method
            //Page_Login plogin = new Page_Login();
            //plogin.Login();
            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //haprompt.GoToDashboard();
            Common coursemenu = new Common();
            coursemenu.ClickCourseMenu();
            Page_ManageStressCourse pcourse = new Page_ManageStressCourse(softassertions);
            is_soft_assert = true;
            pcourse.CompleteManagingStressCourse();
            softassertions.AssertAll();
            cmn.LogOut();

        }
    }
}
