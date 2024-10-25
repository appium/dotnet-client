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


using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;

namespace Appium.Net.Integration.Tests.Options
{
    public class OptionsTest
    {

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