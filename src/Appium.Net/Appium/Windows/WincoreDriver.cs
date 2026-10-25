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
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Android.Interfaces;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.Service;

namespace OpenQA.Selenium.Appium.Windows
{
    /// <summary>
    /// WincoreDriver is an Appium driver created to automate Windows apps.
    /// Read https://github.com/appium/appium-wincore-driver for more details.
    /// </summary>
    public class WincoreDriver : AppiumDriver, ISendsKeyEvents, IHidesKeyboardWithKeyName, IPushesFiles
    {
        private static readonly string Platform = MobilePlatform.Windows;

        /// <summary>
        /// Initializes a new instance of the WincoreDriver class
        /// </summary>
        /// <param name="commandExecutor">An <see cref="ICommandExecutor"/> object which executes commands for the driver.</param>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        public WincoreDriver(ICommandExecutor commandExecutor, DriverOptions driverOptions)
            : base(commandExecutor, SetPlatformAndAutomationToCapabilities(driverOptions))
        {
        }

        /// <summary>
        /// Initializes a new instance of the WincoreDriver class using Appium options
        /// </summary>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options of the browser.</param>
        public WincoreDriver(DriverOptions driverOptions)
            : base(SetPlatformAndAutomationToCapabilities(driverOptions))
        {
        }

        /// <summary>
        /// Initializes a new instance of the WincoreDriver class using Appium options and command timeout
        /// </summary>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public WincoreDriver(DriverOptions driverOptions, TimeSpan commandTimeout)
            : base(SetPlatformAndAutomationToCapabilities(driverOptions), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the WincoreDriver class using the AppiumServiceBuilder instance and Appium options
        /// </summary>
        /// <param name="builder">The object containing settings of the Appium local service which is going to be started.</param>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        public WincoreDriver(AppiumServiceBuilder builder, DriverOptions driverOptions)
            : base(builder, SetPlatformAndAutomationToCapabilities(driverOptions))
        {
        }

        /// <summary>
        /// Initializes a new instance of the WincoreDriver class using the AppiumServiceBuilder instance, Appium options and command timeout
        /// </summary>
        /// <param name="builder">The object containing settings of the Appium local service which is going to be started.</param>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public WincoreDriver(AppiumServiceBuilder builder, DriverOptions driverOptions, TimeSpan commandTimeout)
            : base(builder, SetPlatformAndAutomationToCapabilities(driverOptions), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the WincoreDriver class using the specified remote address and Appium options
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/).</param>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        public WincoreDriver(Uri remoteAddress, DriverOptions driverOptions)
            : base(remoteAddress, SetPlatformAndAutomationToCapabilities(driverOptions))
        {
        }

        /// <summary>
        /// Initializes a new instance of the WincoreDriver class using the specified Appium local service and Appium options
        /// </summary>
        /// <param name="service">The specified Appium local service.</param>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options of the browser.</param>
        public WincoreDriver(AppiumLocalService service, DriverOptions driverOptions)
            : base(service, SetPlatformAndAutomationToCapabilities(driverOptions))
        {
        }

        /// <summary>
        /// Initializes a new instance of the WincoreDriver class using the specified remote address, Appium options, and command timeout.
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/).</param>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public WincoreDriver(Uri remoteAddress, DriverOptions driverOptions, TimeSpan commandTimeout)
            : base(remoteAddress, SetPlatformAndAutomationToCapabilities(driverOptions), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the WincoreDriver class using the specified Appium local service, Appium options, and command timeout.
        /// </summary>
        /// <param name="service">The specified Appium local service.</param>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public WincoreDriver(AppiumLocalService service, DriverOptions driverOptions, TimeSpan commandTimeout)
            : base(service, SetPlatformAndAutomationToCapabilities(driverOptions), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the WincoreDriver class using the specified remote address, Appium options and AppiumClientConfig.
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/).</param>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        /// <param name="clientConfig">An instance of <see cref="AppiumClientConfig"/></param>
        public WincoreDriver(Uri remoteAddress, DriverOptions driverOptions, AppiumClientConfig clientConfig)
            : base(remoteAddress, SetPlatformAndAutomationToCapabilities(driverOptions), clientConfig)
        {
        }

        /// <summary>
        /// Initializes a new instance of the WincoreDriver class using the specified Appium local service, Appium options and AppiumClientConfig.
        /// </summary>
        /// <param name="service">The specified Appium local service.</param>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        /// <param name="clientConfig">An instance of <see cref="AppiumClientConfig"/></param>
        public WincoreDriver(AppiumLocalService service, DriverOptions driverOptions, AppiumClientConfig clientConfig)
            : base(service, SetPlatformAndAutomationToCapabilities(driverOptions), clientConfig)
        {
        }

        /// <summary>
        /// Initializes a new instance of the WincoreDriver class using the specified remote address, Appium options, command timeout and AppiumClientConfig.
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/).</param>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        /// <param name="clientConfig">An instance of <see cref="AppiumClientConfig"/></param>
        public WincoreDriver(Uri remoteAddress, DriverOptions driverOptions, TimeSpan commandTimeout, AppiumClientConfig clientConfig)
            : base(remoteAddress, SetPlatformAndAutomationToCapabilities(driverOptions), commandTimeout, clientConfig)
        {
        }

