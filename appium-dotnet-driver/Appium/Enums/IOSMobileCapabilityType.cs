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
    /// The list of iOS-specific capabilities
    /// Read: https://github.com/appium/appium/blob/1.5/docs/en/writing-running-appium/caps.md#ios-only
    /// </summary>
    public sealed class IOSMobileCapabilityType
    {
        /// <summary>
        /// (Sim-only) Calendar format to set for the iOS Simulator
        /// </summary>
        public static readonly string CalendarFormat = "calendarFormat";

        /// <summary>
        /// Bundle ID of the app under test. Useful for starting an app on a real device or for using other caps which require
        /// the bundle ID during test startup. To run a test on a real device using the bundle ID,
        /// you may omit the 'app' capability, but you must provide 'udid'.
        /// </summary>
        public static readonly string BundleId = "bundleId";

        /// <summary>
        /// Amount of time in ms to wait for instruments before assuming it hung and failing the session
        /// </summary>
        public static readonly string LaunchTimeout = "launchTimeout";

        /// <summary>
        /// (Sim-only) Force location services to be either on or off. Default is to keep current sim setting.
        /// </summary>
        public static readonly string LocationServicesEnabled = "locationServicesEnabled";

        /// <summary>
        /// (Sim-only) Set location services to be authorized or not authorized for app via plist, so that location services
        /// alert doesn't pop up. Default is to keep current sim setting. Note that
        /// if you use this setting you MUST also use the bundleId capability to send in your app's bundle ID.
        /// </summary>
        public static readonly string LocationServicesAuthorized = "locationServicesAuthorized";

        /// <summary>
        /// Accept all iOS alerts automatically if they pop up. This includes privacy access permission alerts
        /// (e.g., location, contacts, photos). Default is false.
        /// </summary>
        public static readonly string AutoAcceptAlerts = "autoAcceptAlerts";

        /// <summary>
        /// Dismiss all iOS alerts automatically if they pop up.
        /// This includes privacy access permission alerts (e.g.,
        /// location, contacts, photos). Default is false.
        /// </summary>
        public static readonly string AutoDismissAlerts = "autoDismissAlerts";

        /// <summary>
        /// Use native intruments lib (ie disable instruments-without-delay).
        /// </summary>
        public static readonly string NativeInstrumentsLib = "nativeInstrumentsLib";

        /// <summary>
        /// (Sim-only) Enable "real", non-javascript-based web taps in Safari.
        /// Default: false.
        /// Warning: depending on viewport size/ratio this might not accurately tap an element
        /// </summary>
        public static readonly string NativeWebTap = "nativeWebTap";

        /// <summary>
        /// (Sim-only) (>= 8.1) Initial safari url, default is a local welcome page
        /// </summary>
        public static readonly string SafariInitialUrl = "safariInitialUrl";

        /// <summary>
        /// (Sim-only) Allow javascript to open new windows in Safari. Default keeps current sim setting
        /// </summary>
        public static readonly string SafariAllowPopups = "safariAllowPopups";

        /// <summary>
        /// (Sim-only) Prevent Safari from showing a fraudulent website warning. Default keeps current sim setting.
        /// </summary>
        public static readonly string SafariIgnoreFraudWarning = "safariIgnoreFraudWarning";

        /// <summary>
        /// (Sim-only) Whether Safari should allow links to open in new windows. Default keeps current sim setting.
        /// </summary>
        public static readonly string SafariOpenLinksInBackground = "safariOpenLinksInBackground";

        /// <summary>
        /// (Sim-only) Whether to keep keychains (Library/Keychains) when appium session is started/finished
        /// </summary>
        public static readonly string KeepKeyChains = "keepKeyChains";

        /// <summary>
        /// Where to look for localizable strings. Default en.lproj
        /// </summary>
        public static readonly string LOCALIZABLE_STRINGS_DIR = "localizableStringsDir";

        /// <summary>
        /// Arguments to pass to the AUT using instruments
        /// </summary>
        public static readonly string ProcessArguments = "processArguments";

        /// <summary>
        /// The delay, in ms, between keystrokes sent to an element when typing.
        /// </summary>
        public static readonly string InterKeyDelay = "interKeyDelay";

        /// <summary>
        /// Whether to show any logs captured from a device in the appium logs. Default false
        /// </summary>
        public static readonly string ShowIOSLog = "showIOSLog";

        /// <summary>
        /// strategy to use to type test into a test field. Simulator default: oneByOne. Real device default: grouped
        /// </summary>
        public static readonly string SendKeyStrategy = "sendKeyStrategy";

        /// <summary>
        /// Max timeout in sec to wait for a screenshot to be generated. default: 10
        /// </summary>
        public static readonly string ScreenshotWaitTimeout = "screenshotWaitTimeout";

        /// <summary>
        /// The ios automation script used to determined if the app has been launched,
        /// by default the system wait for the page source not to be empty. The result must be a boolean
        /// </summary>
        public static readonly string WaitForAppScript = "waitForAppScript";

        /// <summary>
        /// Number of times to send connection message to remote debugger, to get webview. Default: 8
        /// </summary>
        public static readonly string WebviewConnectRetries = "webviewConnectRetries";

        /// <summary>
        /// The display name of the application under test. Used to automate backgrounding the app in iOS 9+.
        /// </summary>
        public static readonly string AppName = "appName";
    }
}