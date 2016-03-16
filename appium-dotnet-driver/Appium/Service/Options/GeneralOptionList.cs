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
    public sealed class GeneralOptionList
    {

        private static void CheckArgumentAndThrowException(string argument, string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("The argument " + argument + " requires not empty value");
            }
        }

        /// <summary>
        /// Enter REPL mode
        /// </summary>
        public static KeyValuePair<string, string> Shell(string value)
        {
            string argument = "--shell";
            CheckArgumentAndThrowException(argument, value);
            return new KeyValuePair<string, string>(argument, value);
        }

        /// <summary>
        /// IOS: abs path to simulator-compiled .app file or the bundle_id of the desired target on device; Android: abs path to .apk file
        /// Sample 
        /// --app /abs/path/to/my.app
        /// </summary>
        [Obsolete("This flag is obsolete since appium node 1.5.x. It will be removed in the next release. Be careful. Please use capabilities instead")]
        public static KeyValuePair<string, string> App(string value)
        {
            string argument = "--app";
            CheckArgumentAndThrowException(argument, value);
            return new KeyValuePair<string, string>(argument, value);
        }

        ///<summary>
        /// Unique device identifier of the connected physical device<br/>
        /// Sample <br/>
        /// --udid 1adsf-sdfas-asdf-123sdf
        ///</summary>
        [Obsolete("This flag is obsolete since appium node 1.5.x. It will be removed in the next release. Be careful. Please use capabilities instead")]
        public static KeyValuePair<string, string> UIID(string value)
        {
            string argument = "--udid";
            CheckArgumentAndThrowException(argument, value);
            return new KeyValuePair<string, string>(argument, value);
        }

        ///<summary>
        /// callback IP Address (default: same as address) <br/>
        /// Sample <br/>
        /// --callback-address 127.0.0.1
        ///</summary>
        public static KeyValuePair<string, string> CallbackAddress(string value)
        {
            return new KeyValuePair<string, string>("--callback-address", value);
        }

        ///<summary>
        /// callback port (default: same as port)<br/>
        /// Sample <br/>
        /// --callback-port 4723
        ///</summary>
        public static KeyValuePair<string, string> CallbackPort(string value)
        {
            return new KeyValuePair<string, string>("--callback-port", value);
        }

        ///<summary>
        /// Enables session override (clobbering) <br/>
        ///</summary>
        public static KeyValuePair<string, string> OverrideSession()
        {
            return new KeyValuePair<string, string>("--session-override", string.Empty);
        }

        ///<summary>
        /// Don’t reset app state between sessions (IOS: don’t delete app plist files; Android: don’t uninstall app before new session)<br/>
        ///</summary>
        [Obsolete("This flag is obsolete since appium node 1.5.x. It will be removed in the next release. Be careful. Please use capabilities instead")]
        public static KeyValuePair<string, string> NoReset()
        {
            return new KeyValuePair<string, string>("--no-reset", string.Empty);
        }

        ///<summary>
        /// Pre-launch the application before allowing the first session (Requires –app and, for Android, –app-pkg and –app-activity) <br/>
        ///</summary>
        public static KeyValuePair<string, string> PreLaunch()
        {
            return new KeyValuePair<string, string>("--pre-launch", string.Empty);
        }

        ///<summary>
        /// The message log level to be shown <br/>
        /// Sample:<br/>
        /// --log-level debug
        ///</summary>
        public static KeyValuePair<string, string> LogLevel(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return new KeyValuePair<string, string>("--log-level", "debug");
            }
            else
            {
                return new KeyValuePair<string, string>("--log-level", value);
            }

        }

        ///<summary>
        /// Show timestamps in console output <br/>
        ///</summary>
        public static KeyValuePair<string, string> LogTimeStamp()
        {
            return new KeyValuePair<string, string>("--log-timestamp", string.Empty);
        }

        ///<summary>
        /// Use local timezone for timestamps <br/>
        ///</summary>
        public static KeyValuePair<string, string> LocalTimezone()
        {
            return new KeyValuePair<string, string>("--local-timezone", string.Empty);
        }

        ///<summary>
        /// Don’t use colors in console output <br/>
        ///</summary>
        public static KeyValuePair<string, string> LogNoColors()
        {
            return new KeyValuePair<string, string>("--log-no-colors", string.Empty);
        }

        ///<summary>
        /// Also send log output to this HTTP listener <br/>
        /// Sample: <br/>
        /// --webhook localhost:9876
        ///</summary>
        public static KeyValuePair<string, string> WeebHook(string value)
        {
            string argument = "--webhook";
            CheckArgumentAndThrowException(argument, value);
            return new KeyValuePair<string, string>(argument, value);
        }

        ///<summary>
        /// Name of the mobile device to use<br/>
        /// Sample: <br/>
        /// --device-name iPhone Retina (4-inch), Android Emulator
        ///</summary>
        [Obsolete("This flag is obsolete since appium node 1.5.x. It will be removed in the next release. Be careful. Please use capabilities instead")]
        public static KeyValuePair<string, string> DeviceName(string value)
        {
            string argument = "--device-name";
            CheckArgumentAndThrowException(argument, value);
            return new KeyValuePair<string, string>(argument, value);
        }

        ///<summary>
        /// Name of the mobile platform: iOS, Android, or FirefoxOS <br/>
        /// Sample:<br/>
        /// --platform-name iOS
        ///</summary>
        [Obsolete("This flag is obsolete since appium node 1.5.x. It will be removed in the next release. Be careful. Please use capabilities instead")]
        public static KeyValuePair<string, string> PlatformName(string value)
        {
            string argument = "--platform-name";
            CheckArgumentAndThrowException(argument, value);
            return new KeyValuePair<string, string>(argument, value);
        }

        ///<summary>
        /// Version of the mobile platform <br/>
        /// Sample:<br/>
        /// --platform-version 7.1
        ///</summary>
        [Obsolete("This flag is obsolete since appium node 1.5.x. It will be removed in the next release. Be careful. Please use capabilities instead")]
        public static KeyValuePair<string, string> PlatformVersion(string value)
        {
            string argument = "--platform-version";
            CheckArgumentAndThrowException(argument, value);
            return new KeyValuePair<string, string>(argument, value);
        }

        ///<summary>
        /// Name of the automation tool: Appium or Selendroid<br/>
        /// Sample:<br/>
        /// --automation-name Appium
        ///</summary>
        [Obsolete("This flag is obsolete since appium node 1.5.x. It will be removed in the next release. Be careful. Please use capabilities instead")]
        public static KeyValuePair<string, string> AutomationName(string value)
        {
            string argument = "--automation-name";
            CheckArgumentAndThrowException(argument, value);
            return new KeyValuePair<string, string>(argument, value);
        }

        ///<summary>
        /// Name of the mobile browser: Safari or Chrome<br/>
        /// Sample: <br/>
        /// --browser-name Safari
        ///</summary>
        [Obsolete("This flag is obsolete since appium node 1.5.x. It will be removed in the next release. Be careful. Please use capabilities instead")]
        public static KeyValuePair<string, string> BrowserName(string value)
        {
            string argument = "--browser-name";
            CheckArgumentAndThrowException(argument, value);
            return new KeyValuePair<string, string>(argument, value);
        }


        ///<summary>
        /// Language for the iOS simulator / Android Emulator <br/>
        /// Sample:<br/>
        /// --language en
        ///</summary>
        [Obsolete("This flag is obsolete since appium node 1.5.x. It will be removed in the next release. Be careful. Please use capabilities instead")]
        public static KeyValuePair<string, string> Language(string value)
        {
            string argument = "--language";
            CheckArgumentAndThrowException(argument, value);
            return new KeyValuePair<string, string>(argument, value);
        }

        ///<summary>
        /// Locale for the iOS simulator / Android Emulator<br/>
        /// Sample:<br/>
        /// --locale en_US
        ///</summary>
        [Obsolete("This flag is obsolete since appium node 1.5.x. It will be removed in the next release. Be careful. Please use capabilities instead")]
        public static KeyValuePair<string, string> Locale(string value)
        {
            string argument = "--locale";
            CheckArgumentAndThrowException(argument, value);
            return new KeyValuePair<string, string>(argument, value);
        }

        ///<summary>
        /// Configuration JSON file to register Appium with selenium grid<br/>
        /// Sample:<br/>
        /// --nodeconfig /abs/path/to/nodeconfig.json
        ///</summary>
        public static KeyValuePair<string, string> NodeConfig(string value)
        {
            string argument = "--nodeconfig";
            CheckArgumentAndThrowException(argument, value);
            return new KeyValuePair<string, string>(argument, value);
        }


        ///<summary>
        /// IP Address of robot<br/>
        /// Sample:<br/>
        /// --robot-address 0.0.0.0
        ///</summary>
        public static KeyValuePair<string, string> RobotAddress(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return new KeyValuePair<string, string>("--robot-address", "0.0.0.0");
            }
            else
            {
                return new KeyValuePair<string, string>("--robot-address", value);
            }
        }

        ///<summary>
        /// Port for robot<br/>
        /// Sample: <br/>
        /// --robot-port 4242
        ///</summary>
        public static KeyValuePair<string, string> RobotPort(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return new KeyValuePair<string, string>("--robot-port", "-1");
            }
            else
            {
                return new KeyValuePair<string, string>("--robot-port", value);
            }
        }

        ///<summary>
        /// Port upon which ChromeDriver will run<br/>
        /// Sample:<br/>
        /// --chromedriver-port 9515
        ///</summary>
        [Obsolete("This flag is deprecated because it is moved to AndroidOptionList.ChromeDriverPort(value)")]
        public static KeyValuePair<string, string> ChromeDriverPort(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return new KeyValuePair<string, string>("--chromedriver-port", "9515");
            }
            else
            {
                return new KeyValuePair<string, string>("--chromedriver-port", value);
            }
        }

        ///<summary>
        /// ChromeDriver executable full path
        ///</summary>
        [Obsolete("This flag is deprecated because it is moved to AndroidOptionList.ChromeDriverExecutable(value)")]
        public static KeyValuePair<string, string> ChromeDriverExecutable(string value)
        {
            string argument = "--chromedriver-executable";
            CheckArgumentAndThrowException(argument, value);
            return new KeyValuePair<string, string>(argument, value);
        }

        ///<summary>
        /// Show info about the Appium server configuration and exit<br/>
        ///</summary>
        public static KeyValuePair<string, string> ShowConfig()
        {
            return new KeyValuePair<string, string>("--show-config", string.Empty);
        }

        ///<summary>
        /// Bypass Appium’s checks to ensure we can read/write necessary files<br/>
        ///</summary>
        public static KeyValuePair<string, string> NoPermsCheck()
        {
            return new KeyValuePair<string, string>("--no-perms-check", string.Empty);
        }


        ///<summary>
        /// The default command timeout for the server to use for all sessions. Will
        /// still be overridden by newCommandTimeout cap<br/>
        /// Default: 60
        ///</summary>
        [Obsolete("This flag is obsolete since appium node 1.5.x. It will be removed in the next release. Be careful. Please use capabilities instead")]
        public static KeyValuePair<string, string> CommandTimeOut(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return new KeyValuePair<string, string>("--command-timeout", "60");
            }
            else
            {
                return new KeyValuePair<string, string>("--command-timeout", value);
            }
        }

        ///<summary>
        /// Cause sessions to fail if desired caps are sent in that Appium does not
        /// recognize as valid for the selected device<br/>
        ///</summary>
        public static KeyValuePair<string, string> StrictCaps()
        {
            return new KeyValuePair<string, string>("--strict-caps", string.Empty);
        }

        ///<summary>
        /// Absolute path to directory Appium can use to manage temporary files, like
        /// built-in iOS apps it needs to move around. On *nix/Mac defaults to /tmp,
        /// on Windows defaults to C:\Windows\Temp<br/>
        ///</summary>
        public static KeyValuePair<string, string> TMP(string value)
        {
            string argument = "--tmp";
            CheckArgumentAndThrowException(argument, value);
            return new KeyValuePair<string, string>(argument, value);
        }

        ///<summary>
        /// Add exaggerated spacing in logs to help with visual inspection<br/>
        ///</summary>
        public static KeyValuePair<string, string> DebugLogSpacing()
        {
            return new KeyValuePair<string, string>("--debug-log-spacing", string.Empty);
        }
    }
}
