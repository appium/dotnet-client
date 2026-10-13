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

using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Interfaces;
using System.Collections.Generic;

namespace Appium.Net.Integration.Tests.helpers
{
    /// <summary>
    /// An <see cref="IExecuteMethod"/> that records every command it receives instead of sending it
    /// to a server, so command helpers can be tested without a device.
    /// </summary>
    internal class FakeExecuteMethod : IExecuteMethod
    {
        private readonly List<ExecutedCommand> _calls = new List<ExecutedCommand>();

        /// <summary>
        /// Gets the commands executed so far, in the order they were received.
        /// </summary>
        public IReadOnlyList<ExecutedCommand> Calls => _calls;

        /// <summary>
        /// Gets or sets the response returned for every command.
        /// </summary>
        public Response ResponseToReturn { get; set; } = new Response(null, null, WebDriverResult.Success);

        public Response Execute(string commandName, Dictionary<string, object> parameters)
        {
            _calls.Add(new ExecutedCommand(commandName, parameters));
            return ResponseToReturn;
        }

        public Response Execute(string driverCommand)
        {
            return Execute(driverCommand, null);
        }

        /// <summary>
        /// A single command received by <see cref="FakeExecuteMethod"/>.
        /// </summary>
        internal class ExecutedCommand
        {
            public ExecutedCommand(string commandName, Dictionary<string, object> parameters)
            {
                CommandName = commandName;
                Parameters = parameters;
            }

            public string CommandName { get; }

            public Dictionary<string, object> Parameters { get; }
        }
    }
}
