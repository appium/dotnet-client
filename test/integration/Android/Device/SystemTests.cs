using System;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android.Device
{
    internal class SystemTests
    {
        private AppiumDriver _driver;
        private AppiumOptions _androidOptions;

        [OneTimeSetUp]
        public void SetUp()
        {
            _androidOptions = Caps.GetAndroidUIAutomatorCaps(Apps.Get(Apps.androidApiDemos));
            _driver = new AndroidDriver(
                Env.ServerIsLocal() ? AppiumServers.LocalServiceUri : AppiumServers.RemoteServerUri,
                _androidOptions);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver.Dispose();
        }

        [Test]
        public void CanGetSystemBarInfoTest()
        {
            var androidDriver = (AndroidDriver) _driver;
            Assert.That(androidDriver.GetSystemBars().Count, Is.EqualTo(2));
        }

        [Test]
        public void CanGetDisplayDensityTest()
        {
            var androidDriver = (AndroidDriver) _driver;
            Assert.That(androidDriver.GetDisplayDensity(), Is.Not.EqualTo(0));
        }

        [Test]
        public void CanOpenNotificationsTest()
        {
            var androidDriver = (AndroidDriver)_driver;
            androidDriver.OpenNotifications();
        }
    }
}