using System.Collections.Generic;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Android.Enums;

namespace Appium.Net.Integration.Tests.Android
{
    public class SettingTest
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
            _driver.CloseApp();
        }

        [Test]
        public void IgnoreUnimportantViewsTest()
        {
            _driver.IgnoreUnimportantViews(true);
            var ignoreViews =
                (bool) _driver.Settings[AutomatorSetting.IgnoreUnimportantViews];
            Assert.True(ignoreViews);
            _driver.IgnoreUnimportantViews(false);
            ignoreViews = (bool) _driver.Settings[AutomatorSetting.IgnoreUnimportantViews];
            Assert.False(ignoreViews);
        }

        [Test]
        public void ConfiguratorTest()
        {
            _driver.ConfiguratorSetActionAcknowledgmentTimeout(500);
            _driver.ConfiguratorSetKeyInjectionDelay(400);
            _driver.ConfiguratorSetScrollAcknowledgmentTimeout(300);
            _driver.ConfiguratorSetWaitForIdleTimeout(600);
            _driver.ConfiguratorSetWaitForSelectorTimeout(1000);

            var settings = _driver.Settings;
            Assert.AreEqual(settings[AutomatorSetting.KeyInjectionDelay], 400);
            Assert.AreEqual(settings[AutomatorSetting.WaitActionAcknowledgmentTimeout], 500);
            Assert.AreEqual(settings[AutomatorSetting.WaitForIDLETimeout], 600);
            Assert.AreEqual(settings[AutomatorSetting.WaitForSelectorTimeout], 1000);
            Assert.AreEqual(settings[AutomatorSetting.WaitScrollAcknowledgmentTimeout], 300);
        }

        [Test]
        public void ConfiguratorPropertyTest()
        {
            var data = new Dictionary<string, object>()
            {
                [AutomatorSetting.KeyInjectionDelay] = 1500,
                [AutomatorSetting.WaitActionAcknowledgmentTimeout] = 2500,
                [AutomatorSetting.WaitForIDLETimeout] = 3500,
                [AutomatorSetting.WaitForSelectorTimeout] = 5000,
                [AutomatorSetting.WaitScrollAcknowledgmentTimeout] = 7000
            };

            _driver.Settings = data;
            var settings = _driver.Settings;
            Assert.AreEqual(settings[AutomatorSetting.KeyInjectionDelay], 1500);
            Assert.AreEqual(settings[AutomatorSetting.WaitActionAcknowledgmentTimeout], 2500);
            Assert.AreEqual(settings[AutomatorSetting.WaitForIDLETimeout], 3500);
            Assert.AreEqual(settings[AutomatorSetting.WaitForSelectorTimeout], 5000);
            Assert.AreEqual(settings[AutomatorSetting.WaitScrollAcknowledgmentTimeout], 7000);
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