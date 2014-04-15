// <copyright file="AppiumDriver.cs" company="WebDriver Committers">
// Copyright 2007-2012 WebDriver committers
// Copyright 2007-2012 Google Inc.
// Portions copyright 2012 Software Freedom Conservancy
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections.ObjectModel;

namespace OpenQA.Selenium.Appium
{
    /// <summary>
    /// Provides a way to access Appium to run your tests by creating a AppiumDriver instance
    /// </summary>
    /// <remarks>
    /// When the WebDriver object has been instantiated the browser will load. The test can then navigate to the URL under test and 
    /// start your test.
    /// </remarks>
    /// <example>
    /// <code>
    /// [TestFixture]
    /// public class Testing
    /// {
    ///     private IWebDriver driver;
    ///     <para></para>
    ///     [SetUp]
    ///     public void SetUp()
    ///     {
    ///         driver = new AppiumDriver();
    ///     }
    ///     <para></para>
    ///     [Test]
    ///     public void TestGoogle()
    ///     {
    ///         driver.Navigate().GoToUrl("http://www.google.co.uk");
    ///         /*
    ///         *   Rest of the test
    ///         */
    ///     }
    ///     <para></para>
    ///     [TearDown]
    ///     public void TearDown()
    ///     {
    ///         driver.Quit();
    ///         driver.Dispose();
    ///     } 
    /// }
    /// </code>
    /// </example>
    public class AppiumDriver : RemoteWebDriver
    {


        #region Constructors
        /// <summary>
        /// Initializes a new instance of the RemoteWebDriver class
        /// </summary>
        /// <param name="commandExecutor">An <see cref="ICommandExecutor"/> object which executes commands for the driver.</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities of the browser.</param>
        public AppiumDriver(ICommandExecutor commandExecutor, ICapabilities desiredCapabilities)
            : base(commandExecutor, desiredCapabilities)
        {
            _AddAppiumCommands();
        }



