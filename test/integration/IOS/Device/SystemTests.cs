using System;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Net.Integration.Tests.IOS.Device
{
    internal class SystemTests
    {
        private IOSDriver iosDriver;

        [OneTimeSetUp]
        public void SetUp()
        {
            var capabilities = Caps.GetIosCaps(Apps.Get("iosUICatalogApp"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            iosDriver = new IOSDriver(serverUri, capabilities, Env.InitTimeoutSec);
            iosDriver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            iosDriver.Dispose();
        }

        [Test]
        public void CanGetSystemTimeTest()
        {
            var time = iosDriver.DeviceTime;
            Assert.That(iosDriver.DeviceTime, Is.Not.Empty);
            Assert.That(DateTime.Parse(time), Is.Not.EqualTo(DateTime.Now.AddDays(3)));
        }
    }
}
