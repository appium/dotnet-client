﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;
using System;
using System.IO;
using System.Net;

namespace Appium.Samples.ServerTests
{
    [TestFixture()]
    public class AppiumLocalServerLaunchingTest
    {
        private string PathToCustomizedAppiumJS;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            byte[] bytes = null;

            bool isWindows = Platform.CurrentPlatform.IsPlatformType(PlatformType.Windows);
            bool isMacOS = Platform.CurrentPlatform.IsPlatformType(PlatformType.Mac);
            bool isLinux = Platform.CurrentPlatform.IsPlatformType(PlatformType.Linux);

            if (isWindows)
            {
                bytes = Properties.Resources.PathToWindowsNode;
            }
            if (isMacOS)
            {
                bytes = Properties.Resources.PathToMacOSNode;
            }
            if (isLinux)
            {
                bytes = Properties.Resources.PathToLinuxNode;
            }

            PathToCustomizedAppiumJS = System.Text.Encoding.UTF8.GetString(bytes);
        }

        [Test]
        public void CheckAbilityToBuildDefaultService()
        {
            AppiumLocalService.BuildDefaultService();
        }

        [Test]
        public void CheckAbilityToBuildServiceWithDefinedParametersAndNodeSetInProperties()
        {
            try
            {
                string definedNode = PathToCustomizedAppiumJS;
                Environment.SetEnvironmentVariable(AppiumServiceConstants.AppiumBinaryPath, definedNode);
                OptionCollector args = new OptionCollector().AddArguments(GeneralOptionList.OverrideSession());
                new AppiumServiceBuilder().UsingPort(4000).WithArguments(args).Build();
            }
            finally
            {

                Environment.SetEnvironmentVariable(AppiumServiceConstants.AppiumBinaryPath, string.Empty);
            }
        }

        [Test]
        public void CheckAbilityToBuildServiceWithDefinedParametersAndExternallyDefinedNode()
        {
            OptionCollector args = new OptionCollector().AddArguments(GeneralOptionList.OverrideSession());
            new AppiumServiceBuilder().WithAppiumJS(new FileInfo(PathToCustomizedAppiumJS)).
                    UsingPort(4000).WithArguments(args).Build();
        }

        [Test]
        public void CheckStartingOfDefaultService()
        {
            AppiumLocalService service = AppiumLocalService.BuildDefaultService();
            service.Start();
            Assert.IsTrue(service.IsRunning);
            service.Dispose();
        }

        [Test]
        public void CheckAbilityToStartServiceOnAFreePort()
        {
            AppiumLocalService service = new AppiumServiceBuilder().UsingAnyFreePort().WithOpenedWindow(true).Build();
            service.Start();
            Assert.IsTrue(service.IsRunning);
            service.Dispose();
        }

        [Test]
        public void CheckStartingOfAServiceWithNonDefaultArguments()
        {
            OptionCollector args = new OptionCollector().AddArguments(GeneralOptionList.LogNoColors());
            AppiumLocalService service = new AppiumServiceBuilder().UsingPort(4000).WithArguments(args).
                WithOpenedWindow(true).Build();
            service.Start();
            Assert.IsTrue(service.IsRunning);
            service.Dispose();
        }

        [Test]
        public void CheckStartingOfTheServiceDefinedByProperty()
        {
            try
            {
                string definedNode = PathToCustomizedAppiumJS;
                Environment.SetEnvironmentVariable(AppiumServiceConstants.AppiumBinaryPath, definedNode);
                AppiumLocalService service = new AppiumServiceBuilder().WithOpenedWindow(true).Build();

                service.Start();
                Assert.IsTrue(service.IsRunning);
                service.Dispose();
            }
            finally
            {
                Environment.SetEnvironmentVariable(AppiumServiceConstants.AppiumBinaryPath, string.Empty);
            }
        }

        [Test]
        public void CheckStartingOfTheServiceDefinedExternally()
        {
            AppiumLocalService service = new AppiumServiceBuilder().WithAppiumJS(new FileInfo(PathToCustomizedAppiumJS)).
                WithOpenedWindow(true).Build();
            service.Start();
            Assert.IsTrue(service.IsRunning);
            service.Dispose();
        }

        [Test]
        public void CheckStartingOfTheServiceDefinedExternallyWithNonDefaultArguments()
        {
            OptionCollector args = new OptionCollector().AddArguments(GeneralOptionList.LogNoColors());
            AppiumLocalService service = new AppiumServiceBuilder().WithAppiumJS(new FileInfo(PathToCustomizedAppiumJS)).
                UsingPort(4000).WithArguments(args).WithOpenedWindow(true).Build();
            service.Start();
            Assert.IsTrue(service.IsRunning);
            service.Dispose();
        }

        [Test]
        public void CheckStartingOfAServiceWithNonLocalhostIP()
        {

            IPHostEntry host;
            string localIp = "?";
            string hostName = Dns.GetHostName();
            host = Dns.GetHostEntry(hostName);
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIp = ip.ToString();
                    break;
                }
            }
            Console.WriteLine(localIp);

            AppiumLocalService service = new AppiumServiceBuilder().WithIPAddress(localIp).UsingPort(4000).
                WithOpenedWindow(true).
                Build();
            service.Start();
            Assert.IsTrue(service.IsRunning);
            service.Dispose();
        }
    }
}
