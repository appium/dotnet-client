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
using OpenQA.Selenium.Appium.Service.Exceptions;
using OpenQA.Selenium.Remote;
using System.IO;
using System.Diagnostics;
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
        private Process Service;
        private readonly bool NeedsToBeOpened;


        public static AppiumLocalService BuildDefaultService()
        {
            return new AppiumServiceBuilder().Build();
        }

        internal AppiumLocalService(FileInfo nodeJS, string arguments, IPAddress ip, int port, TimeSpan initializationTimeout, bool createNoWindow)
        {
            this.NodeJS = nodeJS;
            this.IP = ip;
            this.Arguments = arguments;
            this.Port = port;
            this.InitializationTimeout = initializationTimeout;
            this.NeedsToBeOpened = !createNoWindow;
        }

        public Uri ServiceUrl
        {
            get { return new Uri("http://" + IP.ToString() + ":" + Convert.ToString(Port) + "/wd/hub"); }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Start()
        {
            this.Service = new Process();
            this.Service.StartInfo.FileName = this.NodeJS.FullName;
            this.Service.StartInfo.Arguments = this.Arguments;
            this.Service.StartInfo.UseShellExecute = false;
            this.Service.StartInfo.CreateNoWindow = this.NeedsToBeOpened;

            bool isLaunced = false;
            string msgTxt = "The local appium server has not been started. " +
                    "The given Node.js executable: " + this.NodeJS.FullName + " Arguments: " + this.Arguments + ". " + "\n";
            try
            {
                this.Service.Start();
            }
            catch (Exception e)
            {
                DestroyProcess();
                throw new AppiumServerHasNotBeenStartedLocallyException(msgTxt, e);
            }

            isLaunced = Ping(this.InitializationTimeout);
            if (!isLaunced)
            {
                DestroyProcess();
                throw new AppiumServerHasNotBeenStartedLocallyException(msgTxt + "Time " + this.InitializationTimeout.TotalMilliseconds + 
                    " ms for the service starting has been expired!");
            }

        }

        private void DestroyProcess()
        {
            if (this.Service == null)
            {
                return;
            }

            if (!this.Service.HasExited)
            {
                this.Service.Kill();
                this.Service.Close();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Dispose()
        {
            DestroyProcess();
            GC.SuppressFinalize(this);
        }

        public bool IsRunning
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                return this.Service != null && !this.Service.HasExited && Ping(new TimeSpan(0, 0, 0, 0, 500));
            }
        }

        private bool Ping(TimeSpan span)
        {
            bool pinged = false;

            Uri status;

            Uri service = ServiceUrl;
            if (service.IsLoopback || IP.ToString().Equals(AppiumServiceConstants.DefaultLocalIPAddress))
            {
                status = new Uri("http://localhost:" + Convert.ToString(Port) + "/wd/hub/status");
            }
            else
            {
                status = new Uri(service.ToString() + "/status");
            }

            DateTime endTime = DateTime.Now.Add(this.InitializationTimeout);
            while (!pinged & DateTime.Now < endTime)
            {
                HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(status);
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
