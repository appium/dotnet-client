using Appium.Integration.Tests.Helpers;
using Appium.Integration.Tests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.PageObjects;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace Appium.Integration.Tests.PageObjectTests.IOS
{
    [TestFixture()]
    public class IOSNativeAppAttributesTest
    {
        private IOSDriver<AppiumWebElement> driver;
        private IOSPageObjectChecksAttributesForNativeIOSApp pageObject;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            DesiredCapabilities capabilities = Caps.getIos92Caps(Apps.get("iosTestApp"));
            if (Env.isSauce())
            {
                capabilities.SetCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
                capabilities.SetCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.SetCapability("name", "ios - actions");
                capabilities.SetCapability("tags", new string[] {"sample"});
            }
            Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIForIOS;
            driver = new IOSDriver<AppiumWebElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            TimeOutDuration timeSpan = new TimeOutDuration(new TimeSpan(0, 0, 0, 5, 0));
            pageObject = new IOSPageObjectChecksAttributesForNativeIOSApp();
            PageFactory.InitElements(driver, pageObject, new AppiumPageObjectMemberDecorator(timeSpan));
        }

        [TestFixtureTearDown]
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
        public void CheckMobileElement()
        {
            Assert.NotNull(pageObject.GetMobileElementText());
        }

        [Test()]
        public void CheckMobileElements()
        {
            Assert.GreaterOrEqual(pageObject.GetMobileElementSize(), 1);
        }

        [Test()]
        public void CheckMobileElementProperty()
        {
            Assert.NotNull(pageObject.GetMobileElementPropertyText());
        }

        [Test()]
        public void CheckMobileElementsProperty()
        {
            Assert.GreaterOrEqual(pageObject.GetMobileElementPropertySize(), 1);
        }

        [Test()]
        public void CheckElementFoundUsingMultipleLocators()
        {
            Assert.NotNull(pageObject.GetMultipleFindByElementText());
        }

        [Test()]
        public void CheckElementsFoundUsingMultipleLocators()
        {
            Assert.GreaterOrEqual(pageObject.GetMultipleFindByElementSize(), 1);
        }

        [Test()]
        public void CheckElementFoundUsingMultipleLocatorsProperty()
        {
            Assert.NotNull(pageObject.GetMultipleFindByElementPropertyText());
        }

        [Test()]
        public void CheckElementsFoundUsingMultipleLocatorssProperty()
        {
            Assert.GreaterOrEqual(pageObject.GetMultipleFindByElementPropertySize(), 1);
        }

        [Test()]
        public void CheckElementMatchedToAll()
        {
            Assert.NotNull(pageObject.GetMatchedToAllLocatorsElementText());
        }

        [Test()]
        public void CheckElementsMatchedToAll()
        {
            Assert.GreaterOrEqual(pageObject.GetMatchedToAllLocatorsElementSize(), 1);
            Assert.LessOrEqual(pageObject.GetMatchedToAllLocatorsElementSize(), 13);
        }

        [Test()]
        public void CheckElementMatchedToAllProperty()
        {
            Assert.NotNull(pageObject.GetMatchedToAllLocatorsElementPropertyText());
        }

        [Test()]
        public void CheckElementMatchedToAllElementsProperty()
        {
            Assert.GreaterOrEqual(pageObject.GetMatchedToAllLocatorsElementPropertySize(), 1);
            Assert.LessOrEqual(pageObject.GetMatchedToAllLocatorsElementPropertySize(), 13);
        }
    }
}