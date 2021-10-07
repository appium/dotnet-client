﻿//Licensed under the Apache License, Version 2.0 (the "License");
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
                IDictionary<string, object> originalDictionary = this.options.ToDictionary();
                IDictionary<string, object> givenDictionary = options.ToDictionary();
                IDictionary<string, object> result = new Dictionary<string, object>(originalDictionary);

                foreach (var item in givenDictionary)
                {
                    if (originalDictionary.ContainsKey(item.Key))
                    {
                        result[item.Key] = item.Value;
                    }
                    else
                    {
                        result.Add(item.Key, item.Value);
                    }
                }

                this.options = new AppiumOptions();

                foreach (var item in result)
                {
                    this.options.AddAdditionalCapability(item.Key, item.Value);
                }
            }

            return this;
        }

        private string ParseCapabilitiesIfWindows()
        {
            string result = string.Empty;

            if (options != null)
            {
                IDictionary<string, object> capabilitiesDictionary = options.ToDictionary();

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
                            value = Convert.ToString(value).ToLower();
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

        private string ParseCapabilitiesIfUNIX()
        {
            string result = string.Empty;

            if (options != null)
            {
                IDictionary<string, object> capabilitiesDictionary = options.ToDictionary();

                foreach (var item in capabilitiesDictionary)
                {
                    object value = item.Value;

                    if (value == null)
                    {
                        continue;
                    }

                    if (typeof(string).IsAssignableFrom(value.GetType()))
                    {
                        value = $"\"{value}\"";
                        ;
                    }

                    else
                    {
                        if (typeof(bool).IsAssignableFrom(value.GetType()))
                        {
                            value = Convert.ToString(value).ToLower();
                        }
                    }

                    string key = $"\"{item.Key}\"";
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

            return "'{" + result + "}'";
        }

        /// <summary>
        /// Builds a sequence of server arguments
        /// </summary>
        internal IList<string> Arguments
        {
            get
            {
                List<string> result = new List<string>();
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

                if (options != null && options.ToDictionary().Count > 0)
                {
                    result.Add(CapabilitiesFlag);
                    if (Platform.CurrentPlatform.IsPlatformType(PlatformType.Windows))
                    {
                        result.Add(ParseCapabilitiesIfWindows());
                    }
                    else
                    {
                        result.Add(ParseCapabilitiesIfUNIX());
                    }
                }

                return result.AsReadOnly();
            }
        }
    }
}