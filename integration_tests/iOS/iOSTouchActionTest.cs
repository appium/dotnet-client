using NUnit.Framework;
using System;
using Appium.Integration.Tests.Helpers;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Integration.Tests.iOS
{
    [TestFixture()]
    public class iOSTouchActionTest
    {
        private AppiumDriver<IWebElement> driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            AppiumOptions capabilities = Caps.GetIOSCaps(Apps.get("iosTestApp"));
            if (Env.ServerIsRemote())
            {
                capabilities.AddAdditionalCapability("username", Env.GetEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.GetEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("name", "ios - actions");
                capabilities.AddAdditionalCapability("tags", new string[] {"sample"});
            }
            Uri serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            driver = new IOSDriver<IWebElement>(serverUri, capabilities, Env.InitTimeoutSec);
            driver.Manage().Timeouts().ImplicitWait= Env.ImplicitTimeoutSec;
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
        public void SimpleActionTestCase()
        {
            driver.FindElementById("TextField1").Clear();
            driver.FindElementById("TextField1").SendKeys("1");
            driver.FindElementById("TextField2").Clear();
            driver.FindElementById("TextField2").SendKeys("3");
            IWebElement el = driver.FindElementByAccessibilityId("ComputeSumButton");
            ITouchAction action = new TouchAction(driver);
            action.Press(el, 10, 10).Release();
            action.Perform();
            const string str = "4";
            Assert.AreEqual(driver.FindElementByXPath("//*[@name = \"Answer\"]").Text, str);
        }

        [Test()]
        public void MultiActionTestCase()
        {
            driver.FindElementById("TextField1").Clear();
            driver.FindElementById("TextField1").SendKeys("2");
            driver.FindElementById("TextField2").Clear();
            driver.FindElementById("TextField2").SendKeys("4");
            IWebElement el = driver.FindElementByAccessibilityId("ComputeSumButton");
            ITouchAction a1 = new TouchAction(driver);
            a1.Tap(el, 10, 10);
            ITouchAction a2 = new TouchAction(driver);
            a2.Tap(el);
            IMultiAction m = new MultiAction(driver);
            m.Add(a1).Add(a2);
            m.Perform();
            const string str = "6";
            Assert.AreEqual(driver.FindElementByXPath("//*[@name = \"Answer\"]").Text, str);
        }
    }
}