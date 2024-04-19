using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Appium.Net.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Exceptions;
using OpenQA.Selenium.Appium.Service.Options;

namespace Appium.Net.Integration.Tests.ServerTests
{
    [TestFixture]
    public class AppiumLocalServerLaunchingTest
    {
        private string _pathToAppiumPackageIndex;
        private string _testIp;

        [OneTimeSetUp]
        public void BeforeAll()
        {

            IPHostEntry host;
            var hostName = Dns.GetHostName();
            host = Dns.GetHostEntry(hostName);
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    _testIp = ip.ToString();
                    break;
                }
            }
            Console.WriteLine(_testIp);

            _pathToAppiumPackageIndex = new Paths().PathToAppiumPackageIndex;
        }

        [Test]
        public void CheckAbilityToBuildDefaultService()
        {
            var service = AppiumLocalService.BuildDefaultService();

            try
            {
                service.Start();
                Assert.That(service.IsRunning, Is.EqualTo(true));
            }
            finally
            {
                service.Dispose();
            }
        }

        [Test]
        public void CheckAbilityToLogServiceOutputToConsole()
        {
            var service = AppiumLocalService.BuildDefaultService();
            var lines = new List<string>();

            ManualResetEvent dataReceivedEvent = new ManualResetEvent(false);

            service.OutputDataReceived += (sender, e) =>
            {
                lines.Add(e.Data);
                Console.Out.WriteLine(e.Data);
                dataReceivedEvent.Set();
            };

            try
            {
                service.Start();
                Assert.That(service.IsRunning, Is.EqualTo(true));
                dataReceivedEvent.WaitOne(TimeSpan.FromSeconds(10));
            }
            finally
            {
                service.Dispose();
            }

            Assert.That(lines, Is.Not.Empty);
        }

        [Test]
        public void CheckAbilityToBuildServiceUsingNodeDefinedInProperties()
        {
            AppiumLocalService service = null;
            try
            {
                var definedNode = _pathToAppiumPackageIndex;
                Environment.SetEnvironmentVariable(AppiumServiceConstants.AppiumBinaryPath, definedNode);
                service = AppiumLocalService.BuildDefaultService();
                service.Start();
                Assert.That(service.IsRunning, Is.EqualTo(true));
            }
            finally
            {
                service?.Dispose();
                Environment.SetEnvironmentVariable(AppiumServiceConstants.AppiumBinaryPath, string.Empty);
            }
        }

        [Test]
        public void CheckAbilityToBuildServiceUsingNodeDefinedExplicitly()
        {
            AppiumLocalService service = null;
            try
            {
                service = new AppiumServiceBuilder().WithAppiumJS(new FileInfo(_pathToAppiumPackageIndex)).Build();
                service.Start();
                Assert.That(service.IsRunning, Is.EqualTo(true));
            }
            finally
            {
                service?.Dispose();
            }
        }

        [Test]
        public void CheckAbilityToStartServiceOnAFreePort()
        {
            AppiumLocalService service = null;
            try
            {
                service = new AppiumServiceBuilder().UsingAnyFreePort().Build();
                service.Start();
                Assert.That(service.IsRunning, Is.EqualTo(true));
            }
            finally
            {
                service?.Dispose();
            }
        }

        [Test]
        public void CheckStartingOfAServiceWithNonLocalhostIp()
        {
            var service = new AppiumServiceBuilder().WithIPAddress(_testIp).UsingPort(4000).Build();
            try
            {
                service.Start();
                Assert.That(service.IsRunning);
            }
            finally
            {
                service.Dispose();
            }
        }

        [Test]
        public void CheckAbilityToStartServiceUsingFlags()
        {
            AppiumLocalService service = null;
            var args = new OptionCollector().AddArguments(GeneralOptionList.CallbackAddress(_testIp))
                .AddArguments(GeneralOptionList.OverrideSession());
            try
            {
                service = new AppiumServiceBuilder().WithArguments(args).Build();
                service.Start();
                Assert.That(service.IsRunning);
            }
            finally
            {
                service?.Dispose();
            }
        }

        [Test]
        public void CheckAbilityToStartServiceUsingBasePathFlag()
        {
            AppiumLocalService service = null;
            var args = new OptionCollector().AddArguments(GeneralOptionList.BasePath("/wd/hub"));
            try
            {
                service = new AppiumServiceBuilder().WithArguments(args).Build();
                service.Start();
                Assert.That(service.IsRunning);
            }
            finally
            {
                service?.Dispose();
            }
        }


        [Test]
        public void CheckAbilityToStartServiceUsingCapabilities()
        {
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalAppiumOption(MobileCapabilityType.PlatformName, "Android");
            capabilities.AddAdditionalAppiumOption(MobileCapabilityType.FullReset, true);
            capabilities.AddAdditionalAppiumOption(MobileCapabilityType.NewCommandTimeout, 60);
            capabilities.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppPackage, "io.appium.android.apis");
            capabilities.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppActivity, ".view.WebView1");

            var args = new OptionCollector().AddCapabilities(capabilities);
            AppiumLocalService service = null;
            try
            {
                service = new AppiumServiceBuilder().WithArguments(args).Build();
                service.Start();
                Assert.That(service.IsRunning);
            }
            finally
            {
                service?.Dispose();
            }
        }

        [Test]
        public void CheckAbilityToStartServiceUsingCapabilitiesAndFlags()
        {
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalAppiumOption(MobileCapabilityType.PlatformName, "Android");
            capabilities.AddAdditionalAppiumOption(MobileCapabilityType.FullReset, true);
            capabilities.AddAdditionalAppiumOption(MobileCapabilityType.NewCommandTimeout, 60);
            capabilities.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppPackage, "io.appium.android.apis");
            capabilities.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppActivity, ".view.WebView1");

            var args = new OptionCollector().AddCapabilities(capabilities)
                .AddArguments(GeneralOptionList.CallbackAddress(_testIp))
                .AddArguments(GeneralOptionList.OverrideSession());
            AppiumLocalService service = null;
            try
            {
                service = new AppiumServiceBuilder().WithArguments(args).Build();
                service.Start();
                Assert.That(service.IsRunning);
            }
            finally
            {
                service?.Dispose();
            }
        }

        [Test]
        public void CheckAbilityToShutDownService()
        {
            var service = AppiumLocalService.BuildDefaultService();
            service.Start();
            service.Dispose();
            Assert.That(!service.IsRunning);
        }

        [Test]
        public void CheckAbilityToStartAndShutDownFewServices()
        {
            var service1 = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            var service2 = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            var service3 = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            var service4 = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            service1.Start();
            service2.Start();
            service3.Start();
            service4.Start();
            service1.Dispose();
            Thread.Sleep(1000);
            service2.Dispose();
            Thread.Sleep(1000);
            service3.Dispose();
            Thread.Sleep(1000);
            service4.Dispose();
            Assert.Multiple(() =>
            {
                Assert.That(!service1.IsRunning);
                Assert.That(!service2.IsRunning);
                Assert.That(!service3.IsRunning);
                Assert.That(!service4.IsRunning);
            });
        }


        [Test]
        public void CheckTheAbilityToDefineTheDesiredLogFile()
        {
            var log = new FileInfo("Log.txt");
            var service = new AppiumServiceBuilder().WithLogFile(log).Build();
            try
            {
                service.Start();
                Assert.Multiple(() =>
                {
                    Assert.That(log.Exists, Is.True);
                    Assert.That(log.Length, Is.GreaterThan(0)); //There should be Appium greeting messages
                });
            }
            finally
            {
                service.Dispose();
                if (log.Exists)
                {
                    File.Delete(log.FullName);
                }
                service.Dispose();
            }
        }

        [Test]
        public void CheckAbilityToSetNodeArguments()
        {
            var service = new AppiumServiceBuilder()
                .WithStartUpTimeOut(TimeSpan.FromMilliseconds(500)) // we expect the Appium startup to fail, so fail quickly
                .WithNodeArguments("--version") // show Node version and exit
                .Build();

            var nodeOutput = new StringBuilder();
            try
            {
                service.OutputDataReceived += (o, args) => nodeOutput.AppendLine(args.Data);
                service.Start();
            }
            catch (AppiumServerHasNotBeenStartedLocallyException)
            {
                // expected exception, ignore
            }
            finally
            {
                service.Dispose();
            }

            Assert.That(nodeOutput.ToString(), Does.Match(@"v\d+\.\d+\.\d+"));
        }

        [Test]
        public void AttemptingToSetNodeArgumentsToNullThrowsException()
        {
            var serviceBuilder = new AppiumServiceBuilder();
            string[] nullArray = null;
            Assert.Throws<ArgumentNullException>(() => serviceBuilder.WithNodeArguments(nullArray));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("\n")]
        [TestCase("\t")]
        public void AddingInvalidNodeArgumentThrowsException(string argument)
        {
            var serviceBuilder = new AppiumServiceBuilder();
            Assert.Throws<ArgumentException>(() => serviceBuilder.WithNodeArguments(argument));
        }
    }
}