using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Android.Enums;
using OpenQA.Selenium.Remote;
using System;

namespace Appium.Integration.Tests.Android
{
    class AndroidKeyPressTest
    {
        private AndroidDriver<AndroidElement> driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            AppiumOptions capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidCaps(Apps.get("androidApiDemos"))
                : Caps.GetAndroidCaps(Apps.get("androidApiDemos"));
            if (Env.ServerIsRemote())
            {
                capabilities.AddAdditionalCapability("username", Env.GetEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.GetEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("name", "android - simple");
                capabilities.AddAdditionalCapability("tags", new string[] {"sample"});
            }
            Uri serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            driver = new AndroidDriver<AndroidElement>(serverUri, capabilities, Env.InitTimeoutSec);
            driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [SetUp]
        public void SetUp()
        {
            if (driver != null)
            {
                driver.ResetApp();
            }
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            if (driver != null)
            {
                driver.Quit();
            }
            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test]
        public void PressKeyCodeTest()
        {
            driver.PressKeyCode(AndroidKeyCode.Home);
        }

        [Test]
        public void PressKeyCodeWithMetastateTest()
        {
            driver.PressKeyCode(AndroidKeyCode.Space, AndroidKeyMetastate.Meta_Shift_On);
        }

        [Test]
        public void LongPressKeyCodeTest()
        {
            driver.LongPressKeyCode(AndroidKeyCode.Home);
        }

        [Test]
        public void LongPressKeyCodeWithMetastateTest()
        {
            driver.LongPressKeyCode(AndroidKeyCode.Space, AndroidKeyMetastate.Meta_Shift_On);
        }
    }
}