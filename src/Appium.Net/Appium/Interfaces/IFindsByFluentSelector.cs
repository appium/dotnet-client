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

using System.Collections.ObjectModel;

namespace OpenQA.Selenium.Appium.Interfaces
{
    public interface IFindsByFluentSelector<W> where W : IWebElement
    {
        /// <summary>
        /// Method performs the searching for a single element by some selector defined by string
        /// and value of the given selector
        /// </summary>
        /// <param name="by">is a string selector</param>
        /// <param name="value">is a value of the given selector</param>
        /// <returns>the first found element</returns>
        W FindElement(string by, string value);

        /// <summary>
        /// Method performs the searching for a list of elements by some selector defined by string
        /// and value of the given selector
        /// </summary>
        /// <param name="by">is a string selector</param>
        /// <param name="value">is a value of the given selector</param>
        /// <returns>a list of elements</returns>
        ReadOnlyCollection<W> FindElements(string selector, string value);
    }
}