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
        private AppiumDriver _driver;
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
            if (_driver != null)
            {
                _driver.Dispose();
            }            
        }

        [Test]
        public void CanToggleDataTest()
        {
            var androidDriver = (AndroidDriver) _driver;

            androidDriver.ToggleData();
            ConnectionType currentConnectionType = androidDriver.ConnectionType;
            Assert.That(currentConnectionType, Is.EqualTo(ConnectionType.WifiOnly));
            androidDriver.ToggleData();
            currentConnectionType = androidDriver.ConnectionType;
            Assert.That(currentConnectionType, Is.EqualTo(ConnectionType.AllNetworkOn));
        }

        [Test]
        public void CanToggleAirplaneModeTest()
        {
            var androidDriver = (AndroidDriver) _driver;

            androidDriver.ToggleAirplaneMode();

            var currentConnectionType = androidDriver.ConnectionType;
            Assert.That(currentConnectionType, Is.EqualTo(ConnectionType.AirplaneMode));

            androidDriver.ToggleAirplaneMode();
        }

        [Test]
        public void CanToggleWifiTest()
        {
            var androidDriver = (AndroidDriver) _driver;
            var beforeToggleConnectionType = androidDriver.ConnectionType;
            androidDriver.ToggleWifi();

            var currentConnectionType = androidDriver.ConnectionType;
            Assert.That(currentConnectionType, Is.Not.EqualTo(beforeToggleConnectionType));

            androidDriver.ToggleWifi();
        }

        [Test]
        public void CanMakeGsmCallTest()
        {
            var androidDriver = (AndroidDriver) _driver;

            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => androidDriver.MakeGsmCall("5551234567", GsmCallActions.Call));
                Assert.DoesNotThrow(() => androidDriver.MakeGsmCall("5551234567", GsmCallActions.Accept));
                Assert.DoesNotThrow(() => androidDriver.MakeGsmCall("5551234567", GsmCallActions.Cancel));
                Assert.DoesNotThrow(() => androidDriver.MakeGsmCall("5551234567", GsmCallActions.Hold));
            });
        }

        [Test]
        public void CanSetGsmSignalStrengthTest()
        {
            var androidDriver = (AndroidDriver) _driver;

            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => androidDriver.SetGsmSignalStrength(GsmSignalStrength.NoneOrUnknown));
                Assert.DoesNotThrow(() => androidDriver.SetGsmSignalStrength(GsmSignalStrength.Poor));
                Assert.DoesNotThrow(() => androidDriver.SetGsmSignalStrength(GsmSignalStrength.Good));
                Assert.DoesNotThrow(() => androidDriver.SetGsmSignalStrength(GsmSignalStrength.Moderate));
                Assert.DoesNotThrow(() => androidDriver.SetGsmSignalStrength(GsmSignalStrength.Great));
            });
        }

        [Test]
        public void CanSetGsmVoiceStateTest()
        {
            var androidDriver = (AndroidDriver) _driver;

            Assert.Multiple(() =>
                {
                    Assert.DoesNotThrow(() =>
                        androidDriver.SetGsmVoice(GsmVoiceState.Unregistered));
                    Assert.DoesNotThrow(() =>
                        androidDriver.SetGsmVoice(GsmVoiceState.Home));
                    Assert.DoesNotThrow(() =>
                        androidDriver.SetGsmVoice(GsmVoiceState.Roaming));
                    Assert.DoesNotThrow(() =>
                        androidDriver.SetGsmVoice(GsmVoiceState.Denied));
                    Assert.DoesNotThrow(() =>
                        androidDriver.SetGsmVoice(GsmVoiceState.Off));
                    Assert.DoesNotThrow(() =>
                        androidDriver.SetGsmVoice(GsmVoiceState.On));
                }
            );
        }

        [Test]
        public void CanSendSmsTest()
        {
            var androidDriver = (AndroidDriver) _driver;

            Assert.DoesNotThrow(() => androidDriver.SendSms("5551234567", "Hey lol"));
        }
    }
}
