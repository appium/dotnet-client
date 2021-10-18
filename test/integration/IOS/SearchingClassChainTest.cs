using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Net.Integration.Tests.IOS
{
    public class SearchingClassChainTest
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
        public void FindByClassChainTest()
        {
            var sliderCellStaticTextElements1 = _driver
                .FindElements(
                    new ByIosClassChain("**/XCUIElementTypeCell/XCUIElementTypeStaticText[`name == 'Sliders'`]"));
            Assert.AreEqual(1, sliderCellStaticTextElements1.Count);
            var sliderCellStaticTextElements2 = _driver.FindElements(MobileBy.IosClassChain("**/XCUIElementTypeCell"));
            Assert.AreEqual(18, sliderCellStaticTextElements2.Count);
        }
    }
}