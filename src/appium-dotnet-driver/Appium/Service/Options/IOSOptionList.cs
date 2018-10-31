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
    /// Here is the list of iOS specific server arguments.
    /// All flags are optional, but some are required in conjunction with certain others.
    /// The full list is available here: http://appium.io/slate/en/master/?ruby#appium-server-arguments
    /// Android specific arguments are marked by (IOS-only)
    /// </summary>
    public sealed class IOSOptionList : BaseOptionList
    {
        ///<summary>
        /// absolute path to compiled .ipa file
        /// Sample:<br/>
        /// --ipa /abs/path/to/my.ipa
        ///</summary>
        public static KeyValuePair<string, string> Ipa(string value) =>
            GetKeyValuePair("--ipa", value);

        ///<summary>
        /// How many times to retry launching Instruments before saying it
        /// crashed or timed out<br/>
        /// Sample:<br/>
        /// --backend-retries 3
        ///</summary>
        public static KeyValuePair<string, string> BackEndRetries(string value) =>
            GetKeyValuePairUsingDefaultValue("--backend-retries", value, "3");

        ///<summary>
        /// Use the safari app<br/>
        ///</summary>
        public static KeyValuePair<string, string> Safari() =>
            new KeyValuePair<string, string>("--safari", string.Empty);

        ///<summary>
        /// use the default simulator that instruments launches
        /// on its own<br/>
        ///</summary>
        public static KeyValuePair<string, string> DefaultDevice() =>
            new KeyValuePair<string, string>("--default-device", string.Empty);

        ///<summary>
        /// Use the iPhone Simulator no matter what the app wants<br/>
        ///</summary>
        public static KeyValuePair<string, string> ForceIPhoneSimulator() =>
            new KeyValuePair<string, string>("--force-iphone", string.Empty);

        ///<summary>
        /// Use the iPad Simulator no matter what the app wants<br/>
        ///</summary>
        public static KeyValuePair<string, string> ForceIPadSimulator() =>
            new KeyValuePair<string, string>("--force-ipad", string.Empty);

        ///<summary>
        /// .tracetemplate file to use with Instruments<br/>
        /// Sample:<br/>
        /// --tracetemplate /Users/me/Automation.tracetemplate
        ///</summary>
        public static KeyValuePair<string, string> TraceTemplate(string value) =>
            GetKeyValuePair("--tracetemplate", value);


        ///<summary>
        /// custom path to the instruments commandline tool<br/>
        /// Sample:<br/>
        /// --instruments /path/to/instruments
        ///</summary>
        public static KeyValuePair<string, string> Intstruments(string value) =>
            GetKeyValuePair("--instruments", value);


        ///<summary>
        /// Xcode 6 has a bug on some platforms where a certain simulator can only be
        /// launched without error if all other simulator devices are first deleted.
        /// This option causes Appium to delete all devices other than the one being
        /// used by Appium. Note that this is a permanent deletion, and you are
        /// responsible for using simctl or xcode to manage the categories of devices
        /// used with Appium<br/>.
        ///</summary>
        public static KeyValuePair<string, string> IsolateSimDevice() =>
            new KeyValuePair<string, string>("--isolate-sim-device", string.Empty);


        ///<summary>
        /// Absolute path to directory Appium use to save ios instruments traces,
        /// defaults to /appium-instruments<br/>
        ///</summary>
        public static KeyValuePair<string, string> TraceDirectory(string value) =>
            GetKeyValuePair("--trace-dir", value);

        /// <summary>
        /// Local port used for communication with ios-webkit-debug-proxy.
        /// Sample:<br/>
        /// --webkit-debug-proxy-port 27753
        /// </summary>
        public static KeyValuePair<string, string> WebkitDebugProxyPort(string value) =>
            GetKeyValuePair("--webkit-debug-proxy-port", value);
    }
}