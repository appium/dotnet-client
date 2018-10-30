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
    class IosScreenRecordingTest
    {
        private IOSDriver<AppiumWebElement> _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetIosCaps(Apps.Get("iosUICatalogApp"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new IOSDriver<AppiumWebElement>(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [OneTimeTearDown]
        public void AfterEach()
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
                GetIosStartScreenRecordingOptions()
                    .WithTimeLimit(TimeSpan.FromSeconds(10))
                    .WithVideoType(VideoType.H264));
            Thread.Sleep(1000);
            var result = _driver.StopRecordingScreen();
            Assert.IsNotEmpty(result);
        }
    }
}
