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

namespace OpenQA.Selenium.Appium.Enums
{
    /// <summary>
    /// The list of common capabilities. Read: https://appium.github.io/appium/docs/en/2.0/guides/caps/
    /// </summary>
    public sealed class MobileCapabilityType
    {
        /// <summary>
        /// Capability name used for the apllication setting.
        /// </summary>
        public static readonly string App = "appium:app";

        /// <summary>
        /// Capability name used for the target platform name setting.
        /// </summary>
        public static readonly string PlatformName = "platformName";

        /// <summary>
        /// Capability name used for the target platform version setting.
        /// </summary>
        public static readonly string PlatformVersion = "appium:platformVersion";

        /// <summary>
        /// Capability name used for the device name (e.g. Pixel 3XL, Galaxy S20 and so on) setting.
        /// </summary>
        public static readonly string DeviceName = "appium:deviceName";

        /// <summary>
        /// Time out for the waiting for a new command.
        /// </summary>
        public static readonly string NewCommandTimeout = "appium:newCommandTimeout";

        /// <summary>
        /// Name of mobile web browser to automate. Should be an empty string if automating an app instead.
        /// </summary>
        public static readonly string BrowserName = "browserName";

        /// <summary>
        /// Capability name used for the automation name (e.g. Appium, Selendroid and so on) setting.
        /// </summary>
        public static readonly string AutomationName = "appium:automationName";

        /// <summary>
        /// Capability name used for the setting up of the required appium version.
        /// </summary>
        public static readonly string AppiumVersion = "appium-version";

        /// <summary>
        /// Unique device identifier of the connected physical device
        /// </summary>
        public static readonly string Udid = "appium:udid";

        /// <summary>
        /// (Sim/Emu-only) Language to set for the simulator / emulator
        /// </summary>
        public static readonly string Language = "appium:language";

        /// <summary>
        /// (Sim/Emu-only) Locale to set for the simulator / emulator
        /// </summary>
        public static readonly string Locale = "appium:locale";

        /// <summary>
        /// (Sim/Emu-only) start in a certain orientation
        /// </summary>
        public static readonly string Orientation = "orientation";

        /// <summary>
        ///  Move directly into Webview context. Default false
        /// </summary>
        public static readonly string AutoWebview = "appium:autoWebview";

        /// <summary>
        /// Don't reset app state before this session. Default false
        /// </summary>
        public static readonly string NoReset = "appium:noReset";

        /// <summary>
        /// (iOS) Delete the entire simulator folder. (Android) Reset app state by uninstalling app instead of clearing app data.
        /// On Android, this will also remove the app after the session is complete. Default false
        /// </summary>
        public static readonly string FullReset = "appium:fullReset";

        /// <summary>
        /// App or list of apps (as a JSON array) to install prior to running tests. Note that it will not work with
        /// automationName of Espresso and iOS real devices.
        /// </summary>
        public static readonly string OtherApps = "appium:otherApps";
    }
}