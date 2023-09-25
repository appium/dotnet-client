﻿//Licensed under the Apache License, Version 2.0 (the "License");
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
using System;
using System.Collections.Generic;

namespace OpenQA.Selenium.Appium.Tizen
{
    public sealed class TizenCommandExecutionHelper : AppiumCommandExecutionHelper
    {
        [Obsolete("The ReplaceValue method is deprecated and will be removed in future versions. Please use the following command extensions: 'mobile: replaceElementValue' instead \r\n See https://github.com/appium/appium-uiautomator2-driver#mobile-replaceelementvalue")]
        public static void ReplaceValue(IExecuteMethod executeMethod, string elementId, string value) =>
            executeMethod.Execute(AppiumDriverCommand.ReplaceValue,
                new Dictionary<string, object>()
                { ["id"] = elementId, ["value"] = new string[] { value } });

        public static void SetAttribute(IExecuteMethod executeMethod, string elementId, string name, string value)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            string setAttributeScript = "this.setAttribute('" + name + "','" + value + "'," + elementId + " );";
            parameters.Add("script", setAttributeScript);
            parameters.Add("args", elementId);
            executeMethod.Execute(DriverCommand.ExecuteScript, parameters);
        }
    }
}
