using OpenQA.Selenium.Appium.src.Appium.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.Appium.iOS
{
    public class IOSElement : AppiumWebElement, IFindByIosUIAutomation
    {
        /// <summary>
        /// Initializes a new instance of the IOSElement class.
        /// </summary>
        /// <param name="parent">Driver in use.</param>
        /// <param name="id">ID of the element.</param>
        public IOSElement(AppiumDriver parent, string id)
            : base(parent, id)
        {
        }


        #region IFindByIosUIAutomation Members
        /// <summary>
        /// Finds the first of elements that match the Ios UIAutomation selector supplied
        /// </summary>
        /// <param name="selector">an Ios UIAutomation selector</param>
        /// <returns>IWebElement object so that you can interact that object</returns>
        public IWebElement FindElementByIosUIAutomation(string selector)
        {
            return this.FindElement("-ios uiautomation", selector);
        }

        /// <summary>
        /// Finds a list of elements that match the Ios UIAutomation selector supplied
        /// </summary>
        /// <param name="selector">an Ios UIAutomation selector</param>
        /// <returns>IWebElement object so that you can interact that object</returns>
        public ReadOnlyCollection<IWebElement> FindElementsByIosUIAutomation(string selector)
        {
            return this.FindElements("-ios uiautomation", selector);
        }
        #endregion IFindByIosUIAutomation Members
    }
}
