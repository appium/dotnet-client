using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Net.Integration.Tests.IOS
{
    class ElementTest
    {
        private AppiumDriver _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetIosCaps(Apps.Get("iosTestApp"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new IOSDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [OneTimeTearDown]
        public void AfterEach()
        {
            _driver?.Quit();
            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test]
        public void FindByAccessibilityIdTest()
        {
            By byAccessibilityId = new ByAccessibilityId("ComputeSumButton");
            Assert.Multiple(() =>
            {
                Assert.That(Is.Not.EqualTo(_driver.FindElements(MobileBy.ClassName("UIAWindow"))[1].FindElement(byAccessibilityId).Text), null);
                Assert.That(_driver.FindElements(MobileBy.ClassName("UIAWindow"))[1].FindElements(byAccessibilityId), Is.Not.Empty);
            });
        }

        [Test]
        public void FindByByIosUiAutomationTest()
        {
            By byIosUiAutomation = new ByIosUIAutomation(".elements().withName(\"Answer\")");
            Assert.Multiple(() =>
            {
                Assert.That(_driver.FindElements(MobileBy.ClassName("UIAWindow"))[1].FindElement(byIosUiAutomation).Text, Is.Not.Null);
                Assert.That(_driver.FindElements(MobileBy.ClassName("UIAWindow"))[1].FindElements(byIosUiAutomation), Is.Not.Empty);
            });
        }

    }
}