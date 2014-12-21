using OpenQA.Selenium.Appium.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.Android
{
    public class AndroidElement : AppiumWebElement, IFindByAndroidUIAutomator
    {
        /// <summary>
        /// Initializes a new instance of the AndroidElement class.
        /// </summary>
        /// <param name="parent">Driver in use.</param>
        /// <param name="id">ID of the element.</param>
        public AndroidElement(AppiumDriver parent, string id)
            : base(parent, id)
        {
        }

        #region IFindByAndroidUIAutomator Members
        /// <summary>
        /// Finds the first of elements that match the Android UIAutomator selector supplied
        /// </summary>
        /// <param name="selector">an Android UIAutomator selector</param>
        /// <returns>IWebElement object so that you can interact that object</returns>
        public IWebElement FindElementByAndroidUIAutomator(string selector)
        {
            return this.FindElement("-android uiautomator", selector);
        }

        /// <summary>
        /// Finds a list of elements that match the Android UIAutomator selector supplied
        /// </summary>
        /// <param name="selector">an Android UIAutomator selector</param>
        /// <returns>IWebElement object so that you can interact that object</returns>
        public ReadOnlyCollection<IWebElement> FindElementsByAndroidUIAutomator(string selector)
        {
            return this.FindElements("-android uiautomator", selector);
        }
        #endregion IFindByAndroidUIAutomator Members
    }
}
