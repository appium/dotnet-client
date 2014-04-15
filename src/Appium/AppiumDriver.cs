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

        public void ToggleAirplaneMode()
        {
            this.Execute(AppiumDriverCommand.ToggleAirplaneMode, null);
        }

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
