using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Appium.Integration.Tests.Android
{
    [TestFixture]
    public class AndroidActivityTest
    {
        private AndroidDriver<AppiumWebElement> driver;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            DesiredCapabilities capabilities = Env.isSauce() ?
                Caps.getAndroid18Caps(Apps.get("androidApiDemos")) :
                Caps.getAndroid19Caps(Apps.get("androidApiDemos"));
            if (Env.isSauce())
            {
                capabilities.SetCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
                capabilities.SetCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.SetCapability("name", "android - complex");
                capabilities.SetCapability("tags", new string[] { "sample" });
            }
            Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIAndroid;
            driver = new AndroidDriver<AppiumWebElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            driver.Manage().Timeouts().ImplicitlyWait(Env.IMPLICIT_TIMEOUT_SEC);
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

            _AssertActivityNameContains("Demos");

            driver.StartActivity("io.appium.android.apis", ".accessibility.AccessibilityNodeProviderActivity");

            _AssertActivityNameContains("Node");
        }

        [Test]
        public void StartActivityWithWaitingAppTestCase()
        {
            driver.StartActivity("io.appium.android.apis", ".ApiDemos", "io.appium.android.apis", ".ApiDemos");

            _AssertActivityNameContains("Demos");

            driver.StartActivity("io.appium.android.apis", ".accessibility.AccessibilityNodeProviderActivity", 
                "io.appium.android.apis", ".accessibility.AccessibilityNodeProviderActivity");

            _AssertActivityNameContains("Node");
        }

        [Test]
        public void StartActivityInNewAppTestCase()
        {
            driver.StartActivity("io.appium.android.apis", ".ApiDemos");

            _AssertActivityNameContains("Demos");

            driver.StartActivity("com.android.contacts", ".ContactsListActivity");

            _AssertActivityNameContains("Contact");
            driver.KeyEvent(AndroidKeyCode.Back);
            _AssertActivityNameContains("Contact");
        }

        [Test]
        public void StartActivityInNewAppTestCaseWithoutClosingApp()
        {
            driver.StartActivity("io.appium.android.apis", ".accessibility.AccessibilityNodeProviderActivity");

            _AssertActivityNameContains("Node");

            driver.StartActivity("com.android.contacts", ".ContactsListActivity", "com.android.contacts", ".ContactsListActivity", false);

            _AssertActivityNameContains("Contact");
            driver.KeyEvent(AndroidKeyCode.Back);
            _AssertActivityNameContains("Node");

        }

        private void _AssertActivityNameContains(string activityName)
        {
            Contract.Requires(!String.IsNullOrWhiteSpace(activityName));

            String activity = driver.CurrentActivity;
            Debug.WriteLine(activity);

            Assert.IsNotNullOrEmpty(activity);
            Assert.IsTrue(activity.Contains(activityName));
        }

        [TestFixtureTearDown]
        public void AfterAll()
        {
            if (driver != null)
            {
                driver.Quit();
            }
            if (!Env.isSauce())
            {
                AppiumServers.StopLocalService();
            }
        }
    }
}
