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

using System.Text;
using OpenQA.Selenium.Appium.Android.Enums;
using OpenQA.Selenium.Appium.Interfaces;

namespace OpenQA.Selenium.Appium.Android.UiAutomator
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Docs: https://developer.android.com/reference/android/support/test/uiautomator/UiScrollable
    /// </remarks>
    public class AndroidUiScrollable : IUiAutomatorStatementBuilder
    {
        private readonly StringBuilder _builder;

        /// <summary>
        /// Creates a new scrollable searcher which will match the first scrollable widget
        /// in the view.
        /// </summary>
        public AndroidUiScrollable()
            : this(new AndroidUiSelector().IsScrollable(true))
        {
        }

        /// <summary>
        /// Creates a new scrollable searcher which will match the first widget
        /// which matches the given UISelector.
        /// </summary>
        /// <param name="uiSelector"></param>
        public AndroidUiScrollable(AndroidUiSelector uiSelector)
        {
            _builder = new StringBuilder().AppendFormat("new UiScrollable({0})", uiSelector.Build());
        }

        /// <summary>
        /// Sets the scrolling direction of the list.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public AndroidUiScrollable SetScrollDirection(ListDirection direction)
        {
            _builder.AppendFormat(".setAs{0}List()", direction);
            return this;
        }

        /// <summary>
        /// Perform a scroll forward action to move through the scrollable layout element until a visible item that matches the selector is found.
        /// Maps to the UiScrollable.scrollIntoView(UiSelector) method.
        /// </summary>
        /// <param name="uiSelector"></param>
        /// <remarks>https://developer.android.com/reference/android/support/test/uiautomator/UiScrollable#scrollintoview</remarks>
        public AndroidUiScrollable ScrollIntoView(AndroidUiSelector uiSelector)
        {
            _builder.AppendFormat(".scrollIntoView({0})", uiSelector.Build());
            return this;
        }

        /// <summary>
        /// Append raw text to this <see cref="AndroidUiScrollable"/> instance. The target language is Java.
        /// Text entered here will not be checked for validity. Use this at your own risk.
        /// </summary>
        /// <param name="text">
        /// Text to be appended to the UiScrollable command builder.
        /// </param>
        public AndroidUiScrollable AddRawText(string text)
        {
            _builder.Append(text);
            return this;
        }

        public string Build()
        {
            return Compile(false);
        }

        /// <summary>
        /// Compiles the current UiScrollable statements into a valid Java string which can be executed.
        /// </summary>
        /// <param name="terminateStatement">
        /// Should the statement be returned with a semicolon terminator at the end. Defaults to false.
        /// The terminator is only appended to the returned statement string - this <see cref="AndroidUiScrollable"/> is not affected.
        /// </param>
        public string Compile(bool terminateStatement)
        {
            if (terminateStatement)
                return _builder + ";";

            return _builder.ToString();
        }
    }
}
