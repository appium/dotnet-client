using System;
using OpenQA.Selenium.Appium.Enums;

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

        /// <summary>
        /// Make GSM call (Emulator only)
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="gsmCallAction"></param>
        void MakeGsmCall(string phoneNumber, GsmCallActions gsmCallAction);

        /// <summary>
        /// Simulate an SMS message (Emulator only)
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="message"></param>
        void SendSms(string phoneNumber, string message);
    }
}
