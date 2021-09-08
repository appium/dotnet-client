using System;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;

namespace Appium.Net.Integration.Tests.ServerTests
{
    class StartingAppLocallyTest
    {
        [Test]
        public void StartingAndroidAppWithCapabilitiesOnlyTest()
        {
            var app = Apps.Get("androidApiDemos");
            var capabilities =
                Caps.GetAndroidUIAutomatorCaps(app);

            AndroidDriver<AppiumWebElement> driver = null;
            try
            {
                driver = new AndroidDriver<AppiumWebElement>(capabilities);
                driver.CloseApp();
            }
            finally
            {
                driver?.Quit();
            }
        }

        [Test]
        public void StartingAndroidAppWithCapabilitiesAndServiceTest()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));


            var argCollector = new OptionCollector()
                .AddArguments(GeneralOptionList.OverrideSession()).AddArguments(GeneralOptionList.StrictCaps());
            var builder = new AppiumServiceBuilder().WithArguments(argCollector);

            AndroidDriver<AppiumWebElement> driver = null;
            try
            {
                driver = new AndroidDriver<AppiumWebElement>(builder, capabilities);
                driver.CloseApp();
            }
            finally
            {
                driver?.Quit();
            }
        }


        [Test]
        public void StartingAndroidAppWithCapabilitiesOnTheServerSideTest()
        {
            var app = Apps.Get("androidApiDemos");

            var serverCapabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));

            var clientCapabilities = new AppiumOptions();
            clientCapabilities.AddAdditionalOption(AndroidMobileCapabilityType.AppPackage, "io.appium.android.apis");
            clientCapabilities.AddAdditionalOption(AndroidMobileCapabilityType.AppActivity, ".view.WebView1");

            var argCollector = new OptionCollector().AddCapabilities(serverCapabilities);
            var builder = new AppiumServiceBuilder().WithArguments(argCollector);

            AndroidDriver<AppiumWebElement> driver = null;
            try
            {
                driver = new AndroidDriver<AppiumWebElement>(builder, clientCapabilities);
                driver.CloseApp();
            }
            finally
            {
                driver?.Quit();
            }
        }

        [Test]
        public void StartingIosAppWithCapabilitiesOnlyTest()
        {
            var app = Apps.Get("iosTestApp");
            var capabilities =
                Caps.GetIosCaps(app);

            IOSDriver<AppiumWebElement> driver = null;
            try
            {
                driver = new IOSDriver<AppiumWebElement>(capabilities, Env.InitTimeoutSec);
                driver.CloseApp();
            }
            finally
            {
                driver?.Quit();
            }
        }

        [Test]
        public void StartingIosAppWithCapabilitiesAndServiseTest()
        {
            var app = Apps.Get("iosTestApp");
            var capabilities =
                Caps.GetIosCaps(app);

            var argCollector = new OptionCollector()
                .AddArguments(GeneralOptionList.OverrideSession()).AddArguments(GeneralOptionList.StrictCaps());

            var builder = new AppiumServiceBuilder().WithArguments(argCollector);
            IOSDriver<AppiumWebElement> driver = null;
            try
            {
                driver = new IOSDriver<AppiumWebElement>(builder, capabilities, Env.InitTimeoutSec);
                driver.CloseApp();
            }
            finally
            {
                driver?.Quit();
            }
        }

        [Test]
        public void CheckThatServiseIsNotRunWhenTheCreatingOfANewSessionIsFailed()
        {
            var capabilities = Env.ServerIsRemote()
                ? //it will be a cause of error
                Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));
            capabilities.AddAdditionalOption(MobileCapabilityType.DeviceName, "iPhone Simulator");
            capabilities.AddAdditionalOption(MobileCapabilityType.PlatformName, MobilePlatform.IOS);

            var builder = new AppiumServiceBuilder();
            var service = builder.Build();
            service.Start();

            IOSDriver<AppiumWebElement> driver = null;
            try
            {
                try
                {
                    driver = new IOSDriver<AppiumWebElement>(service, capabilities);
                }
                catch (Exception e)
                {
                    Assert.IsTrue(!service.IsRunning);
                    return;
                }
                throw new Exception("Any exception was expected");
            }
            finally
            {
                driver?.Quit();
            }
        }
    }
}