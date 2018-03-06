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
using OpenQA.Selenium.Remote;

namespace OpenQA.Selenium.Appium.Tizen
{
    public class TizenElement : AppiumWebElement
    {
        /// <summary>
        /// Initializes a new instance of the TizenElement class.
        /// </summary>
        /// <param name="parent">Driver in use.</param>
        /// <param name="id">ID of the element.</param>
        public TizenElement(RemoteWebDriver parent, string id)
            : base(parent, id)
        {
        }

        public void ReplaceValue(string value) => TizenCommandExecutionHelper.ReplaceValue(this, Id, value);

        public void SetAttribute(string name, string value) => TizenCommandExecutionHelper.SetAttribute(this, Id, name, value);
    }
}
