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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenQA.Selenium.Appium
{
    internal class CollectionConverterUnility
    {
        public static ReadOnlyCollection<T> ConvertToExtendedWebElementCollection<T>(IList list) where T : IWebElement
        {
            List<T> result = new List<T>();
            foreach (var element in list)
            {
                result.Add((T) element);
            }
            return result.AsReadOnly();
        }
    }
}
