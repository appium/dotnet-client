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

namespace OpenQA.Selenium.Appium.Interfaces
{
    public interface IHidesKeyboardWithKeyName : IHidesKeyboard
    {
        /// <summary>
        /// Hides the keyboard if it is showing. Hiding the keyboard often
        /// depends on the way an app is implemented, no single strategy always
        /// works.
        /// </summary>
        /// <param name="key">a String, representing the text displayed on the button of the
        /// keyboard you want to press. For example: "Done".
        /// </param>
        /// <param name="strategy">HideKeyboardStrategy</param>
        void HideKeyboard(string key, string strategy = null);
    }
}