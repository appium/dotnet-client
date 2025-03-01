using NUnit.Framework;
using Appium.Net.Integration.Tests.helpers;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium;


namespace Appium.Net.Integration.Tests.IOS.Device
{
    [TestFixture]
    public class IOSKeyboardTests
    {
        private IOSDriver iosDriver;

        [OneTimeSetUp]
        public void SetUp()
        {
            var capabilities = Caps.GetIosCaps(Apps.Get("iosTestApp"));
            capabilities.AddAdditionalAppiumOption(MobileCapabilityType.FullReset, false);
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;

            iosDriver = new IOSDriver(serverUri, capabilities, Env.InitTimeoutSec);
            iosDriver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [Test]
        public void HideKeyboard_WithKeyAndStrategy_ShouldInvokeHelperMethod()
        {
            // Arrange
            var key = "Done";
            var strategy = "pressKey";
            // Act
            iosDriver.FindElement(MobileBy.AccessibilityId("IntegerA")).Click();
            iosDriver.HideKeyboard(key, strategy);

            // Assert
            var keyboard_shown = iosDriver.IsKeyboardShown();
            Assert.That(keyboard_shown, Is.EqualTo(false));
        }

        [Test]
        public void HideKeyboard_WithKeyOnly_ShouldInvokeHelperMethod()
        {
            // Arrange
            var key = "Done";
            iosDriver.FindElement(MobileBy.AccessibilityId("IntegerA")).Click();
            // Act
            iosDriver.HideKeyboard(key);
            iosDriver.HideKeyboard();
            // Assert
            var keyboard_shown = iosDriver.IsKeyboardShown();
            Assert.That(keyboard_shown, Is.EqualTo(false));
        }

        [Test]
        public void HideKeyboard_WithoutParameters_ShouldInvokeHelperMethod()
        {
            // Arrange
            iosDriver.FindElement(MobileBy.AccessibilityId("IntegerA")).Click();
            // Act
            iosDriver.HideKeyboard();
            // Assert
            var keyboard_shown = iosDriver.IsKeyboardShown();
            Assert.That(keyboard_shown, Is.EqualTo(false));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            if (iosDriver.IsLocked())
                iosDriver.Unlock();
            iosDriver?.Quit();

            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }
    }
}