using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.Journey
{
    class Page_MyJourney
    {
        String pageName;
        SoftAssertions softAssertions = null;

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_MyJourney()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_MyJourney(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }

        /// <summary>
        /// Method verified the new members journey recommendations
        /// </summary>
        /// <param name="journeydata"></param>
        public void VerifyNewMemberJourney(List<string[]> journeydata)
        {            
            for (int i = 0; i < (journeydata.Count); i++)
            {
                string elementname = journeydata.ElementAt(i)[2];
                string elementlocatorname = journeydata.ElementAt(i)[3];
                bool elementdisplayedstatus = Convert.ToBoolean(journeydata.ElementAt(i)[4]);
                string varvalue1 = journeydata.ElementAt(i)[5];
                string varvalue2 = journeydata.ElementAt(i)[6];
                Console.WriteLine("varvalue1 : " + varvalue1 + ", varvalue2 : " + varvalue2);
                bool elementpresent = SeleniumKeywords.IsElementPresent(pageName, elementlocatorname, varvalue1, varvalue2);
                softAssertions.Add(i + " Element : " + elementname, elementdisplayedstatus, elementpresent, "equals");           
            }
        }

    }
}
