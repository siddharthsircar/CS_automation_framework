using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages;
using AutomationFramework.Pages.WebPages.DevicesAndApps;
using AutomationFramework.Pages.WebPages.Incentive;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationFramework.Tests.WebTests.Devices
{ 
    [TestFixture]
    [Order(36)]
    public class DevicesAndApps:Base
    {
        String pageName;
        int points;
        string clientname;
        Common cmn = new Common();
        string incentiveEnabled;
        string fitnessEnabled;
        string nutritionEnabled;
        public DevicesAndApps()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }

        /// <summary>
        /// Verify that Device page is loaded
        /// </summary>
        [Test, Order(1)]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyDevices()
        {
            List<string[]> isEnabled = cmn.GetConfig("FitnessDevice", "IncentiveEnabled", "NutritionApp");
            fitnessEnabled = isEnabled.ElementAt(0)[1].ToLower();
            incentiveEnabled = isEnabled.ElementAt(0)[2].ToLower();
            nutritionEnabled = isEnabled.ElementAt(0)[3].ToLower();      
            clientname = GlobalVariables.clientname.ToLower();

            if(fitnessEnabled.Equals("false") && nutritionEnabled.Equals("false"))
            {
                Assert.Ignore("Device and Apps not available for Client");
            }
            //To call the Page Login Method
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            
            if(incentiveEnabled.Equals("true"))
            {
                points = cmn.GetPoints(clientname);
            }
            Page_DevicesAndApps pdevices = new Page_DevicesAndApps();
            //pdevices.AcceptTOS();
            Common devices = new Common();
            //devices.ClickDevicesAndApps();
            string expected = "Start Tracking Your Activity Today";
            
            if (nutritionEnabled.Equals("true") && fitnessEnabled.Equals("false"))
            {
                expected = "Start Tracking Your Nutrition Today";
            }
            devices.ClickDevicesAndApps();
            string actual = pdevices.NavigateToManageDevices();
            Assert.AreEqual(expected, actual);            
        }
        /// <summary>
        /// Search a device uing serarch filter
        /// </summary>
        [Test, Order(2)]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifySearchFilter()
        {
            List<string[]> isEnabled = cmn.GetConfig("FitnessDevice", "IncentiveEnabled", "NutritionApp");
            fitnessEnabled = isEnabled.ElementAt(0)[1].ToLower();
            incentiveEnabled = isEnabled.ElementAt(0)[2].ToLower();
            nutritionEnabled = isEnabled.ElementAt(0)[3].ToLower();
            if (fitnessEnabled.Equals("false") && nutritionEnabled.Equals("false"))
            {
                Assert.Ignore("Device and Apps not available for Client");
            }
            //To call the Page Login Method
            //Login lgn = new Login();
            //lgn.TC_VerifyLogin();
            //Page_HAPrompt haprompt = new Page_HAPrompt();

            Common devices = new Common();
            //devices.ClickDevicesAndApps();
            Page_DevicesAndApps pdevices = new Page_DevicesAndApps();
            //pdevices.NavigateToManageDevices();
            Boolean result=pdevices.VerifyDeviceFileter("devicesfilter", "Fitbit");
            is_soft_assert = false;
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Verify No of device connected befor connect device
        /// </summary>
        [Test, Order(3)]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyDeviceCountBeforeConnect()
        {
            List<string[]> isEnabled = cmn.GetConfig("FitnessDevice", "IncentiveEnabled", "NutritionApp");
            fitnessEnabled = isEnabled.ElementAt(0)[1].ToLower();
            incentiveEnabled = isEnabled.ElementAt(0)[2].ToLower();
            nutritionEnabled = isEnabled.ElementAt(0)[3].ToLower();
            if (fitnessEnabled.Equals("false") && nutritionEnabled.Equals("false"))
            {
                Assert.Ignore("Device and Apps not available for Client");
            }
            if (GlobalVariables.clientname.ToLower().Equals("medicare advantage"))
            {
                Assert.Ignore("Device count not displayed on Dashboard for Client");
            }
            //To call the Page Login Method
            //Login lgn = new Login();
            //lgn.TC_VerifyLogin();
            //Page_HAPrompt haprompt = new Page_HAPrompt();

            Page_DevicesAndApps pdevices = new Page_DevicesAndApps();
            Boolean result = pdevices.verifyDeviceCount("0");
            is_soft_assert = false;
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Verify list of all Nutrition devicess using filter
        /// </summary>
        [Test, Order(4)]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyListOfNutritionDevicesAndApps()
        {
            List<string[]> isEnabled = cmn.GetConfig("FitnessDevice", "IncentiveEnabled", "NutritionApp");
            fitnessEnabled = isEnabled.ElementAt(0)[1].ToLower();
            incentiveEnabled = isEnabled.ElementAt(0)[2].ToLower();
            nutritionEnabled = isEnabled.ElementAt(0)[3].ToLower();
            if (fitnessEnabled.Equals("false") && nutritionEnabled.Equals("false"))
            {
                Assert.Ignore("Device and Apps not available for Client");
            }
            else if (nutritionEnabled.Equals("false"))
            {
                Assert.Ignore("Nutrition Apps not available for Client");
            }

            //Common devices = new Common();
            //devices.ClickDevicesAndApps();
            Page_DevicesAndApps pdevices = new Page_DevicesAndApps(softassertions);
            //pdevices.NavigateToManageDevices();
            List<String[]> devicename = CSVReaderDataTable.GetCSVData("DeviceAppContent", pageName, "nutritiondevicename");

            if (nutritionEnabled.Equals("true") && fitnessEnabled.Equals("true"))
            {
                pdevices.ClickOnNutritionCheckBox();
            }
            
            for (int i = 0; i < devicename.Count; i++)
            {
                pdevices.VerifyNutritionDevicesFromFilter(devicename.ElementAt(i)[4]);
            }
            is_soft_assert = true;
            softassertions.AssertAll();

        }
        /// <summary>
        /// Verify list of all Weight devicess using filter
        /// </summary>
        [Test, Order(5)]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyListOfWeightDevicesAndApps()
        {
            List<string[]> isEnabled = cmn.GetConfig("FitnessDevice", "IncentiveEnabled", "NutritionApp");
            fitnessEnabled = isEnabled.ElementAt(0)[1].ToLower();
            incentiveEnabled = isEnabled.ElementAt(0)[2].ToLower();
            nutritionEnabled = isEnabled.ElementAt(0)[3].ToLower();
            if (fitnessEnabled.Equals("false") && nutritionEnabled.Equals("false"))
            {
                Assert.Ignore("Device and Apps not available for Client");
            }            
            if (GlobalVariables.clientname.ToLower().Equals("onlife health") || GlobalVariables.clientname.ToLower().Equals("health trust"))
            {
                //To call the Page Login Method
                //Login lgn = new Login();
                //lgn.TC_VerifyLogin();
                Page_HAPrompt haprompt = new Page_HAPrompt();
                Common devices = new Common();
                devices.ClickDevicesAndApps();
                Page_DevicesAndApps pdevices = new Page_DevicesAndApps(Base.softassertions);
                //pdevices.NavigateToManageDevices();
                List<String[]> devicename = CSVReaderDataTable.GetCSVData("DeviceAppContent", pageName, "weightdevicename");
                pdevices.ClickOnWeightCheckBox();
                for (int i = 0; i < devicename.Count; i++)
                {
                    pdevices.VerifyWeightDevicesFromFilter(devicename.ElementAt(i)[4]);
                }
                is_soft_assert = true;
                softassertions.AssertAll();
            }
            else
	        {
                Assert.Ignore("Test Case skipped as Weight Devices not available for client");
            }
        }
        /// <summary>
        /// Verify list of all Fitness devicess using filter
        /// </summary>
        [Test, Order(6)]
        [Category("ProdSanity")]
        [Category("AllClientReg")]
        [Category("Regression")]
        public void TC_VerifyListOfFitnessDevicesAndApps()
        {
            List<string[]> isEnabled = cmn.GetConfig("FitnessDevice", "IncentiveEnabled", "NutritionApp");
            fitnessEnabled = isEnabled.ElementAt(0)[1].ToLower();
            incentiveEnabled = isEnabled.ElementAt(0)[2].ToLower();
            nutritionEnabled = isEnabled.ElementAt(0)[3].ToLower();
            if (fitnessEnabled.Equals("false") && nutritionEnabled.Equals("false"))
            {
                Assert.Ignore("Device and Apps not available for Client");
            }
            else if (fitnessEnabled.Equals("false"))
            {
                Assert.Ignore("Fitness Devices not available for Client");
            }

            Common devices = new Common();
            devices.ClickDevicesAndApps();
            Page_DevicesAndApps pdevices = new Page_DevicesAndApps(softassertions);
            //pdevices.NavigateToManageDevices();
            List<String[]> devicename = CSVReaderDataTable.GetCSVData("DeviceAppContent", pageName, "fitnessdevicename");
            pdevices.ClickOnFitnessCheckBox();
            for (int i = 0; i < devicename.Count; i++)
            {
                
                    pdevices.VerifyFitnessDevicesFromFilter(devicename.ElementAt(i)[4]);
                    if (i == 8)
                    {
                        pdevices.ClickOnNextPage();
                    }
                
            }
            is_soft_assert = true;
            softassertions.AssertAll();

        }
        /// <summary>
        /// Verify Login page of all devicess 
        /// </summary>
        [Test, Order(7)]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyLoginPageOfDevices()
        {
            List<string[]> isEnabled = cmn.GetConfig("FitnessDevice", "IncentiveEnabled", "NutritionApp");
            fitnessEnabled = isEnabled.ElementAt(0)[1].ToLower();
            incentiveEnabled = isEnabled.ElementAt(0)[2].ToLower();
            nutritionEnabled = isEnabled.ElementAt(0)[3].ToLower();
            if (fitnessEnabled.Equals("false") && nutritionEnabled.Equals("false"))
            {
                Assert.Ignore("Device and Apps not available for Client");
            }
            else if (fitnessEnabled.Equals("false"))
            {
                Assert.Ignore("Fitness devices not available for Client");
            }

            Common devices = new Common();
            devices.ClickDevicesAndApps();
            Page_DevicesAndApps pdevices = new Page_DevicesAndApps(softassertions);
            //pdevices.NavigateToManageDevices();
            List<String[]> devicename = CSVReaderDataTable.GetCSVData("DeviceAppContent", pageName, "devicename");
            // pdevices.ClickOnFitnessCheckBox();
            Console.WriteLine("Device Name   *******" + devicename);
            pdevices.VerifyLoginPageAllDevices(devicename);
            is_soft_assert = true;
            softassertions.AssertAll();
        }

        /// <summary>
        /// Connect the fitbit device
        /// </summary>
        [Test,Order(8)]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyConnectFitbitDevice()
        {
            List<string[]> isEnabled = cmn.GetConfig("FitnessDevice", "IncentiveEnabled", "NutritionApp");
            fitnessEnabled = isEnabled.ElementAt(0)[1].ToLower();
            incentiveEnabled = isEnabled.ElementAt(0)[2].ToLower();
            nutritionEnabled = isEnabled.ElementAt(0)[3].ToLower();

            if (fitnessEnabled.Equals("false") && nutritionEnabled.Equals("false"))
            {
                Assert.Ignore("Device and Apps not available for Client");
            }
            Common devices = new Common();
            devices.ClickDevicesAndApps();
            Page_DevicesAndApps pdevices = new Page_DevicesAndApps();
            pdevices.NavigateToManageDevices();
            Boolean result = pdevices.VerifyDeviceConnect();
            is_soft_assert = false;
            Assert.IsTrue(result);
        }
        
        /// <summary>
        /// Verify No of device connected after connect device
        /// </summary>
        [Test, Order(9)]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyDeviceCountAfterDeviceConnected()
        {
            List<string[]> isEnabled = cmn.GetConfig("FitnessDevice", "IncentiveEnabled", "NutritionApp");
            fitnessEnabled = isEnabled.ElementAt(0)[1].ToLower();
            incentiveEnabled = isEnabled.ElementAt(0)[2].ToLower();
            nutritionEnabled = isEnabled.ElementAt(0)[3].ToLower();
            if (fitnessEnabled.Equals("false") && nutritionEnabled.Equals("false"))
            {
                Assert.Ignore("Device and Apps not available for Client");
            }
            if (GlobalVariables.clientname.ToLower().Equals("medicare advantage"))
            {
                Assert.Ignore("Device count not displayed on Dashboard for Client");
            }
            Page_DevicesAndApps pdevices = new Page_DevicesAndApps();
            Boolean result = pdevices.verifyDeviceCount("1");
            is_soft_assert = false;
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Refresh the Connected device
        /// </summary>
        [Test, Order(10)]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyRefreshConnection()
        {
            List<string[]> isEnabled = cmn.GetConfig("FitnessDevice", "IncentiveEnabled", "NutritionApp");
            fitnessEnabled = isEnabled.ElementAt(0)[1].ToLower();
            incentiveEnabled = isEnabled.ElementAt(0)[2].ToLower();
            nutritionEnabled = isEnabled.ElementAt(0)[3].ToLower();
            if (fitnessEnabled.Equals("false") && nutritionEnabled.Equals("false"))
            {
                Assert.Ignore("Device and Apps not available for Client");
            }

            //Common devices = new Common();
            //devices.ClickDevicesAndApps();
            Page_DevicesAndApps pdevices = new Page_DevicesAndApps();
            //pdevices.NavigateToManageDevices();

            Boolean result = pdevices.VerifyRefreshConnection();
            is_soft_assert = false;
            Assert.IsTrue(result);
        }
        
        /// <summary>
        /// Disconnect the connected device
        /// </summary>
        [Test, Order(11)]
        [Category("ProdSanity")]
        [Category("Regression")]
        [Category("AllClientReg")]
        public void TC_VerifyDisConnectDevice()
        {
            List<string[]> isEnabled = cmn.GetConfig("FitnessDevice", "IncentiveEnabled", "NutritionApp");
            fitnessEnabled = isEnabled.ElementAt(0)[1].ToLower();
            incentiveEnabled = isEnabled.ElementAt(0)[2].ToLower();
            nutritionEnabled = isEnabled.ElementAt(0)[3].ToLower();
            if (fitnessEnabled.Equals("false") && nutritionEnabled.Equals("false"))
            {
                Assert.Ignore("Device and Apps not available for Client");
            }
   ;
            //Common devices = new Common();
            //devices.ClickDevicesAndApps();

            Page_DevicesAndApps pdevices = new Page_DevicesAndApps(softassertions);
            //pdevices.NavigateToManageDevices();
            is_soft_assert = true;
            pdevices.VerifyDisConnectionDevice();
            softassertions.AssertAll();            
        }

        /// <summary>
        /// Disconnect the connected device
        /// </summary>
        //[Test, Order(12)]
        public void TC_VerifyDeviceFAQs()
        {
            //To call the Page Login Method
            //Login lgn = new Login();
            //lgn.TC_VerifyLogin();
            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //Common devices = new Common();
            //devices.ClickDevicesAndApps();

        }

        [Test, Order(13)]
        public void TC_ValidateDeviceAppsIncentiveHistory()
        {
            List<string[]> isEnabled = cmn.GetConfig("FitnessDevice", "IncentiveEnabled", "NutritionApp");
            fitnessEnabled = isEnabled.ElementAt(0)[1].ToLower();
            incentiveEnabled = isEnabled.ElementAt(0)[2].ToLower();
            nutritionEnabled = isEnabled.ElementAt(0)[3].ToLower();

            if (fitnessEnabled.Equals("false") && nutritionEnabled.Equals("false"))
            {
                Assert.Ignore("Device and Apps not available for Client");
            }
            if (incentiveEnabled.Equals("false"))
            {
                Assert.Ignore("Incentives not enabled for client");
            }

            string category = "I Connected My Device/App";
            List<string[]> incentivehistorydata = CSVReaderDataTable.GetCSVData("IncentiveHistoryData", pageName, category, GlobalVariables.clientname.ToLower());

            if (incentivehistorydata.Count > 0)
            {
                Page_EligibleActivities peligible = new Page_EligibleActivities(softassertions);
                is_soft_assert = true;

                CommonApi cma = new CommonApi();
                String token = cma.GetToken();
                peligible.InitializeIncentiveHistoryRequest();

                peligible.SetHeader(token);
                peligible.SetMethod();
                peligible.SendRequest();

                peligible.VerifyHistoryData(incentivehistorydata, category);
                softassertions.AssertAll();
            }
            else
            {
                Assert.Ignore("Incentives for Device and Apps is not available for Client");
            }
            
        }

        /// <summary>
        /// Verify incentives awarded after device connection
        /// </summary>
        [Test, Order(14)]
        [Category("ProdSanity")]
        [Category("Regression")]
        //[Category("AllClientReg")]
        [Category("PointReg")]
        
        public void TC_ValidatePoints()
        {
            List<string[]> isEnabled = cmn.GetConfig("FitnessDevice", "IncentiveEnabled", "NutritionApp");
            fitnessEnabled = isEnabled.ElementAt(0)[1].ToLower();
            incentiveEnabled = isEnabled.ElementAt(0)[2].ToLower();
            nutritionEnabled = isEnabled.ElementAt(0)[3].ToLower();
            if (fitnessEnabled.Equals("false") && nutritionEnabled.Equals("false"))
            {
                Assert.Ignore("Device and Apps not available for Client");
            }
            if (incentiveEnabled.Equals("false"))
            {
                Assert.Ignore("Incentives not enabled for client");
            }
            //if (GlobalVariables.clientname.ToLower().ToString().Equals("MEABT"))
            //{
            //    Assert.Ignore("Device Connection Incentive not awarded for MEABT");
            //}
            //else
            //{
                Common c = new Common();
                c.ClickFooterDashboardLink();
                int awardedpoints = cmn.GetPoints(clientname);
                int points_connectdevice = Convert.ToInt32(cmn.GetInstancePointsValue(clientname, "ConnectDevice"));
                int expectedtotalpoints = points + points_connectdevice;

                Console.WriteLine("Expected : " + expectedtotalpoints);
                Console.WriteLine("Awarded : " + awardedpoints);
                Assert.AreEqual(expectedtotalpoints, awardedpoints);
                is_soft_assert = false;
                cmn.LogOut();
            //}
        }

    }


    
}
