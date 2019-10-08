using AutomationFramework.Framework;
using AutomationFramework.Pages;
using AutomationFramework.Pages.WebPages.DevicesAndApps;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AutomationFramework.Tests.WebTests.Devices
{
    [TestFixture]
    [Order(36)]
    public class MyNutritrion:Base
    {
        String pageName;
        int points;
        Common cmn = new Common();
        string incentiveEnabled;
        string fitnessEnabled;
        string nutritionEnabled;
        public MyNutritrion()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        /// <summary>
        /// Verify that Device page is loaded
        /// </summary>
        [Test, Order(1)]
        //[Category("ProdSanity")]
        //[Category("Regression")]
        public void TC_VerifyMyNuteritionPages()
        {
            nutritionEnabled = cmn.GetConfig("NutritionApp").ElementAt(0)[1].ToLower();    
            if (nutritionEnabled.Equals("false"))
            {
                Assert.Ignore("Nutrition Apps not available for Client");
            }
            //To call the Page Login Method
            Page_Login plogin = new Page_Login();
            plogin.Login();
            Page_HAPrompt haprompt = new Page_HAPrompt();
            haprompt.GoToDashboard();
            //points = cmn.GetPoints();
            Common devices = new Common();
            devices.ClickDevicesAndApps();
           
            Page_DevicesAndApps pdevices = new Page_DevicesAndApps();
            if ((GlobalVariables.clientname == "NUCOR") || (GlobalVariables.clientname == "Dollar General"))
            {
                string expected = "Start Tracking Your Nutrition Today";
                string actual = pdevices.NavigateToMyNuterition();
                Assert.AreEqual(expected, actual);
            }
            else
            {
                string expected = "Start Tracking Your Activity Today";
                string actual = pdevices.NavigateToManageDevices();
                Assert.AreEqual(expected, actual);
            }

        }
        /// <summary>
        /// Search a nuterition device uing serarch filter
        /// </summary>
        [Test, Order(2)]
        //[Category("ProdSanity")]
        //[Category("Regression")]
        public void TC_VerifySearchFilter()
        {
            //To call the Page Login Method
            //Login lgn = new Login();
            //lgn.TC_VerifyLogin();
            //Page_HAPrompt haprompt = new Page_HAPrompt();
            if ((GlobalVariables.clientname == "NUCOR") || (GlobalVariables.clientname == "Dollar General"))
            {
                Common devices = new Common();
                devices.ClickDevicesAndApps();
                Page_DevicesAndApps pdevices = new Page_DevicesAndApps();
                pdevices.NavigateToMyNuterition();
                Boolean result = pdevices.VerifyDeviceFileter("devicesfilter", "Fitbit");
                is_soft_assert = false;
                Assert.IsTrue(result);
            }
        }

        
        
        /// <summary>
        /// Verify No of device connected befor connect device
        /// </summary>
        [Test, Order(3)]
        //[Category("ProdSanity")]
        //[Category("Regression")]
        public void TC_VerifyDeviceCountBeforeConnect()
        {
            if (nutritionEnabled.Equals("false"))
            {
                Assert.Ignore("Nutrition Apps not available for Client");
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
        //[Category("ProdSanity")]
        //[Category("Regression")]
        public void TC_VerifyListOfNutritionMyNutritrion()
        {
            if (nutritionEnabled.Equals("false"))
            {
                Assert.Ignore("Nutrition Apps not available for Client");
            }
            //To call the Page Login Method
            //Login lgn = new Login();
            //lgn.TC_VerifyLogin();
            //Page_HAPrompt haprompt = new Page_HAPrompt();
            Common devices = new Common();
            devices.ClickDevicesAndApps();
            Page_DevicesAndApps pdevices = new Page_DevicesAndApps(Base.softassertions);
            pdevices.NavigateToMyNuterition();
            List<String[]> devicename = CSVReaderDataTable.GetCSVData("DeviceAppContent", "DevicesAndApps", "nutritiondevicename");
            //pdevices.ClickOnNutritionCheckBox();
            for (int i = 0; i < devicename.Count; i++)
            {
                pdevices.VerifyNutritionDevicesFromFilter(devicename.ElementAt(i)[4]);
            }
            is_soft_assert = true;
            softassertions.AssertAll();

        }/*
        
        
        /// <summary>
        /// Verify Login page of all devicess 
        /// </summary>
        [Test, Order(7)]
        //[Category("ProdSanity")]
        [Category("Regression")]
        public void TC_VerifyLoginPageOfDevices()
        {
            //Login lgn = new Login();
            //lgn.TC_VerifyLogin();
            //Page_HAPrompt haprompt = new Page_HAPrompt();
            Common devices = new Common();
            devices.ClickMyNutritrion();
            Page_DevicesAndApps pdevices = new Page_DevicesAndApps(Base.softassertions);
            pdevices.NavigateToManageDevices();
            List<String[]> devicename = CSVReaderDataTable.GetCSVData("DeviceAppContent", pageName, "devicename");
            // pdevices.ClickOnFitnessCheckBox();
            pdevices.VerifyLoginPageAllDevices(devicename);
            is_soft_assert = true;
            softassertions.AssertAll();
        }

        /// <summary>
        /// Connect the fitbit device
        /// </summary>
        [Test,Order(8)]
        //[Category("ProdSanity")]
        [Category("Regression")]
        public void TC_VerifyConnectFitbitDevice()
        {
            //To call the Page Login Method
            //Login lgn = new Login();
            //lgn.TC_VerifyLogin();
            //Page_HAPrompt haprompt = new Page_HAPrompt();

            Common devices = new Common();
            devices.ClickMyNutritrion();
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
        //[Category("ProdSanity")]
        [Category("Regression")]
        public void TC_VerifyDeviceCountAfterDeviceConnected()
        {
            //To call the Page Login Method
            //Login lgn = new Login();
            //lgn.TC_VerifyLogin();
            //Page_HAPrompt haprompt = new Page_HAPrompt();

            Page_DevicesAndApps pdevices = new Page_DevicesAndApps();
            Boolean result = pdevices.verifyDeviceCount("1");
            is_soft_assert = false;
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Refresh the Connected device
        /// </summary>
        [Test, Order(10)]
        //[Category("ProdSanity")]
        [Category("Regression")]
        public void TC_VerifyRefreshConnection()
        {
            //To call the Page Login Method
            //Login lgn = new Login();
            //lgn.TC_VerifyLogin();
            //Page_HAPrompt haprompt = new Page_HAPrompt();
            Common devices = new Common();
            devices.ClickMyNutritrion();
            Page_DevicesAndApps pdevices = new Page_DevicesAndApps();
            pdevices.NavigateToManageDevices();
            
            Boolean result = pdevices.VerifyRefreshConnection();
            is_soft_assert = false;
            Assert.IsTrue(result);
        }
        
        /// <summary>
        /// Disconnect the connected device
        /// </summary>
        [Test, Order(11)]
        //[Category("ProdSanity")]
        [Category("Regression")]
        public void TC_VerifyDisConnectDevice()
        {
            //To call the Page Login Method
            //Login lgn = new Login();
            //lgn.TC_VerifyLogin();
            //Page_HAPrompt haprompt = new Page_HAPrompt();
            //Common devices = new Common();
            //devices.ClickMyNutritrion();

            Page_DevicesAndApps pdevices = new Page_DevicesAndApps(Base.softassertions);
            //pdevices.NavigateToManageDevices();
            is_soft_assert = true;
            pdevices.VerifyDisConnectionDevice();
            softassertions.AssertAll();            
        }

        /// <summary>
        /// Verify incentives awarded after device connection
        /// </summary>
        [Test, Order(12)]
        //[Category("ProdSanity")]
        [Category("Regression")]
        public void TC_ValidatePoints()
        {
            Common config = new Common();
            string isenabled = config.GetConfig("IncentiveEnabled").ElementAt(0)[1].ToLower();
            if (isenabled.Equals("false"))
            {
                Assert.Ignore("Incentives not enabled for client");
            }
            Common c = new Common();
            c.ClickFooterDashboardLink();
            int awardedpoints = cmn.GetPoints();
            Assert.AreEqual(points + 5, awardedpoints);
            is_soft_assert = false;
            cmn.LogOut();
        }*/

    }


    
}
