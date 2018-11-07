using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Appium.Net.Integration.Tests.helpers
{
    public class Env
    {
        public static TimeSpan InitTimeoutSec = TimeSpan.FromSeconds(180);
        public static TimeSpan ImplicitTimeoutSec = TimeSpan.FromSeconds(10);

        private static Dictionary<string, string> _env;
        private static bool _initialized;

        private static void Init()
        {
            try
            {
                if (!_initialized)
                {
                    _initialized = true;
                    var path = AppDomain.CurrentDomain.BaseDirectory;
                    var sr = new StreamReader(path + "env.json");
                    var jsonString = sr.ReadToEnd();
                    _env = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
                }
            }
            catch
            {
                _env = new Dictionary<string, string>();
            }
        }

        private static bool IsTrue(string val)
        {
            val = val?.ToLower().Trim();
            return (val == "true") || (val == "1");
        }

        public static bool ServerIsRemote()
        {
            Init();
            return _env.ContainsKey("isRemoteAppiumServer") && IsTrue(_env["isRemoteAppiumServer"]);
        }

        public static bool ServerIsLocal()
        {
            Init();
            return _env.ContainsKey("DEV") && IsTrue(_env["DEV"]) || IsTrue(Environment.GetEnvironmentVariable("DEV"));
        }

        public static string GetEnvVar(string name)
        {
            if (_env.ContainsKey(name) && (_env[name] != null))
            {
                return _env[name];
            }
                return Environment.GetEnvironmentVariable(name);
        }
    }
}