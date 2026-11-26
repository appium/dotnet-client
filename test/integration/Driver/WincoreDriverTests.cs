using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.ScreenRecording;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Edge;

namespace Appium.Net.Integration.Tests.Driver
{
    [TestFixture]
    public class WincoreDriverTests
    {
        private class MockCommandExecutor : ICommandExecutor
        {
            public Command LastCommand { get; private set; }
            public List<Command> ExecutedCommands { get; } = new List<Command>();
            public Response ResponseToReturn { get; set; } = new Response("test-session", null, WebDriverResult.Success);

            public Response Execute(Command commandToExecute)
            {
                LastCommand = commandToExecute;
                ExecutedCommands.Add(commandToExecute);

                if (commandToExecute.Name == DriverCommand.NewSession)
                {
                    return new Response(
                        "test-session",
                        new Dictionary<string, object>
                        {
                            { "capabilities", new Dictionary<string, object>() }
                        },
                        WebDriverResult.Success);
                }

                return ResponseToReturn;
            }

            public Task<Response> ExecuteAsync(Command commandToExecute)
            {
                return Task.FromResult(Execute(commandToExecute));
            }

            public bool TryAddCommand(string commandName, CommandInfo commandInfo)
            {
                return true;
            }

            public void Dispose()
            {
            }
        }

        private MockCommandExecutor _executor;
        private WincoreDriver _driver;

        [SetUp]
        public void SetUp()
        {
            _executor = new MockCommandExecutor();
            _driver = new WincoreDriver(_executor, new AppiumOptions());
        }

        [TearDown]
        public void TearDown()
        {
            _driver?.Dispose();
            _executor?.Dispose();
        }

        [Test]
        public void Constructor_SetsDefaultPlatformAndAutomationName()
        {
            var options = new AppiumOptions();
            using var executor = new MockCommandExecutor();
            using var driver = new WincoreDriver(executor, options);

            Assert.That(options.PlatformName, Is.EqualTo(MobilePlatform.Windows));
            Assert.That(options.AutomationName, Is.EqualTo(AutomationName.Wincore));

            var capabilities = options.ToDictionary();
            Assert.That(capabilities["platformName"], Is.EqualTo(MobilePlatform.Windows));
            Assert.That(capabilities["appium:automationName"], Is.EqualTo("Wincore"));
        }

        [Test]
        public void Constructor_PreservesCustomAutomationNameWhenSet()
        {
            var options = new AppiumOptions { AutomationName = "CustomWindowsAutomation" };
            using var executor = new MockCommandExecutor();
            using var driver = new WincoreDriver(executor, options);

            Assert.That(options.PlatformName, Is.EqualTo(MobilePlatform.Windows));
            Assert.That(options.AutomationName, Is.EqualTo("CustomWindowsAutomation"));
        }

        [Test]
        public void Constructor_PreservesExplicitPlatformNameWhenSet()
        {
            var options = new AppiumOptions { PlatformName = "CustomWindowsPlatform" };
            using var executor = new MockCommandExecutor();
            using var driver = new WincoreDriver(executor, options);

            Assert.That(options.PlatformName, Is.EqualTo("CustomWindowsPlatform"));
            Assert.That(options.AutomationName, Is.EqualTo(AutomationName.Wincore));

            var capabilities = options.ToDictionary();
            Assert.That(capabilities["platformName"], Is.EqualTo("CustomWindowsPlatform"));
        }

