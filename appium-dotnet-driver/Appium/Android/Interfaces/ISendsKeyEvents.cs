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

namespace OpenQA.Selenium.Appium.Android.Interfaces
{
    public interface ISendsKeyEvents
    {
        /// <summary>
        /// Triggers device key event with metastate for the keypress
        /// </summary>
        /// <param name="connectionType"></param>
        [Obsolete("This method is obsolete and it is going to be removed soon. Please use PressKeyCode or LongPressKeyCode instead")]
        void KeyEvent(int keyCode, int metastate);

        /// <summary>
        /// Triggers device key event
        /// </summary>
        /// <param name="keyCode">Code for the long key pressed on the Android device</param>
        [Obsolete("This method is obsolete and it is going to be removed soon. Please use PressKeyCode or LongPressKeyCode instead")]
        void KeyEvent(int keyCode);

        /// <summary>
        /// Sends a device key event with metastate
        /// </summary>
        /// <param name="keyCode">Code for the long key pressed on the Android device</param>
        /// <param name="metastate">metastate for the long key press</param>
        void PressKeyCode(int keyCode, int metastate = -1);

        /// <summary>
        /// Sends a device long key event with metastate
        /// </summary>
        /// <param name="keyCode">Code for the long key pressed on the Android device</param>
        /// <param name="metastate">metastate for the long key press</param>
        void LongPressKeyCode(int keyCode, int metastate = -1);
    }
}
