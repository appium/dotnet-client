using Appium.Samples.Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;
using OpenQA.Selenium.Remote;

namespace Appium.Samples.ServerTests
{
    class StartingAppLocallyTest
    {
        [Test]
        public void StartingAndroidAppWithCapabilitiesOnlyTest()
        {
            string app = Apps.get("androidApiDemos");
            DesiredCapabilities capabilities =
                Caps.getAndroid19Caps(app);

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
            string app = Apps.get("androidApiDemos");

            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability(MobileCapabilityType.DeviceName, "Android Emulator");

            OptionCollector argCollector = new OptionCollector().AddArguments(GeneralOptionList.App(app))
                .AddArguments(GeneralOptionList.AutomationName(AutomationName.Appium));
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
        public void StartingIOSAppWithCapabilitiesOnlyTest()
        {
            string app = Apps.get("iosTestApp");
            DesiredCapabilities capabilities =
                Caps.getIos82Caps(app);

            IOSDriver<AppiumWebElement> driver = null;
            try
            {
                driver = new IOSDriver<AppiumWebElement>(capabilities);
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

            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability(MobileCapabilityType.DeviceName, "iPhone Simulator");

            OptionCollector argCollector = new OptionCollector().AddArguments(GeneralOptionList.App(app))
                .AddArguments(GeneralOptionList.AutomationName(AutomationName.Appium)).
                AddArguments(GeneralOptionList.PlatformVersion("8.2"));

            AppiumServiceBuilder builder = new AppiumServiceBuilder().WithArguments(argCollector);
            IOSDriver<AppiumWebElement> driver = null;
            try
            {
                driver = new IOSDriver<AppiumWebElement>(builder, capabilities); ;
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
    }
}
