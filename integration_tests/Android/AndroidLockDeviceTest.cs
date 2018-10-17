using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using System;

namespace Appium.Integration.Tests.Android
{
    class AndroidLockDeviceTest
    {
        private AndroidDriver<AndroidElement> driver;

        [SetUp]
        public void BeforeAll()
        {
            AppiumOptions capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidCaps(Apps.get("androidApiDemos"))
                : Caps.GetAndroidCaps(Apps.get("androidApiDemos"));
            if (Env.ServerIsRemote())
            {
                capabilities.AddAdditionalCapability("username", Env.GetEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.GetEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("name", "android - complex");
                capabilities.AddAdditionalCapability("tags", new string[] {"sample"});
            }
            Uri serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            driver = new AndroidDriver<AndroidElement>(serverUri, capabilities, Env.InitTimeoutSec);
            driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [TearDown]
        public void AfterEach()
        {
            if (driver != null)
            {
                driver.Quit();
            }
            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test()]
        public void LockTest()
        {
            driver.Lock();
            Assert.AreEqual(true, driver.IsLocked());
            driver.Unlock();
            Assert.AreEqual(false, driver.IsLocked());
        }
    }
}