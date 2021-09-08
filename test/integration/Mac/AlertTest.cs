using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Mac;

namespace Appium.Net.Integration.Tests.Mac
{
    public class FindElementTest
    {
        private AppiumDriver<MacElement> _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalOption(MobileCapabilityType.DeviceName, "Mac"); // Requires until Appium 1.15.1
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new MacDriver<MacElement>(serverUri, capabilities, Env.InitTimeoutSec);
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
        public void ClickFinderIconOnDoc()
        {
            _driver.FindElementByXPath("/AXApplication[@AXTitle='Dock']/AXList[0]/AXDockItem[@AXTitle='Finder']").Click();
        }
    }
}
