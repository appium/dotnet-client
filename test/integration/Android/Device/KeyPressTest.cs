using System;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Android.Enums;

namespace Appium.Net.Integration.Tests.Android.Device.Keys
{
    class KeyPressTest
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
        }

        [SetUp]
        public void SetUp()
        {
            string appId = Apps.GetId(Apps.androidApiDemos);
            _driver.TerminateApp(appId);
            _driver.ActivateApp(appId);
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

        [Test]
        public void PressKeyCodeTest()
        {
            _driver.PressKeyCode(AndroidKeyCode.Home);
        }

        [Test]
        public void PressKeyCodeWithMetastateTest()
        {
            _driver.PressKeyCode(AndroidKeyCode.Space, AndroidKeyMetastate.Meta_Shift_On);
        }

        [Test]
        public void LongPressKeyCodeTest()
        {
            _driver.LongPressKeyCode(AndroidKeyCode.Home);
        }

        [Test]
        public void LongPressKeyCodeWithMetastateTest()
        {
            _driver.LongPressKeyCode(AndroidKeyCode.Space, AndroidKeyMetastate.Meta_Shift_On);
        }

        [Test]
        public void PressKeyCodeKeyEventTest()
        {
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => _driver.PressKeyCode(new KeyEvent().WithKeyCode(AndroidKeyCode.Space)
                    .WithMetaKeyModifier(AndroidKeyMetastate.Meta_Shift_On).WithFlag(AndroidKeyCode.FlagSoftKeyboard)));
                Assert.Throws<InvalidOperationException>(() =>
                    _driver.PressKeyCode(new KeyEvent().WithFlag(AndroidKeyCode.Home)));
                Assert.Throws<InvalidOperationException>(() =>
                    _driver.PressKeyCode(new KeyEvent().WithFlag(AndroidKeyCode.Home)
                        .WithMetaKeyModifier(AndroidKeyCode.MetaAlt_ON)));
            });
        }

        [Test]
        public void LongPressKeyCodeKeyEventTest()
        {
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => _driver.LongPressKeyCode(new KeyEvent().WithKeyCode(AndroidKeyCode.Space)
                    .WithMetaKeyModifier(AndroidKeyMetastate.Meta_Shift_On).WithFlag(AndroidKeyCode.FlagLongPress)));
                Assert.Throws<InvalidOperationException>(() =>
                    _driver.LongPressKeyCode(new KeyEvent().WithFlag(AndroidKeyCode.Home)));
                Assert.Throws<InvalidOperationException>(() =>
                    _driver.LongPressKeyCode(new KeyEvent().WithFlag(AndroidKeyCode.Home)
                        .WithMetaKeyModifier(AndroidKeyCode.MetaAlt_ON)));
            });
        }
    }
}
