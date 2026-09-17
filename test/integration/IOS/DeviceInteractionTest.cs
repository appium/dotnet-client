using System;
using System.Collections.Generic;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.iOS.Interfaces;

namespace Appium.Net.Integration.Tests.IOS
{
    [TestFixture]
    [Category("iOS")]
    public class DeviceInteractionTest
    {
        private IOSDriver _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetIosCaps(Apps.Get("iosTestApp"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new IOSDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            _driver?.Quit();
            if (Env.ServerIsRemote()) return;
            AppiumServers.StopLocalService();
        }

        [Test]
        public void ShakeDeviceTest()
        {
            Assert.DoesNotThrow((Action)(() => _driver.ShakeDevice()));
        }

        [Test]
        public void PerformTouchIdTest()
        {
            _driver.ExecuteScript("mobile: enrollBiometric", new Dictionary<string, object> { ["isEnabled"] = true });

            var performsTouchId = (IPerformsTouchID)_driver;
            using (Assert.EnterMultipleScope())
            {
                Assert.DoesNotThrow((Action)(() => performsTouchId.PerformTouchID(true)));
                Assert.DoesNotThrow((Action)(() => performsTouchId.PerformTouchID(false)));
            }
        }
    }
}
