using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.PageObjects;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium.Remote;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;

namespace Appium.Integration.Tests.PageObjectTests.NegativeTests
{
    public class NoSuchElementTestOnAndroid
    {
        private AndroidDriver<AppiumWebElement> driver;

        [FindsBy(How = How.ClassName, Using = "FakeHtmlClass")]
        [FindsByIOSUIAutomation(Accessibility = "FakeAccebility")] private IWebElement inconsistentElement1;

        [FindsBy(How = How.ClassName, Using = "FakeHtmlClass")]
        [FindsByIOSUIAutomation(Accessibility = "FakeAccebility")] private IList<IWebElement> inconsistentElements1;

        [FindsBy(How = How.CssSelector, Using = "fake.css")] [FindsByIOSUIAutomation(Accessibility = "FakeAccebility")]
        private IWebElement inconsistentElement2;

        [FindsBy(How = How.CssSelector, Using = "fake.css")] [FindsByIOSUIAutomation(Accessibility = "FakeAccebility")]
        private IList<IWebElement> inconsistentElements2;

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
            PageFactory.InitElements(driver, this, new AppiumPageObjectMemberDecorator(timeSpan));
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
        public void WhenThereIsNoConsistentLocatorForCurrentPlatform_NoSuchElementExceptionShouldBeThrown()
        {
            try
            {
                string text = inconsistentElement1.Text;
            }
            catch (NoSuchElementException e)
            {
                //it is expected
                return;
            }
        }

        [Test()]
        public void WhenThereIsNoConsistentLocatorForCurrentPlatform_EmptyListShouldBeFound()
        {
            Assert.AreEqual(0, inconsistentElements1.Count);
        }

        [Test()]
        public void WhenDefaultLocatorIsInvalidForCurrentPlatform_NoSuchElementExceptionShouldBeThrown()
        {
            try
            {
                string text = inconsistentElement2.Text;
            }
            catch (NoSuchElementException e)
            {
                //it is expected
                return;
            }
        }

        [Test()]
        public void WhenDefaultLocatorIsInvalidForCurrentPlatform_EmptyListShouldBeFound()
        {
            Assert.AreEqual(0, inconsistentElements2.Count);
        }
    }
}