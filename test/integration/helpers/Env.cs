using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Appium.Net.Integration.Tests.helpers
{
    public class Env
    {
        public static TimeSpan InitTimeoutSec = TimeSpan.FromSeconds(180);
        public static TimeSpan ImplicitTimeoutSec = TimeSpan.FromSeconds(10);

        private static Dictionary<string, JsonElement> _env;
        private static bool _initialized;

        private static void Init()
        {
            _env = new Dictionary<string, JsonElement>
            {
                { "DEV", JsonDocument.Parse("true").RootElement }, 
                { "isRemoteAppiumServer", JsonDocument.Parse("false").RootElement }, 
                { "remoteAppiumServerUri", JsonDocument.Parse("\"http://localhost:4723\"").RootElement }
            };

            if (_initialized) return;

            try
            {
                _initialized = true;
                var path = AppDomain.CurrentDomain.BaseDirectory;
                var sr = new StreamReader(path + "env.json");
                var jsonString = sr.ReadToEnd();
                _env = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Error parsing JSON: {jsonEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing environment: {ex.Message}");
            }
        }

        private static bool IsTrue(object val)
        {
            val = val?.ToString().ToLower().Trim();
            return val.Equals("true") || val.Equals("1");
        }

        public static bool ServerIsRemote()
        {
            Init();
            return _env.ContainsKey("isRemoteAppiumServer") && IsTrue(_env["isRemoteAppiumServer"]);
        }

        public static bool ServerIsLocal()
        {
            Init();
            return (_env.ContainsKey("DEV") && IsTrue(_env["DEV"])) || IsTrue(Environment.GetEnvironmentVariable("DEV"));
        }

        public static string GetEnvVar(string name)
        {
            if (_env.ContainsKey(name))
            {
                JsonElement element = _env[name];

                return element.ValueKind switch
                {
                    JsonValueKind.String => element.GetString(),
                    JsonValueKind.Number => element.GetRawText(),
                    JsonValueKind.True or JsonValueKind.False => element.GetRawText(),
                    JsonValueKind.Null => null,
                    _ => element.GetRawText()
                };
            }
            return Environment.GetEnvironmentVariable(name);
        }
    }
}