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
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenQA.Selenium.Appium.Android.Enums;
using OpenQA.Selenium.Appium.Interactions;

namespace OpenQA.Selenium.Appium.Android
{
    public class AndroidDriver<W> : AppiumDriver<W>, IFindByAndroidUIAutomator<W>, IFindByAndroidDataMatcher<W>,
        IStartsActivity,
        IHasNetworkConnection, INetworkActions, IHasClipboard, IHasPerformanceData,
        ISendsKeyEvents,
        IPushesFiles, IHasSettings where W : IWebElement
    {
        private static readonly string Platform = MobilePlatform.Android;

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
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
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
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
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

        #region IFindByAndroidUIAutomator Members

        public W FindElementByAndroidUIAutomator(string selector) =>
            FindElement(MobileSelector.AndroidUIAutomator, selector);

        public W FindElementByAndroidUIAutomator(IUiAutomatorStatementBuilder selector) =>
            FindElement(MobileSelector.AndroidUIAutomator, selector.Build());

        public IReadOnlyCollection<W> FindElementsByAndroidUIAutomator(string selector) =>
            ConvertToExtendedWebElementCollection<W>(FindElements(MobileSelector.AndroidUIAutomator, selector));

        public IReadOnlyCollection<W> FindElementsByAndroidUIAutomator(IUiAutomatorStatementBuilder selector) =>
            ConvertToExtendedWebElementCollection<W>(FindElements(MobileSelector.AndroidUIAutomator, selector.Build()));

        #endregion IFindByAndroidUIAutomator Members

        #region IFindByAndroidDataMatcher Members

        public W FindElementByAndroidDataMatcher(string selector) =>
            FindElement(MobileSelector.AndroidDataMatcher, selector);

        public IReadOnlyCollection<W> FindElementsByAndroidDataMatcher(string selector) =>
            ConvertToExtendedWebElementCollection<W>(FindElements(MobileSelector.AndroidDataMatcher, selector));

        #endregion IFindByAndroidDataMatcher Members

        public void StartActivity(string appPackage, string appActivity, string appWaitPackage = "",
            string appWaitActivity = "", bool stopApp = true) =>
            AndroidCommandExecutionHelper.StartActivity(this, appPackage, appActivity, appWaitPackage, appWaitActivity,
                stopApp);

        public void StartActivityWithIntent(string appPackage, string appActivity, string intentAction,
            string appWaitPackage = "", string appWaitActivity = "",
            string intentCategory = "", string intentFlags = "", string intentOptionalArgs = "", bool stopApp = true) =>
            AndroidCommandExecutionHelper.StartActivityWithIntent(this, appPackage, appActivity, intentAction,
                appWaitPackage, appWaitActivity,
                intentCategory, intentFlags, intentOptionalArgs, stopApp);

        public string CurrentActivity
        {
            get { return AndroidCommandExecutionHelper.GetCurrentActivity(this); }
        }

        public string CurrentPackage
        {
            get { return AndroidCommandExecutionHelper.GetCurrentPackage(this); }
        }

        #region Connection Type

        public ConnectionType ConnectionType
        {
            get { return AndroidCommandExecutionHelper.GetConection(this); }
            set { AndroidCommandExecutionHelper.SetConection(this, value); }
        }

        #endregion Connection Type

        public void PressKeyCode(int keyCode, int metastate = -1) =>
            AppiumCommandExecutionHelper.PressKeyCode(this, keyCode, metastate);

        public void LongPressKeyCode(int keyCode, int metastate = -1) =>
            AppiumCommandExecutionHelper.LongPressKeyCode(this, keyCode, metastate);

        #region Device Network

        public void ToggleData() =>
            AndroidCommandExecutionHelper.ToggleData(this);

        public void ToggleAirplaneMode() => AndroidCommandExecutionHelper.ToggleAirplaneMode(this);

        public void ToggleWifi() => AndroidCommandExecutionHelper.ToggleWifi(this);

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

        /// <summary>
        /// Get test-coverage data
        /// </summary>
        /// <param name="intent">a string containing the intent.</param>
        /// <param name="path">a string containing the path.</param>
        /// <return>a base64 string containing the data</return> 
        public string EndTestCoverage(string intent, string path) =>
            AndroidCommandExecutionHelper.EndTestCoverage(this, intent, path);

        /// <summary>
        /// Open the notifications 
        /// </summary>
        public void OpenNotifications() => AndroidCommandExecutionHelper.OpenNotifications(this);


        protected override RemoteWebElementFactory CreateElementFactory() => new AndroidElementFactory(this);

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

        /**
        * This method locks a device.
        */
        public void Lock() => AppiumCommandExecutionHelper.Lock(this, 0);

        /// <summary>
        /// Check if the device is locked
        /// </summary>
        /// <returns>true if device is locked, false otherwise</returns>
        public bool IsLocked() => AndroidCommandExecutionHelper.IsLocked(this);

        /**
         * This method unlocks a device.
         */
        public void Unlock() => AndroidCommandExecutionHelper.Unlock(this);

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
            get { return AndroidCommandExecutionHelper.GetSettings(this); }

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
        /// <param name="contentType"></param>
        /// <param name="base64Content"></param>
        public void SetClipboard(ClipboardContentType contentType, string base64Content) =>
            AppiumCommandExecutionHelper.SetClipboard(this, contentType, base64Content);

        /// <summary>
        /// Get the content of the clipboard.
        /// </summary>
        /// <param name="contentType"></param>
        /// <remarks>Android supports plaintext only</remarks>
        /// <returns>The content of the clipboard as base64-encoded string or an empty string if the clipboard is empty</returns>
        public string GetClipboard(ClipboardContentType contentType) =>
            AppiumCommandExecutionHelper.GetClipboard(this, contentType);

        /// <summary>
        /// Sets text to the clipboard
        /// </summary>
        /// <param name="textContent"></param>
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
        /// <param name="url"></param>
        public void SetClipboardUrl(string url) => throw new NotImplementedException();

        /// <summary>
        /// Gets the url string from the clipboard
        /// </summary>
        /// <returns>The url string content of the clipboard or an empty string if the clipboard is empty</returns>
        public string GetClipboardUrl() => throw new NotImplementedException();

        /// <summary>
        /// Sets the image to the clipboard
        /// </summary>
        /// <param name="image"></param>
        public void SetClipboardImage(Image image) => throw new NotImplementedException();

        /// <summary>
        /// Gets the image from the clipboard
        /// </summary>
        /// <returns>The image content of the clipboard as base64-encoded string or null if there is no image on the clipboard</returns>
        public Image GetClipboardImage() => throw new NotImplementedException();
    }
}