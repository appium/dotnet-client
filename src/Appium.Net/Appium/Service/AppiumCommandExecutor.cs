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
using System.Threading.Tasks;

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
           return Task.Run(() => ExecuteAsync(commandToExecute)).GetAwaiter().GetResult();
       }

        public async Task<Response> ExecuteAsync(Command commandToExecute)
        {
            Response result = null;

            try
            {
                bool newSession = HandleNewSessionCommand(commandToExecute);
                result = await RealExecutor.ExecuteAsync(commandToExecute).ConfigureAwait(false);
                if (newSession)
                {
                    RealExecutor = UpdateExecutor(result, RealExecutor);
                }
                return result;
            }
            catch (Exception e)
            {
                HandleCommandException(commandToExecute);

                throw;
            }
            finally
            {
                HandleCommandCompletion(commandToExecute, result);
            }
        }

        /// <summary>
        /// Handles a new session command, starts the service, and modifies the HTTP request header if necessary.
        /// </summary>
        /// <param name="command">The command to be handled.</param>
        /// <returns>True if the command is a new session command and the service was started; otherwise, false.</returns>
        private bool HandleNewSessionCommand(Command command)
        {
            if (command.Name == DriverCommand.NewSession)
            {
                Service?.Start();
                RealExecutor = ModifyNewSessionHttpRequestHeader(RealExecutor);
                return true;
            }
            return false;
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
        /// Handles exceptions that occur while executing a command.
        /// </summary>
        /// <param name="command">The command that caused the exception.</param>
        private void HandleCommandException(Command command)
        {
            if (command.Name == DriverCommand.NewSession)
            {
                Service?.Dispose();
            }  
        }

        /// <summary>
        /// Modifies the HTTP request header for a new session in the command executor.
        /// </summary>
        /// <param name="commandExecutor">The command executor to be modified.</param>
        /// <returns>The modified command executor with the updated HTTP request header.</returns>
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
            if (ClientConfig.DirectConnect == false)
            {
                return currentExecutor;
            }

            var newExecutor = GetNewExecutorWithDirectConnect(result);
            if (newExecutor == null)
            {
                return currentExecutor;
            }

            return newExecutor;
        }

        /// <summary>
        /// Returns a new command executor if the response had directConnect.
        /// </summary>
        /// <param name="response">The result of the command execution.</param>
        /// <returns>A ICommandExecutor instance or null</returns>
        private ICommandExecutor GetNewExecutorWithDirectConnect(Response response)
        {
            var newUri = new DirectConnect(response).GetUri();
            if (newUri != null)
            {
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
