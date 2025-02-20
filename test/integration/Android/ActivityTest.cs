using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Android.Enums;

namespace Appium.Net.Integration.Tests.Android
{
    [TestFixture]
    public class ActivityTest
    {
        private AndroidDriver _driver;
        private const string ContactsActivity = "com.android.contacts.activities.PeopleActivity";
        private const string AppId = "io.appium.android.apis";

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));

            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
            _driver.TerminateApp(AppId);
        }

        [SetUp]
        public void SetUp()
        {
            _driver?.ActivateApp(AppId);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.TerminateApp(AppId);
        }

        [Test]
        public void StartActivityInThisAppTestCase()
        {
            _driver.StartActivity(AppId, ".ApiDemos");

            Assert.That(_driver.CurrentActivity, Is.EqualTo(".ApiDemos"));

            _driver.StartActivity(AppId, ".accessibility.AccessibilityNodeProviderActivity");

            Assert.That(_driver.CurrentActivity, Is.EqualTo(".accessibility.AccessibilityNodeProviderActivity"));
        }

        [Test]
        public void StartActivityWithWaitingAppTestCase()
        {
            _driver.StartActivity(AppId, ".ApiDemos", AppId, ".ApiDemos");

            Assert.That(_driver.CurrentActivity, Is.EqualTo(".ApiDemos"));

            _driver.StartActivity(AppId, ".accessibility.AccessibilityNodeProviderActivity",
                "io.appium.android.apis", ".accessibility.AccessibilityNodeProviderActivity");

            Assert.That(_driver.CurrentActivity, Is.EqualTo(".accessibility.AccessibilityNodeProviderActivity"));
        }

        [Test]
        public void StartActivityInNewAppTestCase()
        {
            _driver.StartActivity(AppId, ".ApiDemos");

            Assert.That(_driver.CurrentActivity, Is.EqualTo(".ApiDemos"));

            _driver.StartActivity("com.google.android.contacts", ContactsActivity);

            Assert.That(_driver.CurrentActivity, Is.EqualTo(ContactsActivity));
            _driver.PressKeyCode(AndroidKeyCode.Back);
            Assert.That(_driver.CurrentActivity, Is.EqualTo(".ApiDemos"));
        }

        [Test]
        public void StartActivityInNewAppTestCaseWithoutClosingApp()
        {
            _driver.StartActivity(AppId, ".accessibility.AccessibilityNodeProviderActivity");

            Assert.That(_driver.CurrentActivity, Is.EqualTo(".accessibility.AccessibilityNodeProviderActivity"));

            _driver.StartActivity("com.google.android.contacts", ContactsActivity, "com.google.android.contacts",
                ContactsActivity, false);

            Assert.That(_driver.CurrentActivity, Is.EqualTo(ContactsActivity));
            _driver.PressKeyCode(AndroidKeyCode.Back);
            Assert.That(_driver.CurrentActivity, Is.EqualTo(".accessibility.AccessibilityNodeProviderActivity"));
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            _driver?.Quit();
            if (!Env.ServerIsRemote()) AppiumServers.StopLocalService();
        }
    }
}