        [Test]
        public void Constructor_NonAppiumDriverOptions_DefaultsAndPreservesAutomationName()
        {
            var edgeOptions = new EdgeOptions();
            using var defaultExecutor = new MockCommandExecutor();
            using var defaultDriver = new WincoreDriver(defaultExecutor, edgeOptions);

            var defaultCaps = edgeOptions.ToCapabilities();
            Assert.That(defaultCaps.GetCapability("platformName"), Is.EqualTo(MobilePlatform.Windows));
            Assert.That(defaultCaps.GetCapability(MobileCapabilityType.AutomationName), Is.EqualTo(AutomationName.Wincore));
            var defaultSessionJson = JsonSerializer.Serialize(
                defaultExecutor.ExecutedCommands.First(c => c.Name == DriverCommand.NewSession).Parameters);
            Assert.That(defaultSessionJson, Does.Contain("\"appium:automationName\":\"Wincore\""));

            var customEdgeOptions = new EdgeOptions();
            customEdgeOptions.AddAdditionalOption(MobileCapabilityType.AutomationName, "CustomAutomation");
            using var customExecutor = new MockCommandExecutor();
            using var customDriver = new WincoreDriver(customExecutor, customEdgeOptions);

            Assert.That(
                customEdgeOptions.ToCapabilities().GetCapability(MobileCapabilityType.AutomationName),
                Is.EqualTo("CustomAutomation"));
        }

        [Test]
        public void LaunchApp_ExecutesWindowsLaunchAppScript()
        {
            _driver.LaunchApp();

            Assert.That(_executor.LastCommand.Name, Is.EqualTo(DriverCommand.ExecuteScript));
            Assert.That(_executor.LastCommand.Parameters["script"], Is.EqualTo("windows: launchApp"));
            Assert.That(_executor.LastCommand.Parameters["args"], Is.EqualTo(Array.Empty<object>()));
        }

        [Test]
        public void CloseApp_ExecutesWindowsCloseAppScript()
        {
            _driver.CloseApp();

            Assert.That(_executor.LastCommand.Name, Is.EqualTo(DriverCommand.ExecuteScript));
            Assert.That(_executor.LastCommand.Parameters["script"], Is.EqualTo("windows: closeApp"));
            Assert.That(_executor.LastCommand.Parameters["args"], Is.EqualTo(Array.Empty<object>()));
        }

        [Test]
        public void StartRecordingScreen_DefaultOptions_SendsExecuteScriptAndHandlesNullResponse()
        {
            _executor.ResponseToReturn = new Response("test-session", null, WebDriverResult.Success);

            var result = _driver.StartRecordingScreen();

            Assert.That(result, Is.Null);
            Assert.That(_executor.LastCommand.Name, Is.EqualTo(DriverCommand.ExecuteScript));
            Assert.That(_executor.LastCommand.Parameters["script"], Is.EqualTo("windows: startRecordingScreen"));
            var args = _executor.LastCommand.Parameters["args"] as object[];
            Assert.That(args, Is.Not.Null.And.Length.EqualTo(1));
            var options = args[0] as IDictionary<string, object>;
            Assert.That(options, Is.Not.Null);
            Assert.That(options, Is.Empty);
        }

        [Test]
        public void StartRecordingScreen_WithWincoreOptions_SendsExpectedParameters()
        {
            _executor.ResponseToReturn = new Response("test-session", "dGVzdA==", WebDriverResult.Success);

            var recordingOptions = WincoreStartScreenRecordingOptions.GetWincoreStartScreenRecordingOptions()
                .WithOutputPath(@"C:\videos\capture.mp4")
                .WithVideoFps(30)
                .EnableCursorCapture()
                .EnableClicksCapture()
                .WithAudioInput("Microphone")
                .WithVideoFilter("scale=1024:-2")
                .WithPreset("fast")
                .WithTimeLimit(TimeSpan.FromMinutes(5))
                .DisableForcedRestart();

            var result = _driver.StartRecordingScreen(recordingOptions);

            Assert.That(typeof(WincoreStartScreenRecordingOptions).GetMethod("WithUploadOptions"), Is.Null);
            Assert.That(result, Is.EqualTo("dGVzdA=="));
            Assert.That(_executor.LastCommand.Name, Is.EqualTo(DriverCommand.ExecuteScript));
            Assert.That(_executor.LastCommand.Parameters["script"], Is.EqualTo("windows: startRecordingScreen"));
            var args = _executor.LastCommand.Parameters["args"] as object[];
            Assert.That(args, Is.Not.Null.And.Length.EqualTo(1));
            var options = args[0] as IDictionary<string, object>;
            Assert.That(options, Is.Not.Null);
            Assert.That(options["outputPath"], Is.EqualTo(@"C:\videos\capture.mp4"));
            Assert.That(options["videoFps"], Is.EqualTo(30));
            Assert.That(options["captureCursor"], Is.EqualTo(true));
            Assert.That(options["captureClicks"], Is.EqualTo(true));
            Assert.That(options["audioInput"], Is.EqualTo("Microphone"));
            Assert.That(options["videoFilter"], Is.EqualTo("scale=1024:-2"));
            Assert.That(options["preset"], Is.EqualTo("fast"));
            Assert.That(options["timeLimit"], Is.EqualTo(300));
            Assert.That(options["forceRestart"], Is.EqualTo(false));
        }

