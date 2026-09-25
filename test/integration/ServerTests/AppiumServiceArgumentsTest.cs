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
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;

namespace Appium.Net.Integration.Tests.ServerTests
{
    /// <summary>
    /// Checks how the Appium server command line is built. None of these tests need a running Appium server.
    /// </summary>
    [TestFixture]
    public class AppiumServiceArgumentsTest
    {
        private FileInfo _dummyAppiumJs;

        [SetUp]
        public void SetUp()
        {
            _dummyAppiumJs = new FileInfo(Path.GetTempFileName());
        }

        [TearDown]
        public void TearDown()
        {
            if (_dummyAppiumJs.Exists)
            {
                _dummyAppiumJs.Delete();
            }
        }

        private static string EscapeArgument(string arg) =>
            (string)GetStaticMethod(typeof(AppiumLocalService), "EscapeArgument").Invoke(null, new object[] { arg });

        private static string BuildCommandLine(IEnumerable<string> rawArguments, IEnumerable<string> arguments) =>
            (string)GetStaticMethod(typeof(AppiumLocalService), "BuildCommandLine").Invoke(null, new object[] { rawArguments, arguments });

        private static MethodInfo GetStaticMethod(Type type, string name)
        {
            var method = type.GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic);
            Assert.That(method, Is.Not.Null, $"{name} method not found. Check for signature or name changes.");
            return method;
        }

        private static IReadOnlyList<string> BuildArguments(AppiumServiceBuilder builder)
        {
            var method = typeof(AppiumServiceBuilder).GetMethod("BuildArguments", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.That(method, Is.Not.Null, "BuildArguments method not found. Check for signature or name changes.");
            return (IReadOnlyList<string>)method.Invoke(builder, null);
        }

        private static string GetCommandLine(AppiumLocalService service)
        {
            var property = typeof(AppiumLocalService).GetProperty("Arguments", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.That(property, Is.Not.Null, "Arguments property not found. Check for signature or name changes.");
            return (string)property.GetValue(service);
        }

        [TestCase("4723", "4723")]
        [TestCase("", "\"\"")]
        [TestCase("C:\\Program Files\\Appium\\log file.txt", "\"C:\\Program Files\\Appium\\log file.txt\"")]
        [TestCase("{\"a\":\"b\"}", "\"{\\\"a\\\":\\\"b\\\"}\"")]
        [TestCase("dir with space\\", "\"dir with space\\\\\"")]
        [TestCase("a\\\"b", "\"a\\\\\\\"b\"")]
        public void EscapeArgument_QuotesOnlyWhenNeeded(string input, string expected)
        {
            Assert.That(EscapeArgument(input), Is.EqualTo(expected));
        }

        [Test]
        public void BuildArguments_ReturnsUnquotedServerArgumentsWithoutNodeOptions()
        {
            var logFile = new FileInfo(Path.Combine(Path.GetTempPath(), "appium dir", "log file.txt"));
            var builder = new AppiumServiceBuilder()
                .WithNodeArguments("--max-old-space-size=4096")
                .WithAppiumJS(_dummyAppiumJs)
                .WithIPAddress("127.0.0.1")
                .UsingPort(4723)
                .WithLogFile(logFile);

            var args = BuildArguments(builder);

            Assert.That(args, Is.EqualTo(new[]
            {
                _dummyAppiumJs.FullName, "--port", "4723", "--address", "127.0.0.1", "--log", logFile.FullName
            }));
        }

        [Test]
        public void Build_PassesNodeArgumentsVerbatim()
        {
            // WithNodeArguments documents that callers escape the values themselves,
            // so a pre-quoted value must reach the command line unchanged.
            const string preQuoted = "--require=\"C:\\Program Files\\hook.js\"";
            var service = new AppiumServiceBuilder()
                .WithNodeArguments("--max-old-space-size=4096", preQuoted)
                .WithAppiumJS(_dummyAppiumJs)
                .WithIPAddress("127.0.0.1")
                .UsingPort(4723)
                .Build();

            var commandLine = GetCommandLine(service);

            Assert.That(commandLine, Does.StartWith($"--max-old-space-size=4096 {preQuoted} "));
        }

        [Test]
        public void Build_EscapesServerArgumentsInCommandLine()
        {
            var collector = new OptionCollector()
                .AddArguments(new KeyValuePair<string, string>("--base-path", "/wd/hub\" --allow-insecure \"*"));
            var service = new AppiumServiceBuilder()
                .WithAppiumJS(_dummyAppiumJs)
                .WithIPAddress("127.0.0.1")
                .UsingPort(4723)
                .WithArguments(collector)
                .Build();

            var commandLine = GetCommandLine(service);

            Assert.That(commandLine, Does.EndWith("--base-path \"/wd/hub\\\" --allow-insecure \\\"*\""));
        }

        [Test]
        public void BuildCommandLine_DeliversEachServerArgumentToProcessIntact()
        {
            var arguments = new[]
            {
                "plain",
                "",
                "C:\\Program Files\\Appium\\log file.txt",
                "trailing backslash\\",
                "{\"platformName\":\"Android\",\"appium:app\":\"C:/apps/my app.apk\"}",
                "x\" --injected \"y",
                "a & calc.exe | echo pwned"
            };
            // The script has no spaces or quotes, so it needs no escaping as a raw Node.js argument.
            var rawNodeArguments = new[] { "-e", "process.stdout.write(JSON.stringify(process.argv.slice(1)))" };

            var startInfo = new ProcessStartInfo
            {
                FileName = "node",
                Arguments = BuildCommandLine(rawNodeArguments, arguments),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            string output;
            try
            {
                using var process = Process.Start(startInfo);
                output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }
            catch (Win32Exception)
            {
                Assert.Ignore("Node.js is not available on PATH.");
                return;
            }

            var received = JsonSerializer.Deserialize<string[]>(output);
            Assert.That(received, Is.EqualTo(arguments));
        }

        [Test]
        public void OptionCollector_SerializesCapabilitiesAsUnquotedJson()
        {
            var options = new AppiumOptions
            {
                App = "C:\\test\\app.apk"
            };
            options.AddAdditionalAppiumOption("platformName", "Android");

            var collector = new OptionCollector().AddCapabilities(options);

            var argsProperty = typeof(OptionCollector).GetProperty("Arguments", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.That(argsProperty, Is.Not.Null, "Arguments property not found. Check for signature or name changes.");
            var args = (IList<string>)argsProperty.GetValue(collector);

            int capsIndex = args.IndexOf("--default-capabilities");
            Assert.That(capsIndex, Is.GreaterThanOrEqualTo(0));
            Assert.That(args.Count, Is.GreaterThan(capsIndex + 1));

            using var caps = JsonDocument.Parse(args[capsIndex + 1]);
            Assert.That(caps.RootElement.GetProperty("appium:platformName").GetString(), Is.EqualTo("Android"));
        }
    }
}
