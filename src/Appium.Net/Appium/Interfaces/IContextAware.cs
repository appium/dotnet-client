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
    /// <summary>
    /// Some implementations of WebDriver, notably those that support native testing, need the ability
    /// to switch between the native and web-based contexts. This can be achieved by using this
    /// interface.
    /// </summary>
    public interface IContextAware
    {
        /// <summary>
        /// Switches the focus of future commands for this driver to the context with the given name
        /// AND
        /// returns an opaque handle to this context that uniquely identifies it within this driver
        /// instance.
        /// </summary>
        string Context { get; set; }

        /// <summary>
        /// Return a list of context handles which can be used to iterate over all contexts of this
        /// WebDriver instance
        /// </summary>       
        ReadOnlyCollection<string> Contexts { get; }
    }
}