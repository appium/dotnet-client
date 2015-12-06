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

namespace OpenQA.Selenium.Appium.Service
{
    public class AppiumLocalService : DriverService
    {
        private readonly string IP;

        internal AppiumLocalService(string servicePath, string ip, int port, string driverServiceExecutableName)
            :base(servicePath, port, driverServiceExecutableName, null)
        {
            this.IP = ip;
        }
        
        public Uri ServiceUrl
        {
            get { return new Uri("http://" + IP + ":" + Convert.ToString(Port) + "/wd/hub"); }
        }

        internal string CommandLineArguments
        {
            set; get;
        }

        internal TimeSpan InitializationTimeout
        {
            set;  get;
        }

        public void Start()
        {
            try
            {
                base.Start();
            }
            catch (Exception e)
            {
                new AppiumServerHasNotBeenStartedLocallyException("Appium local server has not been started", e);
            }
        }

        public static AppiumLocalService BuildDefaultService()
        {
            return new AppiumServiceBuilder().Build();
        }
    }
}
