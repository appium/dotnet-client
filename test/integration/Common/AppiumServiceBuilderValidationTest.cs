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
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Service;

namespace Appium.Net.Integration.Tests.Common
{
    public class AppiumServiceBuilderValidationTest
    {
        [Test]
        public void UsingDriverExecutableThrowsWhenNull()
        {
            var builder = new AppiumServiceBuilder();
            Assert.Throws<ArgumentNullException>((Action)(() => builder.UsingDriverExecutable(null)));
        }

        [Test]
        public void UsingDriverExecutableThrowsWhenFileDoesNotExist()
        {
            var builder = new AppiumServiceBuilder();
            var missingFile = new FileInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".js"));

            Assert.Throws<ArgumentException>((Action)(() => builder.UsingDriverExecutable(missingFile)));
        }

        [Test]
        public void UsingDriverExecutableAcceptsExistingFileAndReturnsSelf()
        {
            var tempFile = new FileInfo(Path.GetTempFileName());
            try
            {
                var builder = new AppiumServiceBuilder();
                var result = builder.UsingDriverExecutable(tempFile);
                Assert.That(result, Is.SameAs(builder));
            }
            finally
            {
                tempFile.Delete();
            }
        }

        [Test]
        public void WithEnvironmentThrowsWhenNull()
        {
            var builder = new AppiumServiceBuilder();
            Assert.Throws<ArgumentNullException>((Action)(() => builder.WithEnvironment(null)));
        }

        [Test]
        public void WithEnvironmentThrowsWhenKeyIsEmpty()
        {
            var builder = new AppiumServiceBuilder();
            var environment = new Dictionary<string, string> { [""] = "value" };

            Assert.Throws<ArgumentNullException>((Action)(() => builder.WithEnvironment(environment)));
        }

        [Test]
        public void WithEnvironmentThrowsWhenValueIsEmpty()
        {
            var builder = new AppiumServiceBuilder();
            var environment = new Dictionary<string, string> { ["KEY"] = "" };

            Assert.Throws<ArgumentNullException>((Action)(() => builder.WithEnvironment(environment)));
        }

        [Test]
        public void WithEnvironmentAcceptsValidDictionaryAndReturnsSelf()
        {
            var builder = new AppiumServiceBuilder();
            var environment = new Dictionary<string, string> { ["KEY"] = "value" };

            var result = builder.WithEnvironment(environment);

            Assert.That(result, Is.SameAs(builder));
        }
    }
}
