using System;
using System.Threading;
using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using static OpenQA.Selenium.Appium.Android.AndroidStartScreenRecordingOptions;

namespace Appium.Integration.Tests.Android
{
    [TestFixture]
    class AndroidScreenRecordingTest
    {
        private AppiumDriver<AndroidElement> driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            AppiumOptions capabilities = Caps.getAndroid27Caps(Apps.get("androidApiDemos"));
            if (Env.isSauce())
            {
                capabilities.AddAdditionalCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("name", "android - complex");
                capabilities.AddAdditionalCapability("tags", new string[] { "sample" });
            }
            Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIAndroid;
            driver = new AndroidDriver<AndroidElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            driver.Manage().Timeouts().ImplicitWait = Env.IMPLICIT_TIMEOUT_SEC;
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            if (driver != null)
            {
                driver.Quit();
            }
            if (!Env.isSauce())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test]
        public void ScreenRecordTest()
        {
            driver.StartRecordingScreen();
            Thread.Sleep(1000);
            string result = driver.StopRecordingScreen();
            Assert.IsNotEmpty(result);
        }
        
        [Test]
        public void ScreenRecordWithOptionsTest()
        {
            driver.StartRecordingScreen(
                GetAndroidStartScreenRecordingOptions()
                    .WithTimeLimit(TimeSpan.FromSeconds(10))
                    .WithBitRate(500000)
                    .WithVideoSize("720x1280"));
            Thread.Sleep(1000);
            string result = driver.StopRecordingScreen();
            Assert.IsNotEmpty(result);
        }
    }
}
