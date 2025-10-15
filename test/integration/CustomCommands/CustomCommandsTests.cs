using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;
using System.IO;
using OpenQA.Selenium.Appium;

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
        public void CustomCommand_Status_ReturnsServerStatusSuccessfully()
        {
            // Register a custom command for /status endpoint (GET)
            _driver.RegisterCustomDriverCommand("getStatusExample", "GET", "/status");

            // Execute the custom command
            var result = _driver.ExecuteCustomDriverCommand("getStatusExample");

            // Assert that the result is a dictionary and contains the expected keys/values
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Dictionary<string, object>>());
            var dict = (Dictionary<string, object>)result;
            Assert.That(dict.ContainsKey("ready"), Is.True);
            Assert.That(dict.ContainsKey("message"), Is.True);
            Assert.That(dict["ready"], Is.True);
            Assert.That(dict["message"], Is.EqualTo("The server is ready to accept new connections"));
        }

        [Test]
        public void RegisterAndExecuteCustomCommand_ShouldReturnCurrentActivity()
        {
            // Register a custom command for /appium/device/current_activity endpoint (GET)
            _driver.RegisterCustomDriverCommand("getCurrentActivityExample", "GET", "/session/{sessionId}/appium/device/current_activity");

            // Execute the custom command
            var result = _driver.ExecuteCustomDriverCommand("getCurrentActivityExample");

            // Assert that the result is a string (activity name)
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<string>());
            Assert.That(result.ToString(), Does.StartWith(".").Or.Contain("Activity"));
        }
    }
}