using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using UserCreation.GroupId;
using UserCreation.UserCreation;
using AutomationFramework.Framework;

namespace AutomationFramework.Pages
{
    public class CreateNewUser
    {
        int ccount = 1;
        string clientName;
        string url;
        string actorid, username;
        string env,environment, isregistered;
        string firstname, lastname, email, password,dob,zipcode,popseg1,ssn;
        static Boolean havenewuser = false;
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        /// <summary>
        /// This method gets called right after the recording has been started.
        /// It can be used to execute recording specific initialization code.
        /// </summary>
        private void Init()
        {
            try
            {
                clientName = GlobalVariables.clientname;//e.g, "meabt";
                url = GlobalVariables.baseurl;
                environment = GlobalVariables.environment;//e.g Staging
                isregistered = GlobalVariables.isregistered;
                username = null;
                int startindex = url.IndexOf("/");
                int endindex = url.IndexOf('.');
                startindex = startindex + 2;
                int index = endindex - startindex;
                env = url.Substring(startindex, index); // e.g, qa2012
                Console.WriteLine("Environment : "+ environment + " Env : " + env + " StartIndex : " + startindex + " EndIndex : " + endindex);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// This method create and fetch new user created form API
        /// </summary>
        /// <returns></returns>
        public Boolean getNewUser()
        {
            try
            {
                Init();
                
                if (!(String.IsNullOrEmpty(env)) && !(String.IsNullOrEmpty(clientName)))
                {
                    string grpId = getGroupID(env, clientName);
                    //string grpId = "6810b98a-a0e4-4831-8b38-a97da0c3185e";
                    if ((grpId != null) || !(String.IsNullOrEmpty(grpId)))
                    {

                        //Method to execute create user APIs
                        createNewUserFromAPI(clientName, env, grpId);
                        Console.WriteLine("Username : " + username);

                        if (username.Equals("Error"))
                        {
                            Console.WriteLine("Unable to create user");
                            return havenewuser;
                        }
                        if ((username != null) || !(String.IsNullOrEmpty(username)))
                        {
                            Console.WriteLine("User created in " + ccount + " attempt");
                            log.Info("User created, User Name:  " + username);
                            havenewuser = true;
                            return havenewuser;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (ccount == 3)
                {
                    havenewuser = false;
                    Console.WriteLine(e.Message);
                    Console.WriteLine("User is not created even after three attempts");
                    return havenewuser;
                }
                else
                {
                    ccount++;
                    havenewuser = true;
                    getNewUser();
                }
            }
            return havenewuser;
        }

        /// <summary>
        /// Http Webclient method  use to create user
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="env"></param>
        /// <param name="grpId"></param>
        public void createNewUserFromAPI(string clientName, string env, string grpId)
        {
            HttpWebResponse webResponse = null;
            string response = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://" + env + "-services2.onlifehealth.com/settings/api/createTestUsers/createUser");
            request.Method = "PUT";
            request.ContentType = "application/json";

            string status = "";

            if(isregistered.ToLower().Equals("true"))
            {
                status = "2"; // to create registered user
            }
            else if(isregistered.ToLower().Equals("false"))
            {
                status = "1"; // to create un-registered user
            }
            string data = "{\"default\":{\"username\":true,\"password\":true,\"fname\":true,\"lname\":true,\"email\":true,\"address1\":true,\"address2\":true,\"phone1\":true,\"phone2\":true,\"zip\":true,\"DoB\":true},\"gender\":\"male\",\"subdep\":\"default\",\"timezone\":\"default\",\"state\":\"default\",\"batch\":false,\"isBusy\":1,\"groupGuid\":\"" + grpId + "\",\"groupName\":\"" + clientName + "\",\"quantity\":1,\"status\":\""+status+"\"}";

            //Console.WriteLine("Data : "+data);
            request.ContentLength = data.Length;
            using (Stream webStream = request.GetRequestStream())
            using (StreamWriter requestWriter = new StreamWriter(webStream, Encoding.ASCII))
            {
                requestWriter.Write(data);
            }

            try
            {
                webResponse = (HttpWebResponse)request.GetResponse();
                
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if(webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            response = responseReader.ReadToEnd();
                            //Console.WriteLine(response);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            if (webResponse.StatusCode==HttpStatusCode.OK)
            {
                //Console.WriteLine("Response : "+response);
                if (!String.IsNullOrEmpty(response))
                {
                    response = response.Replace("\\", "");
                    response = response.Remove(0, 1);
                    response = response.Remove(response.Length - 1, 1);
                    Console.WriteLine("Response=" + response);
                    var des = (NewUserRootObject)JsonConvert.DeserializeObject(response, typeof(NewUserRootObject));
                    //Report.Info(ReportLevel.Info.ToString(),queryResult.Content.ToString());

                    if (des != null)
                    {
                        actorid = des.Eligibility.ActorID;
                        username = des.Eligibility.UserName;
                        password = des.Eligibility.Password;
                        email = des.Eligibility.Email;
                        firstname = des.Eligibility.FirstName;
                        lastname = des.Eligibility.LastName;
                        dob = des.Eligibility.DOB;
                        zipcode = des.Eligibility.ZipCode;
                        popseg1 = des.Eligibility.PopSeg1;
                        ssn = des.Eligibility.SSN;

                        AddConfigurationEntry("actorid", actorid);
                        AddConfigurationEntry("username", username);
                        AddConfigurationEntry("password", password);
                        AddConfigurationEntry("email", email);
                        AddConfigurationEntry("firstname", firstname);
                        AddConfigurationEntry("lastname", lastname);
                        AddConfigurationEntry("dob", dob);
                        AddConfigurationEntry("zipcode", zipcode);
                        AddConfigurationEntry("popseg1", popseg1);
                        AddConfigurationEntry("ssn", ssn);
                        Console.WriteLine("Username set(TestSuite.Current.Parameters['UserName']) : " + username);
                        Console.WriteLine("Email set(TestSuite.Current.Parameters['Email']) : " + email);
                        Console.WriteLine("FullName set(TestSuite.Current.Parameters['FullName']) : " + firstname + " , " + lastname);
                    }
                    else
                    {
                        Console.WriteLine("Can't deserialize json response ,, returning null");
                    }
                }
                else
                {
                    Console.WriteLine("Json string is null");
                }
            }
            else
            {
                Console.WriteLine("Api Response Status Code : " + webResponse.StatusCode);
                username = "Error";
            }

            
        }
        /// <summary>
        /// Method is use to get the Group id of client 
        /// </summary>
        /// <param name="env"></param>
        /// <param name="clName"></param>
        /// <returns></returns>
        public string getGroupID(string env, string clName)
        {
            string grpID = null;
            string response = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://" + env + "-services2.onlifehealth.com/settings/api/admin/Search/" + clName + "/1");
            try
            {
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            response = responseReader.ReadToEnd();
                            //Console.WriteLine("Group response : "+response);
                        }
                    }
                }
            }     
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            var des = (GroupIDRootObject)JsonConvert.DeserializeObject(response, typeof(GroupIDRootObject));
            grpID = des.Groups.ElementAt(0).GroupID;

            Console.WriteLine("GroupId of " + clName + " : " + grpID);
            log.Info("Group Id " + grpID);
            return grpID;
        }

        public void AddToCSV(string first, string second)
        {
            var csv = new StringBuilder();
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            string filePath = projectPath + "Resources//TestData//RegistrationInputData.csv";
            //Suggestion made by KyleMit
            var newLine = string.Format("{0},{1}", first, second);
            csv.Insert(1,newLine);
            
            //after your loop
            File.WriteAllText(filePath, csv.ToString());
        }
        /// <summary>
        /// Use to set user name in Configuration
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void AddConfigurationEntry(string key,string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var entry = config.AppSettings.Settings[key];
            if(entry == null)
            {
                config.AppSettings.Settings.Add(key, value);
            }
            else
            {
                config.AppSettings.Settings[key].Value = value;
            }
            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
            Console.WriteLine("Key : "+key +" Value : "+ConfigurationManager.AppSettings[key]);
        }
    }
}
