﻿//Licensed under the Apache License, Version 2.0 (the "License");
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
        private static readonly string ErrorNodeNotFound = "There is no installed nodes! Please install " +
                                                           " node via NPM (https://www.npmjs.com/package/appium#using-node-js) or download and " +
                                                           "install Appium app (http://appium.io/downloads.html)";

        private OptionCollector ServerOptions;
        private FileInfo AppiumJS;
        private string IpAddress = AppiumServiceConstants.DefaultLocalIPAddress;
        private int Port = AppiumServiceConstants.DefaultAppiumPort;
        private TimeSpan StartUpTimeout = new TimeSpan(0, 2, 0);
        private FileInfo NodeJS;
        private IDictionary<string, string> EnvironmentForAProcess;
        private string PathToLogFile;
        private string[] NodeOptions = Array.Empty<string>();


        private static Process StartSearchingProcess(string file, string arguments)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = file;
            if (!string.IsNullOrEmpty(arguments))
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
                if (string.IsNullOrEmpty(current))
                {
                    continue;
                }
                result = current;
            }
            return result;
        }

        private static void ValidateNodeStructure(FileInfo node)
        {
            string absoluteNodePath = node.FullName;

            if (!node.Exists)
            {
                throw new InvalidServerInstanceException($"The invalid appium node {absoluteNodePath} has been defined",
                    new IOException($"The node {absoluteNodePath} doesn't exist"));
            }
        }

        private static string FindAFileInPATH(string shortName)
        {
            string path = Environment.GetEnvironmentVariable("PATH");
            if (!string.IsNullOrEmpty(path))
            {
                string expandedPath = Environment.ExpandEnvironmentVariables(path);
                if (!Platform.CurrentPlatform.IsPlatformType(PlatformType.Windows) &&
                    !expandedPath.Contains(
                        $"{Path.DirectorySeparatorChar}usr{Path.DirectorySeparatorChar}local{Path.DirectorySeparatorChar}bin")
                )
                {
                    expandedPath =
                        $"{expandedPath}{Path.PathSeparator}{Path.DirectorySeparatorChar}usr{Path.DirectorySeparatorChar}local{Path.DirectorySeparatorChar}bin";
                }

                string[] dirs = expandedPath.Split(Path.PathSeparator);
                foreach (string dir in dirs)
                {
                    if (dir.IndexOfAny(Path.GetInvalidPathChars()) < 0)
                    {
                        string fullPath = Path.Combine(dir, shortName);
                        if (File.Exists(fullPath))
                        {
                            return fullPath;
                        }
                    }
                    else
                    {
                        throw new IOException(
                            $"Can not parse environmental variable PATH because the directory name \"{dir}\" contains invalid characters!");
                    }
                }
            }
            return null;
        }

        private static FileInfo InstalledNodeInCurrentFileSystem
        {
            get
            {
                string nodeInstancePath;
                Process p = null;

                bool isWindows = Platform.CurrentPlatform.IsPlatformType(PlatformType.Windows);
                try
                {
                    if (isWindows)
                    {
                        p = StartSearchingProcess(AppiumServiceConstants.CmdExe, "/C npm root -g");
                    }
                    else
                    {
                        p = StartSearchingProcess(AppiumServiceConstants.Bash, "-c \"" + "echo $(npm root -g);\"");
                    }
                }
                catch (Exception)
                {
                    p?.Close();
                    throw;
                }

                nodeInstancePath = GetTheLastStringFromsOutput(p.StandardOutput);

                try
                {
                    DirectoryInfo defaultAppiumNode;
                    if (string.IsNullOrEmpty(nodeInstancePath) || !(defaultAppiumNode = new DirectoryInfo(
                            nodeInstancePath + Path.DirectorySeparatorChar +
                            AppiumServiceConstants.AppiumFolder)).Exists)
                    {
                        throw new InvalidServerInstanceException(ErrorNodeNotFound);
                    }

                    //appium servers v1.5.x and higher
                    FileInfo newResult;
                    if ((newResult = new FileInfo(defaultAppiumNode.FullName + AppiumServiceConstants.AppiumNodeMask))
                        .Exists)
                    {
                        return newResult;
                    }

                    throw new InvalidServerInstanceException(ErrorNodeNotFound,
                        new IOException($"Could not find the file " +
                                        $"{AppiumServiceConstants.AppiumNodeMask} in the {defaultAppiumNode} directory"));
                }
                finally
                {
                    p.Close();
                }
            }
        }

        private static FileInfo DefaultExecutable
        {
            get
            {
                string appiumJS = Environment.GetEnvironmentVariable(AppiumServiceConstants.NodeBinaryPath);
                if (!string.IsNullOrEmpty(appiumJS))
                {
                    FileInfo result = new FileInfo(appiumJS);
                    if (result.Exists)
                    {
                        return result;
                    }
                    else
                    {
                        throw new InvalidNodeJSInstanceException($"The defined value {result.FullName} of the " +
                                                                 $"{AppiumServiceConstants.NodeBinaryPath} refers to unexisting file!");
                    }
                }

                string filePath;
                try
                {
                    if (Platform.CurrentPlatform.IsPlatformType(PlatformType.Windows))
                    {
                        filePath = FindAFileInPATH($"{AppiumServiceConstants.Node}.exe");
                    }
                    else
                    {
                        filePath = FindAFileInPATH(AppiumServiceConstants.Node);
                    }
                }
                catch (Exception e)
                {
                    throw new InvalidNodeJSInstanceException("Node.js is not installed!", e);
                }

                if (string.IsNullOrEmpty(filePath))
                {
                    string errorMessage =
                        "Couldn't find a path to the default Node.js instance from the PATH environmental variable. It seems Node.js is not " +
                        $"installed on this computer. Please check the PATH environmental variable or define the {AppiumServiceConstants.NodeBinaryPath} " +
                        "environmental variable value.";
                    throw new InvalidNodeJSInstanceException(errorMessage);
                }
                return new FileInfo(filePath);
            }
        }

        /// <summary>
        /// This method specifies Appium server options
        /// </summary>
        /// <param name="serverOptions">A collection of Appium server options</param>
        /// <returns>Self reference</returns>
        public AppiumServiceBuilder WithArguments(OptionCollector serverOptions)
        {
            ServerOptions = serverOptions;
            return this;
        }

        /// <summary>
        /// This method defines the desired Appium server binary
        /// </summary>
        /// <param name="appiumJS">Is a file path to the desired appium.js file</param>
        /// <returns>Self-reference</returns>
        public AppiumServiceBuilder WithAppiumJS(FileInfo appiumJS)
        {
            AppiumJS = appiumJS;
            return this;
        }

        /// <summary>
        /// This method defines the IP Address to listen on
        /// </summary>
        /// <param name="ipAddress">the IP Address to listen on</param>
        /// <returns>Self-reference</returns>
        public AppiumServiceBuilder WithIPAddress(string ipAddress)
        {
            IpAddress = ipAddress;
            return this;
        }

        /// <summary>
        /// Sets time value for the service starting up
        /// </summary>
        /// <param name="startUpTimeout">a time value for the service starting up</param>
        /// <returns>self-reference</returns>
        public AppiumServiceBuilder WithStartUpTimeOut(TimeSpan startUpTimeout)
        {
            if (startUpTimeout <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(startUpTimeout), "A startup timeout should be a positive TimeSpan value");
            }
            StartUpTimeout = startUpTimeout;
            return this;
        }

        /// <summary>
        /// Command line arguments that will be passed to NodeJS when it starts up.
        /// </summary>
        /// <param name="arguments">
        /// A collection of raw string arguments that will be passed to NodeJS.
        /// Spaces will be automatically added between arguments. You are responsible for
        /// properly escaping any special characters for your operating system.
        /// Arguments cannot be null, empty strings, or only whitespace.
        /// </param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public AppiumServiceBuilder WithNodeArguments(params string[] arguments)
        {
            if (arguments == null)
            {
                throw new ArgumentNullException(nameof(arguments), "Argument cannot be null");
            }

            for (var i = 0; i < arguments.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(arguments[i]))
                {
                    throw new ArgumentException($"Invalid Node argument at index {i}. Node arguments cannot be null, empty, or only whitespace.", nameof(arguments));
                }
            }

            NodeOptions = arguments;
            return this;
        }

        private void CheckAppiumJS()
        {
            if (AppiumJS != null)
            {
                ValidateNodeStructure(AppiumJS);
                return;
            }

            string appiumJS = Environment.GetEnvironmentVariable(AppiumServiceConstants.AppiumBinaryPath);
            if (!string.IsNullOrEmpty(appiumJS))
            {
                FileInfo node = new FileInfo(appiumJS);
                ValidateNodeStructure(node);
                AppiumJS = node;
                return;
            }

            AppiumJS = InstalledNodeInCurrentFileSystem;
        }

        /// <summary>
        /// Sets which Node.js the builder will use.
        /// </summary>
        /// <param name="nodeJS">The executable Node.js to use.</param>
        /// <returns>self-reference</returns>
        public AppiumServiceBuilder UsingDriverExecutable(FileInfo nodeJS)
        {
            if (nodeJS == null)
            {
                throw new ArgumentNullException(nameof(nodeJS), "The nodeJS parameter should not be NULL");
            }

            if (!nodeJS.Exists)
            {
                throw new ArgumentException($"The given nodeJS file doesn't exist. Given path {nodeJS.FullName}");
            }

            NodeJS = nodeJS;
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
            if (port < 0)
            {
                throw new ArgumentException("The port parameter should not be negative");
            }

            if (port == 0)
            {
                return UsingAnyFreePort();
            }

            Port = port;
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
                Port = ((IPEndPoint)sock.LocalEndPoint).Port;
                return this;
            }
            finally
            {
                sock?.Dispose();
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
                throw new ArgumentNullException(nameof(environment), "The environment parameter should not be NULL");
            }

            var keys = environment.Keys;
            foreach (var key in keys)
            {
                if (string.IsNullOrEmpty(key))
                {
                    throw new ArgumentNullException("The given environment parameter contains an empty or null key", nameof(key));
                }
            }

            var values = environment.Values;
            foreach (var value in values)
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("The given environment parameter contains an empty or null value", nameof(value));
                }
            }

            EnvironmentForAProcess = environment;
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
                throw new ArgumentNullException(nameof(logFile), "The logFile parameter should not be NULL");
            }
            PathToLogFile = logFile.FullName;
            return this;
        }

        private string Args
        {
            get
            {
                List<string> argList = new List<string>();
                CheckAppiumJS();

                argList.AddRange(NodeOptions);

                argList.Add($"\"{AppiumJS.FullName}\"");
                argList.Add("--port");
                argList.Add($"\"{Port}\"");

                argList.Add("--address");
                argList.Add($"\"{IpAddress}\"");

                if (PathToLogFile != null)
                {
                    argList.Add("--log");
                    argList.Add($"\"{PathToLogFile}\"");
                }

                if (ServerOptions != null)
                {
                    argList.AddRange(ServerOptions.Arguments);
                }

                string result = string.Empty;

                foreach (var value in argList)
                {
                    result = result + value + " ";
                }
                return result.Trim();
            }
        }

        /// <summary>
        /// This method builds an instance of AppiumLocalService using defined parameters
        /// </summary>
        /// <returns>an instance of AppiumLocalService built using defined parameters</returns>
        public AppiumLocalService Build()
        {
            NodeJS ??= DefaultExecutable;
            return new AppiumLocalService(NodeJS, Args, IPAddress.Parse(IpAddress), Port, StartUpTimeout, EnvironmentForAProcess);
        }
    }
}