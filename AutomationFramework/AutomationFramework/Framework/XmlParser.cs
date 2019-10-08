using System;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace AutomationFramework.Framework
{
    /// <summary>
    /// XML Parser util class
    /// </summary>
    public class XmlParser
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static ExtentTestManager Report = new ExtentTestManager();
        /// <summary>
        /// Method parses the Object Repo and fetches the Locator Type and Value based on search parameters
        /// </summary>
        /// <param name="pageNodeName"></param>
        /// <param name="locatorName"></param>
        /// <returns></returns>
        public static string[] GetObjectRepo(string pageNodeName, string locatorName)
        {
            string[] objRepo = new string[] { "", "" };
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            string repo_path = new Uri(actualPath).LocalPath;
            //log.Info("Repo Path : "+ repo_path);
            XmlDocument doc = new XmlDocument();
            if (ConfigurationManager.AppSettings["platform"].ToLower() == "win")
            {
                doc.Load(repo_path.ToString() + "resources/or/Or_Web.xml");
            }
            else if (ConfigurationManager.AppSettings["platform"].ToLower() == "mobile")
            {
                if (ConfigurationManager.AppSettings["os"].ToLower().Equals("ios"))
                {
                    doc.Load(repo_path.ToString() + "resources/or/Or_iOS.xml");
                }
                else if (ConfigurationManager.AppSettings["os"].ToLower().Equals("android"))
                {
                    doc.Load(repo_path.ToString() + "resources/or/Or_Android.xml");
                }
            }
            XmlNodeList obj = doc.SelectNodes("//PageNode[@name='" + pageNodeName + "']//Object[@locatorname='" + locatorName + "']");

            int totalmatchingnodes = obj.Count;
            //log.Info("Total Matching Nodes : "+totalmatchingnodes);

            if(totalmatchingnodes == 1)
            {
                XmlElement objElement = obj.Item(0) as XmlElement;
                objRepo[0] = objElement.GetAttribute("locatortype");
                objRepo[1] = objElement.GetAttribute("locatorvalue");
                log.Info("locatortype : " + objRepo[0] + " , locatorvalue : " + objRepo[1]);
            }
            else if (totalmatchingnodes == 0)
            {
                Report.Fail("No matching node with PageNode : " + pageNodeName + " and LocatorName : " + locatorName);
                log.Info("No matching node with PageNode : " + pageNodeName+ " and LocatorName : "+ locatorName);
            }
            else
            {
                Report.Fail("There are multiple matching nodes with PageNode : " + pageNodeName + " and LocatorName : " + locatorName);
                log.Info("There are multiple matching nodes with PageNode : " + pageNodeName + " and LocatorName : " + locatorName);
            }
            return objRepo;
        }

    }
}
