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
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenQA.Selenium.Appium.WebSocket
{
    /// <summary>
    /// WebSocket client for handling string messages.
    /// </summary>
    public class StringWebSocketClient : ICanHandleMessages<string>, ICanHandleErrors, 
        ICanHandleConnects, ICanHandleDisconnects
    {
        private readonly ClientWebSocket _clientWebSocket;
        private Uri _endpoint;
        private CancellationTokenSource _cancellationTokenSource;
        private Task _receiveTask;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringWebSocketClient"/> class.
        /// </summary>
        public StringWebSocketClient()
        {
            _clientWebSocket = new ClientWebSocket();
            MessageHandlers = new List<Action<string>>();
            ErrorHandlers = new List<Action<Exception>>();
            ConnectionHandlers = new List<Action>();
            DisconnectionHandlers = new List<Action>();
        }

        /// <summary>
        /// Gets the list of all registered web socket message handlers.
        /// </summary>
        public List<Action<string>> MessageHandlers { get; }

        /// <summary>
        /// Gets the list of all registered web socket error handlers.
        /// </summary>
        public List<Action<Exception>> ErrorHandlers { get; }

        /// <summary>
        /// Gets the list of all registered web socket connection handlers.
        /// </summary>
        public List<Action> ConnectionHandlers { get; }

        /// <summary>
        /// Gets the list of all registered web socket disconnection handlers.
        /// </summary>
        public List<Action> DisconnectionHandlers { get; }

        /// <summary>
        /// Gets the endpoint URI.
        /// </summary>
        public Uri Endpoint => _endpoint;

        /// <summary>
        /// Register a new message handler.
        /// </summary>
        /// <param name="handler">A callback function, which accepts the received message as a parameter.</param>
        public void AddMessageHandler(Action<string> handler) => MessageHandlers.Add(handler);

        /// <summary>
        /// Removes existing message handlers.
        /// </summary>
        public void RemoveMessageHandlers() => MessageHandlers.Clear();

        /// <summary>
        /// Register a new error handler.
        /// </summary>
        /// <param name="handler">A callback function, which accepts the received exception instance as a parameter.</param>
        public void AddErrorHandler(Action<Exception> handler) => ErrorHandlers.Add(handler);

        /// <summary>
        /// Removes existing error handlers.
        /// </summary>
        public void RemoveErrorHandlers() => ErrorHandlers.Clear();

        /// <summary>
        /// Register a new connection handler.
        /// </summary>
        /// <param name="handler">A callback function, which is going to be executed when web socket connection event arrives.</param>
        public void AddConnectionHandler(Action handler) => ConnectionHandlers.Add(handler);

        /// <summary>
        /// Removes existing web socket connection handlers.
        /// </summary>
        public void RemoveConnectionHandlers() => ConnectionHandlers.Clear();

        /// <summary>
        /// Register a new web socket disconnect handler.
        /// </summary>
        /// <param name="handler">A callback function, which is going to be executed when web socket disconnect event arrives.</param>
        public void AddDisconnectionHandler(Action handler) => DisconnectionHandlers.Add(handler);

        /// <summary>
        /// Removes existing disconnection handlers.
        /// </summary>
        public void RemoveDisconnectionHandlers() => DisconnectionHandlers.Clear();

        /// <summary>
        /// Connects to a WebSocket endpoint.
        /// </summary>
        /// <param name="endpoint">The full address of an endpoint to connect to. Usually starts with 'ws://'.</param>
        public async Task ConnectAsync(Uri endpoint)
        {
            if (_clientWebSocket.State == WebSocketState.Open)
            {
                if (endpoint.Equals(_endpoint))
                {
                    return;
                }

                await DisconnectAsync();
            }

            try
            {
                _endpoint = endpoint;
                _cancellationTokenSource = new CancellationTokenSource();
                
                await _clientWebSocket.ConnectAsync(endpoint, _cancellationTokenSource.Token);
                
                // Invoke connection handlers
                foreach (var handler in ConnectionHandlers)
                {
                    handler?.Invoke();
                }

                // Start receiving messages
                _receiveTask = Task.Run(async () => await ReceiveMessagesAsync());
            }
            catch (Exception ex)
            {
                // Invoke error handlers
                foreach (var handler in ErrorHandlers)
                {
                    handler?.Invoke(ex);
                }
                throw new WebDriverException("Failed to connect to WebSocket", ex);
            }
        }

        /// <summary>
        /// Disconnects from the WebSocket endpoint.
        /// </summary>
        public async Task DisconnectAsync()
        {
            if (_clientWebSocket.State == WebSocketState.Open)
            {
                try
                {
                    _cancellationTokenSource?.Cancel();
                    await _clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                }
                catch (Exception)
                {
                    // Ignore errors during close
                }
                finally
                {
                    // Invoke disconnection handlers
                    foreach (var handler in DisconnectionHandlers)
                    {
                        handler?.Invoke();
                    }
                }
            }

            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }

        /// <summary>
        /// Removes all the registered handlers.
        /// </summary>
        public void RemoveAllHandlers()
        {
            MessageHandlers.Clear();
            ErrorHandlers.Clear();
            ConnectionHandlers.Clear();
            DisconnectionHandlers.Clear();
        }

        private async Task ReceiveMessagesAsync()
        {
            var buffer = new byte[1024 * 4];
            var messageBuilder = new StringBuilder();

            try
            {
                while (_clientWebSocket.State == WebSocketState.Open && !_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    WebSocketReceiveResult result;
                    
                    do
                    {
                        result = await _clientWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), _cancellationTokenSource.Token);
                        
                        if (result.MessageType == WebSocketMessageType.Text)
                        {
                            messageBuilder.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));
                        }
                        else if (result.MessageType == WebSocketMessageType.Close)
                        {
                            await DisconnectAsync();
                            return;
                        }
                    }
                    while (!result.EndOfMessage);

                    if (messageBuilder.Length > 0)
                    {
                        var message = messageBuilder.ToString();
                        messageBuilder.Clear();

                        // Invoke message handlers
                        foreach (var handler in MessageHandlers)
                        {
                            handler?.Invoke(message);
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Normal cancellation, ignore
            }
            catch (Exception ex)
            {
                // Invoke error handlers
                foreach (var handler in ErrorHandlers)
                {
                    handler?.Invoke(ex);
                }
            }
        }
    }
}
