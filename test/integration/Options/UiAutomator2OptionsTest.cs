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
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace Appium.Net.Integration.Tests.Options
{
    public class UiAutomator2OptionsTest
    {
        [Test]
        public void SetsPlatformAndAutomationName()
        {
            var capabilities = new UiAutomator2Options().ToCapabilities();

            Assert.Multiple(() =>
            {
                Assert.That(capabilities.GetCapability("platformName"), Is.EqualTo(MobilePlatform.Android));
                Assert.That(capabilities.GetCapability("appium:automationName"), Is.EqualTo(AutomationName.AndroidUIAutomator2));
            });
        }

        [Test]
        public void OmitsUnsetProperties()
        {
            var dictionary = new UiAutomator2Options().ToDictionary();

            Assert.That(dictionary.Keys, Is.EquivalentTo(new[] { "platformName", "appium:automationName" }));
        }

        [Test]
        public void SendsTypedPropertiesAsCapabilities()
        {
            var options = new UiAutomator2Options
            {
                DeviceName = "Android Emulator",
                Udid = "emulator-5554",
                NoReset = true,
                FullReset = false,
                NewCommandTimeout = TimeSpan.FromMinutes(2),
                Language = "en",
                Locale = "US",
                AppPackage = "io.appium.android.apis",
                AppActivity = ".ApiDemos",
                AppWaitPackage = "io.appium.android.apis",
                AppWaitActivity = ".ApiDemos",
                AppWaitDuration = TimeSpan.FromSeconds(30),
                Avd = "Pixel_8_API_35",
                AvdLaunchTimeout = TimeSpan.FromMinutes(3),
                AutoGrantPermissions = true,
                SystemPort = 8201,
                DisableWindowAnimation = true,
                ChromedriverExecutable = "/opt/chromedriver"
            };

            var capabilities = options.ToCapabilities();

            Assert.Multiple(() =>
            {
                Assert.That(capabilities.GetCapability("appium:deviceName"), Is.EqualTo("Android Emulator"));
                Assert.That(capabilities.GetCapability("appium:udid"), Is.EqualTo("emulator-5554"));
                Assert.That(capabilities.GetCapability("appium:noReset"), Is.EqualTo(true));
                Assert.That(capabilities.GetCapability("appium:fullReset"), Is.EqualTo(false));
                Assert.That(capabilities.GetCapability("appium:newCommandTimeout"), Is.EqualTo(120L));
                Assert.That(capabilities.GetCapability("appium:language"), Is.EqualTo("en"));
                Assert.That(capabilities.GetCapability("appium:locale"), Is.EqualTo("US"));
                Assert.That(capabilities.GetCapability("appium:appPackage"), Is.EqualTo("io.appium.android.apis"));
                Assert.That(capabilities.GetCapability("appium:appActivity"), Is.EqualTo(".ApiDemos"));
                Assert.That(capabilities.GetCapability("appium:appWaitPackage"), Is.EqualTo("io.appium.android.apis"));
                Assert.That(capabilities.GetCapability("appium:appWaitActivity"), Is.EqualTo(".ApiDemos"));
                Assert.That(capabilities.GetCapability("appium:appWaitDuration"), Is.EqualTo(30000L));
                Assert.That(capabilities.GetCapability("appium:avd"), Is.EqualTo("Pixel_8_API_35"));
                Assert.That(capabilities.GetCapability("appium:avdLaunchTimeout"), Is.EqualTo(180000L));
                Assert.That(capabilities.GetCapability("appium:autoGrantPermissions"), Is.EqualTo(true));
                Assert.That(capabilities.GetCapability("appium:systemPort"), Is.EqualTo(8201));
                Assert.That(capabilities.GetCapability("appium:disableWindowAnimation"), Is.EqualTo(true));
                Assert.That(capabilities.GetCapability("appium:chromedriverExecutable"), Is.EqualTo("/opt/chromedriver"));
            });
        }

        [Test]
        public void AdditionalOptionStillAddsUntypedCapability()
        {
            var options = new UiAutomator2Options();
            options.AddAdditionalAppiumOption("uiautomator2ServerLaunchTimeout", 60000);

            Assert.That(options.ToCapabilities().GetCapability("appium:uiautomator2ServerLaunchTimeout"), Is.EqualTo(60000));
        }

        [TestCase("appPackage")]
        [TestCase("appium:udid")]
        public void AdditionalOptionRejectsTypedCapability(string name)
        {
            var options = new UiAutomator2Options();

            Assert.Throws<ArgumentException>(() => options.AddAdditionalAppiumOption(name, "value"));
        }
    }
}
