using System;
using System.Text.RegularExpressions;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace Appium.Net.Integration.Tests.Android
{
    [TestFixture(Category = "Device")]
    public class FingerprintTest
    {
        private AndroidDriver<IWebElement> _driver;
        private const string ClipboardTestString = "Hello Clipboard";
        private const string Base64RegexPattern = @"^[a-zA-Z0-9\+/]*={0,2}$";

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidCaps(Apps.Get("androidApiDemos"));
            capabilities.AddAdditionalCapability(MobileCapabilityType.FullReset, true);
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver<IWebElement>(serverUri, capabilities, Env.InitTimeoutSec);
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
        public void TestSendFingerprint()
        {
            _driver.FingerPrint(1);
        }
    }
}
