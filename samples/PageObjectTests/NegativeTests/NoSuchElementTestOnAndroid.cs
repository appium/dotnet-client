using Appium.Samples.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.PageObjects;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;

namespace Appium.Samples.PageObjectTests.NegativeTests
{
    public class NoSuchElementTestOnAndroid
    {
        private AndroidDriver<AppiumWebElement> driver;
        private bool allPassed = true;

        [FindsBy(How = How.ClassName, Using = "FakeHtmlClass")]
        [FindsByIOSUIAutomation(Accessibility = "FakeAccebility")]
        private IWebElement inconsistentElement1;

        [FindsBy(How = How.ClassName, Using = "FakeHtmlClass")]
        [FindsByIOSUIAutomation(Accessibility = "FakeAccebility")]
        private IList<IWebElement> inconsistentElements1;

        [FindsBy(How = How.CssSelector, Using = "fake.css")]
        [FindsByIOSUIAutomation(Accessibility = "FakeAccebility")]
        private IWebElement inconsistentElement2;

        [FindsBy(How = How.CssSelector, Using = "fake.css")]
        [FindsByIOSUIAutomation(Accessibility = "FakeAccebility")]
        private IList<IWebElement> inconsistentElements2;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            DesiredCapabilities capabilities = Env.isSauce() ?
                Caps.getAndroid18Caps(Apps.get("androidApiDemos")) :
                Caps.getAndroid19Caps(Apps.get("androidApiDemos"));
            if (Env.isSauce())
            {
                capabilities.SetCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
                capabilities.SetCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.SetCapability("name", "android - complex");
                capabilities.SetCapability("tags", new string[] { "sample" });
            }
            Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.localURI;
            driver = new AndroidDriver<AppiumWebElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            TimeOutDuration timeSpan = new TimeOutDuration(new TimeSpan(0, 0, 0, 5, 0));
            PageFactory.InitElements(driver, this, new AppiumPageObjectMemberDecorator(timeSpan));
        }

        [TestFixtureTearDown]
        public void AfterEach()
        {
            allPassed = allPassed && (TestContext.CurrentContext.Result.State == TestState.Success);
            if (Env.isSauce())
                ((IJavaScriptExecutor)driver).ExecuteScript("sauce:job-result=" + (allPassed ? "passed" : "failed"));
            driver.Quit();
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
