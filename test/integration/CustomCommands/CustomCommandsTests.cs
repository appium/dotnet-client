using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;
using System.IO;

namespace Appium.Net.Integration.Tests
{
    [TestFixture]
    public class CustomCommandTests
    {
        private AndroidDriver _driver;

        [SetUp]
        public void SetUp()
        {
            var capabilities = Caps.GetAndroidUIAutomatorCaps();
            capabilities.UseWebSocketUrl = true;
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
        }

        [TearDown]
        public void TearDown()
        {
            _driver?.Quit();
        }

        [Test]
        public void RegisterAndExecuteCustomCommand_ShouldReturnStatus()
        {
            // Register a custom command for /status endpoint (GET)
            _driver.RegisterCustomDriverCommand("getStatus", "GET", "/status");

            // Execute the custom command
            var result = _driver.ExecuteCustomDriverCommand("getStatus");

            // Assert that the result contains expected keys
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Contains("status"));
        }

        [Test]
        public void RegisterAndExecuteCustomCommand_ShouldReturnCurrentActivity()
        {
            // Register a custom command for /appium/device/current_activity endpoint (GET)
            _driver.RegisterCustomDriverCommand("getCurrentActivity", "GET", "/session/{sessionId}/appium/device/current_activity");

            // Execute the custom command
            var result = _driver.ExecuteCustomDriverCommand("getCurrentActivity");

            // Assert that the result is a string (activity name)
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<string>());
            Assert.That(result.ToString(), Does.StartWith(".").Or.Contain("Activity"));
        }

        [Test]
        public void SetNetworkConditions_CustomCommand_IgnoreIfNotChrome()
        {
            // This test assumes a Chrome session with W3C WebDriver support
            // Adjust driver initialization as needed for Chrome
            // You may need to use OpenQA.Selenium.Chrome.ChromeDriver for desktop

            var browserNameObj = _driver.Capabilities.GetCapability("browserName");
            if (browserNameObj == null || !browserNameObj.ToString().Equals("chrome", StringComparison.OrdinalIgnoreCase))
            {
                Assert.Ignore("Network conditions custom command is only supported on Chrome.");
            }

            var customDriver = _driver as ICustomDriverCommandExecutor;
            Assert.That(customDriver, Is.Not.Null);

            // Register the custom command
            customDriver.RegisterCustomDriverCommand("setNetworkConditions", new HttpCommandInfo("POST", "/session/{sessionId}/chromium/network_conditions"));

            // Save current network conditions (if possible)
            // Not all drivers support getting current network conditions, so this may be skipped

            // Set offline network conditions
            var networkConditions = new Dictionary<string, object>
            {
                ["offline"] = true,
                ["latency"] = 100,
                ["download_throughput"] = 500 * 1024,
                ["upload_throughput"] = 500 * 1024
            };
            customDriver.ExecuteCustomDriverCommand("setNetworkConditions", networkConditions);

            // Try to load a page and expect failure
            Assert.Throws<WebDriverException>(() => _driver.Url = "https://www.example.com");

            // Restore network conditions to online
            var normalNetwork = new Dictionary<string, object>
            {
                ["offline"] = false,
                ["latency"] = 0,
                ["download_throughput"] = -1,
                ["upload_throughput"] = -1
            };
            customDriver.ExecuteCustomDriverCommand("setNetworkConditions", normalNetwork);

            // Should succeed now
            _driver.Url = "https://www.example.com";
            Assert.That(_driver.Title, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void FullPageScreenshot_CustomCommand_IgnoreIfNotFirefox()
        {
            // This test assumes a Firefox session with W3C WebDriver support
            // Adjust driver initialization as needed for Firefox
            // You may need to use OpenQA.Selenium.Firefox.FirefoxDriver for desktop

            var browserNameObj = _driver.Capabilities.GetCapability("browserName");
            if (browserNameObj == null || !browserNameObj.ToString().Equals("firefox", StringComparison.OrdinalIgnoreCase))
            {
                Assert.Ignore("Full page screenshot custom command is only supported on Firefox.");
            }

            var customDriver = _driver as ICustomDriverCommandExecutor;
            Assert.That(customDriver, Is.Not.Null);

            customDriver.RegisterCustomDriverCommand("fullPageScreenshot", new HttpCommandInfo("GET", "/session/{sessionId}/moz/screenshot/full"));

            _driver.Url = "https://www.example.com";
            Thread.Sleep(2000); // Wait for page to load

            var sessionId = _driver.SessionId;
            var fullPageScreenshotCommand = new Command(sessionId, "fullPageScreenshot", null);

            var response = ((IHasCommandExecutor)_driver).CommandExecutor.Execute(fullPageScreenshotCommand);
            string base64 = response.Value.ToString();
            var screenshot = new Screenshot(base64);

            var filePath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "fullpage.png");
            screenshot.SaveAsFile(filePath);

            Assert.That(File.Exists(filePath), Is.True);
        }
    }
}