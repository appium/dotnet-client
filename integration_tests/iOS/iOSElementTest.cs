using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appium.Integration.Tests.iOS
{
    class iOSElementTest
    {
        private AppiumDriver<IOSElement> driver;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            DesiredCapabilities capabilities = Caps.getIos82Caps(Apps.get("iosTestApp"));
            if (Env.isSauce())
            {
                capabilities.SetCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
                capabilities.SetCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.SetCapability("name", "ios - complex");
                capabilities.SetCapability("tags", new string[] { "sample" });
            }
            Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIForIOS;
            driver = new IOSDriver<IOSElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            driver.Manage().Timeouts().ImplicitlyWait(Env.IMPLICIT_TIMEOUT_SEC);
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
        public void FindByAccessibilityIdTest()
        {
            By byAccessibilityId = new ByAccessibilityId("ComputeSumButton");
            Assert.AreNotEqual(driver.FindElementByClassName("UIAWindow").FindElement(byAccessibilityId).Text, null);
            Assert.GreaterOrEqual(driver.FindElementByClassName("UIAWindow").FindElements(byAccessibilityId).Count, 1);
        }

        [Test()]
        public void FindByByIosUIAutomationTest()
        {
            By byIosUIAutomation = new ByIosUIAutomation(".elements().withName(\"Answer\")");
            Assert.IsNotNull(driver.FindElementByClassName("UIAWindow").FindElement(byIosUIAutomation).Text);
            Assert.GreaterOrEqual(driver.FindElementByClassName("UIAWindow").FindElements(byIosUIAutomation).Count, 1);
        }

        [Test()]
        public void SetImmediateValueTest()
        {
            IOSElement slider = driver.FindElementByClassName("UIASlider");
            slider.SetImmediateValue("0%");
            Assert.AreEqual("0%", slider.GetAttribute("value"));
        }
    }
}
