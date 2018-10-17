using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium.Appium.Service;

namespace Appium.Integration.Tests.iOS
{
    class iOSLockDeviceTest
    {
        private IOSDriver<IWebElement> driver;

        [SetUp]
        public void TestSetup()
        {
            AppiumOptions capabilities = Caps.GetIOSCaps(Apps.get("iosWebviewApp"));
            if (Env.ServerIsRemote())
            {
                capabilities.AddAdditionalCapability("username", Env.GetEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.GetEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("tags", new string[] {"sample"});
            }
            AppiumLocalService a = new AppiumServiceBuilder().UsingPort(4723).Build();

            a.Start();
            var isRunning = a.IsRunning;

            Uri serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : a.ServiceUrl;
            

            driver = new IOSDriver<IWebElement>(serverUri, capabilities, Env.InitTimeoutSec);
            driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [TearDown]
        public void Cleanup()
        {
            if(driver.IsLocked())
                driver.Unlock();
            if (driver != null)
            {
                driver.Quit();
            }
            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test]
        public void IsLockedTest()
        {
            Assert.AreEqual(driver.IsLocked(), false);
        }

        [Test]
        public void LockTest()
        {
            Assert.AreEqual(driver.IsLocked(),false);
            driver.Lock();
            Assert.AreEqual(driver.IsLocked(), true);
        }

        [Test]
        public void LockTestWithSeconds()
        {
            Assert.AreEqual(driver.IsLocked(), false);
            driver.Lock(5);
            Assert.AreEqual(driver.IsLocked(), false);
        }

        [Test]
        public void UnlockTest()
        {
            Assert.AreEqual(driver.IsLocked(), false);
            driver.Lock();
            Assert.AreEqual(driver.IsLocked(), true);
            driver.Unlock();
            Assert.AreEqual(driver.IsLocked(), false);
        }

    }
}
 