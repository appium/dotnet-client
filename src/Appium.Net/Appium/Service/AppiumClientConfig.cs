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

namespace OpenQA.Selenium.Appium.Service
{

    public class AppiumClientConfig
    {
        /// <summary>
        /// Return the default Appium Client Config
        /// </summary>
        /// <returns>An AppiumClientConfig instance</returns>
        public static AppiumClientConfig DefaultConfig()
        {
            return new AppiumClientConfig();
        }

        /// <summary>
        /// Gets or sets the directConnect feature availability.
        /// If this flag is true and the target server supports
        /// https://appiumpro.com/editions/86-connecting-directly-to-appium-hosts-in-distributed-environments,
        /// the AppiumCommandExecutor will follow the response directConnect direction.
        ///
        ///   AppiumClientConfig clientConfig = AppiumClientConfig.DefaultConfig();
        ///   clientConfig.DirectConnect = true;
        ///
        /// </summary>
        public bool DirectConnect { get; set; }
    }
}
