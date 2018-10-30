using System.Threading;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Net.Integration.Tests.iOS
{
    [TestFixture]
    public class iOSWebviewTest
    {
        private AppiumDriver<IWebElement> _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetIosCaps(Apps.Get("iosWebviewApp"));
            if (Env.ServerIsRemote())
            {
                capabilities.AddAdditionalCapability("username", Env.GetEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.GetEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("name", "ios - webview");
                capabilities.AddAdditionalCapability("tags", new[] {"sample"});
            }
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new IOSDriver<IWebElement>(serverUri, capabilities, Env.InitTimeoutSec);
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