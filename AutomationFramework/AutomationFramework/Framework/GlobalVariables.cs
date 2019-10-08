using System.Configuration;
using NUnit.Framework;

namespace AutomationFramework.Framework
{
    public class GlobalVariables
    {
        public static string environment;
        public static string baseurl;
        public static string clientname;
        public static string browser;
        public static string platform;
        public static string os;
        public static string username;
        public static string password;
        public static string reportpath;
        public static string webdriverhost;
        public static string webdriverport;
        public static string isregistered;
        public static string firstname;
        public static string lastname;
        public static string zipcode;
        public static string dob;
        public static string email;
        public static string actorid;
        public static string userid;
        public static string servicecycleid;
        public static string ssn;
        public static string popseg1;
		public static string groupid;
        private Log4net log = new Log4net(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.ToString());

        public void InitializeGlobalVariables()
        {
            string defaultbaseurl = ConfigurationManager.AppSettings["baseurl"];
            string defaultenvironment = ConfigurationManager.AppSettings["environment"];
            string defaultclientname = ConfigurationManager.AppSettings["clientname"];
            string defaultbrowser = ConfigurationManager.AppSettings["browser"];
            string defaultplatform = ConfigurationManager.AppSettings["platform"];
            string defaultos = ConfigurationManager.AppSettings["os"];
            string defaultreportpath = ConfigurationManager.AppSettings["reportpath"];
            string defaultusername = ConfigurationManager.AppSettings["username"];
            string defaultpassword = ConfigurationManager.AppSettings["password"];
            string defaultisregistered = ConfigurationManager.AppSettings["isregistered"];
            string defaultwebdriverhost = ConfigurationManager.AppSettings["webdriverhost"];
            string defaultwebdriverport = ConfigurationManager.AppSettings["webdriverport"];
            string defaultfname = ConfigurationManager.AppSettings["firstname"];
            string defaultlname = ConfigurationManager.AppSettings["lastname"];
            string defaultdob = ConfigurationManager.AppSettings["dob"];
            string defaultzip = ConfigurationManager.AppSettings["zipcode"];
            string defaultemail = ConfigurationManager.AppSettings["email"];
            string defaultactorid = ConfigurationManager.AppSettings["actorid"];
            string defaultssn = ConfigurationManager.AppSettings["ssn"];
            string defaultntid = ConfigurationManager.AppSettings["popseg1"];


            GlobalVariables.baseurl = TestContext.Parameters.Get("baseurl", defaultbaseurl);
            GlobalVariables.environment = TestContext.Parameters.Get("env", defaultenvironment);
            GlobalVariables.clientname = TestContext.Parameters.Get("clientname", defaultclientname);
            GlobalVariables.browser = TestContext.Parameters.Get("browser", defaultbrowser);
            GlobalVariables.platform = TestContext.Parameters.Get("platform", defaultplatform);
            GlobalVariables.os = TestContext.Parameters.Get("os", defaultos);
            GlobalVariables.reportpath = TestContext.Parameters.Get("reportpath", defaultreportpath);
            GlobalVariables.reportpath = GlobalVariables.reportpath + "\\BuildSanityReport\\";
            GlobalVariables.username = TestContext.Parameters.Get("username", defaultusername);
            GlobalVariables.password = TestContext.Parameters.Get("password", defaultpassword);
            GlobalVariables.isregistered = TestContext.Parameters.Get("isregistered", defaultisregistered);
            log.Info("Report PathBase : " + GlobalVariables.reportpath);
            GlobalVariables.webdriverhost = TestContext.Parameters.Get("webdriverhost", defaultwebdriverhost);
            GlobalVariables.webdriverport = TestContext.Parameters.Get("webdriverport", defaultwebdriverport);
            GlobalVariables.firstname = TestContext.Parameters.Get("firstname", defaultfname);
            GlobalVariables.lastname = TestContext.Parameters.Get("lastname", defaultlname);
            GlobalVariables.dob = TestContext.Parameters.Get("dob", defaultdob);
            GlobalVariables.zipcode = TestContext.Parameters.Get("zipcode", defaultzip);
            GlobalVariables.email = TestContext.Parameters.Get("email", defaultemail);
            GlobalVariables.actorid = TestContext.Parameters.Get("actorid", defaultactorid);
            GlobalVariables.ssn = TestContext.Parameters.Get("ssn", defaultssn);
            GlobalVariables.popseg1 = TestContext.Parameters.Get("popseg1", defaultntid);
        }
    }
}
