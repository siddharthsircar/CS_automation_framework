using AutomationFramework.Framework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Tests
{
    [TestFixture]
    //[ReadConfig("SSO")]
    public class TestClass1 : Base
    {
        [Test]
        public void TestMethod()
        {
            TestContext.WriteLine("Akkad Bakkad Bambay Bo");
            // TODO: Add your test code here
            Assert.Pass("Your first passing test");
        }
    }
}
