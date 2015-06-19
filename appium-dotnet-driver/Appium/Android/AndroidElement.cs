using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.Android
{
    public class AndroidElement : AppiumWebElement, IFindByAndroidUIAutomator<AppiumWebElement>
    {
        /// <summary>
        /// Initializes a new instance of the AndroidElement class.
        /// </summary>
        /// <param name="parent">Driver in use.</param>
        /// <param name="id">ID of the element.</param>
        public AndroidElement(RemoteWebDriver parent, string id)
            : base(parent, id)
        {
        }

        #region IFindByAndroidUIAutomator Members

        public AppiumWebElement FindElementByAndroidUIAutomator(string selector)
        {
            return (AppiumWebElement) this.FindElement("-android uiautomator", selector);
        }

        public ReadOnlyCollection<AppiumWebElement> FindElementsByAndroidUIAutomator(string selector)
        {
            return CollectionConverterUnility.
                            ConvertToExtendedWebElementCollection<AppiumWebElement>(this.FindElements("-android uiautomator", selector));
        }
        #endregion IFindByAndroidUIAutomator Members
    }
}
