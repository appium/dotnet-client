using System;

// <copyright file="ByIosUIAutomation.cs" company="WebDriver Committers">
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

namespace OpenQA.Selenium.Appium
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Globalization;
	using System.Text;

	/// <summary>
	/// Finds element when the Ios UIAutomation selector has the specified value.
	/// </summary>
	public class ByIosUIAutomation : By
	{
		private string selector = string.Empty;

		/// <summary>
		/// Initializes a new instance of the <see cref="ByIosUIAutomation"/> class.
		/// </summary>
		/// <param name="elementIdentifier">The selector to use in finding the element.</param>
		public ByIosUIAutomation(string selector)
		{
			if (string.IsNullOrEmpty(selector))
			{
				throw new ArgumentException("selector identifier cannot be null or the empty string", "selector");
			}

			this.selector = selector;
		}

		/// <summary>
		/// Find a single element.
		/// </summary>
		/// <param name="context">Context used to find the element.</param>
		/// <returns>The element that matches</returns>
		public override IWebElement FindElement(ISearchContext context)
		{
			return ((AppiumDriver)context).FindElementByIosUIAutomation(selector);
		}

		/// <summary>
		/// Finds many elements
		/// </summary>
		/// <param name="context">Context used to find the element.</param>
		/// <returns>A readonly collection of elements that match.</returns>
		public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
		{
			return ((AppiumDriver)context).FindElementsByIosUIAutomation(selector);
		}

		/// <summary>
		/// Writes out a description of this By object.
		/// </summary>
		/// <returns>Converts the value of this instance to a <see cref="System.String"/></returns>
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "ByIosUIAutomation([{0}])", this.selector);
		}
	}
}
