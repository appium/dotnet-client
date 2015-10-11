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
using OpenQA.Selenium.Remote;
using System;
using System.Collections.ObjectModel;

namespace OpenQA.Selenium.Appium.iOS
{
    public class IOSDriver<W> : AppiumDriver<W>, IFindByIosUIAutomation<W>, IIOSDeviceActionShortcuts where W : IWebElement 
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
        /// Finds the first element that matches the iOS UIAutomation selector
        /// </summary>
        /// <param name="selector">UIAutomation selector</param>
        /// <returns>First element found</returns>
        public W FindElementByIosUIAutomation(string selector)
        {
            return (W) this.FindElement("-ios uiautomation", selector);
        }

        /// <summary>
        /// Finds a list of elements that match the iOS UIAutomation selector
        /// </summary>
        /// <param name="selector">UIAutomation selector</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public ReadOnlyCollection<W> FindElementsByIosUIAutomation(string selector)
        {
            return CollectionConverterUnility.
                            ConvertToExtendedWebElementCollection<W>(this.FindElements("-ios uiautomation", selector));
        }
        #endregion IFindByIosUIAutomation Members

        /// <summary>
        /// Shakes the device.
        /// </summary>
        public void ShakeDevice()
        {
            this.Execute(AppiumDriverCommand.ShakeDevice, null);
        }

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
        /// Create an iOS Element
        /// </summary>
        /// <param name="elementId">element to create</param>
        /// <returns>IOSElement</returns>
        protected override RemoteWebElement CreateElement(string elementId)
        {
            return new IOSElement(this, elementId);
        }

        public override W ScrollTo(string text)
        {
            return (W) ((IScrollsTo<W>) FindElementByClassName("UIATableView")).ScrollTo(text);
        }

        public override W ScrollToExact(string text)
        {
            return (W)((IScrollsTo<W>)FindElementByClassName("UIATableView")).ScrollToExact(text);
        }
        
        public W GetNamedTextField(String name) 
        {
		    W element = FindElementByAccessibilityId(name);
		    if (element.TagName != "TextField") {
			    return (W) ((IFindByAccessibilityId<W>) element).FindElementByAccessibilityId(name);
		    }
		    return element;
	    }
    }
}
