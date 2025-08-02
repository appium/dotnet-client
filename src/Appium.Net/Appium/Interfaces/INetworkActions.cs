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
        /// Switch the state of the location service
        /// </summary>
        void ToggleLocationServices();

        /// <summary>
        /// Make GSM call (Emulator only)
        /// </summary>
        /// <param name="phoneNumber">The string that represents phone number.</param>
        /// <param name="gsmCallAction">The <see cref="GsmCallActions"/> GSM call action.</param>
        void MakeGsmCall(string phoneNumber, GsmCallActions gsmCallAction);

        /// <summary>
        /// Simulate an SMS message (Emulator only)
        /// </summary>
        /// <param name="phoneNumber">The string that represents phone number. </param>
        /// <param name="message">The string that represents message. </param>
        void SendSms(string phoneNumber, string message);

        /// <summary>
        /// Sets GSM signal strength (Emulator only)
        /// </summary>
        /// <param name="gsmSignalStrength">The <see cref="GsmSignalStrength"/> GSM signal strength to be set on the emulator.</param>
        void SetGsmSignalStrength(GsmSignalStrength gsmSignalStrength);

        /// <summary>
        /// Set GSM voice state (Emulator only)
        /// </summary>
        /// <param name="gsmVoiceState">The <see cref="GsmVoiceState"/> GSM voice state to be set on the emulator.</param>
        void SetGsmVoice(GsmVoiceState gsmVoiceState);
    }
}

