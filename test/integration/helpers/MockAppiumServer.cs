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
using System.Linq;
using System.Text.Json;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Appium.Net.Integration.Tests.helpers
{
    public sealed class RecordedHttpRequest
    {
        public string HttpMethod { get; set; }
        public string Path { get; set; }
        public IReadOnlyDictionary<string, string> Headers { get; set; }
        public string Body { get; set; }
    }

    /// <summary>
    /// Lightweight local HTTP mock server backed by <see cref="WireMockServer"/>
    /// for testing client-side WebDriver/Appium HTTP requests and responses without
    /// requiring a real Appium server or device/emulator.
    /// </summary>
    public sealed class MockAppiumServer : IDisposable
    {
        private readonly WireMockServer _server;
        private bool _disposed;

        public string SessionId { get; set; } = "mock-session-id";
        public Uri ServerUri { get; }

        public IReadOnlyCollection<RecordedHttpRequest> Requests
        {
            get
            {
                return _server.LogEntries
                    .Where(entry => entry.RequestMessage != null)
                    .Select(entry =>
                    {
                        var req = entry.RequestMessage;
                        var headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                        if (req.Headers != null)
                        {
                            foreach (var kvp in req.Headers)
                            {
                                headers[kvp.Key] = kvp.Value != null ? string.Join(",", kvp.Value) : string.Empty;
                            }
                        }

                        return new RecordedHttpRequest
                        {
                            HttpMethod = req.Method,
                            Path = req.Path,
                            Headers = headers,
                            Body = req.Body ?? string.Empty
                        };
                    })
                    .ToArray();
            }
        }

        public MockAppiumServer()
        {
            _server = WireMockServer.Start();
            string baseUrl = _server.Url ?? $"http://127.0.0.1:{_server.Port}";
            ServerUri = new Uri(baseUrl.EndsWith("/", StringComparison.Ordinal) ? baseUrl : baseUrl + "/");

            RegisterDefaultRoutes();
        }

        public void OnRequest(string httpMethod, string path, object responseValue, int statusCode = 200)
        {
            _server
                .Given(Request.Create()
                    .WithPath(path)
                    .UsingMethod(httpMethod.ToUpperInvariant()))
                .RespondWith(Response.Create()
                    .WithStatusCode(statusCode)
                    .WithHeader("Content-Type", "application/json; charset=utf-8")
                    .WithBody(SerializeValueEnvelope(responseValue)));
        }

        public void OnExecuteScript(string scriptName, object responseValue, int statusCode = 200)
        {
            string expectedJson = JsonSerializer.Serialize(new Dictionary<string, string>
            {
                ["script"] = scriptName
            });

            _server
                .Given(Request.Create()
                    .WithPath($"/session/{SessionId}/execute/sync")
                    .UsingPost()
                    .WithBody(new JsonPartialMatcher(expectedJson)))
                .RespondWith(Response.Create()
                    .WithStatusCode(statusCode)
                    .WithHeader("Content-Type", "application/json; charset=utf-8")
                    .WithBody(SerializeValueEnvelope(responseValue)));
        }

        private void RegisterDefaultRoutes()
        {
            OnRequest("POST", "/session", new Dictionary<string, object>
            {
                ["sessionId"] = SessionId,
                ["capabilities"] = new Dictionary<string, object>
                {
                    ["platformName"] = "Android",
                    ["appium:automationName"] = "UiAutomator2"
                }
            });

            OnRequest("DELETE", $"/session/{SessionId}", null);
        }

        private static string SerializeValueEnvelope(object value)
        {
            var payload = new Dictionary<string, object>
            {
                ["value"] = value
            };
            return JsonSerializer.Serialize(payload);
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            _disposed = true;
            _server.Stop();
            _server.Dispose();
        }
    }
}
