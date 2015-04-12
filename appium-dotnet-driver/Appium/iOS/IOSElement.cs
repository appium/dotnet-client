using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.iOS
{
    public class IOSElement : AppiumWebElement, IFindByIosUIAutomation<AppiumWebElement>
    {
        /// <summary>
        /// Initializes a new instance of the IOSElement class.
        /// </summary>
        /// <param name="parent">Driver in use.</param>
        /// <param name="id">ID of the element.</param>
        public IOSElement(RemoteWebDriver parent, string id)
            : base(parent, id)
        {
        }


        #region IFindByIosUIAutomation Members

        public AppiumWebElement FindElementByIosUIAutomation(string selector)
        {
            return (AppiumWebElement) this.FindElement("-ios uiautomation", selector);
        }

        public ReadOnlyCollection<AppiumWebElement> FindElementsByIosUIAutomation(string selector)
        {
            return CollectionConverterUnility.
                            ConvertToExtendedWebElementCollection<AppiumWebElement>(this.FindElements("-ios uiautomation", selector));
        }
        #endregion IFindByIosUIAutomation Members
    }
}
