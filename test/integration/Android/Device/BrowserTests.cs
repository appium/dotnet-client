using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;

namespace Appium.Net.Integration.Tests.Android.Device.App
{
    internal class BrowserTests
    {
        private AppiumDriver _driver;
        private AppiumOptions _androidOptions;

        [OneTimeSetUp]
        public void SetUp()
        {
            _androidOptions = new AppiumOptions();
            _androidOptions.BrowserName = "Chrome";

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
        public void Browser()
        {
            _driver.Navigate().GoToUrl("https://github.com/appium");
            Assert.IsNotEmpty(_driver.PageSource);
        }
    }
}