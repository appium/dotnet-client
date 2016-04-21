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
    public sealed class IOSOptionList
    {
        private static void CheckArgumentAndThrowException(string argument, string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("The argument " + argument + " requires not empty value");
            }
        }

        ///<summary>
        /// absolute path to compiled .ipa file
        /// Sample:<br/>
        /// --ipa /abs/path/to/my.ipa
        ///</summary>
        public static KeyValuePair<string, string> Ipa(string value)
        {
            string argument = "--ipa";
            CheckArgumentAndThrowException(argument, value);
            return new KeyValuePair<string, string>(argument, value);
        }

        ///<summary>
        /// How many times to retry launching Instruments before saying it
        /// crashed or timed out<br/>
        /// Sample:<br/>
        /// --backend-retries 3
        ///</summary>
        public static KeyValuePair<string, string> BackEndRetries(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return new KeyValuePair<string, string>("--backend-retries", "3");
            }
            else
            {
                return new KeyValuePair<string, string>("--backend-retries", value);
            }
        }

        ///<summary>
        /// Use the safari app<br/>
        ///</summary>
        public static KeyValuePair<string, string> Safari()
        {
            return new KeyValuePair<string, string>("--safari", string.Empty);
        }

        ///<summary>
        /// use the default simulator that instruments launches
        /// on its own<br/>
        ///</summary>
        public static KeyValuePair<string, string> DefaultDevice()
        {
            return new KeyValuePair<string, string>("--default-device", string.Empty);
        }

        ///<summary>
        /// Use the iPhone Simulator no matter what the app wants<br/>
        ///</summary>
        public static KeyValuePair<string, string> ForceIPhoneSimulator()
        {
            return new KeyValuePair<string, string>("--force-iphone", string.Empty);
        }

        ///<summary>
        /// Use the iPad Simulator no matter what the app wants<br/>
        ///</summary>
        public static KeyValuePair<string, string> ForceIPadSimulator()
        {
            return new KeyValuePair<string, string>("--force-ipad", string.Empty);
        }

        ///<summary>
        /// .tracetemplate file to use with Instruments<br/>
        /// Sample:<br/>
        /// --tracetemplate /Users/me/Automation.tracetemplate
        ///</summary>
        public static KeyValuePair<string, string> TraceTemplate(string value)
        {
            string argument = "--tracetemplate";
            CheckArgumentAndThrowException(argument, value);
            return new KeyValuePair<string, string>(argument, value);
        }


        ///<summary>
        /// custom path to the instruments commandline tool<br/>
        /// Sample:<br/>
        /// --instruments /path/to/instruments
        ///</summary>
        public static KeyValuePair<string, string> Intstruments(string value)
        {
            string argument = "--instruments";
            CheckArgumentAndThrowException(argument, value);
            return new KeyValuePair<string, string>(argument, value);
        }


        ///<summary>
        /// Xcode 6 has a bug on some platforms where a certain simulator can only be
        /// launched without error if all other simulator devices are first deleted.
        /// This option causes Appium to delete all devices other than the one being
        /// used by Appium. Note that this is a permanent deletion, and you are
        /// responsible for using simctl or xcode to manage the categories of devices
        /// used with Appium<br/>.
        ///</summary>
        public static KeyValuePair<string, string> IsolateSimDevice()
        {
            return new KeyValuePair<string, string>("--isolate-sim-device", string.Empty);
        }


        ///<summary>
        /// Absolute path to directory Appium use to save ios instruments traces,
        /// defaults to /appium-instruments<br/>
        ///</summary>
        public static KeyValuePair<string, string> TraceDirectory(string value)
        {
            string argument = "--trace-dir";
            CheckArgumentAndThrowException(argument, value);
            return new KeyValuePair<string, string>(argument, value);
        }
    }
}
