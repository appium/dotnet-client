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
        private AppiumDriver<AndroidElement> _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetAndroidCaps(Apps.Get("androidApiDemos"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver<AndroidElement>(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            if (_driver != null)
            {
                _driver.Quit();
            }
            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test]
        public void ScreenRecordTest()
        {
            _driver.StartRecordingScreen();
            Thread.Sleep(1000);
            var result = _driver.StopRecordingScreen();
            Assert.IsNotEmpty(result);
        }
        
        [Test]
        public void ScreenRecordWithOptionsTest()
        {
            _driver.StartRecordingScreen(
                GetAndroidStartScreenRecordingOptions()
                    .WithTimeLimit(TimeSpan.FromSeconds(10))
                    .WithBitRate(500000)
                    .WithVideoSize("720x1280"));
            Thread.Sleep(1000);
            var result = _driver.StopRecordingScreen();
            Assert.IsNotEmpty(result);
        }
    }
}
