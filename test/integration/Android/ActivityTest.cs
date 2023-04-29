using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android
{
    [TestFixture]
    public class ActivityTest
    {
        private AndroidDriver _driver;
        private const string ContactsActivity = ".activities.PeopleActivity";
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
        public void TearDowwn()
        {
            _driver.TerminateApp(AppId);
        }

        [Test]
        public void StartActivityInThisAppTestCase()
        {
            _driver.StartActivity(AppId, ".ApiDemos");

            Assert.AreEqual(_driver.CurrentActivity, ".ApiDemos");

            _driver.StartActivity(AppId, ".accessibility.AccessibilityNodeProviderActivity");

            Assert.AreEqual(_driver.CurrentActivity, ".accessibility.AccessibilityNodeProviderActivity");
        }

        [Test]
        public void StartActivityWithWaitingAppTestCase()
        {
            _driver.StartActivity(AppId, ".ApiDemos", AppId, ".ApiDemos");

            Assert.AreEqual(_driver.CurrentActivity, ".ApiDemos");

            _driver.StartActivity(AppId, ".accessibility.AccessibilityNodeProviderActivity",
                "io.appium.android.apis", ".accessibility.AccessibilityNodeProviderActivity");

            Assert.AreEqual(_driver.CurrentActivity, ".accessibility.AccessibilityNodeProviderActivity");
        }

        [Test]
        public void StartActivityInNewAppTestCase()
        {
            _driver.StartActivity(AppId, ".ApiDemos");

            Assert.AreEqual(_driver.CurrentActivity, ".ApiDemos");

            _driver.StartActivity("com.android.contacts", ContactsActivity);

            Assert.AreEqual(_driver.CurrentActivity, ContactsActivity);
            _driver.PressKeyCode(AndroidKeyCode.Back);
            Assert.AreEqual(_driver.CurrentActivity, ".ApiDemos");
        }

        [Test]
        public void StartActivityInNewAppTestCaseWithoutClosingApp()
        {
            _driver.StartActivity(AppId, ".accessibility.AccessibilityNodeProviderActivity");

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