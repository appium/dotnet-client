using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Net.Integration.Tests.IOS
{
    class ElementTests
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
                Assert.That(_driver.FindElement(MobileBy.ClassName("UIAWindow")).FindElement(byAccessibilityId).Text, Is.Not.Null);
                Assert.That(_driver.FindElement(MobileBy.ClassName("UIAWindow")).FindElements(byAccessibilityId), Is.Not.Empty);
            });
        }

        [Test]
        public void FindElementsByClassNameTest()
        {
            By byClassName = new ByClassName("XCUIElementTypeTextField");
            Assert.That(_driver.FindElements(byClassName), Has.Count.EqualTo(2));
        }

        [Test]
        public void FindElementMobileByClassNameTest()
        {
            var switchElement = _driver.FindElement(MobileBy.ClassName("XCUIElementTypeSwitch"));
            Assert.That(switchElement, Is.Not.Null);
        }

    }
}