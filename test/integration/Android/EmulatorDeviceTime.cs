using System;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using static System.String;

namespace Appium.Net.Integration.Tests.Android
{
    class EmulatorDeviceTime
    {
        private AppiumDriver<AndroidElement> _driver;

        [SetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver<AndroidElement>(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [TearDown]
        public void AfterEach()
        {
            _driver?.Quit();
            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test]
        public void DeviceTimeTest()
        {
            var time = _driver.DeviceTime;
            Assert.Multiple(() =>
            {
                Assert.NotNull(time);
                Assert.AreNotEqual(Empty, time);
                Console.WriteLine(time);
                Assert.NotNull(DateTime.Parse(time));
            });
        }
    }
}