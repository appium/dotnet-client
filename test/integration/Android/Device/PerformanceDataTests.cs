using System;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android.Device
{
    internal class PerfomanceDataTests
    {
        private AppiumDriver<IWebElement> _driver;
        private AppiumOptions _androidOptions;


        [OneTimeSetUp]
        public void SetUp()
        {
            _androidOptions = Caps.GetAndroidUIAutomatorCaps(Apps.Get(Apps.androidApiDemos));
            _driver = new AndroidDriver<IWebElement>(
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
        public void GetSupportedPerformanceDataTypesTest()
        {
            var androidDriver = _driver as AndroidDriver<IWebElement>;
            Assert.IsNotNull(androidDriver.GetSupportedPerformanceDataTypes());
        }

        [Test]
        public void GetSupportedPerformanceData()
        {
            var androidDriver = _driver as AndroidDriver<IWebElement>;
            var packageName = androidDriver.CurrentPackage;
            Assert.Multiple(() =>
            {
                // Assert.DoesNotThrow(() => androidDriver.GetSupportPerformanceData(packageName, "cpuinfo", 5));
                Assert.That(androidDriver.GetSupportPerformanceData(packageName, "memoryinfo", 5),
                    Is.Not.Null.Or.Empty);
                Assert.That(androidDriver.GetSupportPerformanceData(packageName, "batteryinfo", 5),
                    Is.Not.Null.Or.Empty);
                Assert.That(androidDriver.GetSupportPerformanceData(packageName, "networkinfo", 5),
                    Is.Not.Null.Or.Empty);
            });
        }
    }
}