        /// <summary>
        /// Initializes a new instance of the AppiumDriver class. This constructor defaults proxy to http://127.0.0.1:4723/wd/hub
        /// </summary>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities of the browser.</param>
        public AppiumDriver(ICapabilities desiredCapabilities)
            : this(new Uri("http://127.0.0.1:4723/wd/hub"), desiredCapabilities)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AppiumDriver class
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities of the browser.</param>
        public AppiumDriver(Uri remoteAddress, ICapabilities desiredCapabilities)
            : this(remoteAddress, desiredCapabilities, RemoteWebDriver.DefaultCommandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AppiumDriver class using the specified remote address, desired capabilities, and command timeout.
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities of the browser.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public AppiumDriver(Uri remoteAddress, ICapabilities desiredCapabilities, TimeSpan commandTimeout)
            : this(CommandExecutorFactory.GetHttpCommandExecutor(remoteAddress, commandTimeout), desiredCapabilities)
        {
        }
        #endregion Constructors

		#region IFindsByIosUIAutomation Members
		/// <summary>
		/// Finds the first element in the page that matches the Ios UIAutomation selector supplied
		/// </summary>
		/// <param name="selector">Selector for the element.</param>
		/// <returns>IWebElement object so that you can interact that object</returns>
		/// <example>
		/// <code>
		/// IWebDriver driver = new RemoteWebDriver(DesiredCapabilities.Firefox());
		/// IWebElement elem = driver.FindElementByIosUIAutomation('elements()'))
		/// </code>
		/// </example>
		public IWebElement FindElementByIosUIAutomation(string selector)
		{
			return this.FindElement("-ios uiautomation", selector);
		}

		/// <summary>
		/// Finds a list of elements that match the Ios UIAutomation selector supplied
		/// </summary>
		/// <param name="selector">Selector for the elements.</param>
		/// <returns>ReadOnlyCollection of IWebElement object so that you can interact with those objects</returns>
		/// <example>
		/// <code>
		/// IWebDriver driver = new RemoteWebDriver(DesiredCapabilities.Firefox());
		/// ReadOnlyCollection<![CDATA[<IWebElement>]]> elem = driver.FindElementsByIosUIAutomation(elements())
		/// </code>
		/// </example>
		public ReadOnlyCollection<IWebElement> FindElementsByIosUIAutomation(string selector)
		{
			return this.FindElements("-ios uiautomation", selector);
		}
		#endregion

		#region IFindsByAndroidUIAutomator Members
		/// <summary>
		/// Finds the first element in the page that matches the Android UIAutomator selector supplied
		/// </summary>
		/// <param name="selector">Selector for the element.</param>
		/// <returns>IWebElement object so that you can interact that object</returns>
		/// <example>
		/// <code>
		/// IWebDriver driver = new RemoteWebDriver(DesiredCapabilities.Firefox());
		/// IWebElement elem = driver.FindElementByAndroidUIAutomator('elements()'))
		/// </code>
		/// </example>
		public IWebElement FindElementByAndroidUIAutomator(string selector)
		{
			return this.FindElement("-android uiautomator", selector);
		}

		/// <summary>
		/// Finds a list of elements that match the Android UIAutomator selector supplied
		/// </summary>
		/// <param name="selector">Selector for the elements.</param>
		/// <returns>ReadOnlyCollection of IWebElement object so that you can interact with those objects</returns>
		/// <example>
		/// <code>
		/// IWebDriver driver = new RemoteWebDriver(DesiredCapabilities.Firefox());
		/// ReadOnlyCollection<![CDATA[<IWebElement>]]> elem = driver.FindElementsByAndroidUIAutomator(elements())
		/// </code>
		/// </example>
		public ReadOnlyCollection<IWebElement> FindElementsByAndroidUIAutomator(string selector)
		{
			return this.FindElements("-android uiautomator", selector);
		}
		#endregion

		#region IFindsByAccessibilityId Members
		/// <summary>
		/// Finds the first element in the page that matches the Accessibility Id selector supplied
		/// </summary>
		/// <param name="selector">Selector for the element.</param>
		/// <returns>IWebElement object so that you can interact that object</returns>
		/// <example>
		/// <code>
		/// IWebDriver driver = new RemoteWebDriver(DesiredCapabilities.Firefox());
		/// IWebElement elem = driver.FindElementByAccessibilityId('elements()'))
		/// </code>
		/// </example>
		public IWebElement FindElementByAccessibilityId(string selector)
		{
			return this.FindElement("accessibility id", selector);
		}

		/// <summary>
		/// Finds a list of elements that match the Accessibility Id selector supplied
		/// </summary>
		/// <param name="selector">Selector for the elements.</param>
		/// <returns>ReadOnlyCollection of IWebElement object so that you can interact with those objects</returns>
		/// <example>
		/// <code>
		/// IWebDriver driver = new RemoteWebDriver(DesiredCapabilities.Firefox());
		/// ReadOnlyCollection<![CDATA[<IWebElement>]]> elem = driver.FindElementsByAccessibilityId(elements())
		/// </code>
		/// </example>
		public ReadOnlyCollection<IWebElement> FindElementsByAccessibilityId(string selector)
		{
			return this.FindElements("accessibility id", selector);
		}
		#endregion
        
		#region MJsonMethod Members
		/// <summary>
        /// Shakes the device.
        /// </summary>
        public void ShakeDevice()
        {
            this.Execute(AppiumDriverCommand.ShakeDevice, null);
        }

        /// <summary>
        /// Locks the device.
        /// </summary>
        /// <param name="seconds">The number of seconds during which the device need to be locked for.</param>
        public void LockDevice(int seconds)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("seconds", seconds);
            this.Execute(AppiumDriverCommand.LockDevice, parameters);
        }

        // TODO: future implementation
        /// <summary>
        /// set/get the Airplane mode.
        /// true = phone is in airplane mode, false = phone is NOT in airplane mode
        /// </summary>
        //public bool AirplaneMode
        //{
        //    get
        //    {
        //        Response commandResponse = this.Execute(AppiumDriverCommand.AirplaneMode, null);
        //        return (bool)commandResponse.Value;
        //    }

        //    set
        //    {
        //        var parameters = new Dictionary<string, object>();
        //        // TODO: what is the key that the value needs to be set to?
        //        parameters.Add("value", value);
        //        this.Execute(AppiumDriverCommand.AirplaneMode, null);
        //    }
        //}

