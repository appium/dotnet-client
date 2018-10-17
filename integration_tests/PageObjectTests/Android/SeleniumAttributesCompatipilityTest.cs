using Appium.Integration.Tests.Helpers;
using Appium.Integration.Tests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.PageObjects;
using OpenQA.Selenium.Remote;
using SeleniumExtras.PageObjects;
using System;

namespace Appium.Integration.Tests.PageObjectTests.Android
{
    [TestFixture()]
    public class SeleniumAttributesCompatipilityTest
    {
        private AndroidDriver<AppiumWebElement> driver;
        private AndroidPageObjectChecksSeleniumFindsByCompatibility pageObject;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            AppiumOptions capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidCaps(Apps.get("androidApiDemos"))
                : Caps.GetAndroidCaps(Apps.get("androidApiDemos"));
            if (Env.ServerIsRemote())
            {
                capabilities.AddAdditionalCapability("username", Env.GetEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.GetEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("name", "android - complex");
                capabilities.AddAdditionalCapability("tags", new string[] {"sample"});
            }
            Uri serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            driver = new AndroidDriver<AppiumWebElement>(serverUri, capabilities, Env.InitTimeoutSec);
            TimeOutDuration timeSpan = new TimeOutDuration(new TimeSpan(0, 0, 0, 5, 0));
            pageObject = new AndroidPageObjectChecksSeleniumFindsByCompatibility();
            PageFactory.InitElements(driver, pageObject, new AppiumPageObjectMemberDecorator(timeSpan));
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
        public void CheckElement()
        {
            Assert.NotNull(pageObject.GetElementText());
        }

        [Test()]
        public void CheckElements()
        {
            Assert.GreaterOrEqual(pageObject.GetElementSize(), 1);
        }

        [Test()]
        public void CheckElementProperty()
        {
            Assert.NotNull(pageObject.GetElementPropertyText());
        }

        [Test()]
        public void CheckElementsProperty()
        {
            Assert.GreaterOrEqual(pageObject.GetElementPropertySize(), 1);
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
            Assert.GreaterOrEqual(pageObject.GetMultipleFindByElementSize(), 10);
            Assert.LessOrEqual(pageObject.GetMultipleFindByElementSize(), 14);
        }

        [Test()]
        public void CheckElementFoundUsingMultipleLocatorsProperty()
        {
            Assert.NotNull(pageObject.GetMultipleFindByElementPropertyText());
        }

        [Test()]
        public void CheckElementsFoundUsingMultipleLocatorssProperty()
        {
            Assert.GreaterOrEqual(pageObject.GetMultipleFindByElementPropertySize(), 10);
            Assert.LessOrEqual(pageObject.GetMultipleFindByElementSize(), 14);
        }

        [Test()]
        public void CheckElementFoundByChainedSearch()
        {
            Assert.NotNull(pageObject.GetFoundByChainedSearchElementText());
        }

        [Test()]
        public void CheckElementsFoundByChainedSearch()
        {
            Assert.GreaterOrEqual(pageObject.GetFoundByChainedSearchElementSize(), 10);
            Assert.LessOrEqual(pageObject.GetMultipleFindByElementSize(), 14);
        }

        [Test()]
        public void CheckFoundByChainedSearchElementProperty()
        {
            Assert.NotNull(pageObject.GetFoundByChainedSearchElementPropertyText());
        }

        [Test()]
        public void CheckFoundByChainedSearchElementsProperty()
        {
            Assert.GreaterOrEqual(pageObject.GetFoundByChainedSearchElementPropertySize(), 10);
            Assert.LessOrEqual(pageObject.GetMultipleFindByElementSize(), 14);
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