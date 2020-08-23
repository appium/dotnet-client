using System;
using System.Threading;
using Appium.Net.Integration.Tests.helpers;
using Appium.Net.Integration.Tests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.PageObjects;
using SeleniumExtras.PageObjects;

namespace Appium.Net.Integration.Tests.PageObjectTests.Android
{
    public class WebViewTest
    {
        private AndroidDriver<AppiumWebElement> _driver;
        private AndroidWebView _pageObject;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("selendroidTestApp"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("selendroidTestApp"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver<AppiumWebElement>(serverUri, capabilities, Env.InitTimeoutSec);
            var timeSpan = new TimeOutDuration(new TimeSpan(0, 0, 0, 5, 0));
            _pageObject = new AndroidWebView();
            PageFactory.InitElements(_driver, _pageObject, new AppiumPageObjectMemberDecorator(timeSpan));
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
        public void WebViewTestCase()
        {
            Thread.Sleep(5000);
            if (!Env.ServerIsRemote())
            {
                var contexts = _driver.Contexts;
                string webviewContext = null;
                for (var i = 0; i < contexts.Count; i++)
                {
                    Console.WriteLine(contexts[i]);
                    if (contexts[i].Contains("WEBVIEW"))
                    {
                        webviewContext = contexts[i];
                    }
                }
                Assert.IsNotNull(webviewContext);
                _driver.Context = webviewContext;

                _pageObject.SendMeYourName();
            }
        }
    }
}