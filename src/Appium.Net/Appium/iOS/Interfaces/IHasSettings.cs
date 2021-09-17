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

using OpenQA.Selenium.Appium.Interfaces;
using System.Collections.Generic;

namespace OpenQA.Selenium.Appium.iOS.Interfaces
{
    public interface IHasSettings : IExecuteMethod
    {
        /// <summary>
        /// Set a setting for this test session It's probably better to use a
        /// convenience function, rather than use this function directly. Try finding
        /// the method for the specific setting you want to change.
        /// </summary>
        /// <param name="setting">Setting you wish to set.</param>
        /// <param name="value">value of the setting.</param>
        void SetSetting(string setting, object value);

        /// <summary>
        /// Gets/Sets settings stored for this test session.
        /// </summary>
        Dictionary<string, object> Settings { set; get; }
    }
}