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

using Appium.Interfaces.Generic.SearchContext;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
    public abstract class AppiumWebElement : RemoteWebElement, 
        IMobileElement<AppiumWebElement>
    {
        /// <summary>
        /// Initializes a new instance of the AppiumWebElement class.
        /// </summary>
        /// <param name="parent">Driver in use.</param>
        /// <param name="id">ID of the element.</param>
        public AppiumWebElement(RemoteWebDriver parent, string id)
            : base(parent, id)
        {
        }


        #region MJSonMethods

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
            foreach (KeyValuePair<string, int> opt in opts)
            {
                parameters.Add(opt.Key, opt.Value);
            }
            parameters.Add("element", this.Id);
            this.Execute(AppiumDriverCommand.Rotate, parameters);
        }

        /// <summary>
        /// Sets Immediate Value.
        /// </summary>
        /// <param name="value">the value</param>
        public void SetImmediateValue(string value)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("value", value);
            parameters.Add("id", this.Id);
            this.Execute(AppiumDriverCommand.SetImmediateValue, parameters);
        }

        #endregion

        #region FindMethods

        #region IFindByAccessibilityId Members

        public AppiumWebElement FindElementByAccessibilityId(string selector)
        {
            return (AppiumWebElement)this.FindElement("accessibility id", selector);
        }

        public ReadOnlyCollection<AppiumWebElement> FindElementsByAccessibilityId(string selector)
        {
            return CollectionConverterUnility.
                            ConvertToExtendedWebElementCollection<AppiumWebElement>(this.FindElements("accessibility id", selector));
        }

        #endregion IFindByAccessibilityId Members

        /// <summary>
        /// Finds the first element in the page that matches the OpenQA.Selenium.By object 
        /// </summary>
        /// <param name="by">Mechanism to find element</param>
        /// <returns>first element found</returns>
        public new AppiumWebElement FindElement(By by)
        {
            return (AppiumWebElement)base.FindElement(by);
        }

        /// <summary>
        /// Find the elements on the page by using the <see cref="T:OpenQA.Selenium.By"/> object and returns a ReadonlyCollection of the Elements on the page 
        /// </summary>
        /// <param name="by">Mechanism to find element</param>
        /// <returns>ReadOnlyCollection of elements found</returns
        public new ReadOnlyCollection<AppiumWebElement> FindElements(By by)
        {
            return CollectionConverterUnility.
                ConvertToExtendedWebElementCollection<AppiumWebElement>(base.FindElements(by));
        }

        /// <summary>
        /// Finds the first element in the page that matches the class name supplied
        /// </summary>
        /// <param name="className">CSS class name on the element</param>
        /// <returns>first element found</returns
        public new AppiumWebElement FindElementByClassName(string className)
        {
            return (AppiumWebElement)base.FindElementByClassName(className);
        }

        /// <summary>
        /// Finds a list of elements that match the class name supplied
        /// </summary>
        /// <param name="className">CSS class name on the element</param>
        /// <returns>ReadOnlyCollection of elements found</returns
        public new ReadOnlyCollection<AppiumWebElement> FindElementsByClassName(string className)
        {
            return CollectionConverterUnility.
                ConvertToExtendedWebElementCollection<AppiumWebElement>(base.FindElementsByClassName(className));
        }

        /// <summary>
        /// Finds the first element in the page that matches the ID supplied
        /// </summary>
        /// <param name="id">ID of the element</param>
        /// <returns>First element found</returns>
        public new AppiumWebElement FindElementById(string id)
        {
            return (AppiumWebElement)base.FindElementById(id);
        }

        /// <summary>
        /// Finds a list of elements that match the ID supplied
        /// </summary>
        /// <param name="id">ID of the element</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public new ReadOnlyCollection<AppiumWebElement> FindElementsById(string id)
        {
            return CollectionConverterUnility.
                            ConvertToExtendedWebElementCollection<AppiumWebElement>(base.FindElementsById(id));
        }

        /// <summary>
        /// Finds the first element matching the specified CSS selector
        /// </summary>
        /// <param name="cssSelector">The CSS selector to match</param>
        /// <returns>First element found</returns>
        public new AppiumWebElement FindElementByCssSelector(string cssSelector)
        {
            return (AppiumWebElement)base.FindElementByCssSelector(cssSelector);
        }

        /// <summary>
        /// Finds a list of elements that match the CSS selector
        /// </summary>
        /// <param name="cssSelector">The CSS selector to match</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public new ReadOnlyCollection<AppiumWebElement> FindElementsByCssSelector(string cssSelector)
        {
            return CollectionConverterUnility.
                            ConvertToExtendedWebElementCollection<AppiumWebElement>(base.FindElementsByCssSelector(cssSelector));
        }

        /// <summary>
        /// Finds the first of elements that match the link text supplied
        /// </summary>
        /// <param name="linkText">Link text of element</param>
        /// <returns>First element found</returns>
        public new AppiumWebElement FindElementByLinkText(string linkText)
        {
            return (AppiumWebElement)base.FindElementByLinkText(linkText);
        }

        /// <summary>
        /// Finds a list of elements that match the link text supplied
        /// </summary>
        /// <param name="linkText">Link text of element</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public new ReadOnlyCollection<AppiumWebElement> FindElementsByLinkText(string linkText)
        {
            return CollectionConverterUnility.
                            ConvertToExtendedWebElementCollection<AppiumWebElement>(base.FindElementsByLinkText(linkText));
        }

        /// <summary>
        /// Finds the first of elements that match the name supplied
        /// </summary>
        /// <param name="name">Name of the element on the page</param>
        /// <returns>First element found</returns>
        public new AppiumWebElement FindElementByName(string name)
        {
            return (AppiumWebElement)base.FindElementByName(name);
        }

        /// <summary>
        /// Finds a list of elements that match the name supplied
        /// </summary>
        /// <param name="name">Name of the element on the page</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public new ReadOnlyCollection<AppiumWebElement> FindElementsByName(string name)
        {
            return CollectionConverterUnility.
                            ConvertToExtendedWebElementCollection<AppiumWebElement>(base.FindElementsByName(name));
        }

        /// <summary>
        /// Finds the first of elements that match the part of the link text supplied
        /// </summary>
        /// <param name="partialLinkText">Part of the link text</param>
        /// <returns>First element found</returns>
        public new AppiumWebElement FindElementByPartialLinkText(string partialLinkText)
        {
            return (AppiumWebElement)base.FindElementByPartialLinkText(partialLinkText);
        }

        /// <summary>
        /// Finds a list of elements that match the part of the link text supplied
        /// </summary>
        /// <param name="partialLinkText">Part of the link text</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public new ReadOnlyCollection<AppiumWebElement> FindElementsByPartialLinkText(string partialLinkText)
        {
            return CollectionConverterUnility.
                            ConvertToExtendedWebElementCollection<AppiumWebElement>(base.FindElementsByPartialLinkText(partialLinkText));
        }

        /// <summary>
        /// Finds the first of elements that match the DOM Tag supplied
        /// </summary>
        /// <param name="tagName">DOM tag name of the element being searched</param>
        /// <returns>First element found</returns>
        public new AppiumWebElement FindElementByTagName(string tagName)
        {
            return (AppiumWebElement)base.FindElementByTagName(tagName);
        }

        /// <summary>
        /// Finds a list of elements that match the DOM Tag supplied
        /// </summary>
        /// <param name="tagName">DOM tag name of the element being searched</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public new ReadOnlyCollection<AppiumWebElement> FindElementsByTagName(string tagName)
        {
            return CollectionConverterUnility.
                            ConvertToExtendedWebElementCollection<AppiumWebElement>(base.FindElementsByTagName(tagName));
        }

        /// <summary>
        /// Finds the first of elements that match the XPath supplied
        /// </summary>
        /// <param name="xpath">xpath to the element</param>
        /// <returns>First element found</returns>
        public new AppiumWebElement FindElementByXPath(string xpath)
        {
            return (AppiumWebElement)base.FindElementByXPath(xpath);
        }

        /// <summary>
        /// Finds a list of elements that match the XPath supplied
        /// </summary>
        /// <param name="xpath">xpath to the element</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public new ReadOnlyCollection<AppiumWebElement> FindElementsByXPath(string xpath)
        {
            return CollectionConverterUnility.
                            ConvertToExtendedWebElementCollection<AppiumWebElement>(base.FindElementsByXPath(xpath));
        }
        #endregion

		#region Touch actions

		/// <summary>
		/// Convenience method for pinching the given element.
		/// "pinching" refers to the action of two appendages pressing the screen and sliding towards each other.
		/// NOTE:
		/// This convenience method places the initial touches around the element, if this would happen to place one of them
		/// off the screen, appium with return an outOfBounds error. In this case, revert to using the IMultiAction api
		/// instead of this method.
		/// </summary>
		public void Pinch()
		{
			((ITouchShortcuts) WrappedDriver).Pinch(this);
		}

		/// <summary>
		/// Convenience method for tapping the center of the given element
		/// </summary>
		/// <param name="fingers"> number of fingers/appendages to tap with </param>
		/// <param name="duration">how long between pressing down, and lifting fingers/appendages</param>
		public void Tap(int fingers, int duration) 
		{
			((ITouchShortcuts) WrappedDriver).Tap(fingers, this, duration);
		}

		/// <summary>
		/// Convenience method for "zooming in" on the given element.
		/// "zooming in" refers to the action of two appendages pressing the screen and sliding away from each other.
		/// NOTE:
		/// This convenience method slides touches away from the element, if this would happen to place one of them
		/// off the screen, appium will return an outOfBounds error. In this case, revert to using the IMultiAction api
		/// instead of this method.
		/// </summary>
		public void Zoom()
		{
			((ITouchShortcuts) WrappedDriver).Zoom(this);
		}

		#endregion
    }
}
