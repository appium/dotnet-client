using System;
using System.Linq;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Net.Integration.Tests.IOS.Session.Logs
{
    internal class LogTests
    {
        private IWebDriver _driver;
        private AppiumOptions _iosOptions;
        private const string SyslogLogType = "syslog";
        private const string CrashLogType = "crashlog";
        private const string ServerLogType = "server";

        [OneTimeSetUp]
        public void SetUp()
        {
            _iosOptions = Caps.GetIosCaps(Apps.Get("iosUICatalogApp"));
            _driver = new IOSDriver(
                Env.ServerIsLocal() ? AppiumServers.LocalServiceUri : AppiumServers.RemoteServerUri,
                _iosOptions);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver.Dispose();
        }

        [Test]
        public void CanRetrieveLogTypesTest()
        {
            var logs = _driver.Manage().Logs;
            var availableLogTypes = _driver.Manage().Logs.AvailableLogTypes;
            Assert.Multiple(() =>
            {
                Assert.That(logs.AvailableLogTypes, Is.Not.Null);
                Assert.That(availableLogTypes, Is.Not.Empty, nameof(availableLogTypes));
            });
            Console.WriteLine(@"Available log types:");
            foreach (var logType in availableLogTypes)
            {
                Console.WriteLine(logType);
            }
        }

        [Test]
        public void CanCaptureSyslogTest()
        {
            var availableLogTypes = _driver.Manage().Logs.AvailableLogTypes;
            Assert.That(availableLogTypes, Is.Not.Null);
            Assert.That(availableLogTypes, Has.Member(SyslogLogType));

            Assert.DoesNotThrow(() => _driver.Manage().Logs.GetLog(SyslogLogType));
        }

        [Test]
        public void CanCaptureCrashlogTest()
        {
            var availableLogTypes = _driver.Manage().Logs.AvailableLogTypes;
            Assert.That(availableLogTypes, Is.Not.Null);
            Assert.That(availableLogTypes, Has.Member(CrashLogType));

            Assert.DoesNotThrow(() => _driver.Manage().Logs.GetLog(CrashLogType));
        }

        [Test]
        public void CanCaptureServerTest()
        {
            var availableLogTypes = _driver.Manage().Logs.AvailableLogTypes;
            Assert.That(availableLogTypes, Is.Not.Null);
            Assert.That(availableLogTypes, Has.Member(ServerLogType));

            var appiumServerLog = _driver.Manage().Logs.GetLog(LogType.Server);
            Assert.That(appiumServerLog, Is.Not.Null.And.Count.GreaterThan(1));

            Console.WriteLine(@"Last 10 lines of log...");
            foreach (var entry in appiumServerLog.Skip(appiumServerLog.Count - 10))
            {
                Console.WriteLine(entry);
            }
        }
    }
}