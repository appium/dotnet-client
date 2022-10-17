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
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;

namespace OpenQA.Selenium.Appium.Service
{
    public class AppiumLocalService : ICommandServer
    {
        private readonly FileInfo NodeJS;
        private readonly string Arguments;
        private readonly IPAddress IP;
        private readonly int Port;
        private readonly TimeSpan InitializationTimeout;
        private readonly IDictionary<string, string> EnvironmentForProcess;
        private Process Service;

        /// <summary>
        /// Creates an instance of AppiumLocalService without special settings
        /// </summary>
        /// <returns>An instance of AppiumLocalService without special settings</returns>
        public static AppiumLocalService BuildDefaultService() => new AppiumServiceBuilder().Build();

        internal AppiumLocalService(
            FileInfo nodeJS,
            string arguments,
            IPAddress ip,
            int port,
            TimeSpan initializationTimeout,
            IDictionary<string, string> environmentForProcess)
        {
            NodeJS = nodeJS;
            IP = ip;
            Arguments = arguments;
            Port = port;
            InitializationTimeout = initializationTimeout;
            EnvironmentForProcess = environmentForProcess;
        }

        /// <summary>
        /// The base URL for the managed appium server.
        /// </summary>
        public Uri ServiceUrl => new Uri($"http://{IP}:{Convert.ToString(Port)}");

        /// <summary>
        /// Event that can be used to capture the output of the service
        /// </summary>
        public event DataReceivedEventHandler OutputDataReceived;

        /// <summary>
        /// Starts the defined appium server
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Start()
        {
            if (IsRunning)
            {
                return;
            }

            Service = new Process();
            Service.StartInfo.FileName = NodeJS.FullName;
            Service.StartInfo.Arguments = Arguments;
            Service.StartInfo.UseShellExecute = false;
            Service.StartInfo.CreateNoWindow = true;

            if (EnvironmentForProcess != null)
            {
                foreach (var entry in EnvironmentForProcess)
                {
                    Service.StartInfo.EnvironmentVariables[entry.Key] = entry.Value ?? string.Empty;
                }
            }

            Service.StartInfo.RedirectStandardOutput = true;
            Service.OutputDataReceived += (sender, e) => OutputDataReceived?.Invoke(this, e);

            bool isLaunched = false;
            string msgTxt =
                $"The local appium server has not been started. The given Node.js executable: {NodeJS.FullName} Arguments: {Arguments}. " +
                "\n";

            try
            {
                Service.Start();

                Service.BeginOutputReadLine();
            }
            catch (Exception e)
            {
                DestroyProcess();
                throw new AppiumServerHasNotBeenStartedLocallyException(msgTxt, e);
            }

            isLaunched = Ping(InitializationTimeout);
            if (!isLaunched)
            {
                DestroyProcess();
                throw new AppiumServerHasNotBeenStartedLocallyException(
                    msgTxt +
                    $"Time {InitializationTimeout.TotalMilliseconds} ms for the service starting has been expired!");
            }
        }

        private void DestroyProcess()
        {
            if (Service == null)
            {
                return;
            }

            try
            {
                Service.Kill();
            }
            catch (Exception ignored)
            {
            }
            finally
            {
                Service.Close();
            }
        }

        /// <summary>
        /// Stops this service if it is currently running.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Dispose()
        {
            DestroyProcess();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Is the defined appium server being run or not
        /// </summary>
        public bool IsRunning
        {
            get
            {
                if (Service == null)
                {
                    return false;
                }

                try
                {
                    var pid = Service.Id;
                }
                catch (Exception)
                {
                    return false;
                }

                return Ping(new TimeSpan(0, 0, 0, 0, 500));
            }
        }
        private string GetArgsValue(string argStr)
        {
            int idx;
            List<string> args = Arguments.Split(' ').ToList();
            idx= args.IndexOf(argStr);
            return args[idx + 1];
        }
        private string ParseBasePath()
        {
            if (Arguments.Contains("--base-path"))
            {
                return GetArgsValue("--base-path");
            }
            else if (Arguments.Contains("-pa"))
            {
                return GetArgsValue("-pa");
            }
            return AppiumServiceConstants.DefaultBasePath;
        }
        private Uri CreateStatusUrl()
        {
            Uri status;

            Uri service = ServiceUrl;

            string basePath = ParseBasePath();
            bool defBasePath = basePath.Equals(AppiumServiceConstants.DefaultBasePath);

            if (service.IsLoopback || IP.ToString().Equals(AppiumServiceConstants.DefaultLocalIPAddress))
            {
                var tmpStatus = "http://localhost:" + Convert.ToString(Port);
                if (defBasePath)
                {
                    status = new Uri(tmpStatus + AppiumServiceConstants.StatusUrl);
                }
                else
                {
                    status = new Uri(tmpStatus + basePath + AppiumServiceConstants.StatusUrl);
                }
            }
            else
            {
                if (defBasePath)
                {
                    status = new Uri(service, AppiumServiceConstants.StatusUrl);
                }
                else
                {
                    status = new Uri(service, basePath + AppiumServiceConstants.StatusUrl);
                }
            }
            return status;
        }

        private bool Ping(TimeSpan span)
        {
            bool pinged = false;

            Uri status;

            status = CreateStatusUrl();

            DateTime endTime = DateTime.Now.Add(this.InitializationTimeout);
            while (!pinged & DateTime.Now < endTime)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(status);
                HttpWebResponse response = null;
                try
                {
                    using (response = (HttpWebResponse) request.GetResponse())
                    {
                        pinged = true;
                    }
                }
                catch (Exception e)
                {
                    pinged = false;
                }
                finally
                {
                    if (response != null)
                    {
                        response.Close();
                    }
                }
            }
            return pinged;
        }
    }
}