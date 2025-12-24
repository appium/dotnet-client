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

        #region Activity

        /// <summary>
        /// Start an Android activity by providing its package name and activity name.
        /// For documentation, see <see href="https://github.com/appium/appium-uiautomator2-driver#mobile-startactivity">mobile:startActivity</see>.
        /// </summary>
        /// <param name="executeMethod">The execute method</param>
        /// <param name="intent">The full name of the activity intent to start, e.g. "com.myapp/.MyActivity"</param>
        /// <param name="user">The user ID for which the activity is started. The current user is used by default.</param>
        /// <param name="wait">Set to true to block the method call until the Activity Manager's process returns the control to the system. false by default.</param>
        /// <param name="stop">Set to true to force stop the target app before starting the activity. false by default.</param>
        /// <param name="windowingMode">The windowing mode to launch the activity into. Check WindowConfiguration.java for possible values (constants starting with WINDOWING_MODE_).</param>
        /// <param name="activityType">The activity type to launch the activity as. Check WindowConfiguration.java for possible values (constants starting with ACTIVITY_TYPE_).</param>
        /// <param name="action">Action name. The actual value for the Activity Manager's -a argument.</param>
        /// <param name="uri">Unified resource identifier. The actual value for the Activity Manager's -d argument.</param>
        /// <param name="mimeType">Mime type. The actual value for the Activity Manager's -t argument.</param>
        /// <param name="identifier">Optional identifier. The actual value for the Activity Manager's -i argument.</param>
        /// <param name="categories">One or more category names. The actual value(s) for the Activity Manager's -c argument.</param>
        /// <param name="component">Component name. The actual value for the Activity Manager's -n argument.</param>
        /// <param name="package">Package name. The actual value for the Activity Manager's -p argument.</param>
        /// <param name="extras">Optional intent arguments. Must be represented as an array of arrays, where each subarray contains two or three string items: value type, key (variable name) and the value itself. Supported value types: s (string), sn (null), z (boolean), i (integer), l (long), f (float), u (uri), cn (component name), ia (Integer[]), ial (List&lt;Integer&gt;), la (Long[]), lal (List&lt;Long&gt;), fa (Float[]), fal (List&lt;Float&gt;), sa (String[]), sal (List&lt;String&gt;).</param>
        /// <param name="flags">Intent startup-specific flags as a hexadecimal string. Check Intent documentation for available flag values (constants starting with FLAG_ACTIVITY_). Flag values can be merged using logical 'or' operation, e.g. "0x10200000".</param>
        public static void StartActivity(
            IExecuteMethod executeMethod,
            string intent,
            string user = null,
            bool? wait = null,
            bool? stop = null,
            int? windowingMode = null,
            int? activityType = null,
            string action = null,
            string uri = null,
            string mimeType = null,
            string identifier = null,
            string[] categories = null,
            string component = null,
            string package = null,
            string[][] extras = null,
            string flags = null)
        {
            var args = new Dictionary<string, object> { ["intent"] = intent };

            if (!string.IsNullOrEmpty(user))
                args["user"] = user;
            if (wait.HasValue)
                args["wait"] = wait.Value;
            if (stop.HasValue)
                args["stop"] = stop.Value;
            if (windowingMode.HasValue)
                args["windowingMode"] = windowingMode.Value;
            if (activityType.HasValue)
                args["activityType"] = activityType.Value;
            if (!string.IsNullOrEmpty(action))
                args["action"] = action;
            if (!string.IsNullOrEmpty(uri))
                args["uri"] = uri;
            if (!string.IsNullOrEmpty(mimeType))
                args["mimeType"] = mimeType;
            if (!string.IsNullOrEmpty(identifier))
                args["identifier"] = identifier;
            if (categories != null && categories.Length > 0)
                args["categories"] = categories;
            if (!string.IsNullOrEmpty(component))
                args["component"] = component;
            if (!string.IsNullOrEmpty(package))
                args["package"] = package;
            if (extras != null && extras.Length > 0)
                args["extras"] = extras;
            if (!string.IsNullOrEmpty(flags))
                args["flags"] = flags;

            executeMethod.Execute(DriverCommand.ExecuteScript, new Dictionary<string, object>
            {
                ["script"] = "mobile:startActivity",
                ["args"] = new object[] { args }
            });
        }

        #endregion

        /// <summary>
        /// Install an app on the Android device using mobile: installApp script.
        /// For documentation, see <see href="https://github.com/appium/appium-uiautomator2-driver?tab=readme-ov-file#mobile-installapp">mobile: installApp</see>.
        /// </summary>
        /// <param name="executeMethod">The execute method</param>
        /// <param name="appPath">Full path to the .apk on the local filesystem or a remote URL.</param>
        /// <param name="timeout">Optional timeout in milliseconds to wait for the app installation to complete. 60000ms by default.</param>
        /// <param name="allowTestPackages">Optional flag to allow test packages installation. false by default.</param>
        /// <param name="useSdcard">Optional flag to install the app on sdcard instead of device memory. false by default.</param>
        /// <param name="grantPermissions">Optional flag to grant all permissions requested in the app manifest automatically after installation. false by default.</param>
        /// <param name="replace">Optional flag to upgrade/reinstall if app is already present. true by default.</param>
        /// <param name="checkVersion">Optional flag to skip installation if device has equal or greater app version. false by default.</param>
        public static void InstallApp(
            IExecuteMethod executeMethod, 
            string appPath, 
            int? timeout = null, 
            bool? allowTestPackages = null, 
            bool? useSdcard = null, 
            bool? grantPermissions = null, 
            bool? replace = null, 
            bool? checkVersion = null)
        {
            var args = new Dictionary<string, object> { { "appPath", appPath } };
            
            if (timeout.HasValue)
                args["timeout"] = timeout.Value;
            if (allowTestPackages.HasValue)
                args["allowTestPackages"] = allowTestPackages.Value;
            if (useSdcard.HasValue)
                args["useSdcard"] = useSdcard.Value;
            if (grantPermissions.HasValue)
                args["grantPermissions"] = grantPermissions.Value;
            if (replace.HasValue)
                args["replace"] = replace.Value;
            if (checkVersion.HasValue)
                args["checkVersion"] = checkVersion.Value;

            executeMethod.Execute(DriverCommand.ExecuteScript, new Dictionary<string, object>
            {
                ["script"] = "mobile: installApp",
                ["args"] = new object[] { args }
            });
        }

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