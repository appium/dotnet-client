using System.Threading;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
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
            var capabilities = Caps.GetIosCaps(Apps.Get("iosWebviewApp"));
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
            _driver.FindElement(MobileBy.XPath("//UIATextField[@value='Enter URL']"))
                .SendKeys("www.google.com");
            _driver.FindElement(MobileBy.ClassName("UIAButton")).Click();
            _driver.FindElement(MobileBy.ClassName("UIAWebView")).Click(); // dismissing keyboard
            Thread.Sleep(10000);
            _driver.Context = "WEBVIEW";
            Thread.Sleep(3000);
            var el = _driver.FindElement(MobileBy.ClassName("gsfi"));
            el.SendKeys("Appium");
            el.SendKeys(Keys.Return);
            Thread.Sleep(1000);
            Assert.That(_driver.Title.Contains("Appium"));
        }
    }
}