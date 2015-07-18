using OpenQA.Selenium.Appium.PageObjects.Attributes.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.PageObjects.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = true)]
    public class FindsBySelendroidAttribute: FindsByMobileAttribute
    {
        /// <summary>
        /// Sets the target element link text.
        /// This locator is supported by Selendroid
        /// </summary>
        public String LinkText
        {
            set
            {
                byList.Add(By.LinkText(value));
            }
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Sets the target element partial link text
        /// This locator is supported by Selendroid
        /// </summary>
        public String PartialLinkText
        {
            set
            {
                byList.Add(By.PartialLinkText(value));
            }
            get
            {
                return null;
            }
        }
    }
}
