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


using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Service;

namespace Appium.Net.Integration.Tests.ServerTests
{
    public class DirectConnectTest
    {

        [Test]
        public void WithAnEmptyBody()
        {
            var emptyBody = new Dictionary<string, object>();
            var response = new Response(null, emptyBody, WebDriverResult.Success);

            DirectConnect directConnect = new(response);
            Assert.That(directConnect.GetUri(), Is.Null);
        }


        [Test]
        public void WithAppiumPrefixResponsesWithAppium()
        {
            var body = new Dictionary<string, object>
            {
                ["appium:directConnectProtocol"] = "https",
                ["appium:directConnectHost"] = "example.com",
                ["appium:directConnectPort"] = "9090",
                ["appium:directConnectPath"] = "/path/to/new/direction"
            };

            var response = new Response(null, body, WebDriverResult.Success);

            var directConnect = new DirectConnect(response);
            Assert.That(directConnect.GetUri().ToString(), Is.EqualTo("https://example.com:9090/path/to/new/direction"));
        }

        [Test]
        public void WithAppiumPrefixResponsesWithoutAppium()
        {
            var body = new Dictionary<string, object>
            {
                ["directConnectProtocol"] = "https",
                ["directConnectHost"] = "example.com",
                ["directConnectPort"] = "9090",
                ["directConnectPath"] = "/path/to/new/direction"
            };

            var response = new Response(null, body, WebDriverResult.Success);

            var directConnect = new DirectConnect(response);
            Assert.That(directConnect.GetUri().ToString(), Is.EqualTo("https://example.com:9090/path/to/new/direction"));
        }

        [Test]
        public void WithAppiumPrefixResponsesInvalidProtocol()
        {
            var body = new Dictionary<string, object>
            {
                ["directConnectProtocol"] = "http",
                ["directConnectHost"] = "example.com",
                ["directConnectPort"] = "9090",
                ["directConnectPath"] = "/path/to/new/direction"
            };

            var response = new Response(null, body, WebDriverResult.InvalidArgument);

            var directConnect = new DirectConnect(response);
            Assert.That(directConnect.GetUri(), Is.Null);
        }
        [Test]
        public void WithMissingDirectConnectHost()
        {
            var body = new Dictionary<string, object>
            {
                ["appium:directConnectProtocol"] = "https",
                ["appium:directConnectPort"] = "9090",
                ["appium:directConnectPath"] = "/path/to/new/direction"
            };

            var response = new Response(null, body, WebDriverResult.Success);

            var directConnect = new DirectConnect(response);
            Assert.That(directConnect.GetUri(), Is.Null);
        }

    }
}
