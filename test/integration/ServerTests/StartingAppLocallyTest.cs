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
        private const string _androidAppId = "io.appium.android.apis";

        [Test]
        public void StartingAndroidAppWithCapabilitiesOnlyTest()
        {
            var app = Apps.Get("androidApiDemos");
            var capabilities =
                Caps.GetAndroidUIAutomatorCaps(app);

            AndroidDriver driver = null;
            try
            {
                driver = new AndroidDriver(capabilities);
                driver.TerminateApp(_androidAppId);
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

            AndroidDriver driver = null;
            try
            {
                driver = new AndroidDriver(builder, capabilities);
                driver.TerminateApp(_androidAppId);
            }
            finally
            {
                driver?.Quit();
            }
        }

        [Test]
        public void StartingAndroidAppWithCapabilitiesOnTheServerSideTest()
        {

            var serverCapabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));

            var clientCapabilities = new AppiumOptions();
            clientCapabilities.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppPackage, "io.appium.android.apis");
            clientCapabilities.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppActivity, ".view.WebView1");
            clientCapabilities.AutomationName = AutomationName.AndroidUIAutomator2;

            var argCollector = new OptionCollector().AddCapabilities(serverCapabilities);
            var builder = new AppiumServiceBuilder().WithArguments(argCollector);

            AndroidDriver driver = null;
            try
            {
                driver = new AndroidDriver(builder, clientCapabilities);
                driver.TerminateApp(_androidAppId);
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

            IOSDriver driver = null;
            try
            {
                driver = new IOSDriver(capabilities, Env.InitTimeoutSec);
                driver.TerminateApp(Apps.GetId("iosTestApp"));
            }
            finally
            {
                driver?.Quit();
            }
        }

        [Test]
        public void StartingIosAppWithCapabilitiesAndServiceTest()
        {
            var app = Apps.Get("iosTestApp");
            var capabilities =
                Caps.GetIosCaps(app);

            var argCollector = new OptionCollector()
                .AddArguments(GeneralOptionList.OverrideSession()).AddArguments(GeneralOptionList.StrictCaps());

            var builder = new AppiumServiceBuilder().WithArguments(argCollector);
            IOSDriver driver = null;
            try
            {
                driver = new IOSDriver(builder, capabilities, Env.InitTimeoutSec);
                driver.TerminateApp(Apps.GetId("iosTestApp"));
            }
            finally
            {
                driver?.Quit();
            }
        }

        [Test]
        public void CheckThatServiceIsNotRunWhenTheCreatingOfANewSessionIsFailed()
        {
            var capabilities = Env.ServerIsRemote()
                ? //it will be a cause of error
                Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));
            capabilities.DeviceName = "iPhone Simulator";
            capabilities.AddAdditionalAppiumOption(MobileCapabilityType.PlatformName, MobilePlatform.IOS);

            var builder = new AppiumServiceBuilder();
            var service = builder.Build();
            service.Start();

            IOSDriver driver = null;
            try
            {
                try
                {
                    driver = new IOSDriver(service, capabilities);
                }
                catch (Exception e)
                {
                    Assert.That(!service.IsRunning);
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