using AutomationFramework.Pages;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomationFramework.Framework
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    class ReadConfigAttribute : Attribute//, ITestAction, IApplyToContext (These are commented to remove build error)
    {
        //public ActionTargets Targets => throw new NotImplementedException();
        //public ActionTargets Targets { get; } = ActionTargets.Test;

        //string[] configName;

        //public ReadConfigAttribute(params string[] config)
        //{
        //    configName = config;
        //}
        //List<string[]> clientconfig;

        //public ActionTargets Targets => throw new NotImplementedException();

        //public void BeforeTest(ITest test)
        //{
            
        //}

        //public void AfterTest(ITest test)
        //{
            
        //}


        //public void ApplyToContext(TestExecutionContext context)
        //{
        //    string clientName = GlobalVariables.clientname.ToLower();
        //    clientconfig = CSVReaderDataTable.GetCSVData("ClientConfig", clientName, configName);

        //    var status = clientconfig.ElementAt(0)[1];
        //    if (status.ToLower().Equals("false"))
        //    {
        //        Assert.Ignore("Feature not available for client");
        //    }
        //}
    }
}
