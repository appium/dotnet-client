using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.Interfaces
{
    public interface IDeviceActionShortcuts
    {
        /// <summary>
        /// Hides the device keyboard.
        /// </summary>
        /// <param name="keyName">The button pressed by the mobile driver to attempt hiding the keyboard.</param>
        void HideKeyboard();

        /// <summary>
        /// Triggers Device Key Event.
        /// </summary>
        /// <param name="keyCode">an integer keycode number corresponding to a java.awt.event.KeyEvent.</param>
        void KeyEvent(int keyCode);
    }
}
