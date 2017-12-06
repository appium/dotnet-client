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

using OpenQA.Selenium.Appium.PageObjects.Attributes.Abstract;
using System;

namespace OpenQA.Selenium.Appium.PageObjects.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = true)]
    public class FindsBySelendroidAttribute : FindsByMobileAttribute
    {
        /// <summary>
        /// Sets the target element link text.
        /// This locator is supported by Selendroid
        /// </summary>
        public string LinkText
        {
            set { byList.Add(By.LinkText(value)); }
            get { return null; }
        }

        /// <summary>
        /// Sets the target element partial link text
        /// This locator is supported by Selendroid
        /// </summary>
        public string PartialLinkText
        {
            set { byList.Add(By.PartialLinkText(value)); }
            get { return null; }
        }

        /// <summary>
        /// Sets the target element name
        /// </summary>
        public string Name
        {
            set { byList.Add(By.Name(value)); }
            get { return null; }
        }
    }
}