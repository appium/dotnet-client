// <copyright file="AppiumWebElement.cs" company="WebDriver Committers">
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
using System.Collections.ObjectModel;
using System.Globalization;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Remote;

namespace OpenQA.Selenium.Appium
{
	/// <summary>
	/// AppiumWebElement allows you to have access to specific items that are found on the page.
	/// </summary>
	/// <seealso cref="IWebElement"/>
	/// <seealso cref="ILocatable"/>
	/// <example>
	/// <code>
	/// [Test]
	/// public void TestGoogle()
	/// {
	///     driver = new AppiumDriver();
	///     AppiumWebElement elem = driver.FindElement(By.Name("q"));
	///     elem.SendKeys("Cheese please!");
	/// }
	/// </code>
	/// </example>
	public class AppiumWebElement : RemoteWebElement
	{
		/// <summary>
		/// Initializes a new instance of the AppiumWebElement class.
		/// </summary>
		/// <param name="parent">Driver in use.</param>
		/// <param name="id">ID of the element.</param>
		public AppiumWebElement(AppiumDriver parent, string id)
			: base(parent, id)
		{
		}

		#region FindMethods
		/// <summary>
		/// Finds the first of elements that match the Ios UIAutomation selector supplied
		/// </summary>
		/// <param name="selector">an Ios UIAutomation selector</param>
		/// <returns>IWebElement object so that you can interact that object</returns>
		public IWebElement FindElementByIosUIAutomation(string selector)
		{
			return this.FindElement("-ios uiautomation", selector);
		}

		/// <summary>
		/// Finds a list of elements that match the Ios UIAutomation selector supplied
		/// </summary>
		/// <param name="selector">an Ios UIAutomation selector</param>
		/// <returns>IWebElement object so that you can interact that object</returns>
		public ReadOnlyCollection<IWebElement> FindElementsByIosUIAutomation(string selector)
		{
			return this.FindElements("-ios uiautomation", selector);
		}

		/// <summary>
		/// Finds the first of elements that match the Android UIAutomator selector supplied
		/// </summary>
		/// <param name="selector">an Android UIAutomator selector</param>
		/// <returns>IWebElement object so that you can interact that object</returns>
		public IWebElement FindElementByAndroidUIAutomator(string selector)
		{
			return this.FindElement("-android uiautomator", selector);
		}

		/// <summary>
		/// Finds a list of elements that match the Android UIAutomator selector supplied
		/// </summary>
		/// <param name="selector">an Android UIAutomator selector</param>
		/// <returns>IWebElement object so that you can interact that object</returns>
		public ReadOnlyCollection<IWebElement> FindElementsByAndroidUIAutomator(string selector)
		{
			return this.FindElements("-android uiautomator", selector);
		}
			
		/// <summary>
		/// Finds the first of elements that match the Accessibility Id selector supplied
		/// </summary>
		/// <param name="selector">an Accessibility Id selector</param>
		/// <returns>IWebElement object so that you can interact that object</returns>
		public IWebElement FindElementByAccessibilityId(string selector)
		{
			return this.FindElement("accessibility id", selector);
		}

		/// <summary>
		/// Finds a list of elements that match the Accessibility Id selector supplied
		/// </summary>
		/// <param name="selector">an Accessibility Id selector</param>
		/// <returns>IWebElement object so that you can interact that object</returns>
		public ReadOnlyCollection<IWebElement> FindElementsByAccessibilityId(string selector)
		{
			return this.FindElements("accessibility id", selector);
		}

		#endregion FindMethods

	}
}
	