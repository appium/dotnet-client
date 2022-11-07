﻿/*
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * See the NOTICE file distributed with this work for additional
 * information regarding copyright ownership.
 * You may obtain a copy of the License at
 * 
 *    http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;

namespace OpenQA.Selenium.Appium.Interfaces
{
    /// <summary>
    /// Provides a mechanism for building advanced interactions with the browser/application.
    /// </summary>
    [Obsolete("Touch Actions are deprecated in W3C spec, please use W3C actions instead")]
    public interface IPerformsTouchActions
    {
        /// <summary>
        /// Performs the multi-action sequence.
        /// </summary>
        /// <param name="multiAction">Multi-action to perform.</param>
        void PerformMultiAction(IMultiAction multiAction);

        /// <summary>
        /// Perform the touch-action sequence.
        /// </summary>
        /// <param name="touchAction">touch action to perform</param>
        void PerformTouchAction(ITouchAction touchAction);
    }
}