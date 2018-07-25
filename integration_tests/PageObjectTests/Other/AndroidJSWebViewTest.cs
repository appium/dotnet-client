using Appium.Integration.Tests.Helpers;
using Appium.Integration.Tests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace Appium.Integration.Tests.PageObjectTests.Other
{
    class AndroidJSWebViewTest
    {
        private AndroidDriver<AppiumWebElement> driver;
        private AndroidJavaScriptTestPageObject pageObject;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            DesiredCapabilities capabilities = Env.isSauce()
                ? Caps.getAndroid501Caps(Apps.get("selendroidTestApp"))
                : Caps.getAndroid19Caps(Apps.get("selendroidTestApp"));
            if (Env.isSauce())
            {
                capabilities.SetCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
                capabilities.SetCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.SetCapability("tags", new string[] {"sample"});
            }
            Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIAndroid;
            driver = new AndroidDriver<AppiumWebElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            pageObject = new AndroidJavaScriptTestPageObject(driver);
            driver.StartActivity("io.selendroid.testapp", ".WebViewActivity");
        }

        [TestFixtureTearDown]
        public void AfterEach()
        {
            if (driver != null)
            {
                driver.Quit();
            }
            if (!Env.isSauce())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test()]
        public void HighlightingByJS()
        {
            Thread.Sleep(5000);
            var contexts = driver.Contexts;
            string webviewContext = null;
            for (int i = 0; i < contexts.Count; i++)
            {
                Console.WriteLine(contexts[i]);
                if (contexts[i].Contains("WEBVIEW"))
                {
                    webviewContext = contexts[i];
                    break;
                }
            }
            Assert.IsNotNull(webviewContext);
            driver.Context = webviewContext;
            pageObject.HighlightElement();
        }
    }
}