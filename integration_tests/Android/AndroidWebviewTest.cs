using NUnit.Framework;
using System;
using Appium.Integration.Tests.Helpers;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.Enums;
using System.Threading;
using OpenQA.Selenium.Appium;

namespace Appium.Integration.Tests.Android
{
    [TestFixture()]
    public class AndroidWebviewTest
    {
        private IWebDriver driver;

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
                capabilities.AddAdditionalCapability("name", "android - webview");
                capabilities.AddAdditionalCapability("tags", new string[] {"sample"});
            }
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "io.selendroid.testapp");
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, ".WebViewActivity");
            Uri serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            driver = new AndroidDriver<IWebElement>(serverUri, capabilities, Env.InitTimeoutSec);
            driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [OneTimeTearDown]
        public void AfterAll()
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
        public void WebViewTestCase()
        {
            Thread.Sleep(5000);
            var contexts = ((IContextAware) driver).Contexts;
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
            ((IContextAware) driver).Context = webviewContext;
            Assert.IsTrue(driver.PageSource.Contains("Hello, can you please tell me your name?"));
        }
    }
}