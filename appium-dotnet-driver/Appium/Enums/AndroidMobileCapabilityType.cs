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
    /// The list of Android-specific capabilities
    /// Read: https://github.com/appium/appium/blob/1.5/docs/en/writing-running-appium/caps.md#android-only
    /// </summary>
    public sealed class AndroidMobileCapabilityType
    {
        /// <summary>
        /// Activity name for the Android activity you want to launch from your package. This often needs to be preceded
        /// by a . (e.g., .MainActivity instead of MainActivity)
        /// </summary>
        public static readonly string AppActivity = "appActivity";

        /// <summary>
        /// Java package of the Android app you want to run
        /// </summary>
        public static readonly string AppPackage = "appPackage";

        /// <summary>
        /// Activity name for the Android activity you want to wait for
        /// </summary>
        public static readonly string AppWaitActivity = "appWaitActivity";

        /// <summary>
        /// Java package of the Android app you want to wait for
        /// </summary>
        public static readonly string AppWaitPackage = "appWaitPackage";

        /// <summary>
        /// Timeout in seconds while waiting for device to become ready
        /// </summary>
        public static readonly string DeviceReadyTimeout = "deviceReadyTimeout";

        /// <summary>
        ///  Fully qualified instrumentation class. Passed to -w in adb shell am instrument -e coverage true -w
        /// </summary>
        public static readonly string AndroidCoverage = "androidCoverage";

        /// <summary>
        /// (Chrome and webview only) Enable Chromedriver's performance logging (default false)
        /// </summary>
        public static readonly string EnablePerformanceLogging = "enablePerformanceLogging";

        /// <summary>
        /// Timeout in seconds used to wait for a device to become ready after booting
        /// </summary>
        public static readonly string AndroidDeviceReadyTimeout = "androidDeviceReadyTimeout";

        /// <summary>
        /// Port used to connect to the ADB server (default 5037)
        /// </summary>
        public static readonly string AdbPort = "adbPort";

        /// <summary>
        /// Devtools socket name. Needed only when tested app is a Chromium embedding browser.
        /// The socket is open by the browser and Chromedriver connects to it as a devtools client.
        /// </summary>
        public static readonly string AndroidDeviceSocket = "androidDeviceSocket";

        /// <summary>
        /// Name of avd to launch
        /// </summary>
        public static readonly string Avd = "avd";

        /// <summary>
        /// How long to wait in milliseconds for an avd to launch and connect to ADB (default 120000)
        /// </summary>
        public static readonly string AvdLaunchTimeout = "avdLaunchTimeout";

        /// <summary>
        /// How long to wait in milliseconds for an avd to finish its boot animations (default 120000)
        /// </summary>
        public static readonly string AvdReadyTimeout = "avdReadyTimeout";

        /// <summary>
        /// Additional emulator arguments used when launching an avd
        /// </summary>
        public static readonly string AvdArgs = "avdArgs";

        /// <summary>
        /// Use a custom keystore to sign apks, default false
        /// </summary>
        public static readonly string UseKeystore = "useKeystore";

        /// <summary>
        /// Path to custom keystore, default ~/.android/debug.keystore
        /// </summary>
        public static readonly string KeystorePath = "keystorePath";

        /// <summary>
        /// Password for custom keystore
        /// </summary>
        public static readonly string KeystorePassword = "keystorePassword";

        /// <summary>
        /// Alias for key
        /// </summary>
        public static readonly string KeyAlias = "keyAlias";

        /// <summary>
        /// Password for key
        /// </summary>
        public static readonly string KeyPassword = "keyPassword";

        /// <summary>
        /// The absolute local path to webdriver executable (if Chromium embedder provides its own webdriver,
        /// it should be used instead of original chromedriver bundled with Appium)
        /// </summary>
        public static readonly string ChromedriverExecutable = "chromedriverExecutable";

        /// <summary>
        /// Amount of time to wait for Webview context to become active, in ms. Defaults to 2000
        /// </summary>
        public static readonly string AutoWebviewTimeout = "autoWebviewTimeout";

        /// <summary>
        /// Intent action which will be used to start activity (default android.intent.action.MAIN)
        /// </summary>
        public static readonly string IntentAction = "intentAction";

        /// <summary>
        /// Intent category which will be used to start activity (default android.intent.category.LAUNCHER)
        /// </summary>
        public static readonly string IntentCategory = "intentCategory";

        /// <summary>
        /// Flags that will be used to start activity (default 0x10200000)
        /// </summary>
        public static readonly string IntentFlags = "intentFlags";

        /// <summary>
        /// Additional intent arguments that will be used to start activity. See Intent arguments:
        /// http://developer.android.com/tools/help/adb.html#IntentSpec
        /// </summary>
        public static readonly string OptionalIntentArguments = "optionalIntentArguments";

        /// <summary>
        /// Doesn't stop the process of the app under test, before starting the app using adb.
        /// If the app under test is created by another anchor app, setting this false,
        /// allows the process of the anchor app to be still alive, during the start of the test app using adb.
        /// In other words, with dontStopAppOnReset set to true, we will not include the -S flag in the adb shell am start call.
        /// With this capability omitted or set to false, we include the -S flag. Default false
        /// </summary>
        public static readonly string DontStopAppOnReset = "dontStopAppOnReset";

        /// <summary>
        /// Enable Unicode input, default false
        /// </summary>
        public static readonly string UnicodeKeyboard = "unicodeKeyboard";

        /// <summary>
        /// Reset keyboard to its original state, after running Unicode tests with unicodeKeyboard capability.
        /// Ignored if used alone. Default false
        /// </summary>
        public static readonly string ResetKeyboard = "resetKeyboard";

        /// <summary>
        /// Skip checking and signing of app with debug keys, will work only with
        /// UiAutomator and not with selendroid, default false
        /// </summary>
        public static readonly string NoSign = "noSign";

        /// <summary>
        /// Calls the setCompressedLayoutHierarchy() uiautomator function. This capability can speed up test execution,
        /// since Accessibility commands will run faster ignoring some elements. The ignored elements will not be findable,
        /// which is why this capability has also been implemented as a toggle-able setting as well as a capability.
        /// Defaults to false
        /// </summary>
        public static readonly string IgnoreUnimportantViews = "ignoreUnimportantViews";

        /// <summary>
        /// Disables android watchers that watch for application not responding and application crash,
        /// this will reduce cpu usage on android device/emulator. This capability will work only with
        /// UiAutomator and not with selendroid, default false
        /// </summary>
        public static readonly string DisableAndroidWatchers = "disableAndroidWatchers";

        /// <summary>
        /// Allows passing chromeOptions capability for ChromeDriver. For more information see chromeOptions:
        /// https://sites.google.com/a/chromium.org/chromedriver/capabilities
        /// </summary>
        public static readonly string ChromeOptions = "chromeOptions";

        /**
         * Kill ChromeDriver session when moving to a non-ChromeDriver webview. Defaults to false
         */
        public static readonly string RECREATE_CHROME_DRIVER_SESSIONS = "recreateChromeDriverSessions";

        public static readonly string SELENDROID_PORT = "selendroidPort";

        /// <summary>
        /// In a web context, use native (adb) method for taking a screenshot, rather than proxying
        /// to ChromeDriver, default false.
        /// </summary>
        public static readonly string NativeWebScreenshot = "nativeWebScreenshot";

        /// <summary>
        /// The name of the directory on the device in which the screenshot will be put.
        /// Defaults to /data/local/tmp.
        /// </summary>
        public static readonly string AndroidScreenshotPath = "androidScreenshotPath";

        /// <summary>
        /// Timeout in milliseconds used to wait for an apk to install to the device. Defaults to `90000`.
        /// </summary>
        public static readonly string AndroidInstallTimeout = "androidInstallTimeout";

        /// <summary>
        /// Timeout in seconds while waiting for device to become ready.
        /// </summary>
        public static readonly string AppWaitDuration = "appWaitDuration";
    }
}