using System;
using System.Threading;
using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using static OpenQA.Selenium.Appium.iOS.IOSStartScreenRecordingOptions;

namespace Appium.Integration.Tests.iOS
{
    [TestFixture]
    class IOSScreenRecordingTest
    {
        private IOSDriver<AppiumWebElement> driver;

        [OneTimeSetUp]
        public void beforeAll()
        {
            AppiumOptions capabilities = Caps.getIos102Caps(Apps.get("iosUICatalogApp"));
            if (Env.isSauce())
            {
                capabilities.AddAdditionalCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("name", "ios - complex");
                capabilities.AddAdditionalCapability("tags", new string[] { "sample" });
            }
            Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIForIOS;
            driver = new IOSDriver<AppiumWebElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            driver.Manage().Timeouts().ImplicitWait = Env.IMPLICIT_TIMEOUT_SEC;
        }

        [OneTimeTearDown]
        public void AfterEach()
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
                GetIosStartScreenRecordingOptions()
                    .WithTimeLimit(TimeSpan.FromSeconds(10))
                    .WithVideoType(VideoType.H264));
            Thread.Sleep(1000);
            string result = driver.StopRecordingScreen();
            Assert.IsNotEmpty(result);
        }
    }
}
