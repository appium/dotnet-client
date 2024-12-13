using System;
using System.Linq;
using System.Text.RegularExpressions;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android.Session.Logs
{
    internal class LogTests
    {
        private IWebDriver _driver;
        private AppiumOptions _androidOptions;
        private const string LogcatLogType = "logcat";
        private const string ServerLogType = "server";
        private const string BugReportLogType = "bugreport";

        [OneTimeSetUp]
        public void SetUp()
        {
            _androidOptions = Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));
            _driver = new AndroidDriver(
                Env.ServerIsLocal() ? AppiumServers.LocalServiceUri : AppiumServers.RemoteServerUri,
                _androidOptions);
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
        public void CanCaptureLogcatTest()
        {
            var availableLogTypes = _driver.Manage().Logs.AvailableLogTypes;
            Assert.That(availableLogTypes, Is.Not.Null);
            Assert.That(availableLogTypes, Has.Member(LogcatLogType));

            Assert.DoesNotThrow(() => _driver.Manage().Logs.GetLog(LogcatLogType));
        }

        /// <summary>
        /// For this test to pass, need to run the appium server as follows: `appium --allow-insecure get_server_logs`
        /// </summary>
        [Test]
        public void CanCaptureServerTest()
        {
            var availableLogTypes = _driver.Manage().Logs.AvailableLogTypes;
            Assert.That(availableLogTypes, Is.Not.Null);
            Assert.That(availableLogTypes, Has.Member(ServerLogType));

            var appiumServerLog = _driver.Manage().Logs.GetLog(LogType.Server);
            Assert.That(appiumServerLog, Is.Not.Null.And.Count.GreaterThan(1));

            Console.WriteLine("Last 10 lines of log...");
            foreach (var entry in appiumServerLog.Skip(appiumServerLog.Count - 10))
            {
                Console.WriteLine(entry);
            }
        }

        [Test]
        public void CanCaptureBugReportTest()
        {
            var availableLogTypes = _driver.Manage().Logs.AvailableLogTypes;
            Assert.That(availableLogTypes, Is.Not.Null);
            Assert.That(availableLogTypes, Has.Member(BugReportLogType));

            var bugReportLogEntry = _driver.Manage().Logs.GetLog(BugReportLogType);
            Assert.That(bugReportLogEntry, Is.Not.Null.And.Count.AtLeast(1));
            var bugReportLogPath = bugReportLogEntry.FirstOrDefault()?.Message;

            var match = Regex.Match(bugReportLogPath ?? throw new InvalidOperationException(), @"^([\s\S])*.zip");
            Assert.That(match.Success, Is.True, nameof(match.Success));
            bugReportLogPath = match.Value;

            var bugReportLogByteArray = ((AndroidDriver) _driver).PullFile(bugReportLogPath);
            Assert.That(bugReportLogByteArray.Length, Is.GreaterThan(1));
        }
    }
}