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

using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using System.Collections.Generic;

namespace OpenQA.Selenium.Appium.Android
{
    public class AndroidElement : AppiumWebElement, IFindByAndroidUIAutomator<AppiumWebElement>, IFindByAndroidDataMatcher<AppiumWebElement>
    {
        /// <summary>
        /// Initializes a new instance of the AndroidElement class.
        /// </summary>
        /// <param name="parent">Driver in use.</param>
        /// <param name="id">ID of the element.</param>
        public AndroidElement(WebDriver parent, string id)
            : base(parent, id)
        {
        }

        #region IFindByAndroidUIAutomator Members

        public AppiumWebElement FindElementByAndroidUIAutomator(string selector) =>
            FindElement(MobileSelector.AndroidUIAutomator, selector);

        public AppiumWebElement FindElementByAndroidUIAutomator(IUiAutomatorStatementBuilder selector) =>
            FindElement(MobileSelector.AndroidUIAutomator, selector.Build());

        public IReadOnlyCollection<AppiumWebElement> FindElementsByAndroidUIAutomator(string selector) =>
            FindElements(MobileSelector.AndroidUIAutomator, selector);

        public IReadOnlyCollection<AppiumWebElement> FindElementsByAndroidUIAutomator(IUiAutomatorStatementBuilder selector) => 
            FindElements(MobileSelector.AndroidUIAutomator, selector.Build());

        #endregion IFindByAndroidUIAutomator Members

        #region IFindByAndroidDataMatcher Members

        public AppiumWebElement FindElementByAndroidDataMatcher(string selector) =>
            FindElement(MobileSelector.AndroidDataMatcher, selector);

        public IReadOnlyCollection<AppiumWebElement> FindElementsByAndroidDataMatcher(string selector) =>
            FindElements(MobileSelector.AndroidDataMatcher, selector);

        #endregion IFindByAndroidDataMatcher Members

        #region IFindByAndroidViewMatcher Members

        public AppiumWebElement FindElementByAndroidViewMatcher(string selector) =>
            FindElement(MobileSelector.AndroidViewMatcher, selector);

        public IReadOnlyCollection<AppiumWebElement> FindElementsByAndroidViewMatcher(string selector) =>
            FindElements(MobileSelector.AndroidViewMatcher, selector);

        #endregion IFindByAndroidViewMatcher Members

        public void ReplaceValue(string value) => AndroidCommandExecutionHelper.ReplaceValue(this, Id, value);
    }
}