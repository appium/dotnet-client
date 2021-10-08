using System;
using System.Text.RegularExpressions;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace Appium.Net.Integration.Tests.Android
{
    [TestFixture(Category = "Device")]
    public class ClipboardTest
    {
        private AndroidDriver _driver;
        private const string ClipboardTestString = "Hello Clipboard";
        private const string Base64RegexPattern = @"^[a-zA-Z0-9\+/]*={0,2}$";

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));
            capabilities.AddAdditionalAppiumOption(MobileCapabilityType.FullReset, true);
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
        public void WhenSetClipboardContentTypeIsPlainText_GetClipboardShouldReturnEncodedBase64String()
        {
            _driver.SetClipboard(ClipboardContentType.PlainText, ClipboardTestString);
            Assert.That(() => Regex.IsMatch(_driver.GetClipboard(ClipboardContentType.PlainText), Base64RegexPattern, RegexOptions.Multiline), 
                Is.True);
        }

        [Test]
        public void WhenClipboardContentTypeIsPlainTextWithLabel_GetClipboardTextShouldReturnActualText()
        {
            _driver.SetClipboardText(ClipboardTestString, label:"testing");
            Assert.That(() => _driver.GetClipboardText(), Does.Match(ClipboardTestString));
        }

        [Test]
        public void WhenClipboardContentTypeIsPlainTextWithOutLabel_GetClipboardTextShouldReturnActualText()
        {
            _driver.SetClipboardText(ClipboardTestString, null);
            Assert.That(() => _driver.GetClipboardText(), Does.Match(ClipboardTestString));
        }

        [Test]
        public void WhenClipboardIsEmpty_GetClipboardShouldReturnEmptyString()
        {
            _driver.SetClipboardText(string.Empty, null);
            Assert.That(() => _driver.GetClipboard(ClipboardContentType.PlainText), Is.Empty);
        }

        [Test]
        public void WhenClipboardIsEmpty_GetClipboardTextShouldReturnEmptyString()
        {
            _driver.SetClipboardText(string.Empty, null);
            Assert.That(() => _driver.GetClipboardText(), Is.Empty);
        }

        [Test]
        public void WhenSetClipboardContentTypeIsImage_SetClipboardShouldReturnNotImplementedException()
        {
            Assert.That(() => _driver.SetClipboard(ClipboardContentType.Image, ClipboardTestString),
                Throws.TypeOf<NotImplementedException>());
        }

        [Test]
        public void WhenGetClipboardImage_GetClipboardShouldReturnNotImplementedException()
        {
            Assert.That(() => _driver.GetClipboardImage(),
                Throws.TypeOf<NotImplementedException>());
        }

        [Test]
        public void WhenGetClipboardUrl_GetClipboardShouldReturnNotImplementedException()
        {
            Assert.That(() => _driver.GetClipboardUrl(),
                Throws.TypeOf<NotImplementedException>());
        }

        [Test]
        public void WhenSetClipboardContentTypeIsUrl_SetClipboardShouldReturnNotImplementedException()
        {
            Assert.That(() => _driver.SetClipboard(ClipboardContentType.Url, string.Empty),
                Throws.TypeOf<NotImplementedException>());
        }

        [Test]
        public void WhenClipboardContentTypeIsUrl_GetClipboardShouldReturnNotImplementedException()
        {
            Assert.That(() => _driver.GetClipboard(ClipboardContentType.Url),
                Throws.TypeOf<NotImplementedException>());
        }

        [Test]
        public void WhenClipboardContentTypeIsImage_GetClipboardShouldReturnNotImplementedException()
        {
            Assert.That(() => _driver.GetClipboard(ClipboardContentType.Image),
                Throws.TypeOf<NotImplementedException>());
        }
    }
}
