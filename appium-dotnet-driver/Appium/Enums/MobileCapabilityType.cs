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

namespace OpenQA.Selenium.Appium.Enums
{
    public sealed class MobileCapabilityType
    {
        /// <summary>
        /// Capability name used for the apllication setting.
        /// </summary>
        public static readonly string App = "app";

        /// <summary>
        /// Capability name used for the target platform name setting.
        /// </summary>
        public static readonly string PlatformName = "platformName";

        /// <summary>
        /// Capability name used for the target platform version setting.
        /// </summary>
        public static readonly string PlatformVersion = "platformVersion";

        /// <summary>
        /// Capability name used for the automation name (e.g. Appium, Selendroid and so on) setting.
        /// </summary>
        public static readonly string DeviceName = "deviceName";

        /// <summary>
        /// Time out for the waiting for a new command.
        /// </summary>
        public static readonly string NewCommandTimeout = "newCommandTimeout";

        /// <summary>
        /// Time out for the waiting for the device is ready.
        /// </summary>
        [Obsolete("Moved to AndroidMobileCapabilityType.DeviceReadyTimeout and it is going to be removed from here")]
        public static readonly string DeviceReadyTimeout = "deviceReadyTimeout";

        /// <summary>
        /// Time out for the waiting for the app is launched.
        /// </summary>
        [Obsolete("Moved to IOSMobileCapabilityType.LaunchTimeout and it is going to be removed from here")]
        public static readonly string LaunchTimeout = "launchTimeout";

        /// <summary>
        /// Name of mobile web browser to automate. Should be an empty string if automating an app instead.
        /// </summary>
        public static readonly string BrowserName = "browserName";

        /// <summary>
        /// Capability name used for the automation name (e.g. Appium, Selendroid and so on) setting.
        /// </summary>
        public static readonly string AutomationName = "automationName";

        /// <summary>
        /// Capability name used for the setting up of the required appium version.
        /// </summary>
        public static readonly string AppiumVersion = "appium-version";

        /// <summary>
        /// It is the required package name.
        /// </summary>
        [Obsolete("Moved to AndroidMobileCapabilityType.AppPackage and it is going to be removed from here")]
        public static readonly string AppPackage = "appPackage";

        /// <summary>
        /// It is the required activity name.
        /// </summary>
        [Obsolete("Moved to AndroidMobileCapabilityType.AppActivity and it is going to be removed from here")]
        public static readonly string AppActivity = "appActivity";

        /// <summary>
        /// It is the name of the activity which is expected.
        /// </summary>
        [Obsolete("Moved to AndroidMobileCapabilityType.AppWaitActivity and it is going to be removed from here")]
        public static readonly string AppWaitActivity = "appWaitActivity";

        /// <summary>
        /// It is the name of the package which is expected.
        /// </summary>
        [Obsolete("Moved to AndroidMobileCapabilityType.AppWaitPackage and it is going to be removed from here")]
        public static readonly string AppWaitPackage = "appWaitPackage";

        // <summary>
        /// Unique device identifier of the connected physical device
        /// </summary>
        public static readonly string Udid = "udid";

        /// <summary>
        /// (Sim/Emu-only) Language to set for the simulator / emulator
        /// </summary>
        public static readonly string Language = "language";

        /// <summary>
        /// (Sim/Emu-only) Locale to set for the simulator / emulator
        /// </summary>
        public static readonly string Locale = "locale";

        /// <summary>
        /// (Sim/Emu-only) start in a certain orientation
        /// </summary>
        public static readonly string Orientation = "orientation";

        /// <summary>
        ///  Move directly into Webview context. Default false
        /// </summary>
        public static readonly string AutoWebview = "autoWebview";

        /// <summary>
        /// Don't reset app state before this session. Default false
        /// </summary>
        public static readonly string NoReset = "noReset";

        /// <summary>
        /// (iOS) Delete the entire simulator folder. (Android) Reset app state by uninstalling app instead of clearing app data.
        /// On Android, this will also remove the app after the session is complete. Default false
        /// </summary>
        public static readonly string FullReset = "fullReset";

        // <summary>
        /// It is Selendroid port.
        /// </summary>
        [Obsolete("Moved to AndroidMobileCapabilityType.SelendroidPort and it is going to be removed from here")]
        public static readonly string SelendroidPort = "selendroidPort";

    }
}