		/// <summary>
		/// Toggles Airplane Mode.
		/// </summary>
		public void ToggleAirplaneMode()
        {
            this.Execute(AppiumDriverCommand.ToggleAirplaneMode, null);
        }

		/// <summary>
		/// Triggers Device Key Event.
		/// </summary>
		/// <param name="keyCode">an integer keycode number corresponding to a java.awt.event.KeyEvent.</param>
		public void KeyEvent(String keyCode)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>();
			parameters.Add("keycode", keyCode);
			this.Execute(AppiumDriverCommand.KeyEvent, parameters);
		}

		/// <summary>
		/// Rotates Device.
		/// </summary>
		/// <param name="opts">rotations options like the following:
		/// new Dictionary<string, int> {{"x", 114}, {"y", 198}, {"duration", 5}, 
		/// {"radius", 3}, {"rotation", 220}, {"touchCount", 2}}
		/// </param>
		public void Rotate(Dictionary<string, int> opts)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>();
			foreach(KeyValuePair<string, int> opt in opts){
				parameters.Add(opt.Key, opt.Value);
			}
			this.Execute(AppiumDriverCommand.Rotate, parameters);
		}

		/// <summary>
		/// Gets Current Device Activity.
		/// </summary>
		public string GetCurrentActivity()
		{
			var commandResponse = this.Execute(AppiumDriverCommand.GetCurrentActivity, null);
			return commandResponse.Value as string;
		}

		/// <summary>
		/// Installs an App.
		/// </summary>
		/// <param name="appPath">a string containing the file path or url of the app.</param>
		public void InstallApp(string appPath)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>();
			parameters.Add("appPath", appPath);
			this.Execute(AppiumDriverCommand.InstallApp, parameters);
		}

		/// <summary>
		/// Removes an App.
		/// </summary>
		/// <param name="appPath">a string containing the id of the app.</param>
		public void RemoveApp(string appId)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>();
			parameters.Add("appId", appId);
			this.Execute(AppiumDriverCommand.RemoveApp, parameters);
		}

		/// <summary>
		/// Checks If an App Is Installed.
		/// </summary>
		/// <param name="appPath">a string containing the bundle id.</param>
		/// <return>a bol indicating if the app is installed.</return>
		public bool IsAppInstalled(string bundleId)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>();
			parameters.Add("bundleId", bundleId);
			var commandResponse = this.Execute(AppiumDriverCommand.IsAppInstalled, parameters);
			Console.Out.WriteLine (commandResponse.Value);
			return Convert.ToBoolean (commandResponse.Value.ToString());
		}

		/// <summary>
		/// Pushes a File.
		/// </summary>
		/// <param name="appPath">a string containing the id of the app.</param>
		public void PushFile(string pathOnDevice, string base64Data)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>();
			parameters.Add("path", pathOnDevice);
			parameters.Add("data", base64Data);
			this.Execute(AppiumDriverCommand.PushFile, parameters);
		}

		/// <summary>
		/// Pulls a File.
		/// </summary>
		/// <param name="appPath">a string containing the id of the app.</param>
		public string PullFile(string pathOnDevice)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>();
			parameters.Add("path", pathOnDevice);
			var commandResponse = this.Execute(AppiumDriverCommand.PullFile, parameters);
			return commandResponse.Value as string;
		}

		/// <summary>
		/// Toggles Wifi.
		/// </summary>
		public void ToggleWifi()
		{
			this.Execute(AppiumDriverCommand.ToggleWiFi, null);
		}

		/// <summary>
		/// Toggles Location Services.
		/// </summary>
		public void ToggleLocationServices()
		{
			this.Execute(AppiumDriverCommand.ToggleLocationServices, null);
		}

		/// <summary>
		/// Launches the current app.
		/// </summary>
		public void LaunchApp()
		{
			this.Execute(AppiumDriverCommand.LaunchApp, null);
		}

		/// <summary>
		/// Closes the current app.
		/// </summary>
		public void CloseApp()
		{
			this.Execute(AppiumDriverCommand.CloseApp, null);
		}

		/// <summary>
		/// Resets the current app.
		/// </summary>
		public void ResetApp()
		{
			this.Execute(AppiumDriverCommand.ResetApp, null);
		}

		/// <summary>
		/// Backgrounds the current app for the given number of seconds.
		/// </summary>
		/// <param name="seconds">a string containing the number of seconds.</param>
		public void BackgroundApp(int seconds)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>();
			parameters.Add("seconds", seconds);
			this.Execute(AppiumDriverCommand.BackgroundApp, parameters);
		}

		/// <summary>
		/// Pulls a File.
		/// </summary>
		/// <param name="intent">a string containing the intent.</param>
		/// <param name="path">a string containing the path.</param>
		/// <return>a base64 string containing the data</return> 
		public string EndTestCoverage(string intent, string path)
		{
			Dictionary<string, object> parameters = new Dictionary<string, object>();
			parameters.Add("intent", intent);
			parameters.Add("path", path);
			var commandResponse = this.Execute(AppiumDriverCommand.EndTestCoverage, parameters);
			return commandResponse.Value as string;
		}

		/// <summary>
		/// Gets the App Strings.
		/// </summary>
		public void GetAppStrings()
		{
			this.Execute(AppiumDriverCommand.GetAppStrings, null);
		}

		#endregion

        #region Context
        /// <summary>
        /// Get a list of available contexts
        /// </summary>
        /// <returns>a list of strings representing available contexts or an empty list if no contexts found</returns>
        public List<string> GetContexts()
        {
            var commandResponse = this.Execute(AppiumDriverCommand.Contexts, null);
            var contexts = new List<string>();
            var objects = commandResponse.Value as object[];

            if (null != objects && 0 < objects.Length)
            {
                contexts.AddRange(objects.Select(o => o.ToString()));
            }

            return contexts;
        }

        /// <summary>
        /// Get the current context
        /// </summary>
        /// <returns>current context if available or null representing the default context ("no context")</returns>
        public string GetContext()
        {
            var commandResponse = this.Execute(AppiumDriverCommand.GetContext, null);
            return commandResponse.Value as string;

        }

        /// <summary>
        /// Set the context
        /// </summary>
        /// <remarks>Will throw if the context is not found</remarks>
        /// <param name="name">name of the context to set</param>
        public void SetContext(string name)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("name", name);
            this.Execute(AppiumDriverCommand.SetContext, parameters);
        }
        #endregion Context

		#region Support methods
		/// <summary>
		/// In derived classes, the <see cref="PrepareEnvironment"/> method prepares the environment for test execution.
		/// </summary>
		protected virtual void PrepareEnvironment()
		{
			// Does nothing, but provides a hook for subclasses to do "stuff"
		}

		/// <summary>
		/// Creates a <see cref="RemoteWebElement"/> with the specified ID.
		/// </summary>
		/// <param name="elementId">The ID of this element.</param>
		/// <returns>A <see cref="RemoteWebElement"/> with the specified ID. For the FirefoxDriver this will be a <see cref="FirefoxWebElement"/>.</returns>
		protected override RemoteWebElement CreateElement(string elementId)
		{
			return new AppiumWebElement(this, elementId);
		}
		#endregion

        #region Private Methods
        /// <summary>
        /// Add the set of appium commands
        /// </summary>
        private static void _AddAppiumCommands()
        {
            var commandList = new List<_Commands>()
            {
                new _Commands(CommandInfo.PostCommand, AppiumDriverCommand.ShakeDevice, "/session/{sessionId}/appium/device/shake"),
                new _Commands(CommandInfo.PostCommand, AppiumDriverCommand.LockDevice, "/session/{sessionId}/appium/device/lock"),
                new _Commands(CommandInfo.PostCommand, AppiumDriverCommand.ToggleAirplaneMode, "/session/{sessionId}/appium/device/toggle_airplane_mode"),
                new _Commands(CommandInfo.GetCommand, AppiumDriverCommand.Contexts, "/session/{sessionId}/contexts" ),
                new _Commands(CommandInfo.GetCommand, AppiumDriverCommand.GetContext, "/session/{sessionId}/context" ),
                new _Commands(CommandInfo.PostCommand, AppiumDriverCommand.SetContext, "/session/{sessionId}/context" ),
				new _Commands(CommandInfo.PostCommand, AppiumDriverCommand.KeyEvent, "/session/{sessionId}/appium/device/keyevent"),
				new _Commands(CommandInfo.PostCommand, AppiumDriverCommand.Rotate, "/session/{sessionId}/appium/device/rotate"),
				new _Commands(CommandInfo.GetCommand, AppiumDriverCommand.GetCurrentActivity, "/session/{sessionId}/appium/device/current_activity"),
				new _Commands(CommandInfo.PostCommand, AppiumDriverCommand.InstallApp, "/session/{sessionId}/appium/device/install_app"),
				new _Commands(CommandInfo.PostCommand, AppiumDriverCommand.RemoveApp, "/session/{sessionId}/appium/device/remove_app"),
				new _Commands(CommandInfo.PostCommand, AppiumDriverCommand.IsAppInstalled, "/session/{sessionId}/appium/device/app_installed"),
				new _Commands(CommandInfo.PostCommand, AppiumDriverCommand.PushFile, "/session/{sessionId}/appium/device/push_file"),
				new _Commands(CommandInfo.PostCommand, AppiumDriverCommand.PullFile, "/session/{sessionId}/appium/device/pull_file"),
				new _Commands(CommandInfo.PostCommand, AppiumDriverCommand.ToggleWiFi, "/session/{sessionId}/appium/device/toggle_wifi"),
				new _Commands(CommandInfo.PostCommand, AppiumDriverCommand.ToggleLocationServices, "/session/{sessionId}/appium/device/toggle_location_services"),
				new _Commands(CommandInfo.PostCommand, AppiumDriverCommand.LaunchApp, "/session/{sessionId}/appium/app/launch"),
				new _Commands(CommandInfo.PostCommand, AppiumDriverCommand.CloseApp, "/session/{sessionId}/appium/app/close"),
				new _Commands(CommandInfo.PostCommand, AppiumDriverCommand.ResetApp, "/session/{sessionId}/appium/app/reset"),
				new _Commands(CommandInfo.PostCommand, AppiumDriverCommand.BackgroundApp, "/session/{sessionId}/appium/app/background"),
				new _Commands(CommandInfo.PostCommand, AppiumDriverCommand.EndTestCoverage, "/session/{sessionId}/appium/app/end_test_coverage"),
				new _Commands(CommandInfo.PostCommand, AppiumDriverCommand.GetAppStrings, "/session/{sessionId}/appium/app/strings"),
				new _Commands(CommandInfo.PostCommand, AppiumDriverCommand.SetImmediateValue, "/session/{sessionId}/appium/element/{id}/value"),
			};

            // Add the custom commandInfo of AppiumDriver
            var dynMethod = typeof(CommandInfoRepository).GetMethod("TryAddAdditionalCommand", BindingFlags.NonPublic | BindingFlags.Instance);

            // Add each new command to the Command Info Repository
            foreach (_Commands entry in commandList)
            {
                var commandInfo = new CommandInfo(entry.CommandType, entry.ApiEndpoint);
                dynMethod.Invoke(CommandInfoRepository.Instance, new object[] { entry.Command, commandInfo });
            }
        }

        #endregion Private Methods

        #region Private Class
        /// <summary>
        /// Container class for the command tuple
        /// </summary>
        private class _Commands
        {
            /// <summary>
            /// command type 
            /// </summary>
            internal readonly string CommandType;

            /// <summary>
            /// Command 
            /// </summary>
            internal readonly string Command;

            /// <summary>
            /// API Endpoint
            /// </summary>
            internal readonly string ApiEndpoint;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="commandType">type of command (get/post/delete)</param>
            /// <param name="command">Command</param>
            /// <param name="apiEndpoint">api endpoint</param>
            internal _Commands(string commandType, string command, string apiEndpoint)
            {
                CommandType = commandType;
                Command = command;
                ApiEndpoint = apiEndpoint;
            }
        }
        #endregion Private Class
    }
}
