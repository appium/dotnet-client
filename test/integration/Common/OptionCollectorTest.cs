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
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service.Options;

namespace Appium.Net.Integration.Tests.Common
{
    public class OptionCollectorTest
    {
        private static string GetCapabilitiesArgument(OptionCollector collector)
        {
            var argumentsProperty = typeof(OptionCollector).GetProperty("Arguments", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.That(argumentsProperty, Is.Not.Null, "Arguments property not found.");

            var arguments = argumentsProperty.GetValue(collector) as IList<string>;
            Assert.That(arguments, Is.Not.Null, "Arguments should not be null.");

            var capabilitiesIndex = arguments.IndexOf("--default-capabilities");
            Assert.That(capabilitiesIndex, Is.GreaterThanOrEqualTo(0), "Capabilities flag not found.");
            Assert.That(arguments.Count, Is.GreaterThan(capabilitiesIndex + 1), "Capabilities value missing.");

            return arguments[capabilitiesIndex + 1];
        }

        [Test]
        public void AddCapabilities_DoesNotModifyCallerOptions()
        {
            var first = new AppiumOptions();
            first.AddAdditionalAppiumOption("foo", "1");
            var second = new AppiumOptions();
            second.AddAdditionalAppiumOption("bar", "2");

            var collector = new OptionCollector()
                .AddCapabilities(first)
                .AddCapabilities(second);

            Assert.That(first.ToDictionary().Keys, Is.EquivalentTo(new[] { "appium:foo" }));
            Assert.That(second.ToDictionary().Keys, Is.EquivalentTo(new[] { "appium:bar" }));

            first.AddAdditionalAppiumOption("late", "3");

            var capabilitiesArgument = GetCapabilitiesArgument(collector);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(capabilitiesArgument, Does.Contain("appium:foo"));
                Assert.That(capabilitiesArgument, Does.Contain("appium:bar"));
                Assert.That(capabilitiesArgument, Does.Not.Contain("appium:late"));
            }
        }

        [Test]
        public void AddCapabilities_MergesTypedAppiumProperties()
        {
            var first = new AppiumOptions();
            first.AddAdditionalAppiumOption("foo", "1");
            var second = new AppiumOptions { DeviceName = "emu" };

            var collector = new OptionCollector().AddCapabilities(first);
            Assert.DoesNotThrow(() => collector.AddCapabilities(second));

            var capabilitiesArgument = GetCapabilitiesArgument(collector);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(capabilitiesArgument, Does.Contain("appium:foo"));
                Assert.That(capabilitiesArgument, Does.Contain("appium:deviceName"));
            }
        }

        [Test]
        public void AddCapabilities_KeepsW3CCapabilityNamesWhenMerging()
        {
            var first = new AppiumOptions();
            first.AddAdditionalAppiumOption("foo", "1");
            var second = new AppiumOptions { PlatformName = "Android" };

            var collector = new OptionCollector()
                .AddCapabilities(first)
                .AddCapabilities(second);

            var capabilitiesArgument = GetCapabilitiesArgument(collector);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(capabilitiesArgument, Does.Contain("platformName"));
                Assert.That(capabilitiesArgument, Does.Not.Contain("appium:platformName"));
            }
        }

        [Test]
        public void AddCapabilities_LaterValueWinsForTheSameCapability()
        {
            var first = new AppiumOptions();
            first.AddAdditionalAppiumOption("foo", "first");
            var second = new AppiumOptions();
            second.AddAdditionalAppiumOption("foo", "second");

            var collector = new OptionCollector()
                .AddCapabilities(first)
                .AddCapabilities(second);

            var capabilitiesArgument = GetCapabilitiesArgument(collector);
            using (Assert.EnterMultipleScope())
            {
                Assert.That(capabilitiesArgument.Split(new[] { "appium:foo" }, StringSplitOptions.None).Length - 1, Is.EqualTo(1));
                Assert.That(capabilitiesArgument, Does.Contain("second"));
                Assert.That(capabilitiesArgument, Does.Not.Contain("first"));
            }
        }

        [Test]
        public void AddCapabilities_IgnoresNullOptions()
        {
            var options = new AppiumOptions();
            options.AddAdditionalAppiumOption("foo", "1");

            var collector = new OptionCollector();
            Assert.DoesNotThrow(() => collector.AddCapabilities(null).AddCapabilities(options).AddCapabilities(null));

            Assert.That(GetCapabilitiesArgument(collector), Does.Contain("appium:foo"));
        }
    }
}
