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

using OpenQA.Selenium.Appium.Interfaces;
using System;
using System.IO;

namespace OpenQA.Selenium.Appium.Android.Interfaces
{
    public interface IPushesFiles : IInteractsWithFiles
    {
        /// <summary>
        /// Saves a string as a file on the remote mobile device.
        /// </summary>
        /// <param name="pathOnDevice">Path to file to write data to on remote device</param>
        /// <param name="stringData">A string to write to remote device</param>
        void PushFile(string pathOnDevice, string base64Data);

        /// <summary>
        /// Saves base64 encoded data as a file on the remote mobile device.
        /// </summary>
        /// <param name="pathOnDevice">Path to file to write data to on remote device</param>
        /// <param name="base64Data">Base64 encoded byte array of data to write to remote device</param>
        void PushFile(string pathOnDevice, byte[] base64Data);

        /// <summary>
        /// Saves given file as a file on the remote mobile device.
        /// </summary>
        /// <param name="pathOnDevice">Path to file to write data to on remote device</param>
        /// <param name="base64Data">A file to write to remote device</param>
        void PushFile(string pathOnDevice, FileInfo file);
    }
}