using System;

namespace OpenQA.Selenium.Appium.Android
{
    /// <summary>
    /// Connection Type as described in the JSON Wire Protocol
    /// </summary>
    public enum ConnectionType
    {
        None = 0,
        AirplaneMode = 1,
        WifiOnly = 2,
        DataOnly = 4,
        AllNetworkOn = 6
    }
}
