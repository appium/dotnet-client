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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Appium.Interfaces;
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
    public class AppiumWebElement : RemoteWebElement, IFindByAccessibilityId
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
            foreach(KeyValuePair<string, int> opt in opts){
                parameters.Add(opt.Key, opt.Value);
            }
            parameters.Add ("element", this.Id);
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
            parameters.Add ("id", this.Id);
            this.Execute(AppiumDriverCommand.SetImmediateValue, parameters);
        }

        #endregion

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

        #region IFindByAccessibilityId Members
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
        #endregion IFindByAccessibilityId Members

        #endregion

    }
}
    