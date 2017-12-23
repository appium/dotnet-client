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
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using System.Collections.Generic;
using OpenQA.Selenium.Appium.Android.Enums;

namespace OpenQA.Selenium.Appium.Android
{
    public class AndroidDriver<W> : AppiumDriver<W>, IFindByAndroidUIAutomator<W>, IStartsActivity,
        IHasNetworkConnection,
        ISendsKeyEvents,
        IPushesFiles, IHasSettings where W : IWebElement
    {
        private static readonly string Platform = MobilePlatform.Android;

        /// <summary>
        /// Initializes a new instance of the AndroidDriver class
        /// </summary>
        /// <param name="commandExecutor">An <see cref="ICommandExecutor"/> object which executes commands for the driver.</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        public AndroidDriver(ICommandExecutor commandExecutor, DesiredCapabilities desiredCapabilities)
            : base(commandExecutor, SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }


        /// <summary>
        /// Initializes a new instance of the AndroidDriver class using desired capabilities
        /// </summary>
        /// <param name="desiredCapabilities">An <see cref="DesiredCapabilities"/> object containing the desired capabilities of the browser.</param>
        public AndroidDriver(DesiredCapabilities desiredCapabilities)
            : base(SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the AndroidDriver class using desired capabilities and command timeout
        /// </summary>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public AndroidDriver(DesiredCapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(SetPlatformToCapabilities(desiredCapabilities, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AndroidDriver class using the AppiumServiceBuilder instance and desired capabilities
        /// </summary>
        /// <param name="builder"> object containing settings of the Appium local service which is going to be started</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        public AndroidDriver(AppiumServiceBuilder builder, DesiredCapabilities desiredCapabilities)
            : base(builder, SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the AndroidDriver class using the AppiumServiceBuilder instance, desired capabilities and command timeout
        /// </summary>
        /// <param name="builder"> object containing settings of the Appium local service which is going to be started</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public AndroidDriver(AppiumServiceBuilder builder, DesiredCapabilities desiredCapabilities,
            TimeSpan commandTimeout)
            : base(builder, SetPlatformToCapabilities(desiredCapabilities, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AndroidDriver class using the specified remote address and desired capabilities
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="desiredCapabilities">An <see cref="DesiredCapabilities"/> object containing the desired capabilities.</param>
        public AndroidDriver(Uri remoteAddress, DesiredCapabilities desiredCapabilities)
            : base(remoteAddress, SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the AndroidDriver class using the specified Appium local service and desired capabilities
        /// </summary>
        /// <param name="service">the specified Appium local service</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities of the browser.</param>
        public AndroidDriver(AppiumLocalService service, DesiredCapabilities desiredCapabilities)
            : base(service, SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the AndroidDriver class using the specified remote address, desired capabilities, and command timeout.
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="desiredCapabilities">An <see cref="DesiredCapabilities"/> object containing the desired capabilities.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public AndroidDriver(Uri remoteAddress, DesiredCapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(remoteAddress, SetPlatformToCapabilities(desiredCapabilities, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AndroidDriver class using the specified Appium local service, desired capabilities, and command timeout.
        /// </summary>
        /// <param name="service">the specified Appium local service</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public AndroidDriver(AppiumLocalService service, DesiredCapabilities desiredCapabilities,
            TimeSpan commandTimeout)
            : base(service, SetPlatformToCapabilities(desiredCapabilities, Platform), commandTimeout)
        {
        }

        #region IFindByAndroidUIAutomator Members

        public W FindElementByAndroidUIAutomator(string selector) =>
            FindElement(MobileSelector.AndroidUIAutomator, selector);

        public ReadOnlyCollection<W> FindElementsByAndroidUIAutomator(string selector) =>
            ConvertToExtendedWebElementCollection<W>(FindElements(MobileSelector.AndroidUIAutomator, selector));

        #endregion IFindByAndroidUIAutomator Members

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

        /// <summary>
        /// Toggles Location Services.
        /// </summary>
        public void ToggleLocationServices() => AndroidCommandExecutionHelper.ToggleLocationServices(this);

        /// <summary>
        /// Get test-coverage data
        /// </summary>
        /// <param name="intent">a string containing the intent.</param>
        /// <param name="path">a string containing the path.</param>
        /// <return>a base64 string containing the data</return> 
        public string EndTestCoverage(string intent, string path) =>
            AndroidCommandExecutionHelper.EndTestCoverage(this, intent, path);

        public void PushFile(string pathOnDevice, string stringData) => AndroidCommandExecutionHelper.PushFile(this,
            pathOnDevice, Convert.FromBase64String(Convert.ToBase64String(Encoding.UTF8.GetBytes(stringData))));

        public void PushFile(string pathOnDevice, byte[] base64Data) =>
            AndroidCommandExecutionHelper.PushFile(this, pathOnDevice, base64Data);

        public void PushFile(string pathOnDevice, FileInfo file) =>
            AndroidCommandExecutionHelper.PushFile(this, pathOnDevice, file);

        /// <summary>
        /// Open the notifications 
        /// </summary>
        public void OpenNotifications() => AndroidCommandExecutionHelper.OpenNotifications(this);


        protected override RemoteWebElement CreateElement(string elementId) => new AndroidElement(this, elementId);

        #region locking

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
    }
}