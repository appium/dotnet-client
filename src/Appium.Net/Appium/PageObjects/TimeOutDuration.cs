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

namespace OpenQA.Selenium.Appium.PageObjects
{
    /// <summary>
    /// This class stores the time of the waiting for elements.
    /// It also allows to change this duration in runtime.
    /// </summary>
    public class TimeOutDuration
    {
        public TimeOutDuration(TimeSpan span)
        {
            WaitingDuration = span;
        }

        public TimeSpan WaitingDuration { set; get; }
    }
}