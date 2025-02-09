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

using OpenQA.Selenium.Appium.Enums;

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
        /// and Android &lt;= 21 does not support granting permissions.
        /// </summary>
        void ToggleData();

        /// <summary>
        /// Switch the state of the WiFi service
        /// </summary>
        void ToggleWifi();

        /// <summary>
        /// Switch the state of the location service
        /// </summary>
        void ToggleLocationServices();

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

        /// <summary>
        /// Sets GSM signal strength (Emulator only)
        /// </summary>
        /// <param name="gsmSignalStrength"></param>
        void SetGsmSignalStrength(GsmSignalStrength gsmSignalStrength);

        /// <summary>
        /// Set GSM voice state (Emulator only)
        /// </summary>
        /// <param name="gsmVoiceState"></param>
        void SetGsmVoice(GsmVoiceState gsmVoiceState);
    }
}

