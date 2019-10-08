using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.Coaching
{
    class CommonGoals
    {
        String pageName;
        SoftAssertions softassertions = null;
        public CommonGoals()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public CommonGoals(SoftAssertions softassertions) : this()
        {
            this.softassertions = softassertions;
        }
        public void ClickCancelBtn()
        {
            Thread.Sleep(2000);
            SeleniumKeywords.Click(pageName, "cancelbtn");
        }
        public void ClickPopUpCancelBtn()
        {
            SeleniumKeywords.Click(pageName, "modalwindowcancelbtn");
        }
        public void ClickPopUpOkBtn()
        {
            SeleniumKeywords.Click(pageName, "modalwindowokbtn");
        }
        public void ClickStep1NextBtn()
        {
            SeleniumKeywords.Click(pageName, "step1nextbtn");
        }
        public void ClickStep2NextBtn()
        {
            SeleniumKeywords.Click(pageName, "step2nextbtn");
        }

        public void ClickConfirmBtn()
        {
            SeleniumKeywords.Click(pageName, "confirmbtn");
        }

        public void ClickRemoveBtn()
        {
            Thread.Sleep(3000);
            SeleniumKeywords.Click(pageName, "removebtn");
        }
        public void ClickStep1BackBtn()
        {
            SeleniumKeywords.Click(pageName, "step1backbtn");
        }
        public void ClickStep2BackBtn()
        {
            SeleniumKeywords.Click(pageName, "step2backbtn");
        }
        public void ClickEditBtn()
        {
            SeleniumKeywords.Click(pageName, "editbtn");
            System.Threading.Thread.Sleep(3000);
        }
        public void ClickRemoveScreenYesBtn()
        {
            SeleniumKeywords.Click(pageName, "yesbtn");
        }
        public void RemoveAction()
        {
            for(int i=0;i<3;i++)
            {
                SeleniumKeywords.Click(pageName, "removeactionbtn");
            }
        }

        public void VerifyRemovePopUp()
        {
            List<string[]> removedata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "removepopup");
            Thread.Sleep(2000);
            for (int i = 0; i < removedata.Count; i++)
            {
                string elementname = removedata.ElementAt(i)[2];
                string elementlocatorname = removedata.ElementAt(i)[3];
                string expectedtext = removedata.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);

                softassertions.Add("Element : "+ elementname , expectedtext, actualtext, "contains");
            }
        }
        public void VerifySetUpCancelScreen()
        {
            //ClickCancelBtn();
            Thread.Sleep(2000);
            List<string[]> canceldata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "goalsetupcancel");
            for(int i = 0; i < canceldata.Count; i++)
            {
                string elementname = canceldata.ElementAt(i)[2];
                string elementlocatorname = canceldata.ElementAt(i)[3];
                string expectedtext = canceldata.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);

                softassertions.Add("Element : " + elementname, expectedtext, actualtext, "contains");
            }
        }
        public void ClickModalWindowCancelbutton()
        {
              ClickCancelBtn();
              VerifySetUpCancelScreen();
              ClickPopUpCancelBtn();
        }
        public void ClickModalWindowOkButton()
        {
            ClickCancelBtn();
            VerifySetUpCancelScreen();
            ClickPopUpOkBtn();
        }
        public Boolean VerfiyModalWindowNotExist()
        {
            Thread.Sleep(2000);
            return (SeleniumKeywords.IsElementNotVisible(pageName, "modalwindowcancelbtn"));
        }
    }
}
