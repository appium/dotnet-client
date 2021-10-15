using System;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace Appium.Net.Integration.Tests.Android
{
    [TestFixture]
    public class SessionDetailTest
    {
        
        private AndroidDriver _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("selendroidTestApp"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("selendroidTestApp"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }
        
        [SetUp]
        public void SetUp()
        {
            _driver?.LaunchApp();
        }

        [TearDown]
        public void TearDown()
        {
            _driver?.CloseApp();
        }
        
        [Test]
        public void NativeAppSessionDetails()
        {
            Assert.NotNull(_driver.GetSessionDetail("deviceUDID"));
            Assert.Null(_driver.GetSessionDetail("fakeDetail"));
            Assert.AreEqual(_driver.PlatformName, MobilePlatform.Android);
            Assert.False(_driver.IsBrowser);
        }
        
        [Test]
        public void WebAppSessionDetails()
        {
            _driver.StartActivity("io.selendroid.testapp", ".WebViewActivity");
            var contexts = _driver.Contexts;
            foreach (var t in contexts)
            {
                Console.WriteLine(t);
                if (!t.Contains("WEBVIEW")) continue;
                _driver.Context = t;
                break;
            }
            
            Assert.Null(_driver.GetSessionDetail("deviceUDID"));
            Assert.True(MobilePlatform.Android.IndexOf(_driver.PlatformName, 
                            StringComparison.OrdinalIgnoreCase) >= 0);
            Assert.Null(_driver.AutomationName);
            Assert.True(_driver.IsBrowser);
        }
    }
}