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
        private AppiumDriver _driver;

        [SetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
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
                Assert.That(time, Is.Not.Null);
                Assert.That(time, Is.Not.EqualTo(Empty));
                Console.WriteLine(time);
                Assert.That(DateTime.Parse(time), Is.Not.EqualTo(DateTime.Now.AddDays(3)));
            });
        }
    }
}