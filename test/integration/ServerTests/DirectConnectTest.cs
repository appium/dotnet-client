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

            Response response = new Response();
            Dictionary<string, object> emptyBody = new Dictionary<string, object>();

            response.Value = emptyBody;

            DirectConnect directConnect = new DirectConnect(response);
            Assert.That(directConnect.GetUri(), Is.Null);
        }


        [Test]
        public void WithAppiumPrefixResponsesWithAppium()
        {

            Response response = new Response();
            Dictionary<string, object> body = new Dictionary<string, object>();
            body["appium:directConnectProtocol"] = "https";
            body["appium:directConnectHost"] = "example.com";
            body["appium:directConnectPort"] = "9090";
            body["appium:directConnectPath"] = "/path/to/new/direction";

            response.Value = body;

            DirectConnect directConnect = new DirectConnect(response);
            Assert.That(directConnect.GetUri().ToString(), Is.EqualTo("https://example.com:9090/path/to/new/direction"));
        }

        [Test]
        public void WithAppiumPrefixResponsesWithoutAppium()
        {

            Response response = new Response();
            Dictionary<string, object> body = new Dictionary<string, object>();
            body["directConnectProtocol"] = "https";
            body["directConnectHost"] = "example.com";
            body["directConnectPort"] = "9090";
            body["directConnectPath"] = "/path/to/new/direction";

            response.Value = body;

            DirectConnect directConnect = new DirectConnect(response);
            Assert.That(directConnect.GetUri().ToString(), Is.EqualTo("https://example.com:9090/path/to/new/direction"));
        }

        [Test]
        public void WithAppiumPrefixResponsesInvalidProtocol()
        {

            Response response = new Response();
            Dictionary<string, object> body = new Dictionary<string, object>();
            body["directConnectProtocol"] = "http";
            body["directConnectHost"] = "example.com";
            body["directConnectPort"] = "9090";
            body["directConnectPath"] = "/path/to/new/direction";

            response.Value = body;

            DirectConnect directConnect = new DirectConnect(response);
            Assert.That(directConnect.GetUri(), Is.Null);
        }
    }
}
