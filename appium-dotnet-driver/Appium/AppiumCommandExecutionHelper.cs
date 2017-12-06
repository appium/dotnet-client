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

namespace OpenQA.Selenium.Appium
{
    public class AppiumCommandExecutionHelper
    {
        public static void PressKeyCode(IExecuteMethod executeMethod, int keyCode, int metastate = -1)
        {
            var parameters = new Dictionary<string, object>()
                {["keycode"] = keyCode};
            if (metastate > 0)
            {
                parameters.Add("metastate", metastate);
            }
            executeMethod.Execute(AppiumDriverCommand.PressKeyCode, parameters);
        }

        public static void LongPressKeyCode(IExecuteMethod executeMethod, int keyCode, int metastate = -1)
        {
            var parameters = new Dictionary<string, object>()
                {["keycode"] = keyCode};
            if (metastate > 0)
            {
                parameters.Add("metastate", metastate);
            }
            executeMethod.Execute(AppiumDriverCommand.LongPressKeyCode, parameters);
        }

        public static void HideKeyboard(IExecuteMethod executeMethod, string strategy = null, string key = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            if (strategy != null)
            {
                parameters.Add("strategy", strategy);
            }
            if (key != null)
            {
                parameters.Add("keyName", key);
            }
            executeMethod.Execute(AppiumDriverCommand.HideKeyboard, parameters);
        }

        public static void Lock(IExecuteMethod executeMethod, int seconds) =>
            executeMethod.Execute(AppiumDriverCommand.LockDevice,
                new Dictionary<string, object>()
                    {["seconds"] = seconds});
    }
}