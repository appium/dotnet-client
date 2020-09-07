using System;

namespace OpenQA.Selenium.Appium.Interfaces
{
    public interface INetworkActions
    {
        /// <summary>
        /// Toggle airplane mode on device.
        /// <remarks>Only supports below API 24 (Android 7) on emulators</remarks>
        /// </summary>
        void ToggleAirplaneMode();

        /// <summary>
        /// Switch the state of data service (Emulator Only)
        /// (For Android) This API does not work for Android API level 21+
        /// because it requires system or carrier privileged permission,
        /// and Android <= 21 does not support granting permissions.
        /// </summary>
        void ToggleData();

        /// <summary>
        /// Switch the state of the WiFi service.
        /// Since Android Q, this method to change the WiFi service state has been restricted.
        /// Please toggle the state via UI instead of this method.
        /// The UI flow depends on devices.
        /// </summary>
        void ToggleWifi();

        /// <summary>
        /// Switch the state of the location service
        /// </summary>
        void ToggleLocationServices();
    }
}