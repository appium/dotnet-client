using Appium.Integration.Tests.Helpers;
using Appium.Integration.Tests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.PageObjects;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace Appium.Integration.Tests.PageObjectTests.Other
{
    [TestFixture()]
    class AndroidTouchActionTest
    {
        private AndroidDriver<AppiumWebElement> driver;
        private AndroidPageObjectThatChecksTouchActions pageObject;

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
                capabilities.SetCapability("tags", new string[] { "sample" });
            }
            Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIAndroid;
            driver = new AndroidDriver<AppiumWebElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            TimeOutDuration timeSpan = new TimeOutDuration(new TimeSpan(0, 0, 0, 5, 0));
            pageObject = new AndroidPageObjectThatChecksTouchActions();
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
        public void CheckTap()
        {
            pageObject.CheckTap(driver);
        }
    }
}
