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
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android
{
    [TestFixture]
    public class AndroidCommandExecutionHelperTests
    {
        [Test]
        public void OpenNotifications_CallsExecuteScriptWithCorrectScriptAndArgs()
        {
            var fakeExecuteMethod = new FakeExecuteMethod();

            AndroidCommandExecutionHelper.OpenNotifications(fakeExecuteMethod);

            Assert.That(fakeExecuteMethod.Calls, Has.Count.EqualTo(1));
            var call = fakeExecuteMethod.Calls[0];
            Assert.That(call.CommandName, Is.EqualTo(DriverCommand.ExecuteScript));
            Assert.That(call.Parameters, Is.Not.Null);
            Assert.That(call.Parameters.TryGetValue("script", out var scriptValue), Is.True);
            Assert.That(scriptValue, Is.EqualTo("mobile:openNotifications"));
            Assert.That(call.Parameters.TryGetValue("args", out var argsValue), Is.True);
            Assert.That(argsValue, Is.InstanceOf<object[]>());
            Assert.That((object[])argsValue, Is.Empty);
        }
    }
}
