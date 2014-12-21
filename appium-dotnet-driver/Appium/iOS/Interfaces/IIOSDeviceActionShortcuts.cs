using OpenQA.Selenium.Appium.Appium.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.Appium.iOS.Interfaces
{
    public interface IIOSDeviceActionShortcuts : IDeviceActionShortcuts
    {
        void HideKeyboard(string key, string strategy = null);

        /// <summary>
        /// Shakes the device.
        /// </summary>
        void ShakeDevice();
    }
}
