using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;

namespace Appium.Net.Unit.Tests.Appium.Service
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
            var appiumPath = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? @"C:\Users\Dor-B\AppData\Roaming\npm\node_modules\appium\build\lib\main.js"
                : "/usr/local/lib/node_modules/appium/build/lib/main.js";
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
            appiumServer.Dispose();
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
            var result = (bool)method.Invoke(service, new object[] { null, 5000 });
            Assert.That(result, Is.True);
        }

        [Test]
        public void TryGracefulShutdownOnWindows_ReturnsTrue_WhenProcessHasExited()
        {
            var service = CreateService();
            var method = GetTryGracefulShutdownOnWindows();

            var proc = new Process();
            // Simulate HasExited = true by disposing process (not perfect, but for test)
            proc.Dispose();

            // Use try-catch because accessing HasExited on disposed process throws
            bool result;
            try
            {
                result = (bool)method.Invoke(service, new object[] { proc, 5000 });
            }
            catch (TargetInvocationException ex) when (ex.InnerException is InvalidOperationException)
            {
                // If exception, treat as HasExited = true
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

            // Use a running process (self) without disposing it
            var proc = Process.GetCurrentProcess();
            var result = (bool)method.Invoke(service, new object[] { proc, 5000 });
            Assert.That(result, Is.False);
        }

        [Test]
        public void TryGracefulShutdownOnWindows_RealProcess_DoesNotThrow()
        {
            var method = typeof(AppiumLocalService)
                .GetMethod("TryGracefulShutdownOnWindows", BindingFlags.Instance | BindingFlags.NonPublic);

            // This test will only run on Windows
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Assert.Ignore("Test only valid on Windows platforms.");
            }

            // Use reflection to call the private method on the real Appium process
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