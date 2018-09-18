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
            AppiumOptions capabilities = Env.isSauce()
                ? Caps.getAndroid501Caps(Apps.get("selendroidTestApp"))
                : Caps.getAndroid19Caps(Apps.get("selendroidTestApp"));
            if (Env.isSauce())
            {
                capabilities.AddAdditionalCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("name", "android - webview");
                capabilities.AddAdditionalCapability("tags", new string[] {"sample"});
            }
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "io.selendroid.testapp");
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, ".WebViewActivity");
            Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIAndroid;
            driver = new AndroidDriver<IWebElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            driver.Manage().Timeouts().ImplicitWait = Env.IMPLICIT_TIMEOUT_SEC;
        }

        [OneTimeTearDown]
        public void AfterAll()
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