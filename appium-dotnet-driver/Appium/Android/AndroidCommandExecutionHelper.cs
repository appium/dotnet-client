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

using OpenQA.Selenium.Appium.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;

namespace OpenQA.Selenium.Appium.Android
{
    public sealed class AndroidCommandExecutionHelper : AppiumCommandExecutionHelper
    {
        public static void StartActivity(IExecuteMethod executeMethod, string appPackage, string appActivity,
            string appWaitPackage = "", string appWaitActivity = "", bool stopApp = true)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(appPackage));
            Contract.Requires(!string.IsNullOrWhiteSpace(appActivity));

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                ["appPackage"] = appPackage,
                ["appActivity"] = appActivity,
                ["appWaitPackage"] = appWaitPackage,
                ["appWaitActivity"] = appWaitActivity,
                ["dontStopAppOnReset"] = !stopApp
            };

            executeMethod.Execute(AppiumDriverCommand.StartActivity, parameters);
        }

        public static void StartActivityWithIntent(IExecuteMethod executeMethod, string appPackage, string appActivity,
            string intentAction, string appWaitPackage = "", string appWaitActivity = "",
            string intentCategory = "", string intentFlags = "", string intentOptionalArgs = "", bool stopApp = true)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(appPackage));
            Contract.Requires(!string.IsNullOrWhiteSpace(appActivity));
            Contract.Requires(!string.IsNullOrWhiteSpace(intentAction));

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                ["appPackage"] = appPackage,
                ["appActivity"] = appActivity,
                ["appWaitPackage"] = appWaitPackage,
                ["appWaitActivity"] = appWaitActivity,
                ["dontStopAppOnReset"] = !stopApp,
                ["intentAction"] = intentAction,
                ["intentCategory"] = intentCategory,
                ["intentFlags"] = intentFlags,
                ["optionalIntentArguments"] = intentOptionalArgs
            };

            executeMethod.Execute(AppiumDriverCommand.StartActivity, parameters);
        }

        public static string GetCurrentActivity(IExecuteMethod executeMethod) =>
            executeMethod.Execute(AppiumDriverCommand.GetCurrentActivity).Value as string;

        public static void SetConection(IExecuteMethod executeMethod, ConnectionType connectionType)
        {
            Dictionary<string, object> values = new Dictionary<string, object>()
            {
                ["type"] = connectionType
            };

            Dictionary<string, object> dictionary = new Dictionary<string, object>()
            {
                ["name"] = "network_connection",
                ["parameters"] = values
            };

            executeMethod.Execute(AppiumDriverCommand.SetConnectionType, dictionary);
        }

        public static ConnectionType GetConection(IExecuteMethod executeMethod)
        {
            var commandResponse = executeMethod.Execute(AppiumDriverCommand.GetConnectionType, null);
            if (commandResponse.Status == WebDriverResult.Success)
            {
                return (ConnectionType) (long) commandResponse.Value;
            }
            else
            {
                throw new WebDriverException("The request to get the ConnectionType has failed.");
            }
        }

        public static void ToggleLocationServices(IExecuteMethod executeMethod) =>
            executeMethod.Execute(AppiumDriverCommand.ToggleLocationServices);

        public static string EndTestCoverage(IExecuteMethod executeMethod, string intent, string path) =>
            executeMethod.Execute(AppiumDriverCommand.EndTestCoverage,
                new Dictionary<string, object>()
                    {["intent"] = intent, ["path"] = path}).Value as string;

        public static void PushFile(IExecuteMethod executeMethod, string pathOnDevice, byte[] base64Data) =>
            executeMethod.Execute(AppiumDriverCommand.PushFile, new Dictionary<string, object>()
                {["path"] = pathOnDevice, ["data"] = base64Data});

        public static void PushFile(IExecuteMethod executeMethod, string pathOnDevice, FileInfo file)
        {
            if (file == null)
            {
                throw new ArgumentException("The file argument should not be null");
            }

            if (!file.Exists)
            {
                throw new ArgumentException("The file " + file.FullName + " doesn't exist");
            }

            byte[] bytes = File.ReadAllBytes(file.FullName);
            string fileBase64Data = Convert.ToBase64String(bytes);
            PushFile(executeMethod, pathOnDevice, Convert.FromBase64String(fileBase64Data));
        }

        public static void OpenNotifications(IExecuteMethod executeMethod) =>
            executeMethod.Execute(AppiumDriverCommand.OpenNotifications);

        public static bool IsLocked(IExecuteMethod executeMethod) =>
            (bool) executeMethod.Execute(AppiumDriverCommand.IsLocked).Value;

        public static void Unlock(IExecuteMethod executeMethod) =>
            executeMethod.Execute(AppiumDriverCommand.UnlockDevice);

        public static void ReplaceValue(IExecuteMethod executeMethod, string elementId, string value) =>
            executeMethod.Execute(AppiumDriverCommand.ReplaceValue,
                new Dictionary<string, object>()
                    {["id"] = elementId, ["value"] = new string[] {value}});

        public static Dictionary<string, object> GetSettings(IExecuteMethod executeMethod) =>
            (Dictionary<string, object>) executeMethod.Execute(AppiumDriverCommand.GetSettings).Value;

        public static void SetSetting(IExecuteMethod executeMethod, string setting, object value)
        {
            var settings = new Dictionary<string, object>()
                {[setting] = value};
            var parameters = new Dictionary<string, object>()
                {["settings"] = settings};
            executeMethod.Execute(AppiumDriverCommand.UpdateSettings, parameters);
        }
    }
}