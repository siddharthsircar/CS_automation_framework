using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace AutomationFramework.Pages.WebPages.Journey
{
    public class Page_Journey
    {
        String pageName;
        SoftAssertions softAssertions = null;
        string jsonresponse;
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_Journey()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        public Page_Journey(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }

        /// <summary>
        /// Click any Recommendation tile button
        /// </summary>
        public void ClickJourneyBtn(string varvalue1, string varvalue2)
        {
            SeleniumKeywords.IsElementPresent(pageName, "journeytile_gobtn", varvalue1, varvalue2);
        }

        /// <summary>
        /// Verified recommendations in journey banner
        /// </summary>
        /// <param name="journeydata"></param>
        public void VerifyJourneyBanner(List<string[]> journeydata)
        {
            int no_of_tiles_in_a_page = 3;
            int index = 0;
            for (int i = 0; i < (journeydata.Count/2); i = i + 3)
            {
                if ((i % no_of_tiles_in_a_page) == 0)
                {
                    Console.WriteLine("divisible by 3");
                    for (int j = 0; j < no_of_tiles_in_a_page; j++)
                    {
                        Console.WriteLine("Here index : " + index);
                        string elementname = journeydata.ElementAt(index*2)[2];
                        //string elementlocatorname = journeydata.ElementAt(index)[3];
                        bool elementdisplayedstatus = Convert.ToBoolean(journeydata.ElementAt(index*2)[4]);
                        string varvalue1 = journeydata.ElementAt(index*2)[5];                        
                        Console.WriteLine("varvalue1 : " + varvalue1);
                        bool actualelementpresent;
                        if (elementdisplayedstatus == true)
                            actualelementpresent = SeleniumKeywords.IsElementPresent(pageName, "bannertile", varvalue1);
                        else
                            actualelementpresent = SeleniumKeywords.IsElementNotPresent(pageName, "bannertile", varvalue1);
                        softAssertions.Add(index + "   Element : " + elementname, elementdisplayedstatus, actualelementpresent, "equals");
                        index++;
                        //System.Threading.Thread.Sleep(3000);
                    }
                    if ((i + 3) < ((journeydata.Count/2) - 1))
                    {
                        SeleniumKeywords.Click(pageName, "bannernextbtn");
                        //System.Threading.Thread.Sleep(3000);
                    }

                }
                else
                {
                    Console.WriteLine("Not divisible by 3");
                    int no_of_rest_tiles = i % no_of_tiles_in_a_page;

                    for (int j = 0; j < no_of_rest_tiles; j++)
                    {
                        Console.WriteLine("There index : " + index);
                        string elementname = journeydata.ElementAt(index*2)[2];
                        string elementlocatorname = journeydata.ElementAt(index*2)[3];
                        bool elementdisplayedstatus = Convert.ToBoolean(journeydata.ElementAt(index*2)[4]);
                        string varvalue1 = journeydata.ElementAt(index*2)[5];
                        bool elementpresent = SeleniumKeywords.IsElementPresent(pageName, "bannertile", varvalue1);
                        softAssertions.Add("Element : " + elementname, elementdisplayedstatus, elementpresent, "equals");
                        index++;

                    }
                }
            }
        }

        /// <summary>
        /// Method verified the new members journey recommendations
        /// </summary>
        /// <param name="journeydata"></param>
        public void VerifyNewMemberJourney(List<string[]> journeydata)
        {
            Console.WriteLine("Total Count: "+journeydata.Count());
            int no_of_tiles_in_a_page = 6;
            int index = 0;
            for (int i = 0; i < journeydata.Count; i = i + 6)
            {
                if ((i % no_of_tiles_in_a_page) == 0)
                {
                    Console.WriteLine("divisible by 6");
                    for (int j = 0; j < no_of_tiles_in_a_page; j++)
                    {
                        Console.WriteLine("Here index : " + index);
                        string elementname = journeydata.ElementAt(index)[2];
                        string elementlocatorname = journeydata.ElementAt(index)[3];
                        bool elementdisplayedstatus = Convert.ToBoolean(journeydata.ElementAt(index)[4]);
                        string varvalue1 = journeydata.ElementAt(index)[5];
                        string varvalue2 = journeydata.ElementAt(index)[6];
                        Console.WriteLine("varvalue1 : " + varvalue1 + ",, varvalue2 : " + varvalue2);
                        bool actualelementpresent;
                        if(elementdisplayedstatus == true)
                            actualelementpresent = SeleniumKeywords.IsElementPresent(pageName, elementlocatorname, varvalue1, varvalue2);
                        else
                            actualelementpresent = SeleniumKeywords.IsElementNotPresent(pageName, elementlocatorname, varvalue1, varvalue2);
                        softAssertions.Add(index + "   Element : " + elementname, elementdisplayedstatus, actualelementpresent, "equals");
                        index++;
                        //System.Threading.Thread.Sleep(3000);
                    }
                    if ((i + no_of_tiles_in_a_page) < (journeydata.Count - 1))
                    {
                        Console.WriteLine(journeydata.Count.ToString()+","+(i+ no_of_tiles_in_a_page).ToString());
                        SeleniumKeywords.Click(pageName, "journeyslidernextbtn");
                        //System.Threading.Thread.Sleep(3000);
                    }

                }
                else
                {
                    Console.WriteLine("Not divisible by 6");
                    int no_of_rest_tiles = i % no_of_tiles_in_a_page;

                    for (int j = 0; j < no_of_rest_tiles; j++)
                    {
                        Console.WriteLine("There index : " + index);
                        string elementname = journeydata.ElementAt(index)[2];
                        string elementlocatorname = journeydata.ElementAt(index)[3];
                        bool elementdisplayedstatus = Convert.ToBoolean(journeydata.ElementAt(index)[4]);
                        string varvalue1 = journeydata.ElementAt(index)[5];
                        string varvalue2 = journeydata.ElementAt(index)[6];
                        bool elementpresent = SeleniumKeywords.IsElementPresent(pageName, elementlocatorname, varvalue1, varvalue2);
                        softAssertions.Add("Element : " + elementname, elementdisplayedstatus, elementpresent, "equals");
                        index++;
                    }
                }
            }
        }

        /// <summary>
        /// Method verifies Journey and Recommendations after completing HA
        /// </summary>
        public void VerifyJournerWithRecommendation()
        {
            List<string[]> journeydata = CSVReaderDataTable.GetCSVData("JourneyContent", "Page_FillHA", "highrisk_journeyrecommendation");
            int no_of_tiles_in_a_page = 6;
            int index = 0;
            int divisiblelength = journeydata.Count / 6;
            Console.WriteLine("Journey data length : "+journeydata.Count);
            for (int i=0; i<divisiblelength; i++)
            {
                //if((i % no_of_tiles_in_a_page) == 0)
                //{
                    Console.WriteLine("divisible by 6");
                    for (int j = 0; j < no_of_tiles_in_a_page; j++)
                    {
                        Console.WriteLine("Here index : "+index);
                        string elementname = journeydata.ElementAt(index)[2];
                        string elementlocatorname = journeydata.ElementAt(index)[3];
                        bool elementdisplayedstatus = Convert.ToBoolean(journeydata.ElementAt(index)[4]);
                        string varvalue1 = journeydata.ElementAt(index)[5];
                        string varvalue2 = journeydata.ElementAt(index)[6];
                        Console.WriteLine("varvalue1 : "+varvalue1+ ", varvalue2 : " + varvalue2);
                        bool elementpresent = false;
                        if (elementdisplayedstatus == true)
                        {
                            elementpresent = SeleniumKeywords.IsElementPresent(pageName, elementlocatorname, varvalue1, varvalue2);
                        }
                        else if (elementdisplayedstatus == false)
                        {
                            elementpresent = SeleniumKeywords.IsElementNotPresent(pageName, elementlocatorname, varvalue1, varvalue2);
                        }

                        softAssertions.Add(index+"   Element : " + elementname, elementdisplayedstatus, elementpresent, "equals");
                        index++;
                        System.Threading.Thread.Sleep(3000);
                    }
                if ((i + no_of_tiles_in_a_page) < (journeydata.Count - 1))
                {
                    SeleniumKeywords.Click(pageName, "journeyslidernextbtn");
                    System.Threading.Thread.Sleep(3000);
                }

                //}
                //else
                //{
                //    Console.WriteLine("Not divisible by 6");
                //    int no_of_rest_tiles = i % no_of_tiles_in_a_page;

                //    for (int j = 0; j < no_of_rest_tiles; j++)
                //    {
                //        Console.WriteLine("There index : " + index);
                //        string elementname = journeydata.ElementAt(index)[2];
                //        string elementlocatorname = journeydata.ElementAt(index)[3];
                //        bool elementdisplayedstatus = Convert.ToBoolean(journeydata.ElementAt(index)[4]);
                //        string varvalue1 = journeydata.ElementAt(index)[5];
                //        string varvalue2 = journeydata.ElementAt(index)[6];
                //        bool elementpresent = SeleniumKeywords.IsElementPresent(pageName, elementlocatorname, varvalue1, varvalue2);
                //        softAssertions.Add("Element : " + elementname, elementdisplayedstatus, elementpresent, "equals");
                //        index++;
                //    }
                //}

            }
            for (int i=index;i<journeydata.Count;i++)
            {
                Console.WriteLine("There index : " + index);
                string elementname = journeydata.ElementAt(index)[2];
                string elementlocatorname = journeydata.ElementAt(index)[3];
                bool elementdisplayedstatus = Convert.ToBoolean(journeydata.ElementAt(index)[4]);
                string varvalue1 = journeydata.ElementAt(index)[5];
                string varvalue2 = journeydata.ElementAt(index)[6];
                bool elementpresent = SeleniumKeywords.IsElementPresent(pageName, elementlocatorname, varvalue1, varvalue2);
                softAssertions.Add("Element : " + elementname, elementdisplayedstatus, elementpresent, "equals");
                index++;
            }
        }
        public void InitializeJourneyRequest(string tailurl)
        {
            Common cmn = new Common();
            string env = cmn.GetEnvFromUrl();
            string baseurl = "https://" + env + "services2.onlifehealth.com";
            string apiurl;
            CommonApi capi = new CommonApi();
            capi.getActorid(GlobalVariables.username);
            string actorid = GlobalVariables.actorid;
            apiurl = baseurl + "/internalapi/v1.0/member/" + actorid + tailurl;
            Console.WriteLine(apiurl);
            ApiKeywords.InitializeRequest(apiurl);
        }
        public void SetMethod()
        {
            ApiKeywords.SetMethod("GET");
        }
        public void SendRequest()
        {
            jsonresponse=ApiKeywords.SendRequest();
        }

        /// <summary>
        /// Validate new member journey tiles
        /// </summary>
        public void VerifyJourneyTiles(string filename)
        {
            int length;
            string ClientName = GlobalVariables.clientname.ToLower();
            List<string[]> journeydata = CSVReaderDataTable.GetCSVData(filename, pageName, "newmemberjourney",ClientName);
            string[] path = { "Title", "ActionText", "ShowDismiss", "MoreInfoText",  "IncentiveValue", "IncentiveSymbol" };
            List<string[]> result = ApiKeywords.GetResponse(jsonresponse, path );
            if (result.Count() != 0 && journeydata.Count() != 0)
            {
                if (journeydata.Count() < result.Count())
                    length = journeydata.Count();
                else
                    length = result.Count();
                for (int i = 0; i < length; i++)
                {
                    int expstartindex = 4;
                    string elementname = journeydata.ElementAt(i)[3];
                    for (int j = 0; j < result.ElementAt(0).Count(); j++)
                    {
                        string expectedtext = journeydata.ElementAt(i)[expstartindex];
                        string actualtext = result.ElementAt(i)[j];
                        expstartindex++;
                        Console.WriteLine("Element Name : " + elementname + "\tExpectedText : " + expectedtext + "\tActualText : " + actualtext);
                        softAssertions.Add(elementname, expectedtext, actualtext, "contains");
                        //if (journeydata.Count() < result.Count())
                        //    softAssertions.Add(elementname, "", actualtext, "not equals");
                        //else
                        //    softAssertions.Add(elementname, expectedtext, "", "not equals");
                    }
                }
            }
            else
                softAssertions.Add("Total tiles Doesn't match", journeydata.Count(), result.Count(), "equals");

        }

        /// <summary>
        /// Validate journey banner on Dashboard
        /// </summary>
        public void VerifyJourneyBanner()
        {
            int length;
            string ClientName = GlobalVariables.clientname.ToLower();
            List<string[]> journeydata = CSVReaderDataTable.GetCSVData("JourneyBannerContent",pageName, "newmemberjourney", ClientName);
            string[] path = { "Title" };
            List<string[]> result = ApiKeywords.GetResponse(jsonresponse, path);
            if (result.Count() != 0 && journeydata.Count() != 0)
            {
                if (journeydata.Count() < result.Count())
                    length = journeydata.Count();
                else
                    length = result.Count();
                for (int i = 0; i < length; i++)
                {
                    string elementname = journeydata.ElementAt(i)[3];
                    for (int j = 0; j < result.ElementAt(0).Count(); j++)
                    {
                        string expectedtext = journeydata.ElementAt(i)[4];
                        string actualtext = result.ElementAt(i)[j];
                        Console.WriteLine("Element Name : " + elementname + "\tExpectedText : " + expectedtext + "\tActualText : " + actualtext);
                        softAssertions.Add(elementname, expectedtext, actualtext, "contains");
                        //if (journeydata.Count() < result.Count())
                        //    softAssertions.Add(elementname, "", actualtext, "not equals");
                        //else
                        //    softAssertions.Add(elementname, expectedtext, "", "not equals");
                    }
                }
            }
            else
                softAssertions.Add("Total tiles Doesn't match", journeydata.Count(), result.Count(), "equals");

        }

        /// <summary>
        /// Validate journey after member stratification
        /// </summary>
        public void VerifyClinicalJourney()
        {        
            string ClientName = GlobalVariables.clientname.ToLower();
            List<string[]> journeydata = CSVReaderDataTable.GetCSVData("JourneyAPIContent", pageName, "clinicaljourney", ClientName);
            int length;

            string[] path = { "Title" , "ActionText", "ShowDismiss", "MoreInfoText", "IncentiveValue", "IncentiveSymbol", "AfterCallToActionCards" };
            List<string[]> result = ApiKeywords.GetResponse(jsonresponse, path);
            Console.WriteLine("Journey Count Expected: "+journeydata.Count+ " , Actual Count: "+ result.Count);         
            if (result.Count() != 0 && journeydata.Count() != 0)
            {
                if (journeydata.Count() < result.Count())
                    length = journeydata.Count();
                else
                    length = result.Count();
                for (int i = 0; i < length; i++)
                {
                    int expstartindex = 4;
                    string elementname = journeydata.ElementAt(i)[3];
                    for (int j = 0; j < result.ElementAt(0).Count(); j++)
                    {
                        string expectedtext = journeydata.ElementAt(i)[expstartindex];
                        string actualtext = result.ElementAt(i)[j];
                        expstartindex++;
                        Console.WriteLine("Element Name : " + elementname + "\tExpectedText : " + expectedtext + "\tActualText : " + actualtext);
                        softAssertions.Add(elementname, expectedtext, actualtext, "contains");
                        //if (journeydata.Count() < result.Count())
                        //    softAssertions.Add(elementname, "", actualtext, "not equals");
                        //else
                        //    softAssertions.Add(elementname, expectedtext, "", "not equals");
                    }
                }
            }
            else
                softAssertions.Add("Total tiles Doesn't match", journeydata.Count(), result.Count(), "equals");

        }

    }
}

