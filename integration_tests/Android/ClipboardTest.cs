using System;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace Appium.Integration.Tests.Android
{
    [TestFixture(Category = "Device")]
    public class ClipboardTest
    {
        private AndroidDriver<IWebElement> _driver;
        private const string ClipboardTestString = "Hello Clipboard";
        private const string Base64RegexPattern = @"^[a-zA-Z0-9\+/]*={0,2}$";

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.isSauce()
                ? Caps.getAndroid501Caps(Apps.get("androidApiDemos"))
                : Caps.getAndroid19Caps(Apps.get("androidApiDemos"));
            if (Env.isSauce())
            {
                capabilities.AddAdditionalCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("name", "android - complex");
                capabilities.AddAdditionalCapability("tags", new[] { "sample" });
            }

            capabilities.AddAdditionalCapability(MobileCapabilityType.FullReset, true);
            var serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIAndroid;
            _driver = new AndroidDriver<IWebElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            _driver.Manage().Timeouts().ImplicitWait = Env.IMPLICIT_TIMEOUT_SEC;
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
        public void WhenClipboardContentTypeIsPlainText_GetClipboardShouldReturnEncodedBase64String()
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
        public void WhenClipboardContentTypeIsImage_SetClipboardShouldReturnNotImplementedException()
        {
            Assert.That(() => _driver.SetClipboard(ClipboardContentType.Image, ClipboardTestString),
                Throws.TypeOf<NotImplementedException>());
        }

        [Test]
        public void WhenClipboardContentTypeIsUrl_SetClipboardShouldReturnNotImplementedException()
        {
            var url = new Url("https://github.com/appium/appium-dotnet-driver");
            var urlBytes = Encoding.UTF8.GetBytes(url.ToString());
            var base64UrlString = Convert.ToBase64String(urlBytes);
            Assert.That(() => _driver.SetClipboard(ClipboardContentType.Url, base64UrlString),
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
