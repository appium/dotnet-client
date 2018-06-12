using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
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
            DesiredCapabilities capabilities = Env.isSauce()
                ? Caps.getAndroid501Caps(Apps.get("androidApiDemos"))
                : Caps.getAndroid19Caps(Apps.get("androidApiDemos"));
            if (Env.isSauce())
            {
                capabilities.SetCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
                capabilities.SetCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.SetCapability("name", "android - complex");
                capabilities.SetCapability("tags", new string[] {"sample"});
            }
            Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIAndroid;
            driver = new AndroidDriver<AndroidElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            driver.Manage().Timeouts().ImplicitWait = Env.IMPLICIT_TIMEOUT_SEC;
        }

        [TearDown]
        public void AfterEach()
        {
            if (driver != null)
            {
                driver.Quit();
            }
            if (!Env.isSauce())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test()]
        public void LockTest()
        {
            Assert.AreEqual(driver.IsDeviceLocked(), false);
            driver.LockDevice();
            Assert.AreEqual(driver.IsDeviceLocked(), true);
        }

        [Test]
        public void IsLockedTest()
        {
            Assert.AreEqual(driver.IsDeviceLocked(), false);
        }

        [Test]
        public void LockTestWithSeconds()
        {
            Assert.AreEqual(driver.IsDeviceLocked(), false);
            driver.LockDevice(5);
            Assert.AreEqual(driver.IsDeviceLocked(), false);
        }

        [Test]
        public void UnlockTest()
        {
            Assert.AreEqual(driver.IsDeviceLocked(), false);
            driver.LockDevice();
            Assert.AreEqual(driver.IsDeviceLocked(), true);
            driver.UnlockDevice();
            Assert.AreEqual(driver.IsDeviceLocked(), false);
        }
    }
}