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

namespace OpenQA.Selenium.Appium.Android
{
    public interface IStartsActivity : IExecuteMethod

    {
        /// <summary>
        /// Opens an arbitrary activity during a test. If the activity belongs to
        /// another application, that application is started and the activity is opened.
        /// 
        /// </summary>
        /// <param name="appPackage">The package containing the activity to start.</param>
        /// <param name="appActivity">The activity to start.</param>
        /// <param name="appWaitPackage">Begin automation after this package starts. Can be null or empty.</param>
        /// <param name="appWaitActivity">Begin automation after this activity starts. Can be null or empty.</param>
        /// <param name="stopApp">If true, target app will be stopped.</param>
        void StartActivity(string appPackage, string appActivity, string appWaitPackage = "",
            string appWaitActivity = "", bool stopApp = true);

        /// <summary>
        /// Opens an arbitrary activity during a test. If the activity belongs to
        /// another application, that application is started and the activity is opened.
        /// 
        /// </summary>
        /// <param name="appPackage">The package containing the activity to start.</param>
        /// <param name="appActivity">The activity to start.</param>
        /// <param name="intentAction">Intent action which will be used to start activity.</param>
        /// <param name="appWaitPackage">Begin automation after this package starts. Can be null or empty.</param>
        /// <param name="appWaitActivity">Begin automation after this activity starts. Can be null or empty.</param>
        /// <param name="intentCategory">Intent category which will be used to start activity.</param>
        /// <param name="intentFlags">Flags that will be used to start activity.</param>
        /// <param name="intentOptionalArgs">Additional intent arguments that will be used to start activity.</param>
        /// <param name="stopApp">If true, target app will be stopped.</param>
        void StartActivityWithIntent(string appPackage, string appActivity, string intentAction,
            string appWaitPackage = "", string appWaitActivity = "",
            string intentCategory = "", string intentFlags = "", string intentOptionalArgs = "", bool stopApp = true);

        /// <summary>
        /// Gets Current Device Activity.
        /// </summary>
        /// 
        string CurrentActivity { get; }
    }
}