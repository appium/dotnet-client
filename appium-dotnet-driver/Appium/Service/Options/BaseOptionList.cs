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

using System;
using System.Collections.Generic;


namespace OpenQA.Selenium.Appium.Service.Options
{
    public class BaseOptionList
    {
        protected static KeyValuePair<string, string> GetKeyValuePair(string argument, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"The argument {argument} requires not empty value");
            }

            return new KeyValuePair<string, string>(argument, value);
        }

        protected static KeyValuePair<string, string> GetKeyValuePairUsingDefaultValue(string argument, string value,
            string defaultValue)
        {
            if (string.IsNullOrEmpty(value))
            {
                return new KeyValuePair<string, string>(argument, defaultValue);
            }
            else
            {
                return new KeyValuePair<string, string>(argument, value);
            }
        }
    }
}