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

using OpenQA.Selenium.Appium.WebSocket;
using System;
using System.Collections.Generic;

namespace OpenQA.Selenium.Appium.Android.Interfaces
{
    /// <summary>
    /// Interface for handling Android logcat message broadcasts via WebSocket.
    /// </summary>
    public interface IListensToLogcatMessages
    {
        /// <summary>
        /// Start logcat messages broadcast via web socket.
        /// This method assumes that Appium server is running on localhost and
        /// is assigned to the default port (4723).
        /// </summary>
        void StartLogcatBroadcast();

        /// <summary>
        /// Start logcat messages broadcast via web socket.
        /// This method assumes that Appium server is assigned to the default port (4723).
        /// </summary>
        /// <param name="host">The name of the host where Appium server is running.</param>
        void StartLogcatBroadcast(string host);

        /// <summary>
        /// Start logcat messages broadcast via web socket.
        /// </summary>
        /// <param name="host">The name of the host where Appium server is running.</param>
        /// <param name="port">The port of the host where Appium server is running.</param>
        void StartLogcatBroadcast(string host, int port);

        /// <summary>
        /// Adds a new log messages broadcasting handler.
        /// Several handlers might be assigned to a single server.
        /// Multiple calls to this method will cause such handler
        /// to be called multiple times.
        /// </summary>
        /// <param name="handler">A function, which accepts a single argument, which is the actual log message.</param>
        void AddLogcatMessagesListener(Action<string> handler);

        /// <summary>
        /// Adds a new log broadcasting errors handler.
        /// Several handlers might be assigned to a single server.
        /// Multiple calls to this method will cause such handler
        /// to be called multiple times.
        /// </summary>
        /// <param name="handler">A function, which accepts a single argument, which is the actual exception instance.</param>
        void AddLogcatErrorsListener(Action<Exception> handler);

        /// <summary>
        /// Adds a new log broadcasting connection handler.
        /// Several handlers might be assigned to a single server.
        /// Multiple calls to this method will cause such handler
        /// to be called multiple times.
        /// </summary>
        /// <param name="handler">A function, which is executed as soon as the client is successfully connected to the web socket.</param>
        void AddLogcatConnectionListener(Action handler);

        /// <summary>
        /// Adds a new log broadcasting disconnection handler.
        /// Several handlers might be assigned to a single server.
        /// Multiple calls to this method will cause such handler
        /// to be called multiple times.
        /// </summary>
        /// <param name="handler">A function, which is executed as soon as the client is successfully disconnected from the web socket.</param>
        void AddLogcatDisconnectionListener(Action handler);

        /// <summary>
        /// Removes all existing logcat handlers.
        /// </summary>
        void RemoveAllLogcatListeners();

        /// <summary>
        /// Stops logcat messages broadcast via web socket.
        /// </summary>
        void StopLogcatBroadcast();
    }
}
