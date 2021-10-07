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
                capabilities.AddAdditionalOption("username", Env.GetEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalOption("accessKey", Env.GetEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalOption("name", "ios - webview");
                capabilities.AddAdditionalOption("tags", new[] {"sample"});
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
            _driver.FindElementByXPath("//UIATextField[@value='Enter URL']")
                .SendKeys("www.google.com");
            _driver.FindElementByClassName("UIAButton").Click();
            _driver.FindElementByClassName("UIAWebView").Click(); // dismissing keyboard
            Thread.Sleep(10000);
            _driver.Context = "WEBVIEW";
            Thread.Sleep(3000);
            var el = _driver.FindElementByClassName("gsfi");
            el.SendKeys("Appium");
            el.SendKeys(Keys.Return);
            Thread.Sleep(1000);
            Assert.IsTrue(_driver.Title.Contains("Appium"));
        }
    }
}