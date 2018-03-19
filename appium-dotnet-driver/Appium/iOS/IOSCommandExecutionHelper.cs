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

namespace OpenQA.Selenium.Appium.iOS
{
    public sealed class IOSCommandExecutionHelper
    {
        public static void ShakeDevice(IExecuteMethod executeMethod) =>
            executeMethod.Execute(AppiumDriverCommand.ShakeDevice);

        public static void PerformTouchID(IExecuteMethod executeMethod, bool match) =>
            executeMethod.Execute(AppiumDriverCommand.TouchID,
                new Dictionary<string, object> {["match"] = match});

        public static bool IsLocked(IExecuteMethod executeMethod) =>
            (bool)executeMethod.Execute(AppiumDriverCommand.IsLocked).Value;

        public static void Unlock(IExecuteMethod executeMethod) =>
            executeMethod.Execute(AppiumDriverCommand.UnlockDevice);

        public static void Lock(IExecuteMethod executeMethod) =>
            executeMethod.Execute(AppiumDriverCommand.LockDevice);

    }
}