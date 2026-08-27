using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android
{
    [TestFixture]
    [Category("Device")]
    public class ImeTest
    {
        private AndroidDriver _driver;
        private readonly string _appId = Apps.GetId(Apps.androidApiDemos);

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetAndroidUIAutomatorCaps(Apps.Get(Apps.androidApiDemos));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _driver?.TerminateApp(_appId);
            _driver?.Quit();
        }

        [Test]
        public void GetIMEAvailableEnginesShouldReturnTheInstalledEngines()
        {
            var engines = _driver.GetIMEAvailableEngines();

            Assert.That(engines, Is.Not.Null);
            Assert.That(engines, Is.Not.Empty, "An Android device is expected to ship with at least one IME");
            Assert.That(engines, Has.All.Matches<string>(engine => !string.IsNullOrWhiteSpace(engine)));
        }

        [Test]
        public void GetIMEActiveEngineShouldBeOneOfTheAvailableEngines()
        {
            var engines = _driver.GetIMEAvailableEngines();
            var activeEngine = _driver.GetIMEActiveEngine();

            Assert.That(activeEngine, Is.Not.Null.And.Not.Empty);
            Assert.That(engines, Does.Contain(activeEngine));
        }
    }
}
