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

namespace OpenQA.Selenium.Appium.Service
{
    public class AppiumLocalService : ICommandServer
    {
        private readonly FileInfo NodeJS;
        private readonly string Arguments;
        private readonly string IP;
        private readonly int Port;
        private readonly TimeSpan InitializationTimeout;


        public static AppiumLocalService BuildDefaultService()
        {
            return new AppiumServiceBuilder().Build();
        }

        internal AppiumLocalService(FileInfo nodeJS, string arguments, string ip, int port, TimeSpan initializationTimeout)
        {
            this.NodeJS = nodeJS;
            this.IP = ip;
            this.Arguments = arguments;
            this.Port = port;
            this.InitializationTimeout = initializationTimeout;
        }

        public Uri ServiceUrl
        {
            get { return new Uri("http://" + IP + ":" + Convert.ToString(Port) + "/wd/hub"); }
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool IsRunning
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
