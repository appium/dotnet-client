using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using System;
using System.Drawing;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;

namespace Appium.Net.Integration.Tests.Android
{
    [TestFixture]
    [Category("Device")]
    [Category("Drawing")]
    public class ClipboardTest
    {
        private AndroidDriver _driver;
        private const string ClipboardTestString = "Hello Clipboard";
        private const string Base64RegexPattern = @"^[a-zA-Z0-9\+/]*={0,2}$";
        private readonly string _appId = Apps.GetId(Apps.androidApiDemos);

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));
            capabilities.AddAdditionalAppiumOption(MobileCapabilityType.FullReset, true);
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [SetUp]
        public void SetUp()
        {
            _driver?.ActivateApp(_appId);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _driver?.TerminateApp(_appId);
            _driver?.Quit();
        }

        [Test]
        public void WhenSetClipboardContentTypeIsPlainTextGetClipboardShouldReturnEncodedBase64String()
        {
            _driver.SetClipboard(ClipboardContentType.PlainText, ClipboardTestString);
            Assert.That(() => Regex.IsMatch(_driver.GetClipboard(ClipboardContentType.PlainText), Base64RegexPattern, RegexOptions.Multiline), 
                Is.True);
        }

        [Test]
        public void WhenClipboardContentTypeIsPlainTextWithLabelGetClipboardTextShouldReturnActualText()
        {
            _driver.SetClipboardText(ClipboardTestString, label:"testing");
            Assert.That(() => _driver.GetClipboardText(), Does.Match(ClipboardTestString));
        }

        [Test]
        public void WhenClipboardContentTypeIsPlainTextWithOutLabelGetClipboardTextShouldReturnActualText()
        {
            _driver.SetClipboardText(ClipboardTestString, null);
            Assert.That(() => _driver.GetClipboardText(), Does.Match(ClipboardTestString));
        }

        [Test]
        public void WhenClipboardIsEmptyGetClipboardShouldReturnEmptyString()
        {
            _driver.SetClipboardText(string.Empty, null);
            Assert.That(() => _driver.GetClipboard(ClipboardContentType.PlainText), Is.Empty);
        }

        [Test]
        public void WhenClipboardIsEmptyGetClipboardTextShouldReturnEmptyString()
        {
            _driver.SetClipboardText(string.Empty, null);
            Assert.That(() => _driver.GetClipboardText(), Is.Empty);
        }

        [Test]
        public void WhenSetClipboardContentTypeIsImageSetClipboardShouldReturnNotImplementedException()
        {
            Assert.That(() => _driver.SetClipboard(ClipboardContentType.Image, ClipboardTestString),
                Throws.TypeOf<NotImplementedException>());
        }

        [Test]
#if !NET48
        [SupportedOSPlatform("windows")]
#endif
        public void WhenGetClipboardImageGetClipboardShouldReturnNotImplementedException()
        {
            Assert.That(() => _driver.GetClipboardImage(),
                Throws.TypeOf<NotImplementedException>());
        }

        [Test]
#if !NET48
        [SupportedOSPlatform("windows")]
#endif
        public void WhenSetClipboardImageSetClipboardShouldReturnNotImplementedException()
        {
            // Arrange
            Image testImage = new Bitmap(100, 100); // Create a sample image for testing

            // Act & Assert
            _ = Assert.Throws<NotImplementedException>(() => _driver.SetClipboardImage(testImage));
        }

        [Test]
        public void WhenGetClipboardUrlGetClipboardShouldReturnNotImplementedException()
        {
            Assert.That(() => _driver.GetClipboardUrl(),
                Throws.TypeOf<NotImplementedException>());
        }

        [Test]
        public void WhenSetClipboardContentTypeIsUrlSetClipboardShouldReturnNotImplementedException()
        {
            Assert.That(() => _driver.SetClipboard(ClipboardContentType.Url, string.Empty),
                Throws.TypeOf<NotImplementedException>());
        }

        [Test]
        public void WhenClipboardContentTypeIsUrlGetClipboardShouldReturnNotImplementedException()
        {
            Assert.That(() => _driver.GetClipboard(ClipboardContentType.Url),
                Throws.TypeOf<NotImplementedException>());
        }

        [Test]
        public void WhenClipboardContentTypeIsImageGetClipboardShouldReturnNotImplementedException()
        {
            Assert.That(() => _driver.GetClipboard(ClipboardContentType.Image),
                Throws.TypeOf<NotImplementedException>());
        }
    }
}
