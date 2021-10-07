using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Net.Integration.Tests.iOS
{
    public class SettingTest
    {
        private IOSDriver _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetIosCaps(Apps.Get("iosUICatalogApp"));
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
        public void SettingsUpdateTest()
        {
            _driver.SetSetting(
                setting: "useJSONSource",
                value: true);

            Assert.IsTrue((bool)_driver.Settings["useJSONSource"]);

            _driver.SetSetting(
                setting: "useJSONSource",
                value: false);

            Assert.IsFalse((bool)_driver.Settings["useJSONSource"]);
        }
    }
}