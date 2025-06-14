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

using System;
using OpenQA.Selenium.Appium.Interfaces;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using OpenQA.Selenium.Appium.Enums;

namespace OpenQA.Selenium.Appium.Android
{
    public sealed class AndroidCommandExecutionHelper : AppiumCommandExecutionHelper
    {
        public static string GetCurrentActivity(IExecuteMethod executeMethod)
        {
            return executeMethod.Execute(DriverCommand.ExecuteScript, new Dictionary<string, object> {
                ["script"] = "mobile:getCurrentActivity",
                ["args"] = Array.Empty<object>()
            }).Value.ToString();
        }

        public static string GetCurrentPackage(IExecuteMethod executeMethod)
        {
            return executeMethod.Execute(DriverCommand.ExecuteScript, new Dictionary<string, object> {
                ["script"] = "mobile:getCurrentPackage",
                ["args"] = Array.Empty<object>()
            }).Value.ToString();
        }

        #region Device Network

        public static void ToggleLocationServices(IExecuteMethod executeMethod) {
            executeMethod.Execute(DriverCommand.ExecuteScript, new Dictionary<string, object> {
                ["script"] = "mobile:toggleGps",
                ["args"] = Array.Empty<object>()
            });
        }

        public static void GsmCall(IExecuteMethod executeMethod, string number, GsmCallActions gsmCallAction) {
            executeMethod.Execute(DriverCommand.ExecuteScript, new Dictionary<string, object> {
                ["script"] = "mobile:gsmCall",
                ["args"] = new object[] {
                    new Dictionary<string, object>() {
                        ["phoneNumber"] = number,
                        ["action"] = gsmCallAction.ToString().ToLowerInvariant()
                    }
                }
            });
        }

        public static void SendSms(IExecuteMethod executeMethod, string number, string message) {
            executeMethod.Execute(DriverCommand.ExecuteScript, new Dictionary<string, object> {
                ["script"] = "mobile:sendSms",
                ["args"] = new object[] {
                    new Dictionary<string, object>() {
                        ["phoneNumber"] = number,
                        ["message"] = message
                    }
                }
            });
        }

        public static void SetGsmStrength(IExecuteMethod executeMethod, GsmSignalStrength gsmSignalStrength)
        {
            executeMethod.Execute(DriverCommand.ExecuteScript, new Dictionary<string, object> {
                ["script"] = "mobile:gsmSignal",
                ["args"] = new object[] {
                    new Dictionary<string, object>() {
                        ["strength"] = gsmSignalStrength,
                    }
                }
            });
        }

        public static void SetGsmVoice(IExecuteMethod executeMethod, GsmVoiceState gsmVoiceState)
        {
            executeMethod.Execute(DriverCommand.ExecuteScript, new Dictionary<string, object> {
                ["script"] = "mobile:gsmVoice",
                ["args"] = new object[] {
                    new Dictionary<string, object>()
                    {
                        ["state"] = gsmVoiceState.ToString().ToLowerInvariant(),
                    }
                }
            });
        }

        #endregion

        #region Device Performance

        public static object[] GetPerformanceDataTypes(IExecuteMethod executeMethod) {
            return executeMethod.Execute(DriverCommand.ExecuteScript, new Dictionary<string, object> {
                ["script"] = "mobile:getPerformanceDataTypes",
                ["args"] = Array.Empty<object>()
            }).Value as object[];
        }

        private static Dictionary<string, object> CreatePerformanceDataRequest(params (string Name, object Value)[] args)
        {
            var argDict = new Dictionary<string, object>();
            foreach (var arg in args)
            {
                argDict[arg.Name] = arg.Value;
            }

            return new Dictionary<string, object>
            {
                ["script"] = "mobile:getPerformanceData",
                ["args"] = argDict.Count == 0 ? Array.Empty<object>() : new object[] { argDict }
            };
        }


        public static object[] GetPerformanceData(IExecuteMethod executeMethod, string packageName, string dataType) =>
            executeMethod.Execute(DriverCommand.ExecuteScript,
                CreatePerformanceDataRequest(
                    (nameof(packageName), packageName),
                    (nameof(dataType), dataType)
                )
            ).Value as object[];

        public static object[] GetPerformanceData(IExecuteMethod executeMethod, string packageName, string dataType, int dataReadTimeout) =>
            executeMethod.Execute(
                DriverCommand.ExecuteScript,
                CreatePerformanceDataRequest(
                    (nameof(packageName), packageName),
                    (nameof(dataType), dataType),
                    (nameof(dataReadTimeout), dataReadTimeout)
                )
            ).Value as object[];

        #endregion

        #region Device System

        public static void OpenNotifications(IExecuteMethod executeMethod)
        {
            executeMethod.Execute(DriverCommand.ExecuteScript, new Dictionary<string, object> {
                ["script"] = "mobile:openNotifications",
                ["args"] = Array.Empty<object>()
            });
        }

        public static IDictionary<string, object> GetSystemBars(IExecuteMethod executeMethod)
        {
            return executeMethod.Execute(DriverCommand.ExecuteScript, new Dictionary<string, object> {
                ["script"] = "mobile:getSystemBars",
                ["args"] = Array.Empty<object>()
            }).Value as IDictionary<string, object>;
        }

        public static float GetDisplayDensity(IExecuteMethod executeMethod)
        {
            return Convert.ToSingle(
                executeMethod.Execute(DriverCommand.ExecuteScript, new Dictionary<string, object> {
                ["script"] = "mobile:getDisplayDensity",
                ["args"] = Array.Empty<object>()
            }).Value);
        }

        #endregion

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