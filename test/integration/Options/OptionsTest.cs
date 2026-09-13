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
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;

namespace Appium.Net.Integration.Tests.Options
{
    public class OptionsTest
    {
        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase("simulator-udid", null)]
        [TestCase(null, "/tmp/WebDriverAgentRunner-Runner.app")]
        [TestCase("simulator-udid", "/tmp/WebDriverAgentRunner-Runner.app")]
        [NonParallelizable]
        public void CheckIOSSessionOptions(string udid, string prebuiltWdaPath)
        {
            var originalUdid = Environment.GetEnvironmentVariable("IOS_UDID");
            var originalPrebuiltWdaPath = Environment.GetEnvironmentVariable("LOCAL_PREBUILT_WDA");
            try
            {
                Environment.SetEnvironmentVariable("IOS_UDID", udid);
                Environment.SetEnvironmentVariable("LOCAL_PREBUILT_WDA", prebuiltWdaPath);

                var capabilities = Caps.GetIosCaps("/tmp/Test.app").ToCapabilities();
                using (Assert.EnterMultipleScope())
                {
                    Assert.That(capabilities.GetCapability("appium:wdaLaunchTimeout"), Is.EqualTo(600000));
                    Assert.That(capabilities.GetCapability("appium:wdaConnectionTimeout"), Is.EqualTo(300000));
                    Assert.That(capabilities.GetCapability("appium:udid"),
                        Is.EqualTo(string.IsNullOrEmpty(udid) ? null : udid));
                    Assert.That(capabilities.GetCapability("appium:prebuiltWDAPath"),
                        Is.EqualTo(string.IsNullOrEmpty(prebuiltWdaPath) ? null : prebuiltWdaPath));
                    Assert.That(capabilities.HasCapability("appium:usePreinstalledWDA"),
                        Is.EqualTo(!string.IsNullOrEmpty(prebuiltWdaPath)));
                }
            }
            finally
            {
                Environment.SetEnvironmentVariable("IOS_UDID", originalUdid);
                Environment.SetEnvironmentVariable("LOCAL_PREBUILT_WDA", originalPrebuiltWdaPath);
            }
        }

        [Test]
        public void CheckToDictionaryContainsAppiumOptions()
        {
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalAppiumOption(AndroidMobileCapabilityType.Avd, "Android_Emulator");
            capabilities.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppPackage, Apps.GetId(Apps.androidApiDemos));
            var capsDict = capabilities.ToDictionary();
            Assert.That(capsDict, Is.Not.Empty);
            Assert.That(capsDict, Has.Count.GreaterThan(1));
        }

        [Test]
        public void CheckToDictionaryContainsBothOptions()
        {
            var capabilities = new AppiumOptions
            {
                AutomationName = AutomationName.Appium,
                App = Apps.androidApiDemos,
                PlatformName = MobilePlatform.Android
            };
            capabilities.AddAdditionalAppiumOption(AndroidMobileCapabilityType.Avd, "Android_Emulator");
            capabilities.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppPackage, Apps.GetId(Apps.androidApiDemos));
            var capsDict = capabilities.ToDictionary();
            Assert.That(capsDict, Is.Not.Empty);
            Assert.That(capsDict, Has.Count.EqualTo(5));
        }
    }
}