using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;

namespace AutomationFramework.Pages.WebPages
{
    class CommonApi
    {
        public string GetToken()
        {
            Common cmn = new Common();
            string env = cmn.GetEnvFromUrl();
            string apiurl = "https://"+env+"services.onlifehealth.com/login/auth/token";
            Console.WriteLine("ApiUrl : "+apiurl);
            ApiKeywords.InitializeRequest(apiurl);
            ApiKeywords.SetMethod("post");
            ApiKeywords.SetParameter("platform_Id", "android");
            ApiKeywords.SetParameter("client_secret", "YcOTVgx11etcu0KFpPTWdumUTkvs7A3wxjqSqSjPRZw");
            ApiKeywords.SetParameter("scope", "manage");
            ApiKeywords.SetParameter("password", "Password1");
            ApiKeywords.SetParameter("device_token", "f3SBXqMIq40:APA91bFRoVTbQB0fJk5Cr2fPfQF1b3e_8Tn_rmPMjaLryCrCwAuMCEqzsBAIug-E6OQ7Qe2zaF5UBjtGUh7-P30IslZL6_WV_Yd4nf9hhBVCOkO7-9WnhNqv2JPgid5TJEXkAWZOwLa9");
            ApiKeywords.SetParameter("client_id", "277a638465ca44ab917fe4f45f2154a3");
            ApiKeywords.SetParameter("username", GlobalVariables.username);
            ApiKeywords.SetParameter("grant_type", "password");
            string response = ApiKeywords.SendRequest();
            Console.WriteLine("\nResponse : "+response);
           
            dynamic stuff = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
            string token = stuff.Access_Token;
            token = "Bearer " + token;
            Console.WriteLine("\n\nAccess_Token : "+ token);

            return token;

        }

        public void getUserLoginDetails(string username)
        {
            String url;
            string path;
            url = GetServicesUrl() + "/mobileapi/api/logindetails";
            ApiKeywords.InitializeRequest(url);
            ApiKeywords.SetMethod("GET");
            ApiKeywords.SetParameter("username", username);
            string responsce = ApiKeywords.SendRequest();
            path = "actor_Id";
            string actorid = ApiKeywords.GetValueFromJSONObject(responsce, path);
            System.Console.WriteLine("Response of API  " + responsce);
            //System.Console.WriteLine("Response of API  " + actorid);
            GlobalVariables.actorid = actorid;
            path = "user_Id";
            string user_Id = ApiKeywords.GetValueFromJSONObject(responsce, path);
            GlobalVariables.userid = user_Id;
            path = "serviceCycle_Id";
            string serviceCycle_Id = ApiKeywords.GetValueFromJSONObject(responsce, path);
            GlobalVariables.servicecycleid = serviceCycle_Id;
            path = "group_Id";
            string group_Id = ApiKeywords.GetValueFromJSONObject(responsce, path);
            GlobalVariables.groupid = group_Id;
        }
        public void getActorid(String username)
        {
            
                String url;
                string path;
                url = GetServicesUrl() + "/mobileapi/api/logindetails";
                ApiKeywords.InitializeRequest(url);
                ApiKeywords.SetMethod("GET");
                ApiKeywords.SetParameter("username", username);
                string responsce = ApiKeywords.SendRequest();
                path = "actor_Id";
                string actorid = ApiKeywords.GetValueFromJSONObject(responsce, path);
                //System.Console.WriteLine("Response of API  " + responsce);
                //System.Console.WriteLine("Response of API  " + actorid);
                GlobalVariables.actorid = actorid;
            }
        // return the Base url of API 
        public string GetServicesUrl()
        {
            Common cmn = new Common();
            string env = cmn.GetEnvFromUrl();
            string baseurl = "https://" + env + "services.onlifehealth.com";
            return (baseurl);
        }

        public string GetServices2Url()
        {
            Common cmn = new Common();
            string env = cmn.GetEnvFromUrl();
            string baseurl = "https://" + env + "services2.onlifehealth.com";
            return (baseurl);
        }
        /// <summary>
        /// This mehod pass header information to APIs
        /// </summary>
        /// <param name="token"></param>
        public void SetMobileCommonHeader(string token)
        {
            //cma.GetToken();
            //string token = "Bearer "+ cma.GetToken();
            ////sstring token = "Bearer gAAAABUqJH62DQYjH7RvIjv2vn0BhziE-r8UhoErz6tGiRlf37qRlRZOdq0ctRoH86-XAToVDDhhr-SVstVY1gcnYv8SIPzaWObgNCx3GwkZJSQgdp_BGW4kEsjJJOlPZ_pgUgErezca2e05c4MPX4idftqQV_JPaNSOhw5h6Kkthc2kJAEAAIAAAADp4V1xSfnJLrmLO4OIo9ZbZHCSIb7CsHI7QgCFLCYYo2p1me23WD24qe4Y7zrjx5oJPuIwpL7EpNGF5BmRNk5quNK890_oRB_xSL0u2pfX9UJZDy0D-4pfOz9QIcok0TSIdANlEG7dFR_yAGGbEz_yNCwcg0Gdzw2GAgUWaOXl5O4evPGUsSm5q-Qo-yjKbIIisEFn--ik8FpfvLhQS0AIl3V75KHZ0baJ1BUfnnfCm_mwehqwHNV3sftQloFnCyghV7xHA4C4JBhaFDxk2EOgN6rlxIN0ZwScxph12O45b5XfydKJnFgv87HjnyQuIHmEZY36eul5MYT0FzYYu8SIXK3LmK8Qc6pBstkmOHLvP6Mxark5rGyLPYWcH4SG64M";
            Console.WriteLine("Header : "+token);
            ApiKeywords.SetHeader("Content-Type", "application/json");
            ApiKeywords.SetHeader("Authorization",token);
            ApiKeywords.SetHeader("actor_Id",GlobalVariables.actorid);
            ApiKeywords.SetHeader("serviceCycle_Id", GlobalVariables.servicecycleid);
            ApiKeywords.SetHeader("group_Id", GlobalVariables.groupid);


        }


        /// <summary>
        ///  Return the User Id
        /// </summary>

        public string GetUserID()
        {
            string path;
            Common cmn = new Common();
            string env = cmn.GetEnvFromUrl();
            string apiurl = "https://" + env + "services.onlifehealth.com/mobileapi/api/logindetails?username=" + GlobalVariables.username;
            Console.WriteLine(apiurl);
            ApiKeywords.InitializeRequest(apiurl);
            ApiKeywords.SetMethod("GET");
            string jsonresponse = ApiKeywords.SendRequest();
            path = "user_Id";
            string userid = ApiKeywords.GetValueFromJSONObject(jsonresponse, path);
           
            return userid;
        }
        /// <summary>
        /// return the actor id and token of member
        /// Call in constructor of the API class
        /// </summary>
        /// <returns></returns>
        public string getTokeandActorid()
        {
            string token = GetToken();
            getUserLoginDetails(GlobalVariables.username);
            return token;
        }
    }
}
