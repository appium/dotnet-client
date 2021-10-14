﻿//Licensed under the Apache License, Version 2.0 (the "License");
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

using System.Collections.Generic;

namespace OpenQA.Selenium.Appium.Interfaces
{
    public interface IExecuteMethod
    {
        /// <summary>
        /// Execute a command on the remote server.
        /// </summary>
        /// <param name="commandName">A remote command</param>
        /// <param name="parameters">Parameters to execute</param>
        /// <returns>The result</returns>
        Response Execute(string commandName, Dictionary<string, object> parameters);

        /// <summary>
        /// Execute a command on the remote server.
        /// </summary>
        /// <param name="driverCommand">A remote command</param>
        Response Execute(string driverCommand);
    }
}