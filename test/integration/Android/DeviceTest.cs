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
    public class DeviceTest
    {
        private AndroidDriver<IWebElement> _driver;
        private const string ClipboardTestString = "Hello Clipboard";
        private const string Base64RegexPattern = @"^[a-zA-Z0-9\+/]*={0,2}$";

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetAndroidCaps(Apps.Get("androidApiDemos"));
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
            // There's no way to verify sending fingerprint had an effect,
            // so just test that it's successfully called without an exception
            _driver.FingerPrint(1);
        }

        [Test]
        public void TestToggleData()
        {
            _driver.ToggleData();
            _driver.ToggleData();
        }
    }
}
