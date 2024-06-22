using System;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Support.UI;

namespace Appium.Net.Integration.Tests.IOS
{
    class ElementTests
    {
        private AppiumDriver _driver;
        private WebDriverWait _driverWait;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetIosCaps(Apps.Get("iosTestApp"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new IOSDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
            _driverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
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
                Assert.That(_driver.FindElement(MobileBy.ClassName("UIAWindow")).FindElement(byAccessibilityId).Text, Is.EqualTo("Compute Sum"));
                Assert.That(_driver.FindElement(MobileBy.ClassName("UIAWindow")).FindElements(byAccessibilityId), Is.Not.Empty);
            });
        }

        [Test]
        public void FindElementsByClassNameTest()
        {
            By byClassName = new ByClassName("XCUIElementTypeTextField");
            Assert.That(_driverWait.Until(_driver => _driver.FindElements(byClassName).Count == 2), Is.True);
        }

        [Test]
        public void FindElementMobileByClassNameTest()
        {
            try
            {
                var switchElement = _driver.FindElement(MobileBy.ClassName("XCUIElementTypeSwitch"));
            }   
            catch (NoSuchElementException ex)
            {
                Assert.Fail("Element not found, exception: " + ex.Message);
            }
        }

    }
}