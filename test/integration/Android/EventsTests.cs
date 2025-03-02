using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Appium.Net.Integration.Tests.Android
{
    public class EventTests
    {
        private AndroidDriver _driver;

        private readonly string _appKey = "androidApiDemos";

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get(_appKey))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get(_appKey));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
        }

        [Test]
        public void GetEventsTestCase()
        {
            object value;
            _driver.GetEvents().TryGetValue("commands", out value);
            Assert.That(value, Is.Not.Null);
        }

        [Test]
        public void LogEventTestCase()
        {
            object value;

            _driver.GetEvents().TryGetValue("appium:hello", out value);
            Assert.That(value, Is.Null);

            _driver.LogEvent("appium", "hello");

            _driver.GetEvents().TryGetValue("appium:hello", out value);
            Assert.That(value, Is.Not.Null);
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            if (_driver != null)
            {
                _ = _driver.TerminateApp(Apps.GetId(_appKey));
                _driver?.Quit();
            }
            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }
    }
}
