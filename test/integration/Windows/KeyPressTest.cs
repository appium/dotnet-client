using System;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Android.Enums;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium.Windows.Enums;
using OpenQA.Selenium.Remote;

namespace Appium.Net.Integration.Tests.Windows
{
    internal class KeyPressTest
    {
        private WindowsDriver<WindowsElement> _calculatorSession;
        protected static RemoteWebElement CalculatorResult;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            appCapabilities.AddAdditionalCapability("platformName", "Windows");

            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _calculatorSession = new WindowsDriver<WindowsElement>(serverUri, appCapabilities,
                Env.InitTimeoutSec);
            _calculatorSession.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            CalculatorResult = null;
            _calculatorSession.CloseApp();
            _calculatorSession.Dispose();
            _calculatorSession = null;
        }
        [Test]
        public void PressKeyCodeKeyEventTest()
        {
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => _calculatorSession.PressKeyCode(new KeyEvent().WithKeyCode(WindowsKeyCode.Windows)
                    .WithMetaState(AndroidKeyMetastate.Meta_Shift_On).WithFlag(AndroidKeyCode.FlagSoftKeyboard)));

                Assert.Throws<InvalidOperationException>(() =>
                    _calculatorSession.PressKeyCode(new KeyEvent().WithFlag(WindowsKeyCode.Count)));
                Assert.Throws<InvalidOperationException>(() =>
                    _calculatorSession.PressKeyCode(new KeyEvent().WithFlag(WindowsKeyCode.Power)
                        .WithMetaState(AndroidKeyCode.MetaAlt_ON)));
            });
        }

        [Test]
        public void LongPressKeyCodeKeyEventTest()
        {
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => _calculatorSession.LongPressKeyCode(new KeyEvent().WithKeyCode(WindowsKeyCode.Windows)
                    .WithMetaState(AndroidKeyMetastate.Meta_Shift_On).WithFlag(WindowsKeyCode.Back)));
                Assert.Throws<InvalidOperationException>(() =>
                    _calculatorSession.LongPressKeyCode(new KeyEvent().WithFlag(WindowsKeyCode.Count)));
                Assert.Throws<InvalidOperationException>(() =>
                    _calculatorSession.LongPressKeyCode(new KeyEvent().WithFlag(WindowsKeyCode.Power)
                        .WithMetaState(AndroidKeyCode.MetaAlt_ON)));
            });
        }
    }
}
