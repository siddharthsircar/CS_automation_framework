using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using AutomationFramework.Pages.WebPages;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.MobileAPIs
{
    class MAPI_Trackers

    {
        CommonApi cma = new CommonApi();
        string token;
        string baseurl;
        String Guid;
        // String pageName;
        SoftAssertions softAssertions = null;

        public MAPI_Trackers(SoftAssertions softAssertions)
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
               
        public void VerifyAllmemberTrakerListAPI(string[] tracker)
        {
            String url;
            url = baseurl + "/mobileapi/api/trackers/userId/"+GlobalVariables.userid;
            ApiKeywords.InitializeRequest(url);
            ApiKeywords.SetMethod("GET");
            ApiKeywords.SetParameter("servicecycleid",GlobalVariables.servicecycleid);
            cma.SetMobileCommonHeader(token);
            string responsce = ApiKeywords.SendRequest();
            JToken to = JToken.Parse(responsce);
            
            for(int i=0;i<tracker.Length;i++)
            {
                string exp= "$.[?(@.Name=='"+tracker[i]+"')].Name";


                //String Guid = (String)to.SelectToken(exp,true);
                String name = (String)to.SelectToken(exp,true);
               // System.Console.WriteLine("exp of API  " + exp);
                System.Console.WriteLine("Name of API  " + name);
                softAssertions.Add("Trckers avilable",tracker[i],name, "equals", "false");
            }

            //JToken to = JToken.Parse(responsce);

            //String Guid = (String)to.SelectToken("$.[?(@.Name=='Stress')].Guid");
            //System.Console.WriteLine("Guid of API  " + Guid);

            //string[] path = { "Name" };
            //List<string[]> value = ApiKeywords.GetResponse(responsce, path);
            //for (int i = 0; i < value.Count(); i++)
            //{
            //    System.Console.WriteLine("Response of API  " + tracker[i]);
            //    softAssertions.Add("Trckers avilable", value.ElementAt(i)[0], tracker[i], "equals", "false");
            //}


        }

        public void VerifyTakerAPIs(String tracker, String title)
        {
            String url;
            url = baseurl + "/mobileapi/api/goals/tracker";
            ApiKeywords.InitializeRequest(url);
            ApiKeywords.SetMethod("GET");
            ApiKeywords.SetParameter("trackerType",tracker);
            cma.SetMobileCommonHeader(token);
            string responsce = ApiKeywords.SendRequest();

            System.Console.WriteLine("Response of API  " + responsce);
            string[] parentpath = { "inactive_goals" };
            string[] childpath = { "title" };
            List<string[]> value = ApiKeywords.GetResponse(responsce, parentpath, childpath);
            //for (int i = 0; i < value.Count(); i++)
            //{
            //    System.Console.WriteLine("Response of API  " + value[i]);
            //    softAssertions.Add("Trckers avilable", value.ElementAt(i)[0], tracker, "equals", "false");
            //}
            //softAssertions.Add("Configuration ha_percentage", "0", value, "equals", "false");
            for (int i = 0; i < value.Count; i++)
            {
                for (int j = 0; j < value.ElementAt(0).Count(); j++)
                {
                    Console.WriteLine("Title" + value.ElementAt(i)[j]);
                    softAssertions.Add("Trckers avilable", value.ElementAt(i)[j],title, "equals", "false");
                }
            }

        }

        public void SaveTrcker(string tracker)
        {

           String cdate= DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ");
            Console.WriteLine("Current Date  "+cdate);
            String parameter = "[{\"DataSource\":0,\"DataTime\":\""+cdate+"\",\"Measurement\":{\"Description\":\"Stress\",\"DisplayOrder\":1,\"Id\":25,\"IsRequired\":true,\"MaxRangeValue\":0,\"MeasurementType\":{\"Id\":3,\"Name\":\"Likert scale\"},\"MinRangeValue\":0,\"Name\":\"Stress\",\"ToolTip\":\"\",\"Unit\":{\"Abbreviation\":\"\",\"Description\":\"\",\"DisplayName\":\"\",\"Id\":11,\"Name\":\"best(1) to worst(5) scale\"},\"allowDecimal\":false},\"UserId\":\"" + GlobalVariables.userid + "\",\"Value\":5,\"isValueModified\":true,\"isValueValid\":true}]";
            String url;
            url = baseurl + "/mobileapi/api/trackers/userId/" + GlobalVariables.userid;
            ApiKeywords.InitializeRequest(url);
            ApiKeywords.SetMethod("GET");
            ApiKeywords.SetParameter("servicecycleid", GlobalVariables.servicecycleid);
            cma.SetMobileCommonHeader(token);
            string responsce = ApiKeywords.SendRequest();
            JToken to = JToken.Parse(responsce);
            string exp = "$.[?(@.Name=='" + tracker + "')].Guid";
            Guid = (String)to.SelectToken(exp, true);
            url = baseurl + "/mobileapi/api/trackers/userId/" + GlobalVariables.userid+ "/trackerGuid/"+Guid;
            //Console.WriteLine("URL:  "+url);
            ApiKeywords.InitializeRequest(url);
            //Console.WriteLine("Parameter   : " + parameter);
            ApiKeywords.SetParameter("application / json", parameter, "body");
            ApiKeywords.SetMethod("POST");
            cma.SetMobileCommonHeader(token);
            string responsce1 = ApiKeywords.SendRequest();
            JObject ob = JObject.Parse(responsce1);
            String exp1 = "$.allow_user_feedback";
            String result = (String)ob.SelectToken(exp1);
            //System.Console.WriteLine("Response of API  " +result);
            softAssertions.Add("Stress Tracker save ","false", result, "equals", "false");

            
        }

        public void VerifySavedTrcker()
        {
            String url = baseurl + "/mobileapi/api/trackers/userId/" + GlobalVariables.userid + "/trackerGuid/" + Guid;
            ApiKeywords.InitializeRequest(url);
            ApiKeywords.SetMethod("GET");
            cma.SetMobileCommonHeader(token);
            string responsce1 = ApiKeywords.SendRequest();
            Console.WriteLine("Response of APIs" +responsce1);
            JObject ob = JObject.Parse(responsce1);

            String result = (String)ob["UserId"];
            softAssertions.Add("Stress Tracker save ", result,GlobalVariables.userid, "equals", "false");

        }

    }
       
   
}
