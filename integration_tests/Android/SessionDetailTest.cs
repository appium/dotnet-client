using System;
using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace Appium.Integration.Tests.Android
{
    [TestFixture]
    public class SessionDetailTest
    {
        
        private AndroidDriver<IWebElement> driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.isSauce()
                ? Caps.getAndroid501Caps(Apps.get("selendroidTestApp"))
                : Caps.getAndroid19Caps(Apps.get("selendroidTestApp"));
            if (Env.isSauce())
            {
                capabilities.AddAdditionalCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("name", "android - complex");
                capabilities.AddAdditionalCapability("tags", new[] {"sample"});
            }
            Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIAndroid;
            driver = new AndroidDriver<IWebElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            driver.Manage().Timeouts().ImplicitWait = Env.IMPLICIT_TIMEOUT_SEC;
        }
        
        [SetUp]
        public void SetUp()
        {
            driver?.LaunchApp();
        }

        [TearDown]
        public void TearDowwn()
        {
            driver?.CloseApp();
        }
        
        [Test]
        public void NativeAppSessionDetails()
        {
            Assert.NotNull(driver.GetSessionDetail("deviceUDID"));
            Assert.Null(driver.GetSessionDetail("fakeDetail"));
            Assert.AreEqual(driver.PlatformName, MobilePlatform.Android);
            Assert.False(driver.IsBrowser);
        }
        
        [Test]
        public void WebAppSessionDetails()
        {
            driver.StartActivity("io.selendroid.testapp", ".WebViewActivity");
            var contexts = driver.Contexts;
            foreach (var t in contexts)
            {
                Console.WriteLine(t);
                if (!t.Contains("WEBVIEW")) continue;
                driver.Context = t;
                break;
            }
            
            Assert.Null(driver.GetSessionDetail("deviceUDID"));
            Assert.True(MobilePlatform.Android.IndexOf(driver.PlatformName, 
                            StringComparison.OrdinalIgnoreCase) >= 0);
            Assert.Null(driver.AutomationName);
            Assert.True(driver.IsBrowser);
        }
    }
}