using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Android.UiAutomator;
using System.Collections.Generic;

namespace Appium.Net.Integration.Tests.Android
{
    public class ElementTest
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

        [Test]
        public void FindByAccessibilityIdTest()
        {
            By byAccessibilityId = new ByAccessibilityId("Graphics");
            Assert.Multiple(() =>
            {
                Assert.That(_driver.FindElement(MobileBy.Id("android:id/content")).FindElement(byAccessibilityId).Text, Is.Not.EqualTo(null));
                Assert.That(_driver.FindElement(MobileBy.Id("android:id/content")).Text, Is.Not.EqualTo(null));
                Assert.That(_driver.FindElement(MobileBy.Id("android:id/content")).FindElements(byAccessibilityId), Is.Not.Empty);
            });
        }

        [Test]
        public void FindByAndroidUiAutomatorTest()
        {
            By byAndroidUiAutomator = new ByAndroidUIAutomator("new UiSelector().clickable(true)");
            Assert.Multiple(() =>
            {
                Assert.That(_driver.FindElement(MobileBy.Id("android:id/content")).FindElement(byAndroidUiAutomator).Text, Is.Not.Null);
                Assert.That(_driver.FindElement(MobileBy.Id("android:id/content")).FindElements(byAndroidUiAutomator), Is.Not.Empty);
            });
        }

        [Test]
        public void FindByAndroidUiAutomatorBuilderTest()
        {
            By byAndroidUiAutomator = new ByAndroidUIAutomator(new AndroidUiSelector().IsClickable(true));
            Assert.Multiple(() =>
            {
                Assert.That(_driver.FindElement(MobileBy.Id("android:id/content")).FindElement(byAndroidUiAutomator).Text, Is.Not.Null);
                Assert.That(
                    _driver.FindElement(MobileBy.Id("android:id/content")).FindElements(byAndroidUiAutomator), Is.Not.Empty);
            });
        }

        [Test]
        public void CanFindByDescriptionUsingBuilderWhenNewlineCharacterIncluded()
        {
            _driver.StartActivity("io.appium.android.apis", ".accessibility.TaskListActivity");
            By byAndroidUiAutomator = new ByAndroidUIAutomator(new AndroidUiSelector().DescriptionEquals(
                "1. Enable QueryBack (Settings -> Accessibility -> QueryBack). \n\n" +
                "2. Enable Explore-by-Touch (Settings -> Accessibility -> Explore by Touch). \n\n" +
                "3. Touch explore the list."));

            Assert.Multiple(() =>
            {
                Assert.That(_driver.FindElement(MobileBy.Id("android:id/content")).FindElement(byAndroidUiAutomator).Text, Is.Not.Null);
                Assert.That(_driver.FindElement(MobileBy.Id("android:id/content")).FindElements(byAndroidUiAutomator), Is.Not.Empty);
            });
        }

        [Test]
        public void CanFindByDescriptionUsingBuilderWhenDoubleQuoteCharacterIncluded()
        {
            _driver.StartActivity("io.appium.android.apis", ".text.Link");
            By byAndroidUiAutomator = new ByAndroidUIAutomator(new AndroidUiSelector()
                .DescriptionContains("Use a \"tel:\" URL"));

            Assert.Multiple(() =>
            {
                Assert.That(_driver.FindElement(MobileBy.Id("android:id/content")).FindElement(byAndroidUiAutomator).Text, Is.Not.Null);
                Assert.That(_driver.FindElement(MobileBy.Id("android:id/content")).FindElements(byAndroidUiAutomator), Is.Not.Empty);
            });
        }

        [Test]
        public void ReplaceValueTest()
        {
            var originalValue = "original value";
            var replacedValue = "replaced value";

            _driver.StartActivity("io.appium.android.apis", ".view.Controls1");

            var editElement =
                _driver.FindElement(MobileBy.AndroidUIAutomator("resourceId(\"io.appium.android.apis:id/edit\")"));

            editElement.SendKeys(originalValue);

            Assert.That(editElement.Text, Is.EqualTo(originalValue));

            _driver.ExecuteScript("mobile: replaceElementValue",
                new Dictionary<string, string> { { "elementId", editElement.Id } , { "text", replacedValue } });

            Assert.That(editElement.Text, Is.EqualTo(replacedValue));
        }


        [Test]
        public void ScrollingToSubElement()
        {
            _driver.FindElement(MobileBy.AccessibilityId("Views")).Click();
            var list = _driver.FindElement(By.Id("android:id/list"));
            var locator = new ByAndroidUIAutomator("new UiScrollable(new UiSelector()).scrollIntoView("
                                                   + "new UiSelector().text(\"Radio Group\"));");
            var radioGroup = list.FindElement(locator);
            Assert.Multiple(() =>
            {
                Assert.That(radioGroup.Location.X, Is.GreaterThanOrEqualTo(0));
                Assert.That(radioGroup.Location.Y, Is.GreaterThanOrEqualTo(0));
            });
        }

        [Test]
        public void ScrollingToSubElementUsingBuilder()
        {
            _driver.FindElement(MobileBy.AccessibilityId("Views")).Click();
            var list = _driver.FindElement(By.Id("android:id/list"));
            var locator = new ByAndroidUIAutomator(new AndroidUiScrollable()
                .ScrollIntoView(new AndroidUiSelector().TextEquals("Radio Group")));
            var radioGroup = list.FindElement(locator);
            Assert.Multiple(() =>
            {
                Assert.That(radioGroup.Location.X, Is.GreaterThanOrEqualTo(0));
                Assert.That(radioGroup.Location.Y, Is.GreaterThanOrEqualTo(0));
            });
        }

        [Test]
        public void FindAppiumElementUsingNestedElement()
        {
            var myElement = _driver.FindElement(MobileBy.Id("android:id/content"));
            AppiumElement nestedElement = myElement.FindElement(By.Id("android:id/text1"));
            Assert.That(nestedElement, Is.Not.Null);
        }

        [Test]
        public void FindAppiumElementsListUsingNestedElement()
        {
            var myElement = _driver.FindElement(MobileBy.Id("android:id/content"));
            IList<AppiumElement> myDerivedElements = myElement.FindElements(By.Id("android:id/text1"));
            Assert.That(myDerivedElements, Is.Not.Empty);
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