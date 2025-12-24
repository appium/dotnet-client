using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System.Collections.Generic;

namespace Appium.Net.Integration.Tests.Element
{
    public class AppiumElementCacheTest
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
            _driver.ExecuteScript(
                "mobile:startActivity",
                [
                    new Dictionary<string, object>() {
                        ["intent"] = "io.appium.android.apis/.ApiDemos",
                    }
                ]
            );
        }

        [Test]
        public void TagName_WithCacheDisabled_ReturnsTagNameFromServer()
        {
            AppiumElement element = _driver.FindElement(MobileBy.AccessibilityId("Graphics"));

            var tagName = element.TagName;

            Assert.That(tagName, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void TagName_WithCacheEnabled_ReturnsCachedValue()
        {
            var element = _driver.FindElement(MobileBy.AccessibilityId("Graphics"));
            var expectedTagName = "android.widget.TextView";

            // Enable cache and set the cached value
            element.SetCacheValues(new Dictionary<string, object>
            {
                { "name", expectedTagName }
            });

            var tagName = element.TagName;

            Assert.That(tagName, Is.EqualTo(expectedTagName));
        }

        [Test]
        public void TagName_WithEmptyCacheAndCacheEnabled_FetchesAndCachesValue()
        {
            var element = _driver.FindElement(MobileBy.AccessibilityId("Graphics"));

            // Enable cache with empty dictionary
            element.SetCacheValues(new Dictionary<string, object>());

            // First call should fetch from server and cache
            var tagName1 = element.TagName;

            // Second call should return cached value
            var tagName2 = element.TagName;

            Assert.Multiple(() =>
            {
                Assert.That(tagName1, Is.Not.Null.And.Not.Empty);
                Assert.That(tagName2, Is.EqualTo(tagName1));
            });
        }

        [Test]
        public void TagName_AfterClearCache_FetchesFromServerAgain()
        {
            var element = _driver.FindElement(MobileBy.AccessibilityId("Graphics"));

            // Enable cache and set initial value
            element.SetCacheValues(new Dictionary<string, object>
            {
                { "name", "cachedTagName" }
            });

            // Verify cached value is returned
            var cachedTagName = element.TagName;
            Assert.That(cachedTagName, Is.EqualTo("cachedTagName"));

            // Clear cache
            element.ClearCache();

            // Now it should fetch from server (cache is still enabled but empty)
            var freshTagName = element.TagName;
            Assert.That(freshTagName, Is.Not.EqualTo("cachedTagName"));
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
