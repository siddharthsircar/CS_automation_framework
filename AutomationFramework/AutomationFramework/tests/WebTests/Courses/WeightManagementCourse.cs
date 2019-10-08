using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages;
using AutomationFramework.Pages.WebPages.Courses;
using AutomationFramework.Pages.WebPages.Incentive;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests.WebTests.Courses
{
    /// <summary>
    /// Test Class
    /// </summary>
    [TestFixture]
    [Order(13)]
    public class WeightManagementCourse : Base
    {
        int points;
        string clientname;
        Common cmn = new Common();
        string isenabled, pageName;

        public WeightManagementCourse()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        /// <summary>
        /// Test Case: To fill progress checkin from course
        /// </summary>
        [Test, Order(1)]
        [Category("BuildSanity")]
        [Category("Regression")]
        [Category("ProdSanity")]
        [Category("AllClientReg")]
        public void TC_FillProgressCheckin()
        {
            Common config = new Common();
            isenabled = config.GetConfig("IncentiveEnabled").ElementAt(0)[1].ToLower();
            clientname = GlobalVariables.clientname;
            //To call the Page Login Method
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            if (isenabled.Equals("true"))
            {
                points = cmn.GetPoints(clientname);
            }
            Common coursemenu = new Common();
            coursemenu.ClickCourseMenu();
            Page_WeightManagementCourse pcourse = new Page_WeightManagementCourse();
            pcourse.NavigateToCourse();
            CommonCourses cmnc = new CommonCourses();
            cmnc.GoToProgressCheckin();
            Page_WeightProgressCheckIn pc = new Page_WeightProgressCheckIn();
            List<string[]> result = pc.VerifyProgressCheckIn();
            is_soft_assert = false;
            Assert.Multiple(() =>
            {
                for (int i = 0; i < result.Count; i++)
                {
                    bool textmatchresult = Convert.ToBoolean(result.ElementAt(i)[1]);
                    string msg = result.ElementAt(i)[0];
                    Assert.IsTrue(textmatchresult, msg);
                }
            }
            );
        }

        /// <summary>
        /// Test Case: To complete course
        /// </summary>
        [Test,Order(2)]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyWeightManagementCourse()
        {
            Common coursemenu = new Common();
            coursemenu.ClickCourseMenu();
            Page_WeightManagementCourse pcourse = new Page_WeightManagementCourse(softassertions);
            is_soft_assert = true;
            pcourse.CompleteWeightManagementCourse();
            softassertions.AssertAll();
               
        }

        [Test, Order(3)]
        public void TC_ValidateCourseAndProgressCheckInIncentiveHistory()
        {
            Common config = new Common();
            isenabled = config.GetConfig("IncentiveEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Incentives not enabled for client");
            }

            string category = "I Completed The Weight Course";
            List<string[]> incentivehistorydata = CSVReaderDataTable.GetCSVData("IncentiveHistoryData", pageName, category, GlobalVariables.clientname.ToLower());

            Page_EligibleActivities peligible = new Page_EligibleActivities(softassertions);
            is_soft_assert = true;

            CommonApi cma = new CommonApi();
            String token = cma.GetToken();
            peligible.InitializeIncentiveHistoryRequest();

            peligible.SetHeader(token);
            peligible.SetMethod();
            peligible.SendRequest();

            if (incentivehistorydata.Count > 0)
            {
                peligible.VerifyHistoryData(incentivehistorydata, category);
                softassertions.AssertAll();
            }
            else
            {
                Assert.Ignore("Incentives for Course is not available for Client");
            }

            category = "I Completed The Weight Progress Check-In";
            incentivehistorydata = CSVReaderDataTable.GetCSVData("IncentiveHistoryData", pageName, category, GlobalVariables.clientname.ToLower());
            if (incentivehistorydata.Count > 0)
            {
                peligible.VerifyHistoryData(incentivehistorydata, category);
                softassertions.AssertAll();
            }
            else
            {
                Assert.Ignore("Incentives for  Progress Check-in is not available for Client");
            }




        }

        /// <summary>
        /// Test Case: to verify incentives awarded after course completion
        /// </summary>
        [Test, Order(4)]
        [Category("BuildSanity")]
        [Category("ProdSanity")]
        [Category("Regression")]
        //[Category("AllClientReg")]
        [Category("PointReg")] 
        public void TC_ValidatePoints()
        {            
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Incentives not enabled for client");
            }
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            int awardedpoints = cmn.GetPoints(clientname);
            int points_progresscheckin = Convert.ToInt32(cmn.GetInstancePointsValue(clientname, "ProgressCheckIn"));
            int points_course = Convert.ToInt32(cmn.GetInstancePointsValue(clientname, "Course"));
            int expectedtotalpoints = points + points_progresscheckin + points_course;
            is_soft_assert = false;
            Assert.AreEqual(expectedtotalpoints, awardedpoints);
            cmn.LogOut();
        }
    }
}
