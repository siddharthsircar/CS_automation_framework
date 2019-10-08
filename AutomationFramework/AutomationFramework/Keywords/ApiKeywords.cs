using AutomationFramework.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script;

namespace AutomationFramework.Keywords
{
    public class ApiKeywords
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static RestClient client = new RestClient();
        static RestRequest request = null;
        static IRestResponse response = null;
        static JsonDeserializer ds = null;
        static JsonSerializer serializer = null;
        private static ExtentTestManager Report = new ExtentTestManager();

        public static void InitializeRequest(string url)
        {
            Console.WriteLine("Url : " + url);
            client = new RestClient(url);
            request = new RestRequest();
            //try
            //{

            //    client.BaseUrl = new System.Uri(url);
            //}
            //catch (UriFormatException ur)
            //{
            //    Console.WriteLine(ur.Message);
            //}
            Report.Info("Request Initialized " + url);
        }

       public static void SetBasicAuthorization(string username, string password)
        {
            var authenticator = new HttpBasicAuthenticator(username, password);
            authenticator.Authenticate(client, request);
        }
        public static string SendRequest()
        {
            response = client.Execute(request);
            Report.Info("Request Send ");
            return response.Content;
        }

        public static int GetStatusCode()
        {
            int statuscode = 0;
            if (response != null)
            {
                statuscode = (int)response.StatusCode;
            }
             //Report.Info("Response code " +statuscode);

            return statuscode;
        }
        public static void SetHeader(string name, string value)
        {
            request.AddHeader(name, value);
            Report.Info("Header is set with Name = " + name + " Value = " + value);

        }

        public static void SetParameter(string key, string value)
        {
            request.AddParameter(key, value);
            Report.Info("Parameter is set with Name = " + key + " Value = " + value);

        }

        public static void SetParameter(string key, string value,string type)
        {

            switch(type)
            {
                case "body":
                    request.AddParameter(key, value,ParameterType.RequestBody);
                    break;

            }
           

        }

        public static void SetMethod(String method)
        {
            switch (method.ToLower())
            {
                case "get":
                    request.Method = Method.GET;
                    break;
                case "post":
                    request.Method = Method.POST;
                    break;
                case "put":
                    request.Method = Method.PUT;
                    break;
                case "delete":
                    request.Method = Method.DELETE;
                    break;
                case "head":
                    request.Method = Method.HEAD;
                    break;
                case "merge":
                    request.Method = Method.MERGE;
                    break;
                case "patch":
                    request.Method = Method.PATCH;
                    break;

            }
        }

