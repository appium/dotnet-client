using OpenQA.Selenium.Appium.PageObjects.Attributes.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.PageObjects.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = true)]
    public class FindsByIOSUIAutomationAttribute : FindsByUIAutomatorsAttribute
    {
        /// <summary>
        /// Sets the target UI automation locator
        /// </summary>
        public String IosUIAutomation
        {
            set
            {
                byList.Add(new ByIosUIAutomation(value));
            }
            get
            {
                return null;
            }
        }
    }
}
