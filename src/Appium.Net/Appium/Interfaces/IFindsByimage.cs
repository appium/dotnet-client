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

using System.Collections.Generic;
using OpenQA.Selenium.Appium.Android.Interfaces;

namespace OpenQA.Selenium.Appium.Interfaces
{
    public interface IFindsByImage<out W> : IFindsByFluentSelector<W> where W : IWebElement
    {
        /// <summary>
        /// Performs the lookup for a single element by matching its image template
        /// to the current full screen shot. This type of locator requires OpenCV libraries
        /// and bindings for NodeJS to be installed on the server machine.
        /// Lookup options fine-tuning might be done via <see cref="IHasSettings.SetSetting"/>. (Supported since Appium 1.8.2)
        /// </summary>
        /// <param name="base64Template">base64-encoded template image string.
        /// Supported image formats are the same as for OpenCV library.
        /// </param>
        /// <see href="https://github.com/appium/appium/blob/master/docs/en/writing-running-appium/image-comparison.md"> For the documentation on Image Comparison Features</see>
        /// <see href="https://github.com/appium/appium-base-driver/blob/master/lib/basedriver/device-settings.js">For the settings available for lookup fine-tuning</see>
        /// <returns>The first element that matches the given selector</returns>
        W FindElementByImage(string base64Template);

        /// <summary>
        /// Performs the lookup for a list of elements by matching its image template
        /// to the current full screen shot. This type of locator requires OpenCV libraries
        /// and bindings for NodeJS to be installed on the server machine.
        /// Lookup options fine-tuning might be done via <see cref="IHasSettings.SetSetting"/>. (Supported since Appium 1.8.2)
        /// </summary>
        /// <param name="base64Template">base64-encoded template image string.
        /// Supported image formats are the same as for OpenCV library.
        /// </param>
        /// <see href="https://github.com/appium/appium/blob/master/docs/en/writing-running-appium/image-comparison.md"> For the documentation on Image Comparison Features</see>
        /// <see href="https://github.com/appium/appium-base-driver/blob/master/lib/basedriver/device-settings.js">For the settings available for lookup fine-tuning</see>
        /// <returns>A list of elements that match the given selector or an empty list</returns>
        IReadOnlyCollection<W> FindElementsByImage(string base64Template);
    }
}
