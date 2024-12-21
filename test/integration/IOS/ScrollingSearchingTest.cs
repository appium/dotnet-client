using System.Collections.Generic;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Net.Integration.Tests.IOS
{
    public class ScrollingSearchingTest
    {
        private IOSDriver _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetIosCaps(Apps.Get("iosUICatalogApp"));
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
        public void ScrollToToolbarsElementUsingToVisibleTrueTest()
        {
            var Toolbars = _driver.FindElement(new ByIosNSPredicate("name == 'Toolbars'"));
            _driver.ExecuteScript("mobile: scroll", new Dictionary<string, string> { { "element", Toolbars.Id }, { "toVisible", "true" } });
            Assert.That(Toolbars.Displayed, Is.True, "The 'Toolbars' element should be visible after scrolling.");
        }

        [Test]
        public void ScrollToSwitchesElementUsingDirectionDownTest()
        {
            var table = _driver.FindElement(new ByIosNSPredicate("type == 'XCUIElementTypeTable'"));
            _driver.ExecuteScript("mobile: scroll", new Dictionary<string, string> { { "direction", "down" }, { "element", table.Id } });
            var switches = table.FindElement(new ByIosNSPredicate("name CONTAINS 'Switches'"));
            Assert.That(switches.GetAttribute("visible"), Is.EqualTo("true"));
        }
    }
}