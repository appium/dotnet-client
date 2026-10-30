//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//See the NOTICE file distributed with this work for additional
//information regarding copyright ownership.
//You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace Appium.Net.Integration.Tests.Driver
{
    [TestFixture]
    public class MockServerClientTests
    {
        private const string W3CElementKey = "element-6066-11e4-a52e-4f735466cecf";
        private MockAppiumServer _mockServer;
        private AndroidDriver _driver;

        [SetUp]
        public void SetUp()
        {
            _mockServer = new MockAppiumServer();
            var options = new AppiumOptions
            {
                PlatformName = "Android",
                AutomationName = "UiAutomator2",
                DeviceName = "Android Emulator"
            };
            _driver = new AndroidDriver(_mockServer.ServerUri, options, TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void TearDown()
        {
            _driver?.Dispose();
            _mockServer?.Dispose();
        }

        private static Dictionary<string, string> ExtractW3CCapabilities(JsonElement root)
        {
            var merged = new Dictionary<string, string>(StringComparer.Ordinal);
            Assert.That(root.TryGetProperty("capabilities", out JsonElement capabilities), Is.True);

            if (capabilities.TryGetProperty("alwaysMatch", out JsonElement alwaysMatch) &&
                alwaysMatch.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty prop in alwaysMatch.EnumerateObject())
                {
                    if (prop.Value.ValueKind == JsonValueKind.String)
                    {
                        merged[prop.Name] = prop.Value.GetString();
                    }
                }
            }

            if (capabilities.TryGetProperty("firstMatch", out JsonElement firstMatch) &&
                firstMatch.ValueKind == JsonValueKind.Array &&
                firstMatch.GetArrayLength() > 0 &&
                firstMatch[0].ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty prop in firstMatch[0].EnumerateObject())
                {
                    if (prop.Value.ValueKind == JsonValueKind.String)
                    {
                        merged[prop.Name] = prop.Value.GetString();
                    }
                }
            }

            return merged;
        }

        [Test]
        public void NewSession_SendsIdempotencyHeaderAndCapabilitiesOverHttp()
        {
            var newSessionRequest = _mockServer.Requests.FirstOrDefault(r =>
                r.HttpMethod == "POST" && r.Path == "/session");

            Assert.That(newSessionRequest, Is.Not.Null);
            Assert.That(newSessionRequest.Headers.TryGetValue("X-Idempotency-Key", out string idempotencyKey), Is.True);
            Assert.That(Guid.TryParse(idempotencyKey, out _), Is.True);

            using var doc = JsonDocument.Parse(newSessionRequest.Body);
            Dictionary<string, string> capabilities = ExtractW3CCapabilities(doc.RootElement);
            Assert.That(capabilities.ContainsKey("platformName"), Is.True);
            Assert.That(capabilities["platformName"], Is.EqualTo("Android"));
            Assert.That(capabilities.ContainsKey("appium:automationName"), Is.True);
            Assert.That(capabilities["appium:automationName"], Is.EqualTo("UiAutomator2"));
            Assert.That(capabilities.ContainsKey("appium:deviceName"), Is.True);
            Assert.That(capabilities["appium:deviceName"], Is.EqualTo("Android Emulator"));
            Assert.That(_driver.SessionId.ToString(), Is.EqualTo(_mockServer.SessionId));
        }

        [Test]
        public void FindElementAndClick_SendsExpectedW3CHttpRequests()
        {
            const string elementId = "el-1234";
            _mockServer.OnRequest("POST", $"/session/{_mockServer.SessionId}/element",
                new Dictionary<string, object>
                {
                    [W3CElementKey] = elementId
                });
            _mockServer.OnRequest("POST", $"/session/{_mockServer.SessionId}/element/{elementId}/click", null);
            _mockServer.OnRequest("GET", $"/session/{_mockServer.SessionId}/element/{elementId}/text", "Login");

            AppiumElement element = _driver.FindElement(MobileBy.AccessibilityId("login-button"));
            Assert.That(element.Id, Is.EqualTo(elementId));
            Assert.That(element.Text, Is.EqualTo("Login"));

            element.Click();

            var findRequest = _mockServer.Requests.Last(r =>
                r.HttpMethod == "POST" &&
                r.Path == $"/session/{_mockServer.SessionId}/element");
            using var doc = JsonDocument.Parse(findRequest.Body);
            Assert.That(doc.RootElement.GetProperty("using").GetString(), Is.EqualTo("accessibility id"));
            Assert.That(doc.RootElement.GetProperty("value").GetString(), Is.EqualTo("login-button"));

            Assert.That(_mockServer.Requests.Any(r =>
                r.HttpMethod == "POST" &&
                r.Path == $"/session/{_mockServer.SessionId}/element/{elementId}/click"), Is.True);
        }

        [Test]
        public void AppiumDeviceCommands_RouteAndSerializeOverHttpCorrectly()
        {
            _mockServer.OnExecuteScript("mobile:getCurrentActivity", ".MainActivity");
            _mockServer.OnRequest("POST", $"/session/{_mockServer.SessionId}/appium/device/activate_app", null);
            _mockServer.OnRequest("POST", $"/session/{_mockServer.SessionId}/appium/device/terminate_app", true);
            _mockServer.OnRequest("GET", $"/session/{_mockServer.SessionId}/contexts", new[] { "NATIVE_APP", "WEBVIEW_chrome" });

            Assert.That(_driver.CurrentActivity, Is.EqualTo(".MainActivity"));
            var activityReq = _mockServer.Requests.Last(r =>
                r.HttpMethod == "POST" &&
                r.Path == $"/session/{_mockServer.SessionId}/execute/sync");
            using (var activityDoc = JsonDocument.Parse(activityReq.Body))
            {
                Assert.That(activityDoc.RootElement.GetProperty("script").GetString(), Is.EqualTo("mobile:getCurrentActivity"));
            }

            _driver.ActivateApp("io.appium.android.apis");
            var activateReq = _mockServer.Requests.Last(r =>
                r.HttpMethod == "POST" &&
                r.Path == $"/session/{_mockServer.SessionId}/appium/device/activate_app");
            using (var doc = JsonDocument.Parse(activateReq.Body))
            {
                Assert.That(doc.RootElement.GetProperty("appId").GetString(), Is.EqualTo("io.appium.android.apis"));
            }

            bool terminated = _driver.TerminateApp("io.appium.android.apis");
            Assert.That(terminated, Is.True);

            IList<string> contexts = _driver.Contexts;
            Assert.That(contexts, Is.EqualTo(new[] { "NATIVE_APP", "WEBVIEW_chrome" }));
        }

        [Test]
        public void ClipboardCommands_EncodeAndDecodeBase64OverHttp()
        {
            string expectedBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes("Hello Appium"));
            _mockServer.OnExecuteScript("mobile:setClipboard", null);
            _mockServer.OnExecuteScript("mobile:getClipboard", expectedBase64);

            _driver.SetClipboardText("Hello Appium", "note");
            var setReq = _mockServer.Requests.Last(r =>
                r.HttpMethod == "POST" &&
                r.Path == $"/session/{_mockServer.SessionId}/execute/sync" &&
                r.Body.Contains("mobile:setClipboard"));
            using (var doc = JsonDocument.Parse(setReq.Body))
            {
                Assert.That(doc.RootElement.GetProperty("script").GetString(), Is.EqualTo("mobile:setClipboard"));
                var firstArg = doc.RootElement.GetProperty("args")[0];
                Assert.That(firstArg.GetProperty("content").GetString(), Is.EqualTo(expectedBase64));
                Assert.That(firstArg.GetProperty("contentType").GetString(), Is.EqualTo("plaintext"));
                Assert.That(firstArg.GetProperty("label").GetString(), Is.EqualTo("note"));
            }

            string clipboardText = _driver.GetClipboardText();
            Assert.That(clipboardText, Is.EqualTo("Hello Appium"));
        }

        [Test]
        public void RegisterAndExecuteCustomDriverCommand_WorksAgainstMockServer()
        {
            _mockServer.OnRequest("GET", "/status", new Dictionary<string, object>
            {
                ["ready"] = true,
                ["message"] = "Mock server ready"
            });

            _driver.RegisterCustomDriverCommand("mockStatus", "GET", "/status");
            var result = _driver.ExecuteCustomDriverCommand("mockStatus") as Dictionary<string, object>;

            Assert.That(result, Is.Not.Null);
            Assert.That(result["ready"], Is.EqualTo(true));
            Assert.That(result["message"], Is.EqualTo("Mock server ready"));
        }
    }
}
