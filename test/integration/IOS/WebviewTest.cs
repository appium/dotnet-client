using System.Linq;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Net.Integration.Tests.IOS
{
    [TestFixture]
    public class WebviewTest
    {
        private AppiumDriver _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetIosCaps(Apps.Get("iosUICatalogApp"));
            if (Env.ServerIsRemote())
            {
                capabilities.AddAdditionalAppiumOption("username", Env.GetEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalAppiumOption("accessKey", Env.GetEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalAppiumOption("name", "ios - webview");
                capabilities.AddAdditionalAppiumOption("tags", new[] {"sample"});
            }
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new IOSDriver(serverUri, capabilities, Env.InitTimeoutSec);
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
        public void GetPageTestCase()
        {
            _driver.FindElement(MobileBy.AccessibilityId("Web View")).Click();
            var contexts = _driver.Contexts;
            var webviewContext = contexts.FirstOrDefault(c => c.Contains("WEBVIEW"));
            _driver.Context = webviewContext;
            Assert.That(_driver.Title.Contains("WKWebView"));
            var pageSource = _driver.PageSource;
            Assert.That(pageSource.Contains("<title>WKWebView</title>"));
        }
    }
}