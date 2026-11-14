//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//See the NOTICE file distributed with this work for additional
//information regarding copyright ownership.
//You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Net.Integration.Tests.Options
{
    public class XCUITestOptionsTest
    {
        [Test]
        public void SetsPlatformAndAutomationName()
        {
            var capabilities = new XCUITestOptions().ToCapabilities();

            Assert.Multiple(() =>
            {
                Assert.That(capabilities.GetCapability("platformName"), Is.EqualTo(MobilePlatform.IOS));
                Assert.That(capabilities.GetCapability("appium:automationName"), Is.EqualTo(AutomationName.iOSXcuiTest));
            });
        }

        [Test]
        public void OmitsUnsetProperties()
        {
            var dictionary = new XCUITestOptions().ToDictionary();

            Assert.That(dictionary.Keys, Is.EquivalentTo(new[] { "platformName", "appium:automationName" }));
        }

        [Test]
        public void SendsTypedPropertiesAsCapabilities()
        {
            var options = new XCUITestOptions
            {
                DeviceName = "iPhone 16",
                PlatformVersion = "18.0",
                Udid = "00008110-000A1B2C3D4E5F6G",
                NoReset = true,
                FullReset = false,
                NewCommandTimeout = TimeSpan.FromSeconds(90),
                Language = "fr",
                Locale = "fr_CA",
                BundleId = "io.appium.TestApp",
                AutoAcceptAlerts = true,
                AutoDismissAlerts = false,
                WdaLocalPort = 8101,
                WdaLaunchTimeout = TimeSpan.FromMinutes(2),
                UseNewWda = false,
                UsePrebuiltWda = true,
                UpdatedWdaBundleId = "com.example.WebDriverAgentRunner",
                XcodeOrgId = "ABCDE12345",
                XcodeSigningId = "Apple Development",
                ShowXcodeLog = true,
                SimulatorStartupTimeout = TimeSpan.FromMinutes(4)
            };

            var capabilities = options.ToCapabilities();

            Assert.Multiple(() =>
            {
                Assert.That(capabilities.GetCapability("appium:deviceName"), Is.EqualTo("iPhone 16"));
                Assert.That(capabilities.GetCapability("appium:platformVersion"), Is.EqualTo("18.0"));
                Assert.That(capabilities.GetCapability("appium:udid"), Is.EqualTo("00008110-000A1B2C3D4E5F6G"));
                Assert.That(capabilities.GetCapability("appium:noReset"), Is.EqualTo(true));
                Assert.That(capabilities.GetCapability("appium:fullReset"), Is.EqualTo(false));
                Assert.That(capabilities.GetCapability("appium:newCommandTimeout"), Is.EqualTo(90L));
                Assert.That(capabilities.GetCapability("appium:language"), Is.EqualTo("fr"));
                Assert.That(capabilities.GetCapability("appium:locale"), Is.EqualTo("fr_CA"));
                Assert.That(capabilities.GetCapability("appium:bundleId"), Is.EqualTo("io.appium.TestApp"));
                Assert.That(capabilities.GetCapability("appium:autoAcceptAlerts"), Is.EqualTo(true));
                Assert.That(capabilities.GetCapability("appium:autoDismissAlerts"), Is.EqualTo(false));
                Assert.That(capabilities.GetCapability("appium:wdaLocalPort"), Is.EqualTo(8101));
                Assert.That(capabilities.GetCapability("appium:wdaLaunchTimeout"), Is.EqualTo(120000L));
                Assert.That(capabilities.GetCapability("appium:useNewWDA"), Is.EqualTo(false));
                Assert.That(capabilities.GetCapability("appium:usePrebuiltWDA"), Is.EqualTo(true));
                Assert.That(capabilities.GetCapability("appium:updatedWDABundleId"), Is.EqualTo("com.example.WebDriverAgentRunner"));
                Assert.That(capabilities.GetCapability("appium:xcodeOrgId"), Is.EqualTo("ABCDE12345"));
                Assert.That(capabilities.GetCapability("appium:xcodeSigningId"), Is.EqualTo("Apple Development"));
                Assert.That(capabilities.GetCapability("appium:showXcodeLog"), Is.EqualTo(true));
                Assert.That(capabilities.GetCapability("appium:simulatorStartupTimeout"), Is.EqualTo(240000L));
            });
        }

        [Test]
        public void AdditionalOptionStillAddsUntypedCapability()
        {
            var options = new XCUITestOptions();
            options.AddAdditionalAppiumOption("connectHardwareKeyboard", true);

            Assert.That(options.ToCapabilities().GetCapability("appium:connectHardwareKeyboard"), Is.EqualTo(true));
        }

        [TestCase("bundleId")]
        [TestCase("appium:wdaLocalPort")]
        public void AdditionalOptionRejectsTypedCapability(string name)
        {
            var options = new XCUITestOptions();

            Assert.Throws<ArgumentException>(() => options.AddAdditionalAppiumOption(name, "value"));
        }
    }
}
