using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Remote;

namespace OpenQA.Selenium.Appium.Windows
{
    public class WindowsDriver<W> : AppiumDriver<W>, IFindByWindowsUIAutomation<W> where W : IWebElement
    {
        private static readonly string Platform = MobilePlatform.Windows;

        /// <summary>
        /// Initializes a new instance of the WindowsDriver class using desired capabilities
        /// </summary>
        /// <param name="desiredCapabilities">An <see cref="DesiredCapabilities"/> object containing the desired capabilities of the browser.</param>
        public WindowsDriver(DesiredCapabilities desiredCapabilities)
            : base(SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the WindowsDriver class using desired capabilities and command timeout
        /// </summary>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public WindowsDriver(DesiredCapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(SetPlatformToCapabilities(desiredCapabilities, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the WindowsDriver class using the AppiumServiceBuilder instance and desired capabilities
        /// </summary>
        /// <param name="builder"> object containing settings of the Appium local service which is going to be started</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        public WindowsDriver(AppiumServiceBuilder builder, DesiredCapabilities desiredCapabilities)
            : base(builder, SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the WindowsDriver class using the AppiumServiceBuilder instance, desired capabilities and command timeout
        /// </summary>
        /// <param name="builder"> object containing settings of the Appium local service which is going to be started</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public WindowsDriver(AppiumServiceBuilder builder, DesiredCapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(builder, SetPlatformToCapabilities(desiredCapabilities, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the WindowsDriver class using the specified remote address and desired capabilities
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="desiredCapabilities">An <see cref="DesiredCapabilities"/> object containing the desired capabilities.</param>
        public WindowsDriver(Uri remoteAddress, DesiredCapabilities desiredCapabilities)
            : base(remoteAddress, SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the WindowsDriver class using the specified Appium local service and desired capabilities
        /// </summary>
        /// <param name="service">the specified Appium local service</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities of the browser.</param>
        public WindowsDriver(AppiumLocalService service, DesiredCapabilities desiredCapabilities)
            : base(service, SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the WindowsDriver class using the specified remote address, desired capabilities, and command timeout.
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="desiredCapabilities">An <see cref="DesiredCapabilities"/> object containing the desired capabilities.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public WindowsDriver(Uri remoteAddress, DesiredCapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(remoteAddress, SetPlatformToCapabilities(desiredCapabilities, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the WindowsDriver class using the specified Appium local service, desired capabilities, and command timeout.
        /// </summary>
        /// <param name="service">the specified Appium local service</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public WindowsDriver(AppiumLocalService service, DesiredCapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(service, SetPlatformToCapabilities(desiredCapabilities, Platform), commandTimeout)
        {
        }

        #region IFindByWindowsUIAutomation Members

        /// <summary>
        /// Finds the first element that matches the Windows UIAutomation selector
        /// </summary>
        /// <param name="selector">UIAutomation selector</param>
        /// <returns>First element found</returns>
        public W FindElementByWindowsUIAutomation(string selector)
        {
            return (W)this.FindElement("-windows uiautomation", selector);
        }

        /// <summary>
        /// Finds a list of elements that match the Windows UIAutomation selector
        /// </summary>
        /// <param name="selector">UIAutomation selector</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public ReadOnlyCollection<W> FindElementsByWindowsUIAutomation(string selector)
        {
            return CollectionConverterUnility.ConvertToExtendedWebElementCollection<W>(
                this.FindElements("-windows uiautomation", selector));
        }

        #endregion IFindByWindowsUIAutomation Members

        /// <summary>
        /// Hides the keyboard
        /// </summary>
        /// <param name="key"></param>
        /// <param name="strategy"></param>
        public new void HideKeyboard(string key, string strategy = null)
        {
            base.HideKeyboard(strategy, key);
        }

        /// <summary>
        /// Create a Windows Element
        /// </summary>
        /// <param name="elementId">element to create</param>
        /// <returns>WindowsElement</returns>
        protected override RemoteWebElement CreateElement(string elementId)
        {
            return new WindowsElement(this, elementId);
        }

        public override W ScrollTo(string text)
        {
            throw new NotImplementedException();
        }

        public override W ScrollToExact(string text)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sends a device key event with metastate
        /// </summary>
        /// <param name="keyCode">Code for the long key pressed on the Windows device</param>
        /// <param name="metastate">metastate for the long key press</param>
        public void PressKeyCode(int keyCode, int metastate = -1)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("keycode", keyCode);
            if (metastate > 0)
            {
                parameters.Add("metastate", metastate);
            }
            Execute(AppiumDriverCommand.PressKeyCode, parameters);
        }

        /// <summary>
        /// Sends a device long key event with metastate
        /// </summary>
        /// <param name="keyCode">Code for the long key pressed on the Windows device</param>
        /// <param name="metastate">metastate for the long key press</param>
        public void LongPressKeyCode(int keyCode, int metastate = -1)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("keycode", keyCode);
            if (metastate > 0)
            {
                parameters.Add("metastate", metastate);
            }
            Execute(AppiumDriverCommand.LongPressKeyCode, parameters);
        }
    }
}