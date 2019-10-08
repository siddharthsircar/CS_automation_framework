using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages
{
    class Page_CoachingLearnMore
    {
        String pageName;
         SoftAssertions softAssertion = null;
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_CoachingLearnMore()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_CoachingLearnMore(SoftAssertions softAssertion) : this()
        {
            this.softAssertion = softAssertion;
        }
        
        private Boolean RecommandedCoachingAfterHA(String Coachingname)
        {
            return(SeleniumKeywords.IsElementPresent(pageName, "coaching_recommended", Coachingname));
        }

        private Boolean NotRecommendedCoachingAfterHA(String Coachingname)
        {
            return (SeleniumKeywords.IsElementNotPresent(pageName, "coaching_recommended", Coachingname));
        }

        public void VerifyRecommandedCoaching(String Coachingname)
        {
            
           Boolean result= RecommandedCoachingAfterHA(Coachingname);
           softAssertion.Add("Recommended Coaching",true, result, "contains");
            


        }

        public void VerifyNotRecommandedCoaching(String Coachingname)
        {
            Boolean result = NotRecommendedCoachingAfterHA(Coachingname);
            softAssertion.Add("Not Recommended Coaching", false, result, "contains");
        }
    }
}
