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
using OpenQA.Selenium.Appium.Enums;

namespace OpenQA.Selenium.Appium.Interfaces
{
    public interface IInteractsWithApps : IExecuteMethod
    {
        /// <summary>
        /// Installs an App.
        /// </summary>
        /// <param name="appPath">a string containing the file path or url of the app.</param>
        void InstallApp(string appPath);

        /// <summary>
        /// Checks If an App Is Installed.
        /// </summary>
        /// <param name="appPath">a string containing the bundle id.</param>
        /// <return>a boolean indicating if the app is installed.</return>
        bool IsAppInstalled(string bundleId);

        /// <summary>
        /// Deactivates app completely (as "Home" button does).  
        /// </summary>
        void BackgroundApp();

        /// <summary>
        /// Backgrounds the current app for the given number of seconds or deactivates app completely if negative number is given. 
        /// </summary>
        /// <param name="timepSpan">the timespan of running the app in the background.</param>
        void BackgroundApp(TimeSpan timepSpan);

        /// <summary>
        /// Removes an App.
        /// </summary>
        /// <param name="appPath">a string containing the id of the app.</param>
        void RemoveApp(string appId);

        /// <summary>
        /// Activates the given app by moving to the foreground if it is running in the background or starting it if it is not running yet.
        /// </summary>
        /// <param name="appPath">a string containing the id of the app.</param>
        void ActivateApp(string appId);

        /// <summary>
        /// Terminates an App.
        /// </summary>
        /// <param name="appId">a string containing the id of the app.</param>
        /// <return>a boolean indicating if the app was terminated.</return>
        bool TerminateApp(string appId);

        /// <summary>
        /// Terminates an App.
        /// </summary>
        /// <param name="appId">a string containing the id of the app.</param>
        /// <param name="timeout">a TimeSpan for how long to wait until the application is terminated.</param>
        /// <return>a boolean indicating if the app was terminated in the given timeout.</return>
        bool TerminateApp(string appId, TimeSpan timeout);

        /// <summary>
        /// Gets the State of the app.
        /// </summary>
        /// <param name="appId">a string containing the id of the app.</param>
        /// <returns>an enumeration of the app state.</returns>
        AppState GetAppState(string appId);
    }
}