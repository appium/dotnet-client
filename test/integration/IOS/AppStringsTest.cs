using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
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
            Assert.AreNotSame(0, _driver.GetAppStringDictionary().Count);
        }

        [Test]
        public void GetAppStringsUsingLang()
        {
            Assert.AreNotSame(0, _driver.GetAppStringDictionary("en").Count);
        }

        [Test]
        public void GetAppStringsUsingLangAndFileStrings()
        {
            Assert.AreNotSame(0, _driver.GetAppStringDictionary("en", "Localizable.strings").Count);
        }
    }
}