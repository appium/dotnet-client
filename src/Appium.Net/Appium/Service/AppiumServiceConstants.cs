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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.Service
{
    public sealed class AppiumServiceConstants
    {
        /// <summary>
        /// The environmental variable used to define
        /// the path to executable appium.js (1.4.x and lower) or
        /// main.js (1.5.x and higher)
        /// </summary>
        public static readonly string AppiumBinaryPath = "APPIUM_BINARY_PATH";

        /// <summary>
        /// The environmental variable used to define
        /// the path to executable NodeJS file (node.exe for WIN and
        /// node for Linux/MacOS X)
        /// </summary>
        public static readonly string NodeBinaryPath = "NODE_BINARY_PATH";

        internal readonly static string Bash = "/bin/bash";
        internal readonly static string CmdExe = "cmd.exe";
        internal readonly static string Node = "node";

        internal static readonly string DefaultLocalIPAddress = "127.0.0.1";
        internal static readonly int DefaultAppiumPort = 4723;

        internal static readonly string AppiumFolder = "appium";
        internal static readonly string BinFolder = "bin";

        internal static readonly string BuildFolder = "build";
        internal static readonly string LibFolder = "lib";

        internal static readonly string AppiumJSName = "appium.js";
        internal static readonly string MainJSName = "main.js";

        internal static readonly string AppiumNodeMask =
            $"{Path.DirectorySeparatorChar}{BuildFolder}{Path.DirectorySeparatorChar}" +
            $"{LibFolder}{Path.DirectorySeparatorChar}{MainJSName}";

        internal static readonly IList<string> FilePathCapabilitiesForWindows = new List<string>(new string[]
        {
            AndroidMobileCapabilityType.KeystorePath,
            AndroidMobileCapabilityType.ChromedriverExecutable,
            MobileCapabilityType.App
        }).AsReadOnly();
    }
}