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

namespace OpenQA.Selenium.Appium
{
    public class AppiumDriverCommand
    {
        #region Appium Specific extensions to JSONWP Commands

        /// <summary>
        /// Represents the Shake Device Mapping command
        /// </summary>
        public const string ShakeDevice = "shakeDevice";

        /// <summary>
        /// Toggle's the Airplane Mode ("Flight Safe Mode") Command
        /// </summary>
        public const string ToggleAirplaneMode = "toggleAirplaneMode";

        /// <summary>
        /// Toggle Wifi Command.
        /// </summary>
        public const string GsmCall = "gsm_call";

        // /// <summary>
        // /// Simulate an SMS message
        // /// </summary>
        public const string SendSms = "send_sms";

        /// <summary>
        /// Set GSM signal strength
        /// </summary>
        public const string SetGsmSignalStrength = "gsm_signal";

        /// <summary>
        /// Set GSM voice state
        /// </summary>
        public static string SetGsmVoiceState = "gsm_voice";

        /// <summary>
        /// Toggle Network Data Command
        /// </summary>
        public static string ToggleData = "toggleData";

        /// <summary>
        /// Press key code
        /// </summary>
        public const string PressKeyCode = "pressKeyCode";

        /// <summary>
        /// Long press key code
        /// </summary>
        public const string LongPressKeyCode = "longPressKeyCode";

        /// <summary>
        /// Get CurrentActivity Command.
        /// </summary>
        public const string GetCurrentActivity = "getCurrentActivity";

        /// <summary>
        /// Get CurrentPackage Command.
        /// </summary>
        public const string GetCurrentPackage = "getCurrentPackage";

        /// <summary>
        /// Install App Command.
        /// </summary>
        public const string InstallApp = "installApp";

        /// <summary>
        /// Remove App Command.
        /// </summary>
        public const string RemoveApp = "removeApp";

        /// <summary>
        /// Activate App Command.
        /// </summary>
        public const string ActivateApp = "activateApp";

        /// <summary>
        /// Remove App Command.
        /// </summary>
        public const string TerminateApp = "terminateApp";

        /// <summary>
        /// Is App Installed Command.
        /// </summary>
        public const string IsAppInstalled = "isAppInstalled";

        /// <summary>
        /// Push File Command.
        /// </summary>
        public const string PushFile = "pushFile";

        /// <summary>
        /// Pull File Command.
        /// </summary>
        public const string PullFile = "pullFile";

        /// <summary>
        /// Pull Folder Command.
        /// </summary>
        public const string PullFolder = "pullFolder";

        /// <summary>
        /// Toggle Wifi Command.
        /// </summary>
        public const string ToggleWiFi = "toggleWiFi";

        /// <summary>
        /// Toggle Location Services Command.
        /// </summary>
        public const string ToggleLocationServices = "toggleLocationServices";

        /// <summary>
        ///  Background App Command.
        /// </summary>
        public const string BackgroundApp = "backgroundApp";

        /// <summary>
        ///  End Test Coverage Command.
        /// </summary>
        public const string EndTestCoverage = "endTestCoverage";

        /// <summary>
        ///  Get App Strings Command.
        /// </summary>
        public const string GetAppStrings = "getAppStrings";

        /// <summary>
        /// Get App State Command.
        /// </summary>
        public const string GetAppState = "getAppState";

        /// <summary>
        /// Represents the Hide Keyboard command
        /// </summary>
        public const string HideKeyboard = "hideKeyboard";

        /// <summary>
        /// Whether or not the soft keyboard is shown
        /// </summary>
        public const string IsKeyboardShown = "isKeyboardShown";

        /// <summary>
        ///  Open Notifications Command
        /// </summary>
        public const string OpenNotifications = "openNotifications";

        /// <summary>
        /// Get System Bars
        /// </summary>
        public const string SystemBars = "system_bars";

        /// <summary>
        /// Get the time on the device
        /// </summary>
        public const string SystemTime = "getDeviceTime";

        /// <summary>
        /// Get Display Density
        /// </summary>
        public const string GetDisplayDensity = "display_density";

        /// <summary>
        /// The Start Activity command
        /// </summary>
        public const string StartActivity = "startActivity";

        /// <summary>
        /// Set GPS Location Command.
        /// </summary>
        public const string SetLocation = "setLocation";

        /// <summary>
        /// Get GPS Location Command.
        /// </summary>
        public const string GetLocation = "getLocation";

        public const string GetClipboard = "getClipboard";

        public const string SetClipboard = "setClipboard";

        public static string GetPerformanceData = "getPerformanceData";

        public static string GetPerformanceDataTypes = "getSuppportedPerformanceDataTypes";

        #endregion Appium Specific extensions to JSONWP Commands

        #region W3C Actions

        /// <summary>
        /// Perform multi purpose W3C actions
        /// </summary>
        public const string Actions = "actions";

        #endregion W3C Actions

        #region Context Commands

        /// <summary>
        /// Represents the Contexts command
        /// </summary>
        public const string Contexts = "contexts";

        /// <summary>
        /// Represents the Get Context command
        /// </summary>
        public const string GetContext = "getContext";

        /// <summary>
        /// Represents the Set Context command
        /// </summary>
        public const string SetContext = "setContext";

        #endregion Context Commands

        #region JSON Wire Protocol

        /// <summary>
        /// Represents the Get Orientation Command
        /// </summary>
        public const string GetOrientation = "getOrientation";

        /// <summary>
        /// Represents the Set Orientation Command
        /// </summary>
        public const string SetOrientation = "setOrientation";

        /// <summary>
        /// Represents the Get Network Connection Command
        /// </summary>
        public const string GetConnectionType = "getConnectionType";

        /// <summary>
        /// Represents the Set Network Connection Command
        /// </summary>
        public const string SetConnectionType = "setConnectionType";

        /// <summary>
        /// Represents the Get Available Engines Command
        /// </summary>
        public const string GetAvailableEngines = "getAvailableEngines";

        /// <summary>
        /// Represents the Get Active Engine Command
        /// </summary>
        public const string GetActiveEngine = "getActiveEngine";

        /// <summary>
        /// Represents the is IME active Command
        /// </summary>
        public const string IsIMEActive = "isIMEActive";

        /// <summary>
        /// Represents the Activate Engine Command
        /// </summary>
        public const string ActivateEngine = "activateEngine";

        /// <summary>
        /// Represents the Deactivate Engine Command
        /// </summary>
        public const string DeactivateEngine = "deactivateEngine";

        /// <summary>
        /// Represents the Screenshot Command
        /// </summary>
        public const string GetScreenshot = "screenshot";

        /// <summary>
        /// Represents the GetSettings Command
        /// </summary>
        public const string GetSettings = "getSettings";

        /// <summary>
        /// Represents the UpdateSettings Command
        /// </summary>
        public const string UpdateSettings = "updateSettings";

        /// <summary>
        /// Represents the TouchID command
        /// </summary>
        public const string TouchID = "touchId";

        /// <summary>
        /// Represents the fingerPrint command
        /// </summary>
        public const string FingerPrint = "fingerPrint";

        public const string GetDeviceTime = "getDeviceTime";

        public const string GetSession = "getSession";

        public const string StartRecordingScreen = "startRecordingScreen";

        public const string StopRecordingScreen = "stopRecordingScreen";

        #endregion JSON Wire Protocol

        #region Compare Images

        public const string CompareImages = "compareImages";

        #endregion
    }
}