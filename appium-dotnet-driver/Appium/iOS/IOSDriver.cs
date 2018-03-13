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
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.iOS.Interfaces;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.ObjectModel;

namespace OpenQA.Selenium.Appium.iOS
{
    public class IOSDriver<W> : AppiumDriver<W>, IFindByIosUIAutomation<W>, IFindsByIosClassChain<W>,
        IFindsByIosNSPredicate<W>, IHidesKeyboardWithKeyName,
        IShakesDevice, IPerformsTouchID where W : IWebElement
    {
        private static readonly string Platform = MobilePlatform.IOS;

        /// <summary>
        /// Initializes a new instance of the IOSDriver class
        /// </summary>
        /// <param name="commandExecutor">An <see cref="ICommandExecutor"/> object which executes commands for the driver.</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        public IOSDriver(ICommandExecutor commandExecutor, DesiredCapabilities desiredCapabilities)
            : base(commandExecutor, SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the IOSDriver class using desired capabilities
        /// </summary>
        /// <param name="desiredCapabilities">An <see cref="DesiredCapabilities"/> object containing the desired capabilities of the browser.</param>
        public IOSDriver(DesiredCapabilities desiredCapabilities)
            : base(SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the IOSDriver class using desired capabilities and command timeout
        /// </summary>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public IOSDriver(DesiredCapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(SetPlatformToCapabilities(desiredCapabilities, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the IOSDriver class using the AppiumServiceBuilder instance and desired capabilities
        /// </summary>
        /// <param name="builder"> object containing settings of the Appium local service which is going to be started</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        public IOSDriver(AppiumServiceBuilder builder, DesiredCapabilities desiredCapabilities)
            : base(builder, SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the IOSDriver class using the AppiumServiceBuilder instance, desired capabilities and command timeout
        /// </summary>
        /// <param name="builder"> object containing settings of the Appium local service which is going to be started</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public IOSDriver(AppiumServiceBuilder builder, DesiredCapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(builder, SetPlatformToCapabilities(desiredCapabilities, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the IOSDriver class using the specified remote address and desired capabilities
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="desiredCapabilities">An <see cref="DesiredCapabilities"/> object containing the desired capabilities.</param>
        public IOSDriver(Uri remoteAddress, DesiredCapabilities desiredCapabilities)
            : base(remoteAddress, SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the IOSDriver class using the specified Appium local service and desired capabilities
        /// </summary>
        /// <param name="service">the specified Appium local service</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities of the browser.</param>
        public IOSDriver(AppiumLocalService service, DesiredCapabilities desiredCapabilities)
            : base(service, SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the IOSDriver class using the specified remote address, desired capabilities, and command timeout.
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="desiredCapabilities">An <see cref="DesiredCapabilities"/> object containing the desired capabilities.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public IOSDriver(Uri remoteAddress, DesiredCapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(remoteAddress, SetPlatformToCapabilities(desiredCapabilities, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the IOSDriver class using the specified Appium local service, desired capabilities, and command timeout.
        /// </summary>
        /// <param name="service">the specified Appium local service</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public IOSDriver(AppiumLocalService service, DesiredCapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(service, SetPlatformToCapabilities(desiredCapabilities, Platform), commandTimeout)
        {
        }

        #region IFindByIosUIAutomation Members

        public W FindElementByIosUIAutomation(string selector) => FindElement(MobileSelector.iOSAutomatoion, selector);

        public ReadOnlyCollection<W> FindElementsByIosUIAutomation(string selector) =>
            FindElements(MobileSelector.iOSAutomatoion, selector);

        #endregion IFindByIosUIAutomation Members

        #region IFindsByIosClassChain Members

        public W FindElementByIosClassChain(string selector) => FindElement(MobileSelector.iOSClassChain, selector);

        public ReadOnlyCollection<W> FindElementsByIosClassChain(string selector) =>
            FindElements(MobileSelector.iOSClassChain, selector);

        #endregion IFindsByIosClassChain Members

        #region IFindsByIosNSPredicate Members

        public W FindElementByIosNsPredicate(string selector) =>
            FindElement(MobileSelector.iOSPredicateString, selector);

        public ReadOnlyCollection<W> FindElementsByIosNsPredicate(string selector) =>
            FindElements(MobileSelector.iOSPredicateString, selector);

        #endregion IFindsByIosNSPredicate Members

        public void ShakeDevice() => IOSCommandExecutionHelper.ShakeDevice(this);

        public void HideKeyboard(string key, string strategy = null) =>
            AppiumCommandExecutionHelper.HideKeyboard(this, strategy, key);

        protected override RemoteWebElement CreateElement(string elementId) => new IOSElement(this, elementId);

        /// <summary>
        /// Locks the device.
        /// </summary>
        /// <param name="seconds">The number of seconds during which the device need to be locked for.</param>
        public void Lock(int seconds) => AppiumCommandExecutionHelper.Lock(this, seconds);

        public void PerformTouchID(bool match) => IOSCommandExecutionHelper.PerformTouchID(this, match);

        public bool IsLocked() => IOSCommandExecutionHelper.IsLocked(this);

        public void Unlock() => IOSCommandExecutionHelper.Unlock(this);

        public void Lock() => IOSCommandExecutionHelper.Lock(this);

    }
}