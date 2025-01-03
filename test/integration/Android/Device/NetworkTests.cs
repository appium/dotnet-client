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
        public void CanToggleDataTest()
        {
            ConnectionType beforeToggle = _driver.ConnectionType;
            _driver.ToggleData();
            // Toggle data connection and get the new connection type
            ConnectionType afterFirstToggle = _driver.ConnectionType;
            Assert.That(beforeToggle, Is.Not.EqualTo(afterFirstToggle));
            _driver.ToggleData();
            // afterSecondToggle stores the connection type after the second toggle to verify it matches the initial state
            ConnectionType afterSecondToggle = _driver.ConnectionType;
            Assert.That(afterSecondToggle, Is.EqualTo(beforeToggle));
        }

        [Test]
        public void CanToggleAirplaneModeTest()
        {
            _driver.ToggleAirplaneMode();

            var currentConnectionType = _driver.ConnectionType;
            Assert.That(currentConnectionType, Is.EqualTo(ConnectionType.AirplaneMode));
            _driver.ToggleAirplaneMode();
        }

        [Test]
        public void CanToggleWifiTest()
        {
            var beforeToggleConnectionType = _driver.ConnectionType;
            _driver.ToggleWifi();

            var currentConnectionType = _driver.ConnectionType;
            Assert.That(currentConnectionType, Is.Not.EqualTo(beforeToggleConnectionType));
            _driver.ToggleWifi();
        }

        [Test]
        public void CanMakeGsmCallTest()
        {
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => _driver.MakeGsmCall("5551234567", GsmCallActions.Call));
                Assert.DoesNotThrow(() => _driver.MakeGsmCall("5551234567", GsmCallActions.Accept));
                Assert.DoesNotThrow(() => _driver.MakeGsmCall("5551234567", GsmCallActions.Cancel));
                Assert.DoesNotThrow(() => _driver.MakeGsmCall("5551234567", GsmCallActions.Hold));
            });
        }

        [Test]
        public void CanSetGsmSignalStrengthTest()
        {
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => _driver.SetGsmSignalStrength(GsmSignalStrength.NoneOrUnknown));
                Assert.DoesNotThrow(() => _driver.SetGsmSignalStrength(GsmSignalStrength.Poor));
                Assert.DoesNotThrow(() => _driver.SetGsmSignalStrength(GsmSignalStrength.Good));
                Assert.DoesNotThrow(() => _driver.SetGsmSignalStrength(GsmSignalStrength.Moderate));
                Assert.DoesNotThrow(() => _driver.SetGsmSignalStrength(GsmSignalStrength.Great));
            });
        }

        [Test]
        public void CanSetGsmVoiceStateTest()
        {
            Assert.Multiple(() =>
                {
                    Assert.DoesNotThrow(() =>
                        _driver.SetGsmVoice(GsmVoiceState.Unregistered));
                    Assert.DoesNotThrow(() =>
                        _driver.SetGsmVoice(GsmVoiceState.Home));
                    Assert.DoesNotThrow(() =>
                        _driver.SetGsmVoice(GsmVoiceState.Roaming));
                    Assert.DoesNotThrow(() =>
                        _driver.SetGsmVoice(GsmVoiceState.Denied));
                    Assert.DoesNotThrow(() =>
                        _driver.SetGsmVoice(GsmVoiceState.Off));
                    Assert.DoesNotThrow(() =>
                        _driver.SetGsmVoice(GsmVoiceState.On));
                }
            );
        }

        [Test]
        public void CanSendSmsTest()
        {
            Assert.DoesNotThrow(() => _driver.SendSms("5551234567", "Hey lol"));
        }
    }
}
