using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.Incentive
{
    class Page_EligibleActivities
    {
        String pageName;
        SoftAssertions softAssertions = null;
        string jsonresponse;
        CommonApi capi = new CommonApi();
        public Page_EligibleActivities()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        public Page_EligibleActivities(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }
        public void InitializeEligibleActivitiesRequest()
        {
            Common cmn = new Common();
            
            string env = cmn.GetEnvFromUrl();
            string baseurl = "https://" + env + "services.onlifehealth.com/mobileapi/api/incentives/";

            capi.getActorid(GlobalVariables.username);
            string actorid = GlobalVariables.actorid;
            Console.WriteLine("ActorId : "+actorid);
            string apiurl = baseurl + actorid + "/activities";
            Console.WriteLine("ApiUrl : "+apiurl);
            ApiKeywords.InitializeRequest(apiurl);
        }

        public void InitializeIncentiveHistoryRequest()
        {
            Common cmn = new Common();
            string env = cmn.GetEnvFromUrl();
            string baseurl = "https://" + env + "services.onlifehealth.com/mobileapi/api/incentives/";

            capi.getActorid(GlobalVariables.username);
            string actorid = GlobalVariables.actorid;
            Console.WriteLine("ActorId : " + actorid);
            string apiurl = baseurl + actorid + "/history";
            Console.WriteLine("ApiUrl : " + apiurl);
            ApiKeywords.InitializeRequest(apiurl);
        }

        public void SetMethod()
        {
            ApiKeywords.SetMethod("GET");
        }
        public void SetHeader(string token)
        {
            
            //cma.GetToken();
            //string token = "Bearer "+ cma.GetToken();
            ////sstring token = "Bearer gAAAABUqJH62DQYjH7RvIjv2vn0BhziE-r8UhoErz6tGiRlf37qRlRZOdq0ctRoH86-XAToVDDhhr-SVstVY1gcnYv8SIPzaWObgNCx3GwkZJSQgdp_BGW4kEsjJJOlPZ_pgUgErezca2e05c4MPX4idftqQV_JPaNSOhw5h6Kkthc2kJAEAAIAAAADp4V1xSfnJLrmLO4OIo9ZbZHCSIb7CsHI7QgCFLCYYo2p1me23WD24qe4Y7zrjx5oJPuIwpL7EpNGF5BmRNk5quNK890_oRB_xSL0u2pfX9UJZDy0D-4pfOz9QIcok0TSIdANlEG7dFR_yAGGbEz_yNCwcg0Gdzw2GAgUWaOXl5O4evPGUsSm5q-Qo-yjKbIIisEFn--ik8FpfvLhQS0AIl3V75KHZ0baJ1BUfnnfCm_mwehqwHNV3sftQloFnCyghV7xHA4C4JBhaFDxk2EOgN6rlxIN0ZwScxph12O45b5XfydKJnFgv87HjnyQuIHmEZY36eul5MYT0FzYYu8SIXK3LmK8Qc6pBstkmOHLvP6Mxark5rGyLPYWcH4SG64M";
            //Console.WriteLine("Header : "+token);
            //ApiKeywords.SetHeader("Content-Type", "application/json");
            ApiKeywords.SetHeader("Authorization",token);
            ApiKeywords.SetHeader("Accept","application/json");
        }
        public void SendRequest()
        {
            jsonresponse = ApiKeywords.SendRequest();
            //Console.WriteLine("JsonResponse : "+jsonresponse);
        }

        public void VerifyEligibleActivities()
        {
            int length;
            string ClientName = GlobalVariables.clientname.ToLower();
            List<string[]> eligibleactivitiesdata = CSVReaderDataTable.GetCSVData("EligibleActivitiesData", pageName, "newmembereligibleactivities", ClientName);
            string[] actions = { "Url","ButtonText"};
            object[] matrix = { "Title", "Description","Points", "IncentiveSymbol","FrequencyCount","FrequencyDenomination","LockedForGateKeeper","GateKeeperText","CanEarnText","Actions",actions };
            List<string[]> result = ApiKeywords.ParseEligibleActivitiesResponse(jsonresponse, matrix);
            Console.WriteLine("Result Count : "+result.Count);
            Console.WriteLine("Eligible Activites Count : "+eligibleactivitiesdata.Count);
            for(int i=0; i<result.Count;i++)
            {
                foreach(string s in result.ElementAt(i))
                {
                    Console.Write("Value : "+s+"\t");
                }
                Console.WriteLine();
            }

            if (result.Count() != 0 && eligibleactivitiesdata.Count() != 0)
            {
                if (eligibleactivitiesdata.Count() < result.Count())
                    length = eligibleactivitiesdata.Count();
                else
                    length = result.Count();
                for (int i = 0; i < length; i++)
                {
                    int expstartindex = 4;
                    string elementname = eligibleactivitiesdata.ElementAt(i)[3];
                    for (int j = 0; j < result.ElementAt(i).Count(); j++)
                    {
                        string expectedtext = eligibleactivitiesdata.ElementAt(i)[expstartindex];
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
                softAssertions.Add("Activities count doesn't match", eligibleactivitiesdata.Count(), result.Count(), "equals");

        }

        public void VerifyHistoryData(List<string[]> incentivehistorydata,string category)
        {
            int length;
            string[] actions = { "ActionCategoryInternalName", "BadgeName", "PointsAwarded", "InsertDate", "IncentiveSymbol" };
            List<string[]> result = ApiKeywords.ParseIncentiveHistoryResponse(jsonresponse, "Value", actions);
            Console.WriteLine("Result Count : " + result.Count);
            Console.WriteLine("Eligible Activites Count : " + incentivehistorydata.Count);
            for (int i = 0; i < result.Count; i++)
            {
                foreach (string s in result.ElementAt(i))
                {
                    Console.Write("Value : " + s + "\t");
                }
                Console.WriteLine();
            }
            for(int i=0;i<incentivehistorydata.Count;i++)
            {
                Common cm = new Common();
                incentivehistorydata.ElementAt(i)[5] = cm.GetCurrentDate("M/dd/yyyy"); 
            }
            int explistindex = 0;
            if (result.Count() != 0 && incentivehistorydata.Count() != 0)
            {
                length = result.Count();
                for (int i = 0; i < length; i++)
                {
                    string actioncategoryinternalname = result.ElementAt(i)[0].ToLower().Trim();
                    if (category.ToLower().Equals(actioncategoryinternalname))
                    {
                        int expstartindex = 2;
                        string elementname = incentivehistorydata.ElementAt(explistindex)[2];
                        for (int j = 0; j < result.ElementAt(i).Count(); j++)
                        {
                            string expectedtext = incentivehistorydata.ElementAt(explistindex)[expstartindex];
                            string actualtext = result.ElementAt(i)[j];
                            expstartindex++;
                            Console.WriteLine("Element Name : " + elementname + "\tExpectedText : " + expectedtext + "\tActualText : " + actualtext);
                            softAssertions.Add(elementname, expectedtext, actualtext, "contains");
                            
                        }
                        explistindex++;
                    }
                    
                }
            }
            else
                softAssertions.Add("Activities count doesn't match", incentivehistorydata.Count(), result.Count(), "equals");

        }
    }
}
