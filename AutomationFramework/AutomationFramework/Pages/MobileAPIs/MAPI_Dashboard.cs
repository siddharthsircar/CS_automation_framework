using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using AutomationFramework.Pages.WebPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.MobileAPIs
{
    class MAPI_Dashboard

    {
        CommonApi cma = new CommonApi();
        string token;
        string baseurl;
        // String pageName;
        SoftAssertions softAssertions = null;

        public MAPI_Dashboard(SoftAssertions softAssertions)
        {
            //return the url of API    
            this.softAssertions = softAssertions;
            baseurl = cma.GetServicesUrl();
            token = cma.getTokeandActorid();
        }
        public void getstatuscode(string rc)
        {
            int code = ApiKeywords.GetStatusCode();
            softAssertions.Add(rc, 200, code, "Equals","false");
        }
        public void VerifyAuthToken()
        {

            token = cma.GetToken();
            //System.Console.WriteLine("Token   "+token);
        }


        // call in config test
        public void VerifyMobileConfigurationAPI(string username)
        {
            String url;
            url = baseurl + "/mobileapi/api/configuration";
            ApiKeywords.InitializeRequest(url);
            ApiKeywords.SetMethod("GET");
            cma.SetMobileCommonHeader(token);
            string responsce = ApiKeywords.SendRequest();
            string path = "is_fitness_enabled";
            string value = ApiKeywords.GetValueFromJSONObject(responsce, path);
            softAssertions.Add("Configuration is_fitness_enabled", "true", value, "equals","false");
            System.Console.WriteLine("Response of API  " + responsce);
            //System.Console.WriteLine("Response of API  " + actorid);
            //GlobalVariables.actorid = actorid;
        }
        // call in config test
        public void VerifyNotificationCountAPI()
        {
            String url;
            url = baseurl + "/mobileapi/api/member/" + GlobalVariables.actorid + "/notificationscount";
            ApiKeywords.InitializeRequest(url);
            ApiKeywords.SetMethod("GET");
            cma.SetMobileCommonHeader(token);
            string responsce = ApiKeywords.SendRequest();
            System.Console.WriteLine("Response of API  " + responsce);
            //GlobalVariables.actorid = actorid;
        }
        //Call by New member Journey Test case
        public void VerifyNewMemberJourney()
        {

            String url;
            url = baseurl + "/mobileapi/v1.0/member/" + GlobalVariables.actorid + "/journey";
            ApiKeywords.InitializeRequest(url);
            ApiKeywords.SetMethod("GET");
            cma.SetMobileCommonHeader(token);
            string responsce = ApiKeywords.SendRequest();
            System.Console.WriteLine("Response of API  " + responsce);
            VerifyJourneyTiles("MobileJourneyAPIContent", responsce);
        }
        public void VerifyJourneyTiles(string filename, string response)
        {
            int length;
            string ClientName = GlobalVariables.clientname.ToLower();
            List<string[]> journeydata = CSVReaderDataTable.GetCSVData(filename, "MAPI_Dashboard", "newmemberjourney", ClientName);
            string[] path = { "Title", "ActionText", "ShowDismiss", "MoreInfoText", "IncentiveValue", "IncentiveSymbol" };
            List<string[]> result = ApiKeywords.GetResponse(response, path);
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
                        softAssertions.Add(elementname, expectedtext, actualtext, "contains","false");
                        //if (journeydata.Count() < result.Count())
                        //    softAssertions.Add(elementname, "", actualtext, "not equals");
                        //else
                        //    softAssertions.Add(elementname, expectedtext, "", "not equals");
                    }
                }
            }
            else
                softAssertions.Add("Total tiles Doesn't match", journeydata.Count(), result.Count(), "equals","false");

        }

        public void VerifyEStreamLoding(string response)
        {
            int length;
            string ClientName = GlobalVariables.clientname.ToLower();
           
            string[] path = {"Title"};
            List<string[]> result = ApiKeywords.GetResponse(response, path);
            
                
                       
                        string actualtext = result.ElementAt(0)[1];
                       
                        Console.WriteLine("EStream Tile  \tExpectedText : " + "LiveHealth" + "\tActualText : " + actualtext);
                        softAssertions.Add("EStream Tile", "LiveHealth", actualtext, "contains", "false");
                        //if (journeydata.Count() < result.Count())
                        //    softAssertions.Add(elementname, "", actualtext, "not equals");
                        //else
                        //    softAssertions.Add(elementname, expectedtext, "", "not equals");
                   
        }

        // //Call by New member Journey Test case
        public void VerifyEstream()
        {

            String url;
            url = baseurl + "/mobileapi/v1.0/member/" + GlobalVariables.actorid + "/engagementStream";
            ApiKeywords.InitializeRequest(url);
            ApiKeywords.SetMethod("GET");
            cma.SetMobileCommonHeader(token);
            string responsce = ApiKeywords.SendRequest();
            System.Console.WriteLine("Response of API  " + responsce);
            //VerifyJourneyTiles("MobileJourneyAPIContent", responsce);
            //VerifyEStreamLoding(responsce);
        }

        // Call by Dashboard tile
        public void VerifyDashboardTile()
        {
            String url;
            url = baseurl + "/mobileapi/api/dashboardtiles";
            ApiKeywords.InitializeRequest(url);
            ApiKeywords.SetMethod("POST");
            ApiKeywords.SetParameter("application / json", "{\"date\":\"09\\/19\\/2018\",\"tiles\":[\"points\",\"unread_messages_count\"]}", "body");
            cma.SetMobileCommonHeader(token);
            string responsce = ApiKeywords.SendRequest();

            System.Console.WriteLine("Response of API  " + responsce);
            string path = "ha_percentage";
            string value = ApiKeywords.GetValueFromJSONObject(responsce, path);
            softAssertions.Add("Configuration ha_percentage", "0", value, "equals","false");

        }

    }
       
   
}
