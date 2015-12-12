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

using OpenQA.Selenium.Remote;
using System;
using System.Reflection;

namespace OpenQA.Selenium.Appium.Service
{
    internal class AppiumCommandExecutor : ICommandExecutor
    {

        private readonly AppiumLocalService Service;
        private readonly Uri URL;
        private readonly ICommandExecutor RealExecutor;
        private static readonly TimeSpan TimeForTheServerResponding = new TimeSpan(0, 0, 30);

        private static ICommandExecutor CreateRealExecutor(Uri url)
        {
            Assembly assembly = Assembly.LoadFrom("WebDriver.dll");
            Type realExecutorType = assembly.GetType("OpenQA.Selenium.Remote.HttpCommandExecutor");
            ConstructorInfo constructor = realExecutorType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { typeof(Uri), typeof(TimeSpan) }, null);
            return (constructor.Invoke(realExecutorType, new object[] { url, TimeForTheServerResponding }) as ICommandExecutor);
        }

        private AppiumCommandExecutor(Uri url, ICommandExecutor realExecutor)
        {
            this.URL = url;
            this.RealExecutor = realExecutor;
        }

        internal AppiumCommandExecutor(Uri url)
            : this(url, CreateRealExecutor(url))
        {
            this.Service = null;
        }

        internal AppiumCommandExecutor(AppiumLocalService service)
            :this(service.ServiceUrl, CreateRealExecutor(service.ServiceUrl))
        {
            this.Service = service;
        }

        public CommandInfoRepository CommandInfoRepository
        {
            get
            {
                return RealExecutor.CommandInfoRepository;
            }
        }

        public Response Execute(Command commandToExecute)
        {
            if (commandToExecute.Name == DriverCommand.NewSession && this.Service != null)
            {
                this.Service.Start();
            }

            try
            {
                return RealExecutor.Execute(commandToExecute);
            }
            finally
            {
                if (commandToExecute.Name == DriverCommand.Quit && this.Service != null)
                {
                    this.Service.Dispose();
                }
            }
        }
    }
}
