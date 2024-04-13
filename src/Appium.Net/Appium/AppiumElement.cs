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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

namespace OpenQA.Selenium.Appium
{
    /// <summary>
    /// AppiumElement allows you to have access to specific items that are found on the page.
    /// </summary>
    /// <seealso cref="IWebElement"/>
    /// <seealso cref="ILocatable"/>
    /// <example>
    /// <code>
    /// [Test]
    /// public void TestGoogle()
    /// {
    ///     driver = new AppiumDriver();
    ///     AppiumElement elem = driver.FindElement(By.Name("q"));
    ///     elem.SendKeys("Cheese please!");
    /// }
    /// </code>
    /// </example>
    public class AppiumElement : WebElement, IFindsByFluentSelector<AppiumElement>, IWebElementCached
    {
        /// <summary>
        /// Initializes a new instance of the AppiumElement class.
        /// </summary>
        /// <param name="parent">Driver in use.</param>
        /// <param name="id">ID of the element.</param>
        public AppiumElement(WebDriver parent, string id)
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

        //TODO: Add Integrations tests 
        public string GetProperty(string propertyName) => CacheValue(
                "property/" + propertyName,
                () => base.GetDomProperty(propertyName)
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

        public new Response Execute(string commandName, Dictionary<string, object> parameters) =>
            base.Execute(commandName, parameters);

        public Response Execute(string driverCommand) => Execute(driverCommand, null);

        AppiumElement IFindsByFluentSelector<AppiumElement>.FindElement(string by, string value)
        {
            return (AppiumElement)base.FindElement(by, value);
        }

        IReadOnlyCollection<AppiumElement> IFindsByFluentSelector<AppiumElement>.FindElements(string selector, string value)
        {
            return ConvertToAppiumElementCollection(base.FindElements(selector, value));
        }

        /// <summary>
        /// Finds the first element that matches the specified <paramref name="by"/> selector and returns it as an <see cref="AppiumElement"/>.
        /// </summary>
        /// <param name="by">The <see cref="By"/> selector used to locate the element.</param>
        /// <returns>The first <see cref="AppiumElement"/> that matches the <paramref name="by"/> selector.</returns>
        public new AppiumElement FindElement(By by)
        {
            return (AppiumElement)base.FindElement(by);
        }

        /// <summary>
        /// Finds all elements that match the specified <paramref name="by"/> selector and returns them as a read-only collection of <see cref="AppiumElement"/>.
        /// </summary>
        /// <param name="by">The <see cref="By"/> selector used to locate the elements.</param>
        /// <returns>A read-only collection of <see cref="AppiumElement"/> objects matching the <paramref name="by"/> selector.</returns>
        public new ReadOnlyCollection<AppiumElement> FindElements(By by)
        {
            return ConvertToAppiumElementCollection(base.FindElements(by));
        }

        internal static ReadOnlyCollection<AppiumElement> ConvertToAppiumElementCollection(IEnumerable collection)
        {
            return collection.Cast<AppiumElement>().ToList().AsReadOnly();
        }

        public new string Id => base.Id;
    }
}