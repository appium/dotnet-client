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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Internal;
using System.Reflection;

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
			: base(remoteAddress, desiredCapabilities, commandTimeout)
		{
			// Add the custom commandInfo of PhantomJSDriver
			CommandInfo commandInfo = new CommandInfo(CommandInfo.PostCommand, "/session/{sessionId}/appium/device/shake");
			MethodInfo dynMethod = typeof(CommandInfoRepository).GetMethod("TryAddAdditionalCommand", BindingFlags.NonPublic | BindingFlags.Instance);
			dynMethod.Invoke(CommandInfoRepository.Instance, new object[] { MJsonCommand.ShakeDevice, commandInfo });
		}

		/// <summary>
		/// Shakes the device.
		/// </summary>
		public void ShakeDevice()
		{
			this.Execute(MJsonCommand.ShakeDevice, null);
		}
	}
}
