using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Framework
{
    public class ElementLocator

    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// This method returns the Object of By class
        /// </summary>
        /// <param name="pageNodeName">Page Name present in or.xml</param>
        /// <param name="locatorName">Unique name of every element in or.xml</param>
        /// <returns></returns>
        public static By GetLocator(String pageNodeName, String locatorName,params string[] values)
        {
            By by = null;
            
            string[] objRepo = XmlParser.GetObjectRepo(pageNodeName, locatorName);

            string locatortype = objRepo[0];
            string locatorvalue = objRepo[1];

            //log.Info("Length: "+values.Length);
            if (values.Length>0)
            {
                string variablename = "$varinput";
                for(int i=0; i<values.Length; i++)
                {
                    //variablename = variablename + (i + 1);
                    locatorvalue = locatorvalue.Replace(variablename + (i + 1), values[i]);
                    //log.Info("Locator value after replacing " + variablename + (i + 1) + " : " + locatorvalue);
                }
                Console.WriteLine("Locator value after replacing  : " + locatorvalue);
                log.Info("Locator value after replacing  : " + locatorvalue);
            }
            by = GetBy(locatortype, locatorvalue);
            return by;
        }

        //public static By GetLocator(String pageNodeName, String locatorName,string value)
        //{
        //    By by = null;
        //    string[] objRepo = XmlParser.GetObjectRepo(pageNodeName, locatorName);

        //    string locatortype = objRepo[0];
        //    string locatorvalue = objRepo[1];

            
        //    by = GetBy(locatortype, locatorvalue);
        //    return by;
        //}
            
        public static By GetBy(string locatortype, string locatorvalue)
        {
            By by = null;

            switch (locatortype.ToLower())
            {
                case "id":
                    by = By.Id(locatorvalue);
                    break;
                case "name":
                    by = By.Name(locatorvalue);
                    break;
                case "classname":
                    by = By.ClassName(locatorvalue);
                    break;
                case "css":
                    by = By.CssSelector(locatorvalue);
                    break;
                case "tagname":
                    by = By.TagName(locatorvalue);
                    break;
                case "linktext":
                    by = By.LinkText(locatorvalue);
                    break;
                case "xpath":
                    by = By.XPath(locatorvalue);
                    break;
            }
            return by;
        }
        /// <summary>
        /// Identify element form OR base on the image stored
        /// use in Sikuli keywords
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="locatorName"></param>
        /// <returns></returns>
        public static string GetElementforSikkli(string pageName, string locatorName)
        {
            string[] objRepo = XmlParser.GetObjectRepo(pageName, locatorName);

            string locatortype = objRepo[0];
            string locatorvalue = objRepo[1];
            log.Info("elemet" +objRepo[1]);
                string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
                string projectPath = new Uri(actualPath).LocalPath;
                string element = projectPath + objRepo[1];
                log.Info("Element " + locatorName + "is available at" + element);
            return element;
         }

    }

}
