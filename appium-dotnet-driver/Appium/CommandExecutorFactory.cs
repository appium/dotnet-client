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
using OpenQA.Selenium.Remote;
using System;
using System.Reflection;

namespace OpenQA.Selenium.Appium
{
    internal static class CommandExecutorFactory
    {
        /// <summary>
        /// Creates an HttpCommandExecutor (an internal class from WebDriver assembly) if able to through reflection 
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4444/wd/hub).</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        /// <returns>HttpCommandExecutor object if possible, otherwise null</returns>
        public static ICommandExecutor GetHttpCommandExecutor(Uri remoteAddress, TimeSpan commandTimeout)
        {
            var seleniumAssembly = Assembly.Load("WebDriver");
            var commandType = seleniumAssembly.GetType("OpenQA.Selenium.Remote.HttpCommandExecutor");
            ICommandExecutor commandExecutor = null;

            if (null != commandType)
            {
                commandExecutor = Activator.CreateInstance(commandType, new object[] { remoteAddress, commandTimeout }) as ICommandExecutor;
            }

            return commandExecutor;
        }
    }
}
