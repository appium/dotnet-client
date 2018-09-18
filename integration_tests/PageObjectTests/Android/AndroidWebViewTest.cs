using Appium.Integration.Tests.Helpers;
using Appium.Integration.Tests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.PageObjects;
using OpenQA.Selenium.Remote;
using SeleniumExtras.PageObjects;
using System;
using System.Threading;

namespace Appium.Integration.Tests.PageObjectTests.Android
{
    public class AndroidWebViewTest
    {
        private AndroidDriver<AppiumWebElement> driver;
        private AndroidWebView pageObject;

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
            Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIAndroid;
            driver = new AndroidDriver<AppiumWebElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            TimeOutDuration timeSpan = new TimeOutDuration(new TimeSpan(0, 0, 0, 5, 0));
            pageObject = new AndroidWebView();
            PageFactory.InitElements(driver, pageObject, new AppiumPageObjectMemberDecorator(timeSpan));
            driver.StartActivity("io.selendroid.testapp", ".WebViewActivity");
        }

        [OneTimeTearDown]
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
        public void WebViewTestCase()
        {
            Thread.Sleep(5000);
            if (!Env.isSauce())
            {
                var contexts = driver.Contexts;
                string webviewContext = null;
                for (int i = 0; i < contexts.Count; i++)
                {
                    Console.WriteLine(contexts[i]);
                    if (contexts[i].Contains("WEBVIEW"))
                    {
                        webviewContext = contexts[i];
                    }
                }
                Assert.IsNotNull(webviewContext);
                driver.Context = webviewContext;

                pageObject.SendMeYourName();
            }
        }
    }
}