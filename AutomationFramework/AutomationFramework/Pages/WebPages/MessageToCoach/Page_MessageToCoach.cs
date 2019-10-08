using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages
{
    class Page_MessageToCoach
    {
        string pageName;
        List<string[]> uielementdata = new List<string[]>();
        SoftAssertions softAssertion = null;

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_MessageToCoach()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current Class :" + pageName);
        }

        public Page_MessageToCoach(SoftAssertions softAssertion) : this()
        {
            this.softAssertion = softAssertion;
        }
        /// <summary>
        /// This method is use to verify the text of available control on UI
        /// </summary>
        /// <returns></returns>
        public void VerityMsgToCoachUIText()

        {
            string elementname="";
            string elementlocatorname="";
            string expelementtxt;
            string actualelementtxt;
            //read from CSV 
            uielementdata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "elementuitxt");
            Console.WriteLine("UI Element data from csv"+uielementdata);

            for (int i = 0; i < uielementdata.Count; i++)
            {
                elementname = uielementdata.ElementAt(i)[2];
                elementlocatorname = uielementdata.ElementAt(i)[3];
                expelementtxt = uielementdata.ElementAt(i)[4];
                //get the text of element passed form CSV
                actualelementtxt = SeleniumKeywords.GetText(pageName, elementlocatorname);
                //verify the actual and expected value
                softAssertion.Add("Element : " + elementname, expelementtxt, actualelementtxt, "equals");                
            }
        }
        /// <summary>
        /// This method is used to verify the Text Area and its placeholder value  
        /// </summary>
        /// <returns></returns>
        public void VerifyTxtAreaElement()
        {
            string elementname = "";
            string elementlocatorname = "";
            //Read from CSV value
            List<String[]> uielementattribute = CSVReaderDataTable.GetCSVData("AttributesContent", pageName, "elementuiattr");
            elementname = uielementattribute.ElementAt(0)[2];
            elementlocatorname = uielementattribute.ElementAt(0)[3];
            Console.WriteLine("Element name"+ elementname);
            //Verify the visibility of text area            
            bool elementexistence = SeleniumKeywords.IsElementPresent(pageName, elementlocatorname);
            softAssertion.Add("Element : " + elementname, true, elementexistence, "equals");            
        }
        /// <summary>
        /// Method is use to set the message to be send
        /// </summary>
        /// <param name="msg"></param>
        public void SetMessage(string msg)
        {
            SeleniumKeywords.SetText(pageName, "messageconvtextarea",msg);

        }
        /// <summary>
        /// Method is use to send message 
        /// </summary>
        public void ClickSend()
        {
            SeleniumKeywords.Click(pageName, "sendmsgtocoachbt");
        }
        /// <summary>
        /// Method is use to verify sent message
        /// </summary>
        /// <returns></returns>
        public string VerifySentMessage()
        {
            Console.WriteLine("Verify Message");
            //this call return the text sent to coach
            string result = SeleniumKeywords.GetText(pageName, "verifysentmsg");
            Console.WriteLine("the result:  " +result);
            return result;
        }
        /// <summary>
        /// Click on the Send option form drop down
        /// </summary>
        public void ClickFilterSend()
        {
            //Click on the Send option form drop down 
            SeleniumKeywords.Click(pageName, "sendmsg");
        }
        /// <summary>
        /// Click on the Active option form drop down 
        /// </summary>
        public void ClickFilterActive()
        {
            //Click on the Active option form drop down 
            SeleniumKeywords.Click(pageName, "activemsg");
        }
        /// <summary>
        /// Click on the Inbound option form drop down 
        /// </summary>
        public void ClickFilterInbound()
        {
            //Click on the Inbound option form drop down 
            SeleniumKeywords.Click(pageName, "inboundmsg");
        }
        /// <summary>
        /// Click on the Click on drop down 
        /// </summary>
        public void Clickoption()
        {
            //Click on drop down 
            SeleniumKeywords.Click(pageName, "msgfilterbtn");
        }
        /// <summary>
        /// This method is used to click on archive link
        /// </summary>
        public void ClickArchive()
        {
            SeleniumKeywords.Click(pageName, "archivelnk");

        }
        /// <summary>
        /// Click on Archive option form drop down
        /// </summary>
        public void ClickFilterArchive()
        {
            SeleniumKeywords.Click(pageName, "archivemsg");

        }/// <summary>
         /// Click on ALL option form drop down
         /// </summary>
        public void ClickFilterAll()
        {
            SeleniumKeywords.Click(pageName, "allmsg");

        }
        /// <summary>
        /// Click on Deleted option form drop down
        /// </summary>
        public void ClickFilterDelete()
        {
            SeleniumKeywords.Click(pageName, "deletemsg");

        }
        /// <summary>
        /// Click on Delete button to delete message
        /// </summary>
        public void ClickDeleteBtn()
        {
            SeleniumKeywords.Click(pageName, "deletebtn");
        }
        /// <summary>
         /// Method call by by test case TC_VerifyMessageToCoachUI to verify Message page UI
         /// </summary>
         /// <returns></returns>
        public void VerifyMessagetoCoachPage()
        {
            VerityMsgToCoachUIText();
            VerifyTxtAreaElement();
        }
        
        /// <summary>
        /// Metod is call by TC_VerifySentMsgToCoach to verify sent message 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string VerifyMsgToCoach(string message)
        {
            SetMessage(message);
            ClickSend();
            Console.WriteLine("Message sent");
            //string[]> result = new List<string[]>();
            String result=VerifySentMessage();
            //result.Add(new string[] {res});

            return result;
        }
        /// <summary>
        /// Method is use to get the Inbound default text
        /// </summary>
        /// <returns></returns>
        /// 
        public string VerifyInboundTxt()
        {

            Console.WriteLine("Verify Inbound Text");
            string result = SeleniumKeywords.GetText(pageName, "inboundtxt");
            Console.WriteLine("the result:  " + result);
            return result;
        }
        ///<summary>
        ///Verify sent message from all the option given in dropdown filter
        ///
        /// </summary>
        /// 
        public List<string[]> VerfySendMsgDropDown()
        {
            //string list result;
            List<string[]> result = new List<string[]>();
            //Verify Send from Send option of DropDown ;
            Clickoption();
            ClickFilterSend();
            Console.WriteLine("Verify msg in send filter");
            result.Add(new string[]{VerifySentMessage()});
            //Verify Send from Active option of DropDown ;
            Clickoption();
            ClickFilterActive();
            result.Add(new string[] { VerifySentMessage() });
            //Verify Send from Archive option of DropDown ;
            ClickArchive();
            Clickoption();
            ClickFilterArchive();
            result.Add(new string[] { VerifySentMessage() });
            //Verify Send from Delet option of DropDown ;
            ClickDeleteBtn();
            Clickoption();
            ClickFilterDelete();
            result.Add(new string[] { VerifySentMessage() });
            //Verify Send from All option of DropDown ;
            Clickoption();
            ClickFilterAll();
            result.Add(new string[] { VerifySentMessage() });
            //Verify the default Inbound msg "You have no inbound messages." from Inbound option of DropDown ;
            Clickoption();
            ClickFilterInbound();
            result.Add(new string[] { VerifyInboundTxt() });
            //verifySendfromDropDown(option, optmsg);
            //result.Add(new string[] { verifySentMessage() });
            return result;

        }

    }
}
