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

using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using OpenQA.Selenium.Appium.Enums;

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
            parameters.Add("element", Id);
            Execute(AppiumDriverCommand.Rotate, parameters);
        }

        #endregion

        #region FindMethods

        #region IFindByAccessibilityId Members

        public AppiumWebElement FindElementByAccessibilityId(string selector) =>
            FindElement(MobileSelector.Accessibility, selector);

        public ReadOnlyCollection<AppiumWebElement> FindElementsByAccessibilityId(string selector) =>
            ConvertToExtendedWebElementCollection(FindElements(MobileSelector.Accessibility, selector));

        #endregion IFindByAccessibilityId Members

        /// <summary>
        /// Finds the first element in the page that matches the OpenQA.Selenium.By object 
        /// </summary>
        /// <param name="by">Mechanism to find element</param>
        /// <returns>first element found</returns>
        public new AppiumWebElement FindElement(By by) => (AppiumWebElement) base.FindElement(by);

        /// <summary>
        /// Find the elements on the page by using the <see cref="T:OpenQA.Selenium.By"/> object and returns a ReadonlyCollection of the Elements on the page 
        /// </summary>
        /// <param name="by">Mechanism to find element</param>
        /// <returns>ReadOnlyCollection of elements found</returns
        public new ReadOnlyCollection<AppiumWebElement> FindElements(By by) =>
            ConvertToExtendedWebElementCollection(base.FindElements(by));

        public new AppiumWebElement FindElement(string by, string value) =>
            (AppiumWebElement) base.FindElement(by, value);

        public new ReadOnlyCollection<AppiumWebElement> FindElements(string selector, string value) =>
            ConvertToExtendedWebElementCollection(base.FindElements(selector, value));

        /// <summary>
        /// Finds the first element in the page that matches the class name supplied
        /// </summary>
        /// <param name="className">CSS class name on the element</param>
        /// <returns>first element found</returns
        public new AppiumWebElement FindElementByClassName(string className) =>
            (AppiumWebElement) base.FindElementByClassName(className);

        /// <summary>
        /// Finds a list of elements that match the class name supplied
        /// </summary>
        /// <param name="className">CSS class name on the element</param>
        /// <returns>ReadOnlyCollection of elements found</returns
        public new ReadOnlyCollection<AppiumWebElement> FindElementsByClassName(string className) =>
            ConvertToExtendedWebElementCollection(base.FindElementsByClassName(className));

        /// <summary>
        /// Finds the first element in the page that matches the ID supplied
        /// </summary>
        /// <param name="id">ID of the element</param>
        /// <returns>First element found</returns>
        public new AppiumWebElement FindElementById(string id) =>
            (AppiumWebElement) base.FindElementById(id);

        /// <summary>
        /// Finds a list of elements that match the ID supplied
        /// </summary>
        /// <param name="id">ID of the element</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public new ReadOnlyCollection<AppiumWebElement> FindElementsById(string id) =>
            ConvertToExtendedWebElementCollection(base.FindElementsById(id));

        /// <summary>
        /// Finds the first element matching the specified CSS selector
        /// </summary>
        /// <param name="cssSelector">The CSS selector to match</param>
        /// <returns>First element found</returns>
        public new AppiumWebElement FindElementByCssSelector(string cssSelector) =>
            (AppiumWebElement) base.FindElementByCssSelector(cssSelector);

        /// <summary>
        /// Finds a list of elements that match the CSS selector
        /// </summary>
        /// <param name="cssSelector">The CSS selector to match</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public new ReadOnlyCollection<AppiumWebElement> FindElementsByCssSelector(string cssSelector) =>
            ConvertToExtendedWebElementCollection(base.FindElementsByCssSelector(cssSelector));

        /// <summary>
        /// Finds the first of elements that match the link text supplied
        /// </summary>
        /// <param name="linkText">Link text of element</param>
        /// <returns>First element found</returns>
        public new AppiumWebElement FindElementByLinkText(string linkText) =>
            (AppiumWebElement) base.FindElementByLinkText(linkText);

        /// <summary>
        /// Finds a list of elements that match the link text supplied
        /// </summary>
        /// <param name="linkText">Link text of element</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public new ReadOnlyCollection<AppiumWebElement> FindElementsByLinkText(string linkText) =>
            ConvertToExtendedWebElementCollection(base.FindElementsByLinkText(linkText));

        /// <summary>
        /// Finds the first of elements that match the name supplied
        /// </summary>
        /// <param name="name">Name of the element on the page</param>
        /// <returns>First element found</returns>
        public new AppiumWebElement FindElementByName(string name) =>
            (AppiumWebElement) base.FindElementByName(name);

        /// <summary>
        /// Finds a list of elements that match the name supplied
        /// </summary>
        /// <param name="name">Name of the element on the page</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public new ReadOnlyCollection<AppiumWebElement> FindElementsByName(string name) =>
            ConvertToExtendedWebElementCollection(base.FindElementsByName(name));

        /// <summary>
        /// Finds the first of elements that match the part of the link text supplied
        /// </summary>
        /// <param name="partialLinkText">Part of the link text</param>
        /// <returns>First element found</returns>
        public new AppiumWebElement FindElementByPartialLinkText(string partialLinkText) =>
            (AppiumWebElement) base.FindElementByPartialLinkText(partialLinkText);

        /// <summary>
        /// Finds a list of elements that match the part of the link text supplied
        /// </summary>
        /// <param name="partialLinkText">Part of the link text</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public new ReadOnlyCollection<AppiumWebElement> FindElementsByPartialLinkText(string partialLinkText) =>
            ConvertToExtendedWebElementCollection(base.FindElementsByPartialLinkText(partialLinkText));

        /// <summary>
        /// Finds the first of elements that match the DOM Tag supplied
        /// </summary>
        /// <param name="tagName">DOM tag name of the element being searched</param>
        /// <returns>First element found</returns>
        public new AppiumWebElement FindElementByTagName(string tagName) =>
            (AppiumWebElement) base.FindElementByTagName(tagName);

        /// <summary>
        /// Finds a list of elements that match the DOM Tag supplied
        /// </summary>
        /// <param name="tagName">DOM tag name of the element being searched</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public new ReadOnlyCollection<AppiumWebElement> FindElementsByTagName(string tagName) =>
            ConvertToExtendedWebElementCollection(base.FindElementsByTagName(tagName));

        /// <summary>
        /// Finds the first of elements that match the XPath supplied
        /// </summary>
        /// <param name="xpath">xpath to the element</param>
        /// <returns>First element found</returns>
        public new AppiumWebElement FindElementByXPath(string xpath) =>
            (AppiumWebElement) base.FindElementByXPath(xpath);

        /// <summary>
        /// Finds a list of elements that match the XPath supplied
        /// </summary>
        /// <param name="xpath">xpath to the element</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public new ReadOnlyCollection<AppiumWebElement> FindElementsByXPath(string xpath) =>
            ConvertToExtendedWebElementCollection(base.FindElementsByXPath(xpath));

        #endregion

        public void SetImmediateValue(string value) => Execute(AppiumDriverCommand.SetValue,
            new Dictionary<string, object>() {["id"] = Id, ["value"] = value});

        private ReadOnlyCollection<AppiumWebElement> ConvertToExtendedWebElementCollection(IList list)
        {
            List<AppiumWebElement> result = new List<AppiumWebElement>();
            foreach (var element in list)
            {
                result.Add((AppiumWebElement) element);
            }
            return result.AsReadOnly();
        }

        public new Response Execute(string commandName, Dictionary<string, object> parameters) =>
            base.Execute(commandName, parameters);

        public Response Execute(string driverCommand) => Execute(driverCommand, null);

        public new string Id
        {
            get { return base.Id; }
        }
    }
}