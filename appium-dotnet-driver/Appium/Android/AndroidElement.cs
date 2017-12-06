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

using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenQA.Selenium.Appium.Android
{
    public class AndroidElement : AppiumWebElement, IFindByAndroidUIAutomator<AppiumWebElement>
    {
        /// <summary>
        /// Initializes a new instance of the AndroidElement class.
        /// </summary>
        /// <param name="parent">Driver in use.</param>
        /// <param name="id">ID of the element.</param>
        public AndroidElement(RemoteWebDriver parent, string id)
            : base(parent, id)
        {
        }

        #region IFindByAndroidUIAutomator Members

        public AppiumWebElement FindElementByAndroidUIAutomator(string selector) =>
            FindElement(MobileSelector.AndroidUIAutomator, selector);

        public ReadOnlyCollection<AppiumWebElement> FindElementsByAndroidUIAutomator(string selector) =>
            FindElements(MobileSelector.AndroidUIAutomator, selector);

        #endregion IFindByAndroidUIAutomator Members

        public void ReplaceValue(string value) => AndroidCommandExecutionHelper.ReplaceValue(this, Id, value);
    }
}