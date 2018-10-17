using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appium.Integration.Tests.Android
{
    class IntentAndroidTest
    {
        private AndroidDriver<AppiumWebElement> driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            AppiumOptions capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidCaps(Apps.get("intentApp"))
                : Caps.GetAndroidCaps(Apps.get("intentApp"));
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
        }

        [Test]
        public void StartActivityWithDefaultIntentAndDefaultCategoryWithOptionalArgs()
        {
            driver.StartActivityWithIntent("com.prgguru.android", ".GreetingActivity", "android.intent.action.MAIN",
                null, null,
                "android.intent.category.DEFAULT", "0x4000000",
                "--es \"USERNAME\" \"AppiumIntentTest\" -t \"text/plain\"");
            Assert.AreEqual(driver.FindElementById("com.prgguru.android:id/textView1").Text,
                "Welcome AppiumIntentTest");
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