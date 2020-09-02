using System;

namespace OpenQA.Selenium.Appium.Interfaces
{
    public interface INetworkActions
    {
        /// <summary>
        /// Toggle airplane mode on device
        /// </summary>
        void ToggleAirplaneMode();

        /// <summary>
        /// Switch the state of the WiFi service.
        /// Since Android Q, this method to change the WiFi service state has been restricted.
        /// Please toggle the state via UI instead of this method.
        /// The UI flow depends on devices.
        /// </summary>
        void ToggleWifi();
    }
}
