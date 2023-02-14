﻿using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android
{
    [TestFixture]
    public class ActivityTest
    {
        private AndroidDriver _driver;
        private const string ContactsActivity = ".activities.PeopleActivity";

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));

            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
            _driver.CloseApp();
        }

        [SetUp]
        public void SetUp()
        {
            _driver?.LaunchApp();
        }

        [TearDown]
        public void TearDowwn()
        {
            _driver?.CloseApp();
        }

        [Test]
        public void StartActivityInThisAppTestCase()
        {
            _driver.StartActivity("io.appium.android.apis", ".ApiDemos");

            Assert.AreEqual(_driver.CurrentActivity, ".ApiDemos");

            _driver.StartActivity("io.appium.android.apis", ".accessibility.AccessibilityNodeProviderActivity");

            Assert.AreEqual(_driver.CurrentActivity, ".accessibility.AccessibilityNodeProviderActivity");
        }

        [Test]
        public void StartActivityWithWaitingAppTestCase()
        {
            _driver.StartActivity("io.appium.android.apis", ".ApiDemos", "io.appium.android.apis", ".ApiDemos");

            Assert.AreEqual(_driver.CurrentActivity, ".ApiDemos");

            _driver.StartActivity("io.appium.android.apis", ".accessibility.AccessibilityNodeProviderActivity",
                "io.appium.android.apis", ".accessibility.AccessibilityNodeProviderActivity");

            Assert.AreEqual(_driver.CurrentActivity, ".accessibility.AccessibilityNodeProviderActivity");
        }

        [Test]
        public void StartActivityInNewAppTestCase()
        {
            _driver.StartActivity("io.appium.android.apis", ".ApiDemos");

            Assert.AreEqual(_driver.CurrentActivity, ".ApiDemos");

            _driver.StartActivity("com.android.contacts", ContactsActivity);

            Assert.AreEqual(_driver.CurrentActivity, ContactsActivity);
            _driver.PressKeyCode(AndroidKeyCode.Back);
            Assert.AreEqual(_driver.CurrentActivity, ".ApiDemos");
        }

        [Test]
        public void StartActivityInNewAppTestCaseWithoutClosingApp()
        {
            _driver.StartActivity("io.appium.android.apis", ".accessibility.AccessibilityNodeProviderActivity");

            Assert.AreEqual(_driver.CurrentActivity, ".accessibility.AccessibilityNodeProviderActivity");

            _driver.StartActivity("com.android.contacts", ContactsActivity, "com.android.contacts",
                ContactsActivity, false);

            Assert.AreEqual(_driver.CurrentActivity, ContactsActivity);
            _driver.PressKeyCode(AndroidKeyCode.Back);
            Assert.AreEqual(_driver.CurrentActivity, ".accessibility.AccessibilityNodeProviderActivity");
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            _driver?.Quit();
            if (!Env.ServerIsRemote()) AppiumServers.StopLocalService();
        }
    }
}