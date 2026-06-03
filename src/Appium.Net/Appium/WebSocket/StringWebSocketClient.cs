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
        ICanHandleConnects, ICanHandleDisconnects, IDisposable
    {
        private ClientWebSocket _clientWebSocket;
        private readonly SemaphoreSlim _connectionLock = new SemaphoreSlim(1, 1);
        private Uri _endpoint;
        private CancellationTokenSource _cancellationTokenSource;
        private Task _receiveTask;

        // Plain delegate fields (not events) so Interlocked operations can be used for thread-safe access.
        private Action<string> _messageReceived;
        private Action<Exception> _errorOccurred;
        private Action _connected;
        private Action _disconnected;

        /// <summary>
        /// Gets a read-only snapshot of all registered web socket message handlers.
        /// </summary>
        public IReadOnlyList<Action<string>> MessageHandlers => GetInvocationList<Action<string>>(_messageReceived);

        /// <summary>
        /// Gets a read-only snapshot of all registered web socket error handlers.
        /// </summary>
        public IReadOnlyList<Action<Exception>> ErrorHandlers => GetInvocationList<Action<Exception>>(_errorOccurred);

        /// <summary>
        /// Gets a read-only snapshot of all registered web socket connection handlers.
        /// </summary>
        public IReadOnlyList<Action> ConnectionHandlers => GetInvocationList<Action>(_connected);

        /// <summary>
        /// Gets a read-only snapshot of all registered web socket disconnection handlers.
        /// </summary>
        public IReadOnlyList<Action> DisconnectionHandlers => GetInvocationList<Action>(_disconnected);

        private static IReadOnlyList<T> GetInvocationList<T>(Delegate handler) where T : Delegate
        {
            var invocationList = handler?.GetInvocationList();
            if (invocationList == null) return Array.Empty<T>();
            var list = new T[invocationList.Length];
            for (int i = 0; i < invocationList.Length; i++)
            {
                list[i] = (T)invocationList[i];
            }
            return list;
        }

        /// <summary>
        /// Gets the endpoint URI.
        /// </summary>
        public Uri Endpoint => _endpoint;

        /// <summary>
        /// Register a new message handler.
        /// </summary>
        /// <param name="handler">A callback function, which accepts the received message as a parameter.</param>
        public void AddMessageHandler(Action<string> handler) => InterlockedAdd(ref _messageReceived, handler);

        /// <summary>
        /// Removes a specific message handler.
        /// </summary>
        /// <param name="handler">A callback function to remove.</param>
        public void RemoveMessageHandler(Action<string> handler) => InterlockedRemove(ref _messageReceived, handler);

        /// <summary>
        /// Removes all existing message handlers.
        /// </summary>
        public void RemoveMessageHandlers() => Interlocked.Exchange(ref _messageReceived, null);

        /// <summary>
        /// Register a new error handler.
        /// </summary>
        /// <param name="handler">A callback function, which accepts the received exception instance as a parameter.</param>
        public void AddErrorHandler(Action<Exception> handler) => InterlockedAdd(ref _errorOccurred, handler);

        /// <summary>
        /// Removes a specific error handler.
        /// </summary>
        /// <param name="handler">A callback function to remove.</param>
        public void RemoveErrorHandler(Action<Exception> handler) => InterlockedRemove(ref _errorOccurred, handler);

        /// <summary>
        /// Removes all existing error handlers.
        /// </summary>
        public void RemoveErrorHandlers() => Interlocked.Exchange(ref _errorOccurred, null);

        /// <summary>
        /// Register a new connection handler.
        /// </summary>
        /// <param name="handler">A callback function, which is going to be executed when web socket connection event arrives.</param>
        public void AddConnectionHandler(Action handler) => InterlockedAdd(ref _connected, handler);

        /// <summary>
        /// Removes a specific connection handler.
        /// </summary>
        /// <param name="handler">A callback function to remove.</param>
        public void RemoveConnectionHandler(Action handler) => InterlockedRemove(ref _connected, handler);

        /// <summary>
        /// Removes all existing web socket connection handlers.
        /// </summary>
        public void RemoveConnectionHandlers() => Interlocked.Exchange(ref _connected, null);

        /// <summary>
        /// Register a new web socket disconnect handler.
        /// </summary>
        /// <param name="handler">A callback function, which is going to be executed when web socket disconnect event arrives.</param>
        public void AddDisconnectionHandler(Action handler) => InterlockedAdd(ref _disconnected, handler);

        /// <summary>
        /// Removes a specific disconnection handler.
        /// </summary>
        /// <param name="handler">A callback function to remove.</param>
        public void RemoveDisconnectionHandler(Action handler) => InterlockedRemove(ref _disconnected, handler);

        /// <summary>
        /// Removes all existing disconnection handlers.
        /// </summary>
        public void RemoveDisconnectionHandlers() => Interlocked.Exchange(ref _disconnected, null);

        private static void InterlockedAdd<T>(ref T field, T handler) where T : Delegate
        {
            if (handler == null) return;
            T current, updated;
            do
            {
                current = Volatile.Read(ref field);
                updated = (T)Delegate.Combine(current, handler);
            } while (Interlocked.CompareExchange(ref field, updated, current) != current);
        }

        private static void InterlockedRemove<T>(ref T field, T handler) where T : Delegate
        {
            if (handler == null) return;
            T current, updated;
            do
            {
                current = Volatile.Read(ref field);
                updated = (T)Delegate.Remove(current, handler);
            } while (Interlocked.CompareExchange(ref field, updated, current) != current);
        }

        /// <summary>
        /// Connects to a WebSocket endpoint.
        /// </summary>
        /// <param name="endpoint">The full address of an endpoint to connect to. Usually starts with 'ws://'.</param>
        public async Task ConnectAsync(Uri endpoint)
        {
            await _connectionLock.WaitAsync();
            try
            {
                // If websocket is already open and connected to the same endpoint, return
                if (_clientWebSocket?.State == WebSocketState.Open)
                {
                    if (endpoint.Equals(_endpoint))
                    {
                        return;
                    }

                    await DisconnectInternalAsync();
                }

                // Recreate ClientWebSocket if it's disposed or in a non-connectable state
                if (_clientWebSocket == null || 
                    _clientWebSocket.State == WebSocketState.Closed || 
                    _clientWebSocket.State == WebSocketState.Aborted)
                {
                    _clientWebSocket?.Dispose();
                    _clientWebSocket = new ClientWebSocket();
                }

                try
                {
                    _endpoint = endpoint;
                    _cancellationTokenSource = new CancellationTokenSource();
                    
                    await _clientWebSocket.ConnectAsync(endpoint, _cancellationTokenSource.Token);
                    
                    // Invoke connection handlers
                    _connected?.Invoke();

                    // Start receiving messages
                    _receiveTask = Task.Run(ReceiveMessagesAsync);
                }
                catch (WebSocketException ex)
                {
                    // Invoke error handlers
                    _errorOccurred?.Invoke(ex);
                    throw new WebDriverException($"Failed to connect to WebSocket at {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff} UTC", ex);
                }
                catch (TaskCanceledException ex)
                {
                    // Invoke error handlers
                    _errorOccurred?.Invoke(ex);
                    throw new WebDriverException("WebSocket connection was cancelled", ex);
                }
            }
            finally
            {
                _connectionLock.Release();
            }
        }

        /// <summary>
        /// Disconnects from the WebSocket endpoint.
        /// </summary>
        public async Task DisconnectAsync()
        {
            await _connectionLock.WaitAsync();
            try
            {
                await DisconnectInternalAsync();
            }
            finally
            {
                _connectionLock.Release();
            }
        }

        /// <summary>
        /// Internal disconnect method without lock (called when already holding the lock).
        /// </summary>
        private async Task DisconnectInternalAsync()
        {
            if (_clientWebSocket.State == WebSocketState.Open)
            {
                try
                {
                    _cancellationTokenSource?.Cancel();
                    await _clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                    
                    // Wait for receive task to complete
                    if (_receiveTask != null)
                    {
                        await _receiveTask;
                    }
                }
                catch (Exception ex)
                {
                    // Invoke error handlers for errors during close
                    _errorOccurred?.Invoke(ex);
                }
                finally
                {
                    // Invoke disconnection handlers
                    _disconnected?.Invoke();
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
            Interlocked.Exchange(ref _messageReceived, null);
            Interlocked.Exchange(ref _errorOccurred, null);
            Interlocked.Exchange(ref _connected, null);
            Interlocked.Exchange(ref _disconnected, null);
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
                        _messageReceived?.Invoke(message);
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
                _errorOccurred?.Invoke(ex);
            }
        }

        /// <summary>
        /// Disposes the web socket client and releases resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the StringWebSocketClient and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DisconnectAsync().Wait();
                _clientWebSocket?.Dispose();
                _cancellationTokenSource?.Dispose();
                _connectionLock?.Dispose();
            }
        }
    }
}
