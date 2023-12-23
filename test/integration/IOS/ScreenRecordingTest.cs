using System;
using System.Threading;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Net.Integration.Tests.IOS
{
    [TestFixture]
    class ScreenRecordingTest
    {
        private IOSDriver _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetIosCaps(Apps.Get("iosUICatalogApp"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new IOSDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [OneTimeTearDown]
        public void AfterEach()
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
                IOSStartScreenRecordingOptions.GetIosStartScreenRecordingOptions()
                    .WithTimeLimit(TimeSpan.FromSeconds(10))
                    .WithVideoType(IOSStartScreenRecordingOptions.VideoType.H264)
                    .WithVideoFps("10")
                    .WithVideoScale("320:240"));
            Thread.Sleep(1000);
            var result = _driver.StopRecordingScreen();
            Assert.That(result, Is.Not.Empty);
        }
    }
}
