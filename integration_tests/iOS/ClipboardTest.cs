using System;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Integration.Tests.PageObjectTests.IOS
{
    [TestFixture(Category = "Device")]
    public class ClipboardTest
    {
        private IOSDriver<IWebElement> _driver;
        private const string ClipboardTestString = "Hello Clipboard";
        private const string Base64RegexPattern = @"^[a-zA-Z0-9\+/]*={0,2}$";

        [SetUp]
        public void Setup()
        {
            var capabilities = Caps.getIos112Caps(Apps.get("iosUICatalogApp"));
            if (Env.isSauce())
            {
                capabilities.AddAdditionalCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("tags", new string[] { "sample" });
            }
            capabilities.AddAdditionalCapability(MobileCapabilityType.FullReset, true);
            var serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIForIOS;

            _driver = new IOSDriver<IWebElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            _driver.Manage().Timeouts().ImplicitWait = Env.IMPLICIT_TIMEOUT_SEC;
            
        }

        [Test]
        public void WhenSetClipboardContentTypeIsPlainText_GetClipboardShouldReturnEncodedBase64String()
        {
            string base64ClipboardTestString = Convert.ToBase64String(Encoding.UTF8.GetBytes(ClipboardTestString));
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
            var url = new Url("https://github.com/appium/appium-dotnet-driver");
            
            var urlBytes = Encoding.UTF8.GetBytes(url.ToString());
            var base64UrlString = Convert.ToBase64String(urlBytes);

            _driver.SetClipboard(ClipboardContentType.Url, base64UrlString);
            Assert.That(() => Regex.IsMatch(_driver.GetClipboard(ClipboardContentType.Url), Base64RegexPattern, RegexOptions.Multiline), 
                Is.True);
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

        [TearDown]
        public void TearDown()
        {
            if (_driver.IsLocked())
                _driver.Unlock();
            _driver?.Quit();

            if (!Env.isSauce())
            {
                AppiumServers.StopLocalService();
            }
        }
    }
}
