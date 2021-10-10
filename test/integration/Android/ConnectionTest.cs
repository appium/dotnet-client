using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android
{
    [TestFixture]
    class ConnectionTest
    {
        private AppiumDriver<IWebElement> _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver<IWebElement>(serverUri, capabilities, Env.InitTimeoutSec);
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
            ((AndroidDriver<IWebElement>) _driver).ConnectionType = ConnectionType.AirplaneMode;
            Assert.AreEqual(ConnectionType.AirplaneMode, ((AndroidDriver<IWebElement>) _driver).ConnectionType);

            ((AndroidDriver<IWebElement>) _driver).ConnectionType = ConnectionType.AllNetworkOn;
            Assert.AreEqual(ConnectionType.AllNetworkOn, ((AndroidDriver<IWebElement>) _driver).ConnectionType);
        }
    }
}