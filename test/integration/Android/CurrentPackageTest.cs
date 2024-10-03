using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;

namespace Appium.Net.Integration.Tests.Android
{
    [TestFixture]
    public class CurrentPackageTest
    {
        private AndroidDriver _driver;
        private WebDriverWait _waitDriver;
        private readonly TimeSpan _driverTimeOut = TimeSpan.FromSeconds(5);

        private const string DemoAppPackage = "io.appium.android.apis";

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));

            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
            _driver.TerminateApp(DemoAppPackage);
        }

        [SetUp]
        public void SetUp()
        {
            _driver?.ActivateApp(DemoAppPackage);
        }

        [OneTimeTearDown]
        public void TearDowwn()
        {
            _ = (_driver?.TerminateApp(DemoAppPackage));
            _driver?.Quit();
        }

        [Test]
        public void ReturnsCorrectNameForCurrentApp()
        {
            _waitDriver = new WebDriverWait(_driver, _driverTimeOut);
            _waitDriver.Until(driver => _driver.CurrentPackage == DemoAppPackage);
            Assert.That(_driver.CurrentPackage, Is.EqualTo(DemoAppPackage));
        }
    }
}