        [Test]
        public void StopRecordingScreen_WithWincoreOptions_SendsExpectedParameters()
        {
            _executor.ResponseToReturn = new Response("test-session", "YmFzZTY0dmlkZW8=", WebDriverResult.Success);

            var uploadOptions = new ScreenRecordingUploadOptions()
                .WithRemotePath("https://example.com/upload")
                .WithAuthCredentials("user", "secret")
                .WithHttpMethod(ScreenRecordingUploadOptions.RequestMethod.POST);

            var stopOptions = WincoreStopScreenRecordingOptions.StopScreenRecordingOptions()
                .WithUploadOptions(uploadOptions);

            var result = _driver.StopRecordingScreen(stopOptions);

            Assert.That(result, Is.EqualTo("YmFzZTY0dmlkZW8="));
            Assert.That(_executor.LastCommand.Name, Is.EqualTo(DriverCommand.ExecuteScript));
            Assert.That(_executor.LastCommand.Parameters["script"], Is.EqualTo("windows: stopRecordingScreen"));
            var args = _executor.LastCommand.Parameters["args"] as object[];
            Assert.That(args, Is.Not.Null.And.Length.EqualTo(1));
            var options = args[0] as IDictionary<string, object>;
            Assert.That(options, Is.Not.Null);
            Assert.That(options["remotePath"], Is.EqualTo("https://example.com/upload"));
            Assert.That(options["user"], Is.EqualTo("user"));
            Assert.That(options["pass"], Is.EqualTo("secret"));
            Assert.That(options["method"], Is.EqualTo("POST"));
        }

        [Test]
        public void PullFolder_ThrowsNotSupportedException()
        {
            var ex = Assert.Throws<NotSupportedException>(() => _driver.PullFolder(@"C:\temp"));
            Assert.That(ex.Message, Is.EqualTo("The Wincore driver cannot pull folders"));
        }

        [Test]
        public void PullFile_And_PushFile_ExecuteExpectedCommands()
        {
            var expectedBytes = Encoding.UTF8.GetBytes("hello wincore");
            _executor.ResponseToReturn = new Response(
                "test-session",
                Convert.ToBase64String(expectedBytes),
                WebDriverResult.Success);

            var pulled = _driver.PullFile(@"C:\temp\file.txt");

            Assert.That(pulled, Is.EqualTo(expectedBytes));
            Assert.That(_executor.LastCommand.Name, Is.EqualTo(AppiumDriverCommand.PullFile));
            Assert.That(_executor.LastCommand.Parameters["path"], Is.EqualTo(@"C:\temp\file.txt"));

            _executor.ResponseToReturn = new Response("test-session", null, WebDriverResult.Success);
            _driver.PushFile(@"C:\temp\out.txt", "hello wincore");

            Assert.That(_executor.LastCommand.Name, Is.EqualTo(AppiumDriverCommand.PushFile));
            Assert.That(_executor.LastCommand.Parameters["path"], Is.EqualTo(@"C:\temp\out.txt"));
            Assert.That(_executor.LastCommand.Parameters["data"], Is.EqualTo(expectedBytes));
        }

