using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;
using OpenQA.Selenium.Remote;
using System;

namespace Appium.Integration.Tests.ServerTests
{
    class StartingAppLocallyTest
    {
        [Test]
        public void StartingAndroidAppWithCapabilitiesOnlyTest()
        {
            string app = Apps.get("androidApiDemos");
            AppiumOptions capabilities =
                Caps.GetAndroidCaps(app);

            AndroidDriver<AppiumWebElement> driver = null;
            try
            {
                driver = new AndroidDriver<AppiumWebElement>(capabilities);
                driver.CloseApp();
            }
            finally
            {
                if (driver != null)
                {
                    driver.Quit();
                }
            }
        }

        [Test]
        public void StartingAndroidAppWithCapabilitiesAndServiceTest()
        {
            AppiumOptions capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidCaps(Apps.get("androidApiDemos"))
                : Caps.GetAndroidCaps(Apps.get("androidApiDemos"));


            OptionCollector argCollector = new OptionCollector()
                .AddArguments(GeneralOptionList.OverrideSession()).AddArguments(GeneralOptionList.StrictCaps());
            AppiumServiceBuilder builder = new AppiumServiceBuilder().WithArguments(argCollector);

            AndroidDriver<AppiumWebElement> driver = null;
            try
            {
                driver = new AndroidDriver<AppiumWebElement>(builder, capabilities);
                driver.CloseApp();
            }
            finally
            {
                if (driver != null)
                {
                    driver.Quit();
                }
            }
        }


        [Test]
        public void StartingAndroidAppWithCapabilitiesOnTheServerSideTest()
        {
            string app = Apps.get("androidApiDemos");

            AppiumOptions serverCapabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidCaps(Apps.get("androidApiDemos"))
                : Caps.GetAndroidCaps(Apps.get("androidApiDemos"));

            AppiumOptions clientCapabilities = new AppiumOptions();
            clientCapabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "io.appium.android.apis");
            clientCapabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, ".view.WebView1");

            OptionCollector argCollector = new OptionCollector().AddCapabilities(serverCapabilities);
            AppiumServiceBuilder builder = new AppiumServiceBuilder().WithArguments(argCollector);

            AndroidDriver<AppiumWebElement> driver = null;
            try
            {
                driver = new AndroidDriver<AppiumWebElement>(builder, clientCapabilities);
                driver.CloseApp();
            }
            finally
            {
                if (driver != null)
                {
                    driver.Quit();
                }
            }
        }

        [Test]
        public void StartingIOSAppWithCapabilitiesOnlyTest()
        {
            string app = Apps.get("iosTestApp");
            AppiumOptions capabilities =
                Caps.GetIOSCaps(app);

            IOSDriver<AppiumWebElement> driver = null;
            try
            {
                driver = new IOSDriver<AppiumWebElement>(capabilities, Env.InitTimeoutSec);
                driver.CloseApp();
            }
            finally
            {
                if (driver != null)
                {
                    driver.Quit();
                }
            }
        }

        [Test]
        public void StartingIOSAppWithCapabilitiesAndServiseTest()
        {
            string app = Apps.get("iosTestApp");
            AppiumOptions capabilities =
                Caps.GetIOSCaps(app);

            OptionCollector argCollector = new OptionCollector()
                .AddArguments(GeneralOptionList.OverrideSession()).AddArguments(GeneralOptionList.StrictCaps());

            AppiumServiceBuilder builder = new AppiumServiceBuilder().WithArguments(argCollector);
            IOSDriver<AppiumWebElement> driver = null;
            try
            {
                driver = new IOSDriver<AppiumWebElement>(builder, capabilities, Env.InitTimeoutSec);
                driver.CloseApp();
            }
            finally
            {
                if (driver != null)
                {
                    driver.Quit();
                }
            }
        }

        [Test]
        public void CheckThatServiseIsNotRunWhenTheCreatingOfANewSessionIsFailed()
        {
            AppiumOptions capabilities = Env.ServerIsRemote()
                ? //it will be a cause of error
                Caps.GetAndroidCaps(Apps.get("androidApiDemos"))
                : Caps.GetAndroidCaps(Apps.get("androidApiDemos"));
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "iPhone Simulator");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, MobilePlatform.IOS);

            AppiumServiceBuilder builder = new AppiumServiceBuilder();
            AppiumLocalService service = builder.Build();
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
                if (driver != null)
                {
                    driver.Quit();
                }
            }
        }
    }
}