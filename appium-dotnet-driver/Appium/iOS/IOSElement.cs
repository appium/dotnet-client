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

namespace OpenQA.Selenium.Appium.iOS
{
    public class IOSElement : AppiumWebElement,
        IFindByIosUIAutomation<AppiumWebElement>,
        IScrollsTo<AppiumWebElement>
    {
        /// <summary>
        /// Initializes a new instance of the IOSElement class.
        /// </summary>
        /// <param name="parent">Driver in use.</param>
        /// <param name="id">ID of the element.</param>
        public IOSElement(RemoteWebDriver parent, string id)
            : base(parent, id)
        {
        }


        #region IFindByIosUIAutomation Members

        public AppiumWebElement FindElementByIosUIAutomation(string selector)
        {
            return (AppiumWebElement)this.FindElement("-ios uiautomation", selector);
        }

        public ReadOnlyCollection<AppiumWebElement> FindElementsByIosUIAutomation(string selector)
        {
            return CollectionConverterUnility.
                            ConvertToExtendedWebElementCollection<AppiumWebElement>(this.FindElements("-ios uiautomation", selector));
        }
        #endregion IFindByIosUIAutomation Members

        #region IScrollsTo Members

        /// <summary>
        /// Scroll to the element whose 'text' attribute contains the input text.
        /// Scrolling happens within this element
        /// </summary>
        /// <param name="text">input text contained in text attribute</param>
        public AppiumWebElement ScrollTo(string text)
        {
            return (IOSElement)FindElementByIosUIAutomation(".scrollToElementWithPredicate(\"name CONTAINS '" + text + "'\")");
        }

        /// <summary>
        /// Scroll to the element whose 'text' attribute matches the input text.
        /// Scrolling happens within this element
        /// </summary>
        /// <param name="text">input text contained in text attribute</param>
        public AppiumWebElement ScrollToExact(string text)
        {
            return (IOSElement)FindElementByIosUIAutomation(".scrollToElementWithName(\"" + text + "\")");
        }

        #endregion

        public void SetValue(string value)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("id", Id);
            parameters.Add("value", value);
            this.Execute(AppiumDriverCommand.SetValue, parameters);
        }
    }
}
