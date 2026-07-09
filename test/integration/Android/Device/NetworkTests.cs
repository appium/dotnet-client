using System;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace Appium.Net.Integration.Tests.Android.Device
{
    internal class NetworkTests
    {
        private AndroidDriver _driver;
        private AppiumOptions _androidOptions;

        [OneTimeSetUp]
        public void SetUp()
        {
            _androidOptions = Caps.GetAndroidUIAutomatorCaps(Apps.Get(Apps.androidApiDemos));
            _driver = new AndroidDriver(
                Env.ServerIsLocal() ? AppiumServers.LocalServiceUri : AppiumServers.RemoteServerUri,
                _androidOptions);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver?.Dispose();
        }

        [Test]
        public void CanToggleGPSTest()
        {
            var gpsEnabled = _driver.ExecuteScript("mobile:isGpsEnabled");
            _driver.ToggleLocationServices();
            var currentGpsEnabled = _driver.ExecuteScript("mobile:isGpsEnabled");
            Assert.That(currentGpsEnabled, Is.Not.EqualTo(gpsEnabled));
            _driver.ToggleLocationServices();
        }

        [Test]
        public void CanMakeGsmCallTest()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.DoesNotThrow((System.Action)(() => _driver.MakeGsmCall("5551234567", GsmCallActions.Call)));
                Assert.DoesNotThrow((System.Action)(() => _driver.MakeGsmCall("5551234567", GsmCallActions.Accept)));
                Assert.DoesNotThrow((System.Action)(() => _driver.MakeGsmCall("5551234567", GsmCallActions.Cancel)));
                Assert.DoesNotThrow((System.Action)(() => _driver.MakeGsmCall("5551234567", GsmCallActions.Hold)));
            }
        }

        [Test]
        public void CanSetGsmSignalStrengthTest()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.DoesNotThrow((System.Action)(() => _driver.SetGsmSignalStrength(GsmSignalStrength.NoneOrUnknown)));
                Assert.DoesNotThrow((System.Action)(() => _driver.SetGsmSignalStrength(GsmSignalStrength.Poor)));
                Assert.DoesNotThrow((System.Action)(() => _driver.SetGsmSignalStrength(GsmSignalStrength.Good)));
                Assert.DoesNotThrow((System.Action)(() => _driver.SetGsmSignalStrength(GsmSignalStrength.Moderate)));
                Assert.DoesNotThrow((System.Action)(() => _driver.SetGsmSignalStrength(GsmSignalStrength.Great)));
            }
        }

        [Test]
        public void CanSetGsmVoiceStateTest()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.DoesNotThrow((System.Action)(() =>
                    _driver.SetGsmVoice(GsmVoiceState.Unregistered)));
                Assert.DoesNotThrow((System.Action)(() =>
                    _driver.SetGsmVoice(GsmVoiceState.Home)));
                Assert.DoesNotThrow((System.Action)(() =>
                    _driver.SetGsmVoice(GsmVoiceState.Roaming)));
                Assert.DoesNotThrow((System.Action)(() =>
                    _driver.SetGsmVoice(GsmVoiceState.Denied)));
                Assert.DoesNotThrow((System.Action)(() =>
                    _driver.SetGsmVoice(GsmVoiceState.Off)));
                Assert.DoesNotThrow((System.Action)(() =>
                    _driver.SetGsmVoice(GsmVoiceState.On)));
            }
        }

        [Test]
        public void CanSendSmsTest()
        {
            Assert.DoesNotThrow((System.Action)(() => _driver.SendSms("5551234567", "Hey lol")));
        }
    }
}
