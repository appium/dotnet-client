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
            Assert.AreNotEqual(_driver.FindElements(MobileBy.ClassName("UIAWindow"))[1].FindElement(byAccessibilityId).Text,
                null);
            Assert.GreaterOrEqual(_driver.FindElements(MobileBy.ClassName("UIAWindow"))[1].FindElements(byAccessibilityId).Count,
                1);
        }

        [Test]
        public void FindByByIosUiAutomationTest()
        {
            By byIosUiAutomation = new ByIosUIAutomation(".elements().withName(\"Answer\")");
            Assert.IsNotNull(_driver.FindElements(MobileBy.ClassName("UIAWindow"))[1].FindElement(byIosUiAutomation).Text);
            Assert.GreaterOrEqual(_driver.FindElements(MobileBy.ClassName("UIAWindow"))[1].FindElements(byIosUiAutomation).Count,
                1);
        }

        [Test]
        public void SetImmediateValueTest()
        {
            var slider = _driver.FindElement(MobileBy.ClassName("UIASlider"));
            slider.SetImmediateValue("0%");
            Assert.AreEqual("0%", slider.GetAttribute("value"));
        }
    }
}