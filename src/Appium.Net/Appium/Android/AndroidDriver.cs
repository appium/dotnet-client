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

using OpenQA.Selenium.Appium.Android.Interfaces;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.WebSocket;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenQA.Selenium.Appium.Android.Enums;

namespace OpenQA.Selenium.Appium.Android
{
    public class AndroidDriver : AppiumDriver,
        IStartsActivity,
        INetworkActions, IHasClipboard, IHasPerformanceData,
        ISendsKeyEvents,
        IPushesFiles, IHasSettings, IListensToLogcatMessages
    {
        private static readonly string Platform = MobilePlatform.Android;
        private static readonly StringWebSocketClient LogcatClient = new StringWebSocketClient();
        private const int DefaultAppiumPort = 4723;

        /// <summary>
        /// Initializes a new instance of the AndroidDriver class
        /// </summary>
        /// <param name="commandExecutor">An <see cref="ICommandExecutor"/> object which executes commands for the driver.</param>
        /// <param name="driverOptions">An <see cref="ICapabilities"/> object containing the Appium options.</param>
        public AndroidDriver(ICommandExecutor commandExecutor, DriverOptions driverOptions)
            : base(commandExecutor, SetPlatformToCapabilities(driverOptions, Platform))
        {
        }


        /// <summary>
        /// Initializes a new instance of the AndroidDriver class using Appium options
        /// </summary>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options of the browser.</param>
        public AndroidDriver(DriverOptions driverOptions)
            : base(SetPlatformToCapabilities(driverOptions, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the AndroidDriver class using Appium options and command timeout
        /// </summary>
        /// <param name="driverOptions">An <see cref="ICapabilities"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public AndroidDriver(DriverOptions driverOptions, TimeSpan commandTimeout)
            : base(SetPlatformToCapabilities(driverOptions, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AndroidDriver class using the AppiumServiceBuilder instance and Appium options
        /// </summary>
        /// <param name="builder"> object containing settings of the Appium local service which is going to be started</param>
        /// <param name="driverOptions">An <see cref="ICapabilities"/> object containing the Appium options.</param>
        public AndroidDriver(AppiumServiceBuilder builder, DriverOptions driverOptions)
            : base(builder, SetPlatformToCapabilities(driverOptions, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the AndroidDriver class using the AppiumServiceBuilder instance, Appium options and command timeout
        /// </summary>
        /// <param name="builder"> object containing settings of the Appium local service which is going to be started</param>
        /// <param name="driverOptions">An <see cref="ICapabilities"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public AndroidDriver(AppiumServiceBuilder builder, DriverOptions driverOptions,
            TimeSpan commandTimeout)
            : base(builder, SetPlatformToCapabilities(driverOptions, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AndroidDriver class using the specified remote address and Appium options
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/).</param>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        public AndroidDriver(Uri remoteAddress, DriverOptions driverOptions)
            : base(remoteAddress, SetPlatformToCapabilities(driverOptions, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the AndroidDriver class using the specified Appium local service and Appium options
        /// </summary>
        /// <param name="service">the specified Appium local service</param>
        /// <param name="driverOptions">An <see cref="ICapabilities"/> object containing the Appium options of the browser.</param>
        public AndroidDriver(AppiumLocalService service, DriverOptions driverOptions)
            : base(service, SetPlatformToCapabilities(driverOptions, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the AndroidDriver class using the specified remote address, Appium options, and command timeout.
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/).</param>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public AndroidDriver(Uri remoteAddress, DriverOptions driverOptions, TimeSpan commandTimeout)
            : base(remoteAddress, SetPlatformToCapabilities(driverOptions, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AndroidDriver class using the specified Appium local service, Appium options, and command timeout.
        /// </summary>
        /// <param name="service">the specified Appium local service</param>
        /// <param name="driverOptions">An <see cref="ICapabilities"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public AndroidDriver(AppiumLocalService service, DriverOptions driverOptions,
            TimeSpan commandTimeout)
            : base(service, SetPlatformToCapabilities(driverOptions, Platform), commandTimeout)
        {
        }


        /// <summary>
        /// Initializes a new instance of the AndroidDriver class using the specified remote address, Appium options and AppiumClientConfig.
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/).</param>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        /// <param name="clientConfig">An instance of <see cref="AppiumClientConfig"/></param>
        public AndroidDriver(Uri remoteAddress, DriverOptions driverOptions, AppiumClientConfig clientConfig)
            : base(remoteAddress, SetPlatformToCapabilities(driverOptions, Platform), clientConfig)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AndroidDriver class using the specified Appium local service, Appium options and AppiumClientConfig,
        /// </summary>
        /// <param name="service">the specified Appium local service</param>
        /// <param name="driverOptions">An <see cref="ICapabilities"/> object containing the Appium options.</param>
        /// <param name="clientConfig">An instance of <see cref="AppiumClientConfig"/></param>
        public AndroidDriver(AppiumLocalService service, DriverOptions driverOptions, AppiumClientConfig clientConfig)
            : base(service, SetPlatformToCapabilities(driverOptions, Platform), clientConfig)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AndroidDriver class using the specified remote address, Appium options, command timeout and AppiumClientConfig.
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/).</param>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        /// <param name="clientConfig">An instance of <see cref="AppiumClientConfig"/></param>
        public AndroidDriver(Uri remoteAddress, DriverOptions driverOptions, TimeSpan commandTimeout, AppiumClientConfig clientConfig)
            : base(remoteAddress, SetPlatformToCapabilities(driverOptions, Platform), commandTimeout, clientConfig)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AndroidDriver class using the specified Appium local service, Appium options, command timeout and AppiumClientConfig,
        /// </summary>
        /// <param name="service">the specified Appium local service</param>
        /// <param name="driverOptions">An <see cref="ICapabilities"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        /// <param name="clientConfig">An instance of <see cref="AppiumClientConfig"/></param>
        public AndroidDriver(AppiumLocalService service, DriverOptions driverOptions, TimeSpan commandTimeout, AppiumClientConfig clientConfig)
            : base(service, SetPlatformToCapabilities(driverOptions, Platform), commandTimeout, clientConfig)
        {
        }

        public string CurrentActivity => AndroidCommandExecutionHelper.GetCurrentActivity(this);

        public string CurrentPackage => AndroidCommandExecutionHelper.GetCurrentPackage(this);

        #region Device Kesys

        public void PressKeyCode(int keyCode, int metastate = -1) =>
            AppiumCommandExecutionHelper.PressKeyCode(this, keyCode, metastate);

        public void LongPressKeyCode(int keyCode, int metastate = -1) =>
            AppiumCommandExecutionHelper.LongPressKeyCode(this, keyCode, metastate);

        public void PressKeyCode(KeyEvent keyEvent) =>
            AppiumCommandExecutionHelper.PressKeyCode(this, keyEvent);

        public void LongPressKeyCode(KeyEvent keyEvent) =>
            AppiumCommandExecutionHelper.LongPressKeyCode(this, keyEvent);

        #endregion

        #region Device Network

        public void ToggleLocationServices() => AndroidCommandExecutionHelper.ToggleLocationServices(this);

        public void MakeGsmCall(string phoneNumber, GsmCallActions gsmCallAction) =>
            AndroidCommandExecutionHelper.GsmCall(this, phoneNumber, gsmCallAction);

        public void SendSms(string phoneNumber, string message) =>
            AndroidCommandExecutionHelper.SendSms(this, phoneNumber, message);

        public void SetGsmSignalStrength(GsmSignalStrength gsmSignalStrength)
            => AndroidCommandExecutionHelper.SetGsmStrength(this, gsmSignalStrength);

        public void SetGsmVoice(GsmVoiceState gsmVoiceState) =>
            AndroidCommandExecutionHelper.SetGsmVoice(this, gsmVoiceState);

        #endregion

        #region Device System

        /// <summary>
        /// Open the notifications
        /// </summary>
        public void OpenNotifications() => AndroidCommandExecutionHelper.OpenNotifications(this);

        /// <summary>
        /// Retrieve visibility and bounds information of the status and navigation bars
        /// </summary>
        /// <returns>A dictionary whose string keys are named <code>statusBar</code><code>navigationBar</code></returns>
        public IDictionary<string, object> GetSystemBars() => AndroidCommandExecutionHelper.GetSystemBars(this);

        /// <summary>
        /// Retrieve display density(dpi) of the Android device
        /// </summary>
        /// <returns>Retrieve display density(dpi) of the Android device</returns>
        public float GetDisplayDensity() => AndroidCommandExecutionHelper.GetDisplayDensity(this);

        #endregion

        #region Device Performance Data

        public IList<object> GetPerformanceData(string packageName, string performanceDataType) =>
            AndroidCommandExecutionHelper.GetPerformanceData(this, packageName, performanceDataType)
                ?.ToList();

        public IList<object> GetPerformanceData(string packageName, string performanceDataType,
            int dataReadAttempts)
        {
            if (dataReadAttempts < 1) throw new ArgumentException($"{nameof(dataReadAttempts)} must be greater than 0");
            return AndroidCommandExecutionHelper
                .GetPerformanceData(this, packageName, performanceDataType, dataReadAttempts)
                ?.ToList();
        }

        public IList<string> GetPerformanceDataTypes() =>
            AndroidCommandExecutionHelper.GetPerformanceDataTypes(this)
                ?.Cast<string>()
                .ToList();

        #endregion

        #region Device Interactions

        /// <summary>
        /// Locks the device. Optionally, unlocks it after a specified number of seconds.
        /// </summary>
        /// <param name="seconds">
        /// The number of seconds after which the device will be automatically unlocked.
        /// Set to 0 or leave it empty to require manual unlock.
        /// </param>
        /// <exception cref="WebDriverException">Thrown if the command execution fails.</exception>
        public void Lock(int? seconds = null)
        {
            var parameters = new Dictionary<string, object>();

            if (seconds.HasValue && seconds.Value > 0)
            {
                parameters["seconds"] = seconds.Value;
            }

            ExecuteScript("mobile: lock", parameters);
        }

        /// <summary>
        /// Check if the device is locked
        /// </summary>
        /// <returns>true if device is locked, false otherwise</returns>
        public bool IsLocked() => (bool)ExecuteScript("mobile: isLocked");

        #nullable enable
        /// <summary>
        /// Unlocks the device if it is locked. No operation if the device's screen is not locked.
        /// </summary>
        /// <param name="key">The unlock key. See the documentation on appium:unlockKey capability for more details.</param>
        /// <param name="type">The unlock type. See the documentation on appium:unlockType capability for more details.</param>
        /// <param name="strategy">Optional unlock strategy. See the documentation on appium:unlockStrategy capability for more details.</param>
        /// <param name="timeoutMs">Optional unlock timeout in milliseconds. See the documentation on appium:unlockSuccessTimeout capability for more details.</param>
        /// <exception cref="ArgumentException">Thrown when required arguments are null or empty.</exception>
        /// <exception cref="WebDriverException">Thrown if the command execution fails.</exception>
        public void Unlock(string key, string type, string? strategy = null, int? timeoutMs = null)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Unlock key is required and cannot be null or whitespace.", nameof(key));

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Unlock type is required and cannot be null or whitespace.", nameof(type));

            var parameters = new Dictionary<string, object>
            {
                { "key", key },
                { "type", type }
            };

            if (strategy != null && !string.IsNullOrWhiteSpace(strategy))
            {
                parameters["strategy"] = strategy;
            }

            if (timeoutMs.HasValue)
            {
                parameters["timeoutMs"] = timeoutMs.Value;
            }

            ExecuteScript("mobile: unlock", parameters);
        }
        #endregion

        public void SetSetting(string setting, object value) =>
            AndroidCommandExecutionHelper.SetSetting(this, setting, value);

        public void IgnoreUnimportantViews(bool compress) =>
            SetSetting(AutomatorSetting.IgnoreUnimportantViews, compress);

        public void ConfiguratorSetWaitForIdleTimeout(int timeout) =>
            SetSetting(AutomatorSetting.WaitForIDLETimeout, timeout);

        public void ConfiguratorSetWaitForSelectorTimeout(int timeout) =>
            SetSetting(AutomatorSetting.WaitForSelectorTimeout, timeout);

        public void ConfiguratorSetScrollAcknowledgmentTimeout(int timeout) =>
            SetSetting(AutomatorSetting.WaitScrollAcknowledgmentTimeout, timeout);

        public void ConfiguratorSetKeyInjectionDelay(int delay) =>
            SetSetting(AutomatorSetting.KeyInjectionDelay, delay);

        public void ConfiguratorSetActionAcknowledgmentTimeout(int timeout) =>
            SetSetting(AutomatorSetting.WaitActionAcknowledgmentTimeout, timeout);

        public Dictionary<string, object> Settings
        {
            get => AndroidCommandExecutionHelper.GetSettings(this);

            set
            {
                foreach (var entry in value)
                {
                    SetSetting(entry.Key, entry.Value);
                }
            }
        }

        /// <summary>
        /// Sets the content to the clipboard
        /// </summary>
        /// <param name="contentType">An <see cref="ClipboardContentType"/> object to set (PlainText, Image or Url).</param>
        /// <param name="base64Content">The base64-encoded content to set.</param>
        public void SetClipboard(ClipboardContentType contentType, string base64Content) =>
            AppiumCommandExecutionHelper.MobileSetClipboard(this, contentType, base64Content);

        /// <summary>
        /// Get the content of the clipboard.
        /// </summary>
        /// <param name="contentType">An <see cref="ClipboardContentType"/> object (PlainText, Image or Url).</param>
        /// <remarks>Android supports plaintext only</remarks>
        /// <returns>The content of the clipboard as base64-encoded string or an empty string if the clipboard is empty</returns>
        public string GetClipboard(ClipboardContentType contentType) =>
            AppiumCommandExecutionHelper.MobileGetClipboard(this, contentType);

        /// <summary>
        /// Sets text to the clipboard
        /// </summary>
        /// <param name="textContent">The text content.</param>
        /// <param name="label">For Android only - A user visible label for the clipboard content.</param>
        public void SetClipboardText(string textContent, string label) =>
            AppiumCommandExecutionHelper.SetClipboardText(this, textContent, label);

        /// <summary>
        /// Get the plaintext content of the clipboard.
        /// </summary>
        /// <remarks>Android supports plaintext only</remarks>
        /// <returns>The string content of the clipboard or an empty string if the clipboard is empty</returns>
        public string GetClipboardText() => AppiumCommandExecutionHelper.GetClipboardText(this);

        /// <summary>
        /// Sets the url string to the clipboard
        /// </summary>
        /// <param name="url">The URL string.</param>
        public void SetClipboardUrl(string url) => throw new NotImplementedException();

        /// <summary>
        /// Gets the url string from the clipboard
        /// </summary>
        /// <returns>The url string content of the clipboard or an empty string if the clipboard is empty</returns>
        public string GetClipboardUrl() => throw new NotImplementedException();

        /// <summary>
        /// Sets the image to the clipboard
        /// </summary>
        /// <param name="image">The image object.</param>
        public void SetClipboardImage(Image image) => throw new NotImplementedException();

        /// <summary>
        /// Gets the image from the clipboard
        /// </summary>
        /// <returns>The image content of the clipboard as base64-encoded string or null if there is no image on the clipboard</returns>
        public Image GetClipboardImage() => throw new NotImplementedException();

        /// <summary>
        /// Gets the State of the app.
        /// </summary>
        /// <param name="appId">A string containing the id of the app.</param>
        /// <returns>an enumeration of the app state.</returns>
        public AppState GetAppState(string appId) =>
            (AppState)Convert.ToInt32(
                ExecuteScript(
                    "mobile:queryAppState",
                    new object[] {
                        new Dictionary<string, object>{
                            ["appId"] = appId
                        }
                    }
                )?.ToString() ?? throw new InvalidOperationException("ExecuteScript returned null for mobile:queryAppState")
            );

        #region Logcat Broadcast

        /// <summary>
        /// Start logcat messages broadcast via web socket.
        /// This method assumes that Appium server is running on localhost and
        /// is assigned to the default port (4723).
        /// </summary>
        public void StartLogcatBroadcast() => StartLogcatBroadcast("localhost", DefaultAppiumPort);

        /// <summary>
        /// Start logcat messages broadcast via web socket.
        /// This method assumes that Appium server is assigned to the default port (4723).
        /// </summary>
        /// <param name="host">The name of the host where Appium server is running.</param>
        public void StartLogcatBroadcast(string host) => StartLogcatBroadcast(host, DefaultAppiumPort);

        /// <summary>
        /// Start logcat messages broadcast via web socket.
        /// </summary>
        /// <param name="host">The name of the host where Appium server is running.</param>
        /// <param name="port">The port of the host where Appium server is running.</param>
        public void StartLogcatBroadcast(string host, int port)
        {
            ExecuteScript("mobile: startLogsBroadcast", new Dictionary<string, object>());
            var endpointUri = new Uri($"ws://{host}:{port}/ws/session/{SessionId}/appium/device/logcat");
            LogcatClient.ConnectAsync(endpointUri).Wait();
        }

        /// <summary>
        /// Adds a new log messages broadcasting handler.
        /// Several handlers might be assigned to a single server.
        /// Multiple calls to this method will cause such handler
        /// to be called multiple times.
        /// </summary>
        /// <param name="handler">A function, which accepts a single argument, which is the actual log message.</param>
        public void AddLogcatMessagesListener(Action<string> handler) => 
            LogcatClient.MessageHandlers.Add(handler);

        /// <summary>
        /// Adds a new log broadcasting errors handler.
        /// Several handlers might be assigned to a single server.
        /// Multiple calls to this method will cause such handler
        /// to be called multiple times.
        /// </summary>
        /// <param name="handler">A function, which accepts a single argument, which is the actual exception instance.</param>
        public void AddLogcatErrorsListener(Action<Exception> handler) => 
            LogcatClient.ErrorHandlers.Add(handler);

        /// <summary>
        /// Adds a new log broadcasting connection handler.
        /// Several handlers might be assigned to a single server.
        /// Multiple calls to this method will cause such handler
        /// to be called multiple times.
        /// </summary>
        /// <param name="handler">A function, which is executed as soon as the client is successfully connected to the web socket.</param>
        public void AddLogcatConnectionListener(Action handler) => 
            LogcatClient.ConnectionHandlers.Add(handler);

        /// <summary>
        /// Adds a new log broadcasting disconnection handler.
        /// Several handlers might be assigned to a single server.
        /// Multiple calls to this method will cause such handler
        /// to be called multiple times.
        /// </summary>
        /// <param name="handler">A function, which is executed as soon as the client is successfully disconnected from the web socket.</param>
        public void AddLogcatDisconnectionListener(Action handler) => 
            LogcatClient.DisconnectionHandlers.Add(handler);

        /// <summary>
        /// Removes all existing logcat handlers.
        /// </summary>
        public void RemoveAllLogcatListeners() => LogcatClient.RemoveAllHandlers();

        /// <summary>
        /// Stops logcat messages broadcast via web socket.
        /// </summary>
        public void StopLogcatBroadcast()
        {
            ExecuteScript("mobile: stopLogsBroadcast", new Dictionary<string, object>());
            LogcatClient.DisconnectAsync().Wait();
        }

        #endregion
    }
}