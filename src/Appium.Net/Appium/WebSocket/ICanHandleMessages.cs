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

namespace OpenQA.Selenium.Appium.WebSocket
{
    /// <summary>
    /// Interface for handling WebSocket messages.
    /// </summary>
    /// <typeparam name="T">The type of message to handle.</typeparam>
    public interface ICanHandleMessages<T>
    {
        /// <summary>
        /// Gets the list of web socket message handlers.
        /// </summary>
        List<Action<T>> MessageHandlers { get; }

        /// <summary>
        /// Register a new message handler.
        /// </summary>
        /// <param name="handler">A callback function, which accepts the received message as a parameter.</param>
        void AddMessageHandler(Action<T> handler);

        /// <summary>
        /// Removes existing message handlers.
        /// </summary>
        void RemoveMessageHandlers();
    }
}
