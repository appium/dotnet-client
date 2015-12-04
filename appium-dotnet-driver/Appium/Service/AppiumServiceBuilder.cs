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

using OpenQA.Selenium.Appium.Service.Exceptions;
using OpenQA.Selenium.Appium.Service.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace OpenQA.Selenium.Appium.Service
{
    /// <summary>
    /// This thing accepts parameters and builds instances of AppiumLocalService
    /// </summary>
    class AppiumServiceBuilder
    {
        private readonly static string Bash = "bash";
        private readonly static string CmdExe = "cmd.exe";
        private readonly static string Node = "node";

        private static readonly string DefaultLocalIPAddress = "0.0.0.0";
        public static readonly string AppiumNodeProperty = "appium.node.path";
        private static readonly int DefaultAppiumPort = 4723;

        private static readonly string AppiumFolder = "appium";
        private static readonly string BinFolder = "bin";
        private static readonly string AppiumJSName = "appium.js";
        private static readonly string AppiumNodeMask = Path.PathSeparator +
            AppiumFolder + Path.PathSeparator + BinFolder + Path.PathSeparator + AppiumJSName;


        private OptionCollector ServerOptions;
        private FileInfo AppiumJS;
        private string IpAddress = DefaultLocalIPAddress;
        private int Port = DefaultAppiumPort;
        private TimeSpan StartUpTimeout = new TimeSpan(0, 2, 0);


        private static Process StartSearchingProcess(string file, string arguments)
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

        private static void ValidateNodeStructure(FileInfo node)
        {
            string absoluteNodePath = node.FullName;

            if (!node.Exists)
            {
                throw new InvalidServerInstanceException("The invalid appium node " + absoluteNodePath + " has been defined",
                        new IOException("The node " + absoluteNodePath + "doesn't exist"));
            }

            if (!absoluteNodePath.EndsWith(AppiumNodeMask))
            {
                throw new InvalidServerInstanceException("It is probably there is the corrupted appium server installation. Path " +
                        absoluteNodePath + "doesn't match " + AppiumNodeMask);
            }
        }

        private FileInfo findNodeInCurrentFileSystem()
        {
            String instancePath;
            Process p = null;
            string pathToScript = GetNPMFile().FullName;
            try
            {
                if (Platform.CurrentPlatform.IsPlatformType(PlatformType.Windows))
                {
                    p = StartSearchingProcess(CmdExe, pathToScript);
                }
                else
                {
                    p = StartSearchingProcess(Bash, "-l " + pathToScript);
                }
            }
            catch (Exception e)
            {
                if (p != null)
                {
                    p.Close();
                }
                if (File.Exists(pathToScript))
                {
                    File.Delete(pathToScript);
                }
                throw e;
            }

            instancePath = GetTheLastStringFromsOutput(p.StandardOutput);

            try
            {
                FileInfo result;
                if (String.IsNullOrEmpty(instancePath) || !(result = new FileInfo(instancePath + Path.PathSeparator +
                        AppiumNodeMask)).Exists)
                {
                    String errorOutput = ReadErrorStream(p);
                    throw new InvalidServerInstanceException("There is no installed nodes! Please install " +
                            " node via NPM (https://www.npmjs.com/package/appium#using-node-js) or download and " +
                            "install Appium app (http://appium.io/downloads.html)",
                            new IOException(errorOutput));
                }
                return result;
            }
            finally
            {
                p.Close();
                if (File.Exists(pathToScript))
                {
                    File.Delete(pathToScript);
                }
            }
        }

        private FileInfo findDefaultExecutable()
        {
            Process p = null;
            try
            {
                if (Platform.CurrentPlatform.IsPlatformType(PlatformType.Windows))
                {
                    p = StartSearchingProcess(CmdExe, Node);
                }
                else
                {
                    p = StartSearchingProcess(Bash, "-l -c " + Node);
                }
            }
            catch (Exception e)
            {
                if (p != null)
                {
                    p.Close();
                }
                throw new InvalidNodeJSInstanceException("Node.js is not installed!", e);
            }

            String filePath;
            try
            {
                StreamWriter writer = p.StandardInput;
                writer.WriteLine("console.log(process.execPath);");
                writer.Close();
                filePath = GetTheLastStringFromsOutput(p.StandardOutput);
            }
            catch (Exception e)
            {
                p.Close();
                throw e;
            }

            try
            {
                if (String.IsNullOrEmpty(filePath))
                {
                    String errorOutput = ReadErrorStream(p);
                    String errorMessage = "Can't get a path to the default Node.js instance";
                    throw new InvalidNodeJSInstanceException(errorMessage, new IOException(errorOutput));
                }
                return new FileInfo(filePath);
            }
            finally
            {
                p.Close();
            }
        }

        /// <summary>
        /// This method specifies Appium server options
        /// </summary>
        /// <param name="serverOptions">A collection of Appium server options</param>
        /// <returns>Self reference</returns>
        public AppiumServiceBuilder WithArguments(OptionCollector serverOptions)
        {
            this.ServerOptions = serverOptions;
            return this;
        }

        public AppiumServiceBuilder WithAppiumJS(FileInfo appiumJS)
        {
            this.AppiumJS = appiumJS;
            return this;
        }

        public AppiumServiceBuilder withIPAddress(string ipAddress)
        {
            this.IpAddress = ipAddress;
            return this;
        }

        /// <summary>
        /// Sets time value for the service starting up
        /// </summary>
        /// <param name="startUpTimeout">a time value for the service starting up</param>
        /// <returns>self-reference</returns>
        public AppiumServiceBuilder withStartUpTimeOut(TimeSpan startUpTimeout)
        {
            if (startUpTimeout == null)
            {
                throw new ArgumentException("Startup timeout should not be NULL");
            }
            this.StartUpTimeout = startUpTimeout;
            return this;
        }

        private void checkAppiumJS()
        {
            if (AppiumJS != null)
            {
                ValidateNodeStructure(AppiumJS);
                return;
            }

            string appiumJS = Environment.GetEnvironmentVariable(AppiumNodeProperty);
            if (appiumJS != null)
            {
                FileInfo node = new FileInfo(appiumJS);
                ValidateNodeStructure(node);
                this.AppiumJS = node;
                return;
            }

            this.AppiumJS = findNodeInCurrentFileSystem();
        }
    }
}
