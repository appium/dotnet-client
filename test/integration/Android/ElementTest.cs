using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Android.UiAutomator;

namespace Appium.Net.Integration.Tests.Android
{
    public class ElementTest
    {
        private AndroidDriver<AndroidElement> _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver<AndroidElement>(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [SetUp]
        public void SetUp()
        {
            _driver.StartActivity("io.appium.android.apis", ".ApiDemos");
        }

        [Test]
        public void FindByAccessibilityIdTest()
        {
            By byAccessibilityId = new ByAccessibilityId("Graphics");
            Assert.AreNotEqual(_driver.FindElementById("android:id/content").FindElement(byAccessibilityId).Text, null);
            Assert.GreaterOrEqual(_driver.FindElementById("android:id/content").FindElements(byAccessibilityId).Count,
                1);
        }

        [Test]
        public void FindByAndroidUiAutomatorTest()
        {
            By byAndroidUiAutomator = new ByAndroidUIAutomator("new UiSelector().clickable(true)");
            Assert.IsNotNull(_driver.FindElementById("android:id/content").FindElement(byAndroidUiAutomator).Text);
            Assert.GreaterOrEqual(_driver.FindElementById("android:id/content").FindElements(byAndroidUiAutomator).Count,
                1);
        }

        [Test]
        public void FindByAndroidUiAutomatorBuilderTest()
        {
            By byAndroidUiAutomator = new ByAndroidUIAutomator(new AndroidUiSelector().IsClickable(true));
            Assert.IsNotNull(_driver.FindElementById("android:id/content").FindElement(byAndroidUiAutomator).Text);
            Assert.GreaterOrEqual(
                _driver.FindElementById("android:id/content").FindElements(byAndroidUiAutomator).Count,
                1);
        }

        [Test]
        public void ReplaceValueTest()
        {
            var originalValue = "original value";
            var replacedValue = "replaced value";

            _driver.StartActivity("io.appium.android.apis", ".view.Controls1");

            var editElement =
                _driver.FindElementByAndroidUIAutomator("resourceId(\"io.appium.android.apis:id/edit\")");

            editElement.SendKeys(originalValue);

            Assert.AreEqual(originalValue, editElement.Text);

            editElement.ReplaceValue(replacedValue);

            Assert.AreEqual(replacedValue, editElement.Text);
        }

        [Test]
        public void SetImmediateValueTest()
        {
            var value = "new value";

            _driver.StartActivity("io.appium.android.apis", ".view.Controls1");

            var editElement =
                _driver.FindElementByAndroidUIAutomator("resourceId(\"io.appium.android.apis:id/edit\")");

            editElement.SetImmediateValue(value);

            Assert.AreEqual(value, editElement.Text);
        }

        [Test]
        public void ScrollingToSubElement()
        {
            _driver.FindElementByAccessibilityId("Views").Click();
            var list = _driver.FindElement(By.Id("android:id/list"));
            var locator = new ByAndroidUIAutomator("new UiScrollable(new UiSelector()).scrollIntoView("
                                                   + "new UiSelector().text(\"Radio Group\"));");
            var radioGroup = list.FindElement(locator);
            Assert.NotNull(radioGroup.Location);
        }

        [Test]
        public void ScrollingToSubElementUsingBuilder()
        {
            _driver.FindElementByAccessibilityId("Views").Click();
            var list = _driver.FindElement(By.Id("android:id/list"));
            var locator = new ByAndroidUIAutomator(new AndroidUiScrollable()
                .ScrollIntoView(new AndroidUiSelector().TextEquals("Radio Group")));
            var radioGroup = list.FindElement(locator);
            Assert.NotNull(radioGroup.Location);
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
    }
}