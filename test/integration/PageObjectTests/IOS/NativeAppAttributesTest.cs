using System;
using Appium.Net.Integration.Tests.helpers;
using Appium.Net.Integration.Tests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.PageObjects;
using SeleniumExtras.PageObjects;

namespace Appium.Net.Integration.Tests.PageObjectTests.IOS
{
    [TestFixture]
    public class NativeAppAttributesTest
    {
        private IOSDriver<AppiumWebElement> _driver;
        private IosPageObjectChecksAttributesForNativeIosApp _pageObject;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetIosCaps(Apps.Get("iosTestApp"));
            if (Env.ServerIsRemote())
            {
                capabilities.AddAdditionalAppiumOption("username", Env.GetEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalAppiumOption("accessKey", Env.GetEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalAppiumOption("name", "ios - actions");
                capabilities.AddAdditionalAppiumOption("tags", new[] {"sample"});
            }

            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new IOSDriver<AppiumWebElement>(serverUri, capabilities, Env.InitTimeoutSec);
            var timeSpan = new TimeOutDuration(new TimeSpan(0, 0, 0, 5, 0));
            _pageObject = new IosPageObjectChecksAttributesForNativeIosApp();
            PageFactory.InitElements(_driver, _pageObject, new AppiumPageObjectMemberDecorator(timeSpan));
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
        public void CheckMobileElement()
        {
            Assert.NotNull(_pageObject.GetMobileElementText());
        }

        [Test]
        public void CheckMobileElements()
        {
            Assert.GreaterOrEqual(_pageObject.GetMobileElementSize(), 1);
        }

        [Test]
        public void CheckMobileElementProperty()
        {
            Assert.NotNull(_pageObject.GetMobileElementPropertyText());
        }

        [Test]
        public void CheckMobileElementsProperty()
        {
            Assert.GreaterOrEqual(_pageObject.GetMobileElementPropertySize(), 1);
        }

        [Test]
        public void CheckElementFoundUsingMultipleLocators()
        {
            Assert.NotNull(_pageObject.GetMultipleFindByElementText());
        }

        [Test]
        public void CheckElementsFoundUsingMultipleLocators()
        {
            Assert.GreaterOrEqual(_pageObject.GetMultipleFindByElementSize(), 1);
        }

        [Test]
        public void CheckElementFoundUsingMultipleLocatorsProperty()
        {
            Assert.NotNull(_pageObject.GetMultipleFindByElementPropertyText());
        }

        [Test]
        public void CheckElementsFoundUsingMultipleLocatorssProperty()
        {
            Assert.GreaterOrEqual(_pageObject.GetMultipleFindByElementPropertySize(), 1);
        }

        [Test]
        public void CheckElementMatchedToAll()
        {
            Assert.NotNull(_pageObject.GetMatchedToAllLocatorsElementText());
        }

        [Test]
        public void CheckElementsMatchedToAll()
        {
            Assert.GreaterOrEqual(_pageObject.GetMatchedToAllLocatorsElementSize(), 1);
            Assert.LessOrEqual(_pageObject.GetMatchedToAllLocatorsElementSize(), 13);
        }

        [Test]
        public void CheckElementMatchedToAllProperty()
        {
            Assert.NotNull(_pageObject.GetMatchedToAllLocatorsElementPropertyText());
        }

        [Test]
        public void CheckElementMatchedToAllElementsProperty()
        {
            Assert.GreaterOrEqual(_pageObject.GetMatchedToAllLocatorsElementPropertySize(), 1);
            Assert.LessOrEqual(_pageObject.GetMatchedToAllLocatorsElementPropertySize(), 13);
        }
    }
}