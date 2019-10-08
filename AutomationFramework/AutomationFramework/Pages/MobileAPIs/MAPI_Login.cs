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
    class MAPI_Login

    {
        CommonApi cma = new CommonApi();
        string token;
        string baseurl;
       // String pageName;
        SoftAssertions softAssertions = null;

        public  MAPI_Login(SoftAssertions softAssertions)
        {
            //return the url of API    
            this.softAssertions = softAssertions;
            baseurl = cma.GetServicesUrl();
            token = cma.getTokeandActorid();
        }
        public void getstatuscode( string rc)
        {
            int code=ApiKeywords.GetStatusCode();
            softAssertions.Add(rc, 200, code,"equals");
        }
        public void VerifyAuthToken()
        {
           
           token=cma.GetToken();
            //System.Console.WriteLine("Token   "+token);
        }
        public void VerifyLoginDetailsAPI(string username)
        {
            String url;
            url = baseurl + "/mobileapi/api/logindetails";
            ApiKeywords.InitializeRequest(url);
            ApiKeywords.SetMethod("GET");
            ApiKeywords.SetParameter("username",username);
            string responsce = ApiKeywords.SendRequest();
            string path = "actor_Id";
            string actorid = ApiKeywords.GetValueFromJSONObject(responsce, path);
            //System.Console.WriteLine("Response of API  " + responsce);
            //System.Console.WriteLine("Response of API  " + actorid);
            GlobalVariables.actorid = actorid; 
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
            softAssertions.Add("Configuration is_fitness_enabled", "true", value, "equals");
            System.Console.WriteLine("Response of API  " + responsce);
            //System.Console.WriteLine("Response of API  " + actorid);
            //GlobalVariables.actorid = actorid;
        }
        // Call by Dashboard tile
         public void VerifyDashboardTile()
        {


        }

        public void VerifyMobileTosAPI()
        {
            String url;
            url = baseurl + "/mobileapi/api/tos?tosType=mobile";
            ApiKeywords.InitializeRequest(url);
            ApiKeywords.SetMethod("GET");
            cma.SetMobileCommonHeader(token);
            // ApiKeywords.SetParameter("tosType","mobile");
            string responsce = ApiKeywords.SendRequest();
            string path = "content";
            string content = ApiKeywords.GetValueFromJSONObject(responsce, path);
            softAssertions.Add("TOS content", "Terms of Service", content, "Contains");
            System.Console.WriteLine("Response of API  " + responsce);
            // System.Console.WriteLine("Response of API  " + content);

        }
    }
   
}
