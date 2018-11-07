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

namespace OpenQA.Selenium.Appium
{
    /// <summary>
    /// Extension methods to convert to/from ScreenOrientation enum
    /// </summary>
    public static class ScreenOrientationExtensions
    {
        /// <summary>
        /// Converts the Screen Orientation to the string needed by the JSON Wire Protocol
        /// </summary>
        /// <param name="orientation"></param>
        /// <returns></returns>
        public static string JSONWireProtocolString(this ScreenOrientation orientation) =>
            orientation.ToString().ToUpper();

        /// <summary>
        /// Converts the string to a screen orientation if possible, else throws ArgumentOutOfRangeException 
        /// </summary>
        /// <param name="orientation"></param>
        /// <returns>Screen Orientation</returns>
        public static ScreenOrientation ConvertToScreenOrientation(this string orientation)
        {
            ScreenOrientation retVal = ScreenOrientation.Landscape;
            switch (orientation)
            {
                case "PORTRAIT":
                    retVal = ScreenOrientation.Portrait;
                    break;
                case "LANDSCAPE":
                    retVal = ScreenOrientation.Landscape;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unknown Orientation Type Passed in");
            }
            return retVal;
        }
    }
}