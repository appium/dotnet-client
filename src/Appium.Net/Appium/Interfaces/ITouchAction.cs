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

namespace OpenQA.Selenium.Appium.Interfaces
{
    public interface ITouchAction
    {
        /// <summary>
        /// Press at the specified location in the element until the  context menu appears.
        /// </summary>
        /// <param name="element">The target element.</param>
        /// <param name="x">The x coordinate relative to the element.</param>
        /// <param name="y">The y coordinate relative to the element.</param>
        /// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
        ITouchAction LongPress(IWebElement el, double? x = null, double? y = null);

        /// <summary>
        /// Press at the specified location until the  context menu appears.
        /// </summary>
        /// <param name="element">The target element.</param>
        /// <param name="x">The x coordinate relative to the element.</param>
        /// <param name="y">The y coordinate relative to the element.</param>
        /// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
        ITouchAction LongPress(double x, double y);

        /// <summary>
        /// Move to the specified location in the element.
        /// </summary>
        /// <param name="element">The target element.</param>
        /// <param name="x">The x coordinate relative to the element.</param>
        /// <param name="y">The y coordinate relative to the element.</param>
        /// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
        ITouchAction MoveTo(IWebElement element, double? x = null, double? y = null);

        /// <summary>
        /// Move to the specified location.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
        ITouchAction MoveTo(double x, double y);

        /// <summary>
        /// Press at the specified location in the element.
        /// </summary>
        /// <param name="element">The target element.</param>
        /// <param name="x">The x coordinate relative to the element.</param>
        /// <param name="y">The y coordinate relative to the element.</param>
        /// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
        ITouchAction Press(IWebElement element, double? x = null, double? y = null);

        /// <summary>
        /// Press at the specified location.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
        ITouchAction Press(double x, double y);

        /// <summary>
        /// Release the pressure.
        /// </summary>
        /// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
        ITouchAction Release();

        /// <summary>
        /// Tap at the specified location in the element.
        /// </summary>
        /// <param name="element">The target element.</param>
        /// <param name="x">The x coordinate relative to the element.</param>
        /// <param name="y">The y coordinate relative to the element.</param>
        /// <param name="count">The number of times to tap.</param>
        /// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
        ITouchAction Tap(IWebElement element, double? x = null, double? y = null, long? count = null);

        /// <summary>
        /// Tap at the specified location.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="count">The number of times to tap.</param>
        /// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
        ITouchAction Tap(double x, double y, long? count = null);

        /// <summary>
        /// Wait for the given duration.
        /// </summary>
        /// <param name="ms">The amount of time to wait in milliseconds.</param>
        /// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
        ITouchAction Wait(long? ms = null);

        /// <summary>
        /// Cancels the Multi Action
        /// </summary>
        void Cancel();

        /// <summary>
        /// Performs the Multi Action
        /// </summary>
        void Perform();

        List<Dictionary<string, object>> GetParameters();
    }
}