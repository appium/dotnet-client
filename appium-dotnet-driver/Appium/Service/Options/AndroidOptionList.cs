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
    ///<summary>
    /// Here is the list of Android specific server arguments.
    /// All flags are optional, but some are required in conjunction with certain others.
    /// The full list is available here: http://appium.io/slate/en/master/?ruby#appium-server-arguments
    /// Android specific arguments are marked by (Android-only)
    /// </summary>
    public sealed class AndroidOptionList : BaseOptionList
    {
        ///<summary>
        /// Port to use on device to talk to Appium<br/>
        /// Sample:<br/>
        /// --bootstrap-port 4724
        ///</summary>
        public static KeyValuePair<string, string> BootstrapPort(string value) =>
            GetKeyValuePairUsingDefaultValue("--bootstrap-port", value, "4724");


        ///<summary>
        /// Local port used for communication with Selendroid<br/>
        /// Sample:<br/>
        /// --selendroid-port 8080
        ///</summary>
        public static KeyValuePair<string, string> SelendroidPort(string value) =>
            GetKeyValuePairUsingDefaultValue("--selendroid-port", value, "8080");

        ///<summary>
        /// If set, prevents Appium from killing the adb server
        /// instance<br/>
        ///</summary>
        public static KeyValuePair<string, string> SuppressAdbKillServer() =>
            new KeyValuePair<string, string>("--suppress-adb-kill-server", string.Empty);

        ///<summary>
        /// Port upon which ChromeDriver will run<br/>
        /// Sample:<br/>
        /// --chromedriver-port 9515
        ///</summary>
        public static KeyValuePair<string, string> ChromeDriverPort(string value) =>
            GetKeyValuePairUsingDefaultValue("--chromedriver-port", value, "9515");

        ///<summary>
        /// ChromeDriver executable full path
        ///</summary>
        public static KeyValuePair<string, string> ChromeDriverExecutable(string value) =>
            GetKeyValuePair("--chromedriver-executable", value);
    }
}