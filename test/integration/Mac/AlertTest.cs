using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Mac;

namespace Appium.Net.Integration.Tests.Mac
{
    public class FindElementTest
    {
        private MacDriver _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = new AppiumOptions
            {
                AutomationName = AutomationName.Mac2,
                PlatformName = MobilePlatform.MacOS
            };
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new MacDriver(serverUri, capabilities, Env.InitTimeoutSec);
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
        public void ClickAboutThisMacTest()
        {
            _driver.FindElement(MobileBy.IosNSPredicate("elementType == 56 AND title = 'Apple'")).Click();
            _driver.FindElement(MobileBy.AccessibilityId("_aboutThisMacRequested:")).Click();
        }
    }
}
