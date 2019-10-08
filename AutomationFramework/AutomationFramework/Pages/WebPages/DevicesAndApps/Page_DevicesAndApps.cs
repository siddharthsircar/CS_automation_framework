using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.DevicesAndApps
{
    class Page_DevicesAndApps
    {
        String pageName;
        SoftAssertions softAssertion = null;
        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_DevicesAndApps()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        /// <summary>
        /// overload constructor for softassertion
        /// </summary>
        /// <param name="softAssertion"></param>
        public Page_DevicesAndApps(SoftAssertions softAssertion): this()
        {
            this.softAssertion = softAssertion;           
        }
        /// <summary>
        /// Vlick on manage device link form left menu
        /// </summary>
        private void ClickManageDevices()
        {
            SeleniumKeywords.Click(pageName, "managedevices");
        }
        private void ClickMyNuterition()
        {
            SeleniumKeywords.Click(pageName, "mynutritionlbl");
        }
        /// <summary>
        /// Click on manage device link form left menu
        /// </summary>
        public string NavigateToManageDevices()
        {
            //System.Threading.Thread.Sleep(10000);
            //ClickManageDevices();
            return(verifypageheader());
        }
        /// <summary>
        /// Click on My nutrition  link form left menu
        /// </summary>
        public string NavigateToMyNuterition()
        {
            //System.Threading.Thread.Sleep(10000);
            ClickMyNuterition();
            return (verifypageheader());
        }
        /// <summary>
        /// search a device from search filter 
        /// </summary>
        /// <param name="locatorname"></param>
        /// <param name="devicename"></param>
        public void SearchDevice(String locatorname, String devicename)
        {
            SeleniumKeywords.SetText(pageName, locatorname, devicename);
        }
        /// <summary>
        /// verify the fitbit device after search
        /// </summary>
        /// <returns></returns>
        public Boolean VerifyExistance()
        {
            return(SeleniumKeywords.IsElementPresent(pageName, "fitbit_device_lbl"));
        }

        public void AcceptTOS()
        {
            SeleniumKeywords.Click(pageName, "dashboard_addremovelink");
            SeleniumKeywords.Click(pageName, "devicetos_getstartedlink");
            SeleniumKeywords.Click(pageName, "devicetos_iacceptlink");
        }
        /// <summary>
        /// verify device existence by passing device name
        /// </summary>
        /// <param name="locatorname"></param>
        /// <param name="devicename"></param>
        /// <returns></returns>
        public Boolean VerifyDeviceFileter(String locatorname, String devicename)
        {
            SearchDevice(locatorname, devicename);
            Boolean result=VerifyExistance();
            return result;
        }
        /// <summary>
        /// click on connect device button
        /// </summary>
        /// <param name="devicename"></param>
            public void ClickOnDeviceConnectBtn(string devicename)
        {
            System.Threading.Thread.Sleep(10000);
            SeleniumKeywords.Click(pageName, "device_connect_lbl", devicename);
        }
        /// <summary>
        /// click on fitbit device connect button
        /// </summary>
        public void ClickOnConnectBtn()
        {
            System.Threading.Thread.Sleep(3000);
            SeleniumKeywords.Click(pageName,"fitbit_connect_btn");
        }
        /// <summary>
        /// enter creditioals of fitbit device
        /// </summary>
        private void EnterLoginDetails()
        {
            System.Threading.Thread.Sleep(10000);
            //SeleniumKeywords.Click(pageName, "fitbit_email_txt");
            SeleniumKeywords.SetText(pageName,"fitbit_email_txt", "viveksharma.matrix@gmail.com");
            SeleniumKeywords.SetText(pageName,"fitbit_pass_txt", "Password1");
        }
        /// <summary>
        /// click on login button on fitbit login page
        /// </summary>
        private void ClickOnLoginBtn()
        {
            SeleniumKeywords.Click(pageName, "fitbit_login_btn");
        }
        /// <summary>
        /// click on Allow all checkbox on fit bit site after entering login and password 
        /// </summary>
        private void clickOnAllowAllChkBox()
        {
            SeleniumKeywords.Click(pageName, "fitbit_selectall_chkbox");
        }
        /// <summary>
        /// click on allow btn on fitbit site
        /// </summary>
        private void clickOnAllowBtn()
        {
            SeleniumKeywords.Click(pageName, "fitbit_allow_btn");
        }/// <summary>
        /// 
        /// verify the connected device status as "Connected"
        /// </summary>
        /// <returns></returns>
        private Boolean VerifyDeviceConnected()
        {
            return(SeleniumKeywords.IsElementPresent(pageName, "fitbit_connected_lbl"));
        }
        /// <summary>
        /// Verify Connect btn on fitbit tile
        /// </summary>
        /// <returns></returns>
        private Boolean VerifyDeviceConnectLbl()
        {
            return (SeleniumKeywords.IsElementPresent(pageName, "fitbit_connect_btn"));
        }/// <summary>
        /// Verify device count on gray bar
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public Boolean verifyDeviceCount(string count)
        {
          return(SeleniumKeywords.IsElementPresent("Common", "devicecount", count));
        }/// <summary>
        /// click on metaball present on connected fitbit tile 
        /// </summary>
        public void ClickOnDeviceMetaball()
        {
            SeleniumKeywords.Click(pageName, "device_elipsesball_btn");
        }

        /// <summary>
        /// Verify Connect fitbit device feature
        /// </summary>
        /// <returns></returns>
        public Boolean VerifyDeviceConnect()
        {
            
            ClickOnConnectBtn();
            EnterLoginDetails();
            ClickOnLoginBtn();
            clickOnAllowAllChkBox();
            clickOnAllowBtn();
            Boolean result=VerifyDeviceConnected();
            return (result);

        }/// <summary>
        /// verify popup message on click on leave btn after disconnect device
        /// </summary>
        public void VerifyPopUpMsg()
        {
            System.Threading.Thread.Sleep(7000);
            List<string[]> reportdata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "popuptxt");
            string elementlocatorname = reportdata.ElementAt(0)[3];
            string expreportdata = reportdata.ElementAt(0)[4];
            string actualreportdata = SeleniumKeywords.GetText(pageName, elementlocatorname);
            softAssertion.Add("Popup msg", expreportdata, actualreportdata, "equals");
            Console.WriteLine("Expected msg : " + expreportdata);
            Console.WriteLine("Actual msg : " + actualreportdata);
        }/// <summary>
        /// Disconnect the connected device
        /// </summary>
        private void DisConnectDevice()
        {
            SeleniumKeywords.Click(pageName, "device_disconnect_lnk");
            VerifyPopUpMsg();
            SeleniumKeywords.Click(pageName, "device_disconnectyes_btn");
        }/// <summary>
        /// 
        /// refresh the connected device
        /// </summary>
        /// <returns></returns>
        public Boolean VerifyRefreshConnection()
        {
            ClickOnDeviceMetaball();
            SeleniumKeywords.Click(pageName, "device_Refresh_Connection_lnk");
            Boolean result = VerifyDeviceConnected();
            return (result);
        }
        /// <summary>
        /// Click on device filter drop down
        /// </summary>
        /// <returns></returns>
        public void ClickOnFilterDropDown()
        {
            SeleniumKeywords.Click(pageName, "filterdevice");
        }
        /// <summary>
        /// Click on View All check box form filter
        /// </summary>
        public void ClickOnViewAllCheckBox()

        {
            ClickOnFilterDropDown();
            SeleniumKeywords.Click(pageName, "filteroption_viewAll");
        }
        /// <summary>
        /// Click on fitness check box form filter
        /// </summary>
        public void ClickOnFitnessCheckBox()
        {
            ClickOnFilterDropDown();
            SeleniumKeywords.Click(pageName, "filteroption_fitness");
        }
        /// <summary>
        /// Click on nutrition check box form filter
        /// </summary>
        public void ClickOnNutritionCheckBox()
        {
            ClickOnFilterDropDown();
            SeleniumKeywords.Click(pageName, "filteroption_nutrition");
        }
        /// <summary>
        /// Click on weight check box form filter
        /// </summary>
        public void ClickOnWeightCheckBox()
        {
            ClickOnFilterDropDown();
            SeleniumKeywords.Click(pageName, "filteroption_weight");
        }
        /// <summary>
        /// Click on next page btn 
        /// </summary>
        public void ClickOnNextPage()
        {
           SeleniumKeywords.Click(pageName, "nextpage_btn");
        }
        ///
        /// <summary>
        /// method is use to verify the existence of device and app base on the name
        /// </summary>
        /// <param name="devicename"></param>
        /// <returns></returns>
        private Boolean VerifyDeviceAvailable(string devicename)
        {
           return(SeleniumKeywords.IsElementPresent(pageName, "devices_name", devicename));
        }/// <summary>
        /// call in disconnected device test case
        /// </summary>
        /// <returns></returns>
        public Boolean VerifyDisConnection()
        {
            ClickOnDeviceMetaball();
            DisConnectDevice();

            Boolean result = VerifyDeviceConnectLbl();
            return (result);
        }
        /// <summary>
        /// get title of all device login page
        /// </summary>
        /// <returns></returns>
        private string GetTitleofLoginPage()
        {
            return(SeleniumKeywords.GetPageTitle());
        }

        /// <summary>
        /// call in disconnect device test case
        /// </summary>
        public void VerifyDisConnectionDevice()
        {
            Boolean result = VerifyDisConnection();            
            softAssertion.Add("Device Disconnected", result, true, "equals");
            result = verifyDeviceCount("0");
            softAssertion.Add("Device Count 0", result, true, "equals");
            SeleniumKeywords.RefreshPage();
        }

        public void VerifyNutritionDevicesFromFilter(string devicename)
        {            
            Boolean result = VerifyDeviceAvailable(devicename);
            String msg = "Nutrition Device : " + devicename;
            softAssertion.Add(msg, result, true, "equals");
        }
        public void VerifyWeightDevicesFromFilter(string devicename)
        {
            Boolean result = VerifyDeviceAvailable(devicename);
            String msg = "Weight Device : " + devicename;
            softAssertion.Add(msg,true, result, "equals");
        }
        public void VerifyFitnessDevicesFromFilter(string devicename)
        {
            Boolean result = VerifyDeviceAvailable(devicename);
            String msg = "Fitness Device : " + devicename;
            softAssertion.Add(msg,true,result, "equals");
        }

        public void VerifyLoginPageAllDevices(List<String[]> deviceDetails)
        {
            for (int i = 0; i < deviceDetails.Count; i++)
            {
                //if (i >= 6)
                //{
                //    ClickOnNextPage();
                //}
                string devicetitle = deviceDetails.ElementAt(i)[2];
                string devicename = deviceDetails.ElementAt(i)[4];
                //string url= 
                ClickOnDeviceConnectBtn(devicename);
                //System.Threading.Thread.Sleep(2000);
                string result = GetTitleofLoginPage();
                Console.WriteLine("**********Page title:  " + result);
                softAssertion.Add("Login Page Title", devicetitle, result, "contains");
                string env = ConfigurationManager.AppSettings["environment"];
                SeleniumKeywords.NavigateToPreviousPage();
                //SeleniumKeywords.NavigateToUrl("https://" + env + "-member.onlifehealth.com/BioDevices/Management");  
                if (i >= 6 && i < deviceDetails.Count)
                {
                    ClickOnNextPage();
                }
            }
        }
        private string verifypageheader()
        {
            string result = SeleniumKeywords.GetText(pageName, "devicepagesubheader");
            return result;
        }


    }


}
