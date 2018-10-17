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

        [OneTimeSetUp]
        public void BeforeAll()
        {
            AppiumOptions capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidCaps(Apps.get("selendroidTestApp"))
                : Caps.GetAndroidCaps(Apps.get("selendroidTestApp"));
            if (Env.ServerIsRemote())
            {
                capabilities.AddAdditionalCapability("username", Env.GetEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.GetEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("tags", new string[] {"sample"});
            }
            Uri serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            driver = new AndroidDriver<AppiumWebElement>(serverUri, capabilities, Env.InitTimeoutSec);
            pageObject = new AndroidJavaScriptTestPageObject(driver);
            driver.StartActivity("io.selendroid.testapp", ".WebViewActivity");
        }

        [OneTimeTearDown]
        public void AfterEach()
        {
            if (driver != null)
            {
                driver.Quit();
            }
            if (!Env.ServerIsRemote())
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