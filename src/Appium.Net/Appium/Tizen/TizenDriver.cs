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
using System;
using System.Collections.Generic;

namespace OpenQA.Selenium.Appium.Tizen
{
    public class TizenDriver : AppiumDriver
    {
        private static readonly string Platform = MobilePlatform.Tizen;

        /// <summary>
        /// Initializes a new instance of the TizenDriver class
        /// </summary>
        /// <param name="commandExecutor">An <see cref="ICommandExecutor"/> object which executes commands for the driver.</param>
        /// <param name="AppiumOptions">An <see cref="ICapabilities"/> object containing the Appium options.</param>
        public TizenDriver(ICommandExecutor commandExecutor, AppiumOptions AppiumOptions)
            : base(commandExecutor, SetPlatformToCapabilities(AppiumOptions, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the TizenDriver class using Appium options
        /// </summary>
        /// <param name="AppiumOptions">An <see cref="AppiumOptions"/> object containing the Appium options of the browser.</param>
        public TizenDriver(AppiumOptions AppiumOptions)
            : base(SetPlatformToCapabilities(AppiumOptions, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the TizenDriver class using Appium options and command timeout
        /// </summary>
        /// <param name="AppiumOptions">An <see cref="ICapabilities"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public TizenDriver(AppiumOptions AppiumOptions, TimeSpan commandTimeout)
            : base(SetPlatformToCapabilities(AppiumOptions, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TizenDriver class using the AppiumServiceBuilder instance and Appium options
        /// </summary>
        /// <param name="builder"> object containing settings of the Appium local service which is going to be started</param>
        /// <param name="AppiumOptions">An <see cref="ICapabilities"/> object containing the Appium options.</param>
        public TizenDriver(AppiumServiceBuilder builder, AppiumOptions AppiumOptions)
            : base(builder, SetPlatformToCapabilities(AppiumOptions, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the TizenDriver class using the AppiumServiceBuilder instance, Appium options and command timeout
        /// </summary>
        /// <param name="builder"> object containing settings of the Appium local service which is going to be started</param>
        /// <param name="AppiumOptions">An <see cref="ICapabilities"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public TizenDriver(AppiumServiceBuilder builder, AppiumOptions AppiumOptions, TimeSpan commandTimeout)
            : base(builder, SetPlatformToCapabilities(AppiumOptions, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TizenDriver class using the specified remote address and Appium options
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="AppiumOptions">An <see cref="AppiumOptions"/> object containing the Appium options.</param>
        public TizenDriver(Uri remoteAddress, AppiumOptions AppiumOptions)
            : base(remoteAddress, SetPlatformToCapabilities(AppiumOptions, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the TizenDriver class using the specified Appium local service and Appium options
        /// </summary>
        /// <param name="service">the specified Appium local service</param>
        /// <param name="AppiumOptions">An <see cref="ICapabilities"/> object containing the Appium options of the browser.</param>
        public TizenDriver(AppiumLocalService service, AppiumOptions AppiumOptions)
            : base(service, SetPlatformToCapabilities(AppiumOptions, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the TizenDriver class using the specified remote address, Appium options, and command timeout.
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="AppiumOptions">An <see cref="AppiumOptions"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public TizenDriver(Uri remoteAddress, AppiumOptions AppiumOptions, TimeSpan commandTimeout)
            : base(remoteAddress, SetPlatformToCapabilities(AppiumOptions, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TizenDriver class using the specified Appium local service, Appium options, and command timeout.
        /// </summary>
        /// <param name="service">the specified Appium local service</param>
        /// <param name="AppiumOptions">An <see cref="ICapabilities"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public TizenDriver(AppiumLocalService service, AppiumOptions AppiumOptions, TimeSpan commandTimeout)
            : base(service, SetPlatformToCapabilities(AppiumOptions, Platform), commandTimeout)
        {
        }


        /// <summary>
        /// Initializes a new instance of the TizenDriver class using the specified remote address, Appium options and AppiumClientConfig.
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        /// <param name="clientConfig">An instance of <see cref="AppiumClientConfig"/></param>
        public TizenDriver(Uri remoteAddress, DriverOptions driverOptions, AppiumClientConfig clientConfig)
            : base(remoteAddress, SetPlatformToCapabilities(driverOptions, Platform), clientConfig)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TizenDriver class using the specified Appium local service, Appium options and AppiumClientConfig,
        /// </summary>
        /// <param name="service">the specified Appium local service</param>
        /// <param name="driverOptions">An <see cref="ICapabilities"/> object containing the Appium options.</param>
        /// <param name="clientConfig">An instance of <see cref="AppiumClientConfig"/></param>
        public TizenDriver(AppiumLocalService service, DriverOptions driverOptions, AppiumClientConfig clientConfig)
            : base(service, SetPlatformToCapabilities(driverOptions, Platform), clientConfig)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TizenDriver class using the specified remote address, Appium options, command timeout and AppiumClientConfig.
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        /// <param name="clientConfig">An instance of <see cref="AppiumClientConfig"/></param>
        public TizenDriver(Uri remoteAddress, DriverOptions driverOptions, TimeSpan commandTimeout, AppiumClientConfig clientConfig)
            : base(remoteAddress, SetPlatformToCapabilities(driverOptions, Platform), commandTimeout, clientConfig)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TizenDriver class using the specified Appium local service, Appium options, command timeout and AppiumClientConfig,
        /// </summary>
        /// <param name="service">the specified Appium local service</param>
        /// <param name="driverOptions">An <see cref="ICapabilities"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        /// <param name="clientConfig">An instance of <see cref="AppiumClientConfig"/></param>
        public TizenDriver(AppiumLocalService service, DriverOptions driverOptions, TimeSpan commandTimeout, AppiumClientConfig clientConfig)
            : base(service, SetPlatformToCapabilities(driverOptions, Platform), commandTimeout, clientConfig)
        {
        }
    }
}
