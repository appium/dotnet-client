using System.Collections.Generic;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Android.Enums;

namespace Appium.Net.Integration.Tests.Android
{
    public class SettingTest
    {
        private AndroidDriver _driver;
        private readonly string _appName = "androidApiDemos";

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get(_appName))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get(_appName));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
            _driver.TerminateApp(Apps.GetId(_appName));
        }

        [Test]
        public void IgnoreUnimportantViewsTest()
        {
            _driver.IgnoreUnimportantViews(true);
            var ignoreViews =
                (bool) _driver.Settings[AutomatorSetting.IgnoreUnimportantViews];
            Assert.That(ignoreViews, Is.True);
            _driver.IgnoreUnimportantViews(false);
            ignoreViews = (bool) _driver.Settings[AutomatorSetting.IgnoreUnimportantViews];
            Assert.That(ignoreViews, Is.False);
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
            Assert.Multiple(() =>
            {
                Assert.That(settings[AutomatorSetting.KeyInjectionDelay], Is.EqualTo(400));
                Assert.That(settings[AutomatorSetting.WaitActionAcknowledgmentTimeout], Is.EqualTo(500));
                Assert.That(settings[AutomatorSetting.WaitForIDLETimeout], Is.EqualTo(600));
                Assert.That(settings[AutomatorSetting.WaitForSelectorTimeout], Is.EqualTo(1000));
                Assert.That(settings[AutomatorSetting.WaitScrollAcknowledgmentTimeout], Is.EqualTo(300));
            });
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
            Assert.Multiple(() =>
            {
                Assert.That(settings[AutomatorSetting.KeyInjectionDelay], Is.EqualTo(1500));
                Assert.That(settings[AutomatorSetting.WaitActionAcknowledgmentTimeout], Is.EqualTo(2500));
                Assert.That(settings[AutomatorSetting.WaitForIDLETimeout], Is.EqualTo(3500));
                Assert.That(settings[AutomatorSetting.WaitForSelectorTimeout], Is.EqualTo(5000));
                Assert.That(settings[AutomatorSetting.WaitScrollAcknowledgmentTimeout], Is.EqualTo(7000));
            });
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