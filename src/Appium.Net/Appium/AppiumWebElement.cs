﻿//Licensed under the Apache License, Version 2.0 (the "License");
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

using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Internal;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using OpenQA.Selenium.Appium.Enums;
using System;
using System.Drawing;
using Appium.Interfaces.Generic.SearchContext;

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
    public abstract class AppiumWebElement : WebElement,
            IMobileElement<AppiumWebElement>, IWebElementCached,
            IFindsById, IFindsByClassName, IFindsByName, IFindsByTagName
    {
        /// <summary>
        /// Initializes a new instance of the AppiumWebElement class.
        /// </summary>
        /// <param name="parent">Driver in use.</param>
        /// <param name="id">ID of the element.</param>
        public AppiumWebElement(WebDriver parent, string id)
            : base(parent, id)
        {
        }

        #region Cache 

        protected Dictionary<string, object> cache = null;

        public virtual void SetCacheValues(Dictionary<string, object> cacheValues)
        {
            cache = new Dictionary<string, object>(cacheValues);
        }

        public virtual void ClearCache()
        {
            if (cache != null)
            {
                cache.Clear();
            }
        }

        public virtual void DisableCache()
        {
            cache = null;
        }

        public override string TagName => CacheValue("name", () => base.TagName)?.ToString();

        public override string Text => CacheValue("text", () => base.Text)?.ToString();

        public override bool Displayed => Convert.ToBoolean(CacheValue("displayed", () => Execute(DriverCommand.IsElementDisplayed, new Dictionary<string, object> { { "id", Id } }).Value));

        public override bool Enabled => Convert.ToBoolean(CacheValue("enabled", () => base.Enabled));

        public override bool Selected => Convert.ToBoolean(CacheValue("selected", () => base.Selected));

        public override Point Location => cache == null ? base.Location : Rect.Location;

        public override Size Size => cache == null ? base.Size : Rect.Size;

        public virtual Rectangle Rect
        {
            get
            {
                Dictionary<string, object> rect = null;
                object value;
                if (cache != null && cache.TryGetValue("rect", out value))
                {
                    rect = value as Dictionary<string, object>;
                }
                if (rect == null)
                {
                    Point location = base.Location;
                    Size size = base.Size;
                    rect = new Dictionary<string, object> {
                        {"x", location.X },
                        {"y", location.Y },
                        {"width", size.Width },
                        {"height", size.Height },
                    };
                    if (cache != null)
                    {
                        cache["rect"] = rect;
                    }
                }
                return new Rectangle(
                    Convert.ToInt32(rect["x"]),
                    Convert.ToInt32(rect["y"]),
                    Convert.ToInt32(rect["width"]),
                    Convert.ToInt32(rect["height"]));
            }
        }

        public override string GetAttribute(string attributeName) => CacheValue(
                "attribute/" + attributeName,
                () => _GetAttribute(attributeName)
            )?.ToString();

        private string _GetAttribute(string attributeName)
        {
            Response commandResponse = null;
            string attributeValue = string.Empty;
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("id", Id);
            parameters.Add("name", attributeName);
            commandResponse = Execute(DriverCommand.GetElementAttribute, parameters);

            if (commandResponse.Value == null)
            {
                return null;
            }

            attributeValue = commandResponse.Value.ToString();

            // Normalize string values of boolean results as lowercase.
            if (commandResponse.Value is bool)
            {
                attributeValue = attributeValue.ToLowerInvariant();
            }

            return attributeValue;
        }

        public override string GetCssValue(string propertyName) => CacheValue(
                "css/" + propertyName,
                () => base.GetCssValue(propertyName)
            )?.ToString();

        public override string GetProperty(string propertyName) => CacheValue(
                "property/" + propertyName,
                () => base.GetProperty(propertyName)
            )?.ToString();

        protected virtual object CacheValue(string key, Func<object> getter)
        {
            if (cache == null)
            {
                return getter();
            }
            object value;
            if (!cache.TryGetValue(key, out value))
            {
                value = getter();
                cache.Add(key, value);
            }
            return value;
        }

        #endregion

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
            parameters.Add("element", Id);
            Execute(AppiumDriverCommand.Rotate, parameters);
        }

        #endregion

        #region FindMethods

        #region Overrides to fix "css selector" issue

        IWebElement IFindsByClassName.FindElementByClassName(string className) =>
            base.FindElement(MobileSelector.ClassName, className);

        ReadOnlyCollection<IWebElement> IFindsByClassName.FindElementsByClassName(string className) =>
            base.FindElements(MobileSelector.ClassName, className);

        IWebElement IFindsById.FindElementById(string id) =>
            base.FindElement(MobileSelector.Id, id);

        ReadOnlyCollection<IWebElement> IFindsById.FindElementsById(string id) =>
            base.FindElements(MobileSelector.Id, id);

        IWebElement IFindsByName.FindElementByName(string name) =>
            base.FindElement(MobileSelector.Name, name);

        ReadOnlyCollection<IWebElement> IFindsByName.FindElementsByName(string name) =>
            base.FindElements(MobileSelector.Name, name);

        IWebElement IFindsByTagName.FindElementByTagName(string tagName) =>
            base.FindElement(MobileSelector.TagName, tagName);

        ReadOnlyCollection<IWebElement> IFindsByTagName.FindElementsByTagName(string tagName) =>
            base.FindElements(MobileSelector.TagName, tagName);

        #endregion Overrides to fix "css selector" issue


        #region IFindByAccessibilityId Members

        public AppiumWebElement FindElementByAccessibilityId(string selector) =>
            FindElement(MobileSelector.Accessibility, selector);

        public IReadOnlyCollection<AppiumWebElement> FindElementsByAccessibilityId(string selector) =>
            ConvertToExtendedWebElementCollection(FindElements(MobileSelector.Accessibility, selector));

        #endregion IFindByAccessibilityId Members

        /// <summary>
        /// Finds the first element in the page that matches the OpenQA.Selenium.By object 
        /// </summary>
        /// <param name="by">Mechanism to find element</param>
        /// <returns>first element found</returns>
        public new AppiumWebElement FindElement(By by) => (AppiumWebElement)base.FindElement(by);

        /// <summary>
        /// Find the elements on the page by using the <see cref="T:OpenQA.Selenium.By"/> object and returns a ReadonlyCollection of the Elements on the page 
        /// </summary>
        /// <param name="by">Mechanism to find element</param>
        /// <returns>ReadOnlyCollection of elements found</returns
        public new ReadOnlyCollection<AppiumWebElement> FindElements(By by) =>
            ConvertToExtendedWebElementCollection(base.FindElements(by));

        public new AppiumWebElement FindElement(string by, string value) =>
            (AppiumWebElement)base.FindElement(by, value);

        public new IReadOnlyCollection<AppiumWebElement> FindElements(string selector, string value) =>
            ConvertToExtendedWebElementCollection(base.FindElements(selector, value));

        /// <summary>
        /// Finds the first element in the page that matches the class name supplied
        /// </summary>
        /// <param name="className">CSS class name on the element</param>
        /// <returns>first element found</returns
        public AppiumWebElement FindElementByClassName(string className) =>
            (AppiumWebElement)base.FindElement(MobileSelector.ClassName, className);

        /// <summary>
        /// Finds a list of elements that match the class name supplied
        /// </summary>
        /// <param name="className">CSS class name on the element</param>
        /// <returns>ReadOnlyCollection of elements found</returns
        public ReadOnlyCollection<AppiumWebElement> FindElementsByClassName(string className) =>
            ConvertToExtendedWebElementCollection(base.FindElements(MobileSelector.ClassName, className));

        /// <summary>
        /// Finds the first element in the page that matches the ID supplied
        /// </summary>
        /// <param name="id">ID of the element</param>
        /// <returns>First element found</returns>
        public AppiumWebElement FindElementById(string id) =>
            (AppiumWebElement)base.FindElement(MobileSelector.Id, id);

        /// <summary>
        /// Finds a list of elements that match the ID supplied
        /// </summary>
        /// <param name="id">ID of the element</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public ReadOnlyCollection<AppiumWebElement> FindElementsById(string id) =>
            ConvertToExtendedWebElementCollection(base.FindElements(MobileSelector.Id, id));

        /// <summary>
        /// Finds the first element matching the specified CSS selector
        /// </summary>
        /// <param name="cssSelector">The CSS selector to match</param>
        /// <returns>First element found</returns>
        public AppiumWebElement FindElementByCssSelector(string cssSelector) =>
            (AppiumWebElement)base.FindElement(By.CssSelector(cssSelector));

        /// <summary>
        /// Finds a list of elements that match the CSS selector
        /// </summary>
        /// <param name="cssSelector">The CSS selector to match</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public ReadOnlyCollection<AppiumWebElement> FindElementsByCssSelector(string cssSelector) =>
            ConvertToExtendedWebElementCollection(base.FindElements(By.CssSelector(cssSelector)));

        /// <summary>
        /// Finds the first of elements that match the link text supplied
        /// </summary>
        /// <param name="linkText">Link text of element</param>
        /// <returns>First element found</returns>
        public AppiumWebElement FindElementByLinkText(string linkText) =>
            (AppiumWebElement)base.FindElement(By.LinkText(linkText));

        /// <summary>
        /// Finds a list of elements that match the link text supplied
        /// </summary>
        /// <param name="linkText">Link text of element</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public ReadOnlyCollection<AppiumWebElement> FindElementsByLinkText(string linkText) =>
            ConvertToExtendedWebElementCollection(base.FindElements(By.LinkText(linkText)));

        /// <summary>
        /// Finds the first of elements that match the name supplied
        /// </summary>
        /// <param name="name">Name of the element on the page</param>
        /// <returns>First element found</returns>
        public AppiumWebElement FindElementByName(string name) =>
            (AppiumWebElement)base.FindElement(MobileSelector.Name, name);

        /// <summary>
        /// Finds a list of elements that match the name supplied
        /// </summary>
        /// <param name="name">Name of the element on the page</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public ReadOnlyCollection<AppiumWebElement> FindElementsByName(string name) =>
            ConvertToExtendedWebElementCollection(base.FindElements(MobileSelector.Name, name));

        /// <summary>
        /// Finds the first of elements that match the part of the link text supplied
        /// </summary>
        /// <param name="partialLinkText">Part of the link text</param>
        /// <returns>First element found</returns>
        public AppiumWebElement FindElementByPartialLinkText(string partialLinkText) =>
            (AppiumWebElement)base.FindElement(By.PartialLinkText(partialLinkText));

        /// <summary>
        /// Finds a list of elements that match the part of the link text supplied
        /// </summary>
        /// <param name="partialLinkText">Part of the link text</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public ReadOnlyCollection<AppiumWebElement> FindElementsByPartialLinkText(string partialLinkText) =>
            ConvertToExtendedWebElementCollection(base.FindElements(By.PartialLinkText(partialLinkText)));

        /// <summary>
        /// Finds the first of elements that match the DOM Tag supplied
        /// </summary>
        /// <param name="tagName">DOM tag name of the element being searched</param>
        /// <returns>First element found</returns>
        public AppiumWebElement FindElementByTagName(string tagName) =>
            (AppiumWebElement)base.FindElement(MobileSelector.TagName, tagName);

        /// <summary>
        /// Finds a list of elements that match the DOM Tag supplied
        /// </summary>
        /// <param name="tagName">DOM tag name of the element being searched</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public ReadOnlyCollection<AppiumWebElement> FindElementsByTagName(string tagName) =>
            ConvertToExtendedWebElementCollection(FindElements(MobileSelector.TagName, tagName));

        /// <summary>
        /// Finds the first of elements that match the XPath supplied
        /// </summary>
        /// <param name="xpath">xpath to the element</param>
        /// <returns>First element found</returns>
        public AppiumWebElement FindElementByXPath(string xpath) =>
            (AppiumWebElement)base.FindElement(By.XPath(xpath));

        /// <summary>
        /// Finds a list of elements that match the XPath supplied
        /// </summary>
        /// <param name="xpath">xpath to the element</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public ReadOnlyCollection<AppiumWebElement> FindElementsByXPath(string xpath) =>
            ConvertToExtendedWebElementCollection(base.FindElements(By.XPath(xpath)));

        #endregion

        public void SetImmediateValue(string value) => Execute(AppiumDriverCommand.SetValue,
            new Dictionary<string, object>() { ["id"] = Id, ["value"] = value });

        private ReadOnlyCollection<AppiumWebElement> ConvertToExtendedWebElementCollection(IEnumerable list)
        {
            List<AppiumWebElement> result = new List<AppiumWebElement>();
            foreach (var element in list)
            {
                result.Add((AppiumWebElement)element);
            }
            return result.AsReadOnly();
        }

        public new Response Execute(string commandName, Dictionary<string, object> parameters) =>
            base.Execute(commandName, parameters);

        public Response Execute(string driverCommand) => Execute(driverCommand, null);

        public new string Id => base.Id;
    }
}