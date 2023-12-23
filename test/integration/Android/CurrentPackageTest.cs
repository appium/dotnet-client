using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android
{
    [TestFixture]
    public class CurrentPackageTest
    {
        private AndroidDriver _driver;
        private const string DemoAppPackage = "io.appium.android.apis";

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));

            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
            _driver.CloseApp();
        }

        [SetUp]
        public void SetUp()
        {
            _driver?.LaunchApp();
        }

        [OneTimeTearDown]
        public void TearDowwn()
        {
            _driver?.CloseApp();
            _driver?.Quit();
        }

        [Test]
        public void ReturnsCorrectNameForCurrentApp()
        {
            Assert.That(_driver.CurrentPackage, Is.EqualTo(DemoAppPackage));
        }
    }
}
