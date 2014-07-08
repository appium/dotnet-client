using System;

namespace OpenQA.Selenium.Appium.Appium.Enums
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

    public static class ConnectionTypeExtensions
    {
        /// <summary>
        /// Converts an object type to a ConnectionType enum
        /// </summary>
        /// <param name="connectionType">connection type enum stored as an object type</param>
        /// <returns>Connection Type Enum</returns>
        public static ConnectionType ConvertToConnectionType(this object connectionType)
        {
            var x = Convert.ToInt32(connectionType);
            if (Enum.IsDefined(typeof(ConnectionType), x))
            {
                return (ConnectionType)Enum.ToObject(typeof(ConnectionType), x);
            }

            throw new InvalidCastException("Can not convert object to ConnectionType");
        }
    }
}
