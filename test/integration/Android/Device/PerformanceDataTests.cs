using System;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Android.Enums;

namespace Appium.Net.Integration.Tests.Android.Device
{
    internal class PerformanceDataTests
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
            _driver?.Dispose();
        }

        [Test]
        public void GetPerformanceDataTypesTest()
        {
            var androidDriver = _driver as AndroidDriver;
            Assert.IsNotNull(androidDriver.GetPerformanceDataTypes());
        }

        [Test]
        public void GetPerformanceDataTest()
        {
            var androidDriver = _driver as AndroidDriver;
            var packageName = androidDriver?.CurrentPackage;

            Assert.Multiple(() =>
            {
                Assert.That(androidDriver?.GetPerformanceData(packageName, PerformanceDataType.CpuInfo), Is.Not.Null.Or.Empty);
                Assert.That(androidDriver?.GetPerformanceData(packageName, PerformanceDataType.CpuInfo, 15),
                    Is.Not.Null.Or.Empty);
                Assert.That(androidDriver?.GetPerformanceData(packageName, PerformanceDataType.MemoryInfo, 5),
                    Is.Not.Null.Or.Empty);
                Assert.That(androidDriver?.GetPerformanceData(packageName, PerformanceDataType.BatteryInfo, 5),
                    Is.Not.Null.Or.Empty);
                Assert.That(androidDriver?.GetPerformanceData(packageName, PerformanceDataType.NetworkInfo, 5),
                    Is.Not.Null.Or.Empty);
            });
        } 
    }
}
