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
        public void ScrollToTestCase()
        {
            var slider = _driver
                .FindElement(new ByIosUIAutomation(".tableViews()[0]"
                                                   + ".scrollToElementWithPredicate(\"name CONTAINS 'Slider'\")"));
            Assert.AreEqual(slider.GetAttribute("name"), "Sliders");
        }

        [Test]
        public void ScrollToExactTestCase()
        {
            var table = _driver.FindElement(new ByIosUIAutomation(".tableViews()[0]"));
            var slider = table.FindElement(
                new ByIosUIAutomation(".scrollToElementWithPredicate(\"name CONTAINS 'Slider'\")"));
            Assert.AreEqual(slider.GetAttribute("name"), "Sliders");
        }
    }
}