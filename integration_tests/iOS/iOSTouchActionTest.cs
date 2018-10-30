using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;

namespace Appium.Net.Integration.Tests.iOS
{
    [TestFixture]
    public class IOsTouchActionTest
    {
        private AppiumDriver<IWebElement> _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetIosCaps(Apps.Get("iosTestApp"));
            if (Env.ServerIsRemote())
            {
                capabilities.AddAdditionalCapability("username", Env.GetEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.GetEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("name", "ios - actions");
                capabilities.AddAdditionalCapability("tags", new[] {"sample"});
            }
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new IOSDriver<IWebElement>(serverUri, capabilities, Env.InitTimeoutSec);
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
            _driver.FindElementById("TextField1").Clear();
            _driver.FindElementById("TextField1").SendKeys("1");
            _driver.FindElementById("TextField2").Clear();
            _driver.FindElementById("TextField2").SendKeys("3");
            var el = _driver.FindElementByAccessibilityId("ComputeSumButton");
            ITouchAction action = new TouchAction(_driver);
            action.Press(el, 10, 10).Release();
            action.Perform();
            const string str = "4";
            Assert.AreEqual(_driver.FindElementByXPath("//*[@name = \"Answer\"]").Text, str);
        }

        [Test]
        public void MultiActionTestCase()
        {
            _driver.FindElementById("TextField1").Clear();
            _driver.FindElementById("TextField1").SendKeys("2");
            _driver.FindElementById("TextField2").Clear();
            _driver.FindElementById("TextField2").SendKeys("4");
            var el = _driver.FindElementByAccessibilityId("ComputeSumButton");
            ITouchAction a1 = new TouchAction(_driver);
            a1.Tap(el, 10, 10);
            ITouchAction a2 = new TouchAction(_driver);
            a2.Tap(el);
            IMultiAction m = new MultiAction(_driver);
            m.Add(a1).Add(a2);
            m.Perform();
            const string str = "6";
            Assert.AreEqual(_driver.FindElementByXPath("//*[@name = \"Answer\"]").Text, str);
        }
    }
}