        /// <summary>
        /// Initializes a new instance of the WincoreDriver class using the specified Appium local service, Appium options, command timeout and AppiumClientConfig.
        /// </summary>
        /// <param name="service">The specified Appium local service.</param>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        /// <param name="clientConfig">An instance of <see cref="AppiumClientConfig"/></param>
        public WincoreDriver(AppiumLocalService service, DriverOptions driverOptions, TimeSpan commandTimeout, AppiumClientConfig clientConfig)
            : base(service, SetPlatformAndAutomationToCapabilities(driverOptions), commandTimeout, clientConfig)
        {
        }

        public void SetSetting(string setting, object value) =>
            AndroidCommandExecutionHelper.SetSetting(this, setting, value);

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

        public override string StartRecordingScreen() =>
            StartRecordingScreen(new WincoreStartScreenRecordingOptions());

        public override byte[] PullFolder(string remotePath) =>
            throw new NotSupportedException("The Wincore driver cannot pull folders");

        public new void HideKeyboard(string key) =>
            AppiumCommandExecutionHelper.HideKeyboard(this, key);

        public void PressKeyCode(KeyEvent keyEvent) => throw new NotImplementedException();

        public void LongPressKeyCode(KeyEvent keyEvent) => throw new NotImplementedException();

        public void PressKeyCode(int keyCode, int metastate = -1) =>
            AppiumCommandExecutionHelper.PressKeyCode(this, keyCode, metastate);

        public void LongPressKeyCode(int keyCode, int metastate = -1) =>
            AppiumCommandExecutionHelper.LongPressKeyCode(this, keyCode, metastate);

        #region App management

        /// <summary>
        /// Closes the Windows application.
        /// </summary>
        public void CloseApp()
        {
            Execute(DriverCommand.ExecuteScript, new Dictionary<string, object>
            {
                ["script"] = "windows: closeApp",
                ["args"] = Array.Empty<object>()
            });
        }

        /// <summary>
        /// Launches the Windows application.
        /// </summary>
        public void LaunchApp()
        {
            Execute(DriverCommand.ExecuteScript, new Dictionary<string, object>
            {
                ["script"] = "windows: launchApp",
                ["args"] = Array.Empty<object>()
            });
        }

        #endregion App management

        private static ICapabilities SetPlatformAndAutomationToCapabilities(DriverOptions driverOptions)
        {
            if (driverOptions is AppiumOptions appiumOptions && string.IsNullOrEmpty(appiumOptions.AutomationName))
            {
                appiumOptions.AutomationName = OpenQA.Selenium.Appium.Enums.AutomationName.Wincore;
            }

            return SetPlatformToCapabilities(driverOptions, Platform);
        }
    }
}