        public T DeserializeJson<T>(string responsemsg)
        {

            ds = new JsonDeserializer();

            var content = responsemsg;

            using (var stringReader = new StringReader(content))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    return serializer.Deserialize<T>(jsonTextReader);
                }
            }

        }

        public static List<string[]> ParseJsonResponse(string response, string path)
        {
            List<string[]> output = new List<string[]>();
            //var jsonObject = new System.Web.Script.Serialization.JavaScriptSerializer();
            //var des = jsonObject.DeserializeObject(response);
            //var dicObj = (Dictionary<string,object>)des;
            //var val = dicObj[path];

            if(response.StartsWith("{"))
            {
                var jobject = JObject.Parse(response);

            }
            else if (response.StartsWith("["))
            {

            }
            return output;
        }
        //return the value of 
        public static string GetValueFromJSONObject(string request, string path)
        {
            var ss = JObject.Parse(request);

            string s = ss[path].ToString();
            Console.WriteLine(s);
            return s;
        }

        /// <summary>
        /// Parse JSON response with Object in Array in Object (Please rename the method)
        /// Sample response:
        /// {
        ///     "parentObjPath" : [
        ///     {
        ///         "subObjPath[0]" : ""
        ///         "subObjPath[1]" : ""
        ///     }
        ///   ]
        /// }
        /// </summary>
        /// <param name="request"></param>
        /// <param name="parentObjPath"></param>
        /// <param name="subObjPath"></param>
        /// <returns></returns>
        public static List<string[]> GetResponse(string request, string[] parentObjPath, string[] subObjPath)
        {
            List<string[]> output = new List<string[]>();
            var jObject1 = JObject.Parse(request);
            Console.WriteLine("Parent Object Count: " + jObject1.Count);
            for (int i = 0; i < parentObjPath.Length; i++)
            {
                //Console.WriteLine("Parent Value: " + jObject1.Property(parentObjPath[i]).ToString());
                if (jObject1.Property(parentObjPath[i]).ToString().Contains("["))
                {
                    var subJArray = JArray.Parse(jObject1[parentObjPath[i]].ToString());
                    //Console.WriteLine("subjArray Count: "+subJArray.Count);
                    //Console.WriteLine("subjArray: "+subJArray.ToString());
                    for (int j=0; j < subJArray.Count; j++)
                    {
                        string[] arr = new string[subObjPath.Length];

                        JObject subJobject = (JObject)subJArray[j];
                        //Console.WriteLine("subjObject"+j+": " + subJobject);
                        for (int k = 0; k < subObjPath.Length; k++)
                        {
                            arr[k] = subJobject[subObjPath[k]].ToString();
                        }
                        output.Add(arr);
                    }                   
                }           
            }
            return output;
        }

        public static List<string[]> GetResponse(string request, string[] path)
        {
            List<string[]> output = new List<string[]>();
            
            //Console.WriteLine(request);
            var ss = JArray.Parse(request);
            for (int i = 0; i < ss.Count; i++)
            {
                JObject jObject = (JObject)ss[i];
                string[] arr = new string[path.Length];
                List<string[]> child = new List<string[]>();
                for (int m = 0; m < arr.Length; m++)
                {
                    arr[m] = "";
                }

                for (int j = 0; j < path.Length; j++)
                {
                    if (!jObject.Property(path[j]).ToString().Contains("["))
                    {
                        arr[j] = jObject[path[j]].ToString();
                    }
                    else if (jObject.Property(path[j]).ToString().Contains("["))
                    {
                        var subJArray = JArray.Parse(jObject[path[j]].ToString());
                        for (int k = 0; k < subJArray.Count(); k++)
                        {
                            string[] arr1 = new string[path.Length];
                            JObject subJObject = (JObject)subJArray[k];
                            for (int l = 0; l < path.Length; l++)
                            {
                                arr1[l] = subJObject[path[l]].ToString();
                            }
                            child.Add(arr1);
                        }
                    }
                }
                output.Add(arr);
                output.AddRange(child);
            }

            return output;

             /*Used to fetch value using desearlize
            //var ss = JObject.Parse(request);
            //dynamic ss = Newtonsoft.Json.JsonConvert.DeserializeObject(request);
            //foreach (var sk in ss)
            //{
            //    if ((sk.Value).Path == path)
            //    {
            //        Console.WriteLine(sk.Value);
            //    }
            //}
            //string s = ss.group_Id;*/

             /*     for (int i = 0; i < ss.Count; i++)
                  {
                      JObject jObject = (JObject)ss[i];
                      string[] arr = new string[path.Length];
                      for (int j = 0; j < path.Length; j++)
                      {
                          //for get JObject Proprerty
                          foreach (JProperty p in jObject.Properties())
                          {
                              //Check value to contain JArray or not
                              if (!p.Value.ToString().Contains("[") && p.Name.ToString().Equals(path[j]))
                              {
                                  arr[j] = jObject[path[j]].ToString();
                                  Console.WriteLine(arr[j]);
                                  output.Add(arr);
                              }
                              else if (p.Value.ToString().Contains("["))
                              {
                                   JArray subJArray = (JArray)jObject[p.Name];
                                  foreach (JObject subJObject in subJArray.Children<JObject>())
                                  {

                                  }
                              }

                          }
                      }
                  } */
        }

        //This method will parse IncentiveHistory Json response and return required data
        public static List<string[]> ParseIncentiveHistoryResponse(string response, string parentObjPath, string[] subObjPath)
        {
            List<string[]> output = new List<string[]>();
            //Console.WriteLine(request);
            var ss = JArray.Parse(response);
            
            JObject jObject = (JObject)ss[0];
            var subJArray = JArray.Parse(jObject[parentObjPath].ToString());
            for (int k = 0; k < subJArray.Count(); k++)
            {
                JObject subJObject = (JObject)subJArray[k];
                string[] arr = new string[subObjPath.Length];

                for (int l = 0; l < subObjPath.Length; l++)
                {
                    arr[l] = subJObject[subObjPath[l]].ToString();
                    //Console.WriteLine("Arr: " + arr[l]);
                }
                output.Add(arr);
                //get Values
            }
            
            return output;

        }

        //This method will parse Eligible Activities Json response and return required data
        public static List<string[]> ParseEligibleActivitiesResponse(string response, object[] path)
        {
            List<string[]> output = new List<string[]>();
            //Console.WriteLine(request);
            var ss1 = JObject.Parse(response);
            

            JObject jmainObject = (JObject)ss1;

            var ss = JArray.Parse(jmainObject["Matrix"].ToString());

            for (int i = 0; i < ss.Count; i++)
            {

                JObject jObject = (JObject)ss[i];
                string[] arr = new string[path.Length];
                for(int m=0;m<arr.Length;m++)
                {
                    arr[m] = "";
                }
                int len = path.Length;
                len = len - 1;
                for (int j = 0; j < len; j++)
                {
                    JProperty jprop = null;
                    //Type type = path[j].GetType();
                    //if (type.IsAssignableFrom(typeof(string)))
                        jprop =  jObject.Property(path[j].ToString());
                    Boolean isarray = jprop.Value.ToString().Contains("[");
                    if (!(isarray))
                    {
                        arr[j] = jObject[path[j]].ToString();
                        //Console.WriteLine(arr[j]);
                    }
                    else
                    {
                        var subJArray = JArray.Parse(jObject[path[j]].ToString());
                        string[] action = path[len] as string[];
                        Array.Resize(ref arr, (arr.Length-2) + (subJArray.Count()*action.Length));
                        for(int p=j;p<arr.Length;p++)
                        {
                            arr[p] = "";
                        }
                        int len1 = 0;
                        for (int k = 0; k < subJArray.Count(); k++)
                        {
                            JObject subJObject = (JObject)subJArray[k];
                            action = path[len] as string[];
                            for (int l = 0; l < action.Length; l++)
                            {
                                arr[j + len1] = subJObject[action[l]].ToString();
                                len1++;
                            }
                                
                            //get Values
                        }
                    }
                }

                output.Add(arr);


            }

            return output;
        }

    }
}
