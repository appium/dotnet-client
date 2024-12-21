using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Appium.Net.Integration.Tests.IOS
{
    [TestFixture(Category = "Device")]
    public class ClipboardTest
    {
        private IOSDriver _driver;
        private const string ClipboardTestString = "Hello Clipboard";
        private const string Base64RegexPattern = @"^[a-zA-Z0-9\+/]*={0,2}$";

        [OneTimeSetUp]
        public void Setup()
        {
            var capabilities = Caps.GetIosCaps(Apps.Get("iosUICatalogApp"));
            capabilities.AddAdditionalAppiumOption(MobileCapabilityType.FullReset, false);
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;

            _driver = new IOSDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [Test]
        public void WhenSetClipboardContentTypeIsPlainText_GetClipboardShouldReturnEncodedBase64String()
        {
            var base64ClipboardTestString = Convert.ToBase64String(Encoding.UTF8.GetBytes(ClipboardTestString));
            _driver.SetClipboard(ClipboardContentType.PlainText, base64ClipboardTestString);
            Assert.That(() => Regex.IsMatch(_driver.GetClipboard(ClipboardContentType.PlainText), Base64RegexPattern), 
                Is.True);
        }

        [Test]
        public void WhenClipboardContentTypeIsPlainText_GetClipboardTextShouldReturnActualText()
        {
            _driver.SetClipboardText(ClipboardTestString);
            Assert.That(() => _driver.GetClipboardText(), Does.Match(ClipboardTestString));
        }

        [Test]
        public void WhenSetClipboardContentTypeIsUrl_GetClipboardShouldReturnEncodedBase64String()
        {
            const string urlString = "https://github.com/appium/dotnet-client";
            var base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(urlString));
            _driver.SetClipboard(ClipboardContentType.Url, base64String);

            Assert.That(() => Regex.IsMatch(_driver.GetClipboard(ClipboardContentType.Url), Base64RegexPattern, RegexOptions.Multiline), 
                Is.True);
        }

        [Test]
        public void WhenSetClipboardUrl_GetClipboardUrlShouldReturnUrl()
        {
            const string urlString = "https://github.com/appium/dotnet-client";
            _driver.SetClipboardUrl(urlString);

            Assert.That(() => _driver.GetClipboardUrl(), Does.Match(urlString));
        }

        [Test]
        public void WhenSetClipboardContentTypeIsImage_GetClipboardShouldReturnEncodedBase64String()
        {
            var testImageBytes = _driver.GetScreenshot().AsByteArray;
            var base64Image = Convert.ToBase64String(testImageBytes);
            _driver.SetClipboard(ClipboardContentType.Image, base64Image);

            Assert.That(() => Regex.IsMatch(_driver.GetClipboard(ClipboardContentType.Image), Base64RegexPattern, RegexOptions.Multiline),
                Is.True);
        }

        [Test]
        public void WhenClipboardHasNoImage_GetClipboardImageShouldReturnNull()
        {
            _driver.SetClipboardText(ClipboardTestString);
            Assert.That(() => _driver.GetClipboardImage(), Is.Null);
        }

        [Test]
        public void WhenClipboardIsEmpty_GetClipboardShouldReturnEmptyString()
        {
            _driver.SetClipboardText(string.Empty);
            Assert.That(() => _driver.GetClipboard(ClipboardContentType.PlainText), Is.Empty);
        }

        [Test]
        public void WhenClipboardIsEmpty_GetClipboardTextShouldReturnEmptyString()
        {
            _driver.SetClipboardText(string.Empty, null);
            Assert.That(() => _driver.GetClipboardText(), Is.Empty);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            if (_driver.IsLocked())
                _driver.Unlock();
            _driver?.Quit();

            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }
    }
}
