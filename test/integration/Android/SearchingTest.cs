using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android
{
    [TestFixture]
    public class SearchingTest
    {
        private AndroidDriver _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
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
            Assert.Multiple(() =>
            {
                Assert.That(_driver.FindElement(byAccessibilityId).Text, Is.EqualTo("Graphics"));
                Assert.That(_driver.FindElements(byAccessibilityId), Is.Not.Empty);
            });
        }

        [Test]
        public void FindByAndroidUiAutomatorTest()
        {
            By byAndroidUiAutomator = new ByAndroidUIAutomator("new UiSelector().clickable(true)");
            Assert.Multiple(() =>
            {
                Assert.That(_driver.FindElement(byAndroidUiAutomator).Text, Is.Not.Null);
                Assert.That(_driver.FindElements(byAndroidUiAutomator), Is.Not.Empty);
            });
        }

        [Test]
        public void FindByXPathTest()
        {
            var byXPath = "//android.widget.TextView[contains(@text, 'Animat')]";
            Assert.Multiple(() =>
            {
                Assert.That(_driver.FindElement(By.XPath(byXPath)).Text, Is.Not.Null);
                Assert.That(_driver.FindElements(By.XPath(byXPath)), Has.Count.EqualTo(1));
            });
        }

        [Test]
        public void FindScrollable()
        {
            _driver.FindElement(MobileBy.AccessibilityId("Views")).Click();
            var radioGroup = _driver
                .FindElement(MobileBy.AndroidUIAutomator("new UiScrollable(new UiSelector()"
                                                 + ".resourceId(\"android:id/list\")).scrollIntoView("
                                                 + "new UiSelector().text(\"Radio Group\"));"));
            Assert.That(radioGroup.Location.X, Is.Not.Negative);
        }
    }
}