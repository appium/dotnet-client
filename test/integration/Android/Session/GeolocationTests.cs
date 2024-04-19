using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android.Session.Geolocation
{
    [TestFixture]
    internal class GeolocationTests
    {
        private AppiumDriver _driver;

        [SetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [TearDown]
        public void AfterEach()
        {
            _driver?.Quit();
            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test]
        public void GetLocationTest()
        {
            Assert.Multiple(() =>
            {
                Assert.That(() => _driver.Location, Is.Not.Null);
                Assert.DoesNotThrow(() => _driver.Location.ToDictionary());
                Assert.That(_driver.Location.Altitude, Is.Not.EqualTo(0));
                Assert.That(_driver.Location.Longitude, Is.Not.EqualTo(0));
                Assert.That(_driver.Location.Latitude, Is.Not.EqualTo(0));
            });
        }

        [Test]
        public void SetLocationTest()
        {
            var testLocation = new Location
            {
                Altitude = 10,
                Longitude = 10,
                Latitude = 10,
            };
            _driver.Location = testLocation;
            Assert.Multiple(() =>
            {
                Assert.That(() => _driver.Location, Is.Not.Null);
                Assert.DoesNotThrow(() => _driver.Location.ToDictionary());
                Assert.That(_driver.Location.Altitude, Is.Not.EqualTo(0));
                Assert.That(_driver.Location.Longitude, Is.Not.EqualTo(0));
                Assert.That(_driver.Location.Latitude, Is.Not.EqualTo(0));
            });
        }
    }
}