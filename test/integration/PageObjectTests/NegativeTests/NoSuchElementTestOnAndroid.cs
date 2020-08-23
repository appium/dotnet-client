using System;
using System.Collections.Generic;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.PageObjects;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace Appium.Net.Integration.Tests.PageObjectTests.NegativeTests
{
    public class NoSuchElementTestOnAndroid
    {
        private AndroidDriver<AppiumWebElement> _driver;

        [FindsBy(How = How.ClassName, Using = "FakeHtmlClass")]
        [FindsByIOSUIAutomation(Accessibility = "FakeAccebility")] private IWebElement _inconsistentElement1;

        [FindsBy(How = How.ClassName, Using = "FakeHtmlClass")]
        [FindsByIOSUIAutomation(Accessibility = "FakeAccebility")] private IList<IWebElement> _inconsistentElements1;

        [FindsBy(How = How.CssSelector, Using = "fake.css")] [FindsByIOSUIAutomation(Accessibility = "FakeAccebility")]
        private IWebElement _inconsistentElement2;

        [FindsBy(How = How.CssSelector, Using = "fake.css")] [FindsByIOSUIAutomation(Accessibility = "FakeAccebility")]
        private IList<IWebElement> _inconsistentElements2;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver<AppiumWebElement>(serverUri, capabilities, Env.InitTimeoutSec);
            var timeSpan = new TimeOutDuration(new TimeSpan(0, 0, 0, 5, 0));
            PageFactory.InitElements(_driver, this, new AppiumPageObjectMemberDecorator(timeSpan));
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
        public void WhenThereIsNoConsistentLocatorForCurrentPlatform_NoSuchElementExceptionShouldBeThrown()
        {
            try
            {
                var text = _inconsistentElement1.Text;
            }
            catch (NoSuchElementException e)
            {
                //it is expected
                return;
            }
        }

        [Test]
        public void WhenThereIsNoConsistentLocatorForCurrentPlatform_EmptyListShouldBeFound()
        {
            Assert.AreEqual(0, _inconsistentElements1.Count);
        }

        [Test]
        public void WhenDefaultLocatorIsInvalidForCurrentPlatform_NoSuchElementExceptionShouldBeThrown()
        {
            try
            {
                var text = _inconsistentElement2.Text;
            }
            catch (NoSuchElementException e)
            {
                //it is expected
                return;
            }
        }

        [Test]
        public void WhenDefaultLocatorIsInvalidForCurrentPlatform_EmptyListShouldBeFound()
        {
            Assert.AreEqual(0, _inconsistentElements2.Count);
        }
    }
}