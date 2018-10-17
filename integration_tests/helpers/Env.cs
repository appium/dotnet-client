using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Appium.Integration.Tests.Helpers
{
    public class Env
    {
        public static TimeSpan InitTimeoutSec = TimeSpan.FromSeconds(180);
        public static TimeSpan ImplicitTimeoutSec = TimeSpan.FromSeconds(10);

        private static Dictionary<string, string> env;
        private static bool _initialized;

        private static void Init()
        {
            try
            {
                if (!_initialized)
                {
                    _initialized = true;
                    string path = AppDomain.CurrentDomain.BaseDirectory;
                    StreamReader sr = new StreamReader(path + "env.json");
                    string jsonString = sr.ReadToEnd();
                    env = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
                }
            }
            catch
            {
                env = new Dictionary<string, string>();
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
            return env.ContainsKey("isRemoteAppiumServer") && IsTrue(env["isRemoteAppiumServer"]);
        }

        public static bool ServerIsLocal()
        {
            Init();
            return env.ContainsKey("DEV") && IsTrue(env["DEV"]) || IsTrue(Environment.GetEnvironmentVariable("DEV"));
        }

        public static string GetEnvVar(string name)
        {
            if (env.ContainsKey(name) && (env[name] != null))
            {
                return env[name];
            }
                return Environment.GetEnvironmentVariable(name);
        }
    }
}