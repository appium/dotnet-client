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
using System.Text.Json;

namespace OpenQA.Selenium.Appium.Service.Options
{
    public sealed class OptionCollector
    {
        private readonly IDictionary<string, string> CollectedArgs = new Dictionary<string, string>();
        private AppiumOptions options;
        private static readonly string CapabilitiesFlag = "--default-capabilities";

        /// <summary>
        /// Adds an argument and its value
        /// </summary>
        /// <param name="arguments">is a structure where the first alement is a server argument and the second one
        /// is string value of the passed argument</param>
        /// <returns>self reference</returns>
        public OptionCollector AddArguments(KeyValuePair<string, string> arguments)
        {
            CollectedArgs.Add(arguments);
            return this;
        }

        /// <summary>
        /// Adds/merges server-specific capabilities
        /// </summary>
        /// <param name="options">is an instance of OpenQA.Selenium.Remote.AppiumOptions</param>
        /// <returns>the self-reference</returns>
        public OptionCollector AddCapabilities(AppiumOptions options)
        {
            if (this.options == null)
            {
                this.options = options;
            }
            else
            {
                IDictionary<string, object> givenDictionary = options.ToDictionary();

                foreach (var item in givenDictionary)
                {
                    this.options.AddAdditionalAppiumOption(item.Key, item.Value);
                }
            }

            return this;
        }

        private string ParseCapabilitiesIfWindows(IDictionary<string, object> capabilitiesDictionary)
        {
            string result = string.Empty;

            if (capabilitiesDictionary != null)
            {
                foreach (var item in capabilitiesDictionary)
                {
                    object value = item.Value;

                    if (value == null)
                    {
                        continue;
                    }

                    if (typeof(string).IsAssignableFrom(value.GetType()))
                    {
                        if (AppiumServiceConstants.FilePathCapabilitiesForWindows.Contains(item.Key))
                        {
                            value = $"\\\"{Convert.ToString(value).Replace("\\", "/")}\\\"";
                        }
                        else
                        {
                            value = $"\\\"{value}\\\"";
                        }
                    }
                    else
                    {
                        if (typeof(bool).IsAssignableFrom(value.GetType()))
                        {
                            value = Convert.ToString(value).ToLowerInvariant();
                        }
                    }

                    string key = $"\\\"{item.Key}\\\"";
                    if (string.IsNullOrEmpty(result))
                    {
                        result = $"{key}: {value}";
                    }
                    else
                    {
                        result = result + ", " + key + ": " + value;
                    }
                }
            }

            return "\"{" + result + "}\"";
        }

        private string ParseCapabilitiesIfUNIX(IDictionary<string, object> capabilitiesDictionary)
        {
            if (capabilitiesDictionary == null)
            {
                return string.Empty;
            }

            // Serialize to JSON and escape double quotes so they survive argument parsing
            var json = JsonSerializer.Serialize(capabilitiesDictionary);
            // Escape double quotes with backslash for shell argument
            var escaped = json.Replace("\"", "\\\"");
            return $"\"{escaped}\"";
        }

        /// <summary>
        /// Builds a sequence of server arguments
        /// </summary>
        internal IList<string> Arguments
        {
            get
            {
                List<string> result = [];
                var keys = CollectedArgs.Keys;
                foreach (var key in keys)
                {
                    if (string.IsNullOrEmpty(key))
                    {
                        continue;
                    }
                    result.Add(key);
                    string value = CollectedArgs[key];
                    if (!string.IsNullOrEmpty(value))
                    {
                        result.Add(value);
                    }
                }

                var optionsDictionary = options?.ToDictionary();

                if (optionsDictionary != null && optionsDictionary.Count > 0)
                {
                    result.Add(CapabilitiesFlag);
                    if (Platform.CurrentPlatform.IsPlatformType(PlatformType.Windows))
                    {
                        result.Add(ParseCapabilitiesIfWindows(optionsDictionary));
                    }
                    else
                    {
                        result.Add(ParseCapabilitiesIfUNIX(optionsDictionary));
                    }
                }

                return result.AsReadOnly();
            }
        }
    }
}