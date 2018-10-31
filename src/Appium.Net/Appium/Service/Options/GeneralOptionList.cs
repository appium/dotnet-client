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


namespace OpenQA.Selenium.Appium.Service.Options
{
    ///<summary>
    /// Here is the list of common Appium server arguments.
    /// All flags are optional, but some are required in conjunction with certain others.
    /// The full list is available here: http://appium.io/slate/en/master/?ruby#appium-server-arguments
    /// </summary>
    public sealed class GeneralOptionList : BaseOptionList
    {
        /// <summary>
        /// Enter REPL mode
        /// </summary>
        public static KeyValuePair<string, string> Shell(string value) =>
            GetKeyValuePair("--shell", value);

        ///<summary>
        /// callback IP Address (default: same as address) <br/>
        /// Sample <br/>
        /// --callback-address 127.0.0.1
        ///</summary>
        public static KeyValuePair<string, string> CallbackAddress(string value) =>
            new KeyValuePair<string, string>("--callback-address", value);

        ///<summary>
        /// callback port (default: same as port)<br/>
        /// Sample <br/>
        /// --callback-port 4723
        ///</summary>
        public static KeyValuePair<string, string> CallbackPort(string value) =>
            new KeyValuePair<string, string>("--callback-port", value);

        ///<summary>
        /// Enables session override (clobbering) <br/>
        ///</summary>
        public static KeyValuePair<string, string> OverrideSession() =>
            new KeyValuePair<string, string>("--session-override", string.Empty);

        ///<summary>
        /// Pre-launch the application before allowing the first session (Requires –app and, for Android, –app-pkg and –app-activity) <br/>
        ///</summary>
        public static KeyValuePair<string, string> PreLaunch() =>
            new KeyValuePair<string, string>("--pre-launch", string.Empty);

        ///<summary>
        /// The message log level to be shown <br/>
        /// Sample:<br/>
        /// --log-level debug
        ///</summary>
        public static KeyValuePair<string, string> LogLevel(string value) =>
            GetKeyValuePairUsingDefaultValue("--log-level", value, "debug");

        ///<summary>
        /// Show timestamps in console output <br/>
        ///</summary>
        public static KeyValuePair<string, string> LogTimeStamp() =>
            new KeyValuePair<string, string>("--log-timestamp", string.Empty);

        ///<summary>
        /// Use local timezone for timestamps <br/>
        ///</summary>
        public static KeyValuePair<string, string> LocalTimezone() =>
            new KeyValuePair<string, string>("--local-timezone", string.Empty);

        ///<summary>
        /// Don’t use colors in console output <br/>
        ///</summary>
        public static KeyValuePair<string, string> LogNoColors() =>
            new KeyValuePair<string, string>("--log-no-colors", string.Empty);

        ///<summary>
        /// Also send log output to this HTTP listener <br/>
        /// Sample: <br/>
        /// --webhook localhost:9876
        ///</summary>
        public static KeyValuePair<string, string> WebHook(string value) =>
            GetKeyValuePair("--webhook", value);

        ///<summary>
        /// Configuration JSON file to register Appium with selenium grid<br/>
        /// Sample:<br/>
        /// --nodeconfig /abs/path/to/nodeconfig.json
        ///</summary>
        public static KeyValuePair<string, string> NodeConfig(string value) =>
            GetKeyValuePair("--nodeconfig", value);


        ///<summary>
        /// IP Address of robot<br/>
        /// Sample:<br/>
        /// --robot-address 0.0.0.0
        ///</summary>
        public static KeyValuePair<string, string> RobotAddress(string value) =>
            GetKeyValuePairUsingDefaultValue("--robot-address", value, "0.0.0.0");

        ///<summary>
        /// Port for robot<br/>
        /// Sample: <br/>
        /// --robot-port 4242
        ///</summary>
        public static KeyValuePair<string, string> RobotPort(string value) =>
            GetKeyValuePairUsingDefaultValue("--robot-port", value, "-1");

        ///<summary>
        /// Show info about the Appium server configuration and exit<br/>
        ///</summary>
        public static KeyValuePair<string, string> ShowConfig() =>
            new KeyValuePair<string, string>("--show-config", string.Empty);

        ///<summary>
        /// Bypass Appium’s checks to ensure we can read/write necessary files<br/>
        ///</summary>
        public static KeyValuePair<string, string> NoPermsCheck() =>
            new KeyValuePair<string, string>("--no-perms-check", string.Empty);

        ///<summary>
        /// Cause sessions to fail if desired caps are sent in that Appium does not
        /// recognize as valid for the selected device<br/>
        ///</summary>
        public static KeyValuePair<string, string> StrictCaps() =>
            new KeyValuePair<string, string>("--strict-caps", string.Empty);

        ///<summary>
        /// Absolute path to directory Appium can use to manage temporary files, like
        /// built-in iOS apps it needs to move around. On *nix/Mac defaults to /tmp,
        /// on Windows defaults to C:\Windows\Temp<br/>
        ///</summary>
        public static KeyValuePair<string, string> TMP(string value) =>
            GetKeyValuePair("--tmp", value);

        ///<summary>
        /// Add exaggerated spacing in logs to help with visual inspection<br/>
        ///</summary>
        public static KeyValuePair<string, string> DebugLogSpacing() =>
            new KeyValuePair<string, string>("--debug-log-spacing", string.Empty);

        /// <summary>
        /// Add long stack traces to log entries. Recommended for debugging only.
        /// </summary>
        public static KeyValuePair<string, string> AsyncTrace() =>
            new KeyValuePair<string, string>("--async-trace", string.Empty);
    }
}