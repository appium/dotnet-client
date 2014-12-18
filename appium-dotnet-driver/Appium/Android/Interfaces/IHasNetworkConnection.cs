using OpenQA.Selenium.Appium.Appium.Android.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.Appium.Android.Interfaces
{
    interface IHasNetworkConnection
    {
        /// <summary>
        /// Get the Connection Type
        /// </summary>
        /// <returns>Connection Type of device</returns>
        /// <exception cref="System.InvalidCastException">Thrown when object return was not able to be converted to a ConnectionType Enum</exception>
        ConnectionType GetConnectionType();

        /// <summary>
        /// Set the connection type
        /// </summary>
        /// <param name="connectionType"></param>
        void SetConnectionType(ConnectionType connectionType);
    }
}
