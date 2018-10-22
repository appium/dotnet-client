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
        IFindsByIosNSPredicate<W>, IHidesKeyboardWithKeyName, IHasClipboard
        IShakesDevice, IPerformsTouchID where W : IWebElement
    {
        private static readonly string Platform = MobilePlatform.IOS;

        /// <summary>
        /// Initializes a new instance of the IOSDriver class
        /// </summary>
        /// <param name="commandExecutor">An <see cref="ICommandExecutor"/> object which executes commands for the driver.</param>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        public IOSDriver(ICommandExecutor commandExecutor, DriverOptions driverOptions)
            : base(commandExecutor, SetPlatformToCapabilities(driverOptions, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the IOSDriver class using Appium options
        /// </summary>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options of the browser.</param>
        public IOSDriver(DriverOptions driverOptions)
            : base(SetPlatformToCapabilities(driverOptions, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the IOSDriver class using Appium options and command timeout
        /// </summary>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public IOSDriver(DriverOptions driverOptions, TimeSpan commandTimeout)
            : base(SetPlatformToCapabilities(driverOptions, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the IOSDriver class using the AppiumServiceBuilder instance and Appium options
        /// </summary>
        /// <param name="builder"> object containing settings of the Appium local service which is going to be started</param>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        public IOSDriver(AppiumServiceBuilder builder, DriverOptions driverOptions)
            : base(builder, SetPlatformToCapabilities(driverOptions, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the IOSDriver class using the AppiumServiceBuilder instance, Appium options and command timeout
        /// </summary>
        /// <param name="builder"> object containing settings of the Appium local service which is going to be started</param>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public IOSDriver(AppiumServiceBuilder builder, DriverOptions driverOptions, TimeSpan commandTimeout)
            : base(builder, SetPlatformToCapabilities(driverOptions, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the IOSDriver class using the specified remote address and Appium options
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="DriverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        public IOSDriver(Uri remoteAddress, DriverOptions driverOptions)
            : base(remoteAddress, SetPlatformToCapabilities(driverOptions, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the IOSDriver class using the specified Appium local service and Appium options
        /// </summary>
        /// <param name="service">the specified Appium local service</param>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options of the browser.</param>
        public IOSDriver(AppiumLocalService service, DriverOptions driverOptions)
            : base(service, SetPlatformToCapabilities(driverOptions, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the IOSDriver class using the specified remote address, Appium options, and command timeout.
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="DriverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public IOSDriver(Uri remoteAddress, DriverOptions driverOptions, TimeSpan commandTimeout)
            : base(remoteAddress, SetPlatformToCapabilities(driverOptions, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the IOSDriver class using the specified Appium local service, Appium options, and command timeout.
        /// </summary>
        /// <param name="service">the specified Appium local service</param>
        /// <param name="driverOptions">An <see cref="DriverOptions"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public IOSDriver(AppiumLocalService service, DriverOptions driverOptions, TimeSpan commandTimeout)
            : base(service, SetPlatformToCapabilities(driverOptions, Platform), commandTimeout)
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

        protected override RemoteWebElementFactory CreateElementFactory() => new IOSElementFactory(this);

        /// <summary>
        /// Locks the device.
        /// </summary>
        /// <param name="seconds">The number of seconds during which the device need to be locked for.</param>
        public void Lock(int seconds) => AppiumCommandExecutionHelper.Lock(this, seconds);

        public void PerformTouchID(bool match) => IOSCommandExecutionHelper.PerformTouchID(this, match);

        public bool IsLocked() => IOSCommandExecutionHelper.IsLocked(this);

        public void Unlock() => IOSCommandExecutionHelper.Unlock(this);

        public void Lock() => IOSCommandExecutionHelper.Lock(this);

        /// <inheritdoc />
        public void SetClipboard(byte[] base64Content, ClipboardContentType contentType, string label = null) =>
            AppiumCommandExecutionHelper.SetClipboard(this, contentType, base64Content, label);

        /// <inheritdoc />
        public void SetClipboardText(string textContent) => 
            AppiumCommandExecutionHelper.SetClipboardText(this, ClipboardContentType.PlainText, textContent, null);
        
        /// <inheritdoc />
        public string GetClipboard(ClipboardContentType contentType) => AppiumCommandExecutionHelper.GetClipboard(this, contentType);
    }
}