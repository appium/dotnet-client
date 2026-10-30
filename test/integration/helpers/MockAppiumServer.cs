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
using System.Globalization;
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
    /// Lightweight local HTTP mock server backed by a loopback <see cref="TcpListener"/>
    /// for testing client-side WebDriver/Appium HTTP requests and responses without
    /// requiring administrator privileges, URL ACL reservations, or a real Appium server.
    /// </summary>
    public sealed class MockAppiumServer : IDisposable
    {
        private readonly TcpListener _listener;
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
            _listener = new TcpListener(IPAddress.Loopback, 0);
            _listener.Start();

            int port = ((IPEndPoint)_listener.LocalEndpoint).Port;
            ServerUri = new Uri($"http://127.0.0.1:{port}/");

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
                TcpClient client;
                try
                {
                    client = await _listener.AcceptTcpClientAsync().ConfigureAwait(false);
                }
                catch (SocketException) when (_cts.IsCancellationRequested)
                {
                    break;
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
                catch (InvalidOperationException) when (_cts.IsCancellationRequested)
                {
                    break;
                }

                _ = Task.Run(() => HandleClientAsync(client));
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

        private static async Task<string> ReadAsciiLineAsync(Stream stream, CancellationToken token)
        {
            var sb = new StringBuilder();
            var singleByte = new byte[1];
            while (true)
            {
                int read = await stream.ReadAsync(singleByte, 0, 1, token).ConfigureAwait(false);
                if (read == 0)
                {
                    return sb.Length == 0 ? null : sb.ToString();
                }

                char ch = (char)singleByte[0];
                if (ch == '\n')
                {
                    if (sb.Length > 0 && sb[sb.Length - 1] == '\r')
                    {
                        sb.Length -= 1;
                    }
                    return sb.ToString();
                }

                sb.Append(ch);
            }
        }

        private static async Task<byte[]> ReadExactBytesAsync(Stream stream, int count, CancellationToken token)
        {
            byte[] buffer = new byte[count];
            int offset = 0;
            while (offset < count)
            {
                int read = await stream.ReadAsync(buffer, offset, count - offset, token).ConfigureAwait(false);
                if (read == 0)
                {
                    break;
                }
                offset += read;
            }

            if (offset == count)
            {
                return buffer;
            }

            byte[] partial = new byte[offset];
            Buffer.BlockCopy(buffer, 0, partial, 0, offset);
            return partial;
        }

        private static string GetReasonPhrase(int statusCode)
        {
            return statusCode switch
            {
                200 => "OK",
                400 => "Bad Request",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => "OK"
            };
        }

        private static async Task WriteHttpResponseAsync(Stream stream, MockHttpResponse response, CancellationToken token)
        {
            byte[] bodyBytes = Encoding.UTF8.GetBytes(response.Body ?? string.Empty);
            string reasonPhrase = GetReasonPhrase(response.StatusCode);
            string headerText =
                $"HTTP/1.1 {response.StatusCode} {reasonPhrase}\r\n" +
                "Content-Type: application/json; charset=utf-8\r\n" +
                $"Content-Length: {bodyBytes.Length}\r\n" +
                "Connection: close\r\n\r\n";
            byte[] headerBytes = Encoding.ASCII.GetBytes(headerText);

            await stream.WriteAsync(headerBytes, 0, headerBytes.Length, token).ConfigureAwait(false);
            if (bodyBytes.Length > 0)
            {
                await stream.WriteAsync(bodyBytes, 0, bodyBytes.Length, token).ConfigureAwait(false);
            }
            await stream.FlushAsync(token).ConfigureAwait(false);
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            using (client)
            using (NetworkStream stream = client.GetStream())
            {
                try
                {
                    string requestLine = await ReadAsciiLineAsync(stream, _cts.Token).ConfigureAwait(false);
                    if (string.IsNullOrEmpty(requestLine))
                    {
                        return;
                    }

                    string[] parts = requestLine.Split(' ');
                    string httpMethod = parts.Length > 0 ? parts[0] : "GET";
                    string rawTarget = parts.Length > 1 ? parts[1] : "/";
                    string path = rawTarget;
                    if (Uri.TryCreate(rawTarget, UriKind.Absolute, out Uri absoluteUri))
                    {
                        path = absoluteUri.AbsolutePath;
                    }
                    else
                    {
                        int queryIndex = rawTarget.IndexOf('?');
                        if (queryIndex >= 0)
                        {
                            path = rawTarget.Substring(0, queryIndex);
                        }
                    }

                    var headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    while (true)
                    {
                        string headerLine = await ReadAsciiLineAsync(stream, _cts.Token).ConfigureAwait(false);
                        if (string.IsNullOrEmpty(headerLine))
                        {
                            break;
                        }

                        int colonIndex = headerLine.IndexOf(':');
                        if (colonIndex > 0)
                        {
                            string key = headerLine.Substring(0, colonIndex).Trim();
                            string val = headerLine.Substring(colonIndex + 1).Trim();
                            headers[key] = val;
                        }
                    }

                    if (headers.TryGetValue("Expect", out string expectValue) &&
                        expectValue.IndexOf("100-continue", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        byte[] continueBytes = Encoding.ASCII.GetBytes("HTTP/1.1 100 Continue\r\n\r\n");
                        await stream.WriteAsync(continueBytes, 0, continueBytes.Length, _cts.Token).ConfigureAwait(false);
                        await stream.FlushAsync(_cts.Token).ConfigureAwait(false);
                    }

                    string body = string.Empty;
                    if (headers.TryGetValue("Content-Length", out string contentLengthStr) &&
                        int.TryParse(contentLengthStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out int contentLength) &&
                        contentLength > 0)
                    {
                        byte[] bodyBytes = await ReadExactBytesAsync(stream, contentLength, _cts.Token).ConfigureAwait(false);
                        body = Encoding.UTF8.GetString(bodyBytes);
                    }

                    var recorded = new RecordedHttpRequest
                    {
                        HttpMethod = httpMethod,
                        Path = path,
                        Headers = headers,
                        Body = body
                    };
                    _requests.Enqueue(recorded);

                    string routeKey = $"{httpMethod.ToUpperInvariant()} {path}";
                    MockHttpResponse mockResponse;
                    if (_routes.TryGetValue(routeKey, out var handler) && handler != null)
                    {
                        mockResponse = handler(recorded);
                    }
                    else if (httpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase) &&
                             path.Equals($"/session/{SessionId}/execute/sync", StringComparison.OrdinalIgnoreCase) &&
                             TryExtractScriptName(body, out string scriptName) &&
                             _scriptRoutes.TryGetValue(scriptName, out var scriptHandler) &&
                             scriptHandler != null)
                    {
                        mockResponse = scriptHandler(recorded);
                    }
                    else if (httpMethod.Equals("DELETE", StringComparison.OrdinalIgnoreCase) &&
                             path.Equals($"/session/{SessionId}", StringComparison.OrdinalIgnoreCase))
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

                    await WriteHttpResponseAsync(stream, mockResponse, _cts.Token).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    // Ignore if listener is closing
                }
                catch (ObjectDisposedException)
                {
                    // Ignore if listener is closing
                }
                catch (IOException) when (_cts.IsCancellationRequested)
                {
                    // Ignore if listener is closing
                }
                catch (Exception ex)
                {
                    try
                    {
                        var err = MockHttpResponse.FromValue(new Dictionary<string, object>
                        {
                            ["error"] = "unknown error",
                            ["message"] = ex.Message,
                            ["stacktrace"] = ex.StackTrace ?? string.Empty
                        }, statusCode: 500);
                        await WriteHttpResponseAsync(stream, err, CancellationToken.None).ConfigureAwait(false);
                    }
                    catch
                    {
                        // Ignore secondary errors while closing response
                    }
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
                _listener.Stop();
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
