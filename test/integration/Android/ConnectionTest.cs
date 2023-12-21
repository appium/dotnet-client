using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android
{
    [TestFixture]
    class ConnectionTest
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
        public void NetworkConnectionTest()
        {
            ((AndroidDriver) _driver).ConnectionType = ConnectionType.AirplaneMode;
            Assert.That(((AndroidDriver) _driver).ConnectionType, Is.EqualTo(ConnectionType.AirplaneMode));

            ((AndroidDriver) _driver).ConnectionType = ConnectionType.AllNetworkOn;
            Assert.That(((AndroidDriver) _driver).ConnectionType, Is.EqualTo(ConnectionType.AllNetworkOn));
        }
    }
}