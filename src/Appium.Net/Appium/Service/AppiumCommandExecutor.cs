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

using Newtonsoft.Json;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;

namespace OpenQA.Selenium.Appium.Service
{
    internal class AppiumCommandExecutor : ICommandExecutor
    {
        private readonly AppiumLocalService Service;
        private ICommandExecutor RealExecutor;
        private bool isDisposed;
        private const string IdempotencyHeader = "X-Idempotency-Key";
        private AppiumClientConfig ClientConfig;

        private TimeSpan CommandTimeout;

        private static ICommandExecutor CreateRealExecutor(Uri remoteAddress, TimeSpan commandTimeout)
        {
            return new HttpCommandExecutor(remoteAddress, commandTimeout);
        }

        private AppiumCommandExecutor(ICommandExecutor realExecutor)
        {
            RealExecutor = realExecutor;
        }

        internal AppiumCommandExecutor(Uri url, TimeSpan timeForTheServerResponding, AppiumClientConfig clientConfig)
            : this(CreateRealExecutor(url, timeForTheServerResponding))
        {
            CommandTimeout = timeForTheServerResponding;
            Service = null;
            ClientConfig = clientConfig;
        }

        internal AppiumCommandExecutor(AppiumLocalService service, TimeSpan timeForTheServerResponding, AppiumClientConfig clientConfig)
            : this(CreateRealExecutor(service.ServiceUrl, timeForTheServerResponding))
        {
            CommandTimeout = timeForTheServerResponding;
            Service = service;
            ClientConfig = clientConfig;
        }

        public Response Execute(Command commandToExecute)
        {
            Response result = null;

            try
            {
                if (commandToExecute.Name == DriverCommand.NewSession)
                {
                    Service?.Start();
                    RealExecutor = ModifyNewSessionHttpRequestHeader(RealExecutor);

                    result = RealExecutor.Execute(commandToExecute);
                    RealExecutor = UpdateExecutor(result, RealExecutor);
                }
                else
                {
                    result = RealExecutor.Execute(commandToExecute);
                }

                return result;
            }
            catch (Exception e)
            {
                if ((commandToExecute.Name == DriverCommand.NewSession))
                {
                    Service?.Dispose();
                }

                throw;
            }
            finally
            {
                if (result != null && result.Status != WebDriverResult.Success &&
                    commandToExecute.Name == DriverCommand.NewSession)
                {
                    Dispose();
                }

                if (commandToExecute.Name == DriverCommand.Quit)
                {
                    Dispose();
                }
            }
        }

        private ICommandExecutor ModifyNewSessionHttpRequestHeader(ICommandExecutor commandExecutor)
        {
            if (commandExecutor == null) throw new ArgumentNullException(nameof(commandExecutor));
            var modifiedCommandExecutor = commandExecutor as HttpCommandExecutor;
            
            modifiedCommandExecutor.SendingRemoteHttpRequest += (sender, args) =>
                    args.AddHeader(IdempotencyHeader, Guid.NewGuid().ToString());

            return modifiedCommandExecutor;
        }


        /// <summary>
        /// Return an instance of AppiumCommandExecutor.
        /// If the executor can use as-is, this method will return the given executor without any updates.
        /// </summary>
        /// <param name="result">The result of the command execution.</param>
        /// <param name="currentExecutor">Current AppiumCommandExecutor instance.</param>
        private ICommandExecutor UpdateExecutor(Response result, ICommandExecutor currentExecutor)
        {
            if (ClientConfig.DirectConnect == false) {
                return currentExecutor;
            }

            var newExecutor = GetNewExecutorWithDirectConnect(result);
            if (newExecutor == null) {
                return currentExecutor;
            }

            return newExecutor;
        }

        /// <summary>
        /// Returns a new command executor if the responsed had directConnect.
        /// </summary>
        /// <param name="result">The result of the command execution.</param>
        private ICommandExecutor GetNewExecutorWithDirectConnect(Response response)
        {
            var newUri = new DirectConnect(response).GetUri();
            if (newUri != null) {
                return new HttpCommandExecutor(newUri, CommandTimeout);
            }

            return null;
        }

        public void Dispose() => Dispose(true);

        protected void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    Service?.Dispose();
                }

                isDisposed = true;
            }
        }

        public bool TryAddCommand(string commandName, CommandInfo info)
        {
            return this.RealExecutor.TryAddCommand(commandName, info);
        }
    }
}