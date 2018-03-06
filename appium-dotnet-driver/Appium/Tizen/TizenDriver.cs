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

using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Remote;
using System;

namespace OpenQA.Selenium.Appium.Tizen
{
    public class TizenDriver<W> : AppiumDriver<W>
         where W : IWebElement
    {
        private static readonly string Platform = MobilePlatform.Tizen;

        // TODO: This feature may not work properly at the moment.
        public TizenDriver(ICommandExecutor commandExecutor, DesiredCapabilities desiredCapabilities)
            : base(commandExecutor, SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        // TODO: This feature may not work properly at the moment.
        public TizenDriver(DesiredCapabilities desiredCapabilities)
            : base(SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        // TODO: This feature may not work properly at the moment.
        public TizenDriver(DesiredCapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(SetPlatformToCapabilities(desiredCapabilities, Platform), commandTimeout)
        {
        }

        // TODO: This feature may not work properly at the moment.
        public TizenDriver(AppiumServiceBuilder builder, DesiredCapabilities desiredCapabilities)
            : base(builder, SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        // TODO: This feature may not work properly at the moment.
        public TizenDriver(AppiumServiceBuilder builder, DesiredCapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(builder, SetPlatformToCapabilities(desiredCapabilities, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TizenDriver class using the specified remote address and desired capabilities
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="desiredCapabilities">An <see cref="DesiredCapabilities"/> object containing the desired capabilities.</param>
        public TizenDriver(Uri remoteAddress, DesiredCapabilities desiredCapabilities)
            : base(remoteAddress, SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        // TODO: This feature may not work properly at the moment.
        public TizenDriver(AppiumLocalService service, DesiredCapabilities desiredCapabilities)
            : base(service, SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        // TODO: This feature may not work properly at the moment.
        public TizenDriver(Uri remoteAddress, DesiredCapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(remoteAddress, SetPlatformToCapabilities(desiredCapabilities, Platform), commandTimeout)
        {
        }

        // TODO: This feature may not work properly at the moment.
        public TizenDriver(AppiumLocalService service, DesiredCapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(service, SetPlatformToCapabilities(desiredCapabilities, Platform), commandTimeout)
        {
        }

        protected override RemoteWebElement CreateElement(string elementId) => new TizenElement(this, elementId);
    }
}
