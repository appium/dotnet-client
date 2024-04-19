using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Net.Integration.Tests.IOS
{
    class SearchingTest
    {
        private IOSDriver _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetIosCaps(Apps.Get("iosTestApp"));
            if (Env.ServerIsRemote())
            {
                capabilities.AddAdditionalAppiumOption("username", Env.GetEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalAppiumOption("accessKey", Env.GetEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalAppiumOption("name", "ios - simple");
                capabilities.AddAdditionalAppiumOption("tags", new[] {"sample"});
            }
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new IOSDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [OneTimeTearDown]
        public void AfterAll()
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
                Assert.That(Is.Not.EqualTo(_driver.FindElement(byAccessibilityId).Text), null);
                Assert.That(_driver.FindElements(byAccessibilityId), Is.Not.Empty);
            });
        }

        [Test]
        public void FindByByIosUiAutomationTest()
        {
            By byIosUiAutomation = new ByIosUIAutomation(".elements().withName(\"Answer\")");
            Assert.Multiple(() =>
            {
                Assert.That(_driver.FindElement(byIosUiAutomation).Text, Is.Not.Null);
                Assert.That(_driver.FindElements(byIosUiAutomation), Is.Not.Empty);
            });
        }
    }
}