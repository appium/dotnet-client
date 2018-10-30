using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android
{
    [TestFixture]
    public class AndroidSearchingTest
    {
        private AndroidDriver<AndroidElement> _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidCaps(Apps.Get("androidApiDemos"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver<AndroidElement>(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [SetUp]
        public void SetUp()
        {
            _driver.StartActivity("io.appium.android.apis", ".ApiDemos");
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            _driver?.Quit();
            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test]
        public void FindByAccessibilityIdTest()
        {
            By byAccessibilityId = new ByAccessibilityId("Graphics");
            Assert.AreNotEqual(_driver.FindElement(byAccessibilityId).Text, null);
            Assert.GreaterOrEqual(_driver.FindElements(byAccessibilityId).Count, 1);
        }

        [Test]
        public void FindByAndroidUiAutomatorTest()
        {
            By byAndroidUiAutomator = new ByAndroidUIAutomator("new UiSelector().clickable(true)");
            Assert.IsNotNull(_driver.FindElement(byAndroidUiAutomator).Text);
            Assert.GreaterOrEqual(_driver.FindElements(byAndroidUiAutomator).Count, 1);
        }

        [Test]
        public void FindByXPathTest()
        {
            var byXPath = "//android.widget.TextView[contains(@text, 'Animat')]";
            Assert.IsNotNull(_driver.FindElementByXPath(byXPath).Text);
            Assert.AreEqual(_driver.FindElementsByXPath(byXPath).Count, 1);
        }

        [Test]
        public void FindScrollable()
        {
            _driver.FindElementByAccessibilityId("Views").Click();
            var radioGroup = _driver
                .FindElementByAndroidUIAutomator("new UiScrollable(new UiSelector()"
                                                 + ".resourceId(\"android:id/list\")).scrollIntoView("
                                                 + "new UiSelector().text(\"Radio Group\"));");
            Assert.NotNull(radioGroup.Location);
        }
    }
}