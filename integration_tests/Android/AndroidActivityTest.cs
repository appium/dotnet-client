using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Appium.Integration.Tests.Android
{
    [TestFixture]
    public class AndroidActivityTest
    {
        private AndroidDriver<AppiumWebElement> driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            AppiumOptions capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidCaps(Apps.get("androidApiDemos"))
                : Caps.GetAndroidCaps(Apps.get("androidApiDemos"));
            if (Env.ServerIsRemote())
            {
                capabilities.AddAdditionalCapability("username", Env.GetEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.GetEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("name", "android - complex");
                capabilities.AddAdditionalCapability("tags", new string[] {"sample"});
            }
            Uri serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            driver = new AndroidDriver<AppiumWebElement>(serverUri, capabilities, Env.InitTimeoutSec);
            driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
            driver.CloseApp();
        }

        [SetUp]
        public void SetUp()
        {
            if (driver != null)
            {
                driver.LaunchApp();
            }
        }

        [TearDown]
        public void TearDowwn()
        {
            if (driver != null)
            {
                driver.CloseApp();
            }
        }

        [Test]
        public void StartActivityInThisAppTestCase()
        {
            driver.StartActivity("io.appium.android.apis", ".ApiDemos");

            Assert.AreEqual(driver.CurrentActivity, ".ApiDemos");

            driver.StartActivity("io.appium.android.apis", ".accessibility.AccessibilityNodeProviderActivity");

            Assert.AreEqual(driver.CurrentActivity, ".accessibility.AccessibilityNodeProviderActivity");
        }

        [Test]
        public void StartActivityWithWaitingAppTestCase()
        {
            driver.StartActivity("io.appium.android.apis", ".ApiDemos", "io.appium.android.apis", ".ApiDemos");

            Assert.AreEqual(driver.CurrentActivity, ".ApiDemos");

            driver.StartActivity("io.appium.android.apis", ".accessibility.AccessibilityNodeProviderActivity",
                "io.appium.android.apis", ".accessibility.AccessibilityNodeProviderActivity");

            Assert.AreEqual(driver.CurrentActivity, ".accessibility.AccessibilityNodeProviderActivity");
        }

        [Test]
        public void StartActivityInNewAppTestCase()
        {
            driver.StartActivity("io.appium.android.apis", ".ApiDemos");

            Assert.AreEqual(driver.CurrentActivity, ".ApiDemos");

            driver.StartActivity("com.android.contacts", ".ContactsListActivity");

            Assert.AreEqual(driver.CurrentActivity, ".ContactsListActivity");
            driver.PressKeyCode(AndroidKeyCode.Back);
            Assert.AreEqual(driver.CurrentActivity, ".ContactsListActivity");
        }

        [Test]
        public void StartActivityInNewAppTestCaseWithoutClosingApp()
        {
            driver.StartActivity("io.appium.android.apis", ".accessibility.AccessibilityNodeProviderActivity");

            Assert.AreEqual(driver.CurrentActivity, ".accessibility.AccessibilityNodeProviderActivity");

            driver.StartActivity("com.android.contacts", ".ContactsListActivity", "com.android.contacts",
                ".ContactsListActivity", false);

            Assert.AreEqual(driver.CurrentActivity, ".ContactsListActivity");
            driver.PressKeyCode(AndroidKeyCode.Back);
            Assert.AreEqual(driver.CurrentActivity, ".accessibility.AccessibilityNodeProviderActivity");
        }

        [OneTimeTearDown]
        public void AfterAll()
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
    }
}