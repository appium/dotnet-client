using System;
using System.Threading;
using Appium.Net.Integration.Tests.helpers;
using Appium.Net.Integration.Tests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.PageObjectTests.Other
{
    class AndroidJsWebViewTest
    {
        private AndroidDriver<AppiumWebElement> _driver;
        private AndroidJavaScriptTestPageObject _pageObject;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("selendroidTestApp"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("selendroidTestApp"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver<AppiumWebElement>(serverUri, capabilities, Env.InitTimeoutSec);
            _pageObject = new AndroidJavaScriptTestPageObject(_driver);
            _driver.StartActivity("io.selendroid.testapp", ".WebViewActivity");
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
        public void HighlightingByJs()
        {
            Thread.Sleep(5000);
            var contexts = _driver.Contexts;
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
            _driver.Context = webviewContext;
            _pageObject.HighlightElement();
        }
    }
}