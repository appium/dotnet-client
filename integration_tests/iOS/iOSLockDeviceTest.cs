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

namespace Appium.Integration.Tests.iOS
{
    class iOSLockDeviceTest
    {
        private IOSDriver<IWebElement> driver;

        [SetUp]
        public void TestSetup()
        {
            AppiumOptions capabilities = Caps.getIos112Caps(Apps.get("iosWebviewApp"));
            if (Env.isSauce())
            {
                capabilities.AddAdditionalCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("tags", new string[] {"sample"});
            }
            Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIForIOS;
            

            driver = new IOSDriver<IWebElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            driver.Manage().Timeouts().ImplicitWait = Env.IMPLICIT_TIMEOUT_SEC;
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
            if (!Env.isSauce())
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
 