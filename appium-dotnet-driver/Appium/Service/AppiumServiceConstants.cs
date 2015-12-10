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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.Service
{
    public sealed class AppiumServiceConstants
    {
        public static readonly string AppiumNodeProperty = "appium.node.path";
        public static readonly string AppiumNodeJSExecutableProperty = "appium.node.js.exec.path";

        internal readonly static string Bash = "bash";
        internal readonly static string CmdExe = "cmd.exe";
        internal readonly static string Node = "node";

        internal static readonly string DefaultLocalIPAddress = "0.0.0.0";
        internal static readonly int DefaultAppiumPort = 4723;

        internal static readonly string AppiumFolder = "appium";
        internal static readonly string BinFolder = "bin";
        internal static readonly string AppiumJSName = "appium.js";
        internal static readonly string AppiumNodeMask = Path.DirectorySeparatorChar +
            AppiumFolder + Path.DirectorySeparatorChar + BinFolder + Path.DirectorySeparatorChar + AppiumJSName;
    }
}
