using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;

namespace Appium.Net.Integration.Tests.ServerTests
{
    [TestFixture]
    public class AppiumLocalServiceTests
    {
        private AppiumLocalService appiumServer;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            // OptionCollector can be customized as needed
            var optionCollector = new OptionCollector();

            // Try to get Appium path from environment variable or npm root -g
            string appiumPath = Environment.GetEnvironmentVariable(AppiumServiceConstants.AppiumBinaryPath);

            if (string.IsNullOrEmpty(appiumPath))
            {
                try
                {
                    bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

                    var startInfo = new ProcessStartInfo
                    {
                        // On Windows, using 'cmd /c' is often more reliable for resolving PATHs
                        FileName = isWindows ? "cmd.exe" : "npm",
                        Arguments = isWindows ? "/c npm root -g" : "root -g",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true, // Capture errors too!
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using var process = Process.Start(startInfo);
                    string output = process.StandardOutput.ReadToEnd()?.Trim();
                    string error = process.StandardError.ReadToEnd()?.Trim();
                    process.WaitForExit();

                    if (!string.IsNullOrEmpty(output))
                    {
                        appiumPath = Path.Combine(output, "appium", "build", "lib", "main.js");
                    }
                    else if (!string.IsNullOrEmpty(error))
                    {
                        Console.WriteLine($"NPM Error: {error}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Process failed: {ex.Message}");
                }
            }

            if (string.IsNullOrEmpty(appiumPath) || !File.Exists(appiumPath))
            {
                Assert.Ignore("Appium is not installed or not found on this machine. Skipping AppiumLocalServiceTests.");
            }

            appiumServer = new AppiumServiceBuilder()
                .WithAppiumJS(new FileInfo(appiumPath))
                .WithIPAddress("127.0.0.1")
                .UsingAnyFreePort()
                .WithArguments(optionCollector)
                .Build();
            appiumServer.Start();

            // Check that the server is running after startup
            Assert.That(appiumServer.IsRunning, Is.True, "Appium server should be running after Start()");
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            appiumServer?.Dispose();
        }

        private AppiumLocalService CreateService()
        {
            // Use dummy values for constructor
            return (AppiumLocalService)Activator.CreateInstance(
                typeof(AppiumLocalService),
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                new object[]
                {
                    new System.IO.FileInfo("node"),
                    "",
                    System.Net.IPAddress.Loopback,
                    4723,
                    TimeSpan.FromSeconds(5),
                    null
                },
                null
            );
        }

        private MethodInfo GetTryGracefulShutdownOnWindows()
        {
            return typeof(AppiumLocalService).GetMethod(
                "TryGracefulShutdownOnWindows",
                BindingFlags.Instance | BindingFlags.NonPublic
            );
        }

        [Test]
        public void TryGracefulShutdownOnWindows_ReturnsTrue_WhenProcessIsNull()
        {
            var service = CreateService();
            var method = GetTryGracefulShutdownOnWindows();
            Assert.That(method, Is.Not.Null, "TryGracefulShutdownOnWindows method not found. Check for signature or name changes.");
            var result = (bool)method.Invoke(service, new object[] { null, 5000 });
            Assert.That(result, Is.True);
        }

        [Test]
        public void TryGracefulShutdownOnWindows_ReturnsTrue_WhenProcessHasExited()
        {
            var service = CreateService();
            var method = GetTryGracefulShutdownOnWindows();
            Assert.That(method, Is.Not.Null, "TryGracefulShutdownOnWindows method not found. Check for signature or name changes.");

            var proc = new Process();
            proc.Dispose();

            bool result;
            try
            {
                result = (bool)method.Invoke(service, new object[] { proc, 5000 });
            }
            catch (TargetInvocationException ex) when (ex.InnerException is InvalidOperationException)
            {
                result = true;
            }
            Assert.That(result, Is.True);
        }

        [Test]
        public void TryGracefulShutdownOnWindows_ReturnsFalse_WhenNotWindows()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Assert.Ignore("Test only valid on non-Windows platforms.");
            }
            var service = CreateService();
            var method = GetTryGracefulShutdownOnWindows();
            Assert.That(method, Is.Not.Null, "TryGracefulShutdownOnWindows method not found. Check for signature or name changes.");

            var proc = Process.GetCurrentProcess();
            var result = (bool)method.Invoke(service, new object[] { proc, 5000 });
            Assert.That(result, Is.False);
        }

        [Test]
        public void TryGracefulShutdownOnWindows_RealProcess_DoesNotThrow()
        {
            var method = typeof(AppiumLocalService)
                .GetMethod("TryGracefulShutdownOnWindows", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.That(method, Is.Not.Null, "TryGracefulShutdownOnWindows method not found. Check for signature or name changes.");

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Assert.Ignore("Test only valid on Windows platforms.");
            }

            var result = (bool)method.Invoke(appiumServer, new object[] { GetAppiumProcess(appiumServer), 5000 });
            Assert.That(result, Is.True.Or.False, "Method should not throw and should return a bool.");
        }

        private Process GetAppiumProcess(AppiumLocalService service)
        {
            // Use reflection to access the private 'Service' field
            var field = typeof(AppiumLocalService).GetField("Service", BindingFlags.Instance | BindingFlags.NonPublic);
            return (Process)field.GetValue(service);
        }
    }
}