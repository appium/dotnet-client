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
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Text;
using System.IO;

namespace OpenQA.Selenium.Appium.Android
{
    public class AndroidDriver<W> : AppiumDriver<W>, IFindByAndroidUIAutomator<W>, IStartsActivity,
        IHasNetworkConnection,
        ISendsKeyEvents,
        IPushesFiles where W : IWebElement
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
        public AndroidDriver(AppiumServiceBuilder builder, DesiredCapabilities desiredCapabilities, TimeSpan commandTimeout)
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
        public AndroidDriver(AppiumLocalService service, DesiredCapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(service, SetPlatformToCapabilities(desiredCapabilities, Platform), commandTimeout)
        {
        }

        #region IFindByAndroidUIAutomator Members

        public W FindElementByAndroidUIAutomator(string selector) => (W) FindElement("-android uiautomator", selector);

        public ReadOnlyCollection<W> FindElementsByAndroidUIAutomator(string selector) => 
            CollectionConverterUnility.
            ConvertToExtendedWebElementCollection<W>(this.FindElements("-android uiautomator", selector));

        #endregion IFindByAndroidUIAutomator Members

        /// <summary>
        /// Opens an arbitrary activity during a test. If the activity belongs to
        /// another application, that application is started and the activity is opened.
        /// 
        /// </summary>
        /// <param name="appPackage">The package containing the activity to start.</param>
        /// <param name="appActivity">The activity to start.</param>
        /// <param name="appWaitPackage">Begin automation after this package starts. Can be null or empty.</param>
        /// <param name="appWaitActivity">Begin automation after this activity starts. Can be null or empty.</param>
        /// <param name="stopApp">If true, target app will be stopped.</param>
        public void StartActivity(string appPackage, string appActivity, string appWaitPackage = "", string appWaitActivity = "", bool stopApp = true)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(appPackage));
            Contract.Requires(!string.IsNullOrWhiteSpace(appActivity));

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                ["appPackage"] = appPackage,
                ["appActivity"] = appActivity,
                ["appWaitPackage"] = appWaitPackage,
                ["appWaitActivity"] = appWaitActivity,
                ["dontStopAppOnReset"] = !stopApp
            };

            Execute(AppiumDriverCommand.StartActivity, parameters);
        }

        /// <summary>
        /// Opens an arbitrary activity during a test. If the activity belongs to
        /// another application, that application is started and the activity is opened.
        /// 
        /// </summary>
        /// <param name="appPackage">The package containing the activity to start.</param>
        /// <param name="appActivity">The activity to start.</param>
        /// <param name="intentAction">Intent action which will be used to start activity.</param>
        /// <param name="appWaitPackage">Begin automation after this package starts. Can be null or empty.</param>
        /// <param name="appWaitActivity">Begin automation after this activity starts. Can be null or empty.</param>
        /// <param name="intentCategory">Intent category which will be used to start activity.</param>
        /// <param name="intentFlags">Flags that will be used to start activity.</param>
        /// <param name="intentOptionalArgs">Additional intent arguments that will be used to start activity.</param>
        /// <param name="stopApp">If true, target app will be stopped.</param>
        public void StartActivityWithIntent(string appPackage, string appActivity, string intentAction, string appWaitPackage = "", string appWaitActivity = "",
            string intentCategory = "", string intentFlags = "", string intentOptionalArgs = "", bool stopApp = true)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(appPackage));
            Contract.Requires(!string.IsNullOrWhiteSpace(appActivity));
            Contract.Requires(!string.IsNullOrWhiteSpace(intentAction));

            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                ["appPackage"] = appPackage,
                ["appActivity"] = appActivity,
                ["appWaitPackage"] = appWaitPackage,
                ["appWaitActivity"] = appWaitActivity,
                ["dontStopAppOnReset"] = !stopApp,
                ["intentAction"] = intentAction,
                ["intentCategory"] = intentCategory,
                ["intentFlags"] = intentFlags,
                ["optionalIntentArguments"] = intentOptionalArgs
            };

            Execute(AppiumDriverCommand.StartActivity, parameters);
        }

        #region Connection Type

        public ConnectionType ConnectionType
        {
            get
            {
                var commandResponse = Execute(AppiumDriverCommand.GetConnectionType, null);
                if (commandResponse.Status == WebDriverResult.Success)
                {
                    return (ConnectionType)(long)commandResponse.Value;
                }
                else
                {
                    throw new WebDriverException("The request to get the ConnectionType has failed.");
                }
            }
            set
            {
                Dictionary<string, object> values = new Dictionary<string, object>(){
                    ["type"] = value
                };

                Dictionary<string, object> dictionary = new Dictionary<string, object>(){
                    ["name"] = "network_connection",
                    ["parameters"] = values
                };

                Execute(AppiumDriverCommand.SetConnectionType, dictionary);
            }
        }
        #endregion Connection Type

        /// <summary>
        /// Sends a device key event with metastate
        /// </summary>
        /// <param name="keyCode">Code for the long key pressed on the Android device</param>
        /// <param name="metastate">metastate for the long key press</param>
        public void PressKeyCode(int keyCode, int metastate = -1)
        {
            var parameters = new Dictionary<string, object>()
            {["keycode"] = keyCode };
            if (metastate > 0)
            {
                parameters.Add("metastate", metastate);
            }
            Execute(AppiumDriverCommand.PressKeyCode, parameters);
        }

        /// <summary>
        /// Sends a device long key event with metastate
        /// </summary>
        /// <param name="keyCode">Code for the long key pressed on the Android device</param>
        /// <param name="metastate">metastate for the long key press</param>
        public void LongPressKeyCode(int keyCode, int metastate = -1)
        {
            var parameters = new Dictionary<string, object>()
            {["keycode"] = keyCode};
            if (metastate > 0)
            {
                parameters.Add("metastate", metastate);
            }
            Execute(AppiumDriverCommand.LongPressKeyCode, parameters);
        }

        /// <summary>
        /// Toggles Location Services.
        /// </summary>
        public void ToggleLocationServices() => Execute(AppiumDriverCommand.ToggleLocationServices, null);


        /// <summary>
        /// Gets Current Device Activity.
        /// </summary>
        /// 
        public string CurrentActivity
        {
            get
            {
                var commandResponse = Execute(AppiumDriverCommand.GetCurrentActivity, null);
                return commandResponse.Value as string;
            }
        }

        /// <summary>
        /// Get test-coverage data
        /// </summary>
        /// <param name="intent">a string containing the intent.</param>
        /// <param name="path">a string containing the path.</param>
        /// <return>a base64 string containing the data</return> 
        public string EndTestCoverage(string intent, string path) =>
            Execute(AppiumDriverCommand.EndTestCoverage, 
                new Dictionary<string, object>()
                {["intent"] = intent,["path"] = path }).Value as string;

        /// <summary>
        /// Saves a string as a file on the remote mobile device.
        /// </summary>
        /// <param name="pathOnDevice">Path to file to write data to on remote device</param>
        /// <param name="stringData">A string to write to remote device</param>
        public void PushFile(string pathOnDevice, string stringData) =>
            PushFile(pathOnDevice, Convert.FromBase64String(Convert.
                ToBase64String(Encoding.UTF8.GetBytes(stringData))));

        /// <summary>
        /// Saves base64 encoded data as a file on the remote mobile device.
        /// </summary>
        /// <param name="pathOnDevice">Path to file to write data to on remote device</param>
        /// <param name="base64Data">Base64 encoded byte array of data to write to remote device</param>
        public void PushFile(string pathOnDevice, byte[] base64Data) =>
            Execute(AppiumDriverCommand.PushFile, new Dictionary<string, object>()
                {["path"] = pathOnDevice,["data"] = base64Data });

        /// <summary>
        /// Saves given file as a file on the remote mobile device.
        /// </summary>
        /// <param name="pathOnDevice">Path to file to write data to on remote device</param>
        /// <param name="base64Data">A file to write to remote device</param>
        public void PushFile(string pathOnDevice, FileInfo file)
        {
            if (file == null)
            {
                throw new ArgumentException("The file argument should not be null");
            }

            if (!file.Exists)
            {
                throw new ArgumentException("The file " + file.FullName + " doesn't exist");
            }

            byte[] bytes = File.ReadAllBytes(file.FullName);
            string fileBase64Data = Convert.ToBase64String(bytes);
            PushFile(pathOnDevice, Convert.FromBase64String(fileBase64Data));
        }

        /// <summary>
        /// Open the notifications 
        /// </summary>
        public void OpenNotifications() => Execute(AppiumDriverCommand.OpenNotifications, null);


        protected override RemoteWebElement CreateElement(string elementId) => new AndroidElement(this, elementId);

        /// <summary>
        /// Set "ignoreUnimportantViews" setting.
        /// See: https://github.com/appium/appium/blob/master/docs/en/advanced-concepts/settings.md
        /// </summary>
        public void IgnoreUnimportantViews(bool value) => UpdateSetting("ignoreUnimportantViews", value);

        #region locking
        /**
        * This method locks a device.
        */
        public void Lock() =>
            Execute(AppiumDriverCommand.LockDevice, 
                new Dictionary<string, object>()
                {["seconds"] = 0 });

        /// <summary>
        /// Check if the device is locked
        /// </summary>
        /// <returns>true if device is locked, false otherwise</returns>
        public bool IsLocked() => 
            (bool) Execute(AppiumDriverCommand.IsLocked, null).Value;

        /**
         * This method unlocks a device.
         */
        public void Unlock() => Execute(AppiumDriverCommand.UnlockDevice, null);

        /// <summary>
        /// Convenience method for swiping across the screen
        /// </summary>
        /// <param name="startx">starting x coordinate</param>
        /// <param name="starty">starting y coordinate</param>
        /// <param name="endx">ending x coordinate</param>
        /// <param name="endy">ending y coordinate</param>
        /// <param name="duration">amount of time in milliseconds for the entire swipe action to take</param>
        public override void Swipe(int startx, int starty, int endx, int endy, int duration) =>
            DoSwipe(startx, starty, endx, endy, duration);

        #endregion

    }
}
