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
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Service;

namespace Appium.Net.Integration.Tests.ServerTests
{
    [TestFixture]
    public class RelaxSslValidationTest
    {
        private AppiumDriver _driver;
        AppiumClientConfig appiumClient;
        Uri serverUri;
        AppiumOptions capabilities;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            capabilities = Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));
            // TODO: Create serverUri that will redirect to https://
            serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            appiumClient = AppiumClientConfig.DefaultConfig();
        }

        [Test]
        public void TestRelaxSslValidationTrue()
        {
            appiumClient.RelaxSslValidation = true;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec, appiumClient);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
            Assert.That(_driver, Is.Not.Null, "Driver initialization failed.");
            Assert.That(_driver.SessionId, Is.Not.Null, "Driver session ID is null.");
        }

        [Test]
        public void TestRelaxSslValidationFalse()
        {
            appiumClient.RelaxSslValidation = false;
            try
            {
                _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec, appiumClient);
            }
            catch (Exception e)
            {
                var innerExceptionMessage = e.InnerException.InnerException.Message;
                using (Assert.EnterMultipleScope())
                {

#if NET48_OR_GREATER
                    Assert.That(innerExceptionMessage, Does.Contain("Could not establish trust relationship for the SSL/TLS secure channel."));
#endif
#if NET5_0_OR_GREATER
                    Assert.That(e.Message, Does.Contain("The SSL connection could not be established, see inner exception."));
#endif
                    Assert.That(_driver, Is.Null, "Driver initialization failed as it should.");

                }
            }
        }

        [TearDown]
        public void TearDown()
        {
            _driver?.Dispose();
            _driver = null;
        }

    }
}
