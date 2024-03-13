using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android
{
    [TestFixture]
    class OrientationTest
    {
        private AppiumDriver _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));
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
        public void DeviceOrientationTest()
        {
            IRotatable rotatable = _driver;
            rotatable.Orientation = ScreenOrientation.Portrait;
            Assert.That(rotatable.Orientation, Is.EqualTo(ScreenOrientation.Portrait));
        }

        [Test]
        public void RotationTest()
        {
            _driver.Orientation = ScreenOrientation.Landscape;
            Assert.That(_driver.Orientation, Is.EqualTo(ScreenOrientation.Landscape));
        }
    }
}