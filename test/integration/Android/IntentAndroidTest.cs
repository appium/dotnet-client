using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android
{
    class IntentAndroidTest
    {
        private AndroidDriver _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("intentApp"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("intentApp"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [Test]
        public void StartActivityWithDefaultIntentAndDefaultCategoryWithOptionalArgs()
        {
            _driver.StartActivityWithIntent("com.prgguru.android", ".GreetingActivity", "android.intent.action.MAIN",
                null, null,
                "android.intent.category.DEFAULT", "0x4000000",
                "--es \"USERNAME\" \"AppiumIntentTest\" -t \"text/plain\"");
            Assert.AreEqual(_driver.FindElementById("com.prgguru.android:id/textView1").Text,
                "Welcome AppiumIntentTest");
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