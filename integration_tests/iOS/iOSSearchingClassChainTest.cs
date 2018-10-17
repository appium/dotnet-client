using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.ObjectModel;

namespace Appium.Integration.Tests.iOS
{
    public class IOSSearchingClassChainTest
    {
        private IOSDriver<AppiumWebElement> driver;

        [OneTimeSetUp]
        public void beforeAll()
        {
            AppiumOptions capabilities = Caps.GetIOSCaps(Apps.get("iosUICatalogApp"));
            if (Env.ServerIsRemote())
            {
                capabilities.AddAdditionalCapability("username", Env.GetEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.GetEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("name", "ios - complex");
                capabilities.AddAdditionalCapability("tags", new string[] {"sample"});
            }
            Uri serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            driver = new IOSDriver<AppiumWebElement>(serverUri, capabilities, Env.InitTimeoutSec);
            driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [OneTimeTearDown]
        public void AfterEach()
        {
            if (driver != null)
            {
                driver.Quit();
            }
            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test()]
        public void FindByClassChainTest()
        {
            ReadOnlyCollection<AppiumWebElement> sliderCellStaticTextElements_1 = driver
                .FindElements(
                    new ByIosClassChain("**/XCUIElementTypeCell/XCUIElementTypeStaticText[`name == 'Sliders'`]"));
            Assert.AreEqual(sliderCellStaticTextElements_1.Count, 1);
            ReadOnlyCollection<AppiumWebElement> sliderCellStaticTextElements_2 = driver
                .FindElementsByIosClassChain("**/XCUIElementTypeCell");
            Assert.AreEqual(sliderCellStaticTextElements_2.Count, 18);
        }
    }
}