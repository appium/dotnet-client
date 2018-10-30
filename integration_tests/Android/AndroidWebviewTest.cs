using System;
using System.Threading;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;

namespace Appium.Net.Integration.Tests.Android
{
    [TestFixture]
    public class AndroidWebviewTest
    {
        private IWebDriver _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidCaps(Apps.Get("selendroidTestApp"))
                : Caps.GetAndroidCaps(Apps.Get("selendroidTestApp"));
            if (Env.ServerIsRemote())
            {
                capabilities.AddAdditionalCapability("username", Env.GetEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.GetEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("name", "android - webview");
                capabilities.AddAdditionalCapability("tags", new[] {"sample"});
            }
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "io.selendroid.testapp");
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, ".WebViewActivity");
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver<IWebElement>(serverUri, capabilities, Env.InitTimeoutSec);
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
        public void WebViewTestCase()
        {
            Thread.Sleep(5000);
            var contexts = ((IContextAware) _driver).Contexts;
            string webviewContext = null;
            for (var i = 0; i < contexts.Count; i++)
            {
                Console.WriteLine(contexts[i]);
                if (contexts[i].Contains("WEBVIEW"))
                {
                    webviewContext = contexts[i];
                    break;
                }
            }
            Assert.IsNotNull(webviewContext);
            ((IContextAware) _driver).Context = webviewContext;
            Assert.IsTrue(_driver.PageSource.Contains("Hello, can you please tell me your name?"));
        }
    }
}