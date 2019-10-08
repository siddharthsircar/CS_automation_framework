using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.Courses
{
    class CommonCourses
    {
        String pageName;
        SoftAssertions softassertions = null;
        public CommonCourses()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        public CommonCourses(SoftAssertions softassertions) : this()
        {
            this.softassertions = softassertions;
        }
        public void GoToProgressCheckin()
        {
            SeleniumKeywords.Click(pageName, "lesson1link");
            SeleniumKeywords.Click(pageName, "startlesson_btn");
            System.Threading.Thread.Sleep(3000);
            SeleniumKeywords.Click(pageName, "clickherelink");
        }
        public void ClickNextButton()
        {
            Thread.Sleep(4000);
            SeleniumKeywords.Click(pageName, "nextlessonbtn");
        }
        public void ClickStartLessonButton()
        {
            Thread.Sleep(4000);
            SeleniumKeywords.Click(pageName, "startlessonbtn");
        }
        public void ClickFinishButton()
        {
            Thread.Sleep(4000); ;
            SeleniumKeywords.Click(pageName, "finishlessonbtn");
        }
        public void ConfirmCourse()
        {
            
            bool elementpresent = SeleniumKeywords.IsElementPresent(pageName, "course_confirm_popup_text");
            softassertions.Add("Element : course_confirm_popup_text", true, elementpresent, "equals");

            elementpresent = SeleniumKeywords.IsElementPresent(pageName, "course_confirm_nothankyou_btn");
            softassertions.Add("Element : course_confirm_nothankyou_btn", true, elementpresent, "equals");
            
            SeleniumKeywords.Click(pageName, "course_confirm_yesiamsure_btn");
            Thread.Sleep(25000);

        }

    }
}
