using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using System;

namespace Appium.Net.Integration.Tests.IOS
{
    [TestFixture]
    [Obsolete("Touch Actions are deprecated")]
    //TODO: remove this test once we deprecate touch actions
    public class TouchActionTest
    {
        private AppiumDriver _driver;

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
            _driver = new IOSDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait= Env.ImplicitTimeoutSec;
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
        public void SimpleActionTestCase()
        {
            _driver.FindElement(MobileBy.Id("TextField1")).Clear();
            _driver.FindElement(MobileBy.Id("TextField1")).SendKeys("1");
            _driver.FindElement(MobileBy.Id("TextField2")).Clear();
            _driver.FindElement(MobileBy.Id("TextField2")).SendKeys("3");
            var el = _driver.FindElement(MobileBy.AccessibilityId("ComputeSumButton"));
            ITouchAction action = new TouchAction(_driver);
            action.Press(el, 10, 10).Release();
            action.Perform();
            const string str = "4";
            Assert.That(_driver.FindElement(MobileBy.XPath("//*[@name = \"Answer\"]")).Text, Is.EqualTo(str));
        }

        [Test]
        public void MultiActionTestCase()
        {
            _driver.FindElement(MobileBy.Id("TextField1")).Clear();
            _driver.FindElement(MobileBy.Id("TextField1")).SendKeys("2");
            _driver.FindElement(MobileBy.Id("TextField2")).Clear();
            _driver.FindElement(MobileBy.Id("TextField2")).SendKeys("4");
            var el = _driver.FindElement(MobileBy.AccessibilityId("ComputeSumButton"));
            ITouchAction a1 = new TouchAction(_driver);
            a1.Tap(el, 10, 10);
            ITouchAction a2 = new TouchAction(_driver);
            a2.Tap(el);
            IMultiAction m = new MultiAction(_driver);
            m.Add(a1).Add(a2);
            m.Perform();
            const string str = "6";
            Assert.That(_driver.FindElement(MobileBy.XPath("//*[@name = \"Answer\"]")).Text, Is.EqualTo(str));
        }
    }
}