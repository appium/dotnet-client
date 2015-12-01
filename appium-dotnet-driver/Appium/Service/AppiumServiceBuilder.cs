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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.Service
{
    /// <summary>
    /// This thing accepts parameters and builds instances of AppiumLocalService
    /// </summary>
    class AppiumServiceBuilder
    {
        private readonly static string Bash = "bash";
        private readonly static string CmdExe = "cmd.exe";
        private static readonly string DefaultLocalIPAddress = "0.0.0.0";

        private static Process StartSearchingProcess(string file, string arguments = null)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = file;
            if (!String.IsNullOrEmpty(arguments))
            {
                proc.StartInfo.Arguments = arguments;
            }
            proc.StartInfo.UseShellExecute = false;
            proc.Start();
            proc.WaitForExit();
            return proc;
        }

        private static string GetTheLastStringFromsOutput(StreamReader processOutput) 
        {
            string result = string.Empty;
            while (!processOutput.EndOfStream)
            {
                string current = processOutput.ReadLine();
                if (String.IsNullOrEmpty(current))
                {
                    continue;
                }
                result = current;
            }
            return result;
        }

        private static String ReadErrorStream(Process process)
        {
            string result = string.Empty;
            var errorStream = process.StandardError;

            while (!errorStream.EndOfStream)
            {
                string current = errorStream.ReadLine();
                if (String.IsNullOrEmpty(current))
                {
                    continue;
                }
                result = result + current + "\n";
            }
            return result;
        }

        private static FileInfo GetNPMFile()
        {
            bool isWindows = Platform.CurrentPlatform.IsPlatformType(PlatformType.Windows);
            string extension;

            if (isWindows)
            {
                extension = ".cmd";
            }
            else
            {
                extension = ".sh";
            }

            Guid guid = Guid.NewGuid();
            string guidStr = guid.ToString();

            string path = Path.ChangeExtension(Path.GetTempFileName(), guidStr + extension);

            byte[] bytes;
            if (isWindows)
            {
                bytes = Properties.Resources.npm_script_win;
            }
            else
            {
                bytes = Properties.Resources.npm_script_unix;
            }

            File.WriteAllBytes(path, bytes);
            return new FileInfo(path);
        }                
    }
}
