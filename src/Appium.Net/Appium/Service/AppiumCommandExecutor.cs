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
    public class DirectConnect
    {
        private const string DIRECT_CONNECT_PROTOCOL = "directConnectProtocol";
        private const string DIRECT_CONNECT_HOST = "directConnectHost";
        private const string DIRECT_CONNECT_PORT = "directConnectPort";
        private const string DIRECT_CONNECT_PATH = "directConnectPath";

        private readonly string Protocol;
        private readonly string Host;
        private readonly string Port;
        private readonly string Path;

        internal DirectConnect(Response response)
        {

            this.Protocol = GetDirectConnectValue((Dictionary<string, object>)response.Value, DIRECT_CONNECT_PROTOCOL);
            this.Host = GetDirectConnectValue((Dictionary<string, object>)response.Value, DIRECT_CONNECT_HOST);
            this.Port = GetDirectConnectValue((Dictionary<string, object>)response.Value, DIRECT_CONNECT_PORT);
            this.Path = GetDirectConnectValue((Dictionary<string, object>)response.Value, DIRECT_CONNECT_PATH);
        }

        public Uri GetUri() {
            if (this.Protocol == null || this.Host == null || this.Port == null || this.Path == null) {
                return null;
            }
            return new Uri(this.Protocol + "://" + this.Host + ":" + this.Port + this.Path);
        }

        internal string GetDirectConnectValue(Dictionary<string, object> value, string keyName)
        {
            if (value.ContainsKey("appium:" + keyName))
            {
                return value[keyName].ToString();
            }

            if (value.ContainsKey(keyName)) {
                return value[keyName].ToString();
            }
            return null;
        }
    }
    internal class AppiumCommandExecutor : ICommandExecutor
    {
        private readonly AppiumLocalService Service;
        private ICommandExecutor RealExecutor;
        private bool isDisposed;
        private const string IdempotencyHeader = "X-Idempotency-Key";
        private bool IsDirectConnectEnabled;

        private TimeSpan CommandTimeout;

        private static ICommandExecutor CreateRealExecutor(Uri remoteAddress, TimeSpan commandTimeout)
        {
            return new HttpCommandExecutor(remoteAddress, commandTimeout);
        }

        private AppiumCommandExecutor(ICommandExecutor realExecutor)
        {
            RealExecutor = realExecutor;
        }

        internal AppiumCommandExecutor(Uri url, TimeSpan timeForTheServerResponding, bool isDirectConnectEnabled)
            : this(CreateRealExecutor(url, timeForTheServerResponding))
        {
            CommandTimeout = timeForTheServerResponding;
            Service = null;
            IsDirectConnectEnabled = isDirectConnectEnabled;
        }

        internal AppiumCommandExecutor(AppiumLocalService service, TimeSpan timeForTheServerResponding, bool isDirectConnectEnabled)
            : this(CreateRealExecutor(service.ServiceUrl, timeForTheServerResponding))
        {
            CommandTimeout = timeForTheServerResponding;
            Service = service;
            IsDirectConnectEnabled = isDirectConnectEnabled;
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
                    if (IsDirectConnectEnabled == true) {
                        var newExecutor = UpdateAsDirectConnectURL(result, CommandTimeout);
                        if (newExecutor != null) {
                            RealExecutor = newExecutor;
                        }
                        
                    }
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

        private ICommandExecutor UpdateAsDirectConnectURL(Response response, TimeSpan commandTimeout)
        {
            var newUri = new DirectConnect(response).GetUri();
            if (newUri != null) {
                return new HttpCommandExecutor(newUri, commandTimeout);
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