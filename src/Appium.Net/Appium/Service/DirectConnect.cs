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

namespace OpenQA.Selenium.Appium.Service
{
    public class DirectConnect
    {
        private const string DIRECT_CONNECT_PROTOCOL = "directConnectProtocol";
        private const string DIRECT_CONNECT_HOST = "directConnectHost";
        private const string DIRECT_CONNECT_PORT = "directConnectPort";
        private const string DIRECT_CONNECT_PATH = "directConnectPath";

        private readonly string Protocol;
        private readonly string Host;
        private readonly string Port;
        private readonly string Path;


        /// <summary>
        /// Create a direct connect instance from the given received response.
        /// </summary>
        public DirectConnect(Response response)
        {

            this.Protocol = GetDirectConnectValue((Dictionary<string, object>)response.Value, DIRECT_CONNECT_PROTOCOL);
            this.Host = GetDirectConnectValue((Dictionary<string, object>)response.Value, DIRECT_CONNECT_HOST);
            this.Port = GetDirectConnectValue((Dictionary<string, object>)response.Value, DIRECT_CONNECT_PORT);
            this.Path = GetDirectConnectValue((Dictionary<string, object>)response.Value, DIRECT_CONNECT_PATH);
        }

        /// <summary>
        ///  Returns a URL instance built with members in the DirectConnect instance.
        /// </summary>
        /// <returns>A Uri instance</returns>
        public Uri GetUri() {
            if (this.Protocol == null || this.Host == null || this.Port == null || this.Path == null) {
                return null;
            }

            if (this.Protocol != "https")
            {
                return null;
            }

            return new Uri(this.Protocol + "://" + this.Host + ":" + this.Port + this.Path);
        }

        /// <summary>
        ///  Returns a value of  instance built with members in the DirectConnect instance.
        /// </summary>
        /// <param name="value">The value of the 'value' key in the response body.</param>
        /// <param name="keyName">The key name to get the value.</param>
        /// <returns>A string value or null</returns>
        private string GetDirectConnectValue(Dictionary<string, object> value, string keyName)
        {
            if (value.ContainsKey("appium:" + keyName))
            {
                return value["appium:" + keyName].ToString();
            }

            if (value.ContainsKey(keyName)) {
                return value[keyName].ToString();
            }

            return null;
        }
    }
}
