using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;

namespace Appium.Integration.Tests.iOS
{
    public class IOSScrollingSearchingTest
    {
        private IOSDriver<AppiumWebElement> driver;

        [OneTimeSetUp]
        public void beforeAll()
        {
            AppiumOptions capabilities = Caps.getIos92Caps(Apps.get("iosUICatalogApp"));
            if (Env.isSauce())
            {
                capabilities.AddAdditionalCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("name", "ios - complex");
                capabilities.AddAdditionalCapability("tags", new string[] {"sample"});
            }
            Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIForIOS;
            driver = new IOSDriver<AppiumWebElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            driver.Manage().Timeouts().ImplicitWait = Env.IMPLICIT_TIMEOUT_SEC;
        }

        [OneTimeTearDown]
        public void AfterEach()
        {
            if (driver != null)
            {
                driver.Quit();
            }
            if (!Env.isSauce())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test()]
        public void ScrollToTestCase()
        {
            AppiumWebElement slider = driver
                .FindElement(new ByIosUIAutomation(".tableViews()[0]"
                                                   + ".scrollToElementWithPredicate(\"name CONTAINS 'Slider'\")"));
            Assert.AreEqual(slider.GetAttribute("name"), "Sliders");
        }

        [Test()]
        public void ScrollToExactTestCase()
        {
            AppiumWebElement table = driver.FindElement(new ByIosUIAutomation(".tableViews()[0]"));
            AppiumWebElement slider = table.FindElement(
                new ByIosUIAutomation(".scrollToElementWithPredicate(\"name CONTAINS 'Slider'\")"));
            Assert.AreEqual(slider.GetAttribute("name"), "Sliders");
        }
    }
}