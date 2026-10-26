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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Appium.Net.Integration.Tests.helpers
{
    public sealed class RecordedHttpRequest
    {
        public string HttpMethod { get; set; }
        public string Path { get; set; }
        public IReadOnlyDictionary<string, string> Headers { get; set; }
        public string Body { get; set; }
    }

    public sealed class MockHttpResponse
    {
        public int StatusCode { get; set; } = 200;
        public string Body { get; set; } = "{\"value\":null}";

        public static MockHttpResponse FromValue(object value, int statusCode = 200)
        {
            var payload = new Dictionary<string, object>
            {
                ["value"] = value
            };
            return new MockHttpResponse
            {
                StatusCode = statusCode,
                Body = JsonSerializer.Serialize(payload)
            };
        }
    }

    /// <summary>
    /// Lightweight local HTTP mock server for testing client-side WebDriver/Appium HTTP requests
    /// and responses without launching a real Appium server or emulator.
    /// </summary>
    public sealed class MockAppiumServer : IDisposable
    {
        private readonly HttpListener _listener;
        private readonly CancellationTokenSource _cts = new();
        private readonly ConcurrentQueue<RecordedHttpRequest> _requests = new();
        private readonly ConcurrentDictionary<string, Func<RecordedHttpRequest, MockHttpResponse>> _routes =
            new(StringComparer.OrdinalIgnoreCase);
        private readonly ConcurrentDictionary<string, Func<RecordedHttpRequest, MockHttpResponse>> _scriptRoutes =
            new(StringComparer.OrdinalIgnoreCase);
        private readonly Task _listenLoopTask;
        private bool _disposed;

        public string SessionId { get; set; } = "mock-session-id";
        public Uri ServerUri { get; }

        public IReadOnlyCollection<RecordedHttpRequest> Requests => _requests.ToArray();

        public MockAppiumServer()
        {
            int port = GetFreeLoopbackPort();
            string prefix = $"http://localhost:{port}/";
            ServerUri = new Uri(prefix);

            _listener = new HttpListener();
            _listener.Prefixes.Add(prefix);
            _listener.Start();

            RegisterDefaultRoutes();
            _listenLoopTask = Task.Run(ListenLoopAsync);
        }

        public void OnRequestHandler(string httpMethod, string path, Func<RecordedHttpRequest, MockHttpResponse> handler)
        {
            _routes[$"{httpMethod.ToUpperInvariant()} {path}"] = handler ?? (_ => MockHttpResponse.FromValue(null));
        }

        public void OnRequest(string httpMethod, string path, object responseValue, int statusCode = 200)
        {
            OnRequestHandler(httpMethod, path, _ => MockHttpResponse.FromValue(responseValue, statusCode));
        }

        public void OnExecuteScriptHandler(string scriptName, Func<RecordedHttpRequest, MockHttpResponse> handler)
        {
            _scriptRoutes[scriptName] = handler ?? (_ => MockHttpResponse.FromValue(null));
        }

        public void OnExecuteScript(string scriptName, object responseValue, int statusCode = 200)
        {
            OnExecuteScriptHandler(scriptName, _ => MockHttpResponse.FromValue(responseValue, statusCode));
        }

        public void ClearRecordedRequests()
        {
            while (_requests.TryDequeue(out _))
            {
            }
        }

        private static int GetFreeLoopbackPort()
        {
            var tcpListener = new TcpListener(IPAddress.Loopback, 0);
            tcpListener.Start();
            try
            {
                return ((IPEndPoint)tcpListener.LocalEndpoint).Port;
            }
            finally
            {
                tcpListener.Stop();
            }
        }

        private void RegisterDefaultRoutes()
        {
            OnRequestHandler("POST", "/session", _ => MockHttpResponse.FromValue(new Dictionary<string, object>
            {
                ["sessionId"] = SessionId,
                ["capabilities"] = new Dictionary<string, object>
                {
                    ["platformName"] = "Android",
                    ["appium:automationName"] = "UiAutomator2"
                }
            }));

            OnRequestHandler("DELETE", $"/session/{SessionId}", _ => MockHttpResponse.FromValue(null));
        }

        private async Task ListenLoopAsync()
        {
            while (!_cts.IsCancellationRequested)
            {
                HttpListenerContext context;
                try
                {
                    context = await _listener.GetContextAsync().ConfigureAwait(false);
                }
                catch (HttpListenerException) when (_cts.IsCancellationRequested)
                {
                    break;
                }
                catch (ObjectDisposedException)
                {
                    break;
                }

                _ = Task.Run(() => HandleContextAsync(context));
            }
        }

        private static bool TryExtractScriptName(string body, out string scriptName)
        {
            scriptName = null;
            if (string.IsNullOrWhiteSpace(body))
            {
                return false;
            }
            try
            {
                using var doc = JsonDocument.Parse(body);
                if (doc.RootElement.ValueKind == JsonValueKind.Object &&
                    doc.RootElement.TryGetProperty("script", out var scriptElement) &&
                    scriptElement.ValueKind == JsonValueKind.String)
                {
                    scriptName = scriptElement.GetString();
                    return !string.IsNullOrEmpty(scriptName);
                }
            }
            catch (JsonException)
            {
                // Ignore malformed JSON body
            }
            return false;
        }

        private async Task HandleContextAsync(HttpListenerContext context)
        {
            try
            {
                var req = context.Request;
                var headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (string key in req.Headers.AllKeys)
                {
                    if (key != null)
                    {
                        headers[key] = req.Headers[key];
                    }
                }

                string body = string.Empty;
                if (req.HasEntityBody && req.InputStream != null)
                {
                    using var reader = new StreamReader(req.InputStream, req.ContentEncoding ?? Encoding.UTF8);
                    body = await reader.ReadToEndAsync().ConfigureAwait(false);
                }

                string path = req.Url?.AbsolutePath ?? "/";
                var recorded = new RecordedHttpRequest
                {
                    HttpMethod = req.HttpMethod,
                    Path = path,
                    Headers = headers,
                    Body = body
                };
                _requests.Enqueue(recorded);

                string routeKey = $"{req.HttpMethod.ToUpperInvariant()} {path}";
                MockHttpResponse mockResponse = null;
                if (_routes.TryGetValue(routeKey, out var handler) && handler != null)
                {
                    mockResponse = handler(recorded);
                }
                else if (path.EndsWith("/execute/sync", StringComparison.OrdinalIgnoreCase) &&
                         TryExtractScriptName(body, out string scriptName) &&
                         _scriptRoutes.TryGetValue(scriptName, out var scriptHandler) &&
                         scriptHandler != null)
                {
                    mockResponse = scriptHandler(recorded);
                }
                else if (req.HttpMethod.Equals("DELETE", StringComparison.OrdinalIgnoreCase) &&
                         path.StartsWith("/session/", StringComparison.OrdinalIgnoreCase))
                {
                    mockResponse = MockHttpResponse.FromValue(null);
                }
                else
                {
                    mockResponse = MockHttpResponse.FromValue(new Dictionary<string, object>
                    {
                        ["error"] = "unknown command",
                        ["message"] = $"No mock route registered for {routeKey}",
                        ["stacktrace"] = string.Empty
                    }, statusCode: 404);
                }

                var resp = context.Response;
                resp.StatusCode = mockResponse.StatusCode;
                resp.ContentType = "application/json; charset=utf-8";
                byte[] buffer = Encoding.UTF8.GetBytes(mockResponse.Body ?? string.Empty);
                resp.ContentLength64 = buffer.Length;
                if (buffer.Length > 0)
                {
                    await resp.OutputStream.WriteAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
                }
                resp.OutputStream.Close();
            }
            catch (ObjectDisposedException)
            {
                // Ignore if listener is closing
            }
            catch (Exception ex)
            {
                try
                {
                    var resp = context.Response;
                    resp.StatusCode = 500;
                    resp.ContentType = "application/json; charset=utf-8";
                    var err = MockHttpResponse.FromValue(new Dictionary<string, object>
                    {
                        ["error"] = "unknown error",
                        ["message"] = ex.Message,
                        ["stacktrace"] = ex.StackTrace ?? string.Empty
                    }, statusCode: 500);
                    byte[] buffer = Encoding.UTF8.GetBytes(err.Body);
                    resp.ContentLength64 = buffer.Length;
                    await resp.OutputStream.WriteAsync(buffer, 0, buffer.Length).ConfigureAwait(false);
                    resp.OutputStream.Close();
                }
                catch
                {
                    // Ignore secondary errors while closing response
                }
            }
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            _disposed = true;
            _cts.Cancel();
            try
            {
                _listener.Close();
            }
            catch
            {
                // Ignore shutdown exceptions
            }
            try
            {
                _listenLoopTask.Wait(TimeSpan.FromSeconds(1));
            }
            catch
            {
                // Ignore shutdown exceptions
            }
            _cts.Dispose();
        }
    }
}
