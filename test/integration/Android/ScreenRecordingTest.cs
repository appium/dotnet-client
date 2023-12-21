using System;
using System.Threading;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android
{
    [TestFixture]
    class ScreenRecordingTest
    {
        private AppiumDriver _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }


        [OneTimeTearDown]
        public void AfterAll()
        {
            _driver?.Quit();
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
            Assert.That(result, Is.Not.Empty);
        }
        
        [Test]
        public void ScreenRecordWithOptionsTest()
        {
            _driver.StartRecordingScreen(
                AndroidStartScreenRecordingOptions.GetAndroidStartScreenRecordingOptions()
                    .WithTimeLimit(TimeSpan.FromSeconds(10))
                    .WithBitRate(500000)
                    .WithVideoSize("720x1280"));
            Thread.Sleep(1000);
            var result = _driver.StopRecordingScreen();
            Assert.That(result, Is.Not.Empty);
        }
    }
}
