using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.ScreenRecording;
using OpenQA.Selenium.Appium.Windows;

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
        public void StartRecordingScreen_DefaultOptions_SendsStartRecordingCommandAndHandlesNullResponse()
        {
            _executor.ResponseToReturn = new Response("test-session", null, WebDriverResult.Success);

            var result = _driver.StartRecordingScreen();

            Assert.That(result, Is.Null);
            Assert.That(_executor.LastCommand.Name, Is.EqualTo(AppiumDriverCommand.StartRecordingScreen));
            var options = _executor.LastCommand.Parameters["options"] as IDictionary<string, object>;
            Assert.That(options, Is.Not.Null);
            Assert.That(options, Is.Empty);
        }

        [Test]
        public void StartRecordingScreen_WithWincoreOptions_SendsExpectedParameters()
        {
            _executor.ResponseToReturn = new Response("test-session", "dGVzdA==", WebDriverResult.Success);

            var recordingOptions = WincoreStartScreenRecordingOptions.StartScreenRecordingOptions()
                .WithVideoFps(30)
                .EnableCursorCapture()
                .EnableClicksCapture()
                .WithAudioInput("Microphone")
                .WithVideoFilter("scale=1024:-2")
                .WithPreset("fast")
                .WithTimeLimit(TimeSpan.FromMinutes(5))
                .EnableForcedRestart();

            var result = _driver.StartRecordingScreen(recordingOptions);

            Assert.That(result, Is.EqualTo("dGVzdA=="));
            Assert.That(_executor.LastCommand.Name, Is.EqualTo(AppiumDriverCommand.StartRecordingScreen));
            var options = _executor.LastCommand.Parameters["options"] as IDictionary<string, object>;
            Assert.That(options, Is.Not.Null);
            Assert.That(options["videoFps"], Is.EqualTo(30));
            Assert.That(options["captureCursor"], Is.EqualTo(true));
            Assert.That(options["captureClicks"], Is.EqualTo(true));
            Assert.That(options["audioInput"], Is.EqualTo("Microphone"));
            Assert.That(options["videoFilter"], Is.EqualTo("scale=1024:-2"));
            Assert.That(options["preset"], Is.EqualTo("fast"));
            Assert.That(options["timeLimit"], Is.EqualTo(300));
            Assert.That(options["forceRestart"], Is.EqualTo(true));
        }

        [Test]
        public void StartRecordingScreen_WithFpsAlias_SetsVideoFpsParameter()
        {
            var recordingOptions = WincoreStartScreenRecordingOptions.GetWincoreStartScreenRecordingOptions()
                .WithFps(60);

            var parameters = recordingOptions.GetParameters();
            Assert.That(parameters["videoFps"], Is.EqualTo(60));
        }

        [Test]
        public void StopRecordingScreen_WithWincoreOptions_SendsExpectedParameters()
        {
            _executor.ResponseToReturn = new Response("test-session", "YmFzZTY0dmlkZW8=", WebDriverResult.Success);

            var stopOptions = WincoreStopScreenRecordingOptions.StopScreenRecordingOptions();

            var result = _driver.StopRecordingScreen(stopOptions);

            Assert.That(result, Is.EqualTo("YmFzZTY0dmlkZW8="));
            Assert.That(_executor.LastCommand.Name, Is.EqualTo(AppiumDriverCommand.StopRecordingScreen));
            var options = _executor.LastCommand.Parameters["options"] as IDictionary<string, object>;
            Assert.That(options, Is.Not.Null);
            Assert.That(options, Is.Empty);
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
        public void Settings_GetAndSet_ExecuteExpectedCommands()
        {
            _driver.SetSetting("waitForIdleTimeout", 100);
            Assert.That(_executor.LastCommand.Name, Is.EqualTo(AppiumDriverCommand.UpdateSettings));
            var settingsParam = _executor.LastCommand.Parameters["settings"] as IDictionary<string, object>;
            Assert.That(settingsParam, Is.Not.Null);
            Assert.That(settingsParam["waitForIdleTimeout"], Is.EqualTo(100));

            var expectedSettings = new Dictionary<string, object> { { "waitForIdleTimeout", 100 } };
            _executor.ResponseToReturn = new Response("test-session", expectedSettings, WebDriverResult.Success);
            var currentSettings = _driver.Settings;
            Assert.That(currentSettings["waitForIdleTimeout"], Is.EqualTo(100));
            Assert.That(_executor.LastCommand.Name, Is.EqualTo(AppiumDriverCommand.GetSettings));
        }
    }
}
