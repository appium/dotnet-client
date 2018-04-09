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

using System.Collections.Generic;

namespace OpenQA.Selenium.Appium.Interfaces 
{
    /// <summary>
    /// This interface extends IWebElement and adds caching support.
    /// To enable caching at the driver level, set the shouldUseCompactResponses capability:
    ///    shouldUseCompactResponses: true
    /// To specify the attributes to be cached, set the elementResponseAttributes capability:
    ///    elementResponseAttributes: "name,text,rect,attribute/name,attribute/value"
    /// Note: the cache uses W3C names for attributes. 
    ///       For TagName, use "name"
    ///       For Size, Location use "rect"
    /// </summary>
    public interface IWebElementCached : IWebElement 
    {
        /// <summary>
        /// Replace any existing values in the cache with the supplied values. 
        /// The cache is enabled for this element.
        /// <param name="cacheValues">The new cache values</param>
        /// </summary>
        void SetCacheValues(Dictionary<string, object> cacheValues);

        /// <summary>
        /// Disable the cache for this element.
        /// </summary>
        void DisableCache();

        /// <summary>
        /// Clear all values from the cache.
        /// </summary>
        void ClearCache();
    }
}