        [Test]
        public void Context_And_Contexts_SupportWebViewSwitching()
        {
            _executor.ResponseToReturn = new Response(
                "test-session",
                new object[] { "NATIVE_APP", "WEBVIEW_1" },
                WebDriverResult.Success);

            var contexts = _driver.Contexts;
            Assert.That(contexts, Is.EqualTo(new[] { "NATIVE_APP", "WEBVIEW_1" }));
            Assert.That(_executor.LastCommand.Name, Is.EqualTo(AppiumDriverCommand.Contexts));

            _executor.ResponseToReturn = new Response("test-session", null, WebDriverResult.Success);
            _driver.Context = "WEBVIEW_1";
            Assert.That(_executor.LastCommand.Name, Is.EqualTo(AppiumDriverCommand.SetContext));
            Assert.That(_executor.LastCommand.Parameters["name"], Is.EqualTo("WEBVIEW_1"));

            _executor.ResponseToReturn = new Response("test-session", "WEBVIEW_1", WebDriverResult.Success);
            Assert.That(_driver.Context, Is.EqualTo("WEBVIEW_1"));
            Assert.That(_executor.LastCommand.Name, Is.EqualTo(AppiumDriverCommand.GetContext));
        }

        [Test]
        public void WincoreDriver_ExecutesCommandsOverMockAppiumServerHttp()
        {
            using var mockServer = new MockAppiumServer();
            mockServer.OnExecuteScript("windows: launchApp", null);
            mockServer.OnExecuteScript("windows: closeApp", null);
            mockServer.OnExecuteScript("windows: startRecordingScreen", "dGVzdA==");
            mockServer.OnExecuteScript("windows: stopRecordingScreen", "YmFzZTY0dmlkZW8=");

            using var httpDriver = new WincoreDriver(mockServer.ServerUri, new AppiumOptions(), TimeSpan.FromSeconds(10));

            httpDriver.LaunchApp();
            httpDriver.CloseApp();

            var startOptions = WincoreStartScreenRecordingOptions.GetWincoreStartScreenRecordingOptions()
                .WithOutputPath(@"C:\videos\capture.mp4")
                .WithVideoFps(30)
                .EnableCursorCapture()
                .DisableForcedRestart();
            Assert.That(httpDriver.StartRecordingScreen(startOptions), Is.EqualTo("dGVzdA=="));

            var stopUpload = new ScreenRecordingUploadOptions()
                .WithRemotePath("https://example.com/stop-upload")
                .WithAuthCredentials("stop-user", "stop-secret")
                .WithHttpMethod(ScreenRecordingUploadOptions.RequestMethod.POST);
            var stopOptions = WincoreStopScreenRecordingOptions.StopScreenRecordingOptions()
                .WithUploadOptions(stopUpload);
            Assert.That(httpDriver.StopRecordingScreen(stopOptions), Is.EqualTo("YmFzZTY0dmlkZW8="));

            var executeRequests = mockServer.Requests
                .Where(r => r.HttpMethod == "POST" && r.Path == $"/session/{mockServer.SessionId}/execute/sync")
                .ToList();
            var startReq = executeRequests.Last(r =>
            {
                using var doc = JsonDocument.Parse(r.Body);
                return doc.RootElement.GetProperty("script").GetString() == "windows: startRecordingScreen";
            });
            using var startDoc = JsonDocument.Parse(startReq.Body);
            var startOptsJson = startDoc.RootElement.GetProperty("args")[0];
            Assert.That(startOptsJson.GetProperty("outputPath").GetString(), Is.EqualTo(@"C:\videos\capture.mp4"));
            Assert.That(startOptsJson.GetProperty("videoFps").GetInt32(), Is.EqualTo(30));
            Assert.That(startOptsJson.GetProperty("captureCursor").GetBoolean(), Is.True);
            Assert.That(startOptsJson.GetProperty("forceRestart").GetBoolean(), Is.False);

            var stopReq = executeRequests.Last(r =>
            {
                using var doc = JsonDocument.Parse(r.Body);
                return doc.RootElement.GetProperty("script").GetString() == "windows: stopRecordingScreen";
            });
            using var stopDoc = JsonDocument.Parse(stopReq.Body);
            var stopOptsJson = stopDoc.RootElement.GetProperty("args")[0];
            Assert.That(stopOptsJson.GetProperty("remotePath").GetString(), Is.EqualTo("https://example.com/stop-upload"));
            Assert.That(stopOptsJson.GetProperty("method").GetString(), Is.EqualTo("POST"));
        }
    }
}
