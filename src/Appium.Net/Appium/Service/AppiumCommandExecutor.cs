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
        private const int MaxRetryAttempts = 3;
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

            for (int attempt = 1; attempt <= MaxRetryAttempts; attempt++)
            {
                try
                {
                    HandleNewSessionCommand(commandToExecute);

                    result = RealExecutor.Execute(commandToExecute);

                    HandleCommandCompletion(commandToExecute, result);

                    if (result.Status == WebDriverResult.Success)
                    {
                        return result;
                    }
                }
                catch (Exception)
                {
                    HandleCommandException(commandToExecute);
                    throw;
                }
            }
            return result;
        }

        /// <summary>
        /// Handles a new session command.
        /// If the command is a NewSession command, it starts the service if not already started,
        /// and modifies the HTTP request header of the real executor for a new session.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        private void HandleNewSessionCommand(Command command)
        {
            if (command.Name == DriverCommand.NewSession)
            {
                Service?.Start();
                RealExecutor = ModifyNewSessionHttpRequestHeader(RealExecutor);
            }
        }
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

        /// <summary>
        /// Handles the completion of a command.
        /// If the result indicates a failure for a NewSession command, disposes the resources.
        /// If the command is a Quit command, disposes the resources.
        /// </summary>
        /// <param name="command">The command that was executed.</param>
        /// <param name="result">The result of the command execution.</param>
        private void HandleCommandCompletion(Command command, Response result)
        {
            if (result != null && result.Status != WebDriverResult.Success &&
                command.Name == DriverCommand.NewSession)
            {
                Dispose();
            }

            if (command.Name == DriverCommand.Quit)
            {
                Dispose();
            }
        }

        /// <summary>
        /// Handles exceptions that occur while processing a command.
        /// If the command is a new session command, it disposes the resources.
        /// </summary>
        /// <param name="command">The command to handle.</param>
        private void HandleCommandException(Command command)
        {
            if (command.Name == DriverCommand.NewSession)
            {
                Dispose();
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
        /// <param name="currentExecutor">Current ICommandExecutor instance.</param>
        /// <returns>A ICommandExecutor instance</returns>
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
        /// Returns a new command executor if the response had directConnect.
        /// </summary>
        /// <param name="result">The result of the command execution.</param>
        /// <returns>A ICommandExecutor instance or null</returns>
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
