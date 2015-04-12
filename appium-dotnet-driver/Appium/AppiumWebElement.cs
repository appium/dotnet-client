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
    public abstract class AppiumWebElement : RemoteWebElement, IFindByAccessibilityId<AppiumWebElement>, IGenericSearchContext<AppiumWebElement>,
        IGenericFindsByClassName<AppiumWebElement>,
        IGenericFindsById<AppiumWebElement>, IGenericFindsByCssSelector<AppiumWebElement>, IGenericFindsByLinkText<AppiumWebElement>, 
        IGenericFindsByName<AppiumWebElement>,
        IGenericFindsByPartialLinkText<AppiumWebElement>, IGenericFindsByTagName<AppiumWebElement>, IGenericFindsByXPath<AppiumWebElement>
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

        #region IFindByAccessibilityId Members

        public AppiumWebElement FindElementByAccessibilityId(string selector)
        {
            return (AppiumWebElement) this.FindElement("accessibility id", selector);
        }

        public ReadOnlyCollection<AppiumWebElement> FindElementsByAccessibilityId(string selector)
        {
            return CollectionConverterUnility.
                            ConvertToExtendedWebElementCollection<AppiumWebElement>(this.FindElements("accessibility id", selector));
        }

        #endregion IFindByAccessibilityId Members

        public AppiumWebElement FindElement(By by)
        {
            return (AppiumWebElement) base.FindElement(by);
        }

        public ReadOnlyCollection<AppiumWebElement> FindElements(By by)
        {
            return CollectionConverterUnility.
                ConvertToExtendedWebElementCollection<AppiumWebElement>(base.FindElements(by));
        }

        public AppiumWebElement FindElementByClassName(string className)
        {
            return (AppiumWebElement) base.FindElementByClassName(className);
        }

        public ReadOnlyCollection<AppiumWebElement> FindElementsByClassName(string className)
        {
            return CollectionConverterUnility.
                ConvertToExtendedWebElementCollection<AppiumWebElement>(base.FindElementsByClassName(className));
        }

        public AppiumWebElement FindElementById(string id)
        {
            return (AppiumWebElement) base.FindElementById(id);
        }


        public ReadOnlyCollection<AppiumWebElement> FindElementsById(string id)
        {
            return CollectionConverterUnility.
                            ConvertToExtendedWebElementCollection<AppiumWebElement>(base.FindElementsById(id));
        }

        public AppiumWebElement FindElementByCssSelector(string cssSelector)
        {
            return (AppiumWebElement) base.FindElementByCssSelector(cssSelector);
        }


        public ReadOnlyCollection<AppiumWebElement> FindElementsByCssSelector(string cssSelector)
        {
            return CollectionConverterUnility.
                            ConvertToExtendedWebElementCollection<AppiumWebElement>(base.FindElementsByCssSelector(cssSelector));
        }

        public AppiumWebElement FindElementByLinkText(string linkText)
        {
            return (AppiumWebElement) base.FindElementByLinkText(linkText);
        }

        public ReadOnlyCollection<AppiumWebElement> FindElementsByLinkText(string linkText)
        {
            return CollectionConverterUnility.
                            ConvertToExtendedWebElementCollection<AppiumWebElement>(base.FindElementsByLinkText(linkText));
        }

        public AppiumWebElement FindElementByName(string name)
        {
            return (AppiumWebElement) base.FindElementByName(name);
        }

        public ReadOnlyCollection<AppiumWebElement> FindElementsByName(string name)
        {
            return CollectionConverterUnility.
                            ConvertToExtendedWebElementCollection<AppiumWebElement>(base.FindElementsByName(name));
        }

        public AppiumWebElement FindElementByPartialLinkText(string partialLinkText)
        {
            return (AppiumWebElement) base.FindElementByPartialLinkText(partialLinkText);
        }

        public ReadOnlyCollection<AppiumWebElement> FindElementsByPartialLinkText(string partialLinkText)
        {
            return CollectionConverterUnility.
                            ConvertToExtendedWebElementCollection<AppiumWebElement>(base.FindElementsByPartialLinkText(partialLinkText));
        }

        public AppiumWebElement FindElementByTagName(string tagName)
        {
            return (AppiumWebElement) base.FindElementByTagName(tagName);
        }

        public ReadOnlyCollection<AppiumWebElement> FindElementsByTagName(string tagName)
        {
            return CollectionConverterUnility.
                            ConvertToExtendedWebElementCollection<AppiumWebElement>(base.FindElementsByTagName(tagName));
        }

        public AppiumWebElement FindElementByXPath(string xpath)
        {
            return (AppiumWebElement)base.FindElementByXPath(xpath);
        }

        public ReadOnlyCollection<AppiumWebElement> FindElementsByXPath(string xpath)
        {
            return CollectionConverterUnility.
                            ConvertToExtendedWebElementCollection<AppiumWebElement>(base.FindElementsByXPath(xpath));
        }



        #endregion
    }
}
    