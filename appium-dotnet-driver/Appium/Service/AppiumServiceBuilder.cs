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
using System.Net;
using System.Net.Sockets;

namespace OpenQA.Selenium.Appium.Service
{
    /// <summary>
    /// This thing accepts parameters and builds instances of AppiumLocalService
    /// </summary>
    public class AppiumServiceBuilder
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
        private static readonly string AppiumNodeMask = Path.DirectorySeparatorChar +
            AppiumFolder + Path.DirectorySeparatorChar + BinFolder + Path.DirectorySeparatorChar + AppiumJSName;


        private OptionCollector ServerOptions;
        private FileInfo AppiumJS;
        private string IpAddress = DefaultLocalIPAddress;
        private int Port = DefaultAppiumPort;
        private TimeSpan StartUpTimeout = new TimeSpan(0, 2, 0);
        private FileInfo NodeJS;
        private IDictionary<string, string> EnvironmentForAProcess;
        private string PathToLogFile;


        private static Process StartSearchingProcess(string file, string arguments)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = file;
            if (!String.IsNullOrEmpty(arguments))
            {
                proc.StartInfo.Arguments = arguments;
            }
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
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

        private static FileInfo GetTempFile(string extension, byte[] bytes)
        {
            Guid guid = Guid.NewGuid();
            string guidStr = guid.ToString();

            string path = Path.ChangeExtension(Path.GetTempFileName(), guidStr + extension);

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

        private FileInfo InstalledNodeInCurrentFileSystem
        {
            get
            {
                string instancePath;
                Process p = null;

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

                byte[] bytes;
                if (isWindows)
                {
                    bytes = Properties.Resources.npm_script_win;
                }
                else
                {
                    bytes = Properties.Resources.npm_script_unix;
                }

                string pathToScript = GetTempFile(extension, bytes).FullName;

                try
                {
                    if (isWindows)
                    {
                        p = StartSearchingProcess(CmdExe, "/C " + pathToScript);
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
                    if (String.IsNullOrEmpty(instancePath) || !(result = new FileInfo(instancePath + Path.DirectorySeparatorChar +
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
        }

        private FileInfo DefaultExecutable
        {
            get
            {
                Process p = null;
                byte[] bytes = Properties.Resources.getExe;
                string extension = ".js";
                string pathToScript = GetTempFile(extension, bytes).FullName;

                try
                {
                    if (Platform.CurrentPlatform.IsPlatformType(PlatformType.Windows))
                    {
                        p = StartSearchingProcess(CmdExe, "/C " + Node + " " + pathToScript);
                    }
                    else
                    {
                        p = StartSearchingProcess(Bash, "-l -c " + Node + " " + pathToScript);
                    }
                }
                catch (Exception e)
                {
                    if (p != null)
                    {
                        p.Close();
                    }
                    File.Delete(pathToScript);
                    throw new InvalidNodeJSInstanceException("Node.js is not installed!", e);
                }

                string filePath = GetTheLastStringFromsOutput(p.StandardOutput);
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
                    File.Delete(pathToScript);
                }
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

        public AppiumServiceBuilder WithIPAddress(string ipAddress)
        {
            this.IpAddress = ipAddress;
            return this;
        }

        /// <summary>
        /// Sets time value for the service starting up
        /// </summary>
        /// <param name="startUpTimeout">a time value for the service starting up</param>
        /// <returns>self-reference</returns>
        public AppiumServiceBuilder WithStartUpTimeOut(TimeSpan startUpTimeout)
        {
            if (startUpTimeout == null)
            {
                throw new ArgumentNullException("A startup timeout should not be NULL");
            }
            this.StartUpTimeout = startUpTimeout;
            return this;
        }

        private void CheckAppiumJS()
        {
            if (AppiumJS != null)
            {
                ValidateNodeStructure(AppiumJS);
                return;
            }

            string appiumJS = Environment.GetEnvironmentVariable(AppiumNodeProperty);
            if (!String.IsNullOrEmpty(appiumJS))
            {
                FileInfo node = new FileInfo(appiumJS);
                ValidateNodeStructure(node);
                this.AppiumJS = node;
                return;
            }

            this.AppiumJS = InstalledNodeInCurrentFileSystem;
        }

        /// <summary>
        /// Sets which Node.js the builder will use.
        /// </summary>
        /// <param name="nodeJSExecutable">The executable Node.js to use.</param>
        /// <returns>self-reference</returns>
        public AppiumServiceBuilder UsingDriverExecutable(FileInfo nodeJS)
        {
            if (nodeJS == null)
            {
                throw new ArgumentNullException("The nodeJS parameter should not be NULL");
            }

            if (!nodeJS.Exists)
            {
                throw new ArgumentException("The given nodeJS file doesn't exist. Given path " + nodeJS.FullName);
            }

            this.NodeJS = nodeJS;
            return this;
        }

        /// <summary>
        /// Sets which port the appium server should be started on. A value of 0 indicates that any
        /// free port may be used.
        /// </summary>
        /// <param name="port">The port to use; must be non-negative.</param>
        /// <returns>self-reference</returns>
        public AppiumServiceBuilder UsingPort(int port)
        {
            if (port < 0 )
            {
                throw new ArgumentException("The port parameter should not be negative");
            }

            if (port == 0)
            {
                return UsingAnyFreePort();
            }

            this.Port = port;
            return this;
        }

        /// <summary>
        /// Configures the appium server to start on any available port.
        /// </summary>
        /// <returns>self-reference</returns>
        public AppiumServiceBuilder UsingAnyFreePort()
        {
            Socket sock = null;

            try
            {
                sock = new Socket(AddressFamily.InterNetwork,
                         SocketType.Stream, ProtocolType.Tcp);
                sock.Bind(new IPEndPoint(IPAddress.Any, 0));
                this.Port = ((IPEndPoint)sock.LocalEndPoint).Port;
                return this;
            }
            finally
            {
                if (sock != null)
                {
                    sock.Dispose();
                }
            }
        }

        /// <summary>
        /// Defines the environment for the launched appium server.
        /// </summary>
        /// <param name="environment">A dictionary of the environment variables to launch the
        ///     appium server with.</param>
        /// <returns>self-reference</returns>
        public AppiumServiceBuilder WithEnvironment(IDictionary<string, string> environment)
        {
            if (environment == null)
            {
                throw new ArgumentNullException("The environment parameter should not be NULL");
            }

            var keys = environment.Keys;
            foreach (var key in keys)
            {
                if (String.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException("The given environment parameter contains an empty or null key");
                }
            }

            var values = environment.Values;
            foreach (var value in values)
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("The given environment parameter contains an empty or null value");
                }
            }

            this.EnvironmentForAProcess = environment;
            return this;
        }

        /// <summary>
        /// Configures the appium server to write log to the given file.
        /// </summary>
        /// <param name="logFile">A file to write log to.</param>
        /// <returns>self-reference</returns>
        public AppiumServiceBuilder WithLogFile(FileInfo logFile)
        {
            if (logFile == null)
            {
                throw new ArgumentNullException("The logFile parameter should not be NULL");
            }
            this.PathToLogFile = logFile.FullName;
            return this;
        }

        private string Args
        {
            get
            {
                List<string> argList = new List<string>();
                CheckAppiumJS();
                argList.Add(this.AppiumJS.FullName);
                argList.Add("--port");
                argList.Add(Convert.ToString(this.Port));

                argList.Add("--address");
                argList.Add(IpAddress);

                if (this.PathToLogFile != null)
                {
                    argList.Add("--log");
                    argList.Add(this.PathToLogFile);
                }

                if (this.ServerOptions != null)
                {
                    argList.AddRange(this.ServerOptions.Argiments);
                }

                string result = string.Empty;

                foreach (var value in argList)
                {
                    result = result + value + " ";
                }

                return result.Trim();
            }
        }

        public AppiumLocalService Build()
        {
            if (NodeJS == null)
            {
                NodeJS = DefaultExecutable;
            }
            AppiumLocalService service = 
                new AppiumLocalService(NodeJS.Directory.FullName, this.IpAddress, this.Port, NodeJS.Name);
            service.InitializationTimeout = StartUpTimeout;
            service.CommandLineArguments = Args;
            return service;
        }
    }
}
