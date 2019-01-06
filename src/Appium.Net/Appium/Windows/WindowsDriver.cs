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
using System.Collections.ObjectModel;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Remote;

namespace OpenQA.Selenium.Appium.Windows
{
    public class WindowsDriver<W> : AppiumDriver<W>, ISendsKeyEvents, IHidesKeyboardWithKeyName,
        IFindByWindowsUIAutomation<W> where W : IWebElement
    {
        private static readonly string Platform = MobilePlatform.Windows;

        /// <summary>
        /// Initializes a new instance of the WindowsDriver class using Appium options
        /// </summary>
        /// <param name="AppiumOptions">An <see cref="AppiumOptions"/> object containing the Appium options of the browser.</param>
        public WindowsDriver(AppiumOptions AppiumOptions)
            : base(SetPlatformToCapabilities(AppiumOptions, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the WindowsDriver class using Appium options and command timeout
        /// </summary>
        /// <param name="AppiumOptions">An <see cref="ICapabilities"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public WindowsDriver(AppiumOptions AppiumOptions, TimeSpan commandTimeout)
            : base(SetPlatformToCapabilities(AppiumOptions, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the WindowsDriver class using the AppiumServiceBuilder instance and Appium options
        /// </summary>
        /// <param name="builder"> object containing settings of the Appium local service which is going to be started</param>
        /// <param name="AppiumOptions">An <see cref="ICapabilities"/> object containing the Appium options.</param>
        public WindowsDriver(AppiumServiceBuilder builder, AppiumOptions AppiumOptions)
            : base(builder, SetPlatformToCapabilities(AppiumOptions, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the WindowsDriver class using the AppiumServiceBuilder instance, Appium options and command timeout
        /// </summary>
        /// <param name="builder"> object containing settings of the Appium local service which is going to be started</param>
        /// <param name="AppiumOptions">An <see cref="ICapabilities"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public WindowsDriver(AppiumServiceBuilder builder, AppiumOptions AppiumOptions,
            TimeSpan commandTimeout)
            : base(builder, SetPlatformToCapabilities(AppiumOptions, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the WindowsDriver class using the specified remote address and Appium options
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="AppiumOptions">An <see cref="AppiumOptions"/> object containing the Appium options.</param>
        public WindowsDriver(Uri remoteAddress, AppiumOptions AppiumOptions)
            : base(remoteAddress, SetPlatformToCapabilities(AppiumOptions, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the WindowsDriver class using the specified Appium local service and Appium options
        /// </summary>
        /// <param name="service">the specified Appium local service</param>
        /// <param name="AppiumOptions">An <see cref="ICapabilities"/> object containing the Appium options of the browser.</param>
        public WindowsDriver(AppiumLocalService service, AppiumOptions AppiumOptions)
            : base(service, SetPlatformToCapabilities(AppiumOptions, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the WindowsDriver class using the specified remote address, Appium options, and command timeout.
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="AppiumOptions">An <see cref="AppiumOptions"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public WindowsDriver(Uri remoteAddress, AppiumOptions AppiumOptions, TimeSpan commandTimeout)
            : base(remoteAddress, SetPlatformToCapabilities(AppiumOptions, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the WindowsDriver class using the specified Appium local service, Appium options, and command timeout.
        /// </summary>
        /// <param name="service">the specified Appium local service</param>
        /// <param name="AppiumOptions">An <see cref="ICapabilities"/> object containing the Appium options.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public WindowsDriver(AppiumLocalService service, AppiumOptions AppiumOptions,
            TimeSpan commandTimeout)
            : base(service, SetPlatformToCapabilities(AppiumOptions, Platform), commandTimeout)
        {
        }

        #region Contex

        public override string Context
        {
            get => "NATIVE_APP";
            set => throw new NotImplementedException();
        }

        public override ReadOnlyCollection<string> Contexts
        {
            get
            {
                var contexts = new List<string> { Context };
                return contexts.AsReadOnly();
            }
        }

        #endregion Context

        #region IFindByWindowsUIAutomation Members

        /// <summary>
        /// Finds the first of elements that match the Windows UIAutomation selector supplied
        /// </summary>
        /// <param name="selector">a Windows UIAutomation selector</param>
        /// <returns>IWebElement object so that you can interact that object</returns>
        public W FindElementByWindowsUIAutomation(string selector) =>
            FindElement(MobileSelector.WindowsUIAutomation, selector);

        /// <summary>
        /// Finds a list of elements that match the Windows UIAutomation selector supplied
        /// </summary>
        /// <param name="selector">a Windows UIAutomation selector</param>
        /// <returns>ReadOnlyCollection of IWebElement objects so that you can interact with those objects</returns>
        public IReadOnlyCollection<W> FindElementsByWindowsUIAutomation(string selector) =>
            FindElements(MobileSelector.WindowsUIAutomation, selector);

        #endregion IFindByWindowsUIAutomation Members

        public void HideKeyboard(string key, string strategy = null) =>
            AppiumCommandExecutionHelper.HideKeyboard(this, strategy, key);

        protected override RemoteWebElementFactory CreateElementFactory() => new WindowsElementFactory(this);

        public void PressKeyCode(int keyCode, int metastate = -1) =>
            AppiumCommandExecutionHelper.PressKeyCode(this, keyCode, metastate);

        public void LongPressKeyCode(int keyCode, int metastate = -1) =>
            AppiumCommandExecutionHelper.LongPressKeyCode(this, keyCode, metastate);
    }
}