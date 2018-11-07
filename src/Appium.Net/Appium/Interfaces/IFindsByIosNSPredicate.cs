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
    public interface IFindsByIosNSPredicate<W> : IFindsByFluentSelector<W> where W : IWebElement
    {
        /// <summary>
        /// Finds the first of elements that match the IosNsPredicate selector supplied
        /// </summary>
        /// <param name="selector">an IosNsPredicate selector</param>
        /// <returns>IWebElement object so that you can interact that object</returns>
        W FindElementByIosNsPredicate(string selector);

        /// <summary>
        /// Finds a list of elements that match the IosNsPredicate selector supplied
        /// </summary>
        /// <param name="selector">an IosNsPredicate selector</param>
        /// <returns>ReadOnlyCollection of IWebElement objects so that you can interact with those objects</returns>
        ReadOnlyCollection<W> FindElementsByIosNsPredicate(string selector);
    }
}