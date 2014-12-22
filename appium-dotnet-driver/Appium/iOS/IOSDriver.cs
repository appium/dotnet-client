using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS.Interfaces;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.iOS
{
    public class IOSDriver : AppiumDriver, IFindByIosUIAutomation, IIOSDeviceActionShortcuts
    {
        private static readonly string Platform = MobilePlatform.IOS;
         /// <summary>
        /// Initializes a new instance of the IOSDriver class
        /// </summary>
        /// <param name="commandExecutor">An <see cref="ICommandExecutor"/> object which executes commands for the driver.</param>
        /// <param name="desiredCapabilities">An <see cref="DesiredCapabilities"/> object containing the desired capabilities of the browser.</param>
        public IOSDriver(ICommandExecutor commandExecutor, DesiredCapabilities desiredCapabilities)
            : base(commandExecutor, SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the IOSDriver class. This constructor defaults proxy to http://127.0.0.1:4723/wd/hub
        /// </summary>
        /// <param name="desiredCapabilities">An <see cref="DesiredCapabilities"/> object containing the desired capabilities of the browser.</param>
        public IOSDriver(DesiredCapabilities desiredCapabilities)
            : base(SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the IOSDriver class
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="desiredCapabilities">An <see cref="DesiredCapabilities"/> object containing the desired capabilities of the browser.</param>
        public IOSDriver(Uri remoteAddress, DesiredCapabilities desiredCapabilities)
            : base(remoteAddress, SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the IOSDriver class using the specified remote address, desired capabilities, and command timeout.
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="desiredCapabilities">An <see cref="DesiredCapabilities"/> object containing the desired capabilities of the browser.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public IOSDriver(Uri remoteAddress, DesiredCapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(remoteAddress, SetPlatformToCapabilities(desiredCapabilities, Platform), commandTimeout)
        {
        }

        #region IFindByIosUIAutomation Members
        /// <summary>
        /// Finds the first element in the page that matches the Ios UIAutomation selector supplied
        /// </summary>
        /// <param name="selector">Selector for the element.</param>
        /// <returns>IWebElement object so that you can interact that object</returns>
        /// <example>
        /// <code>
        /// IWebDriver driver = new RemoteWebDriver(DesiredCapabilities.Firefox());
        /// IWebElement elem = driver.FindElementByIosUIAutomation('elements()'))
        /// </code>
        /// </example>
        public IWebElement FindElementByIosUIAutomation(string selector)
        {
            return this.FindElement("-ios uiautomation", selector);
        }

        /// <summary>
        /// Finds a list of elements that match the Ios UIAutomation selector supplied
        /// </summary>
        /// <param name="selector">Selector for the elements.</param>
        /// <returns>ReadOnlyCollection of IWebElement object so that you can interact with those objects</returns>
        /// <example>
        /// <code>
        /// IWebDriver driver = new RemoteWebDriver(DesiredCapabilities.Firefox());
        /// ReadOnlyCollection<![CDATA[<IWebElement>]]> elem = driver.FindElementsByIosUIAutomation(elements())
        /// </code>
        /// </example>
        public ReadOnlyCollection<IWebElement> FindElementsByIosUIAutomation(string selector)
        {
            return this.FindElements("-ios uiautomation", selector);
        }
        #endregion IFindByIosUIAutomation Members

        /// <summary>
        /// Shakes the device.
        /// </summary>
        public void ShakeDevice()
        {
            this.Execute(AppiumDriverCommand.ShakeDevice, null);
        }

        public void HideKeyboard(string key, string strategy = null)
        {
            base.HideKeyboard(strategy, key);
        }


        protected override RemoteWebElement CreateElement(string elementId)
        {
            return new IOSElement(this, elementId);
        }
    }
}
