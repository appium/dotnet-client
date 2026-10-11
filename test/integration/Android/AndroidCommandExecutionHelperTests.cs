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

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;

namespace Appium.Net.Integration.Tests.Android
{
    [TestFixture]
    public class AndroidCommandExecutionHelperTests
    {
        private class FakeExecuteMethod : IExecuteMethod
        {
            public string ExecutedCommandName { get; private set; }
            public Dictionary<string, object> ExecutedParameters { get; private set; }
            public int ExecutionCount { get; private set; }
            public Response ResponseToReturn { get; set; } = new Response(null, null, WebDriverResult.Success);

            public Response Execute(string commandName, Dictionary<string, object> parameters)
            {
                ExecutedCommandName = commandName;
                ExecutedParameters = parameters;
                ExecutionCount++;
                return ResponseToReturn;
            }

            public Response Execute(string driverCommand)
            {
                return Execute(driverCommand, null);
            }
        }

        [Test]
        public void OpenNotifications_CallsExecuteScriptWithCorrectScriptAndArgs()
        {
            var fakeExecuteMethod = new FakeExecuteMethod();

            AndroidCommandExecutionHelper.OpenNotifications(fakeExecuteMethod);

            Assert.That(fakeExecuteMethod.ExecutionCount, Is.EqualTo(1));
            Assert.That(fakeExecuteMethod.ExecutedCommandName, Is.EqualTo(DriverCommand.ExecuteScript));
            Assert.That(fakeExecuteMethod.ExecutedParameters, Is.Not.Null);
            Assert.That(fakeExecuteMethod.ExecutedParameters.TryGetValue("script", out var scriptValue), Is.True);
            Assert.That(scriptValue, Is.EqualTo("mobile:openNotifications"));
            Assert.That(fakeExecuteMethod.ExecutedParameters.TryGetValue("args", out var argsValue), Is.True);
            Assert.That(argsValue, Is.InstanceOf<object[]>());
            Assert.That((object[])argsValue, Is.Empty);
        }
    }
}
