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

namespace OpenQA.Selenium.Appium.iOS
{
    public class IOSElement : AppiumWebElement,
        IFindByIosUIAutomation<AppiumWebElement>
    {
        /// <summary>
        /// Initializes a new instance of the IOSElement class.
        /// </summary>
        /// <param name="parent">Driver in use.</param>
        /// <param name="id">ID of the element.</param>
        public IOSElement(WebDriver parent, string id)
            : base(parent, id)
        {
        }


        #region IFindByIosUIAutomation Members

        public AppiumWebElement FindElementByIosUIAutomation(string selector) =>
            FindElement(MobileSelector.iOSAutomatoion, selector);

        public IReadOnlyCollection<AppiumWebElement> FindElementsByIosUIAutomation(string selector) =>
            FindElements(MobileSelector.iOSAutomatoion, selector);

        #endregion IFindByIosUIAutomation Members
    }
}