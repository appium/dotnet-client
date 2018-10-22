using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace Appium.Integration.Tests.Android
{
    [TestFixture(Category = "Device")]
    class ClipboardTest
    {
        private AndroidDriver<IWebElement> driver;
        private string testString = "Hello Clipboard"; 

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
        public void TearDown()
        {
            driver?.CloseApp();
        }

        [Test]
        public void WhenPlainTextWithoutLabelHasBeenSetToClipboard_ClipboardShouldHavePlainText()
        {
            driver.SetClipboardText(testString, null);
            Assert.That(() => driver.GetClipboard(ClipboardContentType.PlainText), Does.Match(testString));
        }

        [Test]
        public void WhenPlainTextWithLabelHasBeenSetToClipboard_ClipboardShouldHavePlainText()
        {
            driver.SetClipboardText(testString, "testing");
            Assert.That(() => driver.GetClipboard(ClipboardContentType.PlainText), Does.Match(testString));
        }

    }
}
