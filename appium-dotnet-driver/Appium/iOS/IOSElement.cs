using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.iOS
{
    public class IOSElement : AppiumWebElement, 
        IFindByIosUIAutomation<AppiumWebElement>, 
        IScrollsTo<AppiumWebElement>
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

        #region IScrollsTo Members

        /// <summary>
        /// Scroll to the element whose 'text' attribute contains the input text.
        /// Scrolling happens within this element
        /// </summary>
        /// <param name="text">input text contained in text attribute</param>
        public AppiumWebElement ScrollTo(string text)
        {
            return (IOSElement) FindElementByIosUIAutomation(".scrollToElementWithPredicate(\"name CONTAINS '" + text + "'\")");
        }

        /// <summary>
        /// Scroll to the element whose 'text' attribute matches the input text.
        /// Scrolling happens within this element
        /// </summary>
        /// <param name="text">input text contained in text attribute</param>
        public AppiumWebElement ScrollToExact(string text)
        {
            return (IOSElement) FindElementByIosUIAutomation(".scrollToElementWithName(\"" + text + "\")");
        }

        #endregion
    }
}
