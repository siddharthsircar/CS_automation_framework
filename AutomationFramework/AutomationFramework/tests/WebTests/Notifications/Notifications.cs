using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages.Notifications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests.WebTests.Notifications
{
    [TestFixture]
    [Order(46)]
    public class Notifications : Base
    {
        /// <summary>
        /// 
        /// </summary>
        [Test,Order(1)]
        [Category("ProdSanity")]
        public void TC_GoToNotificationPage()
        {
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            System.Threading.Thread.Sleep(8000);
            Page_Notifications pn = new Page_Notifications();
            pn.ClickViewAllLink();
        }

        /// <summary>
        /// 
        /// </summary>
        [Test,Order(2)]
        [Category("ProdSanity")]
        public void TC_VerifyNotification()
        {
            Page_Notifications pn = new Page_Notifications();
            List<string[]> result = pn.VerifyAllNotifications();
            Assert.Multiple(() =>
            {
                for (int i = 0; i < result.Count; i++)
                {
                    bool textmatch = Convert.ToBoolean(result.ElementAt(i)[1]);
                    string msg = result.ElementAt(i)[0];
                    Assert.IsTrue(textmatch, msg);

                }
            }
           );
            Common logout = new Common();
            logout.LogOut();
        }
    }
}
