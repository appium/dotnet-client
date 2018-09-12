using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;

namespace Appium.Integration.Tests.iOS
{
    class iOSElementTest
    {
        private AppiumDriver<IOSElement> driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            AppiumOptions capabilities = Caps.getIos92Caps(Apps.get("iosTestApp"));
            if (Env.isSauce())
            {
                capabilities.AddAdditionalCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("name", "ios - complex");
                capabilities.AddAdditionalCapability("tags", new string[] {"sample"});
            }
            Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIForIOS;
            driver = new IOSDriver<IOSElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
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
        public void FindByAccessibilityIdTest()
        {
            By byAccessibilityId = new ByAccessibilityId("ComputeSumButton");
            Assert.AreNotEqual(driver.FindElementsByClassName("UIAWindow")[1].FindElement(byAccessibilityId).Text,
                null);
            Assert.GreaterOrEqual(driver.FindElementsByClassName("UIAWindow")[1].FindElements(byAccessibilityId).Count,
                1);
        }

        [Test()]
        public void FindByByIosUIAutomationTest()
        {
            By byIosUIAutomation = new ByIosUIAutomation(".elements().withName(\"Answer\")");
            Assert.IsNotNull(driver.FindElementsByClassName("UIAWindow")[1].FindElement(byIosUIAutomation).Text);
            Assert.GreaterOrEqual(driver.FindElementsByClassName("UIAWindow")[1].FindElements(byIosUIAutomation).Count,
                1);
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