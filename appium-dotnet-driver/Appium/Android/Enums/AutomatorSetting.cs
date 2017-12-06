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

namespace OpenQA.Selenium.Appium.Android.Enums
{
    public sealed class AutomatorSetting
    {
        public static readonly string IgnoreUnimportantViews = "ignoreUnimportantViews";
        public static readonly string WaitForIDLETimeout = "setWaitForIdleTimeout";
        public static readonly string WaitForSelectorTimeout = "setWaitForSelectorTimeout";
        public static readonly string WaitScrollAcknowledgmentTimeout = "setScrollAcknowledgmentTimeout";
        public static readonly string WaitActionAcknowledgmentTimeout = "setActionAcknowledgmentTimeout";
        public static readonly string KeyInjectionDelay = "setKeyInjectionDelay";
    }
}