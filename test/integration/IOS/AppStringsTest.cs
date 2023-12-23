using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Net.Integration.Tests.IOS
{
    public class AppStringsTest
    {
        private AppiumDriver _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetIosCaps(Apps.Get("iosTestApp"));
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
        public void GetAppStrings()
        {
            Assert.That(_driver.GetAppStringDictionary(), Is.Not.Empty);
        }

        [Test]
        public void GetAppStringsUsingLang()
        {
            Assert.That(_driver.GetAppStringDictionary("en"), Is.Not.Empty);
        }

        [Test]
        public void GetAppStringsUsingLangAndFileStrings()
        {
            Assert.That(_driver.GetAppStringDictionary("en", "Localizable.strings"), Is.Not.Empty);
        }
    }
}