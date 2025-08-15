using System;
using System.Collections.Generic;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Android.Enums;
using OpenQA.Selenium.Support.UI;

namespace Appium.Net.Integration.Tests.Android
{
    [TestFixture]
    public class ActivityTest
    {
        private AndroidDriver _driver;
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
        public void CurrentActivityTest()
        {
            _driver.ExecuteScript(
                "mobile:startActivity",
                new object[] {
                    new Dictionary<string, object>() {
                        ["intent"] = "io.appium.android.apis/.ApiDemos",
                        ["wait"] = true,
                    }
                }
            );
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(drv => {
                return ((AndroidDriver) drv).CurrentActivity == ".ApiDemos";
            });
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            _driver?.Quit();
            if (!Env.ServerIsRemote()) AppiumServers.StopLocalService();
        }
    }
}